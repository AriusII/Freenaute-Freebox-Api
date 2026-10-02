using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DownloadTracker. Unknown enum strings are preserved.</summary>
public sealed record DownloadTracker
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.announce</summary>
    [JsonPropertyName("announce")]
    public string? Announce { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.is_backup</summary>
    [JsonPropertyName("is_backup")]
    public bool? IsBackup { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.status</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.interval</summary>
    [JsonPropertyName("interval")]
    public long? Interval { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.min_interval</summary>
    [JsonPropertyName("min_interval")]
    public long? MinInterval { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.reannounce_in</summary>
    [JsonPropertyName("reannounce_in")]
    public long? ReannounceIn { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.nseeders</summary>
    [JsonPropertyName("nseeders")]
    public long? Nseeders { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.nleechers</summary>
    [JsonPropertyName("nleechers")]
    public long? Nleechers { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.is_enabled</summary>
    [JsonPropertyName("is_enabled")]
    public bool? IsEnabled { get; init; }

}
