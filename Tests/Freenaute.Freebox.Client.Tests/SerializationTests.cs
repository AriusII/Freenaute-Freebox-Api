using System.Text.Json;
using Freenaute.Freebox.Mapper.ClientSide.Api.AirMedia;
using Freenaute.Freebox.Mapper.ClientSide.Authentication.Login;
using Freenaute.Freebox.Mapper.Common.Types;
using Freenaute.Freebox.Mapper.Serialization;
using Freenaute.Freebox.Mapper.ServerSide.General;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class SerializationTests
{
    [Fact]
    public void GeneratedMetadataWorksWithReflectionDisabled()
    {
        Assert.False(JsonSerializer.IsReflectionEnabledByDefault);
        var request = new TokenRequest("org.fixture", "Fixture", "1.0", "Test device");
        using var json = JsonDocument.Parse(JsonSerializer.Serialize(request,
            FreeboxJsonSerializerContext.Default.TokenRequest));

        Assert.Equal("org.fixture", json.RootElement.GetProperty("app_id").GetString());
        Assert.Equal("Fixture", json.RootElement.GetProperty("app_name").GetString());
        Assert.Equal("1.0", json.RootElement.GetProperty("app_version").GetString());
        Assert.Equal("Test device", json.RootElement.GetProperty("device_name").GetString());
        Assert.Equal(4, json.RootElement.EnumerateObject().Count());
    }

    [Theory]
    [InlineData(AirMediaAction.Start, AirMediaMediaType.Video, "start", "video")]
    [InlineData(AirMediaAction.Stop, AirMediaMediaType.Photo, "stop", "photo")]
    public void AirMediaUsesWireStringValuesAndOmitsOptionalNulls(
        AirMediaAction action, AirMediaMediaType type, string wireAction, string wireType)
    {
        var request = new AirMediaReceiverRequest(action, type, "https://media.example/video.mp4");
        using var json = JsonDocument.Parse(JsonSerializer.Serialize(request,
            FreeboxJsonSerializerContext.Default.AirMediaReceiverRequest));

        Assert.Equal(wireAction, json.RootElement.GetProperty("action").GetString());
        Assert.Equal(wireType, json.RootElement.GetProperty("media_type").GetString());
        Assert.False(json.RootElement.TryGetProperty("password", out _));
        Assert.False(json.RootElement.TryGetProperty("position", out _));
    }

    [Fact]
    public void DiscoveryReadsBoxModelAndIgnoresUnknownFutureFields()
    {
        var discovery = JsonSerializer.Deserialize(
            """
            {"uid":"fixture","device_name":"Freebox Server","box_model":"fbxgw9-r1/full",
             "box_model_name":"Freebox Ultra","api_version":"16.0","api_domain":"fixture.example",
             "api_base_url":"/api/","https_available":true,"https_port":443,
             "future_field":{"enabled":true}}
            """, FreeboxJsonSerializerContext.Default.ApiVersionResponse);

        Assert.NotNull(discovery);
        Assert.Equal("fbxgw9-r1/full", discovery.BoxModel);
        Assert.Equal(BoxModels.FreeboxV9R1, discovery.KnownBoxModel);
        Assert.Equal("https://fixture.example/api/v16/", discovery.ApiUrl);
        Assert.Equal("https://fixture.example/api/v16/airmedia/config/",
            new Uri(discovery.ApiUri, "airmedia/config/").AbsoluteUri);
    }

    [Theory]
    [InlineData("/api", true, 443, "https://fixture.example/api/v16/")]
    [InlineData("/api/", true, 443, "https://fixture.example/api/v16/")]
    [InlineData("//api///", true, 8443, "https://fixture.example:8443/api/v16/")]
    [InlineData("/api/", false, 443, "http://fixture.example/api/v16/")]
    [InlineData("/custom/api/", true, 443, "https://fixture.example/custom/api/v16/")]
    public void ApiAddressNormalizesPathAndRetainsVersionForRelativeRequests(
        string basePath, bool https, int port, string expected)
    {
        var discovery = new ApiVersionResponse("fixture", "Server", BoxModels.FreeboxV9R1,
            "Ultra", "16.0", "fixture.example", basePath, https, port);

        Assert.Equal(expected, discovery.ApiUrl);
        Assert.Equal(expected + "system/", new Uri(discovery.ApiUri, "system/").AbsoluteUri);
    }

    [Fact]
    public void GeneratedContractsOmitRemovedAndComputedFields()
    {
        var discovery = new ApiVersionResponse("fixture", "Server", BoxModels.FreeboxV9R1,
            "Ultra", "16.0", "fixture.example", "/api/", true, 443);
        using var version = JsonDocument.Parse(JsonSerializer.Serialize(discovery,
            FreeboxJsonSerializerContext.Default.ApiVersionResponse));
        Assert.False(version.RootElement.TryGetProperty("ApiUrl", out _));
        Assert.False(version.RootElement.TryGetProperty("ApiUri", out _));
        Assert.False(version.RootElement.TryGetProperty("device_type", out _));

        var session = JsonSerializer.Deserialize(
            """{"session_token":"fixture-session","challenge":"next","permissions":{"settings":true,"parental":true}}""",
            FreeboxJsonSerializerContext.Default.SessionResponse);
        Assert.NotNull(session);
        Assert.True(session.PermissionsModel.Settings);
        Assert.False(session.PermissionsModel.Contacts);
        using var sessionJson = JsonDocument.Parse(JsonSerializer.Serialize(session,
            FreeboxJsonSerializerContext.Default.SessionResponse));
        Assert.False(sessionJson.RootElement.GetProperty("permissions").TryGetProperty("parental", out _));
    }

    [Fact]
    public void WebSocketContractsReadDynamicPayloadsWithoutReflection()
    {
        var notification = JsonSerializer.Deserialize(
            """{"action":"notification","success":true,"source":"system","event":"changed","result":{"value":42,"nested":[true,"text"]}}""",
            FreeboxJsonSerializerContext.Default.WebSocketNotification);
        Assert.NotNull(notification);
        Assert.Equal(42, notification.Result?.GetProperty("value").GetInt32());
        Assert.True(notification.Result?.GetProperty("nested")[0].GetBoolean());

        var response = JsonSerializer.Deserialize("""{"action":"subscribe","success":true}""",
            FreeboxJsonSerializerContext.Default.WebSocketResponse);
        Assert.NotNull(response);
        Assert.Null(response.RequestId);
        Assert.Null(response.Result);
        Assert.Null(response.ErrorCode);
    }
}
