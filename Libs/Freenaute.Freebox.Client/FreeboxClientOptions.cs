namespace Freenaute.Freebox.Client;

/// <summary>Configures one Freebox and one application identity.</summary>
public sealed class FreeboxClientOptions
{
    /// <summary>The HTTP(S) origin used for discovery and subsequent requests.</summary>
    public Uri ServerAddress { get; set; } = new("https://mafreebox.freebox.fr/");

    /// <summary>
    /// An explicit API major version override. Leave unset to discover the latest version from the server.
    /// </summary>
    public int? ApiVersion { get; set; }

    /// <summary>The API root path used with an explicit version override.</summary>
    public string ApiBasePath { get; set; } = "/api/";

    /// <summary>The timeout applied by the client to each HTTP operation.</summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    public string? ApplicationId { get; set; }

    /// <summary>The application version sent when opening a session, when configured.</summary>
    public string? ApplicationVersion { get; set; }

    /// <summary>A sensitive application token obtained through device authorization.</summary>
    public string? AppToken { get; set; }

    /// <summary>Whether authenticated requests may lazily open a session with the configured credentials.</summary>
    public bool AuthenticateAutomatically { get; set; } = true;

    public FreeboxClientOptions UseServer(Uri serverAddress)
    {
        ServerAddress = serverAddress;
        return this;
    }

    public FreeboxClientOptions UseCredentials(string applicationId, string appToken)
    {
        ApplicationId = applicationId;
        AppToken = appToken;
        return this;
    }

    public FreeboxClientOptions WithTimeout(TimeSpan timeout)
    {
        Timeout = timeout;
        return this;
    }

    public FreeboxClientOptions WithApplicationVersion(string applicationVersion)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationVersion);
        ApplicationVersion = applicationVersion;
        return this;
    }

    internal FreeboxClientOptions CreateValidatedSnapshot()
    {
        ArgumentNullException.ThrowIfNull(ServerAddress);
        if (!ServerAddress.IsAbsoluteUri ||
            (ServerAddress.Scheme != Uri.UriSchemeHttp && ServerAddress.Scheme != Uri.UriSchemeHttps) ||
            !string.IsNullOrEmpty(ServerAddress.UserInfo) ||
            !string.IsNullOrEmpty(ServerAddress.Query) || !string.IsNullOrEmpty(ServerAddress.Fragment) ||
            ServerAddress.AbsolutePath != "/")
        {
            throw new ArgumentException("ServerAddress must be an absolute HTTP(S) origin without a path, credentials, query or fragment.", nameof(ServerAddress));
        }

        if (ApiVersion is <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ApiVersion), "The API major version must be positive.");
        }

        if (Timeout != System.Threading.Timeout.InfiniteTimeSpan &&
            (Timeout <= TimeSpan.Zero || Timeout.TotalMilliseconds > int.MaxValue))
        {
            throw new ArgumentOutOfRangeException(nameof(Timeout));
        }

        if ((ApplicationId is null) != (AppToken is null))
        {
            throw new ArgumentException("ApplicationId and AppToken must be configured together.");
        }

        if (ApplicationId is not null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(ApplicationId);
            ArgumentException.ThrowIfNullOrWhiteSpace(AppToken);
        }

        if (ApplicationVersion is not null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(ApplicationVersion);
        }

        var basePath = FreeboxApiPath.NormalizeBasePath(ApiBasePath);
        return new FreeboxClientOptions
        {
            ServerAddress = ServerAddress,
            ApiVersion = ApiVersion,
            ApiBasePath = basePath,
            Timeout = Timeout,
            ApplicationId = ApplicationId,
            ApplicationVersion = ApplicationVersion,
            AppToken = AppToken,
            AuthenticateAutomatically = AuthenticateAutomatically
        };
    }
}
