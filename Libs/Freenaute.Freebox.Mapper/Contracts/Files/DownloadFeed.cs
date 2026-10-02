using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DownloadFeed. Unknown enum strings are preserved.</summary>
public sealed record DownloadFeed
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.status</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.url</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.title</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.desc</summary>
    [JsonPropertyName("desc")]
    public string? Desc { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.image_url</summary>
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.nb_read</summary>
    [JsonPropertyName("nb_read")]
    public long? NbRead { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.nb_unread</summary>
    [JsonPropertyName("nb_unread")]
    public long? NbUnread { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.auto_download</summary>
    [JsonPropertyName("auto_download")]
    public bool? AutoDownload { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.fetch_ts</summary>
    [JsonPropertyName("fetch_ts")]
    public long? FetchTs { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.pub_ts</summary>
    [JsonPropertyName("pub_ts")]
    public long? PubTs { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.error</summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }

}
