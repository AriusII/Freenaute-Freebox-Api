using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of ContactAddress; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactAddress" />
public sealed record ContactAddressWriteRequest
{
    [JsonPropertyName("contact_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> ContactId { get; init; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ContactAddressType>))]
    public Optional<ContactAddressType> Type { get; init; }

    [JsonPropertyName("number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Number { get; init; }

    [JsonPropertyName("street")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Street { get; init; }

    [JsonPropertyName("street2")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Street2 { get; init; }

    [JsonPropertyName("city")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> City { get; init; }

    [JsonPropertyName("zipcode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Zipcode { get; init; }

    [JsonPropertyName("country")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Country { get; init; }

    public void Validate()
    {
        if (ContactId.IsNull) throw new ArgumentException("Explicit null is not documented for ContactAddress.ContactId.", nameof(ContactId));
        if (Type.IsNull) throw new ArgumentException("Explicit null is not documented for ContactAddress.Type.", nameof(Type));
        if (Number.IsNull) throw new ArgumentException("Explicit null is not documented for ContactAddress.Number.", nameof(Number));
        if (Street.IsNull) throw new ArgumentException("Explicit null is not documented for ContactAddress.Street.", nameof(Street));
        if (Street2.IsNull) throw new ArgumentException("Explicit null is not documented for ContactAddress.Street2.", nameof(Street2));
        if (City.IsNull) throw new ArgumentException("Explicit null is not documented for ContactAddress.City.", nameof(City));
        if (Zipcode.IsNull) throw new ArgumentException("Explicit null is not documented for ContactAddress.Zipcode.", nameof(Zipcode));
        if (Country.IsNull) throw new ArgumentException("Explicit null is not documented for ContactAddress.Country.", nameof(Country));
    }

    public override string ToString() => nameof(ContactAddressWriteRequest);
}
