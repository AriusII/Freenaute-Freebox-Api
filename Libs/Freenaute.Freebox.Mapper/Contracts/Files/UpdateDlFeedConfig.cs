using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#DlFeedConfig. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record UpdateDlFeedConfig
{
    [JsonPropertyName("fetch_interval")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> FetchInterval { get; init; }

    [JsonPropertyName("max_items")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> MaxItems { get; init; }

}
