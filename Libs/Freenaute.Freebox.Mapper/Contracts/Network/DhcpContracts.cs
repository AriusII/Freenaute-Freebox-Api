using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DhcpConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class DhcpConfig
{
    /// <summary>Enable/Disable the DHCP server</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>Always assign the same IP to a given host</summary>
    [JsonPropertyName("sticky_assign")]
    public bool? StickyAssign { get; init; }

    /// <summary>Gateway IP address</summary>
    [JsonPropertyName("gateway")]
    public string? Gateway { get; init; }

    /// <summary>Gateway subnet netmask</summary>
    [JsonPropertyName("netmask")]
    public string? Netmask { get; init; }

    /// <summary>DHCP range start IP</summary>
    [JsonPropertyName("ip_range_start")]
    public string? IpRangeStart { get; init; }

    /// <summary>DHCP range end IP</summary>
    [JsonPropertyName("ip_range_end")]
    public string? IpRangeEnd { get; init; }

    /// <summary>Always broadcast DHCP responses</summary>
    [JsonPropertyName("always_broadcast")]
    public bool? AlwaysBroadcast { get; init; }

    /// <summary>Ignore requested address if it is outside of the DHCP range</summary>
    [JsonPropertyName("ignore_out_of_range_hint")]
    public bool? IgnoreOutOfRangeHint { get; init; }

    /// <summary>Address of the TFTP server used when booting via TFTP.</summary>
    [JsonPropertyName("boot_server")]
    public string? BootServer { get; init; }

    /// <summary>Boot file to download from the TFTP server when booting via TFTP.</summary>
    [JsonPropertyName("boot_file")]
    public string? BootFile { get; init; }

    /// <summary>List of dns servers to include in DHCP reply</summary>
    [JsonPropertyName("dns")]
    public string[]? Dns { get; init; }

    /// <summary>List of dns options to include in DHCP reply</summary>
    [JsonPropertyName("options")]
    public DhcpOption[]? Options { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DhcpOption.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class DhcpOption
{
    /// <summary>The valid option identifiers and types are: Identifier Type Description time_offset s32 Time offset time_server ip_list Time server log_server ip_list Log server cookie_server ip_list Cookie server lpr_server ip_list LPR server impress_server ip_list Impress server resource_location_server ip_list Resource location server hostname string Hostname merit_dump_file string Merit dump file domain_name string Domain name swap_server ip_list Swap server root_path string Root path extensions_path string Extensions path ip_fwd bool IP forwarding ip_fwd_non_local bool Non-local IP source routing ip_max_reassembly_size u16 Maximum IP reassembly size ip_ttl u8 Default IP TTL ip_pmtu_timeout u32 IP Path MTU timeout mtu u16 Interface MTU local_subnets bool All subnets are local mask_discovery bool Perform mask discovery mask_supplier bool Mask supplier perform_rd bool Perform router discovery rs_address ip Router solicitation address trailer_encapsulation bool Trailer encapsulation arp_cache_timeout u32 ARP cache timeout eth_encapsulation bool Ethernet encapsulation tcp_ttl u8 Default TCP TTL tcp_keepalive_interval u32 TCP keepalive interval tcp_keepalive_garbage bool TCP keepalive garbage nis_domain string NIS domain nis_server ip_list NIS server ntp_server ip_list NTP server vendor_specific hexstring Vendor specific information nis_plus_domain string NIS+ domain nis_plus_server ip_list NIS+ server tftp_server_name string TFTP server name bootfile_name string Bootfile name mobile_ip_agent ip_list Mobile IP home agent smtp_server ip_list SMTP server pop3_server ip_list POP3 server nntp_server ip_list NNTP server www_server ip_list Default WWW server finger_server ip_list Default Finger server irc_server ip_list Default IRC server streettalk_server ip_list StreetTalk server stda_server ip_list StreetTalk directory assistance server slp_directory_agent ip_list SLP directory agent slp_service_scope hexstring SLP service scope nds_servers ip_list NDS servers nds_tree_name string NDS tree name nds_context string NDS context ldap_servers ip_list LDAP servers timezone_posix string Timezone POSIX timezone_database string Timezone database name_service hexstring Name service domain_search hexstring Domain search classless_static_route hexstring Classless static route capwap_ac ip_list CAPWAP access controller tftp_server_address ip_list TFTP server address</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>The value sent by the DHCP server when this option is requested by the client. The formats depend on the option type: ip: A single IPv4 address (as described in RFC 791) ip_list: A comma-separated list of IPv4 addresses string: A string of ASCII characters hexstring: A string of ASCII hexadecimal characters [0-9a-fA-F] representing a binary value (example: C0A801FE) bool: one of [ ‘true’, ‘false’, ‘1’, ‘0’ ] s8, s16, s32: An n-bit signed integer value u8, u16, u32: An n-bit unsigned integer value</summary>
    [JsonPropertyName("val")]
    public string? Val { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DhcpStaticLease.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class DhcpStaticLease
{
    /// <summary>DHCP static lease object id</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Host mac address</summary>
    [JsonPropertyName("mac")]
    public string? Mac { get; init; }

    /// <summary>an optional comment</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    /// <summary>hostname matching the mac address</summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; init; }

    /// <summary>IPv4 to assign to the host</summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; init; }

    /// <summary>LAN host information from LAN browser (refer to LanHost documentation)</summary>
    [JsonPropertyName("host")]
    public LanHost? Host { get; init; }

    /// <summary>List of dns options to include in DHCP reply</summary>
    [JsonPropertyName("options")]
    public DhcpOption[]? Options { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DhcpDynamicLease.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class DhcpDynamicLease
{
    /// <summary>Host mac address</summary>
    [JsonPropertyName("mac")]
    public string? Mac { get; init; }

    /// <summary>hostname matching the mac address</summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; init; }

    /// <summary>IPv4 assigned to the host</summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; init; }

    /// <summary>time left before lease needs to be refreshed</summary>
    [JsonPropertyName("lease_remaining")]
    public long? LeaseRemaining { get; init; }

    /// <summary>timestamp of the lease first assignment</summary>
    [JsonPropertyName("assign_time")]
    public long? AssignTime { get; init; }

    /// <summary>timestamp of the last lease refresh</summary>
    [JsonPropertyName("refresh_time")]
    public long? RefreshTime { get; init; }

    /// <summary>is the lease static</summary>
    [JsonPropertyName("is_static")]
    public bool? IsStatic { get; init; }

    /// <summary>LAN host information from LAN browser (refer to LanHost documentation)</summary>
    [JsonPropertyName("host")]
    public LanHost? Host { get; init; }

    [JsonPropertyName("options")]
    public DhcpOption[]? Options { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DhcpConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class DhcpConfigurationPatch
{
    /// <summary>Enable/Disable the DHCP server</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>Always assign the same IP to a given host</summary>
    [JsonPropertyName("sticky_assign")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> StickyAssign { get; init; }

    /// <summary>DHCP range start IP</summary>
    [JsonPropertyName("ip_range_start")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> IpRangeStart { get; init; }

    /// <summary>DHCP range end IP</summary>
    [JsonPropertyName("ip_range_end")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> IpRangeEnd { get; init; }

    /// <summary>Always broadcast DHCP responses</summary>
    [JsonPropertyName("always_broadcast")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> AlwaysBroadcast { get; init; }

    /// <summary>Ignore requested address if it is outside of the DHCP range</summary>
    [JsonPropertyName("ignore_out_of_range_hint")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> IgnoreOutOfRangeHint { get; init; }

    /// <summary>Address of the TFTP server used when booting via TFTP.</summary>
    [JsonPropertyName("boot_server")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> BootServer { get; init; }

    /// <summary>Boot file to download from the TFTP server when booting via TFTP.</summary>
    [JsonPropertyName("boot_file")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> BootFile { get; init; }

    /// <summary>List of dns servers to include in DHCP reply</summary>
    [JsonPropertyName("dns")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string[]>))]
    public Optional<string[]> Dns { get; init; }

    /// <summary>List of dns options to include in DHCP reply</summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<DhcpOptionWrite[]>))]
    public Optional<DhcpOptionWrite[]> Options { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DhcpOption.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class DhcpOptionWrite
{
    /// <summary>The valid option identifiers and types are: Identifier Type Description time_offset s32 Time offset time_server ip_list Time server log_server ip_list Log server cookie_server ip_list Cookie server lpr_server ip_list LPR server impress_server ip_list Impress server resource_location_server ip_list Resource location server hostname string Hostname merit_dump_file string Merit dump file domain_name string Domain name swap_server ip_list Swap server root_path string Root path extensions_path string Extensions path ip_fwd bool IP forwarding ip_fwd_non_local bool Non-local IP source routing ip_max_reassembly_size u16 Maximum IP reassembly size ip_ttl u8 Default IP TTL ip_pmtu_timeout u32 IP Path MTU timeout mtu u16 Interface MTU local_subnets bool All subnets are local mask_discovery bool Perform mask discovery mask_supplier bool Mask supplier perform_rd bool Perform router discovery rs_address ip Router solicitation address trailer_encapsulation bool Trailer encapsulation arp_cache_timeout u32 ARP cache timeout eth_encapsulation bool Ethernet encapsulation tcp_ttl u8 Default TCP TTL tcp_keepalive_interval u32 TCP keepalive interval tcp_keepalive_garbage bool TCP keepalive garbage nis_domain string NIS domain nis_server ip_list NIS server ntp_server ip_list NTP server vendor_specific hexstring Vendor specific information nis_plus_domain string NIS+ domain nis_plus_server ip_list NIS+ server tftp_server_name string TFTP server name bootfile_name string Bootfile name mobile_ip_agent ip_list Mobile IP home agent smtp_server ip_list SMTP server pop3_server ip_list POP3 server nntp_server ip_list NNTP server www_server ip_list Default WWW server finger_server ip_list Default Finger server irc_server ip_list Default IRC server streettalk_server ip_list StreetTalk server stda_server ip_list StreetTalk directory assistance server slp_directory_agent ip_list SLP directory agent slp_service_scope hexstring SLP service scope nds_servers ip_list NDS servers nds_tree_name string NDS tree name nds_context string NDS context ldap_servers ip_list LDAP servers timezone_posix string Timezone POSIX timezone_database string Timezone database name_service hexstring Name service domain_search hexstring Domain search classless_static_route hexstring Classless static route capwap_ac ip_list CAPWAP access controller tftp_server_address ip_list TFTP server address</summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Id { get; init; }

    /// <summary>The value sent by the DHCP server when this option is requested by the client. The formats depend on the option type: ip: A single IPv4 address (as described in RFC 791) ip_list: A comma-separated list of IPv4 addresses string: A string of ASCII characters hexstring: A string of ASCII hexadecimal characters [0-9a-fA-F] representing a binary value (example: C0A801FE) bool: one of [ ‘true’, ‘false’, ‘1’, ‘0’ ] s8, s16, s32: An n-bit signed integer value u8, u16, u32: An n-bit unsigned integer value</summary>
    [JsonPropertyName("val")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Val { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DhcpStaticLease.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class DhcpStaticLeasePatch
{
    /// <summary>Host mac address</summary>
    [JsonPropertyName("mac")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Mac { get; init; }

    /// <summary>an optional comment</summary>
    [JsonPropertyName("comment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Comment { get; init; }

    /// <summary>IPv4 to assign to the host</summary>
    [JsonPropertyName("ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Ip { get; init; }

    /// <summary>List of dns options to include in DHCP reply</summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<DhcpOptionWrite[]>))]
    public Optional<DhcpOptionWrite[]> Options { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DhcpStaticLease.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class CreateDhcpStaticLeaseRequest
{
    /// <summary>Host mac address</summary>
    [JsonPropertyName("mac")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Mac { get; init; }

    /// <summary>an optional comment</summary>
    [JsonPropertyName("comment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Comment { get; init; }

    /// <summary>IPv4 to assign to the host</summary>
    [JsonPropertyName("ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Ip { get; init; }

    /// <summary>List of dns options to include in DHCP reply</summary>
    [JsonPropertyName("options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<DhcpOptionWrite[]>))]
    public Optional<DhcpOptionWrite[]> Options { get; init; }

}
