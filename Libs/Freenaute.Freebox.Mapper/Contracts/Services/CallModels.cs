using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Read projection of the documented CallEntry wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#CallEntry" />
public sealed record CallEntry
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("type")]
    public CallType? Type { get; init; }

    [JsonPropertyName("datetime")]
    public long? Datetime { get; init; }

    [JsonPropertyName("number")]
    public string? Number { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("duration")]
    public long? Duration { get; init; }

    [JsonPropertyName("new")]
    public bool? New { get; init; }

    [JsonPropertyName("contact_id")]
    public long? ContactId { get; init; }

    [JsonPropertyName("line_id")]
    public long? LineId { get; init; }

    public override string ToString() => nameof(CallEntry);
}

/// <summary>Read projection of the documented VoicemailEntry wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry" />
public sealed record VoicemailEntry
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("country_code")]
    public StringOrInteger? CountryCode { get; init; }

    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; init; }

    [JsonPropertyName("date")]
    public long? Date { get; init; }

    [JsonPropertyName("read")]
    public bool? Read { get; init; }

    [JsonPropertyName("duration")]
    public long? Duration { get; init; }

    public override string ToString() => nameof(VoicemailEntry);
}

/// <summary>Read projection of the documented CallAccount wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-account" />
public sealed record CallAccount
{
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; init; }

    public override string ToString() => nameof(CallAccount);
}
