using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using Freenaute.Freebox.Client.Domains.WebSockets;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class WebSocketApiTests
{
    [Fact]
    public async Task UploadPipelinesBinaryChunksWhileReceivingProgressAndWaitsForFinalAck()
    {
        using var socket = new ScriptedSocket();
        var seenProgress = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var progress = new InlineProgress<FreeboxUploadProgress>(value =>
        {
            if (value.TotalFileLength == 8 && !value.Complete) seenProgress.TrySetResult();
        });
        var binaryCount = 0;
        socket.Respond = async frame =>
        {
            if (frame.Type == WebSocketMessageType.Binary)
            {
                var count = Interlocked.Increment(ref binaryCount);
                if (count == 2) socket.Text("""{"action":"upload_data","request_id":1,"success":true,"result":{"total_len":8,"complete":false}}""");
                if (count == 3) await seenProgress.Task.WaitAsync(TimeSpan.FromSeconds(5));
                return;
            }
            using var json = JsonDocument.Parse(frame.Bytes);
            var action = json.RootElement.GetProperty("action").GetString();
            if (action == "upload_start") socket.Text(StartAck);
            else if (action == "upload_finalize") socket.Text("""{"action":"upload_finalize","request_id":2,"success":true,"result":{"total_len":9,"complete":true}}""");
        };
        var transport = new SocketFixtureTransport(socket);
        var path = EncodedFreeboxPath.FromEncoded("L0Rpc3F1ZSBkdXIvMF91cGxvYWRfdGVzdA==");
        await using var source = new MemoryStream(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        var result = await new FreeboxWebSocketsApi(transport).Uploads.To(path).File("épreuve.bin")
            .Overwrite().WithSize(9).WithChunkSize(4).SendAsync(source, progress: progress);

        Assert.Equal(9, result.TotalFileLength);
        Assert.Equal(9, result.BytesSent);
        Assert.Equal("ws/upload", Assert.Single(transport.Paths));
        Assert.Equal(3, binaryCount);
        Assert.Equal(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, socket.Sent.Where(x => x.Type == WebSocketMessageType.Binary).SelectMany(x => x.Bytes));
        var start = socket.Controls().First();
        Assert.Equal("upload_start", start.GetProperty("action").GetString());
        Assert.Equal(path.Value, start.GetProperty("dirname").GetString());
        Assert.Equal("épreuve.bin", start.GetProperty("filename").GetString());
        Assert.Equal("overwrite", start.GetProperty("force").GetString());
        Assert.Equal(9, start.GetProperty("size").GetInt64());
        Assert.DoesNotContain(socket.Controls(), x => x.GetProperty("action").GetString() == "upload_cancel");
        Assert.True(socket.WasDisposed);
    }

    [Fact]
    public async Task ResumeDoesNotInspectStreamLengthOrSeekAndOmitsUnknownSize()
    {
        using var socket = SuccessfulUploadSocket(3);
        using var source = new NonSeekableReadStream([7, 8, 9]);
        var command = new FreeboxWebSocketsApi(new SocketFixtureTransport(socket)).Uploads
            .To(EncodedFreeboxPath.FromUtf8Path("/Disque dur")).File("resume.bin");
        var resume = command.Resume();

        await resume.SendAsync(source);

        var start = socket.Controls().First();
        Assert.Equal("resume", start.GetProperty("force").GetString());
        Assert.False(start.TryGetProperty("size", out _));
        Assert.Equal(new byte[] { 7, 8, 9 }, socket.Sent.Where(x => x.Type == WebSocketMessageType.Binary).SelectMany(x => x.Bytes));
        Assert.False(source.WasDisposed);
    }

    [Fact]
    public async Task CallerCancellationClosesAndKeepsPartialWithoutFinalizeOrDestructiveCancel()
    {
        using var socket = new ScriptedSocket();
        var writing = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        socket.RespondWithCancellation = async (frame, cancellationToken) =>
        {
            if (frame.Type == WebSocketMessageType.Text) socket.Text(StartAck);
            else
            {
                writing.SetResult();
                await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            }
        };
        using var cancellation = new CancellationTokenSource();
        using var source = new MemoryStream([1, 2, 3]);
        var pending = new FreeboxWebSocketsApi(new SocketFixtureTransport(socket)).Uploads
            .To(EncodedFreeboxPath.FromUtf8Path("/Disque dur")).File("partial.bin").SendAsync(source, cancellation.Token);
        await writing.Task.WaitAsync(TimeSpan.FromSeconds(5));
        cancellation.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => pending);

        Assert.True(socket.WasDisposed);
        Assert.Equal("upload_start", Assert.Single(socket.Controls()).GetProperty("action").GetString());
    }

    [Fact]
    public async Task ExplicitSessionCancelSendsDestructiveActionAndRequiresItsCorrelatedAck()
    {
        using var socket = new ScriptedSocket();
        socket.Respond = frame =>
        {
            if (frame.Type == WebSocketMessageType.Text)
            {
                using var json = JsonDocument.Parse(frame.Bytes);
                if (json.RootElement.GetProperty("action").GetString() == "upload_start") socket.Text(StartAck);
                else socket.Text("""{"action":"upload_cancel","request_id":3,"success":true,"result":{"complete":true,"cancelled":true}}""");
            }
            return Task.CompletedTask;
        };
        await using var session = await new FreeboxWebSocketsApi(new SocketFixtureTransport(socket)).Uploads
            .To(EncodedFreeboxPath.FromUtf8Path("/Disque dur")).File("cancel.bin").OpenAsync();
        await session.SendChunkAsync(new byte[] { 1, 2 });
        await session.CancelAsync();

        Assert.Equal(FreeboxUploadState.Cancelled, session.State);
        Assert.Equal(2, session.BytesSent);
        Assert.Equal("upload_cancel", socket.Controls().Last().GetProperty("action").GetString());
        Assert.Equal(3, socket.Controls().Last().GetProperty("request_id").GetInt64());
    }

    [Fact]
    public async Task StartRejectionPreservesConflictCodeAndExistingLargeSizeWithoutLeakingMessage()
    {
        using var socket = new ScriptedSocket();
        socket.Respond = _ =>
        {
            socket.Text("""{"action":"upload_start","request_id":1,"success":false,"file_size":5000000000,"error_code":"conflict","msg":"untrusted secret"}""");
            return Task.CompletedTask;
        };
        using var source = new MemoryStream([1]);
        var exception = await Assert.ThrowsAsync<FreeboxWebSocketApiException>(() =>
            new FreeboxWebSocketsApi(new SocketFixtureTransport(socket)).Uploads
                .To(EncodedFreeboxPath.FromUtf8Path("/Disque dur")).File("conflict.bin").SendAsync(source));

        Assert.Equal("conflict", exception.ErrorCode);
        Assert.Equal(5_000_000_000, exception.ExistingFileSize);
        Assert.Equal("untrusted secret", exception.ApiMessage);
        Assert.DoesNotContain("untrusted secret", exception.Message);
        Assert.DoesNotContain(socket.Sent, x => x.Type == WebSocketMessageType.Binary);
        Assert.Single(socket.Controls());
        Assert.True(socket.WasDisposed);
    }

    [Theory]
    [InlineData(999, true)]
    [InlineData(2, false)]
    public async Task FinalizeCannotSucceedWithWrongCorrelationOrMissingCompletion(long requestId, bool complete)
    {
        using var socket = new ScriptedSocket();
        socket.Respond = frame =>
        {
            if (frame.Type == WebSocketMessageType.Text)
            {
                using var json = JsonDocument.Parse(frame.Bytes);
                if (json.RootElement.GetProperty("action").GetString() == "upload_start") socket.Text(StartAck);
                else socket.Text($"{{\"action\":\"upload_finalize\",\"request_id\":{requestId},\"success\":true,\"result\":{{\"total_len\":0,\"complete\":{complete.ToString().ToLowerInvariant()}}}}}");
            }
            return Task.CompletedTask;
        };
        using var source = new MemoryStream();
        await Assert.ThrowsAsync<JsonException>(() => new FreeboxWebSocketsApi(new SocketFixtureTransport(socket)).Uploads
            .To(EncodedFreeboxPath.FromUtf8Path("/Disque dur")).File("empty.bin").WithSize(0).SendAsync(source));
        Assert.True(socket.WasDisposed);
        Assert.Equal(0, socket.Controls().First().GetProperty("size").GetInt64());
    }

    [Fact]
    public async Task ServerDataErrorStopsStreamAndIsNotRetriedOrConvertedToUserCancellation()
    {
        using var socket = new ScriptedSocket();
        var errorRead = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        socket.RespondWithCancellation = async (frame, cancellationToken) =>
        {
            if (frame.Type == WebSocketMessageType.Text) socket.Text(StartAck);
            else
            {
                socket.Text("""{"action":"upload_data","request_id":1,"success":false,"error_code":"disk_full","msg":"disque plein"}""");
                errorRead.TrySetResult();
                await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            }
        };
        using var source = new MemoryStream([1, 2, 3]);
        var exception = await Assert.ThrowsAsync<FreeboxWebSocketApiException>(() => new FreeboxWebSocketsApi(new SocketFixtureTransport(socket)).Uploads
            .To(EncodedFreeboxPath.FromUtf8Path("/Disque dur")).File("disk-full.bin").SendAsync(source));

        Assert.Equal("disk_full", exception.ErrorCode);
        Assert.Single(socket.Sent, x => x.Type == WebSocketMessageType.Binary);
        Assert.Equal("upload_start", Assert.Single(socket.Controls()).GetProperty("action").GetString());
    }

    [Fact]
    public async Task EventsPreserveFourExactNamesAndReadFragmentedNdjsonWithTypedPayloads()
    {
        using var socket = new ScriptedSocket();
        socket.Respond = _ =>
        {
            const string messages = "{\"action\":\"register\",\"request_id\":1,\"success\":true}\n{\"action\":\"notification\",\"success\":true,\"source\":\"vm\",\"event\":\"disk_task_done\",\"result\":{\"id\":12,\"done\":true,\"error\":false}}\n{\"action\":\"notification\",\"success\":true,\"source\":\"lan\",\"event\":\"host_l3addr_reachable\",\"result\":{\"id\":\"hôte\",\"l2ident\":{\"id\":\"mac\",\"type\":\"mac_address\"}}}\n";
            socket.FragmentedText(messages, Encoding.UTF8.GetBytes(messages[..messages.IndexOf('ô')]).Length + 1);
            socket.NormalClose();
            return Task.CompletedTask;
        };
        var transport = new SocketFixtureTransport(socket);
        await using var subscription = await new FreeboxWebSocketsApi(transport).Events.Subscribe(
            FreeboxServerEvent.VmStateChanged, FreeboxServerEvent.VmDiskTaskDone,
            FreeboxServerEvent.LanHostL3AddressReachable, FreeboxServerEvent.LanHostL3AddressUnreachable).OpenAsync();
        var events = new List<FreeboxEventNotification>();
        await foreach (var notification in subscription.ReadAllAsync()) events.Add(notification);

        Assert.Equal("ws/event", Assert.Single(transport.Paths));
        Assert.Equal(new[] { "vm_state_changed", "vm_disk_task_done", "lan_host_l3addr_reachable", "lan_host_l3addr_unreachable" },
            socket.Controls().Single().GetProperty("events").EnumerateArray().Select(x => x.GetString()));
        Assert.Equal(2, events.Count);
        Assert.Equal(12, events[0].GetVmDiskTask()!.Id);
        Assert.True(events[0].GetVmDiskTask()!.Done);
        Assert.Equal("hôte", events[1].GetLanHost()!.Id);
        Assert.Equal(ObjectOrArrayKind.Object, events[1].GetLanHost()!.L2Ident!.Value.Kind);
        Assert.True(socket.WasDisposed);
    }

    [Fact]
    public async Task NotificationCanArriveWithoutRegistrationAckAndRemainReadableAfterDispose()
    {
        using var socket = new ScriptedSocket();
        socket.Respond = _ =>
        {
            socket.Text("""{"action":"notification","success":true,"source":"vm","event":"state_changed","result":{"id":42,"status":"future-state"}}""");
            socket.NormalClose();
            return Task.CompletedTask;
        };
        await using var subscription = await new FreeboxWebSocketsApi(new SocketFixtureTransport(socket)).Events
            .Subscribe(FreeboxServerEvent.VmStateChanged).OpenAsync();
        FreeboxEventNotification? received = null;
        await foreach (var notification in subscription.ReadAllAsync()) { received = notification; break; }

        Assert.Equal(42, received!.GetVmStateChange()!.Id);
        Assert.Equal("future-state", received.GetVmStateChange()!.Status);
        Assert.True(socket.WasDisposed);
    }

    [Fact]
    public async Task EventReaderCancellationClosesSubscriptionWithoutBackgroundPolling()
    {
        using var socket = new ScriptedSocket();
        await using var subscription = await new FreeboxWebSocketsApi(new SocketFixtureTransport(socket)).Events
            .Subscribe(FreeboxServerEvent.VmStateChanged).OpenAsync();
        using var cancellation = new CancellationTokenSource();
        await using var reader = subscription.ReadAllAsync(cancellation.Token).GetAsyncEnumerator();
        var pending = reader.MoveNextAsync().AsTask();
        cancellation.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => pending);
        Assert.True(socket.WasDisposed);
        Assert.Single(socket.Controls());
    }

    private const string StartAck = """{"action":"upload_start","request_id":1,"success":true}""";
    private static ScriptedSocket SuccessfulUploadSocket(long totalLength)
    {
        var socket = new ScriptedSocket();
        socket.Respond = frame =>
        {
            if (frame.Type == WebSocketMessageType.Text)
            {
                using var json = JsonDocument.Parse(frame.Bytes);
                if (json.RootElement.GetProperty("action").GetString() == "upload_start") socket.Text(StartAck);
                else socket.Text($"{{\"action\":\"upload_finalize\",\"request_id\":2,\"success\":true,\"result\":{{\"total_len\":{totalLength},\"complete\":true}}}}");
            }
            return Task.CompletedTask;
        };
        return socket;
    }
}

internal sealed class InlineProgress<T>(Action<T> report) : IProgress<T>
{
    public void Report(T value) => report(value);
}

internal sealed class SocketFixtureTransport(ScriptedSocket socket) : IFreeboxWebSocketTransport
{
    public ConcurrentQueue<string> Paths { get; } = new();
    public Task<FreeboxWebSocketConnection> ConnectAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Paths.Enqueue(relativePath);
        return Task.FromResult(new FreeboxWebSocketConnection(socket));
    }
}

internal sealed record SocketFrame(byte[] Bytes, WebSocketMessageType Type, bool EndOfMessage);

internal sealed class ScriptedSocket : WebSocket
{
    private readonly Channel<SocketFrame> _incoming = Channel.CreateUnbounded<SocketFrame>();
    private int _state = (int)WebSocketState.Open;
    private SocketFrame? _pending;
    private int _pendingOffset;
    private WebSocketCloseStatus? _closeStatus;
    public ConcurrentQueue<SocketFrame> Sent { get; } = new();
    public Func<SocketFrame, Task>? Respond { get; set; }
    public Func<SocketFrame, CancellationToken, Task>? RespondWithCancellation { get; set; }
    public bool WasDisposed { get; private set; }
    public override WebSocketCloseStatus? CloseStatus => _closeStatus;
    public override string? CloseStatusDescription => null;
    public override string? SubProtocol => null;
    public override WebSocketState State => (WebSocketState)Volatile.Read(ref _state);

    public void Text(string json) => _incoming.Writer.TryWrite(new(Encoding.UTF8.GetBytes(json), WebSocketMessageType.Text, true));
    public void FragmentedText(string json, int split)
    {
        var bytes = Encoding.UTF8.GetBytes(json);
        _incoming.Writer.TryWrite(new(bytes[..split], WebSocketMessageType.Text, false));
        _incoming.Writer.TryWrite(new(bytes[split..], WebSocketMessageType.Text, true));
    }
    public void NormalClose() => _incoming.Writer.TryWrite(new([], WebSocketMessageType.Close, true));
    public IEnumerable<JsonElement> Controls() => Sent.Where(x => x.Type == WebSocketMessageType.Text).Select(x =>
    {
        using var json = JsonDocument.Parse(x.Bytes);
        return json.RootElement.Clone();
    });
    public override async Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var frame = new SocketFrame(buffer.ToArray(), messageType, endOfMessage);
        Sent.Enqueue(frame);
        if (RespondWithCancellation is { } withCancellation) await withCancellation(frame, cancellationToken);
        else if (Respond is { } respond) await respond(frame);
    }
    public override async Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
    {
        _pending ??= await _incoming.Reader.ReadAsync(cancellationToken);
        var frame = _pending;
        if (frame.Type == WebSocketMessageType.Close)
        {
            _closeStatus = WebSocketCloseStatus.NormalClosure;
            Volatile.Write(ref _state, (int)WebSocketState.CloseReceived);
            _pending = null;
            return new(0, WebSocketMessageType.Close, true, _closeStatus, null);
        }
        var count = Math.Min(buffer.Count, frame.Bytes.Length - _pendingOffset);
        frame.Bytes.AsMemory(_pendingOffset, count).CopyTo(buffer.AsMemory());
        _pendingOffset += count;
        var end = _pendingOffset == frame.Bytes.Length;
        if (end) { _pending = null; _pendingOffset = 0; }
        return new(count, frame.Type, end && frame.EndOfMessage);
    }
    public override Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
    {
        _closeStatus = closeStatus;
        Volatile.Write(ref _state, (int)WebSocketState.Closed);
        return Task.CompletedTask;
    }
    public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
    {
        _closeStatus = closeStatus;
        Volatile.Write(ref _state, (int)WebSocketState.CloseSent);
        return Task.CompletedTask;
    }
    public override void Abort() => Volatile.Write(ref _state, (int)WebSocketState.Aborted);
    public override void Dispose()
    {
        WasDisposed = true;
        Volatile.Write(ref _state, (int)WebSocketState.Closed);
        _incoming.Writer.TryComplete();
    }
}

internal sealed class NonSeekableReadStream(byte[] data) : Stream
{
    private readonly MemoryStream _source = new(data);
    public bool WasDisposed { get; private set; }
    public override bool CanRead => true;
    public override bool CanSeek => false;
    public override bool CanWrite => false;
    public override long Length => throw new NotSupportedException("Length must not be inferred.");
    public override long Position { get => throw new NotSupportedException("Position must not be inspected."); set => throw new NotSupportedException(); }
    public override int Read(byte[] buffer, int offset, int count) => _source.Read(buffer, offset, count);
    public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => _source.ReadAsync(buffer, cancellationToken);
    public override void Flush() => throw new NotSupportedException();
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException("Resume must not seek.");
    public override void SetLength(long value) => throw new NotSupportedException();
    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    protected override void Dispose(bool disposing)
    {
        WasDisposed = true;
        if (disposing) _source.Dispose();
        base.Dispose(disposing);
    }
}
