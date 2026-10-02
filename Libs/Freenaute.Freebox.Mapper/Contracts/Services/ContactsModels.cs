using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Read projection of the documented ContactEntry wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactEntry" />
public sealed record ContactEntry
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("display_name")]
    public string? DisplayName { get; init; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; init; }

    [JsonPropertyName("company")]
    public string? Company { get; init; }

    [JsonPropertyName("photo_url")]
    public string? PhotoUrl { get; init; }

    [JsonPropertyName("last_update")]
    public long? LastUpdate { get; init; }

    [JsonPropertyName("notes")]
    public string? Notes { get; init; }

    [JsonPropertyName("addresses")]
    public ContactAddress[]? Addresses { get; init; }

    [JsonPropertyName("emails")]
    public ContactEmail[]? Emails { get; init; }

    [JsonPropertyName("numbers")]
    public ContactNumber[]? Numbers { get; init; }

    [JsonPropertyName("urls")]
    public ContactUrl[]? Urls { get; init; }

    [JsonPropertyName("birthday")]
    public string? Birthday { get; init; }

    public override string ToString() => nameof(ContactEntry);
}

/// <summary>Read projection of the documented ContactNumber wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactNumber" />
public sealed record ContactNumber
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("contact_id")]
    public long? ContactId { get; init; }

    [JsonPropertyName("type")]
    public ContactNumberType? Type { get; init; }

    [JsonPropertyName("number")]
    public string? Number { get; init; }

    [JsonPropertyName("is_default")]
    public bool? IsDefault { get; init; }

    [JsonPropertyName("is_own")]
    public bool? IsOwn { get; init; }

    public override string ToString() => nameof(ContactNumber);
}

/// <summary>Read projection of the documented ContactAddress wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactAddress" />
public sealed record ContactAddress
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("contact_id")]
    public long? ContactId { get; init; }

    [JsonPropertyName("type")]
    public ContactAddressType? Type { get; init; }

    [JsonPropertyName("number")]
    public string? Number { get; init; }

    [JsonPropertyName("street")]
    public string? Street { get; init; }

    [JsonPropertyName("street2")]
    public string? Street2 { get; init; }

    [JsonPropertyName("city")]
    public string? City { get; init; }

    [JsonPropertyName("zipcode")]
    public string? Zipcode { get; init; }

    [JsonPropertyName("country")]
    public string? Country { get; init; }

    public override string ToString() => nameof(ContactAddress);
}

/// <summary>Read projection of the documented ContactUrl wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactUrl" />
public sealed record ContactUrl
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("contact_id")]
    public long? ContactId { get; init; }

    [JsonPropertyName("type")]
    public ContactUrlType? Type { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    public override string ToString() => nameof(ContactUrl);
}

/// <summary>Read projection of the documented ContactEmail wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactEmail" />
public sealed record ContactEmail
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("contact_id")]
    public long? ContactId { get; init; }

    [JsonPropertyName("type")]
    public ContactEmailType? Type { get; init; }

    [JsonPropertyName("email")]
    public string? Email { get; init; }

    public override string ToString() => nameof(ContactEmail);
}
