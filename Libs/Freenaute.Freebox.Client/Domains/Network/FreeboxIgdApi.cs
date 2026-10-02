using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxIgdApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxIgdApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }

    public FreeboxIgdRedirection Redirection(string id) => new(_transport, id);

    /// <summary>Get the current UPnP IGD configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upnpigd-config-</remarks>
    public Task<UPnPIGDConfig> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "upnpigd/config/", Json.UPnPIGDConfig, cancellationToken);
    }

    /// <summary>Update the UPnP IGD configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-upnpigd-config-</remarks>
    public Task<UPnPIGDConfig> UpdateConfigurationAsync(IgdConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "upnpigd/config/", request, Json.IgdConfigurationPatch, Json.UPnPIGDConfig, cancellationToken);
    }

    /// <summary>Get the list of current redirection.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upnpigd-redir-</remarks>
    public Task<UPnPRedir[]> GetRedirectionsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "upnpigd/redir/", Json.UPnPRedirArray, cancellationToken);
    }

}
