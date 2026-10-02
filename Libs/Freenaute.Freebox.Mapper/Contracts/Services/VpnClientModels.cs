using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Read projection of the documented VPNClientConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig" />
public sealed record VpnClientConfig
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("type")]
    public VpnClientType? Type { get; init; }

    [JsonPropertyName("active")]
    public bool? Active { get; init; }

    [JsonPropertyName("conf_pptp")]
    public VpnClientPptpConfig? ConfPptp { get; init; }

    [JsonPropertyName("conf_wireguard")]
    public VpnClientWireGuardConfig? ConfWireGuard { get; init; }

    public override string ToString() => nameof(VpnClientConfig);
}

/// <summary>Read projection of the documented VPNClientConfigPPTP wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP" />
public sealed record VpnClientPptpConfig
{
    [JsonPropertyName("remote_host")]
    public string? RemoteHost { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    [JsonPropertyName("mppe")]
    public VpnMppeMode? Mppe { get; init; }

    [JsonPropertyName("allowed_auth")]
    public VpnClientAuthentication? AllowedAuth { get; init; }

    public override string ToString() => nameof(VpnClientPptpConfig);
}

/// <summary>Read projection of the documented VPNClientConfigWireGuard wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard" />
public sealed record VpnClientWireGuardConfig
{
    [JsonPropertyName("remote_addr")]
    public string? RemoteAddr { get; init; }

    [JsonPropertyName("remote_port")]
    public long? RemotePort { get; init; }

    [JsonPropertyName("remote_public_key")]
    public string? RemotePublicKey { get; init; }

    [JsonPropertyName("remote_preshared_key")]
    public string? RemotePresharedKey { get; init; }

    [JsonPropertyName("local_priv_key")]
    public string? LocalPrivKey { get; init; }

    [JsonPropertyName("local_addr")]
    public VpnClientWireGuardAddress[]? LocalAddr { get; init; }

    [JsonPropertyName("dns")]
    public string[]? Dns { get; init; }

    [JsonPropertyName("mtu")]
    public long? Mtu { get; init; }

    public override string ToString() => nameof(VpnClientWireGuardConfig);
}

/// <summary>Read projection of the documented VPNClientConfigWireGuardIP wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuardIP" />
public sealed record VpnClientWireGuardAddress
{
    [JsonPropertyName("ip")]
    public string? Ip { get; init; }

    [JsonPropertyName("len")]
    public long? Len { get; init; }

    public override string ToString() => nameof(VpnClientWireGuardAddress);
}

/// <summary>Read projection of the documented VPNClientStatus wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus" />
public sealed record VpnClientStatus
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("active_vpn")]
    public string? ActiveVpn { get; init; }

    [JsonPropertyName("active_vpn_description")]
    public string? ActiveVpnDescription { get; init; }

    [JsonPropertyName("type")]
    public VpnClientType? Type { get; init; }

    [JsonPropertyName("state")]
    public VpnClientState? State { get; init; }

    [JsonPropertyName("last_up")]
    public long? LastUp { get; init; }

    [JsonPropertyName("last_try")]
    public long? LastTry { get; init; }

    [JsonPropertyName("next_try")]
    public long? NextTry { get; init; }

    [JsonPropertyName("last_error")]
    public VpnClientError? LastError { get; init; }

    [JsonPropertyName("stats")]
    public VpnClientStats? Stats { get; init; }

    [JsonPropertyName("IPv4")]
    public VpnClientIpInfo? IPv4Declared { get; init; }

    [JsonPropertyName("ipv4")]
    public VpnClientIpInfo? Ipv4Example { get; init; }

    [JsonIgnore]
    public VpnClientIpInfo? IpV4 => Ipv4Example ?? IPv4Declared;

    public override string ToString() => nameof(VpnClientStatus);
}

/// <summary>Read projection of the documented VpnClientStats wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VpnClientStats" />
public sealed record VpnClientStats
{
    [JsonPropertyName("rate_up")]
    public long? RateUp { get; init; }

    [JsonPropertyName("rate_down")]
    public long? RateDown { get; init; }

    [JsonPropertyName("bytes_up")]
    public long? BytesUp { get; init; }

    [JsonPropertyName("bytes_down")]
    public long? BytesDown { get; init; }

    public override string ToString() => nameof(VpnClientStats);
}

/// <summary>Read projection of the documented VpnClientIpInfo wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo" />
public sealed record VpnClientIpInfo
{
    [JsonPropertyName("config_valid")]
    public bool? ConfigValid { get; init; }

    [JsonPropertyName("ip_mask")]
    public VpnClientIpMask? IpMask { get; init; }

    [JsonPropertyName("domain")]
    public string? Domain { get; init; }

    [JsonPropertyName("gateway")]
    public string? Gateway { get; init; }

    [JsonPropertyName("dns")]
    public string[]? Dns { get; init; }

    [JsonPropertyName("provider")]
    public VpnIpProvider? Provider { get; init; }

    [JsonPropertyName("dhcp")]
    public VpnClientDhcpState? Dhcp { get; init; }

    public override string ToString() => nameof(VpnClientIpInfo);
}

/// <summary>Read projection of the documented VpnClientAuthentication wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP.allowed_auth" />
public sealed record VpnClientAuthentication
{
    [JsonPropertyName("eap")]
    public bool? Eap { get; init; }

    [JsonPropertyName("pap")]
    public bool? Pap { get; init; }

    [JsonPropertyName("chap")]
    public bool? Chap { get; init; }

    [JsonPropertyName("mschap")]
    public bool? Mschap { get; init; }

    [JsonPropertyName("mschapv2")]
    public bool? Mschapv2 { get; init; }

    public override string ToString() => nameof(VpnClientAuthentication);
}

/// <summary>Read projection of the documented VpnClientIpMask wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-status" />
public sealed record VpnClientIpMask
{
    [JsonPropertyName("ip")]
    public string? Ip { get; init; }

    [JsonPropertyName("mask")]
    public string? Mask { get; init; }

    public override string ToString() => nameof(VpnClientIpMask);
}

/// <summary>Read projection of the documented VpnClientDhcpState wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-status" />
public sealed record VpnClientDhcpState
{
    [JsonPropertyName("state")]
    public string? State { get; init; }

    [JsonPropertyName("renew_remaining")]
    public long? RenewRemaining { get; init; }

    [JsonPropertyName("lease_remaining")]
    public long? LeaseRemaining { get; init; }

    [JsonPropertyName("lease_time")]
    public long? LeaseTime { get; init; }

    [JsonPropertyName("rebind_remaining")]
    public long? RebindRemaining { get; init; }

    [JsonPropertyName("server_id")]
    public long? ServerId { get; init; }

    public override string ToString() => nameof(VpnClientDhcpState);
}
