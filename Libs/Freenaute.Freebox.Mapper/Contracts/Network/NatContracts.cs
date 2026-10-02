using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DmzConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class DmzConfig
{
    /// <summary>dmz host IP</summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; init; }

    /// <summary>is dmz enabled</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#PortForwardingConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class PortForwardingConfig
{
    /// <summary>forwarding id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>is forwarding enabled</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>ip_proto Description tcp TCP udp UDP</summary>
    [JsonPropertyName("ip_proto")]
    public string? IpProto { get; init; }

    /// <summary>forwarding range start</summary>
    [JsonPropertyName("wan_port_start")]
    public StringOrInteger? WanPortStart { get; init; }

    /// <summary>forwarding range end</summary>
    [JsonPropertyName("wan_port_end")]
    public long? WanPortEnd { get; init; }

    /// <summary>forwarding target on LAN</summary>
    [JsonPropertyName("lan_ip")]
    public string? LanIp { get; init; }

    /// <summary>forwarding target start port on LAN, (last port is lan_port + wan_port_end - wan_port_start)</summary>
    [JsonPropertyName("lan_port")]
    public long? LanPort { get; init; }

    /// <summary>forwarding target host name</summary>
    [JsonPropertyName("hostname")]
    public string? Hostname { get; init; }

    /// <summary>forwarding target host information (see: LanHost )</summary>
    [JsonPropertyName("host")]
    public LanHost? Host { get; init; }

    /// <summary>if src_ip == 0.0.0.0 this rule will apply to any src ip otherwise it will only apply to the specified ip address</summary>
    [JsonPropertyName("src_ip")]
    public string? SrcIp { get; init; }

    /// <summary>comment</summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#IncomingPortConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class IncomingPortConfig
{
    /// <summary>incoming port id id Description http http port for remote access to Freebox OS https https port for tls remote access to Freebox OS bittorrent-main main bittorrent port for Freebox downloader bittorrent-dht bittorrent port for DHT openvpn_routed routed openvpn port openvpn_bridge bridged openvpn port ipsec_ike ipsec ikev2 vpn port ipsec_nat ipsec nat vpn port pptp pptp vpn server port ftp ftp control port for FTP remote access ftp_pasv ftp data port for FTP remote access</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>is the port binding allowed</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>is the port binding currently active</summary>
    [JsonPropertyName("active")]
    public bool? Active { get; init; }

    /// <summary>ip_proto Description tcp TCP udp UDP tcp_udp both TCP and UDP</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>binding port</summary>
    [JsonPropertyName("in_port")]
    public long? InPort { get; init; }

    /// <summary>network namespace. The service may be running on a different namespace (for instance if the service uses the vpn client).</summary>
    [JsonPropertyName("netns")]
    public string? Netns { get; init; }

    /// <summary>This field indicate the minimum possible value for in_port (see ConnectionStatus ipv4_port_range)</summary>
    [JsonPropertyName("min_port")]
    public long? MinPort { get; init; }

    /// <summary>This field indicate the maximum possible value for in_port (see ConnectionStatus ipv4_port_range)</summary>
    [JsonPropertyName("max_port")]
    public long? MaxPort { get; init; }

    /// <summary>If set to true, the in_port field cannot be changed because of the underlying protocol does not allow it</summary>
    [JsonPropertyName("readonly")]
    public bool? Readonly { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DmzConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class DmzConfigurationPatch
{
    /// <summary>dmz host IP</summary>
    [JsonPropertyName("ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Ip { get; init; }

    /// <summary>is dmz enabled</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#PortForwardingConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class PortForwardingPatch
{
    /// <summary>is forwarding enabled</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>ip_proto Description tcp TCP udp UDP</summary>
    [JsonPropertyName("ip_proto")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> IpProto { get; init; }

    /// <summary>forwarding range start</summary>
    [JsonPropertyName("wan_port_start")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<StringOrInteger>))]
    public Optional<StringOrInteger> WanPortStart { get; init; }

    /// <summary>forwarding range end</summary>
    [JsonPropertyName("wan_port_end")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> WanPortEnd { get; init; }

    /// <summary>forwarding target on LAN</summary>
    [JsonPropertyName("lan_ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LanIp { get; init; }

    /// <summary>forwarding target start port on LAN, (last port is lan_port + wan_port_end - wan_port_start)</summary>
    [JsonPropertyName("lan_port")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> LanPort { get; init; }

    /// <summary>if src_ip == 0.0.0.0 this rule will apply to any src ip otherwise it will only apply to the specified ip address</summary>
    [JsonPropertyName("src_ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> SrcIp { get; init; }

    /// <summary>comment</summary>
    [JsonPropertyName("comment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Comment { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#PortForwardingConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class CreatePortForwardingRequest
{
    /// <summary>is forwarding enabled</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>ip_proto Description tcp TCP udp UDP</summary>
    [JsonPropertyName("ip_proto")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> IpProto { get; init; }

    /// <summary>forwarding range start</summary>
    [JsonPropertyName("wan_port_start")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<StringOrInteger>))]
    public Optional<StringOrInteger> WanPortStart { get; init; }

    /// <summary>forwarding range end</summary>
    [JsonPropertyName("wan_port_end")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> WanPortEnd { get; init; }

    /// <summary>forwarding target on LAN</summary>
    [JsonPropertyName("lan_ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LanIp { get; init; }

    /// <summary>forwarding target start port on LAN, (last port is lan_port + wan_port_end - wan_port_start)</summary>
    [JsonPropertyName("lan_port")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> LanPort { get; init; }

    /// <summary>if src_ip == 0.0.0.0 this rule will apply to any src ip otherwise it will only apply to the specified ip address</summary>
    [JsonPropertyName("src_ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> SrcIp { get; init; }

    /// <summary>comment</summary>
    [JsonPropertyName("comment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Comment { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#IncomingPortConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class IncomingPortPatch
{
    /// <summary>is the port binding allowed</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>binding port</summary>
    [JsonPropertyName("in_port")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> InPort { get; init; }

}
