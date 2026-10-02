using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VPNUser; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNUser" />
public sealed record VpnUserWriteRequest
{
    [JsonPropertyName("login")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Login { get; init; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnUserType>))]
    public Optional<VpnUserType> Type { get; init; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Password { get; init; }

    [JsonPropertyName("ip_reservation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> IpReservation { get; init; }

    [JsonPropertyName("conf_wireguard")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VpnUserWireGuardPatch>))]
    public Optional<VpnUserWireGuardPatch> ConfWireGuard { get; init; }

    public void Validate()
    {
        if (Login.IsNull) throw new ArgumentException("Explicit null is not documented for VPNUser.Login.", nameof(Login));
        if (Type.IsNull) throw new ArgumentException("Explicit null is not documented for VPNUser.Type.", nameof(Type));
        if (Password.IsNull) throw new ArgumentException("Explicit null is not documented for VPNUser.Password.", nameof(Password));
        if (IpReservation.IsNull) throw new ArgumentException("Explicit null is not documented for VPNUser.IpReservation.", nameof(IpReservation));
        if (ConfWireGuard.IsNull) throw new ArgumentException("Explicit null is not documented for VPNUser.ConfWireGuard.", nameof(ConfWireGuard));
        if (ConfWireGuard.HasValue) ConfWireGuard.Value.Validate();
        if (Password.HasValue && (Password.Value.Length < 8 || Password.Value.Length > 32)) throw new ArgumentException("VPN passwords must contain 8 to 32 characters.", nameof(Password));
        if (Type.HasValue && Type.Value == VpnUserType.WireGuard && (!IpReservation.HasValue || string.IsNullOrEmpty(IpReservation.Value))) throw new ArgumentException("A WireGuard user requires an IP reservation.", nameof(IpReservation));
    }

    public override string ToString() => nameof(VpnUserWriteRequest);
}
