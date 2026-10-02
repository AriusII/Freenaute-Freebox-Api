using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v8-rrd-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record FetchRrdRequest
{
    [JsonPropertyName("db")]
    public required RrdDatabase Db { get; init; }

    [JsonPropertyName("date_start")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> DateStart { get; init; }

    [JsonPropertyName("date_end")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> DateEnd { get; init; }

    [JsonPropertyName("precision")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Precision { get; init; }

    [JsonPropertyName("fields")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string[]>))]
    public Optional<string[]> Fields { get; init; }

}
