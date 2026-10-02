using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Read projection of the documented PvrConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#PvrConfig" />
public sealed record PvrConfig
{
    [JsonPropertyName("margin_before")]
    public long? MarginBefore { get; init; }

    [JsonPropertyName("margin_after")]
    public long? MarginAfter { get; init; }

    public override string ToString() => nameof(PvrConfig);
}

/// <summary>Read projection of the documented PvrQuota wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#PvrQuota" />
public sealed record PvrQuota
{
    [JsonPropertyName("quota_exceeded")]
    public bool? QuotaExceeded { get; init; }

    [JsonPropertyName("needed_tresh")]
    public long? NeededTresh { get; init; }

    [JsonPropertyName("cur_tresh")]
    public long? CurTresh { get; init; }

    public override string ToString() => nameof(PvrQuota);
}

/// <summary>Read projection of the documented Precord wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Precord" />
public sealed record ProgrammedRecording
{
    [JsonPropertyName("id")]
    public StringOrInteger? Id { get; init; }

    [JsonPropertyName("media")]
    public string? Media { get; init; }

    [JsonPropertyName("path")]
    public string? Path { get; init; }

    [JsonPropertyName("has_record_gen")]
    public bool? HasRecordGen { get; init; }

    [JsonPropertyName("record_gen_id")]
    public long? RecordGenId { get; init; }

    [JsonPropertyName("conflict")]
    public bool? Conflict { get; init; }

    [JsonPropertyName("overlap_list")]
    public long[]? OverlapList { get; init; }

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("altered")]
    public bool? Altered { get; init; }

    [JsonPropertyName("state")]
    public RecordingState? State { get; init; }

    [JsonPropertyName("error")]
    public RecordingError? Error { get; init; }

    [JsonPropertyName("channel_uuid")]
    public string? ChannelUuid { get; init; }

    [JsonPropertyName("channel_name")]
    public string? ChannelName { get; init; }

    [JsonPropertyName("channel_quality")]
    public RecordingQuality? ChannelQuality { get; init; }

    [JsonPropertyName("channel_type")]
    public RecordingChannelType? ChannelType { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("subname")]
    public string? Subname { get; init; }

    [JsonPropertyName("broadcast_type")]
    public RecordingBroadcastType? BroadcastType { get; init; }

    [JsonPropertyName("start")]
    public long? Start { get; init; }

    [JsonPropertyName("end")]
    public long? End { get; init; }

    [JsonPropertyName("legacy_uri")]
    public string? LegacyUri { get; init; }

    [JsonPropertyName("force_channel_name")]
    public string? ForceChannelName { get; init; }

    [JsonPropertyName("margin_before")]
    public long? MarginBefore { get; init; }

    [JsonPropertyName("margin_after")]
    public long? MarginAfter { get; init; }

    public override string ToString() => nameof(ProgrammedRecording);
}

/// <summary>Read projection of the documented Frecord wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Frecord" />
public sealed record FinishedRecording
{
    [JsonPropertyName("id")]
    public StringOrInteger? Id { get; init; }

    [JsonPropertyName("media")]
    public string? Media { get; init; }

    [JsonPropertyName("path")]
    public string? Path { get; init; }

    [JsonPropertyName("filename")]
    public string? Filename { get; init; }

    [JsonPropertyName("byte_size")]
    public long? ByteSize { get; init; }

    [JsonPropertyName("has_record_gen")]
    public bool? HasRecordGen { get; init; }

    [JsonPropertyName("record_gen_id")]
    public long? RecordGenId { get; init; }

    [JsonPropertyName("altered")]
    public bool? Altered { get; init; }

    [JsonPropertyName("state")]
    public RecordingState? State { get; init; }

    [JsonPropertyName("error")]
    public RecordingError? Error { get; init; }

    [JsonPropertyName("channel_uuid")]
    public string? ChannelUuid { get; init; }

    [JsonPropertyName("channel_name")]
    public string? ChannelName { get; init; }

    [JsonPropertyName("channel_quality")]
    public RecordingQuality? ChannelQuality { get; init; }

    [JsonPropertyName("channel_type")]
    public RecordingChannelType? ChannelType { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("subname")]
    public string? Subname { get; init; }

    [JsonPropertyName("broadcast_type")]
    public RecordingBroadcastType? BroadcastType { get; init; }

    [JsonPropertyName("start")]
    public long? Start { get; init; }

    [JsonPropertyName("end")]
    public long? End { get; init; }

    [JsonPropertyName("secure")]
    public bool? Secure { get; init; }

    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    public override string ToString() => nameof(FinishedRecording);
}

/// <summary>Read projection of the documented Media wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Media" />
public sealed record PvrMedia
{
    [JsonPropertyName("media")]
    public string? Media { get; init; }

    [JsonPropertyName("free_bytes")]
    public long? FreeBytes { get; init; }

    [JsonPropertyName("total_bytes")]
    public long? TotalBytes { get; init; }

    [JsonPropertyName("record_time")]
    public PvrRecordTime? RecordTime { get; init; }

    public override string ToString() => nameof(PvrMedia);
}
