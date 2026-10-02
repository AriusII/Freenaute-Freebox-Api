using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#Download. Unknown enum strings are preserved.</summary>
public sealed record Download
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.type</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.status</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.size</summary>
    [JsonPropertyName("size")]
    public long? Size { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.queue_pos</summary>
    [JsonPropertyName("queue_pos")]
    public long? QueuePos { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.io_priority</summary>
    [JsonPropertyName("io_priority")]
    public string? IoPriority { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.tx_bytes</summary>
    [JsonPropertyName("tx_bytes")]
    public long? TxBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.rx_bytes</summary>
    [JsonPropertyName("rx_bytes")]
    public long? RxBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.tx_rate</summary>
    [JsonPropertyName("tx_rate")]
    public long? TxRate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.rx_rate</summary>
    [JsonPropertyName("rx_rate")]
    public long? RxRate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.tx_pct</summary>
    [JsonPropertyName("tx_pct")]
    public long? TxPct { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.rx_pct</summary>
    [JsonPropertyName("rx_pct")]
    public long? RxPct { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.error</summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.created_ts</summary>
    [JsonPropertyName("created_ts")]
    public long? CreatedTs { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.eta</summary>
    [JsonPropertyName("eta")]
    public long? Eta { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.download_dir</summary>
    [JsonPropertyName("download_dir")]
    public EncodedFreeboxPath? DownloadDir { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.stop_ratio</summary>
    [JsonPropertyName("stop_ratio")]
    public long? StopRatio { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.archive_password</summary>
    [JsonPropertyName("archive_password")]
    public string? ArchivePassword { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.info_hash</summary>
    [JsonPropertyName("info_hash")]
    public string? InfoHash { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#Download.piece_length</summary>
    [JsonPropertyName("piece_length")]
    public long? PieceLength { get; init; }

}
