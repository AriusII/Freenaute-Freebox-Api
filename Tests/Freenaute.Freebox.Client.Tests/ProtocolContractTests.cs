using System.Text.Json;
using Freenaute.Freebox.Mapper.ClientSide.Authentication.Login;
using Freenaute.Freebox.Mapper.ClientSide.WebSocket;
using Freenaute.Freebox.Mapper.Common.Types;
using Freenaute.Freebox.Mapper.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class ProtocolContractTests
{
    [Theory]
    [InlineData("fbxgw9-r1", BoxModels.FreeboxV9R1)]
    [InlineData("fbxgw9-r1/full", BoxModels.FreeboxV9R1)]
    [InlineData("fbxgw-future/custom", null)]
    public void DiscoveryPreservesRawIdentifiersAndClassifiesKnownModels(string rawModel, BoxModels? knownModel)
    {
        var discovery = JsonSerializer.Deserialize(
            $$"""{"box_model":"{{rawModel}}","api_version":"16.0","api_base_url":"/api/"}""",
            FreeboxJsonSerializerContext.Default.ApiVersionResponse);

        Assert.NotNull(discovery);
        Assert.Equal(rawModel, discovery.BoxModel);
        Assert.Equal(knownModel, discovery.KnownBoxModel);
        using var serialized = JsonDocument.Parse(JsonSerializer.Serialize(discovery,
            FreeboxJsonSerializerContext.Default.ApiVersionResponse));
        Assert.Equal(rawModel, serialized.RootElement.GetProperty("box_model").GetString());
        Assert.False(serialized.RootElement.TryGetProperty("KnownBoxModel", out _));
    }

    [Fact]
    public async Task PartialRemoteDiscoveryUsesConfiguredHttpsOriginWithoutInventingMetadata()
    {
        const string partial = """{"api_version":"16.0","api_base_url":"/api/"}""";
        using var handler = FixtureHandler.Authenticated((request, _) => Task.FromResult(FixtureHandler.Json(
            request.Address.AbsolutePath == "/api_version" ? partial :
                """{"success":true,"result":{"enabled":true}}""")));
        using var http = new HttpClient(handler);
        using var client = new FreeboxClient(http, new FreeboxClientOptions()
            .UseServer(new Uri("https://configured.example:8443/"))
            .UseCredentials("org.fixture", "fixture-app-token"));

        var discovery = await client.Discovery.GetApiVersionAsync();
        Assert.Null(discovery.Uid);
        Assert.Null(discovery.DeviceName);
        Assert.Null(discovery.BoxModel);
        Assert.Null(discovery.BoxModelName);
        Assert.Null(discovery.KnownBoxModel);
        Assert.Null(discovery.ApiDomain);
        Assert.Null(discovery.HttpsAvailable);
        Assert.Null(discovery.HttpsPort);
        Assert.Throws<ArgumentNullException>(() => discovery.ApiUri);
        Assert.True((await client.AirMedia.GetConfigurationAsync()).Enabled);
        Assert.Equal("https://configured.example:8443/api/v16/airmedia/config/", handler.Requests.Last().Address.AbsoluteUri);
        Assert.Single(handler.Requests, request => request.Address.AbsolutePath == "/api_version");
    }

    [Fact]
    public void MissingHttpsMetadataCannotBePresentedAsAnHttpAddress()
    {
        var discovery = JsonSerializer.Deserialize(
            """{"api_version":"16.0","api_base_url":"/api/","api_domain":"fixture.example"}""",
            FreeboxJsonSerializerContext.Default.ApiVersionResponse);

        Assert.NotNull(discovery);
        Assert.Throws<InvalidOperationException>(() => discovery.ApiUri);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("1.2.3")]
    public async Task DependencyInjectionEmitsApplicationVersionOnlyWhenConfigured(string? version)
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"enabled":true}}""");
        var services = new ServiceCollection();
        services.AddFreeboxClient(options =>
        {
            options.ApiVersion = 16;
            options.UseServer(new Uri("https://fixture.example/"))
                .UseCredentials("org.fixture", "fixture-app-token");
            if (version is not null) options.WithApplicationVersion(version);
        }).ConfigurePrimaryHttpMessageHandler(() => handler);
        using var provider = services.BuildServiceProvider();

        await provider.GetRequiredService<IFreeboxClient>().AirMedia.GetConfigurationAsync();

        var session = Assert.Single(handler.Requests, request => request.Address.AbsolutePath.EndsWith("/login/session/"));
        using var body = JsonDocument.Parse(session.Body!);
        Assert.Equal("8951185163485b44ef71e6f92c2e872f7817f13c", body.RootElement.GetProperty("password").GetString());
        Assert.Equal(version is not null, body.RootElement.TryGetProperty("app_version", out var wireVersion));
        if (version is not null) Assert.Equal(version, wireVersion.GetString());
    }

    [Fact]
    public void SessionStartGeneratedMetadataOmitsUnconfiguredApplicationVersion()
    {
        using var json = JsonDocument.Parse(JsonSerializer.Serialize(new SessionStartRequest("org.fixture", "fixture-hmac"),
            FreeboxJsonSerializerContext.Default.SessionStartRequest));

        Assert.False(json.RootElement.TryGetProperty("app_version", out _));
    }

    [Fact]
    public void WebSocketCorrelationUsesRequestIdWireFieldInBothDirections()
    {
        using var request = JsonDocument.Parse(JsonSerializer.Serialize(new WebSocketRequest(17, "ping"),
            FreeboxJsonSerializerContext.Default.WebSocketRequest));
        Assert.Equal(17, request.RootElement.GetProperty("request_id").GetInt32());
        Assert.False(request.RootElement.TryGetProperty("req_id", out _));

        var response = JsonSerializer.Deserialize(
            """{"request_id":17,"action":"ping","success":true,"result":{"response":"pong"}}""",
            FreeboxJsonSerializerContext.Default.WebSocketResponse);
        Assert.NotNull(response);
        Assert.Equal(17, response.RequestId);
        Assert.Equal("pong", response.Result?.GetProperty("response").GetString());
        using var optional = JsonDocument.Parse(JsonSerializer.Serialize(new WebSocketRequest(null, "ping"),
            FreeboxJsonSerializerContext.Default.WebSocketRequest));
        Assert.False(optional.RootElement.TryGetProperty("request_id", out _));
    }

    [Fact]
    public void CameraPermissionIsExplicitAndMissingPermissionsDenyAccess()
    {
        var session = JsonSerializer.Deserialize(
            """{"session_token":"fixture","challenge":"fixture","permissions":{"camera":true,"parental":true}}""",
            FreeboxJsonSerializerContext.Default.SessionResponse);
        Assert.NotNull(session);
        Assert.True(session.PermissionsModel.Camera);
        Assert.False(session.PermissionsModel.Settings);
        Assert.False(session.PermissionsModel.Contacts);
        Assert.False(session.PermissionsModel.Calls);
        Assert.False(session.PermissionsModel.Explorer);
        Assert.False(session.PermissionsModel.Downloader);
        Assert.False(session.PermissionsModel.Pvr);
        Assert.False(session.PermissionsModel.Profile);
        using var json = JsonDocument.Parse(JsonSerializer.Serialize(session, FreeboxJsonSerializerContext.Default.SessionResponse));
        var permissions = json.RootElement.GetProperty("permissions");
        Assert.True(permissions.GetProperty("camera").GetBoolean());
        Assert.False(permissions.TryGetProperty("parental", out _));
    }
}
