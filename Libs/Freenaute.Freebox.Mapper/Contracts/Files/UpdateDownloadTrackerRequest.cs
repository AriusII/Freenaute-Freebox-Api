using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-task_id-trackers-announce. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record UpdateDownloadTrackerRequest
{
    [JsonPropertyName("announce")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Announce { get; init; }

    [JsonPropertyName("is_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> IsEnabled { get; init; }

}
