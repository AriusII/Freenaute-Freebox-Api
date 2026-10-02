using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VpnClientAuthentication; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP.allowed_auth" />
public sealed record VpnClientAuthenticationPatch
{
    [JsonPropertyName("eap")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Eap { get; init; }

    [JsonPropertyName("pap")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Pap { get; init; }

    [JsonPropertyName("chap")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Chap { get; init; }

    [JsonPropertyName("mschap")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Mschap { get; init; }

    [JsonPropertyName("mschapv2")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Mschapv2 { get; init; }

    public void Validate()
    {
        if (Eap.IsNull) throw new ArgumentException("Explicit null is not documented for VpnClientAuthentication.Eap.", nameof(Eap));
        if (Pap.IsNull) throw new ArgumentException("Explicit null is not documented for VpnClientAuthentication.Pap.", nameof(Pap));
        if (Chap.IsNull) throw new ArgumentException("Explicit null is not documented for VpnClientAuthentication.Chap.", nameof(Chap));
        if (Mschap.IsNull) throw new ArgumentException("Explicit null is not documented for VpnClientAuthentication.Mschap.", nameof(Mschap));
        if (Mschapv2.IsNull) throw new ArgumentException("Explicit null is not documented for VpnClientAuthentication.Mschapv2.", nameof(Mschapv2));
    }

    public override string ToString() => nameof(VpnClientAuthenticationPatch);
}
