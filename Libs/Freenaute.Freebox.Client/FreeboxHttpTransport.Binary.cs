using System.Text.Json;
using Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;

namespace Freenaute.Freebox.Client;

internal sealed partial class FreeboxHttpTransport
{
    public async Task<FreeboxDownload> DownloadAsync(HttpMethod method, string relativePath,
        CancellationToken cancellationToken = default, bool requiresAuthentication = true)
    {
        ArgumentNullException.ThrowIfNull(method);
        FreeboxApiPath.ValidateRelativePath(relativePath);
        cancellationToken.ThrowIfCancellationRequested();
        var lifetime = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var timeout = standaloneTimeout ?? httpClient.Timeout;
        if (timeout != Timeout.InfiniteTimeSpan) lifetime.CancelAfter(timeout);
        HttpResponseMessage? response = null;
        SessionResponse? session = null;
        try
        {
            var apiAddress = await GetApiAddressAsync(lifetime.Token).ConfigureAwait(false);
            if (requiresAuthentication)
            {
                session = Volatile.Read(ref state.Session);
                if (session is null)
                {
                    if (!state.Options.AuthenticateAutomatically)
                        throw new FreeboxAuthenticationException("Open a session before making authenticated requests, or enable automatic authentication.");
                    session = await OpenSessionAsync(lifetime.Token).ConfigureAwait(false);
                }
            }

            using var request = new HttpRequestMessage(method, FreeboxApiPath.Resolve(apiAddress, relativePath));
            if (session is not null) request.Headers.Add(AuthenticationHeader, session.SessionToken);
            response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead,
                lifetime.Token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                await using var errorBody = await response.Content.ReadAsStreamAsync(lifetime.Token).ConfigureAwait(false);
                try
                {
                    using var error = await JsonDocument.ParseAsync(errorBody, cancellationToken: lifetime.Token)
                        .ConfigureAwait(false);
                    throw CreateApiException(response.StatusCode, error.RootElement);
                }
                catch (JsonException)
                {
                    throw new FreeboxApiException(response.StatusCode);
                }
            }

            var stream = await response.Content.ReadAsStreamAsync(lifetime.Token).ConfigureAwait(false);
            var download = new FreeboxDownload(response, stream, lifetime);
            response = null;
            return download;
        }
        catch (FreeboxApiException exception) when (session is not null &&
            (exception.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
             exception.ErrorCode is "auth_required" or "invalid_session"))
        {
            Interlocked.CompareExchange(ref state.Session, null, session);
            response?.Dispose();
            lifetime.Dispose();
            throw;
        }
        catch
        {
            response?.Dispose();
            lifetime.Dispose();
            throw;
        }
    }
}
