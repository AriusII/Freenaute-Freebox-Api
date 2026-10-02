using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

/// <summary>
///     Represents the possible statuses of an authorization.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AuthorizationTrackEnum>))]
public enum AuthorizationTrackEnum
{
    /// <summary>
    ///     The app_token is invalid or has been revoked.
    /// </summary>
    [JsonStringEnumMemberName("unknown")] Unknown,

    /// <summary>
    ///     The user has not confirmed the authorization request yet.
    /// </summary>
    [JsonStringEnumMemberName("pending")] Pending,

    /// <summary>
    ///     The user did not confirm the authorization within the given time.
    /// </summary>
    [JsonStringEnumMemberName("timeout")] Timeout,

    /// <summary>
    ///     The app_token is valid and can be used to open a session.
    /// </summary>
    [JsonStringEnumMemberName("granted")] Granted,

    /// <summary>
    ///     The user denied the authorization request.
    /// </summary>
    [JsonStringEnumMemberName("denied")] Denied
}
