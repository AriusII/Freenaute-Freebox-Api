using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of VoicemailEntry; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry" />
public sealed record VoicemailPatch
{
    [JsonPropertyName("read")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Read { get; init; }

    public void Validate()
    {
        if (Read.IsNull) throw new ArgumentException("Explicit null is not documented for VoicemailEntry.Read.", nameof(Read));
    }

    public override string ToString() => nameof(VoicemailPatch);
}
