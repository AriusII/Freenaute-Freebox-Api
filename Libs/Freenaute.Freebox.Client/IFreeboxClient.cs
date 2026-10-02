using System.Text.Json.Serialization.Metadata;
using Freenaute.Freebox.Mapper.ClientSide.Api.AirMedia;
using Freenaute.Freebox.Mapper.ClientSide.Authentication.Login;
using Freenaute.Freebox.Mapper.ServerSide.Api.AirMedia;
using Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;
using Freenaute.Freebox.Mapper.ServerSide.General;
using Freenaute.Freebox.Client.Domains.Protocol;
using Freenaute.Freebox.Client.Domains.Network;
using Freenaute.Freebox.Client.Domains.Services;
using Freenaute.Freebox.Client.Domains.Files;
using Freenaute.Freebox.Client.Domains.SystemHome;
using Freenaute.Freebox.Client.Domains.WebSockets;

namespace Freenaute.Freebox.Client;

public interface IFreeboxClient
{
    IFreeboxDiscoveryApi Discovery { get; }
    IFreeboxAuthenticationApi Authentication { get; }
    IFreeboxAirMediaApi AirMedia { get; }
    IFreeboxCamerasApi Cameras { get; }
    IFreeboxNotificationsApi Notifications { get; }
    IFreeboxNetworkApi Network { get; }
    IFreeboxServicesApi Services { get; }
    IFreeboxFilesApi Files { get; }
    IFreeboxSystemHomeApi SystemHome { get; }
    IFreeboxWebSocketsApi WebSockets { get; }

    /// <summary>The typed transport for additional contracts with generated JSON metadata.</summary>
    IFreeboxTransport Transport { get; }
    IFreeboxBinaryTransport BinaryTransport { get; }
    IFreeboxWebSocketTransport WebSocketTransport { get; }
}

public interface IFreeboxDiscoveryApi
{
    Task<ApiVersionResponse> GetApiVersionAsync(CancellationToken cancellationToken = default);
}

public interface IFreeboxAuthenticationApi
{
    Task<AuthorizeResponse> AuthorizeAsync(TokenRequest request, CancellationToken cancellationToken = default);
    Task<AuthorizeTrackResponse> TrackAuthorizationAsync(int trackId, CancellationToken cancellationToken = default);

    /// <summary>Polls asynchronously until authorization leaves the pending state; the caller controls cancellation.</summary>
    Task<AuthorizeTrackResponse> WaitForAuthorizationAsync(int trackId, TimeSpan? pollInterval = null,
        CancellationToken cancellationToken = default);

    Task<LoginResponse> GetLoginAsync(CancellationToken cancellationToken = default);
    Task<SessionResponse> OpenSessionAsync(CancellationToken cancellationToken = default);
    Task CloseSessionAsync(CancellationToken cancellationToken = default);
}

public interface IFreeboxAirMediaApi
{
    Task<AirMediaConfigResponse> GetConfigurationAsync(CancellationToken cancellationToken = default);
    Task<AirMediaConfigResponse> UpdateConfigurationAsync(UpdateAirMediaConfigRequest request,
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AirMediaReceiverResponse>> GetReceiversAsync(CancellationToken cancellationToken = default);
    Task SendToReceiverAsync(string receiverName, AirMediaReceiverRequest request,
        CancellationToken cancellationToken = default);
    FreeboxAirMediaReceiver Receiver(string receiverName);
}

/// <summary>A reflection-free transport. Response metadata describes the envelope's <c>result</c>.</summary>
public interface IFreeboxTransport
{
    /// <summary>Sends form or multipart content and reads a generated JSON result. Ownership of content transfers to this operation.</summary>
    Task<TResponse> SendContentAsync<TResponse>(HttpMethod method, string relativePath,
        HttpContent content, JsonTypeInfo<TResponse> responseType,
        CancellationToken cancellationToken = default, bool requiresAuthentication = true);

    /// <summary>Sends form or multipart content for a success-only command. Ownership of content transfers to this operation.</summary>
    Task SendContentAsync(HttpMethod method, string relativePath, HttpContent content,
        CancellationToken cancellationToken = default, bool requiresAuthentication = true);

    Task<TResponse> SendAsync<TResponse>(HttpMethod method, string relativePath,
        JsonTypeInfo<TResponse> responseType, CancellationToken cancellationToken = default,
        bool requiresAuthentication = true);

    Task<TResponse> SendAsync<TRequest, TResponse>(HttpMethod method, string relativePath,
        TRequest request, JsonTypeInfo<TRequest> requestType, JsonTypeInfo<TResponse> responseType,
        CancellationToken cancellationToken = default, bool requiresAuthentication = true);

    Task SendAsync(HttpMethod method, string relativePath, CancellationToken cancellationToken = default,
        bool requiresAuthentication = true);

    Task SendAsync<TRequest>(HttpMethod method, string relativePath, TRequest request,
        JsonTypeInfo<TRequest> requestType, CancellationToken cancellationToken = default,
        bool requiresAuthentication = true);
}
