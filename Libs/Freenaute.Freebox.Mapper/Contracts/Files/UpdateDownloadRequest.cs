using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-id. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record UpdateDownloadRequest
{
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<DownloadStatus>))]
    public Optional<DownloadStatus> Status { get; init; }

    [JsonPropertyName("queue_pos")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> QueuePos { get; init; }

    [JsonPropertyName("io_priority")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<DownloadIoPriority>))]
    public Optional<DownloadIoPriority> IoPriority { get; init; }

    [JsonPropertyName("archive_password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> ArchivePassword { get; init; }

}
