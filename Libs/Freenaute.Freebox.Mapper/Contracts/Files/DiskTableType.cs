using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.table_type</summary>
[JsonConverter(typeof(JsonStringEnumConverter<DiskTableType>))]
public enum DiskTableType
{
    [JsonStringEnumMemberName("msdos")] Msdos,
    [JsonStringEnumMemberName("gpt")] Gpt,
    [JsonStringEnumMemberName("superfloppy")] Superfloppy,
    [JsonStringEnumMemberName("empty")] Empty,
}
