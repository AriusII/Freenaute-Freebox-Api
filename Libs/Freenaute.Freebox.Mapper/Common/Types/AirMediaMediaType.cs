using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

[JsonConverter(typeof(JsonStringEnumConverter<AirMediaMediaType>))]
public enum AirMediaMediaType
{
    [JsonStringEnumMemberName("photo")] Photo,
    [JsonStringEnumMemberName("video")] Video
}
