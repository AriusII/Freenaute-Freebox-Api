using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxVpnApi
{
    private readonly IFreeboxTransport _transport;
    private readonly IFreeboxBinaryTransport? _binary;
    internal FreeboxVpnApi(IFreeboxTransport transport, IFreeboxBinaryTransport? binary)
    { _transport = transport; _binary = binary; }
    public Task<VpnServer[]> GetServersAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, "vpn/", ServicesJsonSerializerContext.Default.VpnServerArray, cancellationToken);
    public Task<VpnServerConfig> GetServerConfigurationAsync(string serverId, CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, $"vpn/{ServicesPath.Segment(serverId)}/config/", ServicesJsonSerializerContext.Default.VpnServerConfig, cancellationToken);
    public Task<VpnServerConfig> UpdateOpenVpnRoutedConfigurationAsync(VpnOpenVpnRoutedPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return _transport.SendAsync(HttpMethod.Put, "vpn/openvpn_routed/config/", request,
            ServicesJsonSerializerContext.Default.VpnOpenVpnRoutedPatch, ServicesJsonSerializerContext.Default.VpnServerConfig, cancellationToken);
    }
    public FreeboxServiceCommand<VpnOpenVpnRoutedPatch, VpnServerConfig> ConfigureOpenVpnRouted() => new(new(), UpdateOpenVpnRoutedConfigurationAsync);
    public Task<VpnUser[]> GetUsersAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, "vpn/user/", ServicesJsonSerializerContext.Default.VpnUserArray, cancellationToken);
    public FreeboxServiceResource<VpnUser, VpnUserWriteRequest> User(string login) =>
        new(_transport, $"vpn/user/{ServicesPath.Segment(login)}", ServicesJsonSerializerContext.Default.VpnUser,
            ServicesJsonSerializerContext.Default.VpnUserWriteRequest, request => request.Validate());
    public Task<VpnUser> CreateUserAsync(VpnUserWriteRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return _transport.SendAsync(HttpMethod.Post, "vpn/user/", request, ServicesJsonSerializerContext.Default.VpnUserWriteRequest,
            ServicesJsonSerializerContext.Default.VpnUser, cancellationToken);
    }
    public FreeboxServiceCommand<VpnUserWriteRequest, VpnUser> CreateUser() => new(new(), CreateUserAsync);
    public Task<VpnIpPool> GetIpPoolAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, "vpn/ip_pool/", ServicesJsonSerializerContext.Default.VpnIpPool, cancellationToken);
    public Task<VpnConnection[]> GetConnectionsAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, "vpn/connection/", ServicesJsonSerializerContext.Default.VpnConnectionArray, cancellationToken);
    public Task DisconnectAsync(string connectionId, CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Delete, $"vpn/connection/{ServicesPath.Segment(connectionId)}", cancellationToken);
    /// <summary>Downloads the plain configuration. Generating a new OpenVPN configuration invalidates the previous file; this operation sends once.</summary>
    public Task<FreeboxDownload> DownloadConfigurationAsync(string serverName, string login, CancellationToken cancellationToken = default) =>
        (_binary ?? throw new NotSupportedException("The supplied transport does not support binary downloads."))
        .DownloadAsync(HttpMethod.Get, $"vpn/download_config/{ServicesPath.Segment(serverName)}/{ServicesPath.Segment(login)}/plain", cancellationToken);
}
