using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

public sealed record AirMediaCapabilities(
    [property: JsonPropertyName("photo")] bool CanDisplayPhotos,
    [property: JsonPropertyName("audio")] bool CanPlayAudio,
    [property: JsonPropertyName("video")] bool CanPlayVideo,
    [property: JsonPropertyName("screen")] bool CanDisplayScreen
);
