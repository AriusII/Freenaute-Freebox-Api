using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem. Unknown enum strings are preserved.</summary>
public sealed record DownloadFeedItem
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.feed_id</summary>
    [JsonPropertyName("feed_id")]
    public long? FeedId { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.title</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.desc</summary>
    [JsonPropertyName("desc")]
    public string? Desc { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.author</summary>
    [JsonPropertyName("author")]
    public string? Author { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.link</summary>
    [JsonPropertyName("link")]
    public string? Link { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.is_read</summary>
    [JsonPropertyName("is_read")]
    public bool? IsRead { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.is_downloaded</summary>
    [JsonPropertyName("is_downloaded")]
    public bool? IsDownloaded { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.fetch_ts</summary>
    [JsonPropertyName("fetch_ts")]
    public long? FetchTs { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.pub_ts</summary>
    [JsonPropertyName("pub_ts")]
    public long? PubTs { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.enclosure_url</summary>
    [JsonPropertyName("enclosure_url")]
    public string? EnclosureUrl { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.enclosure_type</summary>
    [JsonPropertyName("enclosure_type")]
    public string? EnclosureType { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.enclosure_length</summary>
    [JsonPropertyName("enclosure_length")]
    public long? EnclosureLength { get; init; }

}
