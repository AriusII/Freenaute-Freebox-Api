using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

public sealed record RaidMemberId([property: JsonPropertyName("id")] long Id);
public sealed record FileListing
{
    [JsonPropertyName("entries")] public FileEntry[]? Entries { get; init; }
    [JsonPropertyName("cursor")] public string? Cursor { get; init; }
}
public sealed record FileHash([property: JsonPropertyName("hash")] string? Hash);
public sealed record FileSystemAdvice
{
    [JsonPropertyName("fstype")] public string? FsType { get; init; }
    [JsonPropertyName("table_type")] public string? TableType { get; init; }
    [JsonPropertyName("reason")] public string? Reason { get; init; }
    [JsonPropertyName("partitions_to_delete")] public DiskPartition[]? PartitionsToDelete { get; init; }
}
public sealed record DownloadThrottlingStatus
{
    [JsonPropertyName("is_scheduled")] public bool? IsScheduled { get; init; }
    [JsonPropertyName("throttling")] public string? Throttling { get; init; }
}
/// <summary>Feed mutations use feed_id in the example, independently of id from read endpoints.</summary>
public sealed record DownloadFeedMutation
{
    [JsonPropertyName("feed_id")] public long? FeedId { get; init; }
    [JsonPropertyName("id")] public long? Id { get; init; }
    [JsonPropertyName("status")] public string? Status { get; init; }
    [JsonPropertyName("url")] public string? Url { get; init; }
    [JsonPropertyName("title")] public string? Title { get; init; }
    [JsonPropertyName("desc")] public string? Desc { get; init; }
    [JsonPropertyName("image_url")] public string? ImageUrl { get; init; }
    [JsonPropertyName("nb_read")] public long? NbRead { get; init; }
    [JsonPropertyName("nb_unread")] public long? NbUnread { get; init; }
    [JsonPropertyName("auto_download")] public bool? AutoDownload { get; init; }
    [JsonPropertyName("fetch_ts")] public long? FetchTs { get; init; }
    [JsonPropertyName("pub_ts")] public long? PubTs { get; init; }
    [JsonPropertyName("error")] public string? Error { get; init; }
}
public sealed record RrdResult
{
    [JsonPropertyName("date_start")] public long? DateStart { get; init; }
    [JsonPropertyName("date_end")] public long? DateEnd { get; init; }
    [JsonPropertyName("data")] public Dictionary<string, long?>[]? Data { get; init; }
}
