using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

[JsonConverter(typeof(JsonStringEnumConverter<AirMediaAction>))]
public enum AirMediaAction
{
    [JsonStringEnumMemberName("start")] Start,
    [JsonStringEnumMemberName("stop")] Stop
}
