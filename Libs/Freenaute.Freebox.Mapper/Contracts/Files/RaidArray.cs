using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#RaidArray. Unknown enum strings are preserved.</summary>
public sealed record RaidArray
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.state</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.level</summary>
    [JsonPropertyName("level")]
    public string? Level { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.disk_id</summary>
    [JsonPropertyName("disk_id")]
    public long? DiskId { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.uuid</summary>
    [JsonPropertyName("uuid")]
    public string? Uuid { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_action</summary>
    [JsonPropertyName("sync_action")]
    public string? SyncAction { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.sysfs_state</summary>
    [JsonPropertyName("sysfs_state")]
    public string? SysfsState { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.array_size</summary>
    [JsonPropertyName("array_size")]
    public long? ArraySize { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.raid_disks</summary>
    [JsonPropertyName("raid_disks")]
    public long? RaidDisks { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_speed</summary>
    [JsonPropertyName("sync_speed")]
    public long? SyncSpeed { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_completed_pos</summary>
    [JsonPropertyName("sync_completed_pos")]
    public long? SyncCompletedPos { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_completed_end</summary>
    [JsonPropertyName("sync_completed_end")]
    public long? SyncCompletedEnd { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_completed_percent</summary>
    [JsonPropertyName("sync_completed_percent")]
    public long? SyncCompletedPercent { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.check_interval</summary>
    [JsonPropertyName("check_interval")]
    public long? CheckInterval { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.last_check</summary>
    [JsonPropertyName("last_check")]
    public long? LastCheck { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.next_check</summary>
    [JsonPropertyName("next_check")]
    public long? NextCheck { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.degraded</summary>
    [JsonPropertyName("degraded")]
    public bool? Degraded { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.members</summary>
    [JsonPropertyName("members")]
    public RaidMember[]? Members { get; init; }

}
