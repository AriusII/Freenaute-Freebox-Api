using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxTftpApi(IFreeboxTransport transport)
{
    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v16-tftp-config-" />
    public Task<TftpConfig> GetConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "tftp/config/", ServicesJsonSerializerContext.Default.TftpConfig, cancellationToken);

    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#put--api-latest-tftp-config-" />
    public Task<TftpConfig> UpdateConfigurationAsync(TftpConfigPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return transport.SendAsync(HttpMethod.Put, "tftp/config/", request, ServicesJsonSerializerContext.Default.TftpConfigPatch, ServicesJsonSerializerContext.Default.TftpConfig, cancellationToken);
    }

    public FreeboxServiceCommand<TftpConfigPatch, TftpConfig> Configure() => new(new(), UpdateConfigurationAsync);
}
