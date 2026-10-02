using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of SambaConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#SambaConfig" />
public sealed record SambaConfigPatch
{
    [JsonPropertyName("file_share_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> FileShareEnabled { get; init; }

    [JsonPropertyName("print_share_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> PrintShareEnabled { get; init; }

    [JsonPropertyName("logon_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> LogonEnabled { get; init; }

    [JsonPropertyName("logon_user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LogonUser { get; init; }

    [JsonPropertyName("logon_password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LogonPassword { get; init; }

    [JsonPropertyName("workgroup")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Workgroup { get; init; }

    [JsonPropertyName("smbv2_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Smbv2Enabled { get; init; }

    public void Validate()
    {
        if (FileShareEnabled.IsNull) throw new ArgumentException("Explicit null is not documented for SambaConfig.FileShareEnabled.", nameof(FileShareEnabled));
        if (PrintShareEnabled.IsNull) throw new ArgumentException("Explicit null is not documented for SambaConfig.PrintShareEnabled.", nameof(PrintShareEnabled));
        if (LogonEnabled.IsNull) throw new ArgumentException("Explicit null is not documented for SambaConfig.LogonEnabled.", nameof(LogonEnabled));
        if (LogonUser.IsNull) throw new ArgumentException("Explicit null is not documented for SambaConfig.LogonUser.", nameof(LogonUser));
        if (LogonPassword.IsNull) throw new ArgumentException("Explicit null is not documented for SambaConfig.LogonPassword.", nameof(LogonPassword));
        if (Workgroup.IsNull) throw new ArgumentException("Explicit null is not documented for SambaConfig.Workgroup.", nameof(Workgroup));
        if (Smbv2Enabled.IsNull) throw new ArgumentException("Explicit null is not documented for SambaConfig.Smbv2Enabled.", nameof(Smbv2Enabled));
    }

    public override string ToString() => nameof(SambaConfigPatch);
}
