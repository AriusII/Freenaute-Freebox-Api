using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiGlobalConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiGlobalConfig
{
    /// <summary>is wifi enabled</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>mac_filter_state Description disabled mac filter is disabled whitelist mac filter is enabled, using a whitelist blacklist mac filter is enabled, using a blacklist</summary>
    [JsonPropertyName("mac_filter_state")]
    public string? MacFilterState { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiSteeringConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiSteeringConfig
{
    /// <summary>Wi-Fi steering level. Value Description 0 Wi-Fi steering is disabled 1 Devices are steered when they accept the change 2 Devices are steered more aggressively</summary>
    [JsonPropertyName("steering_level")]
    public long? SteeringLevel { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiGlobalState.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiGlobalState
{
    /// <summary>wifi global state state Description enabled Wifi is enabled disabled Wi-Fi is disabled disabled_planning Wi-Fi is disabled by planning</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>expected wifi cards</summary>
    [JsonPropertyName("expected_phys")]
    public ExpectedPhy[]? ExpectedPhys { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#ExpectedPhy.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class ExpectedPhy
{
    /// <summary>state Description 2d4g 2.4GHz band 5g 5GHz band 6g 6 GHz band 60g 60GHz band</summary>
    [JsonPropertyName("band")]
    public string? Band { get; init; }

    /// <summary>id of the phy</summary>
    [JsonPropertyName("phy_id")]
    public long? PhyId { get; init; }

    /// <summary>true if the wifi card is detected</summary>
    [JsonPropertyName("detected")]
    public bool? Detected { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiAp.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiAp
{
    /// <summary>wifi ap id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>wifi ap name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>ap status</summary>
    [JsonPropertyName("status")]
    public WifiApStatus? Status { get; init; }

    /// <summary>ap capabilities</summary>
    [JsonPropertyName("capabilities")]
    public WifiApCapabilities? Capabilities { get; init; }

    /// <summary>ap configuration</summary>
    [JsonPropertyName("config")]
    public WifiApConfig? Config { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApStatus.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiApStatus
{
    /// <summary>state Description scanning Ap is probing wifi channels no_param Ap is not configured bad_param Ap has an invalid configuration disabled Ap is permanently disabled disabled_planning Ap is currently disabled according to planning disabled_power_saving Ap is currently disabled according to power save disabled_temp Ap is currently disabled temporarily no_active_bss Ap has no active BSS starting Ap is starting starting Ap is stopping acs Ap is selecting the best available channel ht_scan Ap is scanning for other access point dfs Ap is performing dynamic frequency selection active Ap is active failed Ap has failed to start</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>effective channel width (in MHz)</summary>
    [JsonPropertyName("channel_width")]
    public StringOrInteger? ChannelWidth { get; init; }

    /// <summary>effective primary channel</summary>
    [JsonPropertyName("primary_channel")]
    public long? PrimaryChannel { get; init; }

    /// <summary>effective secondary channel</summary>
    [JsonPropertyName("secondary_channel")]
    public long? SecondaryChannel { get; init; }

    /// <summary>time left in dfs state</summary>
    [JsonPropertyName("dfs_cac_remaining_time")]
    public long? DfsCacRemainingTime { get; init; }

    /// <summary>Indicates if DFS channels are unavailable regardless of how the WifiApConfig is configured for this phy. This is enabled when your freebox is in compatibility mode for other Freebox wifi products.</summary>
    [JsonPropertyName("dfs_disabled")]
    public bool? DfsDisabled { get; init; }

    /// <summary>Optional remaining time this access point is temporarily disabled.</summary>
    [JsonPropertyName("temp_disable_remaining_time")]
    public long? TempDisableRemainingTime { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApCapabilities.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiApCapabilities
{
    /// <summary>map of capabilities in 2.4 GHz band</summary>
    [JsonPropertyName("2d4g")]
    public WifiBandCapabilities? Band2D4G { get; init; }

    /// <summary>map of capabilities in 5 GHz band</summary>
    [JsonPropertyName("5g")]
    public WifiBandCapabilities? Band5G { get; init; }

    /// <summary>map of capabilities in 6 GHz band</summary>
    [JsonPropertyName("6g")]
    public WifiBandCapabilities? Band6G { get; init; }

    /// <summary>map of capabilities in 60 GHz band</summary>
    [JsonPropertyName("60g")]
    public WifiBandCapabilities? Band60G { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApHtConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiApHtConfig
{
    /// <summary>enable 802.11ac</summary>
    [JsonPropertyName("ac_enabled")]
    public bool? AcEnabled { get; init; }

    /// <summary>enable 802.11n [UNSTABLE]</summary>
    [JsonPropertyName("ht_enabled")]
    public bool? HtEnabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApHeConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiApHeConfig
{
    /// <summary>enable 802.11ax (HE) [UNSTABLE]</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiApConfig
{
    /// <summary>band Description 2d4g 2.4 GHz 5g 5 GHz 6g 6 GHz 60g 60 GHz</summary>
    [JsonPropertyName("band")]
    public string? Band { get; init; }

    /// <summary>wanted channel width (in MHz) : 20 MHz 40 MHz 80 MHz 160 MHz</summary>
    [JsonPropertyName("channel_width")]
    public StringOrInteger? ChannelWidth { get; init; }

    /// <summary>wanted primary channel, value of 0 means automatic selection</summary>
    [JsonPropertyName("primary_channel")]
    public long? PrimaryChannel { get; init; }

    /// <summary>wanted secondary channel, value of 0 means automatic selection</summary>
    [JsonPropertyName("secondary_channel")]
    public long? SecondaryChannel { get; init; }

    /// <summary>enable channels that require DFS</summary>
    [JsonPropertyName("dfs_enabled")]
    public bool? DfsEnabled { get; init; }

    /// <summary>wifi ht config</summary>
    [JsonPropertyName("ht")]
    public WifiApHtConfig? Ht { get; init; }

    /// <summary>wifi HE config</summary>
    [JsonPropertyName("he")]
    public WifiApHeConfig? He { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApChannelSurveyData.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiApChannelSurveyData
{
    /// <summary>timestamp at which the survey data was retrieved</summary>
    [JsonPropertyName("timestamp")]
    public long? Timestamp { get; init; }

    /// <summary>percentage of time the channel was sensed busy</summary>
    [JsonPropertyName("busy_percent")]
    public long? BusyPercent { get; init; }

    /// <summary>percentage of time spent sending on the channel</summary>
    [JsonPropertyName("tx_percent")]
    public long? TxPercent { get; init; }

    /// <summary>percentage of time spent receiving Wi-Fi traffic on the channel</summary>
    [JsonPropertyName("rx_percent")]
    public long? RxPercent { get; init; }

    /// <summary>percentage of time spent receiving Wi-Fi traffic for a local BSS</summary>
    [JsonPropertyName("rx_bss_percent")]
    public long? RxBssPercent { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiAllowedComb.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiAllowedComb
{
    /// <summary>the band for which the combination can be used band Description 2d4g 2.4 GHz 5g 5 GHz 60g 60 GHz</summary>
    [JsonPropertyName("band")]
    public string? Band { get; init; }

    /// <summary>the channel_width for which the combination can be used</summary>
    [JsonPropertyName("channel_width")]
    public string? ChannelWidth { get; init; }

    /// <summary>does this combination requires DFS. You should only allow this combination if ap has allowed dfs.</summary>
    [JsonPropertyName("need_dfs")]
    public bool? NeedDfs { get; init; }

    /// <summary>time required in dfs state before being able to start the AP.</summary>
    [JsonPropertyName("dfs_cac_time")]
    public long? DfsCacTime { get; init; }

    /// <summary>is this using a PSC channel as primary. Some phones/PCs can only see 6GHz APs when their primary channel is a Preferred Scanning Channel (PSC).</summary>
    [JsonPropertyName("psc")]
    public bool? Psc { get; init; }

    /// <summary>primary channel</summary>
    [JsonPropertyName("primary")]
    public long? Primary { get; init; }

    /// <summary>secondary channel (zero means that secondary channel will not be used)</summary>
    [JsonPropertyName("secondary")]
    public long? Secondary { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiStation.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiStation
{
    /// <summary>station id</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>client MAC address</summary>
    [JsonPropertyName("mac")]
    public string? Mac { get; init; }

    /// <summary>bssid on which the client is associated</summary>
    [JsonPropertyName("bssid")]
    public string? Bssid { get; init; }

    /// <summary>client host name</summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; init; }

    /// <summary>client host information</summary>
    [JsonPropertyName("host")]
    public LanHost? Host { get; init; }

    /// <summary>state Description associated station is associated authenticated station is authenticated</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>inactive duration (in seconds)</summary>
    [JsonPropertyName("inactive")]
    public long? Inactive { get; init; }

    /// <summary>connection duration (in seconds)</summary>
    [JsonPropertyName("conn_duration")]
    public long? ConnDuration { get; init; }

    /// <summary>received bytes (from station to Freebox)</summary>
    [JsonPropertyName("rx_bytes")]
    public long? RxBytes { get; init; }

    /// <summary>transmitted bytes (from Freebox to station)</summary>
    [JsonPropertyName("tx_bytes")]
    public long? TxBytes { get; init; }

    /// <summary>reception data rate (in bytes/s)</summary>
    [JsonPropertyName("tx_rate")]
    public long? TxRate { get; init; }

    /// <summary>transmission data rate (in bytes/s)</summary>
    [JsonPropertyName("rx_rate")]
    public long? RxRate { get; init; }

    /// <summary>signal attenuation (in dB)</summary>
    [JsonPropertyName("signal")]
    public long? Signal { get; init; }

    /// <summary>station flags</summary>
    [JsonPropertyName("flags")]
    public WifiStationFlags? Flags { get; init; }

    /// <summary>last rx stats</summary>
    [JsonPropertyName("last_rx")]
    public WifiStationStats? LastRx { get; init; }

    /// <summary>last tx stats</summary>
    [JsonPropertyName("last_tx")]
    public WifiStationStats? LastTx { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiStationFlags.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiStationFlags
{
    /// <summary>does station uses legacy wifi (802.11a, 802.11b)</summary>
    [JsonPropertyName("legacy")]
    public bool? Legacy { get; init; }

    /// <summary>does station support ht (802.11n)</summary>
    [JsonPropertyName("ht")]
    public bool? Ht { get; init; }

    /// <summary>does station support vht (802.11ac)</summary>
    [JsonPropertyName("vht")]
    public bool? Vht { get; init; }

    /// <summary>does station support he (802.11ax)</summary>
    [JsonPropertyName("he")]
    public bool? He { get; init; }

    /// <summary>is the station authenticated</summary>
    [JsonPropertyName("authorized")]
    public bool? Authorized { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiStationStats.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiStationStats
{
    /// <summary>physical link rate (in 1/10th of MBit/s), -1 if unknown</summary>
    [JsonPropertyName("bitrate")]
    public long? Bitrate { get; init; }

    /// <summary>current link mcs, -1 if not used</summary>
    [JsonPropertyName("mcs")]
    public long? Mcs { get; init; }

    /// <summary>current link vht mcs, -1 if not used</summary>
    [JsonPropertyName("vht_mcs")]
    public long? VhtMcs { get; init; }

    /// <summary>current channel width</summary>
    [JsonPropertyName("width")]
    public string? Width { get; init; }

    /// <summary>is shortgi enabled</summary>
    [JsonPropertyName("shortgi")]
    public bool? Shortgi { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiBss.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiBss
{
    /// <summary>bss id</summary>
    [JsonPropertyName("id")]
    public StringOrInteger? Id { get; init; }

    /// <summary>associated AP id</summary>
    [JsonPropertyName("phy_id")]
    public StringOrInteger? PhyId { get; init; }

    /// <summary>bss status</summary>
    [JsonPropertyName("status")]
    public WifiBssStatus? Status { get; init; }

    /// <summary>if set to True the bss will use the shared parameters stored under shared_bss_params if not the bss will use a configuration specific to this bss stored under bss_params when you want to edit the bss config you should change the config values using values from bss_params or shared_bss_params as a source and update use_shared_params accordingly.</summary>
    [JsonPropertyName("use_shared_params")]
    public bool? UseSharedParams { get; init; }

    /// <summary>bss configuration (use this field for editing)</summary>
    [JsonPropertyName("config")]
    public WifiBssConfig? Config { get; init; }

    /// <summary>current configuration specific to this bss</summary>
    [JsonPropertyName("bss_params")]
    public WifiBssConfig? BssParams { get; init; }

    /// <summary>current configuration for shared bss config</summary>
    [JsonPropertyName("shared_bss_params")]
    public WifiBssConfig? SharedBssParams { get; init; }

    /// <summary>Whether or not this BSS can work with wep encryption or not</summary>
    [JsonPropertyName("disable_wep")]
    public bool? DisableWep { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiBssStatus.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiBssStatus
{
    /// <summary>state Description phy_stopped associated AP is stopped no_param bss is missing config bad_param bss has an invalid config disabled bss is disabled temp_disabled bss has been temporary disabled starting bss is starting active bss is active failed bss has failed to start</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>number of stations for this bss</summary>
    [JsonPropertyName("sta_count")]
    public long? StaCount { get; init; }

    /// <summary>number of authenticated stations for this bss</summary>
    [JsonPropertyName("authorized_sta_count")]
    public long? AuthorizedStaCount { get; init; }

    /// <summary>SSID to use with custom keys</summary>
    [JsonPropertyName("custom_key_ssid")]
    public string? CustomKeySsid { get; init; }

    /// <summary>The currently active MLO partners’s AP for this BSS. Can be empty if MLO is disabled. See the MLO chapter for more info</summary>
    [JsonPropertyName("partners")]
    public long[]? Partners { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiBssConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiBssConfig
{
    /// <summary>enable this BSS. Note that if you want the AP to completely stop emitting wifi you should use WifiGlobalConfig enabled attribute.</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>bss displayed name</summary>
    [JsonPropertyName("ssid")]
    public string? Ssid { get; init; }

    /// <summary>don’t show bss in bss list</summary>
    [JsonPropertyName("hide_ssid")]
    public StringOrBoolean? HideSsid { get; init; }

    /// <summary>Whether or not to use GCMP-256 (only in WPA3 &amp; for box that supports 802.11-be)</summary>
    [JsonPropertyName("gcmp256")]
    public string? Gcmp256 { get; init; }

    /// <summary>encryption Description wep wep (should not use) wpa_psk_auto wpa1 CCMP+TKIP (should not use) wpa_psk_tkip wpa1 TKIP (should not use) wpa_psk_ccmp wpa1 CCMP (should not use) wpa12_psk_auto wpa1+wpa2 CCMP+TKIP (should not use) wpa2_psk_auto wpa2 CCMP+TKIP (should not use) wpa2_psk_tkip wpa2 TKIP (should not use) wpa2_psk_ccmp wpa2 CCMP wpa23_psk_ccmp wpa2+wpa3 CCMP WPA3-personal transition mode wpa23_psk_ccmp_mrsno wpa2+wpa3 CCMP WPA3-personal compatibility mode wpa3_psk_ccmp wpa3 CCMP WPA3-personal only mode</summary>
    [JsonPropertyName("encryption")]
    public string? Encryption { get; init; }

    /// <summary>wifi key “ **** ” will be returned when insufficient permission</summary>
    [JsonPropertyName("key")]
    public string? Key { get; init; }

    /// <summary>eapol version</summary>
    [JsonPropertyName("eapol_version")]
    public long? EapolVersion { get; init; }

    [JsonPropertyName("wps_enabled")]
    public bool? WpsEnabled { get; init; }

    [JsonPropertyName("wps_uuid")]
    public string? WpsUuid { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiNeighbor.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiNeighbor
{
    /// <summary>neighbor bssid</summary>
    [JsonPropertyName("bssid")]
    public string? Bssid { get; init; }

    /// <summary>neighbor ssid</summary>
    [JsonPropertyName("ssid")]
    public string? Ssid { get; init; }

    /// <summary>the band for which the combination can be used band Description 2d4g 2.4 GHz 5g 5 GHz 60g 60 GHz</summary>
    [JsonPropertyName("band")]
    public string? Band { get; init; }

    /// <summary>neighbor channel_width</summary>
    [JsonPropertyName("channel_width")]
    public StringOrInteger? ChannelWidth { get; init; }

    /// <summary>neighbor primary channel</summary>
    [JsonPropertyName("channel")]
    public long? Channel { get; init; }

    /// <summary>neighbor secondary channel (0 for unused)</summary>
    [JsonPropertyName("secondary_channel")]
    public long? SecondaryChannel { get; init; }

    /// <summary>signal attenuation in dB</summary>
    [JsonPropertyName("signal")]
    public long? Signal { get; init; }

    /// <summary>neighbor capabilities</summary>
    [JsonPropertyName("capabilities")]
    public WifiNeighborCap? Capabilities { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiNeighborCap.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiNeighborCap
{
    /// <summary>neighbor uses legacy wifi (802.11a, 802.11b)</summary>
    [JsonPropertyName("legacy")]
    public bool? Legacy { get; init; }

    /// <summary>neighbor supports ht (802.11n)</summary>
    [JsonPropertyName("ht")]
    public bool? Ht { get; init; }

    /// <summary>neighbor supports vht (802.11ac)</summary>
    [JsonPropertyName("vht")]
    public bool? Vht { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiChannelUsage.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiChannelUsage
{
    /// <summary>channel number</summary>
    [JsonPropertyName("channel")]
    public long? Channel { get; init; }

    /// <summary>band Description 2d4g 2.4 GHz 5g 5 GHz 60g 60 GHz</summary>
    [JsonPropertyName("band")]
    public string? Band { get; init; }

    /// <summary>noise level on channel in dB</summary>
    [JsonPropertyName("noise_level")]
    public long? NoiseLevel { get; init; }

    /// <summary>rx channel busy time percentage</summary>
    [JsonPropertyName("rx_busy_percent")]
    public long? RxBusyPercent { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiPlanning.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiPlanning
{
    /// <summary>is the planning enabled</summary>
    [JsonPropertyName("use_planning")]
    public bool? UsePlanning { get; init; }

    /// <summary>planning resolution (number of slots per day)</summary>
    [JsonPropertyName("resolution")]
    public long? Resolution { get; init; }

    /// <summary>mapping for planning : “on” or “off” mapping[0] is monday at 0:0 mapping[7 * resolution - 1] is sunday last slot (each slot has a duration of 60 * 24 / resolution minutes)</summary>
    [JsonPropertyName("mapping")]
    public string[]? Mapping { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiMacFilter.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiMacFilter
{
    /// <summary>filter id</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>MAC address to filter</summary>
    [JsonPropertyName("mac")]
    public string? Mac { get; init; }

    /// <summary>comment</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    /// <summary>type Description whitelist if mac_filter is set to whitelist this station will be allowed blacklist if mac_filter is set to blacklist this station will be rejected</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>host name when available</summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; init; }

    /// <summary>host information when available</summary>
    [JsonPropertyName("host")]
    public LanHost? Host { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiDiagItem.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiDiagItem
{
    /// <summary>When this item relates to an AP, this indicates the AP’s index When this item relates to a BSS, this field is unset</summary>
    [JsonPropertyName("ap_id")]
    public long? ApId { get; init; }

    /// <summary>When this item relates to a BSS, this field indicates the bss’s id When this item relates to an AP, this field is unset</summary>
    [JsonPropertyName("bssid")]
    public string? Bssid { get; init; }

    /// <summary>The code identifying which param is faulty/suboptimal Code Description all This is a the same as doing a full reset of this AP/BSS network_disabled This changes the ‘enabled’ field in WifiBssConfig network_security This changes the ‘encryption’ field in WifiBssConfig network_visibility This changes the ‘hide_ssid’ field in WifiBssConfig channel_width This changes the ‘channel_width’ field in WifiApConfig channel_value This changes the ‘channel’ &amp; ‘secondary_channel’ fields in WifiApConfig</summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>Severity Description minor minor problems don’t have performance/compatibility implications major major problems do</summary>
    [JsonPropertyName("severity")]
    public string? Severity { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiWpsCandidate.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiWpsCandidate
{
    /// <summary>bss id</summary>
    [JsonPropertyName("bssid")]
    public string? Bssid { get; init; }

    /// <summary>wifi network name</summary>
    [JsonPropertyName("ssid")]
    public string? Ssid { get; init; }

    /// <summary>bss uuid for wps</summary>
    [JsonPropertyName("bss_uuid")]
    public string? BssUuid { get; init; }

    /// <summary>band Description 2d4g 2.4 GHz 5g 5 GHz 60g 60 GHz</summary>
    [JsonPropertyName("band")]
    public string? Band { get; init; }

    /// <summary>currently configured encryption mode see WifiBssConfig encryption field</summary>
    [JsonPropertyName("encryption")]
    public string? Encryption { get; init; }

    /// <summary>is wps enabled for this bss</summary>
    [JsonPropertyName("wps_enabled")]
    public bool? WpsEnabled { get; init; }

    /// <summary>the current state of the associated ap see WifiBssStatus state</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiWpsSession.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiWpsSession
{
    /// <summary>wps session id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>bss wps uuid</summary>
    [JsonPropertyName("bss_uuid")]
    public string? BssUuid { get; init; }

    /// <summary>ssid</summary>
    [JsonPropertyName("ssid")]
    public string? Ssid { get; init; }

    /// <summary>is the session active</summary>
    [JsonPropertyName("active")]
    public bool? Active { get; init; }

    /// <summary>result of the wps session result Description success success user_canceled canceled by user self_canceled canceled by restart of bss failed_timeout timeout while waiting for station failed_overlap another wps session was active failed_unknown unknown failure</summary>
    [JsonPropertyName("result")]
    public string? Result { get; init; }

    /// <summary>session start date (timestamp)</summary>
    [JsonPropertyName("start_date")]
    public long? StartDate { get; init; }

    /// <summary>session end date (timestamp)</summary>
    [JsonPropertyName("end_date")]
    public StringOrInteger? EndDate { get; init; }

    /// <summary>mac of the associated client (in case of success)</summary>
    [JsonPropertyName("mac")]
    public string? Mac { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiCustomKeyConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiCustomKeyConfig
{
    /// <summary>The name of the dedicated wifi network</summary>
    [JsonPropertyName("ssid")]
    public string? Ssid { get; init; }

    /// <summary>When true, the SSID name cannot be changed.</summary>
    [JsonPropertyName("ssid_read_only")]
    public bool? SsidReadOnly { get; init; }

    /// <summary>When true, the SSID used for guest network is hidden.</summary>
    [JsonPropertyName("hide_ssid")]
    public bool? HideSsid { get; init; }

    /// <summary>Encryption used for guest Wi-Fi network.</summary>
    [JsonPropertyName("encryption")]
    public string? Encryption { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiCustomKeyHost.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiCustomKeyHost
{
    /// <summary>host name</summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; init; }

    /// <summary>optional host information from Lan Browser (if available)</summary>
    [JsonPropertyName("host")]
    public LanHost? Host { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiCustomKeyParams.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiCustomKeyParams
{
    /// <summary>description of the custom key</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Wi-Fi password for this custom access “ **** ” will be returned when insufficient permission</summary>
    [JsonPropertyName("key")]
    public string? Key { get; init; }

    /// <summary>Number of different hosts that can connect to this network (maximum 127) 0 has special meaning, it means unlimited number of users.</summary>
    [JsonPropertyName("max_use_count")]
    public StringOrInteger? MaxUseCount { get; init; }

    /// <summary>Number of seconds before the custom access is revoked</summary>
    [JsonPropertyName("duration")]
    public long? Duration { get; init; }

    /// <summary>access_type Description full stations will get full access to local network + internet net_only stations connected using this custom key will be isolated and won’t have access to local network devices</summary>
    [JsonPropertyName("access_type")]
    public string? AccessType { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiCustomKey.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiCustomKey
{
    /// <summary>custom key id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>time remaining before the access (seconds) if 0 then it does not expire</summary>
    [JsonPropertyName("remaining")]
    public long? Remaining { get; init; }

    /// <summary>custom key parameters</summary>
    [JsonPropertyName("params")]
    public WifiCustomKeyParams? Params { get; init; }

    /// <summary>list of hosts that used the custom key</summary>
    [JsonPropertyName("users")]
    public WifiCustomKeyHost[]? Users { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#TemporaryWifiDisable.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class TemporaryWifiDisable
{
    /// <summary>remaining seconds the wifi is temporarily disabled. Set to 0 to stop the temporary wifi disabling period.</summary>
    [JsonPropertyName("remaining")]
    public long? Remaining { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiMLOConfiguration.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiMLOConfiguration
{
    /// <summary>List of phys participating in the MLD for the BSS An empty array means MLO is disabled An array with only the BSS’s AP index in it means SLO (single link mode) The allowed combinations are retrieved by the mlo/allowed_comb api.</summary>
    [JsonPropertyName("partners")]
    public long[]? Partners { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-default.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiDefaultAp
{
    [JsonPropertyName("ap_id")]
    public long? ApId { get; init; }

    [JsonPropertyName("params")]
    public WifiApConfig? Params { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-default.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiDefaultBss
{
    [JsonPropertyName("bssid")]
    public string? Bssid { get; init; }

    [JsonPropertyName("params")]
    public WifiBssConfig? Params { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-default.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiDefaultsResult
{
    [JsonPropertyName("aps")]
    public WifiDefaultAp[]? Aps { get; init; }

    [JsonPropertyName("bsss")]
    public WifiDefaultBss[]? Bsss { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-diag.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiDiagnosticsResult
{
    [JsonPropertyName("aps")]
    public WifiDiagItem[]? Aps { get; init; }

    [JsonPropertyName("bsss")]
    public WifiDiagItem[]? Bsss { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-wps-config-.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class WifiWpsConfiguration
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiGlobalConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiGlobalConfigurationPatch
{
    /// <summary>is wifi enabled</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>mac_filter_state Description disabled mac filter is disabled whitelist mac filter is enabled, using a whitelist blacklist mac filter is enabled, using a blacklist</summary>
    [JsonPropertyName("mac_filter_state")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> MacFilterState { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiSteeringConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiSteeringConfigurationPatch
{
    /// <summary>Wi-Fi steering level. Value Description 0 Wi-Fi steering is disabled 1 Devices are steered when they accept the change 2 Devices are steered more aggressively</summary>
    [JsonPropertyName("steering_level")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> SteeringLevel { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApHtConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiApHtConfigurationPatch
{
    /// <summary>enable 802.11ac</summary>
    [JsonPropertyName("ac_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> AcEnabled { get; init; }

    /// <summary>enable 802.11n [UNSTABLE]</summary>
    [JsonPropertyName("ht_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> HtEnabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApHeConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiApHeConfigurationPatch
{
    /// <summary>enable 802.11ax (HE) [UNSTABLE]</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiApConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiApConfigurationPatch
{
    /// <summary>band Description 2d4g 2.4 GHz 5g 5 GHz 6g 6 GHz 60g 60 GHz</summary>
    [JsonPropertyName("band")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Band { get; init; }

    /// <summary>wanted channel width (in MHz) : 20 MHz 40 MHz 80 MHz 160 MHz</summary>
    [JsonPropertyName("channel_width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<StringOrInteger>))]
    public Optional<StringOrInteger> ChannelWidth { get; init; }

    /// <summary>wanted primary channel, value of 0 means automatic selection</summary>
    [JsonPropertyName("primary_channel")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> PrimaryChannel { get; init; }

    /// <summary>wanted secondary channel, value of 0 means automatic selection</summary>
    [JsonPropertyName("secondary_channel")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> SecondaryChannel { get; init; }

    /// <summary>enable channels that require DFS</summary>
    [JsonPropertyName("dfs_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> DfsEnabled { get; init; }

    /// <summary>wifi ht config</summary>
    [JsonPropertyName("ht")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<WifiApHtConfigurationPatch>))]
    public Optional<WifiApHtConfigurationPatch> Ht { get; init; }

    /// <summary>wifi HE config</summary>
    [JsonPropertyName("he")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<WifiApHeConfigurationPatch>))]
    public Optional<WifiApHeConfigurationPatch> He { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiAp.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiAccessPointPatch
{
    /// <summary>ap configuration</summary>
    [JsonPropertyName("config")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<WifiApConfigurationPatch>))]
    public Optional<WifiApConfigurationPatch> Config { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiBssConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiBssConfigurationPatch
{
    /// <summary>enable this BSS. Note that if you want the AP to completely stop emitting wifi you should use WifiGlobalConfig enabled attribute.</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>bss displayed name</summary>
    [JsonPropertyName("ssid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Ssid { get; init; }

    /// <summary>don’t show bss in bss list</summary>
    [JsonPropertyName("hide_ssid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<StringOrBoolean>))]
    public Optional<StringOrBoolean> HideSsid { get; init; }

    /// <summary>Whether or not to use GCMP-256 (only in WPA3 &amp; for box that supports 802.11-be)</summary>
    [JsonPropertyName("gcmp256")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Gcmp256 { get; init; }

    /// <summary>encryption Description wep wep (should not use) wpa_psk_auto wpa1 CCMP+TKIP (should not use) wpa_psk_tkip wpa1 TKIP (should not use) wpa_psk_ccmp wpa1 CCMP (should not use) wpa12_psk_auto wpa1+wpa2 CCMP+TKIP (should not use) wpa2_psk_auto wpa2 CCMP+TKIP (should not use) wpa2_psk_tkip wpa2 TKIP (should not use) wpa2_psk_ccmp wpa2 CCMP wpa23_psk_ccmp wpa2+wpa3 CCMP WPA3-personal transition mode wpa23_psk_ccmp_mrsno wpa2+wpa3 CCMP WPA3-personal compatibility mode wpa3_psk_ccmp wpa3 CCMP WPA3-personal only mode</summary>
    [JsonPropertyName("encryption")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Encryption { get; init; }

    /// <summary>wifi key “ **** ” will be returned when insufficient permission</summary>
    [JsonPropertyName("key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Key { get; init; }

    [JsonPropertyName("wps_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> WpsEnabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiBss.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiBssPatch
{
    /// <summary>if set to True the bss will use the shared parameters stored under shared_bss_params if not the bss will use a configuration specific to this bss stored under bss_params when you want to edit the bss config you should change the config values using values from bss_params or shared_bss_params as a source and update use_shared_params accordingly.</summary>
    [JsonPropertyName("use_shared_params")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> UseSharedParams { get; init; }

    /// <summary>bss configuration (use this field for editing)</summary>
    [JsonPropertyName("config")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<WifiBssConfigurationPatch>))]
    public Optional<WifiBssConfigurationPatch> Config { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiPlanning.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiPlanningPatch
{
    /// <summary>is the planning enabled</summary>
    [JsonPropertyName("use_planning")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> UsePlanning { get; init; }

    /// <summary>mapping for planning : “on” or “off” mapping[0] is monday at 0:0 mapping[7 * resolution - 1] is sunday last slot (each slot has a duration of 60 * 24 / resolution minutes)</summary>
    [JsonPropertyName("mapping")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string[]>))]
    public Optional<string[]> Mapping { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiMacFilter.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiMacFilterPatch
{
    /// <summary>comment</summary>
    [JsonPropertyName("comment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Comment { get; init; }

    /// <summary>type Description whitelist if mac_filter is set to whitelist this station will be allowed blacklist if mac_filter is set to blacklist this station will be rejected</summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Type { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiMacFilter.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class CreateWifiMacFilterRequest
{
    /// <summary>MAC address to filter</summary>
    [JsonPropertyName("mac")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Mac { get; init; }

    /// <summary>comment</summary>
    [JsonPropertyName("comment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Comment { get; init; }

    /// <summary>type Description whitelist if mac_filter is set to whitelist this station will be allowed blacklist if mac_filter is set to blacklist this station will be rejected</summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Type { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-diag.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiDiagnosticFix
{
    /// <summary>The operation prose explicitly allows omitting aps and/or bsss.</summary>
    [JsonPropertyName("aps")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<WifiApDiagnosticFix[]>))]
    public Optional<WifiApDiagnosticFix[]> Aps { get; init; }

    /// <summary>The operation prose explicitly allows omitting aps and/or bsss.</summary>
    [JsonPropertyName("bsss")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<WifiBssDiagnosticFix[]>))]
    public Optional<WifiBssDiagnosticFix[]> Bsss { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-diag.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiApDiagnosticFix
{
    [JsonPropertyName("ap_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> ApId { get; init; }

    [JsonPropertyName("code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Code { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-diag.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiBssDiagnosticFix
{
    [JsonPropertyName("bssid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Bssid { get; init; }

    [JsonPropertyName("code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Code { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-wps-config-.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiWpsConfigurationPatch
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-wps-start-.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiWpsStartRequest
{
    [JsonPropertyName("bssid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Bssid { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#stop-a-wps-session.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiWpsStopRequest
{
    [JsonPropertyName("session_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> SessionId { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiCustomKeyConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WifiCustomKeyConfigurationPatch
{
    /// <summary>The name of the dedicated wifi network</summary>
    [JsonPropertyName("ssid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Ssid { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#WifiCustomKeyParams.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class CreateWifiCustomKeyRequest
{
    /// <summary>description of the custom key</summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Description { get; init; }

    /// <summary>Wi-Fi password for this custom access “ **** ” will be returned when insufficient permission</summary>
    [JsonPropertyName("key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Key { get; init; }

    /// <summary>Number of different hosts that can connect to this network (maximum 127) 0 has special meaning, it means unlimited number of users.</summary>
    [JsonPropertyName("max_use_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<StringOrInteger>))]
    public Optional<StringOrInteger> MaxUseCount { get; init; }

    /// <summary>Number of seconds before the custom access is revoked</summary>
    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Duration { get; init; }

    /// <summary>access_type Description full stations will get full access to local network + internet net_only stations connected using this custom key will be isolated and won’t have access to local network devices</summary>
    [JsonPropertyName("access_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> AccessType { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#TemporaryWifiDisable.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class TemporaryWifiDisableRequest
{
    /// <summary>temporary disable duration</summary>
    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Duration { get; init; }

    /// <summary>specify a wifi band to keep active keep Description 2d4g keep only 2,4Ghz band active 5g keep only 5GHz bands active 6g keep only 6GHz band active</summary>
    [JsonPropertyName("keep")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Keep { get; init; }

}
