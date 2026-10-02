using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry. Unknown enum strings are preserved.</summary>
public sealed record DownloadBlacklistEntry
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.host</summary>
    [JsonPropertyName("host")]
    public string? Host { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.reason</summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.expire</summary>
    [JsonPropertyName("expire")]
    public long? Expire { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.global</summary>
    [JsonPropertyName("global")]
    public bool? Global { get; init; }

}
