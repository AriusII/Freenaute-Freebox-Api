using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ClientSide.Authentication.Login;

/// <summary>
///     Represents a request for a token with application and device details.
/// </summary>
/// <param name="AppId">The application ID.</param>
/// <param name="AppName">The application name.</param>
/// <param name="AppVersion">The application version.</param>
/// <param name="DeviceName">The device name.</param>
public sealed record TokenRequest(
    [property: JsonPropertyName("app_id")] string AppId,
    [property: JsonPropertyName("app_name")]
    string AppName,
    [property: JsonPropertyName("app_version")]
    string AppVersion,
    [property: JsonPropertyName("device_name")]
    string DeviceName);
