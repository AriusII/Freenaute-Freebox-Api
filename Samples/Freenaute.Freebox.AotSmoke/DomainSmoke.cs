using System.Net;
using System.Text.Json;
using Freenaute.Freebox.Client;
using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Contracts.Protocol;
using Freenaute.Freebox.Mapper.Contracts.SystemHome;
using Freenaute.Freebox.Mapper.Contracts.WebSockets;
using Freenaute.Freebox.Mapper.Serialization;

internal static class DomainSmoke
{
    internal const int ProtectedRequests = 11;
    internal const string EncodedPath = "L9+/w6k="; // UTF8 /߿é; standard Base64 retains '+' and '/'.

    internal static async Task RunAsync(IFreeboxClient client)
    {
        var status = await client.Network.Connection.GetStatusAsync();
        Check(status.State == "future-state" && status.BandwidthDown == 8_000_000_000 &&
            status.Ipv4PortRange is [16_384, 32_767], "Network read metadata must retain open states and Int64 counters.");
        var connection = await client.Network.Connection.UpdateConfigurationAsync(new ConnectionConfigurationPatch
        {
            Ping = false
        });
        Check(connection.Ping == false, "Network patches must retain explicit false.");

        var ftp = await client.Services.Ftp.Configure()
            .With(fields => fields with { Enabled = false, PortCtrl = 0 })
            .SendAsync();
        Check(ftp.Enabled == false && ftp.PortCtrl == 0, "Services patches must retain false and zero.");

        var storage = await client.Files.Storage.UpdateConfigurationAsync(new UpdateStorageConfiguration
        {
            ExternalPmEnabled = false,
            ExternalPmIdleBeforeSpindown = 0
        });
        Check(storage.ExternalPmEnabled == false && storage.ExternalPmIdleBeforeSpindown == 0,
            "Files configuration metadata must retain supplied scalar values.");
        var arrayDownloads = await client.Files.Downloads.GetAllAsync();
        var objectDownloads = await client.Files.Downloads.GetAllAsync();
        Check(arrayDownloads.IsArray && objectDownloads.IsObject &&
            arrayDownloads.Items.Count == 1 && objectDownloads.Items.Count == 1 &&
            objectDownloads.Items[0].Status == "future-status",
            "Typed object-or-array results must preserve their wire shape and open response values.");
        var file = await client.Files.FileSystem.At(EncodedFreeboxPath.FromEncoded(EncodedPath)).GetInformationAsync();
        Check(file.Path?.Value == EncodedPath && file.Size == 5_000_000_000,
            "Filesystem metadata must retain exact Base64 and large file sizes.");
        var added = await client.Files.Downloads
            .FromUrls("https://one.fixture.example/?a=1&b=é", "magnet:?xt=urn:btih:fixture")
            .ToDirectory(EncodedFreeboxPath.FromEncoded(EncodedPath))
            .AddAsync();
        Check(added.WasMultiple && added.Ids.SequenceEqual(new long[] { 42, 43 }),
            "Form submissions must deserialize their typed scalar-or-array identifier result.");

        var lcd = await client.SystemHome.Lcd.Configure()
            .UseFields(new LcdConfigPatch().WithBrightness(0).WithOrientationForced(false))
            .SendAsync();
        Check(lcd.Brightness == 0 && lcd.OrientationForced == false &&
            lcd.Screensaver is { IsKnown: false, Value: "future-mode" },
            "SystemHome metadata must retain patches and unknown typed wire tokens.");

        var target = await client.Notifications.Target("fixture-target").GetAsync();
        Check(target.IsArray && target.Items is [{ Id: "fixture-target", Type: "future-platform" }],
            "Protocol notification metadata must accept the attested array result.");
        await client.Notifications.CreateTargetAsync(new NotificationTargetWrite
        {
            Name = "Fixture target",
            Type = "android",
            Token = "fixture-notification-token",
            ApiUrl = "https://consumer.fixture.example/",
            MessageType = "plain",
            Subscriptions = ["download"]
        });

        ExerciseSourceGeneratedWebSocketContracts();
        ExerciseHomeScalarUnion();
        ExerciseLocalizedIntegerStringAndEmptyObjectUnions();
    }

    private static void ExerciseSourceGeneratedWebSocketContracts()
    {
        using var registration = JsonDocument.Parse(JsonSerializer.Serialize(new RegisterEventsRequest
        {
            RequestId = 31,
            Events = ["vm_disk_task_done"]
        }, WebSocketJsonSerializerContext.Default.RegisterEventsRequest));
        Check(registration.RootElement.GetProperty("action").GetString() == "register" &&
            registration.RootElement.GetProperty("request_id").GetInt64() == 31,
            "WebSocket registration must use generated control-message metadata.");
        using var upload = JsonDocument.Parse(JsonSerializer.Serialize(new FileUploadStartRequest
        {
            RequestId = 32,
            Directory = EncodedFreeboxPath.FromEncoded(EncodedPath),
            Filename = "fixture.bin",
            Size = 0
        }, WebSocketJsonSerializerContext.Default.FileUploadStartRequest));
        Check(upload.RootElement.GetProperty("dirname").GetString() == EncodedPath &&
            upload.RootElement.GetProperty("size").GetInt64() == 0 &&
            !upload.RootElement.TryGetProperty("force", out _),
            "Upload control JSON must preserve Base64, explicit zero and optional omission.");
        var response = JsonSerializer.Deserialize(
            """{"request_id":32,"action":"upload_chunk","success":true,"result":{"total_len":5000000000,"complete":false,"cancelled":false}}""",
            WebSocketJsonSerializerContext.Default.FileUploadControlResponse)!;
        Check(response.RequestId == 32 && response.Result?.TotalLength == 5_000_000_000 && response.Result.Complete == false,
            "Typed WebSocket upload replies must deserialize without reflection.");
        var notification = JsonSerializer.Deserialize(
            """{"action":"notification","source":"event","event":"vm_disk_task_done","success":true,"result":{"id":42,"done":true}}""",
            WebSocketJsonSerializerContext.Default.FreeboxWebSocketMessage)!;
        Check(notification.Result?.GetProperty("id").GetInt64() == 42,
            "Open WebSocket event payloads must remain available as JsonElement.");
    }

    private static void ExerciseHomeScalarUnion()
    {
        var values = new (string Json, HomeIoValueKind Kind)[]
        {
            ("null", HomeIoValueKind.Null), ("false", HomeIoValueKind.Boolean),
            ("0", HomeIoValueKind.Integer), ("1.0", HomeIoValueKind.Float), ("\"fixture\"", HomeIoValueKind.String)
        };
        foreach (var (json, expectedKind) in values)
        {
            var value = JsonSerializer.Deserialize(json, SystemHomeJsonSerializerContext.Default.HomeIoValue)!;
            Check(value.Kind == expectedKind && JsonSerializer.Serialize(value, SystemHomeJsonSerializerContext.Default.HomeIoValue) == json,
                "The Home scalar union must preserve null, Boolean, integer, float and string kinds.");
        }
    }

    private static void ExerciseLocalizedIntegerStringAndEmptyObjectUnions()
    {
        var disk = JsonSerializer.Deserialize("""{"table_type":"gpt","total_bytes":6000000000000}""",
            FilesJsonSerializerContext.Default.StorageDisk)!;
        var numericDisk = JsonSerializer.Deserialize("""{"table_type":1}""", FilesJsonSerializerContext.Default.StorageDisk)!;
        Check(disk.TableType?.String == "gpt" && numericDisk.TableType?.Integer == 1 && disk.TotalBytes == 6_000_000_000_000,
            "Localized integer/string unions must retain their original kind without global coercion.");
        var emptyPeer = JsonSerializer.Deserialize("""{"requests":{}}""", FilesJsonSerializerContext.Default.DownloadPeer)!;
        var arrayPeer = JsonSerializer.Deserialize("""{"requests":[0,42]}""", FilesJsonSerializerContext.Default.DownloadPeer)!;
        Check(emptyPeer.Requests?.Kind == EmptyObjectOrInt32ArrayKind.EmptyObject &&
            arrayPeer.Requests?.Values.SequenceEqual(new[] { 0, 42 }) == true,
            "The peer request union must retain empty-object and integer-array shapes.");
    }

    internal static void Check(bool condition, string message)
    {
        if (!condition) throw new InvalidOperationException(message);
    }
}

/// <summary>Only local HTTP fixtures; it never opens a socket or interprets these responses as hardware proof.</summary>
internal sealed class DomainFixture
{
    private int downloadLists;

    internal async Task<HttpResponseMessage?> TryRespondAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var path = request.RequestUri!.AbsolutePath;
        if (path == "/api/v16/connection/" && request.Method == HttpMethod.Get)
            return Json("""{"success":true,"result":{"state":"future-state","bandwidth_down":8000000000,"ipv4_port_range":[16384,32767]}}""");
        if (path == "/api/v16/connection/config/" && request.Method == HttpMethod.Put)
        {
            using var body = await BodyAsync(request, cancellationToken);
            var patch = JsonSerializer.Deserialize(body.RootElement, NetworkJsonSerializerContext.Default.ConnectionConfigurationPatch)!;
            DomainSmoke.Check(patch.Ping.HasValue && !patch.Ping.Value && !patch.Wol.IsSet &&
                body.RootElement.EnumerateObject().Count() == 1, "Network must omit unset patch fields.");
            return Json("""{"success":true,"result":{"ping":false}}""");
        }
        if (path == "/api/v16/ftp/config/" && request.Method == HttpMethod.Put)
        {
            using var body = await BodyAsync(request, cancellationToken);
            var patch = JsonSerializer.Deserialize(body.RootElement, ServicesJsonSerializerContext.Default.FtpConfigPatch)!;
            DomainSmoke.Check(patch.Enabled.HasValue && !patch.Enabled.Value && patch.PortCtrl.Value == 0 &&
                !patch.Password.IsSet && body.RootElement.EnumerateObject().Count() == 2,
                "Services must omit unset fields while sending false and zero.");
            return Json("""{"success":true,"result":{"enabled":false,"port_ctrl":0}}""");
        }
        if (path == "/api/v16/storage/config/" && request.Method == HttpMethod.Put)
        {
            using var body = await BodyAsync(request, cancellationToken);
            var patch = JsonSerializer.Deserialize(body.RootElement, FilesJsonSerializerContext.Default.UpdateStorageConfiguration)!;
            DomainSmoke.Check(patch.ExternalPmEnabled.HasValue && !patch.ExternalPmEnabled.Value &&
                patch.ExternalPmIdleBeforeSpindown.Value == 0 && body.RootElement.EnumerateObject().Count() == 2,
                "Files must serialize supplied scalar values with generated metadata.");
            return Json("""{"success":true,"result":{"external_pm_enabled":false,"external_pm_idle_before_spindown":0}}""");
        }
        if (path == "/api/v16/downloads/" && request.Method == HttpMethod.Get)
        {
            downloadLists++;
            return Json(downloadLists == 1
                ? """{"success":true,"result":[{"id":42,"status":"future-status"}]}"""
                : """{"success":true,"result":{"id":42,"status":"future-status"}}""");
        }
        if (path == "/api/v16/fs/info/L9%2B%2Fw6k%3D" && request.Method == HttpMethod.Get)
            return Json("""{"success":true,"result":{"path":"L9+/w6k=","size":5000000000,"name":"߿é"}}""");
        if (path == "/api/v16/downloads/add" && request.Method == HttpMethod.Post)
        {
            DomainSmoke.Check(request.Content?.Headers.ContentType?.MediaType == "application/x-www-form-urlencoded",
                "Download URL submission must use the documented form encoding.");
            var fields = DecodeForm(await request.Content!.ReadAsStringAsync(cancellationToken));
            DomainSmoke.Check(fields.Count == 2 && fields["download_dir"] == DomainSmoke.EncodedPath &&
                fields["download_url_list"] == "https://one.fixture.example/?a=1&b=é\nmagnet:?xt=urn:btih:fixture",
                "Form encoding must retain UTF8, reserved URL characters and the newline delimiter.");
            return Json("""{"success":true,"result":{"id":[42,43]}}""");
        }
        if (path == "/api/v16/lcd/config/" && request.Method == HttpMethod.Put)
        {
            using var body = await BodyAsync(request, cancellationToken);
            var patch = JsonSerializer.Deserialize(body.RootElement, SystemHomeJsonSerializerContext.Default.LcdConfigPatch)!;
            DomainSmoke.Check(patch.Brightness.Value == 0 && patch.OrientationForced.HasValue &&
                !patch.OrientationForced.Value && !patch.LedStripEnabled.IsSet && body.RootElement.EnumerateObject().Count() == 2,
                "SystemHome patches must omit unset fields and retain false/zero.");
            return Json("""{"success":true,"result":{"brightness":0,"orientation_forced":false,"screensaver":"future-mode"}}""");
        }
        if (path == "/api/v16/notif/targets/fixture-target" && request.Method == HttpMethod.Get)
            return Json("""{"success":true,"result":[{"id":"fixture-target","type":"future-platform","subscriptions":["download"]}]}""");
        if (path == "/api/v16/notif/targets/" && request.Method == HttpMethod.Post)
        {
            using var body = await BodyAsync(request, cancellationToken);
            var target = JsonSerializer.Deserialize(body.RootElement, ProtocolJsonSerializerContext.Default.NotificationTargetWrite)!;
            DomainSmoke.Check(target.Token == "fixture-notification-token" && target.Subscriptions is ["download"] &&
                body.RootElement.EnumerateObject().Count() == 6, "Protocol writes must use their own generated DTO metadata.");
            return Json("""{"success":true}""");
        }
        return null;
    }

    private static async Task<JsonDocument> BodyAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
        JsonDocument.Parse(await request.Content!.ReadAsStringAsync(cancellationToken));
    private static HttpResponseMessage Json(string body) => FixtureHandler.Json(body);
    private static Dictionary<string, string> DecodeForm(string body) => body.Split('&').Select(field => field.Split('=', 2))
        .ToDictionary(field => Uri.UnescapeDataString(field[0].Replace('+', ' ')), field => Uri.UnescapeDataString(field[1].Replace('+', ' ')));
}
