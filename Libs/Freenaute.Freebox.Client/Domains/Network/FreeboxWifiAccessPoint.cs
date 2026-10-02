using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxWifiAccessPoint
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;
    private readonly string _id;
    private readonly long _idValue;

    public FreeboxWifiAccessPoint(IFreeboxTransport transport, long id)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        _id = FreeboxApiPath.EncodeSegment(NetworkPath.Number(id));
        _idValue = id;
    }

    public FreeboxWifiStation Station(string mac) => new(_transport, _idValue, mac);

    /// <summary>Get a particular AP.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id</remarks>
    public Task<WifiAp> GetAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}", Json.WifiAp, cancellationToken);
    }

    /// <summary>Update an AP.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-ap-id</remarks>
    public Task<WifiAp> UpdateAsync(WifiAccessPointPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, $"wifi/ap/{_id}", request, Json.WifiAccessPointPatch, Json.WifiAp, cancellationToken);
    }

    /// <summary>Wi-Fi AP allowed channels.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-allowed_channel_comb</remarks>
    public Task<WifiAllowedComb[]> GetAllowedChannelsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}/allowed_channel_comb", Json.WifiAllowedCombArray, cancellationToken);
    }

    /// <summary>Get Wi-Fi Stations List.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-stations-</remarks>
    public Task<WifiStation[]> GetStationsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}/stations/", Json.WifiStationArray, cancellationToken);
    }

    /// <summary>Get survey data history.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-channel_survey_history-timestamp</remarks>
    public Task<WifiApChannelSurveyData[]> GetSurveyHistoryAsync(long timestamp, CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}/channel_survey_history/{NetworkPath.Number(timestamp)}", Json.WifiApChannelSurveyDataArray, cancellationToken);
    }

    /// <summary>Restart an AP.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-ap-id-restart</remarks>
    public Task RestartAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Post, $"wifi/ap/{_id}/restart", cancellationToken);
    }

    /// <summary>List AP neighbors.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-neighbors-</remarks>
    public Task<WifiNeighbor[]> GetNeighborsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}/neighbors/", Json.WifiNeighborArray, cancellationToken);
    }

    /// <summary>List Wi-Fi channels usage.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-channel_usage-</remarks>
    public Task<WifiChannelUsage[]> GetChannelUsageAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}/channel_usage/", Json.WifiChannelUsageArray, cancellationToken);
    }

    /// <summary>Refresh radar informations.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-ap-id-neighbors-scan</remarks>
    public Task ScanNeighborsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Post, $"wifi/ap/{_id}/neighbors/scan", cancellationToken);
    }

    /// <summary>Config reset value of an AP.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-default</remarks>
    public Task<WifiApConfig> GetDefaultsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}/default", Json.WifiApConfig, cancellationToken);
    }

    /// <summary>Per AP/BSS diagnostic.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-diag &amp; -api-v9-wifi-bss-id-diag</remarks>
    public Task<WifiDiagItem[]> GetDiagnosticsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}/diag", Json.WifiDiagItemArray, cancellationToken);
    }

    /// <summary>Per AP/BSS diagnostic.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-ap-id-diag &amp; -api-v9-wifi-bss-id-diag</remarks>
    public Task FixDiagnosticsAsync(string[] request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, $"wifi/ap/{_id}/diag", request, Json.StringArray, cancellationToken);
    }

}
