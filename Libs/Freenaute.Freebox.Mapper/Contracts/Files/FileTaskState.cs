using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.state</summary>
[JsonConverter(typeof(JsonStringEnumConverter<FileTaskState>))]
public enum FileTaskState
{
    [JsonStringEnumMemberName("queued")] Queued,
    [JsonStringEnumMemberName("running")] Running,
    [JsonStringEnumMemberName("paused")] Paused,
    [JsonStringEnumMemberName("done")] Done,
    [JsonStringEnumMemberName("failed")] Failed,
}
