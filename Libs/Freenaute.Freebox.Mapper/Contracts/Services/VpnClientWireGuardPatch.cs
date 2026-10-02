using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VPNClientConfigWireGuard; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard" />
public sealed record VpnClientWireGuardPatch
{
    [JsonPropertyName("remote_addr")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> RemoteAddr { get; init; }

    [JsonPropertyName("remote_port")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> RemotePort { get; init; }

    [JsonPropertyName("remote_public_key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> RemotePublicKey { get; init; }

    [JsonPropertyName("remote_preshared_key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> RemotePresharedKey { get; init; }

    [JsonPropertyName("local_priv_key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LocalPrivKey { get; init; }

    [JsonPropertyName("local_addr")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnClientWireGuardAddressWriteRequest[]>))]
    public Optional<VpnClientWireGuardAddressWriteRequest[]> LocalAddr { get; init; }

    [JsonPropertyName("dns")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string[]>))]
    public Optional<string[]> Dns { get; init; }

    public void Validate()
    {
        if (RemoteAddr.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuard.RemoteAddr.", nameof(RemoteAddr));
        if (RemotePort.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuard.RemotePort.", nameof(RemotePort));
        if (RemotePublicKey.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuard.RemotePublicKey.", nameof(RemotePublicKey));
        if (RemotePresharedKey.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuard.RemotePresharedKey.", nameof(RemotePresharedKey));
        if (LocalPrivKey.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuard.LocalPrivKey.", nameof(LocalPrivKey));
        if (LocalAddr.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuard.LocalAddr.", nameof(LocalAddr));
        if (LocalAddr.HasValue) foreach (var item in LocalAddr.Value) { ArgumentNullException.ThrowIfNull(item); item.Validate(); }
        if (Dns.IsNull) throw new ArgumentException("Explicit null is not documented for VPNClientConfigWireGuard.Dns.", nameof(Dns));
    }

    public override string ToString() => nameof(VpnClientWireGuardPatch);
}
