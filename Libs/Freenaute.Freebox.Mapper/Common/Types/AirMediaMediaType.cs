using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

public enum AirMediaMediaType
{
    [JsonPropertyName("photo")] Photo,
    [JsonPropertyName("video")] Video
}
