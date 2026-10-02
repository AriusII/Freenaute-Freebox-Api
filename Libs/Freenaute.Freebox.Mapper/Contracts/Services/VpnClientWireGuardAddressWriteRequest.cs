using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VPNClientConfigWireGuardIP; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuardIP" />
public sealed record VpnClientWireGuardAddressWriteRequest
{
    [JsonPropertyName("ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Ip { get; init; }

    [JsonPropertyName("len")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Len { get; init; }

    public void Validate()
    {
        if (Ip.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuardIP.Ip.", nameof(Ip));
        if (Len.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuardIP.Len.", nameof(Len));
    }

    public override string ToString() => nameof(VpnClientWireGuardAddressWriteRequest);
}
