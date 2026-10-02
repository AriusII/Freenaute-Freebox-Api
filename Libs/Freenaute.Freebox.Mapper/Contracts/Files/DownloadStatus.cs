using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.status</summary>
[JsonConverter(typeof(JsonStringEnumConverter<DownloadStatus>))]
public enum DownloadStatus
{
    [JsonStringEnumMemberName("stopped")] Stopped,
    [JsonStringEnumMemberName("queued")] Queued,
    [JsonStringEnumMemberName("starting")] Starting,
    [JsonStringEnumMemberName("downloading")] Downloading,
    [JsonStringEnumMemberName("stopping")] Stopping,
    [JsonStringEnumMemberName("error")] Error,
    [JsonStringEnumMemberName("done")] Done,
    [JsonStringEnumMemberName("checking")] Checking,
    [JsonStringEnumMemberName("repairing")] Repairing,
    [JsonStringEnumMemberName("extracting")] Extracting,
    [JsonStringEnumMemberName("seeding")] Seeding,
    [JsonStringEnumMemberName("retry")] Retry,
}
