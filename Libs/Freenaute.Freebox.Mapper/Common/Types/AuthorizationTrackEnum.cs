namespace Freenaute.Freebox.Mapper.Common.Types;

/// <summary>
///     Represents the possible statuses of an authorization.
/// </summary>
public enum AuthorizationTrackEnum
{
    /// <summary>
    ///     The app_token is invalid or has been revoked.
    /// </summary>
    Unknown,

    /// <summary>
    ///     The user has not confirmed the authorization request yet.
    /// </summary>
    Pending,

    /// <summary>
    ///     The user did not confirm the authorization within the given time.
    /// </summary>
    Timeout,

    /// <summary>
    ///     The app_token is valid and can be used to open a session.
    /// </summary>
    Granted,

    /// <summary>
    ///     The user denied the authorization request.
    /// </summary>
    Denied
}
