using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#RaidMember. Unknown enum strings are preserved.</summary>
public sealed record RaidMember
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.array_id</summary>
    [JsonPropertyName("array_id")]
    public long? ArrayId { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.role</summary>
    [JsonPropertyName("role")]
    public string? Role { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.set_name</summary>
    [JsonPropertyName("set_name")]
    public string? SetName { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.set_uuid</summary>
    [JsonPropertyName("set_uuid")]
    public string? SetUuid { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.dev_uuid</summary>
    [JsonPropertyName("dev_uuid")]
    public string? DevUuid { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.device_location</summary>
    [JsonPropertyName("device_location")]
    public string? DeviceLocation { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.total_bytes</summary>
    [JsonPropertyName("total_bytes")]
    public long? TotalBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.active_device</summary>
    [JsonPropertyName("active_device")]
    public long? ActiveDevice { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.corrected_read_errors</summary>
    [JsonPropertyName("corrected_read_errors")]
    public long? CorrectedReadErrors { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.sct_erc_supported</summary>
    [JsonPropertyName("sct_erc_supported")]
    public bool? SctErcSupported { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.sct_erc_enabled</summary>
    [JsonPropertyName("sct_erc_enabled")]
    public bool? SctErcEnabled { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidMember.disk</summary>
    [JsonPropertyName("disk")]
    public RaidDisk? Disk { get; init; }

}
