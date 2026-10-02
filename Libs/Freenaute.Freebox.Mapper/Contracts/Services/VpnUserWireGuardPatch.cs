using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of conf_wireguard; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNUser.conf_wireguard" />
public sealed record VpnUserWireGuardPatch
{
    [JsonPropertyName("keepalive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Keepalive { get; init; }

    [JsonPropertyName("psk")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Psk { get; init; }

    public void Validate()
    {
        if (Keepalive.IsNull) throw new ArgumentException("Explicit null is not documented for conf_wireguard.Keepalive.", nameof(Keepalive));
        if (Psk.IsNull) throw new ArgumentException("Explicit null is not documented for conf_wireguard.Psk.", nameof(Psk));
    }

    public override string ToString() => nameof(VpnUserWireGuardPatch);
}
