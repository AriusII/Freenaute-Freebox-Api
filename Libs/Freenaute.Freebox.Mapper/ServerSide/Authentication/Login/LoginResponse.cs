using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;

public sealed record LoginResponse(
    [property: JsonPropertyName("logged_in")]
    bool LoggedIn,
    [property: JsonPropertyName("challenge")]
    string Challenge
);
