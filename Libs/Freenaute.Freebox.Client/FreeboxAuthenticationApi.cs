using System.Globalization;
using Freenaute.Freebox.Mapper.ClientSide.Authentication.Login;
using Freenaute.Freebox.Mapper.Common.Types;
using Freenaute.Freebox.Mapper.Serialization;
using Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;

namespace Freenaute.Freebox.Client;

internal sealed class FreeboxAuthenticationApi(FreeboxHttpTransport transport) : IFreeboxAuthenticationApi
{
    private static readonly FreeboxJsonSerializerContext Json = FreeboxJsonSerializerContext.Default;

    public Task<AuthorizeResponse> AuthorizeAsync(TokenRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.AppId);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.AppName);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.AppVersion);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.DeviceName);
        return transport.SendAsync(HttpMethod.Post, "login/authorize/", request, Json.TokenRequest,
            Json.AuthorizeResponse, cancellationToken, requiresAuthentication: false);
    }

    public Task<AuthorizeTrackResponse> TrackAuthorizationAsync(int trackId,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(trackId);
        return transport.SendAsync(HttpMethod.Get, $"login/authorize/{trackId.ToString(CultureInfo.InvariantCulture)}/",
            Json.AuthorizeTrackResponse, cancellationToken, requiresAuthentication: false);
    }

    public async Task<AuthorizeTrackResponse> WaitForAuthorizationAsync(int trackId, TimeSpan? pollInterval = null,
        CancellationToken cancellationToken = default)
    {
        var interval = pollInterval ?? TimeSpan.FromSeconds(1);
        if (interval <= TimeSpan.Zero || interval.TotalMilliseconds > uint.MaxValue - 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pollInterval));
        }

        while (true)
        {
            var result = await TrackAuthorizationAsync(trackId, cancellationToken).ConfigureAwait(false);
            if (result.Status != AuthorizationTrackEnum.Pending)
            {
                return result;
            }

            await Task.Delay(interval, cancellationToken).ConfigureAwait(false);
        }
    }

    public Task<LoginResponse> GetLoginAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "login/", Json.LoginResponse, cancellationToken,
            requiresAuthentication: false);

    public Task<SessionResponse> OpenSessionAsync(CancellationToken cancellationToken = default) =>
        transport.OpenSessionAsync(cancellationToken);

    public Task CloseSessionAsync(CancellationToken cancellationToken = default) =>
        transport.CloseSessionAsync(cancellationToken);
}
