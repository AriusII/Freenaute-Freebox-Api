using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;

public sealed record AuthorizeResponse(
    [property: JsonPropertyName("app_token")]
    string AppToken,
    [property: JsonPropertyName("track_id")]
    int TrackId
);
