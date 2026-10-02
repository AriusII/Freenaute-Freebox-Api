using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxVpnClientApi(IFreeboxTransport transport)
{
    public Task<VpnClientConfig[]> GetConfigurationsAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "vpn_client/config/", ServicesJsonSerializerContext.Default.VpnClientConfigArray, cancellationToken);
    public FreeboxServiceResource<VpnClientConfig, VpnClientWriteRequest> Configuration(string id) =>
        new(transport, $"vpn_client/config/{ServicesPath.Segment(id)}", ServicesJsonSerializerContext.Default.VpnClientConfig,
            ServicesJsonSerializerContext.Default.VpnClientWriteRequest, request => request.Validate());
    public Task<VpnClientConfig> CreateConfigurationAsync(VpnClientWriteRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        if (request.Type.HasValue && request.Type.Value == VpnClientType.OpenVpn)
            throw new NotSupportedException("The source does not document the OpenVPN client configuration schema or import contract.");
        return transport.SendAsync(HttpMethod.Post, "vpn_client/config/", request, ServicesJsonSerializerContext.Default.VpnClientWriteRequest,
            ServicesJsonSerializerContext.Default.VpnClientConfig, cancellationToken);
    }
    public FreeboxServiceCommand<VpnClientWriteRequest, VpnClientConfig> CreateConfiguration() => new(new(), CreateConfigurationAsync);
    public Task<VpnClientStatus> GetStatusAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "vpn_client/status", ServicesJsonSerializerContext.Default.VpnClientStatus, cancellationToken);
    public Task<string> GetLogAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "vpn_client/log", ServicesJsonSerializerContext.Default.String, cancellationToken);
}
