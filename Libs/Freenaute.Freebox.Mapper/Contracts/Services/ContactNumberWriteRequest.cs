using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of ContactNumber; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactNumber" />
public sealed record ContactNumberWriteRequest
{
    [JsonPropertyName("contact_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> ContactId { get; init; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ContactNumberType>))]
    public Optional<ContactNumberType> Type { get; init; }

    [JsonPropertyName("number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Number { get; init; }

    [JsonPropertyName("is_default")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> IsDefault { get; init; }

    [JsonPropertyName("is_own")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> IsOwn { get; init; }

    public void Validate()
    {
        if (ContactId.IsNull) throw new ArgumentException("Explicit null is not documented for ContactNumber.ContactId.", nameof(ContactId));
        if (Type.IsNull) throw new ArgumentException("Explicit null is not documented for ContactNumber.Type.", nameof(Type));
        if (Number.IsNull) throw new ArgumentException("Explicit null is not documented for ContactNumber.Number.", nameof(Number));
        if (IsDefault.IsNull) throw new ArgumentException("Explicit null is not documented for ContactNumber.IsDefault.", nameof(IsDefault));
        if (IsOwn.IsNull) throw new ArgumentException("Explicit null is not documented for ContactNumber.IsOwn.", nameof(IsOwn));
    }

    public override string ToString() => nameof(ContactNumberWriteRequest);
}
