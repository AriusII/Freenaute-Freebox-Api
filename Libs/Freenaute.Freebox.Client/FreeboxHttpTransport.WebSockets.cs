using System.Net;
using System.Net.WebSockets;
using Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;

namespace Freenaute.Freebox.Client;

internal sealed partial class FreeboxHttpTransport
{
    public async Task<FreeboxWebSocketConnection> ConnectAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        FreeboxApiPath.ValidateRelativePath(relativePath);
        cancellationToken.ThrowIfCancellationRequested();
        var socket = new ClientWebSocket();
        socket.Options.CollectHttpResponseDetails = true;
        SessionResponse? session = null;
        var timeout = standaloneTimeout ?? httpClient.Timeout;
        using var handshake = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        if (timeout != Timeout.InfiniteTimeSpan) handshake.CancelAfter(timeout);
        try
        {
            var apiAddress = await GetApiAddressAsync(handshake.Token).ConfigureAwait(false);
            session = Volatile.Read(ref state.Session);
            if (session is null)
            {
                if (!state.Options.AuthenticateAutomatically)
                    throw new FreeboxAuthenticationException("Open a session before opening an authenticated WebSocket.");
                session = await OpenSessionAsync(handshake.Token).ConfigureAwait(false);
            }
            var address = FreeboxApiPath.Resolve(apiAddress, relativePath);
            var websocketAddress = new UriBuilder(address)
            {
                Scheme = address.Scheme == Uri.UriSchemeHttps ? "wss" : "ws"
            }.Uri;
            socket.Options.SetRequestHeader(AuthenticationHeader, session.SessionToken);
            // The provided invoker retains the configured TLS roots, hostname validation and DI handlers.
            await socket.ConnectAsync(websocketAddress, httpClient, handshake.Token).ConfigureAwait(false);
            return new FreeboxWebSocketConnection(socket);
        }
        catch (WebSocketException error) when (session is not null &&
            (socket.HttpStatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden ||
             error.InnerException is HttpRequestException { StatusCode: HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden }))
        {
            Interlocked.CompareExchange(ref state.Session, null, session);
            socket.Dispose();
            throw;
        }
        catch
        {
            socket.Dispose();
            throw;
        }
    }
}
