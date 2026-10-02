using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxLanApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxLanApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }

    public FreeboxLanInterface Interface(string name) => new(_transport, name);

    /// <summary>Get the current Lan configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-lan-config-</remarks>
    public Task<LanConfig> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "lan/config/", Json.LanConfig, cancellationToken);
    }

    /// <summary>Update the current Lan configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-lan-config-</remarks>
    public Task<LanConfig> UpdateConfigurationAsync(LanConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "lan/config/", request, Json.LanConfigurationPatch, Json.LanConfig, cancellationToken);
    }

    /// <summary>Get the current routing configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v16-lan-routes</remarks>
    public Task<Route[]> GetRoutesAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "lan/routes", Json.RouteArray, cancellationToken);
    }

    /// <summary>Update the current routing configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v16-lan-routes-</remarks>
    public Task<Route[]> ReplaceRoutesAsync(LanRouteWrite[] request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "lan/routes/", request, Json.LanRouteWriteArray, Json.RouteArray, cancellationToken);
    }

    /// <summary>Getting the list of browsable LAN interfaces.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-lan-browser-interfaces-</remarks>
    public Task<LanBrowserInterface[]> GetInterfacesAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "lan/browser/interfaces/", Json.LanBrowserInterfaceArray, cancellationToken);
    }

    /// <summary>Getting available lan host types.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-lan-browser-types-</remarks>
    public Task<LanHostTypeDescriptor[]> GetHostTypesAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "lan/browser/types/", Json.LanHostTypeDescriptorArray, cancellationToken);
    }

}
