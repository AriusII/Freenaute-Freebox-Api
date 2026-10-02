using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of Frecord; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Frecord" />
public sealed record FinishedRecordingPatch
{
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Name { get; init; }

    [JsonPropertyName("subname")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Subname { get; init; }

    public void Validate()
    {
        if (Name.IsNull) throw new ArgumentException("Explicit null is not documented for Frecord.Name.", nameof(Name));
        if (Subname.IsNull) throw new ArgumentException("Explicit null is not documented for Frecord.Subname.", nameof(Subname));
    }

    public override string ToString() => nameof(FinishedRecordingPatch);
}
