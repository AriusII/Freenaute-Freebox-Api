using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of TftpConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#TftpConfig" />
public sealed record TftpConfigPatch
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    [JsonPropertyName("root")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Root { get; init; }

    public void Validate()
    {
        if (Enabled.IsNull) throw new ArgumentException("Explicit null is not documented for TftpConfig.Enabled.", nameof(Enabled));
        if (Root.IsNull) throw new ArgumentException("Explicit null is not documented for TftpConfig.Root.", nameof(Root));
    }

    public override string ToString() => nameof(TftpConfigPatch);
}
