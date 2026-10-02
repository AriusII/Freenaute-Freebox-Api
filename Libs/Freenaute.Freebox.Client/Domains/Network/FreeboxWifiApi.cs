using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxWifiApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxWifiApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        Wps = new(transport);
        GuestAccess = new(transport);
    }

    public FreeboxWifiAccessPoint AccessPoint(long id) => new(_transport, id);
    public FreeboxWifiBss Bss(string id) => new(_transport, id);
    public FreeboxWifiBss Bss(long id) => Bss(NetworkPath.Number(id));
    public FreeboxWifiMacFilter MacFilter(string id) => new(_transport, id);
    public FreeboxWifiWpsApi Wps { get; }
    public FreeboxWifiGuestAccessApi GuestAccess { get; }

    /// <summary>Get the current Wi-Fi global configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-config-</remarks>
    public Task<WifiGlobalConfig> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/config/", Json.WifiGlobalConfig, cancellationToken);
    }

    /// <summary>Update the Wi-Fi global configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-config-</remarks>
    public Task<WifiGlobalConfig> UpdateConfigurationAsync(WifiGlobalConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "wifi/config/", request, Json.WifiGlobalConfigurationPatch, Json.WifiGlobalConfig, cancellationToken);
    }

    /// <summary>Get the current Wi-Fi steering configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v16-wifi-steering-config-</remarks>
    public Task<WifiSteeringConfig> GetSteeringAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/steering/config/", Json.WifiSteeringConfig, cancellationToken);
    }

    /// <summary>Update the Wi-Fi steering configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v16-wifi-steering-config-</remarks>
    public Task<WifiSteeringConfig> UpdateSteeringAsync(WifiSteeringConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "wifi/steering/config/", request, Json.WifiSteeringConfigurationPatch, Json.WifiSteeringConfig, cancellationToken);
    }

    /// <summary>Get the global wifi state.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v10-wifi-state-</remarks>
    public Task<ObjectOrArray<WifiGlobalState>> GetStateAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/state/", Json.ObjectOrArrayWifiGlobalState, cancellationToken);
    }

    /// <summary>Get the ap list.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-</remarks>
    public Task<WifiAp[]> GetAccessPointsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/ap/", Json.WifiApArray, cancellationToken);
    }

    /// <summary>Get the bss list.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-bss-</remarks>
    public Task<WifiBss[]> GetBssListAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/bss/", Json.WifiBssArray, cancellationToken);
    }

    /// <summary>Get Wi-Fi Planning.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-planning-</remarks>
    public Task<WifiPlanning> GetPlanningAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/planning/", Json.WifiPlanning, cancellationToken);
    }

    /// <summary>Update Wi-Fi Planning.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-planning-</remarks>
    public Task<WifiPlanning> UpdatePlanningAsync(WifiPlanningPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "wifi/planning/", request, Json.WifiPlanningPatch, Json.WifiPlanning, cancellationToken);
    }

    /// <summary>Get the MAC filter list.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-mac_filter-</remarks>
    public Task<WifiMacFilter[]> GetMacFiltersAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/mac_filter/", Json.WifiMacFilterArray, cancellationToken);
    }

    /// <summary>Create a new MAC filter.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-mac_filter-</remarks>
    public Task<WifiMacFilter> CreateMacFilterAsync(CreateWifiMacFilterRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, "wifi/mac_filter/", request, Json.CreateWifiMacFilterRequest, Json.WifiMacFilter, cancellationToken);
    }

    /// <summary>Global reset.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-config-reset-</remarks>
    public Task ResetAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Post, "wifi/config/reset/", cancellationToken);
    }

    /// <summary>Config reset value (bulk).</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-default</remarks>
    public Task<WifiDefaultsResult> GetDefaultsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/default", Json.WifiDefaultsResult, cancellationToken);
    }

    /// <summary>Global diagnostic.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-diag</remarks>
    public Task<WifiDiagnosticsResult> GetDiagnosticsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/diag", Json.WifiDiagnosticsResult, cancellationToken);
    }

    /// <summary>Global diagnostic.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-diag</remarks>
    public Task FixDiagnosticsAsync(WifiDiagnosticFix request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, "wifi/diag", request, Json.WifiDiagnosticFix, cancellationToken);
    }

    /// <summary>Get temporary disable state.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v13-wifi-temp_disable</remarks>
    public Task<TemporaryWifiDisable> GetTemporaryDisableAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/temp_disable", Json.TemporaryWifiDisable, cancellationToken);
    }

    /// <summary>Get temporary disable state.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v13-wifi-temp_disable</remarks>
    public Task TemporarilyDisableAsync(TemporaryWifiDisableRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, "wifi/temp_disable", request, Json.TemporaryWifiDisableRequest, cancellationToken);
    }

}
