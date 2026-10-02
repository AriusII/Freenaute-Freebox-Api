using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of CallEntry; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#CallEntry" />
public sealed record CallEntryPatch
{
    [JsonPropertyName("new")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> New { get; init; }

    public void Validate()
    {
        if (New.IsNull) throw new ArgumentException("Explicit null is not documented for CallEntry.New.", nameof(New));
    }

    public override string ToString() => nameof(CallEntryPatch);
}
