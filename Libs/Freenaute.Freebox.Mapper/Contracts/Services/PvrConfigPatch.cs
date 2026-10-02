using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of PvrConfig; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#PvrConfig" />
public sealed record PvrConfigPatch
{
    [JsonPropertyName("margin_before")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> MarginBefore { get; init; }

    [JsonPropertyName("margin_after")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> MarginAfter { get; init; }

    public void Validate()
    {
        if (MarginBefore.IsNull) throw new ArgumentException("Explicit null is not documented for PvrConfig.MarginBefore.", nameof(MarginBefore));
        if (MarginAfter.IsNull) throw new ArgumentException("Explicit null is not documented for PvrConfig.MarginAfter.", nameof(MarginAfter));
    }

    public override string ToString() => nameof(PvrConfigPatch);
}
