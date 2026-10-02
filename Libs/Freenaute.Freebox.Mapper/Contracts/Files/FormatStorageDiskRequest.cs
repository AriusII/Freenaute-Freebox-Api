using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-disk-id-format-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record FormatStorageDiskRequest
{
    [JsonPropertyName("table_type")]
    public required DiskTableType TableType { get; init; }

    [JsonPropertyName("fs_type")]
    public required PartitionFileSystem FsType { get; init; }

    [JsonPropertyName("label")]
    public required string Label { get; init; }

}
