using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VPNOpenVpnConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNOpenVpnConfig" />
public sealed record VpnOpenVpnConfigPatch
{
    [JsonPropertyName("cipher")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnCipher>))]
    public Optional<VpnCipher> Cipher { get; init; }

    [JsonPropertyName("disable_fragment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> DisableFragment { get; init; }

    [JsonPropertyName("use_tcp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> UseTcp { get; init; }

    public void Validate()
    {
        if (Cipher.IsNull) throw new ArgumentException("Explicit null is not documented for VPNOpenVpnConfig.Cipher.", nameof(Cipher));
        if (DisableFragment.IsNull) throw new ArgumentException("Explicit null is not documented for VPNOpenVpnConfig.DisableFragment.", nameof(DisableFragment));
        if (UseTcp.IsNull) throw new ArgumentException("Explicit null is not documented for VPNOpenVpnConfig.UseTcp.", nameof(UseTcp));
    }

    public override string ToString() => nameof(VpnOpenVpnConfigPatch);
}
