using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of FtpConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#FtpConfig" />
public sealed record FtpConfigPatch
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    [JsonPropertyName("allow_anonymous")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> AllowAnonymous { get; init; }

    [JsonPropertyName("allow_anonymous_write")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> AllowAnonymousWrite { get; init; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Password { get; init; }

    [JsonPropertyName("allow_remote_access")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> AllowRemoteAccess { get; init; }

    [JsonPropertyName("port_ctrl")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> PortCtrl { get; init; }

    [JsonPropertyName("port_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> PortData { get; init; }

    [JsonPropertyName("remote_domain")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> RemoteDomain { get; init; }

    public void Validate()
    {
        if (Enabled.IsNull) throw new ArgumentException("Explicit null is not documented for FtpConfig.Enabled.", nameof(Enabled));
        if (AllowAnonymous.IsNull) throw new ArgumentException("Explicit null is not documented for FtpConfig.AllowAnonymous.", nameof(AllowAnonymous));
        if (AllowAnonymousWrite.IsNull) throw new ArgumentException("Explicit null is not documented for FtpConfig.AllowAnonymousWrite.", nameof(AllowAnonymousWrite));
        if (Password.IsNull) throw new ArgumentException("Explicit null is not documented for FtpConfig.Password.", nameof(Password));
        if (AllowRemoteAccess.IsNull) throw new ArgumentException("Explicit null is not documented for FtpConfig.AllowRemoteAccess.", nameof(AllowRemoteAccess));
        if (PortCtrl.IsNull) throw new ArgumentException("Explicit null is not documented for FtpConfig.PortCtrl.", nameof(PortCtrl));
        if (PortData.IsNull) throw new ArgumentException("Explicit null is not documented for FtpConfig.PortData.", nameof(PortData));
        if (RemoteDomain.IsNull) throw new ArgumentException("Explicit null is not documented for FtpConfig.RemoteDomain.", nameof(RemoteDomain));
    }

    public override string ToString() => nameof(FtpConfigPatch);
}
