using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxUPnpAvApi(IFreeboxTransport transport)
{
    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upnpav-config-" />
    public Task<UPnpAvConfig> GetConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "upnpav/config/", ServicesJsonSerializerContext.Default.UPnpAvConfig, cancellationToken);

    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#put--api-v8-upnpav-config-" />
    public Task<UPnpAvConfig> UpdateConfigurationAsync(UPnpAvConfigPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return transport.SendAsync(HttpMethod.Put, "upnpav/config/", request, ServicesJsonSerializerContext.Default.UPnpAvConfigPatch, ServicesJsonSerializerContext.Default.UPnpAvConfig, cancellationToken);
    }

    public FreeboxServiceCommand<UPnpAvConfigPatch, UPnpAvConfig> Configure() => new(new(), UpdateConfigurationAsync);
}
