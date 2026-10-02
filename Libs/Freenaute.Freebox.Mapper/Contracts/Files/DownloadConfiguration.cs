using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration. Unknown enum strings are preserved.</summary>
public sealed record DownloadConfiguration
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.max_downloading_tasks</summary>
    [JsonPropertyName("max_downloading_tasks")]
    public long? MaxDownloadingTasks { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.download_dir</summary>
    [JsonPropertyName("download_dir")]
    public EncodedFreeboxPath? DownloadDir { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.watch_dir</summary>
    [JsonPropertyName("watch_dir")]
    public EncodedFreeboxPath? WatchDir { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.use_watch_dir</summary>
    [JsonPropertyName("use_watch_dir")]
    public bool? UseWatchDir { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.throttling</summary>
    [JsonPropertyName("throttling")]
    public DlThrottlingConfig? Throttling { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.news</summary>
    [JsonPropertyName("news")]
    public DlNewsConfig? News { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.bt</summary>
    [JsonPropertyName("bt")]
    public DlBtConfig? Bt { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.feed</summary>
    [JsonPropertyName("feed")]
    public DlFeedConfig? Feed { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.blocklist</summary>
    [JsonPropertyName("blocklist")]
    public JsonElement? Blocklist { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.dns1</summary>
    [JsonPropertyName("dns1")]
    public string? Dns1 { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.dns2</summary>
    [JsonPropertyName("dns2")]
    public string? Dns2 { get; init; }

}
