using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Common.Types;

namespace Freenaute.Freebox.Mapper.ClientSide.Api.AirMedia;

public sealed record AirMediaReceiverRequest(
    [property: JsonPropertyName("action")] AirMediaAction Action,
    [property: JsonPropertyName("media_type")]
    AirMediaMediaType MediaType,
    [property: JsonPropertyName("media")] string Media,
    [property: JsonPropertyName("password")]
    string? Password = null,
    [property: JsonPropertyName("position")]
    int? Position = null
);
