using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ServerSide.Models;

/// <summary>
///     Represents the permissions for a session.
/// </summary>
/// <param name="Settings">Allow modifying the Freebox settings (reading settings is always allowed).</param>
/// <param name="Contacts">Access to contact list.</param>
/// <param name="Calls">Access to call logs.</param>
/// <param name="Explorer">Access to filesystem.</param>
/// <param name="Downloader">Access to downloader.</param>
/// <param name="Pvr">Access personal video recorder.</param>
/// <param name="Profile">Access to user profile management.</param>
/// <param name="Camera">Access to camera recordings and live streams.</param>
public sealed record SessionPermissionModel(
    [property: JsonPropertyName("settings")]
    bool Settings = false,
    [property: JsonPropertyName("contacts")]
    bool Contacts = false,
    [property: JsonPropertyName("calls")] bool Calls = false,
    [property: JsonPropertyName("explorer")]
    bool Explorer = false,
    [property: JsonPropertyName("downloader")]
    bool Downloader = false,
    [property: JsonPropertyName("pvr")] bool Pvr = false,
    [property: JsonPropertyName("profile")]
    bool Profile = false,
    [property: JsonPropertyName("camera")] bool Camera = false);
