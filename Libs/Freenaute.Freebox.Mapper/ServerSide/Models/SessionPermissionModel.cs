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
/// <param name="Parental">Access to parental control (obsolete).</param>
/// <param name="Pvr">Access personal video recorder.</param>
/// <param name="Profile">Access to user profile management.</param>
public sealed record SessionPermissionModel(
    [property: JsonPropertyName("settings")]
    bool? Settings,
    [property: JsonPropertyName("contacts")]
    bool? Contacts,
    [property: JsonPropertyName("calls")] bool? Calls,
    [property: JsonPropertyName("explorer")]
    bool? Explorer,
    [property: JsonPropertyName("downloader")]
    bool? Downloader,
    [property: JsonPropertyName("parental")]
    bool? Parental,
    [property: JsonPropertyName("pvr")] bool? Pvr,
    [property: JsonPropertyName("profile")]
    bool? Profile);
