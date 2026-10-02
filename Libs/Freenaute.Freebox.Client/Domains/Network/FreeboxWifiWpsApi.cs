using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxWifiWpsApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxWifiWpsApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }


    /// <summary>Enable/disable WPS on all Wi-Fi cards.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-wps-config-</remarks>
    public Task<WifiWpsConfiguration> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/wps/config/", Json.WifiWpsConfiguration, cancellationToken);
    }

    /// <summary>Enable/disable WPS on all Wi-Fi cards.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-wps-config-</remarks>
    public Task<WifiWpsConfiguration> UpdateConfigurationAsync(WifiWpsConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "wifi/wps/config/", request, Json.WifiWpsConfigurationPatch, Json.WifiWpsConfiguration, cancellationToken);
    }

    /// <summary>Start a Wps session on a bss.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-wps-start-</remarks>
    public Task<long> StartAsync(WifiWpsStartRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, "wifi/wps/start/", request, Json.WifiWpsStartRequest, Json.Int64, cancellationToken);
    }

    /// <summary>List the Wps session.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-wps-sessions-</remarks>
    public Task<WifiWpsSession[]> GetSessionsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/wps/sessions/", Json.WifiWpsSessionArray, cancellationToken);
    }

    /// <summary>Clear all Wps Sessions.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#delete--api-v9-wifi-wps-sessions-</remarks>
    public Task ClearSessionsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Delete, "wifi/wps/sessions/", cancellationToken);
    }

    /// <summary>Stop a Wps session.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#stop-a-wps-session</remarks>
    public Task StopAsync(WifiWpsStopRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, "wifi/wps/stop/", request, Json.WifiWpsStopRequest, cancellationToken);
    }

}
