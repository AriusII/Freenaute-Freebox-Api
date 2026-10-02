using System.Text.Json;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Contracts.Protocol;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class ProtocolDomainTests
{
    [Fact]
    public async Task CameraSelectionPreservesOpaqueIdAndStreamAddress()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""
            {"success":true,"result":{"id":"012345678901","node_id":0,"name":"Salon",
            "stream_url":"/camera/stream/012345678901/stream.m3u8","lan_gid":"ether-3c:98:72:fa:36:15"}}
            """);
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var camera = await client.Cameras.Camera("012345678901").GetAsync();
        Assert.Equal("012345678901", camera.Id);
        Assert.Equal("/camera/stream/012345678901/stream.m3u8", camera.StreamUrl);
        Assert.EndsWith("/api/v16/camera/012345678901", handler.Requests.Last().Address.AbsoluteUri);
        Assert.All(handler.Requests, request => Assert.Equal("fixture.example", request.Address.Host));
    }

    [Theory]
    [InlineData("{\"id\":\"001\",\"type\":\"future\"}", JsonValueKind.Object)]
    [InlineData("[{\"id\":\"001\",\"type\":\"future\"}]", JsonValueKind.Array)]
    public async Task NotificationDetailPreservesBothAttestedShapes(string result, JsonValueKind expectedShape)
    {
        using var handler = FixtureHandler.AuthenticatedConstant("{\"success\":true,\"result\":" + result + "}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var detail = await client.Notifications.Target("001").GetAsync();
        var json = JsonSerializer.Serialize(detail,
            Freenaute.Freebox.Mapper.Serialization.ProtocolJsonSerializerContext.Default.ObjectOrArrayNotificationTarget);
        using var roundTrip = JsonDocument.Parse(json);
        Assert.Equal(expectedShape, roundTrip.RootElement.ValueKind);
        Assert.Equal("001", detail.Items.Single().Id);
        Assert.Equal("future", detail.Items.Single().Type);
    }

    [Fact]
    public async Task NotificationConfigurationDoesNotSendCredentialsToCallbackOrigin()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        await client.Notifications.CreateTargetAsync(new NotificationTargetWrite
        {
            Name = "Application",
            Type = "firebase",
            Token = "test-device-token",
            ApiUrl = "https://notifications.example/application",
            MessageType = "data",
            Subscriptions = ["phone", "download"]
        });
        var request = handler.Requests.Last();
        Assert.Equal("https://fixture.example/api/v16/notif/targets/", request.Address.AbsoluteUri);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.Equal("https://notifications.example/application", body.RootElement.GetProperty("api_url").GetString());
        Assert.All(handler.Requests, capture => Assert.Equal("fixture.example", capture.Address.Host));
    }
}
