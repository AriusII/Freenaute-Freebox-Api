using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ClientSide.Authentication.Login;

/// <summary>
///     Represents a request to start a session.
/// </summary>
/// <param name="AppId">The application ID used in TokenRequest to get the app_token.</param>
/// <param name="Password">The password computed using the challenge and the app_token.</param>
public sealed record SessionStartRequest(
    [property: JsonPropertyName("app_id")] string AppId,
    [property: JsonPropertyName("password")]
    string Password
);
