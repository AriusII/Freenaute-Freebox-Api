using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of AfpConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#AfpConfig" />
public sealed record AfpConfigPatch
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    [JsonPropertyName("guest_allow")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> GuestAllow { get; init; }

    [JsonPropertyName("server_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<AfpServerType>))]
    public Optional<AfpServerType> ServerType { get; init; }

    [JsonPropertyName("login_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LoginName { get; init; }

    [JsonPropertyName("login_password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LoginPassword { get; init; }

    public void Validate()
    {
        if (Enabled.IsNull) throw new ArgumentException("Explicit null is not documented for AfpConfig.Enabled.", nameof(Enabled));
        if (GuestAllow.IsNull) throw new ArgumentException("Explicit null is not documented for AfpConfig.GuestAllow.", nameof(GuestAllow));
        if (ServerType.IsNull) throw new ArgumentException("Explicit null is not documented for AfpConfig.ServerType.", nameof(ServerType));
        if (LoginName.IsNull) throw new ArgumentException("Explicit null is not documented for AfpConfig.LoginName.", nameof(LoginName));
        if (LoginPassword.IsNull) throw new ArgumentException("Explicit null is not documented for AfpConfig.LoginPassword.", nameof(LoginPassword));
    }

    public override string ToString() => nameof(AfpConfigPatch);
}
