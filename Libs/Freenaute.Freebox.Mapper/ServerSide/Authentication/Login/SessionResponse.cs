using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.ServerSide.Models;

namespace Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;

/// <summary>
///     Represents the response received after a successful login session.
/// </summary>
/// <param name="SessionToken">The token for the session.</param>
/// <param name="Challenge">The challenge string for the session.</param>
/// <param name="PermissionsModel">The permissions associated with the session.</param>
/// <seealso cref="SessionPermissionModel" />
public sealed record SessionResponse(
    [property: JsonPropertyName("session_token")]
    string SessionToken,
    [property: JsonPropertyName("challenge")]
    string Challenge,
    [property: JsonPropertyName("permissions")]
    SessionPermissionModel PermissionsModel
);
