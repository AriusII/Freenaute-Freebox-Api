using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.fstype</summary>
[JsonConverter(typeof(JsonStringEnumConverter<PartitionFileSystem>))]
public enum PartitionFileSystem
{
    [JsonStringEnumMemberName("empty")] Empty,
    [JsonStringEnumMemberName("unknown")] Unknown,
    [JsonStringEnumMemberName("xfs")] Xfs,
    [JsonStringEnumMemberName("ext4")] Ext4,
    [JsonStringEnumMemberName("vfat")] Vfat,
    [JsonStringEnumMemberName("ntfs")] Ntfs,
    [JsonStringEnumMemberName("hf")] Hf,
    [JsonStringEnumMemberName("hfsplus")] Hfsplus,
    [JsonStringEnumMemberName("swap")] Swap,
    [JsonStringEnumMemberName("exfat")] Exfat,
}
