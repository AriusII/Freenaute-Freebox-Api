using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VPNClientConfigPPTP; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP" />
public sealed record VpnClientPptpPatch
{
    [JsonPropertyName("remote_host")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> RemoteHost { get; init; }

    [JsonPropertyName("username")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Username { get; init; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Password { get; init; }

    [JsonPropertyName("mppe")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnMppeMode>))]
    public Optional<VpnMppeMode> Mppe { get; init; }

    [JsonPropertyName("allowed_auth")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnClientAuthenticationPatch>))]
    public Optional<VpnClientAuthenticationPatch> AllowedAuth { get; init; }

    public void Validate()
    {
        if (RemoteHost.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigPPTP.RemoteHost.", nameof(RemoteHost));
        if (Username.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigPPTP.Username.", nameof(Username));
        if (Password.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigPPTP.Password.", nameof(Password));
        if (Mppe.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigPPTP.Mppe.", nameof(Mppe));
        if (AllowedAuth.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigPPTP.AllowedAuth.", nameof(AllowedAuth));
        if (AllowedAuth.HasValue) AllowedAuth.Value.Validate();
    }

    public override string ToString() => nameof(VpnClientPptpPatch);
}
