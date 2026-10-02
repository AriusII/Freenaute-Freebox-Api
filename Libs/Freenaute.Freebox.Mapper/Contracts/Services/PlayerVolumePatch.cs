using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Write projection of PlayerVolume; unset fields are omitted and explicit null is rejected.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#put--api-v8-player-id_player-api-v6-control-volume-" />
public sealed record PlayerVolumePatch
{
    [JsonPropertyName("volume")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<int>))]
    public Optional<int> Volume { get; init; }

    [JsonPropertyName("mute")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Mute { get; init; }

    public void Validate()
    {
        if (Volume.IsNull) throw new ArgumentException("Explicit null is not documented for PlayerVolume.Volume.", nameof(Volume));
        if (Mute.IsNull) throw new ArgumentException("Explicit null is not documented for PlayerVolume.Mute.", nameof(Mute));
        if (Volume.HasValue && (Volume.Value < 0 || Volume.Value > 100)) throw new ArgumentOutOfRangeException(nameof(Volume), "Volume must be between 0 and 100.");
    }

    public override string ToString() => nameof(PlayerVolumePatch);
}
