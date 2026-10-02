using System.Text.Json;
using Freenaute.Freebox.Client.Domains.Network;
using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class NetworkApiTests
{
    [Fact]
    public async Task DhcpPatchPreservesExplicitFalseAndStringValuedOptions()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"enabled":false}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var network = new FreeboxNetworkApi(client.Transport);

        var result = await network.Dhcp.UpdateConfigurationAsync(new DhcpConfigurationPatch
        {
            Enabled = false,
            Options = new DhcpOptionWrite[] { new() { Id = "tcp_ttl", Val = "0" } }
        });

        var request = handler.Requests.Last();
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal("/api/v16/dhcp/config/", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.False(body.RootElement.GetProperty("enabled").GetBoolean());
        Assert.Equal("0", body.RootElement.GetProperty("options")[0].GetProperty("val").GetString());
        Assert.Equal(2, body.RootElement.EnumerateObject().Count());
        Assert.False(result.Enabled);
    }

    [Fact]
    public async Task AccessPointPatchKeepsNestedFalseAndZeroWithoutOtherConfigurationFields()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"id":0,"config":{"channel_width":"20"},"capabilities":{"2d4g":{"n":true,"future":false}}}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var network = new FreeboxNetworkApi(client.Transport);

        var result = await network.Wifi.AccessPoint(0).UpdateAsync(new WifiAccessPointPatch
        {
            Config = new WifiApConfigurationPatch
            {
                PrimaryChannel = 0,
                Ht = new WifiApHtConfigurationPatch { AcEnabled = false }
            }
        });

        var request = handler.Requests.Last();
        Assert.Equal("/api/v16/wifi/ap/0", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        var configuration = body.RootElement.GetProperty("config");
        Assert.Equal(0, configuration.GetProperty("primary_channel").GetInt64());
        Assert.False(configuration.GetProperty("ht").GetProperty("ac_enabled").GetBoolean());
        Assert.Equal(2, configuration.EnumerateObject().Count());
        Assert.Single(configuration.GetProperty("ht").EnumerateObject());
        Assert.Equal("20", result.Config!.ChannelWidth!.Value.String);
        Assert.False(result.Capabilities!.Band2D4G!.Value.Flags["future"]);
    }

    [Fact]
    public async Task LanSelectorsRetainIndependentEncodedInterfaceAndHostIdentifiers()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"id":"fixture","l2ident":{"id":"00:11:22:33:44:55","type":"mac_address"}}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var network = new FreeboxNetworkApi(client.Transport);
        var lan = network.Lan.Interface("réseau &?");
        var first = lan.Host("host#1");
        var second = lan.Host("host%2");

        Assert.Empty(handler.Requests);
        var host = await first.GetAsync();
        await second.GetAsync();
        var requests = handler.Requests.Where(r => r.Address.AbsolutePath.Contains("/lan/", StringComparison.Ordinal)).ToArray();
        Assert.Equal("https://fixture.example/api/v16/lan/browser/r%C3%A9seau%20%26%3F/host%231/", requests[0].Address.AbsoluteUri);
        Assert.Equal("https://fixture.example/api/v16/lan/browser/r%C3%A9seau%20%26%3F/host%252/", requests[1].Address.AbsoluteUri);
        Assert.Equal(ObjectOrArrayKind.Object, host.L2Ident!.Value.Kind);
        Assert.Equal("mac_address", host.L2Ident.Value.Object.Type);
    }

    [Theory]
    [InlineData("{\"id\":\"02:00:00:00:00:08\",\"phy_id\":0}", StringOrIntegerKind.String, StringOrIntegerKind.Integer)]
    [InlineData("{\"id\":1,\"phy_id\":\"0\"}", StringOrIntegerKind.Integer, StringOrIntegerKind.String)]
    public async Task BssIdentifiersPreserveBothDocumentedKinds(string resultJson, StringOrIntegerKind idKind, StringOrIntegerKind phyKind)
    {
        using var handler = FixtureHandler.AuthenticatedConstant("{\"success\":true,\"result\":" + resultJson + "}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var network = new FreeboxNetworkApi(client.Transport);

        var bss = await network.Wifi.Bss("02:00:00:00:00:08").GetAsync();

        Assert.Equal(idKind, bss.Id!.Value.Kind);
        Assert.Equal(phyKind, bss.PhyId!.Value.Kind);
    }

    [Theory]
    [InlineData("{\"state\":\"future_state\"}", ObjectOrArrayKind.Object)]
    [InlineData("[{\"state\":\"future_state\"}]", ObjectOrArrayKind.Array)]
    public async Task GlobalWifiStatePreservesObjectOrArrayAndUnknownTokens(string json, ObjectOrArrayKind kind)
    {
        using var handler = FixtureHandler.AuthenticatedConstant("{\"success\":true,\"result\":" + json + "}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var state = await new FreeboxNetworkApi(client.Transport).Wifi.GetStateAsync();

        Assert.Equal(kind, state.Kind);
        Assert.Equal("future_state", state.GetSingle().State);
        using var roundtrip = JsonDocument.Parse(JsonSerializer.Serialize(state, NetworkJsonSerializerContext.Default.ObjectOrArrayWifiGlobalState));
        Assert.Equal(kind == ObjectOrArrayKind.Object ? JsonValueKind.Object : JsonValueKind.Array, roundtrip.RootElement.ValueKind);
    }

    [Fact]
    public async Task GuestCreateSendsDirectParametersAndRetainsStringMaxUseCount()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"id":11,"params":{"max_use_count":100}}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var result = await new FreeboxNetworkApi(client.Transport).Wifi.GuestAccess.CreateKeyAsync(new CreateWifiCustomKeyRequest
        {
            Description = "Guest",
            Key = "fixture-secret",
            MaxUseCount = StringOrInteger.FromString("100"),
            Duration = 0,
            AccessType = "net_only"
        });

        var request = handler.Requests.Last();
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v16/wifi/custom_key/", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.False(body.RootElement.TryGetProperty("params", out _));
        Assert.Equal("100", body.RootElement.GetProperty("max_use_count").GetString());
        Assert.Equal(0, body.RootElement.GetProperty("duration").GetInt64());
        Assert.Equal(100, result.Params!.MaxUseCount!.Value.Integer);
    }

    [Fact]
    public async Task JsonNullAndDocumentedGuestLimitFailBeforeAnyHttpRequest()
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var network = new FreeboxNetworkApi(client.Transport);

        await Assert.ThrowsAsync<ArgumentException>(() => network.Wifi.UpdateConfigurationAsync(new WifiGlobalConfigurationPatch
        {
            Enabled = Optional<bool>.Null
        }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => network.Wifi.GuestAccess.CreateKeyAsync(new CreateWifiCustomKeyRequest
        {
            MaxUseCount = StringOrInteger.FromInteger(128)
        }));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task RouteReplacementUsesArrayBodyAndPreservesDocumentedTrailingSlash()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":[{"prefix":"192.168.42.0/24","enabled":false}]}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var network = new FreeboxNetworkApi(client.Transport);

        await network.Lan.ReplaceRoutesAsync([new LanRouteWrite { Prefix = "192.168.42.0/24", Enabled = false }]);

        var request = handler.Requests.Last();
        Assert.Equal("/api/v16/lan/routes/", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.Equal(JsonValueKind.Array, body.RootElement.ValueKind);
        Assert.False(body.RootElement[0].GetProperty("enabled").GetBoolean());
    }

    [Fact]
    public async Task NetworkOperationPassesCancellationToTransport()
    {
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        using var handler = FixtureHandler.Authenticated(async (_, cancellationToken) =>
        {
            started.SetResult();
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            throw new InvalidOperationException("The canceled fixture must not finish normally.");
        });
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        using var cancellation = new CancellationTokenSource();
        var pending = new FreeboxNetworkApi(client.Transport).Wifi.AccessPoint(0).ScanNeighborsAsync(cancellation.Token);
        await started.Task.WaitAsync(TimeSpan.FromSeconds(5));
        cancellation.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => pending);
    }

    [Theory]
    [InlineData("true")]
    [InlineData("1.5")]
    [InlineData("{}")]
    public void NatPortUnionRejectsUnestablishedJsonKinds(string value)
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(
            "{\"wan_port_start\":" + value + "}", NetworkJsonSerializerContext.Default.PortForwardingConfig));
    }

    [Fact]
    public void SourceGeneratedContractsIgnoreDeprecatedBssFlagsAndRetainCurrentReplacement()
    {
        var bss = JsonSerializer.Deserialize("""{"use_shared_params":false,"status":{"is_main_bss":true},"config":{"use_default_config":true,"hide_ssid":false}}""",
            NetworkJsonSerializerContext.Default.WifiBss)!;
        using var serialized = JsonDocument.Parse(JsonSerializer.Serialize(bss, NetworkJsonSerializerContext.Default.WifiBss));

        Assert.False(bss.UseSharedParams);
        Assert.False(bss.Config!.HideSsid!.Value.Boolean);
        Assert.False(serialized.RootElement.GetProperty("status").TryGetProperty("is_main_bss", out _));
        Assert.False(serialized.RootElement.GetProperty("config").TryGetProperty("use_default_config", out _));
    }

    [Theory]
    [InlineData("[1,2]")]
    [InlineData("{\"n\":1}")]
    public void WifiCapabilitiesRejectShapesBeyondDocumentedIntegerOrBooleanMap(string value)
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(
            "{\"2d4g\":" + value + "}", NetworkJsonSerializerContext.Default.WifiApCapabilities));
    }
}
