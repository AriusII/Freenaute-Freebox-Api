using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DownloadStats. Unknown enum strings are preserved.</summary>
public sealed record DownloadStats
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks</summary>
    [JsonPropertyName("nb_tasks")]
    public long? NbTasks { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_stopped</summary>
    [JsonPropertyName("nb_tasks_stopped")]
    public long? NbTasksStopped { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_checking</summary>
    [JsonPropertyName("nb_tasks_checking")]
    public long? NbTasksChecking { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_queued</summary>
    [JsonPropertyName("nb_tasks_queued")]
    public long? NbTasksQueued { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_extracting</summary>
    [JsonPropertyName("nb_tasks_extracting")]
    public long? NbTasksExtracting { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_done</summary>
    [JsonPropertyName("nb_tasks_done")]
    public long? NbTasksDone { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_repairing</summary>
    [JsonPropertyName("nb_tasks_repairing")]
    public long? NbTasksRepairing { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_seeding</summary>
    [JsonPropertyName("nb_tasks_seeding")]
    public long? NbTasksSeeding { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_downloading</summary>
    [JsonPropertyName("nb_tasks_downloading")]
    public long? NbTasksDownloading { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_error</summary>
    [JsonPropertyName("nb_tasks_error")]
    public long? NbTasksError { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_stopping</summary>
    [JsonPropertyName("nb_tasks_stopping")]
    public long? NbTasksStopping { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_active</summary>
    [JsonPropertyName("nb_tasks_active")]
    public long? NbTasksActive { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_rss</summary>
    [JsonPropertyName("nb_rss")]
    public long? NbRss { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_rss_items_unread</summary>
    [JsonPropertyName("nb_rss_items_unread")]
    public long? NbRssItemsUnread { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.rx_rate</summary>
    [JsonPropertyName("rx_rate")]
    public long? RxRate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.tx_rate</summary>
    [JsonPropertyName("tx_rate")]
    public long? TxRate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.throttling_mode</summary>
    [JsonPropertyName("throttling_mode")]
    public string? ThrottlingMode { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.throttling_is_scheduled</summary>
    [JsonPropertyName("throttling_is_scheduled")]
    public bool? ThrottlingIsScheduled { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.throttling_rate</summary>
    [JsonPropertyName("throttling_rate")]
    public DlRate? ThrottlingRate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nzb_config_status</summary>
    [JsonPropertyName("nzb_config_status")]
    public NzbConfigStatus? NzbConfigStatus { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.conn_ready</summary>
    [JsonPropertyName("conn_ready")]
    public bool? ConnReady { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_peer</summary>
    [JsonPropertyName("nb_peer")]
    public long? NbPeer { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.blocklist_entries</summary>
    [JsonPropertyName("blocklist_entries")]
    public long? BlocklistEntries { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.blocklist_hits</summary>
    [JsonPropertyName("blocklist_hits")]
    public long? BlocklistHits { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadStats.dht_stats</summary>
    [JsonPropertyName("dht_stats")]
    public DhtStats? DhtStats { get; init; }

}
