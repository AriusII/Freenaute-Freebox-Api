using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ClientSide.Api.AirMedia;

public sealed record UpdateAirMediaConfigRequest(
    [property: JsonPropertyName("enabled")]
    bool Enabled,
    [property: JsonPropertyName("password")]
    string Password
);
