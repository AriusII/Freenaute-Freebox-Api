using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of Precord; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Precord" />
public sealed record ProgrammedRecordingWriteRequest
{
    [JsonPropertyName("media")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Media { get; init; }

    [JsonPropertyName("path")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Path { get; init; }

    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    [JsonPropertyName("channel_uuid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> ChannelUuid { get; init; }

    [JsonPropertyName("channel_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> ChannelName { get; init; }

    [JsonPropertyName("channel_quality")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<RecordingQuality>))]
    public Optional<RecordingQuality> ChannelQuality { get; init; }

    [JsonPropertyName("channel_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<RecordingChannelType>))]
    public Optional<RecordingChannelType> ChannelType { get; init; }

    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Name { get; init; }

    [JsonPropertyName("subname")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Subname { get; init; }

    [JsonPropertyName("broadcast_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<RecordingBroadcastType>))]
    public Optional<RecordingBroadcastType> BroadcastType { get; init; }

    [JsonPropertyName("start")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Start { get; init; }

    [JsonPropertyName("end")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> End { get; init; }

    [JsonPropertyName("legacy_uri")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> LegacyUri { get; init; }

    [JsonPropertyName("force_channel_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> ForceChannelName { get; init; }

    public void Validate()
    {
        if (Media.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.Media.", nameof(Media));
        if (Path.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.Path.", nameof(Path));
        if (Enabled.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.Enabled.", nameof(Enabled));
        if (ChannelUuid.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.ChannelUuid.", nameof(ChannelUuid));
        if (ChannelName.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.ChannelName.", nameof(ChannelName));
        if (ChannelQuality.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.ChannelQuality.", nameof(ChannelQuality));
        if (ChannelType.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.ChannelType.", nameof(ChannelType));
        if (Name.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.Name.", nameof(Name));
        if (Subname.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.Subname.", nameof(Subname));
        if (BroadcastType.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.BroadcastType.", nameof(BroadcastType));
        if (Start.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.Start.", nameof(Start));
        if (End.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.End.", nameof(End));
        if (LegacyUri.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.LegacyUri.", nameof(LegacyUri));
        if (ForceChannelName.IsNull) throw new ArgumentException("Explicit null is not documented for Precord.ForceChannelName.", nameof(ForceChannelName));
    }

    public override string ToString() => nameof(ProgrammedRecordingWriteRequest);
}
