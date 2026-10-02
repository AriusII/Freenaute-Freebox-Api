using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.priority</summary>
[JsonConverter(typeof(JsonStringEnumConverter<DownloadFilePriority>))]
public enum DownloadFilePriority
{
    [JsonStringEnumMemberName("no_dl")] NoDl,
    [JsonStringEnumMemberName("low")] Low,
    [JsonStringEnumMemberName("normal")] Normal,
    [JsonStringEnumMemberName("high")] High,
}
