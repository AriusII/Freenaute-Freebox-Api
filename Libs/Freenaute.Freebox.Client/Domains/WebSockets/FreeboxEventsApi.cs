using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.WebSockets;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.WebSockets;

/// <summary>The four event names documented by RegisterAction.events.</summary>
public enum FreeboxServerEvent
{
    VmStateChanged,
    VmDiskTaskDone,
    LanHostL3AddressReachable,
    LanHostL3AddressUnreachable
}

public sealed class FreeboxEventsApi
{
    private readonly IFreeboxWebSocketTransport _transport;
    public FreeboxEventsApi(IFreeboxWebSocketTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }
    public FreeboxEventSubscriptionCommand Subscribe(params FreeboxServerEvent[] events) => new(_transport, events);
}

/// <summary>Immutable event selection; registration is sent only by OpenAsync.</summary>
public sealed class FreeboxEventSubscriptionCommand
{
    private readonly IFreeboxWebSocketTransport _transport;
    private readonly string[] _events;
    public FreeboxEventSubscriptionCommand(IFreeboxWebSocketTransport transport, params FreeboxServerEvent[] events)
    {
        ArgumentNullException.ThrowIfNull(transport);
        ArgumentNullException.ThrowIfNull(events);
        _transport = transport;
        _events = events.Select(EventName).Distinct(StringComparer.Ordinal).ToArray();
    }

    public async Task<FreeboxEventSubscription> OpenAsync(CancellationToken cancellationToken = default)
    {
        var connection = await _transport.ConnectAsync("ws/event", cancellationToken).ConfigureAwait(false);
        try
        {
            await connection.SendJsonAsync(new RegisterEventsRequest { RequestId = 1, Events = (string[])_events.Clone() },
                WebSocketJsonSerializerContext.Default.RegisterEventsRequest, cancellationToken).ConfigureAwait(false);
            return new(connection);
        }
        catch
        {
            await connection.DisposeAsync().ConfigureAwait(false);
            throw;
        }
    }

    private static string EventName(FreeboxServerEvent value) => value switch
    {
        FreeboxServerEvent.VmStateChanged => "vm_state_changed",
        FreeboxServerEvent.VmDiskTaskDone => "vm_disk_task_done",
        FreeboxServerEvent.LanHostL3AddressReachable => "lan_host_l3addr_reachable",
        FreeboxServerEvent.LanHostL3AddressUnreachable => "lan_host_l3addr_unreachable",
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };
}

/// <summary>A single receive stream. Ending enumeration closes the socket; there is no implicit reconnection or polling.</summary>
public sealed class FreeboxEventSubscription : IAsyncDisposable
{
    private readonly FreeboxWebSocketConnection _connection;
    private readonly CancellationTokenSource _stop = new();
    private int _reading;
    private int _disposed;
    internal FreeboxEventSubscription(FreeboxWebSocketConnection connection) => _connection = connection;

    /// <summary>Registration replies, if received, are checked; notifications may arrive without a separate register reply.</summary>
    public async IAsyncEnumerable<FreeboxEventNotification> ReadAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);
        if (Interlocked.CompareExchange(ref _reading, 1, 0) != 0)
            throw new InvalidOperationException("An event subscription permits only one reader.");
        using var stop = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _stop.Token);
        try
        {
            while (true)
            {
                JsonElement raw = default;
                var end = false;
                try { raw = await _connection.ReceiveJsonAsync(stop.Token).ConfigureAwait(false); }
                catch (EndOfStreamException) { end = true; }
                if (end) yield break;
                var message = raw.Deserialize(WebSocketJsonSerializerContext.Default.FreeboxWebSocketMessage)
                    ?? throw new JsonException("An event WebSocket message must be an object.");
                if (message.Success == false)
                    throw new FreeboxWebSocketApiException(message.Action, message.RequestId, message.ErrorCode, message.Message);
                if (message.Success != true) throw new JsonException("An event WebSocket message must declare success.");
                if (message.Action == "register")
                {
                    if (message.RequestId != 1) throw new JsonException("A register acknowledgement has an unexpected request_id.");
                    continue;
                }
                if (message.Action != "notification") throw new JsonException("Unexpected event WebSocket action.");
                yield return new(message);
            }
        }
        finally
        {
            Interlocked.Exchange(ref _reading, 0);
            await DisposeAsync().ConfigureAwait(false);
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0) return;
        await _stop.CancelAsync().ConfigureAwait(false);
        await _connection.DisposeAsync().ConfigureAwait(false);
    }
}

/// <summary>Notification envelope with typed access to established VM/LAN payloads and an extensible JSON result.</summary>
public sealed class FreeboxEventNotification
{
    private readonly FreeboxWebSocketMessage _message;
    internal FreeboxEventNotification(FreeboxWebSocketMessage message) => _message = message;
    public string? Source => _message.Source;
    public string? Event => _message.Event;
    public JsonElement? Result => _message.Result;

    public VmStateChange? GetVmStateChange() => Is("vm", "state_changed")
        ? DeserializeResult(FilesJsonSerializerContext.Default.VmStateChange) : null;
    public VmDiskTask? GetVmDiskTask() => Is("vm", "disk_task_done")
        ? DeserializeResult(FilesJsonSerializerContext.Default.VmDiskTask) : null;
    public LanHost? GetLanHost() => (Source + "_" + Event) is "lan_host_l3addr_reachable" or "lan_host_l3addr_unreachable"
        ? DeserializeResult(NetworkJsonSerializerContext.Default.LanHost) : null;

    public T? DeserializeResult<T>(JsonTypeInfo<T> typeInfo) where T : class
    {
        ArgumentNullException.ThrowIfNull(typeInfo);
        return Result?.Deserialize(typeInfo);
    }
    private bool Is(string source, string eventName) => Source == source && Event == eventName;
}
