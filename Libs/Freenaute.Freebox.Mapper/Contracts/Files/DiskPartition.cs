using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DiskPartition. Unknown enum strings are preserved.</summary>
public sealed record DiskPartition
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.disk_id</summary>
    [JsonPropertyName("disk_id")]
    public long? DiskId { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.state</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.fstype</summary>
    [JsonPropertyName("fstype")]
    public string? Fstype { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.label</summary>
    [JsonPropertyName("label")]
    public string? Label { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.path</summary>
    [JsonPropertyName("path")]
    public EncodedFreeboxPath? Path { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.total_bytes</summary>
    [JsonPropertyName("total_bytes")]
    public long? TotalBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.used_bytes</summary>
    [JsonPropertyName("used_bytes")]
    public long? UsedBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.free_bytes</summary>
    [JsonPropertyName("free_bytes")]
    public long? FreeBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.fsck_result</summary>
    [JsonPropertyName("fsck_result")]
    public string? FsckResult { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DiskPartition.operation_pct</summary>
    [JsonPropertyName("operation_pct")]
    public OperationProgress? OperationPct { get; init; }

}
