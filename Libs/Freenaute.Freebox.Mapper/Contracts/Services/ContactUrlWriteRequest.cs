using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of ContactUrl; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactUrl" />
public sealed record ContactUrlWriteRequest
{
    [JsonPropertyName("contact_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> ContactId { get; init; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ContactUrlType>))]
    public Optional<ContactUrlType> Type { get; init; }

    [JsonPropertyName("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Url { get; init; }

    public void Validate()
    {
        if (ContactId.IsNull) throw new ArgumentException("Explicit null is not documented for ContactUrl.ContactId.", nameof(ContactId));
        if (Type.IsNull) throw new ArgumentException("Explicit null is not documented for ContactUrl.Type.", nameof(Type));
        if (Url.IsNull) throw new ArgumentException("Explicit null is not documented for ContactUrl.Url.", nameof(Url));
    }

    public override string ToString() => nameof(ContactUrlWriteRequest);
}
