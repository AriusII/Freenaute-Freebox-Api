using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of ContactEmail; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactEmail" />
public sealed record ContactEmailWriteRequest
{
    [JsonPropertyName("contact_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> ContactId { get; init; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ContactEmailType>))]
    public Optional<ContactEmailType> Type { get; init; }

    [JsonPropertyName("email")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Email { get; init; }

    public void Validate()
    {
        if (ContactId.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEmail.ContactId.", nameof(ContactId));
        if (Type.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEmail.Type.", nameof(Type));
        if (Email.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEmail.Email.", nameof(Email));
    }

    public override string ToString() => nameof(ContactEmailWriteRequest);
}
