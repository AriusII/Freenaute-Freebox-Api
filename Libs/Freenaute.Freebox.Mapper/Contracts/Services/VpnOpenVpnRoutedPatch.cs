using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VPNServerConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig" />
public sealed record VpnOpenVpnRoutedPatch
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    [JsonPropertyName("enable_ipv4")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> EnableIpv4 { get; init; }

    [JsonPropertyName("enable_ipv6")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> EnableIpv6 { get; init; }

    [JsonPropertyName("port")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Port { get; init; }

    [JsonPropertyName("conf_openvpn")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnOpenVpnConfigPatch>))]
    public Optional<VpnOpenVpnConfigPatch> ConfOpenVpn { get; init; }

    public void Validate()
    {
        if (Enabled.IsNull) throw new ArgumentException("Explicit null is not documented for VPNServerConfig.Enabled.", nameof(Enabled));
        if (EnableIpv4.IsNull) throw new ArgumentException("Explicit null is not documented for VPNServerConfig.EnableIpv4.", nameof(EnableIpv4));
        if (EnableIpv6.IsNull) throw new ArgumentException("Explicit null is not documented for VPNServerConfig.EnableIpv6.", nameof(EnableIpv6));
        if (Port.IsNull) throw new ArgumentException("Explicit null is not documented for VPNServerConfig.Port.", nameof(Port));
        if (ConfOpenVpn.IsNull) throw new ArgumentException("Explicit null is not documented for VPNServerConfig.ConfOpenVpn.", nameof(ConfOpenVpn));
        if (ConfOpenVpn.HasValue) ConfOpenVpn.Value.Validate();
    }

    public override string ToString() => nameof(VpnOpenVpnRoutedPatch);
}
