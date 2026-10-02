using Freenaute.Freebox.Client.Domains.Protocol;
using Freenaute.Freebox.Client.Domains.Network;
using Freenaute.Freebox.Client.Domains.Services;
using Freenaute.Freebox.Client.Domains.Files;
using Freenaute.Freebox.Client.Domains.SystemHome;
using Freenaute.Freebox.Client.Domains.WebSockets;

namespace Freenaute.Freebox.Client;

/// <summary>An asynchronous Freebox HTTP facade backed by generated JSON contracts.</summary>
public sealed class FreeboxClient : IFreeboxClient, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly FreeboxClientState _state;
    private readonly bool _ownsState;
    private readonly bool _ownsHttpClient;

    public IFreeboxDiscoveryApi Discovery { get; }
    public IFreeboxAuthenticationApi Authentication { get; }
    public IFreeboxAirMediaApi AirMedia { get; }
    public IFreeboxCamerasApi Cameras { get; }
    public IFreeboxNotificationsApi Notifications { get; }
    public IFreeboxNetworkApi Network { get; }
    public IFreeboxServicesApi Services { get; }
    public IFreeboxFilesApi Files { get; }
    public IFreeboxSystemHomeApi SystemHome { get; }
    public IFreeboxWebSocketsApi WebSockets { get; }
    public IFreeboxTransport Transport { get; }
    public IFreeboxBinaryTransport BinaryTransport { get; }
    public IFreeboxWebSocketTransport WebSocketTransport { get; }

    /// <summary>
    /// Creates a standalone client. The caller configures the supplied HTTP handler, including redirect policy.
    /// The supplied HTTP client remains owned by the caller unless <paramref name="disposeHttpClient"/> is true.
    /// Its configuration is preserved. The configured operation timeout also covers response body reads.
    /// </summary>
    public FreeboxClient(HttpClient httpClient, FreeboxClientOptions options, bool disposeHttpClient = false)
        : this(httpClient, new FreeboxClientState((options ?? throw new ArgumentNullException(nameof(options)))
            .CreateValidatedSnapshot()), ownsState: true, ownsHttpClient: disposeHttpClient)
    {
    }

    internal FreeboxClient(HttpClient httpClient, FreeboxClientState state)
        : this(httpClient, state, ownsState: false, ownsHttpClient: true)
    {
    }

    private FreeboxClient(HttpClient httpClient, FreeboxClientState state, bool ownsState, bool ownsHttpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        _httpClient = httpClient;
        _state = state;
        _ownsState = ownsState;
        _ownsHttpClient = ownsHttpClient;
        var transport = new FreeboxHttpTransport(httpClient, state, ownsState ? state.Options.Timeout : null);
        Transport = transport;
        BinaryTransport = transport;
        WebSocketTransport = transport;
        Discovery = new FreeboxDiscoveryApi(transport);
        Authentication = new FreeboxAuthenticationApi(transport);
        AirMedia = new FreeboxAirMediaApi(transport);
        Cameras = new FreeboxCamerasApi(transport);
        Notifications = new FreeboxNotificationsApi(transport);
        Network = new FreeboxNetworkApi(transport);
        Services = new FreeboxServicesApi(transport);
        Files = new FreeboxFilesApi(transport, transport);
        SystemHome = new FreeboxSystemHomeApi(transport);
        WebSockets = new FreeboxWebSocketsApi(transport);
    }

    public void Dispose()
    {
        if (_ownsHttpClient)
        {
            _httpClient.Dispose();
        }
        if (_ownsState)
        {
            _state.Dispose();
        }
    }
}
