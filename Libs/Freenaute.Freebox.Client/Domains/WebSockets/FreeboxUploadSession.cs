using System.Buffers;
using System.Runtime.ExceptionServices;
using System.Text.Json;
using Freenaute.Freebox.Mapper.Contracts.WebSockets;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.WebSockets;

/// <summary>
/// One upload state machine. A dedicated reader receives progress while data is pipelined by a serialized writer.
/// Closing/disposal retains a partial upload. Only CancelAsync sends the destructive upload_cancel action.
/// </summary>
public sealed class FreeboxUploadSession : IAsyncDisposable
{
    internal const long StartRequestId = 1;
    private const long FinalizeRequestId = 2;
    private const long CancelRequestId = 3;
    private static WebSocketJsonSerializerContext Json => WebSocketJsonSerializerContext.Default;
    private readonly FreeboxWebSocketConnection _connection;
    private readonly int _chunkSize;
    private readonly long? _expectedSize;
    private readonly CancellationTokenSource _receiveStop = new();
    private readonly CancellationTokenSource _failureStop = new();
    private readonly SemaphoreSlim _writer = new(1, 1);
    private readonly TaskCompletionSource<FileUploadControlResponse> _start = NewAcknowledgement();
    private readonly TaskCompletionSource<FileUploadControlResponse> _finalize = NewAcknowledgement();
    private readonly TaskCompletionSource<FileUploadControlResponse> _cancel = NewAcknowledgement();
    private readonly TaskCompletionSource<Exception> _failure = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private readonly Task _reader;
    private IProgress<FreeboxUploadProgress>? _progress;
    private long _bytesSent;
    private int _state = (int)FreeboxUploadState.Starting;
    private int _disposed;
    private int _dataStarted;
    private int _finalizeSent;
    private int _cancelSent;

    private FreeboxUploadSession(FreeboxWebSocketConnection connection, int chunkSize, long? expectedSize)
    {
        _connection = connection;
        _chunkSize = chunkSize;
        _expectedSize = expectedSize;
        _reader = ReadResponsesAsync();
    }

    public FreeboxUploadState State => (FreeboxUploadState)Volatile.Read(ref _state);
    public long BytesSent => Interlocked.Read(ref _bytesSent);

    internal static async Task<FreeboxUploadSession> StartAsync(FreeboxWebSocketConnection connection,
        FileUploadStartRequest request, int chunkSize, CancellationToken cancellationToken)
    {
        var session = new FreeboxUploadSession(connection, chunkSize, request.Size);
        try
        {
            await connection.SendJsonAsync(request, Json.FileUploadStartRequest, cancellationToken).ConfigureAwait(false);
            await session.AwaitAcknowledgementAsync(session._start.Task, cancellationToken).ConfigureAwait(false);
            Volatile.Write(ref session._state, (int)FreeboxUploadState.Ready);
            return session;
        }
        catch
        {
            await session.DisposeAsync().ConfigureAwait(false);
            throw;
        }
    }

    /// <summary>Sends file content without waiting for each upload_data reply, then finalizes. Caller owns source.</summary>
    public async Task<FreeboxUploadResult> SendAsync(Stream source, CancellationToken cancellationToken = default,
        IProgress<FreeboxUploadProgress>? progress = null)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (!source.CanRead) throw new ArgumentException("The upload stream must be readable.", nameof(source));
        await _writer.WaitAsync(cancellationToken).ConfigureAwait(false);
        byte[]? buffer = null;
        try
        {
            EnsureReady();
            Volatile.Write(ref _progress, progress);
            Volatile.Write(ref _state, (int)FreeboxUploadState.Sending);
            using var stop = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _failureStop.Token, _receiveStop.Token);
            buffer = ArrayPool<byte>.Shared.Rent(_chunkSize);
            while (true)
            {
                ThrowIfFailed();
                var count = await source.ReadAsync(buffer.AsMemory(0, _chunkSize), stop.Token).ConfigureAwait(false);
                if (count == 0) break;
                Volatile.Write(ref _dataStarted, 1);
                await _connection.SendBinaryAsync(buffer.AsMemory(0, count), stop.Token).ConfigureAwait(false);
                Interlocked.Add(ref _bytesSent, count);
            }
            return await FinalizeCoreAsync(stop.Token).ConfigureAwait(false);
        }
        catch
        {
            await DisposeAsync().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfFailed();
            throw;
        }
        finally
        {
            if (buffer is not null) ArrayPool<byte>.Shared.Return(buffer, clearArray: true);
            _writer.Release();
        }
    }

    /// <summary>Advanced explicit chunk operation; allows callers to choose finalize or destructive cancel later.</summary>
    public async Task SendChunkAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    {
        if (data.Length > 1_000_000) throw new ArgumentOutOfRangeException(nameof(data));
        await _writer.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            EnsureReady();
            if (data.IsEmpty) return;
            using var stop = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _failureStop.Token, _receiveStop.Token);
            Volatile.Write(ref _dataStarted, 1);
            await _connection.SendBinaryAsync(data, stop.Token).ConfigureAwait(false);
            Interlocked.Add(ref _bytesSent, data.Length);
        }
        catch
        {
            await DisposeAsync().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfFailed();
            throw;
        }
        finally { _writer.Release(); }
    }

    public async Task<FreeboxUploadResult> FinalizeAsync(CancellationToken cancellationToken = default)
    {
        await _writer.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            EnsureReady();
            using var stop = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _failureStop.Token, _receiveStop.Token);
            return await FinalizeCoreAsync(stop.Token).ConfigureAwait(false);
        }
        catch
        {
            await DisposeAsync().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfFailed();
            throw;
        }
        finally { _writer.Release(); }
    }

    /// <summary>Explicitly deletes the partial destination according to FileUploadCancelAction. Never called on token cancellation.</summary>
    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        await _writer.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            EnsureReady();
            using var stop = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _failureStop.Token, _receiveStop.Token);
            Volatile.Write(ref _cancelSent, 1);
            await _connection.SendJsonAsync(new FileUploadCancelRequest { RequestId = CancelRequestId },
                Json.FileUploadCancelRequest, stop.Token).ConfigureAwait(false);
            var response = await AwaitAcknowledgementAsync(_cancel.Task, stop.Token).ConfigureAwait(false);
            if (response.Result?.Complete != true || response.Result.Cancelled != true)
                throw new JsonException("The upload_cancel acknowledgement must confirm complete and cancelled.");
            Volatile.Write(ref _state, (int)FreeboxUploadState.Cancelled);
        }
        catch
        {
            await DisposeAsync().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfFailed();
            throw;
        }
        finally { _writer.Release(); }
    }

    private async Task<FreeboxUploadResult> FinalizeCoreAsync(CancellationToken cancellationToken)
    {
        ThrowIfFailed();
        Volatile.Write(ref _finalizeSent, 1);
        await _connection.SendJsonAsync(new FileUploadFinalizeRequest { RequestId = FinalizeRequestId },
            Json.FileUploadFinalizeRequest, cancellationToken).ConfigureAwait(false);
        var response = await AwaitAcknowledgementAsync(_finalize.Task, cancellationToken).ConfigureAwait(false);
        if (response.Result?.Complete != true || response.Result.Cancelled == true || response.Result.TotalLength is not { } totalLength)
            throw new JsonException("The upload_finalize acknowledgement must confirm a completed file and total_len.");
        Volatile.Write(ref _state, (int)FreeboxUploadState.Completed);
        Volatile.Read(ref _progress)?.Report(new(BytesSent, totalLength, _expectedSize, true));
        return new(totalLength, BytesSent);
    }

    private async Task<FileUploadControlResponse> AwaitAcknowledgementAsync(Task<FileUploadControlResponse> acknowledgement,
        CancellationToken cancellationToken)
    {
        using var stop = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _receiveStop.Token);
        await Task.WhenAny(acknowledgement, _failure.Task).WaitAsync(stop.Token).ConfigureAwait(false);
        ThrowIfFailed();
        return await acknowledgement.ConfigureAwait(false);
    }

    private async Task ReadResponsesAsync()
    {
        try
        {
            while (!_receiveStop.IsCancellationRequested)
            {
                var raw = await _connection.ReceiveJsonAsync(_receiveStop.Token).ConfigureAwait(false);
                var response = raw.Deserialize(Json.FileUploadControlResponse)
                    ?? throw new JsonException("An upload response must be an object.");
                var expectedId = response.Action switch
                {
                    "upload_start" or "upload_data" => StartRequestId,
                    "upload_finalize" => FinalizeRequestId,
                    "upload_cancel" => CancelRequestId,
                    _ => throw new JsonException("Unknown action in the upload state machine.")
                };
                if (response.RequestId != expectedId) throw new JsonException("An upload acknowledgement has an unexpected request_id.");
                if (response.Success is not { } success) throw new JsonException("An upload acknowledgement must declare success.");
                if (!success) throw new FreeboxWebSocketApiException(response.Action, response.RequestId,
                    response.ErrorCode, response.Message, response.FileSize);
                switch (response.Action)
                {
                    case "upload_start": _start.TrySetResult(response); break;
                    case "upload_data":
                        if (Volatile.Read(ref _dataStarted) == 0) throw new JsonException("Upload progress arrived before any binary content was sent.");
                        Volatile.Read(ref _progress)?.Report(new(BytesSent, response.Result?.TotalLength, _expectedSize,
                            response.Result?.Complete == true));
                        break;
                    case "upload_finalize":
                        if (Volatile.Read(ref _finalizeSent) == 0) throw new JsonException("A finalize acknowledgement arrived before finalize was sent.");
                        _finalize.TrySetResult(response); return;
                    case "upload_cancel":
                        if (Volatile.Read(ref _cancelSent) == 0) throw new JsonException("A cancel acknowledgement arrived before cancel was sent.");
                        _cancel.TrySetResult(response); return;
                }
            }
        }
        catch (OperationCanceledException) when (_receiveStop.IsCancellationRequested) { }
        catch (Exception exception)
        {
            Volatile.Write(ref _state, (int)FreeboxUploadState.Faulted);
            _failure.TrySetResult(exception);
            await _failureStop.CancelAsync().ConfigureAwait(false);
        }
    }

    private void EnsureReady()
    {
        ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
        ThrowIfFailed();
        if (State != FreeboxUploadState.Ready) throw new InvalidOperationException("The upload session is not ready for this operation.");
    }

    private void ThrowIfFailed()
    {
        if (_failure.Task.IsCompletedSuccessfully) ExceptionDispatchInfo.Capture(_failure.Task.Result).Throw();
    }

    private static TaskCompletionSource<FileUploadControlResponse> NewAcknowledgement() =>
        new(TaskCreationOptions.RunContinuationsAsynchronously);

    public async ValueTask DisposeAsync()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0) return;
        await _receiveStop.CancelAsync().ConfigureAwait(false);
        await _reader.ConfigureAwait(false);
        await _connection.DisposeAsync().ConfigureAwait(false);
        if (State is not (FreeboxUploadState.Completed or FreeboxUploadState.Cancelled or FreeboxUploadState.Faulted))
            Volatile.Write(ref _state, (int)FreeboxUploadState.Disposed);
        // Keep synchronization objects valid for waiters released concurrently by cancellation/disposal.
    }
}
