using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

public enum AirMediaAction
{
    [JsonPropertyName("start")] Start,
    [JsonPropertyName("stop")] Stop
}
