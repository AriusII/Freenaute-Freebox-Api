using System.Buffers;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Freenaute.Freebox.Client;

/// <summary>An owned connection with one concurrent reader and writer, bounded frames and strict UTF-8 JSON.</summary>
public sealed class FreeboxWebSocketConnection : IDisposable, IAsyncDisposable
{
    public const int MaximumMessageBytes = 1_000_000;
    private static readonly UTF8Encoding Utf8 = new(false, true);
    private readonly WebSocket _socket;
    private readonly SemaphoreSlim _reader = new(1, 1);
    private readonly SemaphoreSlim _writer = new(1, 1);
    private readonly CancellationTokenSource _closed = new();
    private readonly Queue<JsonElement> _messages = new();
    private int _disposed;

    public WebSocketState State => _socket.State;

    /// <summary>Transfers ownership of an already connected socket, including a consumer-supplied transport.</summary>
    public FreeboxWebSocketConnection(WebSocket socket)
    {
        ArgumentNullException.ThrowIfNull(socket);
        if (socket.State != WebSocketState.Open) throw new ArgumentException("The WebSocket must be open.", nameof(socket));
        _socket = socket;
    }

    public Task SendJsonAsync<T>(T value, JsonTypeInfo<T> jsonType, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(jsonType);
        return SendAsync(JsonSerializer.SerializeToUtf8Bytes(value, jsonType), WebSocketMessageType.Text, cancellationToken);
    }

    public Task SendBinaryAsync(ReadOnlyMemory<byte> value, CancellationToken cancellationToken = default) =>
        SendAsync(value, WebSocketMessageType.Binary, cancellationToken);

    private async Task SendAsync(ReadOnlyMemory<byte> value, WebSocketMessageType type, CancellationToken cancellationToken)
    {
        ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
        if (value.Length > MaximumMessageBytes)
            throw new ArgumentOutOfRangeException(nameof(value), "The Freebox WebSocket frame limit is 1 MB.");
        using var operation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _closed.Token);
        await _writer.WaitAsync(operation.Token).ConfigureAwait(false);
        try
        {
            await _socket.SendAsync(value, type, endOfMessage: true, operation.Token).ConfigureAwait(false);
        }
        finally { _writer.Release(); }
    }

    public async Task<JsonElement> ReceiveJsonAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
        using var operation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _closed.Token);
        await _reader.WaitAsync(operation.Token).ConfigureAwait(false);
        try
        {
            if (_messages.TryDequeue(out var pending)) return pending;
            var rented = ArrayPool<byte>.Shared.Rent(16 * 1024);
            try
            {
                using var content = new MemoryStream();
                while (true)
                {
                    var result = await _socket.ReceiveAsync(rented.AsMemory(), operation.Token).ConfigureAwait(false);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        if (_socket.CloseStatus is null or WebSocketCloseStatus.NormalClosure)
                            throw new EndOfStreamException("The Freebox WebSocket closed normally.");
                        throw new WebSocketException($"The Freebox WebSocket closed with {_socket.CloseStatus}.");
                    }
                    if (result.MessageType != WebSocketMessageType.Text)
                        throw new JsonException("This operation expects JSON text messages.");
                    if (content.Length + result.Count > MaximumMessageBytes)
                    {
                        _socket.Abort();
                        throw new JsonException("The Freebox WebSocket message exceeds 1 MB.");
                    }
                    content.Write(rented, 0, result.Count);
                    if (!result.EndOfMessage) continue;

                    var text = Utf8.GetString(content.GetBuffer(), 0, checked((int)content.Length));
                    try
                    {
                        using var complete = JsonDocument.Parse(text);
                        if (complete.RootElement.ValueKind != JsonValueKind.Object)
                            throw new JsonException("A WebSocket JSON record must be an object.");
                        return complete.RootElement.Clone();
                    }
                    catch (JsonException)
                    {
                        // ws/event additionally documents one JSON record per line; other APIs use ordinary JSON objects.
                        var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                        if (lines.Length < 2) throw;
                        var records = new List<JsonElement>(lines.Length);
                        foreach (var line in lines)
                        {
                            using var document = JsonDocument.Parse(line);
                            if (document.RootElement.ValueKind != JsonValueKind.Object)
                                throw new JsonException("A WebSocket JSON record must be an object.");
                            records.Add(document.RootElement.Clone());
                        }
                        foreach (var record in records) _messages.Enqueue(record);
                    }
                    if (_messages.TryDequeue(out var message)) return message;
                    content.SetLength(0);
                }
            }
            catch (Exception error) when (error is JsonException or DecoderFallbackException)
            {
                _messages.Clear();
                _socket.Abort();
                throw;
            }
            finally { ArrayPool<byte>.Shared.Return(rented); }
        }
        finally { _reader.Release(); }
    }

    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        if (Volatile.Read(ref _disposed) != 0) return;
        await _writer.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (_socket.State is WebSocketState.Open or WebSocketState.CloseReceived)
                await _socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Client closed", cancellationToken)
                    .ConfigureAwait(false);
        }
        finally { _writer.Release(); }
    }

    public void Dispose()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0) return;
        _closed.Cancel();
        _socket.Abort();
        _socket.Dispose();
        // Keep gates alive for in-flight finally blocks; they hold no unmanaged resources unless WaitHandle was used.
        _closed.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (Volatile.Read(ref _disposed) != 0) return;
        using var deadline = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        try { await CloseAsync(deadline.Token).ConfigureAwait(false); }
        catch (Exception error) when (error is WebSocketException or OperationCanceledException or ObjectDisposedException) { }
        finally { Dispose(); }
    }
}
