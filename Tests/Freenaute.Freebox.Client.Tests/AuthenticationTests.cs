using System.Net;
using System.Text.Json;
using Freenaute.Freebox.Mapper.ClientSide.Api.AirMedia;
using Freenaute.Freebox.Mapper.ClientSide.Authentication.Login;
using Freenaute.Freebox.Mapper.Common.Types;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class AuthenticationTests
{
    [Fact]
    public async Task AutomaticLoginComputesHmacAndKeepsTokenOffPublicEndpoints()
    {
        using var handler = new FixtureHandler((request, _) => Task.FromResult(Respond(request)));
        using var http = new HttpClient(handler);
        var client = HttpClientTests.CreateClient(http, authenticate: true);

        var configuration = await client.AirMedia.GetConfigurationAsync();
        await client.Authentication.GetLoginAsync();
        await client.Discovery.GetApiVersionAsync();

        Assert.True(configuration.Enabled);
        var sessionRequest = Assert.Single(handler.Requests, x => x.Address.AbsolutePath.EndsWith("/login/session/"));
        using var body = JsonDocument.Parse(sessionRequest.Body!);
        Assert.Equal("org.fixture", body.RootElement.GetProperty("app_id").GetString());
        // Independently calculated HMAC-SHA1(app_token, challenge), in lowercase hexadecimal.
        Assert.Equal("8951185163485b44ef71e6f92c2e872f7817f13c",
            body.RootElement.GetProperty("password").GetString());
        Assert.DoesNotContain("fixture-app-token", sessionRequest.Body!);
        Assert.All(handler.Requests.Where(x => !x.Address.AbsolutePath.Contains("airmedia", StringComparison.Ordinal)),
            request => Assert.Null(request.SessionToken));
        Assert.Equal("fixture-session", Assert.Single(handler.Requests, x =>
            x.Address.AbsolutePath.Contains("airmedia", StringComparison.Ordinal)).SessionToken);
        Assert.False(http.DefaultRequestHeaders.Contains("X-Fbx-App-Auth"));
    }

    [Fact]
    public async Task ConcurrentProtectedRequestsOpenOneSession()
    {
        var loginEntered = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var releaseLogin = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        using var handler = new FixtureHandler(async (request, cancellationToken) =>
        {
            if (request.Address.AbsolutePath.EndsWith("/login/", StringComparison.Ordinal))
            {
                loginEntered.TrySetResult();
                await releaseLogin.Task.WaitAsync(cancellationToken);
            }
            return Respond(request);
        });
        using var http = new HttpClient(handler);
        var client = HttpClientTests.CreateClient(http, authenticate: true);

        var calls = Enumerable.Range(0, 20).Select(_ => client.AirMedia.GetConfigurationAsync()).ToArray();
        await loginEntered.Task.WaitAsync(TimeSpan.FromSeconds(10));
        releaseLogin.SetResult();
        var results = await Task.WhenAll(calls).WaitAsync(TimeSpan.FromSeconds(10));

        Assert.All(results, configuration => Assert.True(configuration.Enabled));
        Assert.Single(handler.Requests, x => x.Address.AbsolutePath.EndsWith("/login/", StringComparison.Ordinal));
        Assert.Single(handler.Requests, x => x.Address.AbsolutePath.EndsWith("/login/session/", StringComparison.Ordinal));
        var protectedRequests = handler.Requests.Where(x => x.Address.AbsolutePath.Contains("airmedia", StringComparison.Ordinal)).ToArray();
        Assert.Equal(20, protectedRequests.Length);
        Assert.All(protectedRequests, request => Assert.Equal("fixture-session", request.SessionToken));
    }

    [Fact]
    public async Task CancelledSessionWaiterDoesNotCancelAnotherRequest()
    {
        var loginEntered = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var releaseLogin = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        using var handler = new FixtureHandler(async (request, cancellationToken) =>
        {
            if (request.Address.AbsolutePath.EndsWith("/login/", StringComparison.Ordinal))
            {
                loginEntered.TrySetResult();
                await releaseLogin.Task.WaitAsync(cancellationToken);
            }
            return Respond(request);
        });
        using var http = new HttpClient(handler);
        var client = HttpClientTests.CreateClient(http, authenticate: true);
        using var cancellation = new CancellationTokenSource();

        var owner = client.AirMedia.GetConfigurationAsync();
        await loginEntered.Task.WaitAsync(TimeSpan.FromSeconds(10));
        var waiter = client.AirMedia.GetConfigurationAsync(cancellation.Token);
        cancellation.Cancel();
        try
        {
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => waiter.WaitAsync(TimeSpan.FromSeconds(10)));
        }
        finally
        {
            releaseLogin.TrySetResult();
        }

        Assert.True((await owner.WaitAsync(TimeSpan.FromSeconds(10))).Enabled);
        Assert.Single(handler.Requests, x => x.Address.AbsolutePath.Contains("airmedia", StringComparison.Ordinal));
    }

    [Fact]
    public async Task ExpiredSessionDoesNotReplayMutationAndNextCallCanReauthenticate()
    {
        using var handler = new FixtureHandler((request, _) => Task.FromResult(
            request.Address.AbsolutePath.Contains("/airmedia/receivers/", StringComparison.Ordinal)
                ? FixtureHandler.Json("""{"success":false,"error_code":"auth_required","msg":"Expired session"}""", HttpStatusCode.Forbidden)
                : Respond(request)));
        using var http = new HttpClient(handler);
        var client = HttpClientTests.CreateClient(http, authenticate: true);

        await Assert.ThrowsAsync<FreeboxApiException>(() => client.AirMedia.SendToReceiverAsync("TV",
            new AirMediaReceiverRequest(AirMediaAction.Start, AirMediaMediaType.Video,
                "https://media.example/video.mp4")));
        Assert.Single(handler.Requests, x => x.Address.AbsolutePath.Contains("/airmedia/receivers/", StringComparison.Ordinal));

        Assert.True((await client.AirMedia.GetConfigurationAsync()).Enabled);
        Assert.Equal(2, handler.Requests.Count(x => x.Address.AbsolutePath.EndsWith("/login/session/", StringComparison.Ordinal)));
    }

    [Fact]
    public async Task ClientsSharingHttpClientKeepTheirSessionsIsolated()
    {
        using var handler = new FixtureHandler((request, _) =>
        {
            if (request.Address.AbsolutePath.EndsWith("/login/session/", StringComparison.Ordinal))
            {
                using var body = JsonDocument.Parse(request.Body!);
                var session = body.RootElement.GetProperty("app_id").GetString() == "org.fixture.a" ? "session-a" : "session-b";
                return Task.FromResult(FixtureHandler.Json(
                    "{\"success\":true,\"result\":{\"session_token\":\"" + session + "\",\"challenge\":\"next\",\"permissions\":{\"settings\":true}}}"));
            }
            return Task.FromResult(Respond(request));
        });
        using var http = new HttpClient(handler);
        var first = Client("org.fixture.a");
        var second = Client("org.fixture.b");

        await first.AirMedia.GetConfigurationAsync();
        await second.AirMedia.GetConfigurationAsync();
        await first.AirMedia.GetConfigurationAsync();

        Assert.Equal(new[] { "session-a", "session-b", "session-a" }, handler.Requests
            .Where(x => x.Address.AbsolutePath.Contains("airmedia", StringComparison.Ordinal))
            .Select(x => x.SessionToken!).ToArray());
        Assert.False(http.DefaultRequestHeaders.Contains("X-Fbx-App-Auth"));

        FreeboxClient Client(string appId) => new(http, new FreeboxClientOptions
        {
            ServerAddress = new Uri("https://fixture.example/"),
            ApiVersion = 16,
            ApplicationId = appId,
            AppToken = "fixture-app-token"
        });
    }

    [Fact]
    public async Task MissingCredentialsFailBeforeAnyProtectedRequest()
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        var client = new FreeboxClient(http, new FreeboxClientOptions
        {
            ServerAddress = new Uri("https://fixture.example/"),
            ApiVersion = 16
        });

        await Assert.ThrowsAsync<FreeboxAuthenticationException>(() => client.AirMedia.GetConfigurationAsync());

        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task EnrollmentUsesPostBodyAndTracksAcceptedStatus()
    {
        using var handler = new FixtureHandler((request, _) => Task.FromResult(FixtureHandler.Json(
            request.Method == HttpMethod.Post
                ? """{"success":true,"result":{"app_token":"fixture-app-token","track_id":42}}"""
                : """{"success":true,"result":{"status":"granted","challenge":"fixture-challenge"}}""")));
        using var http = new HttpClient(handler);
        var client = HttpClientTests.CreateClient(http);

        var authorization = await client.Authentication.AuthorizeAsync(
            new TokenRequest("org.fixture", "Fixture", "1.0", "Cloud"));
        var track = await client.Authentication.TrackAuthorizationAsync(authorization.TrackId);

        Assert.Equal(42, authorization.TrackId);
        Assert.Equal("fixture-app-token", authorization.AppToken);
        Assert.Equal(AuthorizationTrackEnum.Granted, track.Status);
        Assert.Collection(handler.Requests,
            request =>
            {
                Assert.Equal(HttpMethod.Post, request.Method);
                Assert.Equal("/api/v16/login/authorize/", request.Address.AbsolutePath);
                Assert.Null(request.SessionToken);
                using var body = JsonDocument.Parse(request.Body!);
                Assert.Equal("org.fixture", body.RootElement.GetProperty("app_id").GetString());
                Assert.Equal("Cloud", body.RootElement.GetProperty("device_name").GetString());
            },
            request =>
            {
                Assert.Equal(HttpMethod.Get, request.Method);
                Assert.Equal("/api/v16/login/authorize/42/", request.Address.AbsolutePath);
                Assert.Null(request.SessionToken);
            });
    }

    [Fact]
    public async Task LogoutUsesSessionHeaderThenClearsLocalSession()
    {
        using var handler = new FixtureHandler((request, _) => Task.FromResult(
            request.Address.AbsolutePath.EndsWith("/login/logout/", StringComparison.Ordinal)
                ? FixtureHandler.Json("""{"success":true}""") : Respond(request)));
        using var http = new HttpClient(handler);
        var client = HttpClientTests.CreateClient(http, authenticate: true);

        await client.AirMedia.GetConfigurationAsync();
        await client.Authentication.CloseSessionAsync();
        await client.AirMedia.GetConfigurationAsync();

        var logout = Assert.Single(handler.Requests, x => x.Address.AbsolutePath.EndsWith("/login/logout/", StringComparison.Ordinal));
        Assert.Equal(HttpMethod.Post, logout.Method);
        Assert.Equal("fixture-session", logout.SessionToken);
        Assert.Equal(2, handler.Requests.Count(x => x.Address.AbsolutePath.EndsWith("/login/session/", StringComparison.Ordinal)));
    }

    private static HttpResponseMessage Respond(CapturedRequest request)
    {
        var path = request.Address.AbsolutePath;
        if (path == "/api_version") return FixtureHandler.Json(HttpClientTests.DiscoveryJson);
        if (path.EndsWith("/login/", StringComparison.Ordinal))
            return FixtureHandler.Json("""{"success":true,"result":{"logged_in":false,"challenge":"fixture-challenge"}}""");
        if (path.EndsWith("/login/session/", StringComparison.Ordinal))
            return FixtureHandler.Json("""{"success":true,"result":{"session_token":"fixture-session","challenge":"next-challenge","permissions":{"settings":true}}}""");
        return FixtureHandler.Json("""{"success":true,"result":{"enabled":true}}""");
    }
}
