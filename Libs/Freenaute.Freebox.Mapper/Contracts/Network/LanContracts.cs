using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LanConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LanConfig
{
    /// <summary>Freebox Server IPv4 address</summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; init; }

    /// <summary>Freebox Server name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Freebox Server DNS name</summary>
    [JsonPropertyName("name_dns")]
    public string? NameDns { get; init; }

    /// <summary>Freebox Server mDNS name</summary>
    [JsonPropertyName("name_mdns")]
    public string? NameMdns { get; init; }

    /// <summary>Freebox Server netbios name</summary>
    [JsonPropertyName("name_netbios")]
    public string? NameNetbios { get; init; }

    /// <summary>The valid LAN modes are: Type Description router The Freebox acts as a network router bridge The Freebox acts as a network bridge NOTE: in bridge mode, most of Freebox services are disabled. It is recommended to use the router mode, and third party apps should not change this setting</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Example-only mode key; formal property is type. Both read separately, neither writable until conflict resolved.</summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#Route.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class Route
{
    /// <summary>Destination network IPv4 prefix in CIDR format (e.g. 192.168.1.0/24). A prefix is considered invalid if it is a subprefix of any reserved network listed below. Network Description 127.0.0.0/8 Loopback network 169.254.0.0/16 Link-local addresses 224.0.0.0/4 IANA: multicast 192.168.27.0/24 Used for VPN and guest WIFI addresses Only one enabled route may exist for a given prefix. An exists error will be returned if multiple active routes share the same prefix.</summary>
    [JsonPropertyName("prefix")]
    public string? Prefix { get; init; }

    /// <summary>IP address of the next-hop gateway.</summary>
    [JsonPropertyName("gateway")]
    public string? Gateway { get; init; }

    /// <summary>If false the route is not added to the routing table.</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>Optional text describing the route.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LanHost.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LanHost
{
    /// <summary>Host id (unique on this interface)</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Host primary name (chosen from the list of available names, or manually set by user)</summary>
    [JsonPropertyName("primary_name")]
    public string? PrimaryName { get; init; }

    /// <summary>Host domain name on the local network (manually set by user, or automatically configured during device registration). The string must respect the following rules: Must end with ‘.home’ 63 characters long at max Only alphabetical characters are accepted Digits are accepted provided they are not placed at the beginning of the string, nor after another dot character. Hyphens and dots are accepted provided they are not placed at the beginning or the end of the string, nor after or before another dot character. It is also possible to use an empty string. This special value means no local domain should be registered for this host.</summary>
    [JsonPropertyName("domain_name")]
    public string? DomainName { get; init; }

    /// <summary>When possible, the Freebox will try to guess the host_type, but you can manually override this to the correct value Possible values are: source Description workstation Workstation laptop Laptop smartphone Smartphone tablet Tablet printer Printer vg_console Video game console television TV nas Nas ip_camera IP Camera ip_phone IP Phone freebox_player Freebox Player freebox_hd Freebox HD freebox_crystal Freebox Crystal freebox_mini Freebox Mini 4k freebox_delta Freebox Delta freebox_one Freebox One freebox_wifi Freebox Wi-Fi Pop freebox_pop Freebox Pop networking_device Networking device multimedia_device Multimedia device car Connected car watch Smartwatch light Light outlet Connected outlet appliances Household appliances thermostat Thermostat shutter Electric shutter other Other</summary>
    [JsonPropertyName("host_type")]
    public string? HostType { get; init; }

    /// <summary>If true the primary name has been set manually</summary>
    [JsonPropertyName("primary_name_manual")]
    public bool? PrimaryNameManual { get; init; }

    /// <summary>Layer 2 network id and its type</summary>
    [JsonPropertyName("l2ident")]
    [JsonConverter(typeof(ObjectOrArrayJsonConverter<LanHostL2Ident>))]
    public ObjectOrArray<LanHostL2Ident>? L2Ident { get; init; }

    /// <summary>Host vendor name (from the mac address)</summary>
    [JsonPropertyName("vendor_name")]
    public string? VendorName { get; init; }

    /// <summary>If true the host is always shown even if it has not been active since the Freebox startup</summary>
    [JsonPropertyName("persistent")]
    public bool? Persistent { get; init; }

    /// <summary>If true the host can receive traffic from the Freebox</summary>
    [JsonPropertyName("reachable")]
    public bool? Reachable { get; init; }

    /// <summary>Last time the host was reached</summary>
    [JsonPropertyName("last_time_reachable")]
    public long? LastTimeReachable { get; init; }

    /// <summary>If true the host sends traffic to the Freebox</summary>
    [JsonPropertyName("active")]
    public bool? Active { get; init; }

    /// <summary>Last time the host sent traffic</summary>
    [JsonPropertyName("last_activity")]
    public long? LastActivity { get; init; }

    /// <summary>First time the host sent traffic, or 0 (Unix Epoch) if it wasn’t seen before this field was added.</summary>
    [JsonPropertyName("first_activity")]
    public long? FirstActivity { get; init; }

    /// <summary>List of available names, and their source</summary>
    [JsonPropertyName("names")]
    public LanHostName[]? Names { get; init; }

    /// <summary>List of available layer 3 network connections</summary>
    [JsonPropertyName("l3connectivities")]
    public LanHostL3Connectivity[]? L3Connectivities { get; init; }

    /// <summary>If device is associated with a profile, contains profile summary.</summary>
    [JsonPropertyName("network_control")]
    public LanHostNetworkControl? NetworkControl { get; init; }

    /// <summary>Contains detailed information that could be gathered about the device.</summary>
    [JsonPropertyName("info")]
    public Dictionary<string, JsonElement>? Info { get; init; }

    [JsonPropertyName("interface")]
    public string? Interface { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LanHostName.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LanHostName
{
    /// <summary>Host name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>source of the name</summary>
    [JsonPropertyName("source")]
    public string? Source { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LanHostL2Ident.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LanHostL2Ident
{
    /// <summary>Layer 2 id</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Type of layer 2 address source Description dhcp DHCP netbios Netbios mdns mDNS hostname mdns_srv mDNS service upnp UPnP wsd WS-Discovery</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LanHostL3Connectivity.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LanHostL3Connectivity
{
    /// <summary>Layer 3 address</summary>
    [JsonPropertyName("addr")]
    public string? Addr { get; init; }

    /// <summary>af Description ipv4 IPv4 ipv6 IPv6</summary>
    [JsonPropertyName("af")]
    public string? Af { get; init; }

    /// <summary>is the connection active</summary>
    [JsonPropertyName("active")]
    public bool? Active { get; init; }

    /// <summary>is the connection reachable</summary>
    [JsonPropertyName("reachable")]
    public bool? Reachable { get; init; }

    /// <summary>last activity timestamp</summary>
    [JsonPropertyName("last_activity")]
    public long? LastActivity { get; init; }

    /// <summary>last reachable timestamp</summary>
    [JsonPropertyName("last_time_reachable")]
    public long? LastTimeReachable { get; init; }

    /// <summary>device model if known</summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LanHostNetworkControl.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LanHostNetworkControl
{
    /// <summary>Id of profile this device is associated with.</summary>
    [JsonPropertyName("profile_id")]
    public long? ProfileId { get; init; }

    /// <summary>Name of profile this device is associated with.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Mode described in Network Control Object</summary>
    [JsonPropertyName("current_mode")]
    public string? CurrentMode { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v8-lan-browser-interfaces-.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LanBrowserInterface
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("host_count")]
    public long? HostCount { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v8-lan-browser-types-.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class LanHostTypeDescriptor
{
    [JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("category")]
    public string? Category { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LanConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class LanConfigurationPatch
{
    /// <summary>Freebox Server IPv4 address</summary>
    [JsonPropertyName("ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Ip { get; init; }

    /// <summary>Freebox Server name</summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Name { get; init; }

    /// <summary>Freebox Server DNS name</summary>
    [JsonPropertyName("name_dns")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> NameDns { get; init; }

    /// <summary>Freebox Server mDNS name</summary>
    [JsonPropertyName("name_mdns")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> NameMdns { get; init; }

    /// <summary>Freebox Server netbios name</summary>
    [JsonPropertyName("name_netbios")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> NameNetbios { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#Route.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class LanRouteWrite
{
    /// <summary>Destination network IPv4 prefix in CIDR format (e.g. 192.168.1.0/24). A prefix is considered invalid if it is a subprefix of any reserved network listed below. Network Description 127.0.0.0/8 Loopback network 169.254.0.0/16 Link-local addresses 224.0.0.0/4 IANA: multicast 192.168.27.0/24 Used for VPN and guest WIFI addresses Only one enabled route may exist for a given prefix. An exists error will be returned if multiple active routes share the same prefix.</summary>
    [JsonPropertyName("prefix")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Prefix { get; init; }

    /// <summary>IP address of the next-hop gateway.</summary>
    [JsonPropertyName("gateway")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Gateway { get; init; }

    /// <summary>If false the route is not added to the routing table.</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>Optional text describing the route.</summary>
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Description { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#LanHost.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class LanHostPatch
{
    /// <summary>Host primary name (chosen from the list of available names, or manually set by user)</summary>
    [JsonPropertyName("primary_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> PrimaryName { get; init; }

    /// <summary>Host domain name on the local network (manually set by user, or automatically configured during device registration). The string must respect the following rules: Must end with ‘.home’ 63 characters long at max Only alphabetical characters are accepted Digits are accepted provided they are not placed at the beginning of the string, nor after another dot character. Hyphens and dots are accepted provided they are not placed at the beginning or the end of the string, nor after or before another dot character. It is also possible to use an empty string. This special value means no local domain should be registered for this host.</summary>
    [JsonPropertyName("domain_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> DomainName { get; init; }

    /// <summary>When possible, the Freebox will try to guess the host_type, but you can manually override this to the correct value Possible values are: source Description workstation Workstation laptop Laptop smartphone Smartphone tablet Tablet printer Printer vg_console Video game console television TV nas Nas ip_camera IP Camera ip_phone IP Phone freebox_player Freebox Player freebox_hd Freebox HD freebox_crystal Freebox Crystal freebox_mini Freebox Mini 4k freebox_delta Freebox Delta freebox_one Freebox One freebox_wifi Freebox Wi-Fi Pop freebox_pop Freebox Pop networking_device Networking device multimedia_device Multimedia device car Connected car watch Smartwatch light Light outlet Connected outlet appliances Household appliances thermostat Thermostat shutter Electric shutter other Other</summary>
    [JsonPropertyName("host_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> HostType { get; init; }

    /// <summary>If true the host is always shown even if it has not been active since the Freebox startup</summary>
    [JsonPropertyName("persistent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Persistent { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#post--api-v8-lan-wol-interface-.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class WakeOnLanRequest
{
    [JsonPropertyName("mac")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Mac { get; init; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Password { get; init; }

}
