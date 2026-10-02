using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Client;
using Freenaute.Freebox.Mapper.Common.Types;
using Freenaute.Freebox.Mapper.ClientSide.WebSocket;
using Freenaute.Freebox.Mapper.Serialization;
using Microsoft.Extensions.DependencyInjection;

if (JsonSerializer.IsReflectionEnabledByDefault)
{
    throw new InvalidOperationException("This example requires generated JSON metadata.");
}

// Exercise generated protocol contracts and production root parsing in the native executable.
// These are controlled fixture checks, not claims of compatibility with physical Freebox hardware.
using var tlsHandler = FreeboxTls.CreateHandler();
var unknown = JsonSerializer.Deserialize(
    """{"box_model":"fbxgw-future/custom","api_version":"16.0","api_base_url":"/api/"}""",
    FreeboxJsonSerializerContext.Default.ApiVersionResponse)!;
Require(unknown.BoxModel == "fbxgw-future/custom" && unknown.KnownBoxModel is null,
    "Unknown box models must remain available as raw identifiers.");
using (var roundTrip = JsonDocument.Parse(JsonSerializer.Serialize(unknown,
    FreeboxJsonSerializerContext.Default.ApiVersionResponse)))
{
    Require(roundTrip.RootElement.GetProperty("box_model").GetString() == "fbxgw-future/custom",
        "Unknown discovery identifiers must round-trip without reflection.");
}
var partial = JsonSerializer.Deserialize("""{"api_version":"16.0","api_base_url":"/api/"}""",
    FreeboxJsonSerializerContext.Default.ApiVersionResponse)!;
Require(partial.BoxModel is null && partial.ApiDomain is null && partial.HttpsAvailable is null && partial.HttpsPort is null,
    "Remote discovery must retain absent metadata.");
using (var socketRequest = JsonDocument.Parse(JsonSerializer.Serialize(new WebSocketRequest(17, "ping"),
    FreeboxJsonSerializerContext.Default.WebSocketRequest)))
{
    Require(socketRequest.RootElement.GetProperty("request_id").GetInt32() == 17 &&
        !socketRequest.RootElement.TryGetProperty("req_id", out _), "WebSocket correlation uses request_id.");
}
var socketResponse = JsonSerializer.Deserialize("""{"request_id":17,"action":"ping","success":true}""",
    FreeboxJsonSerializerContext.Default.WebSocketResponse)!;
Require(socketResponse.RequestId == 17, "WebSocket responses must retain correlation.");
var permissions = JsonSerializer.Deserialize(
    """{"session_token":"fixture","challenge":"fixture","permissions":{"camera":true}}""",
    FreeboxJsonSerializerContext.Default.SessionResponse)!;
Require(permissions.PermissionsModel.Camera && !permissions.PermissionsModel.Settings,
    "Camera permission must be read and missing permissions must deny access.");

var services = new ServiceCollection();
var fixture = new FixtureHandler();
services.AddFreeboxClient(options => options
        .UseServer(new Uri("https://fixture.example/"))
        .UseCredentials("org.fixture", "fixture-app-token")
        .WithApplicationVersion("1.0.0"))
    .ConfigurePrimaryHttpMessageHandler(() => fixture);
using var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<IFreeboxClient>();

var discovery = await client.Discovery.GetApiVersionAsync();
Require(discovery.BoxModel == "fbxgw9-r1" && discovery.KnownBoxModel == BoxModels.FreeboxV9R1,
    "Discovery must preserve the raw SDK 16 model identifier and its known classification.");
Require(discovery.ApiUrl == "https://advertised.example/api/v16/", "The advertised address was not normalized.");

var configuration = await client.AirMedia.GetConfigurationAsync();
Require(configuration.Enabled, "The AirMedia configuration was not read correctly.");
var receivers = await client.AirMedia.GetReceiversAsync();
Require(receivers is [{ Name: "Séjour", Capabilities.CanPlayVideo: true }], "The receiver capabilities were not read correctly.");

await client.AirMedia.Receiver("Séjour")
    .PlayVideo("https://media.example/video.mp4")
    .AtPercent(50.123m)
    .SendAsync();

await client.AirMedia.Receiver("Séjour").StopVideo().SendAsync();

await DomainSmoke.RunAsync(client);

// Consumers can extend the typed transport using their own generated contracts.
var result = await client.Transport.SendAsync(HttpMethod.Put, "fixture/",
    new FixtureCommand("AOT"), FixtureJsonContext.Default.FixtureCommand,
    FixtureJsonContext.Default.FixtureResult);
Require(result.Accepted, "Consumer generated JSON metadata did not work.");

// DI facades share the session, while authentication headers stay on individual requests.
var secondClient = provider.GetRequiredService<IFreeboxClient>();
await secondClient.AirMedia.GetConfigurationAsync();
Require(fixture.DiscoveryRequests == 1, "Discovery should be shared by DI facades.");
Require(fixture.SessionRequests == 1, "Authentication should be shared by DI facades.");
Require(fixture.ProtectedRequests == 6 + DomainSmoke.ProtectedRequests, "Unexpected authenticated fixture request count.");

Console.WriteLine("PASS: 17 protected HTTP fixtures; reflection-free JSON, DI/session, AirMedia, Network, Services, Files, SystemHome, Protocol, typed unions/patches and WebSocket control metadata.");

static void Require(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}

internal sealed class FixtureHandler : HttpMessageHandler
{
    private readonly DomainFixture domainFixture = new();

    public int DiscoveryRequests { get; private set; }
    public int SessionRequests { get; private set; }
    public int ProtectedRequests { get; private set; }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var path = request.RequestUri!.AbsolutePath;
        if (request.RequestUri.Scheme != "https" || request.RequestUri.Host != "fixture.example")
            throw new InvalidOperationException("Discovery must retain the configured HTTP origin.");
        var authenticated = request.Headers.TryGetValues("X-Fbx-App-Auth", out var values);

        if (path == "/api_version")
        {
            if (authenticated) throw new InvalidOperationException("Discovery must not include a session token.");
            DiscoveryRequests++;
            return Json("""
                {"uid":"fixture","device_name":"Server","box_model":"fbxgw9-r1",
                 "box_model_name":"Ultra","api_version":"16.0","api_domain":"advertised.example",
                 "api_base_url":"/api/","https_available":true,"https_port":443}
                """);
        }

        if (path == "/api/v16/login/")
        {
            if (authenticated) throw new InvalidOperationException("Login must not include a session token.");
            return Json("""{"success":true,"result":{"logged_in":false,"challenge":"fixture-challenge"}}""");
        }

        if (path == "/api/v16/login/session/")
        {
            if (authenticated) throw new InvalidOperationException("Opening a session must not include a session token.");
            using var body = JsonDocument.Parse(await request.Content!.ReadAsStringAsync(cancellationToken));
            if (request.Method != HttpMethod.Post ||
                body.RootElement.GetProperty("app_id").GetString() != "org.fixture" ||
                body.RootElement.GetProperty("app_version").GetString() != "1.0.0" ||
                body.RootElement.GetProperty("password").GetString() != "8951185163485b44ef71e6f92c2e872f7817f13c")
                throw new InvalidOperationException("The HMAC session request is invalid.");
            SessionRequests++;
            return Json("""{"success":true,"result":{"session_token":"fixture-session","challenge":"next","permissions":{"settings":true}}}""");
        }

        if (!authenticated || values!.Single() != "fixture-session")
            throw new InvalidOperationException("The protected request has no valid session token.");
        ProtectedRequests++;

        if (path == "/api/v16/airmedia/config/" && request.Method == HttpMethod.Get)
            return Json("""{"success":true,"result":{"enabled":true}}""");

        if (path == "/api/v16/airmedia/receivers/" && request.Method == HttpMethod.Get)
            return Json("""{"success":true,"result":[{"name":"Séjour","password_protected":false,"capabilities":{"photo":true,"audio":false,"video":true,"screen":false}}]}""");

        if (path.StartsWith("/api/v16/airmedia/receivers/", StringComparison.Ordinal) && request.Method == HttpMethod.Post)
        {
            if (request.RequestUri.AbsoluteUri != "https://fixture.example/api/v16/airmedia/receivers/S%C3%A9jour/")
                throw new InvalidOperationException("The receiver name was not escaped.");
            using var body = JsonDocument.Parse(await request.Content!.ReadAsStringAsync(cancellationToken));
            if (body.RootElement.GetProperty("media_type").GetString() != "video")
                throw new InvalidOperationException("The AirMedia media type is invalid.");
            var action = body.RootElement.GetProperty("action").GetString();
            if (action == "start")
            {
                if (body.RootElement.GetProperty("position").GetInt32() != 50_123 ||
                    body.RootElement.GetProperty("media").GetString() != "https://media.example/video.mp4")
                    throw new InvalidOperationException("The AirMedia start request has invalid media or percentage units.");
            }
            else if (action == "stop")
            {
                if (body.RootElement.TryGetProperty("media", out _) || body.RootElement.EnumerateObject().Count() != 2)
                    throw new InvalidOperationException("The AirMedia stop request must omit media.");
            }
            else throw new InvalidOperationException("The AirMedia action is invalid.");
            return Json("""{"success":true}""");
        }

        if (path == "/api/v16/fixture/" && request.Method == HttpMethod.Put)
        {
            var body = await request.Content!.ReadAsStringAsync(cancellationToken);
            var command = JsonSerializer.Deserialize(body, FixtureJsonContext.Default.FixtureCommand);
            if (command?.Value != "AOT") throw new InvalidOperationException("The generated request metadata failed.");
            return Json("""{"success":true,"result":{"accepted":true}}""");
        }

        var domainResponse = await domainFixture.TryRespondAsync(request, cancellationToken);
        if (domainResponse is not null) return domainResponse;

        throw new InvalidOperationException("Unexpected fixture route or HTTP method.");
    }

    internal static HttpResponseMessage Json(string body) => new(HttpStatusCode.OK)
    {
        Content = new StringContent(body, Encoding.UTF8, "application/json")
    };
}

internal sealed record FixtureCommand([property: JsonPropertyName("value")] string Value);
internal sealed record FixtureResult([property: JsonPropertyName("accepted")] bool Accepted);

[JsonSerializable(typeof(FixtureCommand))]
[JsonSerializable(typeof(FixtureResult))]
internal partial class FixtureJsonContext : JsonSerializerContext;
