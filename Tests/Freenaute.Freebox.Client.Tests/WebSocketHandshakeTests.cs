using System.Net;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class WebSocketHandshakeTests
{
    [Fact]
    public async Task WebSocketUpgradeUsesConfiguredInvokerOriginAndSession()
    {
        using var handler = new UpgradeHandler();
        using var http = new HttpClient(handler);
        using var client = new FreeboxClient(http, new FreeboxClientOptions
        {
            ServerAddress = new Uri("https://fixture.example:8443/"),
            ApiVersion = 16,
            ApplicationId = "org.fixture",
            AppToken = "fixture-token"
        });
        using var connection = await client.WebSocketTransport.ConnectAsync("ws/event");
        Assert.Equal("fixture.example", handler.UpgradeAddress!.Host);
        Assert.Equal(8443, handler.UpgradeAddress.Port);
        Assert.Equal("/api/v16/ws/event", handler.UpgradeAddress.AbsolutePath);
        Assert.Equal("fixture-session", handler.SessionToken);
        Assert.Equal(1, handler.Upgrades);
    }

    [Theory]
    [InlineData(HttpStatusCode.Unauthorized)]
    [InlineData(HttpStatusCode.Forbidden)]
    public async Task RejectedHandshakeInvalidatesSessionWithoutReplayingIt(HttpStatusCode status)
    {
        using var handler = new UpgradeHandler(status);
        using var http = new HttpClient(handler);
        using var client = new FreeboxClient(http, new FreeboxClientOptions
        {
            ServerAddress = new Uri("https://fixture.example/"),
            ApiVersion = 16,
            ApplicationId = "org.fixture",
            AppToken = "fixture-token"
        });
        await Assert.ThrowsAsync<System.Net.WebSockets.WebSocketException>(() => client.WebSocketTransport.ConnectAsync("ws/event"));
        Assert.Equal(1, handler.Upgrades);
        Assert.Equal(1, handler.SessionRequests);
        await client.Authentication.OpenSessionAsync();
        Assert.Equal(2, handler.SessionRequests);
        Assert.Equal(1, handler.Upgrades);
    }

    private sealed class UpgradeHandler(HttpStatusCode upgradeStatus = HttpStatusCode.SwitchingProtocols) : HttpMessageHandler
    {
        public Uri? UpgradeAddress { get; private set; }
        public string? SessionToken { get; private set; }
        public int Upgrades { get; private set; }
        public int SessionRequests { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri!.AbsolutePath.EndsWith("/login/", StringComparison.Ordinal))
                return Task.FromResult(FixtureHandler.Json("""{"success":true,"result":{"challenge":"fixture"}}"""));
            if (request.RequestUri.AbsolutePath.EndsWith("/login/session/", StringComparison.Ordinal))
            {
                SessionRequests++;
                return Task.FromResult(FixtureHandler.Json("""{"success":true,"result":{"session_token":"fixture-session","permissions":{}}}"""));
            }
            UpgradeAddress = request.RequestUri;
            SessionToken = request.Headers.GetValues("X-Fbx-App-Auth").Single();
            Upgrades++;
            var key = request.Headers.GetValues("Sec-WebSocket-Key").Single();
            var accept = Convert.ToBase64String(SHA1.HashData(Encoding.ASCII.GetBytes(key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11")));
            var response = new HttpResponseMessage(upgradeStatus)
            {
                Version = HttpVersion.Version11,
                Content = new DuplexContent()
            };
            response.Headers.TryAddWithoutValidation("Connection", "Upgrade");
            response.Headers.TryAddWithoutValidation("Upgrade", "websocket");
            response.Headers.TryAddWithoutValidation("Sec-WebSocket-Accept", accept);
            return Task.FromResult(response);
        }
    }

    private sealed class DuplexContent : HttpContent
    {
        protected override Stream CreateContentReadStream(CancellationToken cancellationToken) => new MemoryStream();
        protected override Task<Stream> CreateContentReadStreamAsync() => Task.FromResult<Stream>(new MemoryStream());
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) => Task.CompletedTask;
        protected override bool TryComputeLength(out long length) { length = 0; return false; }
    }
}
