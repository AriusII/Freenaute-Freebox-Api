using System.Net;
using System.Text;
using System.Text.Json;
using Freenaute.Freebox.Client.Domains.Services;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class ServicesApiTests
{
    [Fact]
    public async Task FluentConfigurationBranchesKeepExplicitFalseAndZeroWithoutSendingUnsetFields()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"enabled":false}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var services = new FreeboxServicesApi(client.Transport);
        var baseline = services.Ftp.Configure();
        var disabled = baseline.With(patch => patch with { Enabled = false, PortCtrl = 0 });
        var anonymous = baseline.With(patch => patch with { AllowAnonymous = false });
        Assert.Empty(handler.Requests);

        await disabled.SendAsync();
        await anonymous.SendAsync();

        var requests = handler.Requests.Where(r => r.Address.AbsolutePath.Contains("/ftp/", StringComparison.Ordinal)).ToArray();
        Assert.Equal("/api/v16/ftp/config/", requests[0].Address.AbsolutePath);
        using var first = JsonDocument.Parse(requests[0].Body!);
        using var second = JsonDocument.Parse(requests[1].Body!);
        Assert.False(first.RootElement.GetProperty("enabled").GetBoolean());
        Assert.Equal(0, first.RootElement.GetProperty("port_ctrl").GetInt64());
        Assert.Equal(2, first.RootElement.EnumerateObject().Count());
        Assert.False(second.RootElement.GetProperty("allow_anonymous").GetBoolean());
        Assert.Single(second.RootElement.EnumerateObject());
    }

    [Fact]
    public async Task ContactItemUsesGlobalRouteAndBindsSelectedContactWhenCreated()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"id":2,"contact_id":7,"number":"0999999999","is_default":false}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var services = new FreeboxServicesApi(client.Transport);
        var numbers = services.Contacts.Contact(7).Numbers;

        await numbers.CreateAsync(new ContactNumberWriteRequest { ContactId = 999, Number = "0999999999", IsDefault = false });
        await numbers.Item(2).UpdateAsync(new ContactNumberWriteRequest { IsOwn = false });

        var requests = handler.Requests.Where(r => r.Address.AbsolutePath.Contains("/number/", StringComparison.Ordinal)).ToArray();
        Assert.Equal("/api/v16/number/", requests[0].Address.AbsolutePath);
        using var first = JsonDocument.Parse(requests[0].Body!);
        Assert.Equal(7, first.RootElement.GetProperty("contact_id").GetInt64());
        Assert.False(first.RootElement.GetProperty("is_default").GetBoolean());
        Assert.Equal("/api/v16/number/2", requests[1].Address.AbsolutePath);
        Assert.Equal("{\"is_own\":false}", requests[1].Body);
    }

    [Fact]
    public async Task PlayerVolumePreservesInnerVersionAndIndependentCommands()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"volume":0,"mute":false}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var services = new FreeboxServicesApi(client.Transport);
        var volume = services.Player.Device(3).Volume();
        await volume.Level(0).Muted(false).SendAsync();
        await volume.Level(50).SendAsync();

        var requests = handler.Requests.Where(r => r.Address.AbsolutePath.Contains("/player/", StringComparison.Ordinal)).ToArray();
        Assert.Equal("/api/v16/player/3/api/v6/control/volume/", requests[0].Address.AbsolutePath);
        using var first = JsonDocument.Parse(requests[0].Body!);
        Assert.Equal(0, first.RootElement.GetProperty("volume").GetInt32());
        Assert.False(first.RootElement.GetProperty("mute").GetBoolean());
        Assert.Equal("{\"volume\":50}", requests[1].Body);
    }

    [Fact]
    public async Task NestedVpnAuthenticationPatchRetainsExplicitFalse()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"id":"vpnclient-1","active":false}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var services = new FreeboxServicesApi(client.Transport);
        await services.VpnClient.Configuration("vpnclient-1").UpdateAsync(new VpnClientWriteRequest
        {
            Active = false,
            ConfPptp = new VpnClientPptpPatch { AllowedAuth = new VpnClientAuthenticationPatch { Eap = false } }
        });
        var request = handler.Requests.Last();
        Assert.Equal("/api/v16/vpn_client/config/vpnclient-1", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.False(body.RootElement.GetProperty("active").GetBoolean());
        Assert.False(body.RootElement.GetProperty("conf_pptp").GetProperty("allowed_auth").GetProperty("eap").GetBoolean());
        Assert.Single(body.RootElement.GetProperty("conf_pptp").EnumerateObject());
    }

    [Fact]
    public async Task FormalCallBatchMethodsAndCurrentTftpRouteAreUsed()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var services = new FreeboxServicesApi(client.Transport);
        await services.Call.MarkAllLogEntriesAsReadAsync();
        await services.Call.DeleteAllLogEntriesAsync();
        await services.Tftp.UpdateConfigurationAsync(new TftpConfigPatch { Enabled = false });
        var requests = handler.Requests.Where(r => !r.Address.AbsolutePath.Contains("/login/", StringComparison.Ordinal)).ToArray();
        Assert.Equal(HttpMethod.Post, requests[0].Method);
        Assert.Equal(HttpMethod.Post, requests[1].Method);
        Assert.Equal("/api/v16/tftp/config/", requests[2].Address.AbsolutePath);
        Assert.Equal("{\"enabled\":false}", requests[2].Body);
    }

    [Fact]
    public async Task BinaryVoicemailAndPlainVpnConfigurationKeepEncodedIdentifiersAndSendOnce()
    {
        using var handler = FixtureHandler.Authenticated((request, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(request.Address.AbsolutePath.Contains("voicemail", StringComparison.Ordinal) ? "RIFF-fixture" : "client\nremote fixture", Encoding.ASCII,
                request.Address.AbsolutePath.Contains("voicemail", StringComparison.Ordinal) ? "audio/wav" : "text/plain")
        }));
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var services = new FreeboxServicesApi(client.Transport);
        await using var audio = await services.Call.Voicemail("20221215_154135_#1.au").DownloadAudioAsync();
        using var reader = new StreamReader(audio.Content, leaveOpen: true);
        Assert.Equal("RIFF-fixture", await reader.ReadToEndAsync());
        Assert.Equal("audio/wav", audio.ContentType);
        await using var profile = await services.Vpn.DownloadConfigurationAsync("openvpn_routed", "a&b#c");
        var requests = handler.Requests.Where(r => !r.Address.AbsolutePath.Contains("/login/", StringComparison.Ordinal)).ToArray();
        Assert.Equal(2, requests.Length);
        Assert.Equal("https://fixture.example/api/v16/call/voicemail/20221215_154135_%231.au/audio_file", requests[0].Address.AbsoluteUri);
        Assert.Equal("https://fixture.example/api/v16/vpn/download_config/openvpn_routed/a%26b%23c/plain", requests[1].Address.AbsoluteUri);
        Assert.Equal("text/plain", profile.ContentType);
    }

    [Fact]
    public void RecordingTimeAndIpSecAuthenticationModesPreserveEveryDocumentedKind()
    {
        var integer = JsonSerializer.Deserialize("42", ServicesJsonSerializerContext.Default.PvrRecordTime);
        var map = JsonSerializer.Deserialize("""{"dvb":{"sd":42,"3d":0}}""", ServicesJsonSerializerContext.Default.PvrRecordTime);
        Assert.Equal(PvrRecordTimeKind.Integer, integer.Kind);
        Assert.Equal(42, integer.Integer);
        Assert.Equal(PvrRecordTimeKind.Map, map.Kind);
        Assert.Equal(0, map.Map["dvb"]["3d"]);
        Assert.Equal("42", JsonSerializer.Serialize(integer, ServicesJsonSerializerContext.Default.PvrRecordTime));
        Assert.Equal("{\"dvb\":{\"sd\":42,\"3d\":0}}", JsonSerializer.Serialize(map, ServicesJsonSerializerContext.Default.PvrRecordTime));
        var authMap = JsonSerializer.Deserialize("""{"psk":{"id_source":"hostname"}}""", ServicesJsonSerializerContext.Default.VpnIpSecAuthModes);
        var authArray = JsonSerializer.Deserialize("""[{"id_source":"hostname"}]""", ServicesJsonSerializerContext.Default.VpnIpSecAuthModes);
        Assert.Equal(VpnIpSecAuthModesKind.Map, authMap.Kind);
        Assert.Equal("hostname", authMap.Map["psk"].IdSource!.Value.Value);
        Assert.Equal(VpnIpSecAuthModesKind.Array, authArray.Kind);
        Assert.Single(authArray.Items);
        Assert.StartsWith("{\"psk\":", JsonSerializer.Serialize(authMap, ServicesJsonSerializerContext.Default.VpnIpSecAuthModes));
        Assert.StartsWith("[{", JsonSerializer.Serialize(authArray, ServicesJsonSerializerContext.Default.VpnIpSecAuthModes));
    }

    [Fact]
    public void ProjectionKeepsNumericKindsLargeCountersUnknownEnumsAndBothIpv4Spellings()
    {
        var voicemail = JsonSerializer.Deserialize("""{"id":"fixture.au","country_code":33} """, ServicesJsonSerializerContext.Default.VoicemailEntry)!;
        Assert.Equal(StringOrIntegerKind.Integer, voicemail.CountryCode!.Value.Kind);
        var recording = JsonSerializer.Deserialize("""{"id":123,"state":"future_state","byte_size":39376555016} """, ServicesJsonSerializerContext.Default.FinishedRecording)!;
        Assert.Equal(StringOrIntegerKind.Integer, recording.Id!.Value.Kind);
        Assert.Equal("future_state", recording.State!.Value.Value);
        Assert.Equal(39376555016L, recording.ByteSize);
        var lower = JsonSerializer.Deserialize("""{"ipv4":{"provider":"none"}}""", ServicesJsonSerializerContext.Default.VpnClientStatus)!;
        var upper = JsonSerializer.Deserialize("""{"IPv4":{"provider":"static"}}""", ServicesJsonSerializerContext.Default.VpnClientStatus)!;
        Assert.Equal("none", lower.IpV4!.Provider!.Value.Value);
        Assert.Equal("static", upper.IpV4!.Provider!.Value.Value);
    }

    [Fact]
    public void UnsupportedNullOrMissingSchemaAndInvalidVolumeFailBeforeHttp()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var services = new FreeboxServicesApi(client.Transport);
        Assert.Throws<ArgumentException>(() => { _ = services.Ftp.UpdateConfigurationAsync(new FtpConfigPatch { Password = Optional<string>.Null }); });
        Assert.Throws<ArgumentOutOfRangeException>(() => { _ = services.Player.Device(1).Volume().Level(101).SendAsync(); });
        Assert.Throws<NotSupportedException>(() => { _ = services.VpnClient.CreateConfigurationAsync(new VpnClientWriteRequest { Type = VpnClientType.OpenVpn }); });
        Assert.Throws<ArgumentException>(() => { _ = services.Player.Device(1).SendMediaCommandAsync(new("seek_to")); });
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public void RequestAndResponseToStringDoNotEmitSecretsOrContactDetails()
    {
        Assert.Equal(nameof(FtpConfigPatch), new FtpConfigPatch { Password = "fixture-secret" }.ToString());
        Assert.Equal(nameof(VpnClientWireGuardPatch), new VpnClientWireGuardPatch { LocalPrivKey = "fixture-key" }.ToString());
        Assert.Equal(nameof(ContactWriteRequest), new ContactWriteRequest { DisplayName = "fixture-person" }.ToString());
    }
}
