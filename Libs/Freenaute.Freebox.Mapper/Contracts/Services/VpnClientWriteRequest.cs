using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VPNClientConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig" />
public sealed record VpnClientWriteRequest
{
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Description { get; init; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnClientType>))]
    public Optional<VpnClientType> Type { get; init; }

    [JsonPropertyName("active")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Active { get; init; }

    [JsonPropertyName("conf_pptp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnClientPptpPatch>))]
    public Optional<VpnClientPptpPatch> ConfPptp { get; init; }

    [JsonPropertyName("conf_wireguard")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnClientWireGuardPatch>))]
    public Optional<VpnClientWireGuardPatch> ConfWireGuard { get; init; }

    public void Validate()
    {
        if (Description.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfig.Description.", nameof(Description));
        if (Type.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfig.Type.", nameof(Type));
        if (Active.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfig.Active.", nameof(Active));
        if (ConfPptp.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfig.ConfPptp.", nameof(ConfPptp));
        if (ConfPptp.HasValue) ConfPptp.Value.Validate();
        if (ConfWireGuard.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfig.ConfWireGuard.", nameof(ConfWireGuard));
        if (ConfWireGuard.HasValue) ConfWireGuard.Value.Validate();
    }

    public override string ToString() => nameof(VpnClientWriteRequest);
}
