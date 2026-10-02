using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#StorageDisk. Unknown enum strings are preserved.</summary>
public sealed record StorageDisk
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.type</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.state</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.connector</summary>
    [JsonPropertyName("connector")]
    public long? Connector { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.total_bytes</summary>
    [JsonPropertyName("total_bytes")]
    public long? TotalBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.table_type</summary>
    [JsonPropertyName("table_type")]
    public StringOrInteger? TableType { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.model</summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.serial</summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.firmware</summary>
    [JsonPropertyName("firmware")]
    public string? Firmware { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.temp</summary>
    [JsonPropertyName("temp")]
    public long? Temp { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.operation_pct</summary>
    [JsonPropertyName("operation_pct")]
    public OperationProgress? OperationPct { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.partitions</summary>
    [JsonPropertyName("partitions")]
    public DiskPartition[]? Partitions { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.idle</summary>
    [JsonPropertyName("idle")]
    public bool? Idle { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.idle_duration</summary>
    [JsonPropertyName("idle_duration")]
    public long? IdleDuration { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.spinning</summary>
    [JsonPropertyName("spinning")]
    public bool? Spinning { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.active_duration</summary>
    [JsonPropertyName("active_duration")]
    public long? ActiveDuration { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.time_before_spindown</summary>
    [JsonPropertyName("time_before_spindown")]
    public long? TimeBeforeSpindown { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.read_requests</summary>
    [JsonPropertyName("read_requests")]
    public long? ReadRequests { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.read_error_requests</summary>
    [JsonPropertyName("read_error_requests")]
    public long? ReadErrorRequests { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.write_requests</summary>
    [JsonPropertyName("write_requests")]
    public long? WriteRequests { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.write_error_requests</summary>
    [JsonPropertyName("write_error_requests")]
    public long? WriteErrorRequests { get; init; }

}
