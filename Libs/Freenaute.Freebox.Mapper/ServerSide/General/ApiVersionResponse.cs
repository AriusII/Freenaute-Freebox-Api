using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Common.Types;

namespace Freenaute.Freebox.Mapper.ServerSide.General;

/// <summary>
///     Represents the API version model for the Freebox.
/// </summary>
/// <param name="BoxModelName">The display name of the box model.</param>
/// <param name="ApiBaseUrl">The API root path on the HTTP server.</param>
/// <param name="HttpsPort">The port to use for remote HTTPS access to the Freebox API.</param>
/// <param name="DeviceName">The name of the device.</param>
/// <param name="HttpsAvailable">Indicates if HTTPS has been configured on the Freebox.</param>
/// <param name="BoxModel">The box model.</param>
/// <param name="ApiDomain">The domain to use in place of the hardcoded Freebox IP.</param>
/// <param name="Uid">The unique ID of the device.</param>
/// <param name="ApiVersion">The current API version on the Freebox.</param>
/// <param name="DeviceType">The type of the device (deprecated: use box_model).</param>
public sealed record ApiVersionResponse(
    [property: JsonPropertyName("uid")] string Uid,
    [property: JsonPropertyName("device_name")]
    string DeviceName,
    [property: JsonPropertyName("box_model")]
    BoxModels BoxModel,
    [property: JsonPropertyName("box_model_name")]
    string BoxModelName,
    [property: JsonPropertyName("api_version")]
    string ApiVersion,
    [property: JsonPropertyName("api_domain")]
    string ApiDomain,
    [property: JsonPropertyName("api_base_url")]
    string ApiBaseUrl,
    [property: JsonPropertyName("https_available")]
    bool HttpsAvailable,
    [property: JsonPropertyName("https_port")]
    int HttpsPort,
    [property: JsonPropertyName("device_type")]
    string DeviceType
)
{
    /// <summary>
    ///     Gets the full Freebox API URL based on the current configuration.
    /// </summary>
    /// <remarks>
    ///     Constructs the URL using the following components:
    ///     <para>
    ///         - Protocol: "http" or "https" based on the HttpsAvailable property. <br />
    ///         - Domain: The ApiDomain property. <br />
    ///         - Port: The HttpsPort property if HTTPS is available, otherwise port 80. <br />
    ///         - Base URL: The ApiBaseUrl property. <br />
    ///         - API Version: The major version number from the ApiVersion property.
    ///     </para>
    /// </remarks>
    /// <example>
    ///     If HttpsAvailable is true, ApiDomain is "example.com", HttpsPort is 443, ApiBaseUrl is "/api", and ApiVersion is
    ///     "1.0", <br />
    ///     the resulting URL will be "https://example.com:443/api/v1/".
    /// </example>
    public string ApiUrl =>
        $"http{(HttpsAvailable ? "s" : "")}://{ApiDomain}:{(HttpsAvailable ? HttpsPort : 80)}{ApiBaseUrl}/v{ApiVersion.Split('.')[0]}";
}
