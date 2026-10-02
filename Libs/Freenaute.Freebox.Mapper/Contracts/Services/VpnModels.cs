using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Read projection of the documented VPNServer wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNServer" />
public sealed record VpnServer
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("type")]
    public VpnServerType? Type { get; init; }

    [JsonPropertyName("state")]
    public VpnServerState? State { get; init; }

    [JsonPropertyName("connection_count")]
    public long? ConnectionCount { get; init; }

    [JsonPropertyName("auth_connection_count")]
    public long? AuthConnectionCount { get; init; }

    public override string ToString() => nameof(VpnServer);
}

/// <summary>Read projection of the documented VPNPPTPConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNPPTPConfig" />
public sealed record VpnPptpConfig
{
    [JsonPropertyName("mppe")]
    public VpnMppeMode? Mppe { get; init; }

    [JsonPropertyName("allowed_auth")]
    public VpnServerAuthentication? AllowedAuth { get; init; }

    public override string ToString() => nameof(VpnPptpConfig);
}

/// <summary>Read projection of the documented VPNOpenVpnConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNOpenVpnConfig" />
public sealed record VpnOpenVpnConfig
{
    [JsonPropertyName("cipher")]
    public VpnCipher? Cipher { get; init; }

    [JsonPropertyName("disable_fragment")]
    public bool? DisableFragment { get; init; }

    [JsonPropertyName("use_tcp")]
    public bool? UseTcp { get; init; }

    public override string ToString() => nameof(VpnOpenVpnConfig);
}

/// <summary>Read projection of the documented VPNWireGuardConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNWireGuardConfig" />
public sealed record VpnWireGuardConfig
{
    [JsonPropertyName("mtu")]
    public long? Mtu { get; init; }

    public override string ToString() => nameof(VpnWireGuardConfig);
}

/// <summary>Read projection of the documented VPNIPSecAuthMode wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNIPSecAuthMode" />
public sealed record VpnIpSecAuthMode
{
    [JsonPropertyName("id_source")]
    public VpnIpSecIdSource? IdSource { get; init; }

    [JsonPropertyName("id_custom")]
    public string? IdCustom { get; init; }

    public override string ToString() => nameof(VpnIpSecAuthMode);
}

/// <summary>Read projection of the documented VPNIPSecConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNIPSecConfig" />
public sealed record VpnIpSecConfig
{
    [JsonPropertyName("ike_version")]
    public long? IkeVersion { get; init; }

    [JsonPropertyName("auth_modes")]
    public VpnIpSecAuthModes? AuthModes { get; init; }

    public override string ToString() => nameof(VpnIpSecConfig);
}

/// <summary>Read projection of the documented VPNServerConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig" />
public sealed record VpnServerConfig
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("type")]
    public VpnServerType? Type { get; init; }

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("enable_ipv4")]
    public bool? EnableIpv4 { get; init; }

    [JsonPropertyName("enable_ipv6")]
    public bool? EnableIpv6 { get; init; }

    [JsonPropertyName("port")]
    public long? Port { get; init; }

    [JsonPropertyName("min_port")]
    public long? MinPort { get; init; }

    [JsonPropertyName("max_port")]
    public long? MaxPort { get; init; }

    [JsonPropertyName("port_ike")]
    public long? PortIke { get; init; }

    [JsonPropertyName("port_nat")]
    public long? PortNat { get; init; }

    [JsonPropertyName("conf_pptp")]
    public VpnPptpConfig? ConfPptp { get; init; }

    [JsonPropertyName("conf_openvpn")]
    public VpnOpenVpnConfig? ConfOpenVpn { get; init; }

    [JsonPropertyName("conf_ipsec")]
    public VpnIpSecConfig? ConfIpsec { get; init; }

    [JsonPropertyName("conf_wireguard")]
    public VpnWireGuardConfig? ConfWireGuard { get; init; }

    [JsonPropertyName("ip_start")]
    public string? IpStart { get; init; }

    [JsonPropertyName("ip_end")]
    public string? IpEnd { get; init; }

    [JsonPropertyName("ip6_start")]
    public string? Ip6Start { get; init; }

    [JsonPropertyName("ip6_end")]
    public string? Ip6End { get; init; }

    public override string ToString() => nameof(VpnServerConfig);
}

/// <summary>Read projection of the documented VPNUser wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNUser" />
public sealed record VpnUser
{
    [JsonPropertyName("login")]
    public string? Login { get; init; }

    [JsonPropertyName("type")]
    public VpnUserType? Type { get; init; }

    [JsonPropertyName("password_set")]
    public bool? PasswordSet { get; init; }

    [JsonPropertyName("ip_reservation")]
    public string? IpReservation { get; init; }

    [JsonPropertyName("conf_wireguard")]
    public VpnUserWireGuardConfig? ConfWireGuard { get; init; }

    public override string ToString() => nameof(VpnUser);
}

/// <summary>Read projection of the documented conf_wireguard wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNUser.conf_wireguard" />
public sealed record VpnUserWireGuardConfig
{
    [JsonPropertyName("keepalive")]
    public long? Keepalive { get; init; }

    [JsonPropertyName("psk")]
    public bool? Psk { get; init; }

    public override string ToString() => nameof(VpnUserWireGuardConfig);
}

/// <summary>Read projection of the documented VPNConnection wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNConnection" />
public sealed record VpnConnection
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("vpn")]
    public string? Vpn { get; init; }

    [JsonPropertyName("user")]
    public string? User { get; init; }

    [JsonPropertyName("authenticated")]
    public bool? Authenticated { get; init; }

    [JsonPropertyName("auth_time")]
    public long? AuthTime { get; init; }

    [JsonPropertyName("src_ip")]
    public string? SrcIp { get; init; }

    [JsonPropertyName("src_port")]
    public long? SrcPort { get; init; }

    [JsonPropertyName("local_ip")]
    public StringOrInteger? LocalIp { get; init; }

    [JsonPropertyName("rx_bytes")]
    public long? RxBytes { get; init; }

    [JsonPropertyName("tx_bytes")]
    public long? TxBytes { get; init; }

    public override string ToString() => nameof(VpnConnection);
}

/// <summary>Read projection of the documented VPNIpPool wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-" />
public sealed record VpnIpPool
{
    [JsonPropertyName("ip_start")]
    public string? IpStart { get; init; }

    [JsonPropertyName("ip_end")]
    public string? IpEnd { get; init; }

    [JsonPropertyName("reservations")]
    public VpnIpReservation[]? Reservations { get; init; }

    public override string ToString() => nameof(VpnIpPool);
}

/// <summary>Read projection of the documented VPNIpReservation wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-" />
public sealed record VpnIpReservation
{
    [JsonPropertyName("login")]
    public string? Login { get; init; }

    [JsonPropertyName("ip")]
    public string? Ip { get; init; }

    public override string ToString() => nameof(VpnIpReservation);
}

/// <summary>Read projection of the documented VpnServerAuthentication wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNPPTPConfig.allowed_auth" />
public sealed record VpnServerAuthentication
{
    [JsonPropertyName("pap")]
    public bool? Pap { get; init; }

    [JsonPropertyName("chap")]
    public bool? Chap { get; init; }

    [JsonPropertyName("mschapv2")]
    public bool? Mschapv2 { get; init; }

    public override string ToString() => nameof(VpnServerAuthentication);
}
