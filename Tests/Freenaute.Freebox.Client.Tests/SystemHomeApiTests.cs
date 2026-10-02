using System.Collections.Immutable;
using System.Text.Json;
using Freenaute.Freebox.Client.Domains.SystemHome;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Contracts.SystemHome;
using Freenaute.Freebox.Mapper.Serialization;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class SystemHomeApiTests
{
    // Boolean/integer/null are shown by Home examples; float forms are synthetic boundaries
    // of the explicitly documented float UI contract. No server behavior is inferred.
    [Theory]
    [InlineData("null", HomeIoValueKind.Null)]
    [InlineData("true", HomeIoValueKind.Boolean)]
    [InlineData("false", HomeIoValueKind.Boolean)]
    [InlineData("38", HomeIoValueKind.Integer)]
    [InlineData("1.0", HomeIoValueKind.Float)]
    [InlineData("1e2", HomeIoValueKind.Float)]
    [InlineData("\"text\"", HomeIoValueKind.String)]
    public void HomeScalarRoundtripPreservesJsonKind(string wire, HomeIoValueKind kind)
    {
        Assert.False(JsonSerializer.IsReflectionEnabledByDefault);
        var value = JsonSerializer.Deserialize(wire, SystemHomeJsonSerializerContext.Default.HomeIoValue);
        Assert.NotNull(value);
        Assert.Equal(kind, value.Kind);
        Assert.Equal(wire, JsonSerializer.Serialize(value, SystemHomeJsonSerializerContext.Default.HomeIoValue));
    }

    [Fact]
    public void IntegralFloatFactoryDoesNotBecomeIntegerOnRoundtrip()
    {
        var wire = JsonSerializer.Serialize(HomeIoValue.FromFloat(1), SystemHomeJsonSerializerContext.Default.HomeIoValue);
        Assert.Equal("1.0", wire);
        Assert.Equal(HomeIoValueKind.Float,
            JsonSerializer.Deserialize(wire, SystemHomeJsonSerializerContext.Default.HomeIoValue)!.Kind);
    }

    [Fact]
    public void ExplicitNullAndAbsentHomeValueRemainDistinct()
    {
        var explicitNull = JsonSerializer.Deserialize("""{"value":null,"value_type":"void"}""",
            SystemHomeJsonSerializerContext.Default.HomeNodeEndpointValue);
        var absent = JsonSerializer.Deserialize("{}", SystemHomeJsonSerializerContext.Default.HomeNodeEndpointValue);
        Assert.Same(HomeIoValue.Null, explicitNull!.Value);
        Assert.Null(absent!.Value);
    }

    [Theory]
    [InlineData("{}")]
    [InlineData("[]")]
    [InlineData("9223372036854775808")]
    public void HomeScalarRejectsUndocumentedShapesAndIntegerOverflow(string wire) =>
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(wire,
            SystemHomeJsonSerializerContext.Default.HomeIoValue));

    [Fact]
    public async Task LcdPatchSendsFalseAndZeroWhileOmittingUnsetAndReadonlyFields()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"brightness":0}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var api = new FreeboxSystemHomeApi(client.Transport);
        await api.Lcd.Configure().UseFields(new LcdConfigPatch().WithBrightness(0).WithOrientationForced(false)).SendAsync();
        var request = handler.Requests.Last();
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal("/api/v16/lcd/config/", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.Equal(0, body.RootElement.GetProperty("brightness").GetInt32());
        Assert.False(body.RootElement.GetProperty("orientation_forced").GetBoolean());
        Assert.Equal(2, body.RootElement.EnumerateObject().Count());
        Assert.False(body.RootElement.TryGetProperty("available_led_strip_animations", out _));
        Assert.False(body.RootElement.TryGetProperty("hide_led", out _));
    }

    [Fact]
    public async Task UpdatingACommandLeavesItsOriginalFieldsUnchanged()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var original = new FreeboxSystemHomeApi(client.Transport).Lcd.WithBrightness(50);
        var modified = original.UseFields(original.Fields.WithHideWifiKey(false));
        Assert.Empty(handler.Requests);
        await modified.SendAsync();
        using var modifiedBody = JsonDocument.Parse(handler.Requests.Last().Body!);
        Assert.False(modifiedBody.RootElement.GetProperty("hide_wifi_key").GetBoolean());
        await original.SendAsync();
        using var originalBody = JsonDocument.Parse(handler.Requests.Last().Body!);
        Assert.False(originalBody.RootElement.TryGetProperty("hide_wifi_key", out _));
    }

    [Fact]
    public void PatchNullIsRejectedBeforeNetworkWhenNoClearSemanticsAreDocumented()
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var command = new FreeboxSystemHomeApi(client.Transport).Lcd.Configure()
            .UseFields(new() { HideWifiKey = Optional<bool>.Null });
        Assert.Throws<ArgumentException>(() => { _ = command.SendAsync(); });
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task HomeButtonTriggerSendsExplicitNull()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        await new FreeboxSystemHomeApi(client.Transport).Home.Node(14).Endpoint(1).Trigger().SendAsync();
        var request = handler.Requests.Last();
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal("/api/v16/home/endpoints/14/1", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.Equal(JsonValueKind.Null, body.RootElement.GetProperty("value").ValueKind);
        Assert.Single(body.RootElement.EnumerateObject());
    }

    [Fact]
    public async Task PairingVariantsUseOneRouteWithSeparateTypedBodies()
    {
        // Correct JSON punctuation for the documented Next Step example. This fixture
        // validates its shown field shapes, without treating a syntax repair as new evidence.
        using var handler = FixtureHandler.AuthenticatedConstant(
            """{"success":true,"result":{"fields":[],"pageid":2,"refresh":1000,"session":62328}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var pairing = new FreeboxSystemHomeApi(client.Transport).Home.Adapter(1).Pairing;
        await pairing.Start("node::domus::freebox::secmod").SendAsync();
        using var start = JsonDocument.Parse(handler.Requests.Last().Body!);
        Assert.Equal("start", start.RootElement.GetProperty("op").GetString());
        Assert.False(start.RootElement.TryGetProperty("session", out _));
        await pairing.Next(StringOrInteger.FromString("659887"), StringOrInteger.FromString("1"),
            [HomeIoValue.Null, HomeIoValue.FromString("mon texte"), HomeIoValue.FromBoolean(false), HomeIoValue.FromInteger(38)]).SendAsync();
        using var next = JsonDocument.Parse(handler.Requests.Last().Body!);
        Assert.Equal("next", next.RootElement.GetProperty("op").GetString());
        Assert.Equal(JsonValueKind.String, next.RootElement.GetProperty("session").ValueKind);
        Assert.Equal(JsonValueKind.Null, next.RootElement.GetProperty("fields")[0].ValueKind);
        Assert.False(next.RootElement.GetProperty("fields")[2].GetBoolean());
        await pairing.Stop(15645).SendAsync();
        using var stop = JsonDocument.Parse(handler.Requests.Last().Body!);
        Assert.Equal("stop", stop.RootElement.GetProperty("op").GetString());
        Assert.Equal(15645, stop.RootElement.GetProperty("session").GetInt64());
        Assert.Equal(2, stop.RootElement.EnumerateObject().Count());
        Assert.All(handler.Requests.Where(r => r.Address.AbsolutePath.Contains("/home/pairing/", StringComparison.Ordinal)),
            request => Assert.Equal("/api/v16/home/pairing/1", request.Address.AbsolutePath));
    }

    [Fact]
    public async Task PlanningRequestSnapshotsMappingAndExcludesReadonlyResolution()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"resolution":48}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var values = Enumerable.Repeat(false, 7 * 48).ToArray();
        var fields = new StandbyConfigUpdate().WithUsePlanning(false).WithMapping(values);
        values[0] = true;
        await new FreeboxSystemHomeApi(client.Transport).Standby.Configure().UseFields(fields).SendAsync();
        var request = handler.Requests.Last();
        Assert.Equal("/api/v16/standby/config", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.False(body.RootElement.GetProperty("use_planning").GetBoolean());
        Assert.False(body.RootElement.GetProperty("mapping")[0].GetBoolean());
        Assert.False(body.RootElement.TryGetProperty("resolution", out _));
    }

    [Fact]
    public void UnknownStandbyModeAndDistinctProfileKeysArePreserved()
    {
        var standby = JsonSerializer.Deserialize("""{"planning_mode":"suspend"}""",
            SystemHomeJsonSerializerContext.Default.StandbyConfig)!;
        Assert.Equal("suspend", standby.PlanningMode!.Value.Value);
        Assert.False(standby.PlanningMode.Value.IsKnown);
        var profile = JsonSerializer.Deserialize("""{"icon":"declared","url":"example"}""",
            SystemHomeJsonSerializerContext.Default.Profile)!;
        Assert.Equal("declared", profile.Icon);
        Assert.Equal("example", profile.Url);
        using var serialized = JsonDocument.Parse(JsonSerializer.Serialize(profile, SystemHomeJsonSerializerContext.Default.Profile));
        Assert.Equal("declared", serialized.RootElement.GetProperty("icon").GetString());
        Assert.Equal("example", serialized.RootElement.GetProperty("url").GetString());
    }

    [Theory]
    [InlineData("[\"3DS-Thibault\"]", NetworkControlHostsKind.Names)]
    [InlineData("[{\"id\":\"ether-host\"}]", NetworkControlHostsKind.Hosts)]
    [InlineData("[]", NetworkControlHostsKind.Empty)]
    public void NetworkControlHostsPreserveNamesOrHostObjects(string wire, NetworkControlHostsKind kind)
    {
        var hosts = JsonSerializer.Deserialize(wire, SystemHomeJsonSerializerContext.Default.NetworkControlHosts)!;
        Assert.Equal(kind, hosts.Kind);
        var restored = JsonSerializer.Deserialize(JsonSerializer.Serialize(hosts,
            SystemHomeJsonSerializerContext.Default.NetworkControlHosts), SystemHomeJsonSerializerContext.Default.NetworkControlHosts)!;
        Assert.Equal(kind, restored.Kind);
    }

    [Fact]
    public void HostUnionRejectsHeterogeneousArrays() =>
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize("""["name",{"id":"host"}]""",
            SystemHomeJsonSerializerContext.Default.NetworkControlHosts));

    [Fact]
    public void ModernSystemSensorsAndExpansionModulesRemainSeparate()
    {
        var system = JsonSerializer.Deserialize(
            """{"firmware_version":"4.9","sensors":[{"id":"temp_sw","value":38}],"expansions":[{"slot":1,"present":true}],"model_info":{"name":"fbxgw9-r1/full","has_lan_sfp":true}}""",
            SystemHomeJsonSerializerContext.Default.SystemConfig)!;
        Assert.Equal(38, system.Sensors!.Value[0].Value);
        Assert.True(system.Expansions!.Value[0].Present);
        Assert.True(system.ModelInfo!.HasLanSfp);
    }
}
