namespace Freenaute.Freebox.Mapper.Common.Types;

/// <summary>
///     Enumeration of possible authentication errors when accessing the API.
/// </summary>
public enum AuthenticationErrorEnum
{
    /// <summary>
    ///     Invalid session token, or no session token sent.
    /// </summary>
    AuthRequired,

    /// <summary>
    ///     The app token you are trying to use is invalid or has been revoked.
    /// </summary>
    InvalidToken,

    /// <summary>
    ///     The app token you are trying to use has not been validated by the user yet.
    /// </summary>
    PendingToken,

    /// <summary>
    ///     Your app permissions do not allow accessing this API.
    /// </summary>
    InsufficientRights,

    /// <summary>
    ///     You are trying to get an app token from a remote IP.
    /// </summary>
    DeniedFromExternalIp,

    /// <summary>
    ///     Your request is invalid.
    /// </summary>
    InvalidRequest,

    /// <summary>
    ///     Too many authentication errors have been made from your IP.
    /// </summary>
    RateLimited,

    /// <summary>
    ///     New application token request has been disabled.
    /// </summary>
    NewAppsDenied,

    /// <summary>
    ///     API access from apps has been disabled.
    /// </summary>
    AppsDenied,

    /// <summary>
    ///     Internal error.
    /// </summary>
    InternalError
}
