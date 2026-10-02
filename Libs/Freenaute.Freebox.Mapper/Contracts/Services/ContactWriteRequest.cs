using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of ContactEntry; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#ContactEntry" />
public sealed record ContactWriteRequest
{
    [JsonPropertyName("display_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> DisplayName { get; init; }

    [JsonPropertyName("first_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> FirstName { get; init; }

    [JsonPropertyName("last_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LastName { get; init; }

    [JsonPropertyName("company")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Company { get; init; }

    [JsonPropertyName("photo_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> PhotoUrl { get; init; }

    [JsonPropertyName("notes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Notes { get; init; }

    public void Validate()
    {
        if (DisplayName.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEntry.DisplayName.", nameof(DisplayName));
        if (FirstName.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEntry.FirstName.", nameof(FirstName));
        if (LastName.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEntry.LastName.", nameof(LastName));
        if (Company.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEntry.Company.", nameof(Company));
        if (PhotoUrl.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEntry.PhotoUrl.", nameof(PhotoUrl));
        if (Notes.IsNull) throw new ArgumentException("Explicit null is not documented for ContactEntry.Notes.", nameof(Notes));
    }

    public override string ToString() => nameof(ContactWriteRequest);
}
