using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.state</summary>
[JsonConverter(typeof(JsonStringEnumConverter<DiskPartitionState>))]
public enum DiskPartitionState
{
    [JsonStringEnumMemberName("error")] Error,
    [JsonStringEnumMemberName("checking")] Checking,
    [JsonStringEnumMemberName("formatting")] Formatting,
    [JsonStringEnumMemberName("mounting")] Mounting,
    [JsonStringEnumMemberName("maintenance")] Maintenance,
    [JsonStringEnumMemberName("mounted")] Mounted,
    [JsonStringEnumMemberName("umounting")] Umounting,
    [JsonStringEnumMemberName("umounted")] Umounted,
    [JsonStringEnumMemberName("ejecting")] Ejecting,
}
