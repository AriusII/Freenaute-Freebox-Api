using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Contracts.Protocol;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Protocol;

public interface IFreeboxNotificationsApi
{
    Task<NotificationTarget[]> ListTargetsAsync(CancellationToken cancellationToken = default);
    Task CreateTargetAsync(NotificationTargetWrite request, CancellationToken cancellationToken = default);
    FreeboxNotificationTarget Target(string id);
}

public sealed class FreeboxNotificationsApi(IFreeboxTransport transport) : IFreeboxNotificationsApi
{
    public Task<NotificationTarget[]> ListTargetsAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "notif/targets", ProtocolJsonSerializerContext.Default.NotificationTargetArray,
            cancellationToken);

    public Task CreateTargetAsync(NotificationTargetWrite request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "notif/targets/", Snapshot(request),
            ProtocolJsonSerializerContext.Default.NotificationTargetWrite, cancellationToken);

    public FreeboxNotificationTarget Target(string id) => new(transport, id);

    internal static NotificationTargetWrite Snapshot(NotificationTargetWrite request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Name);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Type);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Token);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.MessageType);
        ArgumentNullException.ThrowIfNull(request.Subscriptions);
        if (!Uri.TryCreate(request.ApiUrl, UriKind.Absolute, out var uri) ||
            uri.Scheme is not ("http" or "https") || !string.IsNullOrEmpty(uri.UserInfo))
            throw new ArgumentException("The notification API URL must be an absolute HTTP(S) address without credentials.", nameof(request));
        return request with { Subscriptions = [.. request.Subscriptions] };
    }
}

public sealed class FreeboxNotificationTarget
{
    private readonly IFreeboxTransport _transport;
    private readonly string _path;

    internal FreeboxNotificationTarget(IFreeboxTransport transport, string id)
    {
        _transport = transport;
        _path = $"notif/targets/{FreeboxApiPath.EncodeSegment(id)}";
    }

    /// <summary>Preserves the documented object-or-array wire shape.</summary>
    public Task<ObjectOrArray<NotificationTarget>> GetAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, _path,
            ProtocolJsonSerializerContext.Default.ObjectOrArrayNotificationTarget, cancellationToken);

    public Task UpdateAsync(NotificationTargetWrite request, CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Put, _path, FreeboxNotificationsApi.Snapshot(request),
            ProtocolJsonSerializerContext.Default.NotificationTargetWrite, cancellationToken);

    public Task DeleteAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Delete, _path, cancellationToken);
}
