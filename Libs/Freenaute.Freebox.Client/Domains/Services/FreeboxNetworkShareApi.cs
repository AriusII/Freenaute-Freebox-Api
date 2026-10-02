using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxNetworkShareApi(IFreeboxTransport transport)
{
    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v8-netshare-samba-" />
    public Task<SambaConfig> GetSambaConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "netshare/samba/", ServicesJsonSerializerContext.Default.SambaConfig, cancellationToken);

    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#put--api-v8-netshare-samba-" />
    public Task<SambaConfig> UpdateSambaConfigurationAsync(SambaConfigPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return transport.SendAsync(HttpMethod.Put, "netshare/samba/", request, ServicesJsonSerializerContext.Default.SambaConfigPatch, ServicesJsonSerializerContext.Default.SambaConfig, cancellationToken);
    }

    public FreeboxServiceCommand<SambaConfigPatch, SambaConfig> ConfigureSamba() => new(new(), UpdateSambaConfigurationAsync);

    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v8-netshare-afp-" />
    public Task<AfpConfig> GetAfpConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "netshare/afp/", ServicesJsonSerializerContext.Default.AfpConfig, cancellationToken);

    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#put--api-v8-netshare-afp-" />
    public Task<AfpConfig> UpdateAfpConfigurationAsync(AfpConfigPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return transport.SendAsync(HttpMethod.Put, "netshare/afp/", request, ServicesJsonSerializerContext.Default.AfpConfigPatch, ServicesJsonSerializerContext.Default.AfpConfig, cancellationToken);
    }

    public FreeboxServiceCommand<AfpConfigPatch, AfpConfig> ConfigureAfp() => new(new(), UpdateAfpConfigurationAsync);

}
