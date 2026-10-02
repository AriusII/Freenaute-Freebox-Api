using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxFtpApi(IFreeboxTransport transport)
{
    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v8-ftp-config-" />
    public Task<FtpConfig> GetConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "ftp/config/", ServicesJsonSerializerContext.Default.FtpConfig, cancellationToken);

    /// <seealso href="http://mafreebox.freebox.fr/doc/index.html#put--api-v8-ftp-config-" />
    public Task<FtpConfig> UpdateConfigurationAsync(FtpConfigPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return transport.SendAsync(HttpMethod.Put, "ftp/config/", request, ServicesJsonSerializerContext.Default.FtpConfigPatch, ServicesJsonSerializerContext.Default.FtpConfig, cancellationToken);
    }

    public FreeboxServiceCommand<FtpConfigPatch, FtpConfig> Configure() => new(new(), UpdateConfigurationAsync);
}
