using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#ConnectionStatus.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class ConnectionStatus
{
    /// <summary>State Description going_up connection is initializing up connection is active going_down connection is about to become inactive down connection is inactive</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>Type Description ethernet FTTH/ethernet rfc2684 xDSL (unbundled) pppoatm xDSL</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Media Description ftth FTTH ethernet ethernet xdsl xDSL backup_4g Internet Backup</summary>
    [JsonPropertyName("media")]
    public string? Media { get; init; }

    /// <summary>Freebox IPv4 address NOTE: this field is only available when connection state is up</summary>
    [JsonPropertyName("ipv4")]
    public string? Ipv4 { get; init; }

    /// <summary>Freebox IPv6 address NOTE: this field is only available when connection state is up</summary>
    [JsonPropertyName("ipv6")]
    public string? Ipv6 { get; init; }

    /// <summary>current upload rate in byte/s</summary>
    [JsonPropertyName("rate_up")]
    public long? RateUp { get; init; }

    /// <summary>current download rate in byte/s</summary>
    [JsonPropertyName("rate_down")]
    public long? RateDown { get; init; }

    /// <summary>available upload bandwidth in bit/s</summary>
    [JsonPropertyName("bandwidth_up")]
    public long? BandwidthUp { get; init; }

    /// <summary>available download bandwidth in bit/s</summary>
    [JsonPropertyName("bandwidth_down")]
    public long? BandwidthDown { get; init; }

    /// <summary>total uploaded bytes since last connection</summary>
    [JsonPropertyName("bytes_up")]
    public long? BytesUp { get; init; }

    /// <summary>total downloaded bytes since last connection</summary>
    [JsonPropertyName("bytes_down")]
    public long? BytesDown { get; init; }

    /// <summary>Some customers share the same IPv4 and each customer is then assigned a port range. The first value is the first port of the assigned range and the second value is the last port (inclusive). All PortForwardingConfig must use ports in this range to be effective.</summary>
    [JsonPropertyName("ipv4_port_range")]
    public long[]? Ipv4PortRange { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#ConnectionConfiguration.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class ConnectionConfiguration
{
    /// <summary>should the Freebox respond to external ping requests</summary>
    [JsonPropertyName("ping")]
    public bool? Ping { get; init; }

    /// <summary>is the admin password secure enough to enable remote access</summary>
    [JsonPropertyName("is_secure_pass")]
    public bool? IsSecurePass { get; init; }

    /// <summary>enable/disable HTTP remote access</summary>
    [JsonPropertyName("remote_access")]
    public bool? RemoteAccess { get; init; }

    /// <summary>port number to use for remote HTTP access</summary>
    [JsonPropertyName("remote_access_port")]
    public long? RemoteAccessPort { get; init; }

    /// <summary>This field indicate the minimum possible value for remote_access_port (see ConnectionStatus ipv4_port_range)</summary>
    [JsonPropertyName("remote_access_min_port")]
    public long? RemoteAccessMinPort { get; init; }

    /// <summary>This field indicate the maximum possible value for remote_access_port (see ConnectionStatus ipv4_port_range)</summary>
    [JsonPropertyName("remote_access_max_port")]
    public long? RemoteAccessMaxPort { get; init; }

    /// <summary>IPv4 to use for remote access (can be missing if connection is down)</summary>
    [JsonPropertyName("remote_access_ip")]
    public string? RemoteAccessIp { get; init; }

    /// <summary>is remote access enabled for apps, or share link</summary>
    [JsonPropertyName("api_remote_access")]
    public bool? ApiRemoteAccess { get; init; }

    /// <summary>enable/disable Wake-on-lan proxy</summary>
    [JsonPropertyName("wol")]
    public bool? Wol { get; init; }

    /// <summary>is ads blocking feature enabled</summary>
    [JsonPropertyName("adblock")]
    public bool? Adblock { get; init; }

    /// <summary>if set to true adblock setting has never been set by the user</summary>
    [JsonPropertyName("adblock_not_set")]
    public bool? AdblockNotSet { get; init; }

    /// <summary>if false, user has disabled new token request. New apps can’t request a new token. Apps that already have a token are still allowed</summary>
    [JsonPropertyName("allow_token_request")]
    public bool? AllowTokenRequest { get; init; }

    /// <summary>Status Description disabled Fully disable SIP ALG direct_media Enable SIP ALG, RTP only allowed between SIP UA any_media Enable SIP ALG, RTP allowed between any host (dangerous for untrusted hosts)</summary>
    [JsonPropertyName("sip_alg")]
    public string? SipAlg { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#ConnectionIpv6Delegation.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class ConnectionIpv6Delegation
{
    /// <summary>IPv6 prefix</summary>
    [JsonPropertyName("prefix")]
    public string? Prefix { get; init; }

    /// <summary>the next hop for the prefix</summary>
    [JsonPropertyName("next_hop")]
    public string? NextHop { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#ConnectionIpv6Configuration.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class ConnectionIpv6Configuration
{
    /// <summary>is IPv6 enabled</summary>
    [JsonPropertyName("ipv6_enabled")]
    public bool? Ipv6Enabled { get; init; }

    /// <summary>is IPv6 firewall enabled</summary>
    [JsonPropertyName("ipv6_firewall")]
    public bool? Ipv6Firewall { get; init; }

    /// <summary>is IPv6 firewall enabled on secondary prefixes</summary>
    [JsonPropertyName("ipv6_prefix_firewall")]
    public bool? Ipv6PrefixFirewall { get; init; }

    /// <summary>Freebox IPv6 link local address</summary>
    [JsonPropertyName("ipv6ll")]
    public string? Ipv6ll { get; init; }

    /// <summary>list of IPv6 delegations</summary>
    [JsonPropertyName("delegations")]
    public ConnectionIpv6Delegation[]? Delegations { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#XdslStatus.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class XdslStatus
{
    /// <summary>Status Description down unsynchronized training synchronizing step 1/4 started synchronizing step 2/4 chan_analysis synchronizing step 3/4 msg_exchange synchronizing step 4/4 showtime Ready disabled Disabled</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>Protocol Description t1413 T1.413 adsl1_a ADSL adsl2_a ADSL2 adsl2plus_a ADSL2+ readsl2 ReachDSL adsl2_m ADSL2 annex M adsl2plus_m ADSL2+ annex M unknown Unknown</summary>
    [JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    /// <summary>Protocol Description adsl ADSL vdsl VDSL</summary>
    [JsonPropertyName("modulation")]
    public string? Modulation { get; init; }

    /// <summary>uptime in seconds</summary>
    [JsonPropertyName("uptime")]
    public long? Uptime { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#XdslStats.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class XdslStats
{
    /// <summary>ATM max rate in kbit/s</summary>
    [JsonPropertyName("maxrate")]
    public long? Maxrate { get; init; }

    /// <summary>ATM rate in kbit/s</summary>
    [JsonPropertyName("rate")]
    public long? Rate { get; init; }

    /// <summary>in dB</summary>
    [JsonPropertyName("snr")]
    public long? Snr { get; init; }

    /// <summary>in dB</summary>
    [JsonPropertyName("attn")]
    public long? Attn { get; init; }

    /// <summary>in dB/10</summary>
    [JsonPropertyName("snr_10")]
    public long? Snr10 { get; init; }

    /// <summary>in dB/10</summary>
    [JsonPropertyName("attn_10")]
    public long? Attn10 { get; init; }

    [JsonPropertyName("fec")]
    public long? Fec { get; init; }

    [JsonPropertyName("crc")]
    public long? Crc { get; init; }

    [JsonPropertyName("hec")]
    public long? Hec { get; init; }

    [JsonPropertyName("es")]
    public long? Es { get; init; }

    [JsonPropertyName("ses")]
    public long? Ses { get; init; }

    [JsonPropertyName("phyr")]
    public bool? Phyr { get; init; }

    [JsonPropertyName("ginp")]
    public bool? Ginp { get; init; }

    [JsonPropertyName("nitro")]
    public bool? Nitro { get; init; }

    /// <summary>only available when phyr is on</summary>
    [JsonPropertyName("rxmt")]
    public long? Rxmt { get; init; }

    /// <summary>only available when phyr is on</summary>
    [JsonPropertyName("rxmt_corr")]
    public long? RxmtCorr { get; init; }

    /// <summary>only available when phyr is on</summary>
    [JsonPropertyName("rxmt_uncorr")]
    public long? RxmtUncorr { get; init; }

    /// <summary>only available when ginp is on</summary>
    [JsonPropertyName("rtx_tx")]
    public long? RtxTx { get; init; }

    /// <summary>only available when ginp is on</summary>
    [JsonPropertyName("rtx_c")]
    public long? RtxC { get; init; }

    /// <summary>only available when ginp is on</summary>
    [JsonPropertyName("rtx_uc")]
    public long? RtxUc { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#XdslInfos.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class XdslInfos
{
    [JsonPropertyName("status")]
    public XdslStatus? Status { get; init; }

    [JsonPropertyName("down")]
    public XdslStats? Down { get; init; }

    [JsonPropertyName("up")]
    public XdslStats? Up { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LteRadioBand.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LteRadioBand
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("bandwidth")]
    public long? Bandwidth { get; init; }

    [JsonPropertyName("rsrq")]
    public long? Rsrq { get; init; }

    [JsonPropertyName("rsrp")]
    public long? Rsrp { get; init; }

    [JsonPropertyName("rssi")]
    public long? Rssi { get; init; }

    [JsonPropertyName("band")]
    public long? Band { get; init; }

    [JsonPropertyName("pci")]
    public long? Pci { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LteNetwork.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LteNetwork
{
    [JsonPropertyName("pdn_up")]
    public bool? PdnUp { get; init; }

    [JsonPropertyName("has_ipv6")]
    public bool? HasIpv6 { get; init; }

    [JsonPropertyName("ipv6_dns")]
    public string? Ipv6Dns { get; init; }

    [JsonPropertyName("ipv6")]
    public string? Ipv6 { get; init; }

    [JsonPropertyName("ipv6_netmask")]
    public string? Ipv6Netmask { get; init; }

    [JsonPropertyName("has_ipv4")]
    public bool? HasIpv4 { get; init; }

    [JsonPropertyName("ipv4_dns")]
    public string? Ipv4Dns { get; init; }

    [JsonPropertyName("ipv4")]
    public string? Ipv4 { get; init; }

    [JsonPropertyName("ipv4_netmask")]
    public string? Ipv4Netmask { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LteSim.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LteSim
{
    [JsonPropertyName("present")]
    public bool? Present { get; init; }

    [JsonPropertyName("pin_locked")]
    public bool? PinLocked { get; init; }

    [JsonPropertyName("puk_remaining")]
    public long? PukRemaining { get; init; }

    [JsonPropertyName("iccid")]
    public string? Iccid { get; init; }

    [JsonPropertyName("puk_locked")]
    public bool? PukLocked { get; init; }

    [JsonPropertyName("pin_remaining")]
    public long? PinRemaining { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LteTunnelDetails.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LteTunnelDetails
{
    [JsonPropertyName("connected")]
    public bool? Connected { get; init; }

    [JsonPropertyName("last_error")]
    public string? LastError { get; init; }

    [JsonPropertyName("tx_flows_rate")]
    public long? TxFlowsRate { get; init; }

    [JsonPropertyName("tx_max_rate")]
    public long? TxMaxRate { get; init; }

    [JsonPropertyName("tx_used_rate")]
    public long? TxUsedRate { get; init; }

    [JsonPropertyName("rx_flows_rate")]
    public long? RxFlowsRate { get; init; }

    [JsonPropertyName("rx_max_rate")]
    public long? RxMaxRate { get; init; }

    [JsonPropertyName("rx_used_rate")]
    public long? RxUsedRate { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LteTunnel.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LteTunnel
{
    [JsonPropertyName("lte")]
    public LteTunnelDetails? Lte { get; init; }

    [JsonPropertyName("xdsl")]
    public LteTunnelDetails? Xdsl { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#FtthStatus.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class FtthStatus
{
    [JsonPropertyName("sfp_present")]
    public bool? SfpPresent { get; init; }

    [JsonPropertyName("sfp_alim_ok")]
    public bool? SfpAlimOk { get; init; }

    [JsonPropertyName("sfp_has_power_report")]
    public bool? SfpHasPowerReport { get; init; }

    [JsonPropertyName("sfp_has_signal")]
    public bool? SfpHasSignal { get; init; }

    [JsonPropertyName("link")]
    public bool? Link { get; init; }

    [JsonPropertyName("sfp_serial")]
    public string? SfpSerial { get; init; }

    [JsonPropertyName("sfp_model")]
    public string? SfpModel { get; init; }

    [JsonPropertyName("sfp_vendor")]
    public string? SfpVendor { get; init; }

    /// <summary>scaled by 100 (in dBm)</summary>
    [JsonPropertyName("sfp_pwr_tx")]
    public long? SfpPwrTx { get; init; }

    /// <summary>scaled by 100 (in dBm)</summary>
    [JsonPropertyName("sfp_pwr_rx")]
    public long? SfpPwrRx { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DDNSStatus.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class DDNSStatus
{
    /// <summary>Status Description disabled Disabled ok Ok wait Updating reqfail Request failed authfail Authentication error nocredential Invalid credential ipinval Invalid IP hostinval Invalid hostname abuse Blocked because of abuse dnserror DNS error unavailable Service unavailable nowan Unable to get wan IP unknown Unknown</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>next refresh timestamp</summary>
    [JsonPropertyName("next_refresh")]
    public long? NextRefresh { get; init; }

    /// <summary>last refresh timestamp</summary>
    [JsonPropertyName("last_refresh")]
    public long? LastRefresh { get; init; }

    /// <summary>next retry timestamp</summary>
    [JsonPropertyName("next_retry")]
    public long? NextRetry { get; init; }

    /// <summary>last error timestamp</summary>
    [JsonPropertyName("last_error")]
    public long? LastError { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DDNSConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class DDNSConfig
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>dns name to use to register</summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; init; }

    /// <summary>username to use to register</summary>
    [JsonPropertyName("user")]
    public string? User { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-aggregation.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LteAggregationResult
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("tunnel")]
    public LteTunnel? Tunnel { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#ConnectionConfiguration.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class ConnectionConfigurationPatch
{
    /// <summary>should the Freebox respond to external ping requests</summary>
    [JsonPropertyName("ping")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Ping { get; init; }

    /// <summary>enable/disable HTTP remote access</summary>
    [JsonPropertyName("remote_access")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> RemoteAccess { get; init; }

    /// <summary>port number to use for remote HTTP access</summary>
    [JsonPropertyName("remote_access_port")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> RemoteAccessPort { get; init; }

    /// <summary>enable/disable Wake-on-lan proxy</summary>
    [JsonPropertyName("wol")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Wol { get; init; }

    /// <summary>is ads blocking feature enabled</summary>
    [JsonPropertyName("adblock")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Adblock { get; init; }

    /// <summary>if false, user has disabled new token request. New apps can’t request a new token. Apps that already have a token are still allowed</summary>
    [JsonPropertyName("allow_token_request")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> AllowTokenRequest { get; init; }

    /// <summary>Status Description disabled Fully disable SIP ALG direct_media Enable SIP ALG, RTP only allowed between SIP UA any_media Enable SIP ALG, RTP allowed between any host (dangerous for untrusted hosts)</summary>
    [JsonPropertyName("sip_alg")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> SipAlg { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#ConnectionIpv6Configuration.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class ConnectionIpv6ConfigurationPatch
{
    /// <summary>is IPv6 enabled</summary>
    [JsonPropertyName("ipv6_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Ipv6Enabled { get; init; }

    /// <summary>is IPv6 firewall enabled</summary>
    [JsonPropertyName("ipv6_firewall")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Ipv6Firewall { get; init; }

    /// <summary>is IPv6 firewall enabled on secondary prefixes</summary>
    [JsonPropertyName("ipv6_prefix_firewall")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Ipv6PrefixFirewall { get; init; }

    /// <summary>list of IPv6 delegations</summary>
    [JsonPropertyName("delegations")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ConnectionIpv6DelegationPatch[]>))]
    public Optional<ConnectionIpv6DelegationPatch[]> Delegations { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#ConnectionIpv6Delegation.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class ConnectionIpv6DelegationPatch
{
    /// <summary>IPv6 prefix</summary>
    [JsonPropertyName("prefix")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Prefix { get; init; }

    /// <summary>the next hop for the prefix</summary>
    [JsonPropertyName("next_hop")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> NextHop { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-aggregation.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class LteAggregationUpdate
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DDNSConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class DdnsConfigurationPatch
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>dns name to use to register</summary>
    [JsonPropertyName("hostname")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Hostname { get; init; }

    /// <summary>password to use to register</summary>
    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Password { get; init; }

    /// <summary>username to use to register</summary>
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> User { get; init; }

}
