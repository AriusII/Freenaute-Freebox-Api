using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of UPnPAVConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#UPnPAVConfig" />
public sealed record UPnpAvConfigPatch
{
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    public void Validate()
    {
        if (Enabled.IsNull) throw new ArgumentException("Explicit null is not documented for UPnPAVConfig.Enabled.", nameof(Enabled));
    }

    public override string ToString() => nameof(UPnpAvConfigPatch);
}
