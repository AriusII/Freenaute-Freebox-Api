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
/// <param name="BoxModel">The original model identifier advertised by the box.</param>
/// <param name="ApiDomain">The domain to use in place of the hardcoded Freebox IP.</param>
/// <param name="Uid">The unique ID of the device.</param>
/// <param name="ApiVersion">The current API version on the Freebox.</param>
[method: JsonConstructor]
public sealed record ApiVersionResponse(
    [property: JsonPropertyName("uid")] string? Uid,
    [property: JsonPropertyName("device_name")]
    string? DeviceName,
    [property: JsonPropertyName("box_model")]
    string? BoxModel,
    [property: JsonPropertyName("box_model_name")]
    string? BoxModelName,
    [property: JsonPropertyName("api_version")]
    string ApiVersion,
    [property: JsonPropertyName("api_domain")]
    string? ApiDomain,
    [property: JsonPropertyName("api_base_url")]
    string ApiBaseUrl,
    [property: JsonPropertyName("https_available")]
    bool? HttpsAvailable,
    [property: JsonPropertyName("https_port")]
    int? HttpsPort
)
{
    /// <summary>Creates discovery metadata from a known model identifier.</summary>
    public ApiVersionResponse(string? Uid, string? DeviceName, BoxModels BoxModel, string? BoxModelName,
        string ApiVersion, string? ApiDomain, string ApiBaseUrl, bool HttpsAvailable, int HttpsPort)
        : this(Uid, DeviceName, BoxModel.GetWireName(), BoxModelName, ApiVersion, ApiDomain, ApiBaseUrl,
            HttpsAvailable, HttpsPort)
    {
    }

    /// <summary>The known model classification, or null for an absent or unrecognized identifier.</summary>
    [JsonIgnore]
    public BoxModels? KnownBoxModel => BoxModelNames.Classify(BoxModel);

    /// <summary>
    ///     Gets the absolute Freebox API URL, with a normalized base path and a trailing slash.
    /// </summary>
    /// <remarks>
    ///     Constructs the URL using the following components:
    ///     <para>
    ///         The trailing slash allows relative request paths to retain the API version segment.
    ///         HTTPS uses the advertised port; URI formatting omits a default port.
    ///     </para>
    /// </remarks>
    /// <example>
    ///     If HttpsAvailable is true, ApiDomain is "example.com", HttpsPort is 443, ApiBaseUrl is "/api", and ApiVersion is
    ///     "1.0", <br />
    ///     the resulting URL will be "https://example.com/api/v1/".
    /// </example>
    [JsonIgnore]
    public string ApiUrl => ApiUri.AbsoluteUri;

    /// <summary>
    ///     Gets the advertised API base address for use by an HTTP client.
    /// </summary>
    [JsonIgnore]
    public Uri ApiUri
    {
        get
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(ApiDomain);
            ArgumentException.ThrowIfNullOrWhiteSpace(ApiVersion);
            ArgumentNullException.ThrowIfNull(ApiBaseUrl);

            if (HttpsAvailable is null)
            {
                throw new InvalidOperationException("Discovery did not advertise whether HTTPS is available.");
            }

            var versionSeparator = ApiVersion.IndexOf('.');
            var majorVersion = versionSeparator < 0 ? ApiVersion.AsSpan() : ApiVersion.AsSpan(0, versionSeparator);
            if (!int.TryParse(majorVersion, System.Globalization.NumberStyles.None,
                    System.Globalization.CultureInfo.InvariantCulture, out var major) || major <= 0)
            {
                throw new FormatException("The advertised API version must have a positive integer major version.");
            }

            if (HttpsAvailable == true && (HttpsPort is null or < 1 or > 65535))
            {
                throw new ArgumentOutOfRangeException(nameof(HttpsPort), "The advertised HTTPS port is invalid.");
            }

            var basePath = string.Join('/', ApiBaseUrl.Split('/', StringSplitOptions.RemoveEmptyEntries));
            var path = basePath.Length == 0 ? $"/v{major}/" : $"/{basePath}/v{major}/";
            return new UriBuilder
            {
                Scheme = HttpsAvailable == true ? Uri.UriSchemeHttps : Uri.UriSchemeHttp,
                Host = ApiDomain,
                Port = HttpsAvailable == true ? HttpsPort!.Value : 80,
                Path = path
            }.Uri;
        }
    }
}
