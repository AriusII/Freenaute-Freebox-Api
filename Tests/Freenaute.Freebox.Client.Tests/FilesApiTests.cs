using System.Net;
using System.Text;
using System.Text.Json;
using Freenaute.Freebox.Client.Domains.Files;
using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class FilesApiTests
{
    [Fact]
    public async Task ListingPreservesBase64AndOpaqueCursorAndReturnsPagingObject()
    {
        // A valid Base64 value containing '/', '+' and padding is intentionally not Base64URL-normalized.
        var path = EncodedFreeboxPath.FromEncoded("L9+/w6k=");
        using var handler = FixtureHandler.AuthenticatedConstant(
            """{"success":true,"result":{"entries":[{"name":"large","path":"L9+/w6k=","size":5000000000,"type":"future-kind"}],"cursor":"next/+&="}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var files = Create(client);
        var resource = files.FileSystem.At(path);
        Assert.Empty(handler.Requests);

        var listing = await resource.ListAsync(new FileListingOptions
        {
            OnlyFolders = false,
            CountSubFolders = true,
            ExifMode = "light",
            Limit = 100,
            Cursor = "opaque/+&=%"
        });

        Assert.Equal("next/+&=", listing.Cursor);
        var entry = Assert.Single(listing.Entries!);
        Assert.Equal(5_000_000_000, entry.Size);
        Assert.Equal("future-kind", entry.Type);
        Assert.Equal(path, entry.Path);
        var request = ResourceRequests(handler).Single();
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal("https://fixture.example/api/v16/fs/ls/L9%2B%2Fw6k%3D?onlyFolder=false&countSubFolder=true&exifMode=light&limit=100&cursor=opaque%2F%2B%26%3D%25",
            request.Address.AbsoluteUri);
    }

    [Fact]
    public async Task FileCopyReturnsAsyncTaskWithoutPollingAndKeepsPathsAsEncodedStrings()
    {
        using var handler = FixtureHandler.AuthenticatedConstant(
            """{"success":true,"result":{"id":43,"type":"cp","state":"running","progress":24,"src":["/plain/source"],"dst":"/plain/destination"}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var source = EncodedFreeboxPath.FromUtf8Path("/Disque dur/e\u0301té.bin");
        var destination = EncodedFreeboxPath.FromUtf8Path("/Disque dur/archive");

        var task = await Create(client).FileSystem.CopyAsync(new FileTransferRequest
        {
            Files = [source],
            Dst = destination,
            Mode = FileConflictMode.Both
        });

        Assert.Equal("running", task.State);
        Assert.Equal(24, task.Progress); // Preserve the documented raw counter; no inferred percentage normalization.
        Assert.Equal("/plain/destination", task.Dst);
        var request = Assert.Single(ResourceRequests(handler));
        Assert.Equal("/api/v16/fs/cp/", request.Address.AbsolutePath);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.Equal(source.Value, body.RootElement.GetProperty("files")[0].GetString());
        Assert.Equal(destination.Value, body.RootElement.GetProperty("dst").GetString());
        Assert.Equal("both", body.RootElement.GetProperty("mode").GetString());
    }

    [Fact]
    public async Task UrlSubmissionUsesFormEncodingAndImmutableDeferredBuilder()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"id":5000000000}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var original = Create(client).Downloads.FromUrl("https://source.example/video?a=1&b=é+space");
        var customized = original.WithFileName("épisode.mp4").Recursively(false)
            .ToDirectory(EncodedFreeboxPath.FromUtf8Path("/Disque dur/videos"));
        Assert.Empty(handler.Requests);

        var result = await customized.AddAsync();
        await original.AddAsync();

        Assert.False(result.WasMultiple);
        Assert.Equal(5_000_000_000, Assert.Single(result.Ids));
        var requests = ResourceRequests(handler).ToArray();
        Assert.Equal(2, requests.Length);
        Assert.All(requests, request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("/api/v16/downloads/add", request.Address.AbsolutePath);
            Assert.Equal("application/x-www-form-urlencoded", request.ContentType);
        });
        var first = DecodeForm(requests[0].Body!);
        Assert.Equal("https://source.example/video?a=1&b=é+space", first["download_url"]);
        Assert.Equal("épisode.mp4", first["filename"]);
        Assert.Equal("false", first["recursive"]);
        Assert.Equal(EncodedFreeboxPath.FromUtf8Path("/Disque dur/videos").Value, first["download_dir"]);
        Assert.Single(DecodeForm(requests[1].Body!));
    }

    [Fact]
    public async Task MultipleUrlsUseDocumentedNewlineDelimiterAndReadIntegerArrayResult()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"id":[42,43]}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var urls = new[] { "https://one.example/?q=a&b=2", "magnet:?xt=urn:btih:fixture" };

        var result = await Create(client).Downloads.FromUrls(urls).AddAsync();

        Assert.True(result.WasMultiple);
        Assert.Equal(new long[] { 42, 43 }, result.Ids);
        Assert.Equal(string.Join('\n', urls), DecodeForm(Assert.Single(ResourceRequests(handler)).Body!)["download_url_list"]);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task MultipartDownloadDescriptorHasExplicitStreamOwnership(bool leaveOpen)
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"id":42}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        using var stream = new MemoryStream("torrent fixture bytes"u8.ToArray());

        await Create(client).Downloads.AddFileAsync(stream, "fixture.torrent",
            EncodedFreeboxPath.FromUtf8Path("/Disque dur/torrents"), "fixture-password", leaveOpen);

        Assert.Equal(leaveOpen, stream.CanRead);
        var request = Assert.Single(ResourceRequests(handler));
        Assert.Equal("multipart/form-data", request.ContentType);
        Assert.Contains("name=download_file", request.Body!);
        Assert.Contains("filename=fixture.torrent", request.Body!);
        Assert.Contains("torrent fixture bytes", request.Body!);
        Assert.Contains("name=download_dir", request.Body!);
        Assert.Contains("name=archive_password", request.Body!);
    }

    [Fact]
    public async Task SubmissionRejectsAmbiguousOrUnsupportedOverridesBeforeAnyIo()
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var downloads = Create(client).Downloads;

        await Assert.ThrowsAsync<ArgumentException>(() => downloads.AddUrlAsync(new DownloadUrlRequest
        {
            DownloadUrl = "https://one.example/",
            DownloadUrls = ["https://two.example/"]
        }));
        await Assert.ThrowsAsync<ArgumentException>(() => downloads.FromUrls("https://one.example/", "https://two.example/")
            .WithFileName("one-file").AddAsync());
        await Assert.ThrowsAsync<ArgumentException>(() => downloads.FromUrl("https://one.example/").Recursively()
            .WithHash("sha256:fixture").AddAsync());
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task TrackerUrlIsOneOpaqueSegmentAndPluralRouteHasNoFallback()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);

        await Create(client).Downloads.DeleteTrackerAsync(42, "https://tracker.example/announce?key=a+b&pass=x");

        var request = Assert.Single(ResourceRequests(handler));
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal("https://fixture.example/api/v16/downloads/42/trackers/https%3A%2F%2Ftracker.example%2Fannounce%3Fkey%3Da%2Bb%26pass%3Dx",
            request.Address.AbsoluteUri);
    }

    [Theory]
    [InlineData("{\"id\":42,\"status\":\"future\"}", true)]
    [InlineData("[{\"id\":42,\"status\":\"future\"}]", false)]
    public async Task DownloadCollectionPreservesBothAttestedResultShapes(string result, bool objectShape)
    {
        using var handler = FixtureHandler.AuthenticatedConstant("{\"success\":true,\"result\":" + result + "}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);

        var downloads = await Create(client).Downloads.GetAllAsync();

        Assert.Equal(objectShape, downloads.IsObject);
        Assert.Equal("future", Assert.Single(downloads.Items).Status);
    }

    [Fact]
    public void ContradictoryFieldKindsRemainDistinctWithGeneratedJson()
    {
        Assert.False(JsonSerializer.IsReflectionEnabledByDefault);
        var disk = JsonSerializer.Deserialize("""{"id":1,"table_type":"gpt","total_bytes":6000000000000}""", FilesJsonSerializerContext.Default.StorageDisk)!;
        var file = JsonSerializer.Deserialize("""{"id":"opaque","task_id":"42","filepath":"Lw==","path":"deprecated"}""", FilesJsonSerializerContext.Default.DownloadFile)!;
        var peer = JsonSerializer.Deserialize("""{"requests":{}}""", FilesJsonSerializerContext.Default.DownloadPeer)!;
        Assert.Equal("gpt", disk.TableType?.String);
        Assert.Equal(6_000_000_000_000, disk.TotalBytes);
        Assert.Equal("42", file.TaskId?.String);
        Assert.Equal(EncodedFreeboxPath.FromEncoded("Lw=="), file.Filepath);
        Assert.Equal(EmptyObjectOrInt32ArrayKind.EmptyObject, peer.Requests?.Kind);
        using var serialized = JsonDocument.Parse(JsonSerializer.Serialize(file, FilesJsonSerializerContext.Default.DownloadFile));
        Assert.False(serialized.RootElement.TryGetProperty("path", out _));
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize("""{"requests":{"invented":42}}""", FilesJsonSerializerContext.Default.DownloadPeer));
    }

    [Fact]
    public async Task ConfigurationPatchKeepsFalseAndZeroWhileOmittingUnsetAndReadOnlyFields()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"use_watch_dir":false}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);

        await Create(client).Downloads.UpdateConfigurationAsync(new UpdateDownloadConfiguration
        {
            UseWatchDir = false,
            MaxDownloadingTasks = 0,
            News = new UpdateDlNewsConfig { Password = "fixture-password", Ssl = false, Port = StringOrInteger.FromString("119") }
        });

        using var body = JsonDocument.Parse(Assert.Single(ResourceRequests(handler)).Body!);
        Assert.Equal(3, body.RootElement.EnumerateObject().Count());
        Assert.False(body.RootElement.GetProperty("use_watch_dir").GetBoolean());
        Assert.Equal(0, body.RootElement.GetProperty("max_downloading_tasks").GetInt64());
        Assert.Equal("119", body.RootElement.GetProperty("news").GetProperty("port").GetString());
        Assert.False(body.RootElement.TryGetProperty("download_dir", out _));
        Assert.False(body.RootElement.TryGetProperty("blocklist", out _));
        await Assert.ThrowsAsync<ArgumentException>(() => Create(client).Downloads.UpdateConfigurationAsync(new UpdateDownloadConfiguration
        {
            Dns1 = Optional<string>.Null
        }));
    }

    [Fact]
    public async Task FeedMutationIdIsNotSilentlyCoalescedIntoReadId()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"feed_id":42,"title":"Fixture"}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);

        var feed = await Create(client).Downloads.Feeds.CreateAsync(new CreateDownloadFeedRequest { Url = "https://feed.example/rss" });

        Assert.Equal(42, feed.FeedId);
        Assert.Null(feed.Id);
        var request = Assert.Single(ResourceRequests(handler));
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/v16/downloads/feeds/", request.Address.AbsolutePath);
    }

    [Fact]
    public async Task StorageQueriesOmitUnspecifiedValuesAndKeepExplicitFalse()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"fstype":"xfs","partitions_to_delete":[]}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var storage = Create(client).Storage;

        await storage.GetFileSystemAdviceAsync(1);
        await storage.GetFileSystemAdviceAsync(1, 2, false);

        Assert.Equal(new[] { "https://fixture.example/api/v16/storage/disk/1/fsadvice",
            "https://fixture.example/api/v16/storage/disk/1/fsadvice?partition_id=2&dedicated_disk=false" },
            ResourceRequests(handler).Select(request => request.Address.AbsoluteUri).ToArray());
    }

    [Fact]
    public async Task RaidMutationsEmitOnlyDefinedRequestFieldsAndIgnoreUndocumentedResults()
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true,"result":{"unknown_future":true}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var raid = Create(client).Raid;

        await raid.CreateAsync(new CreateRaidRequest { Level = RaidLevel.Raid1, Name = "Fixture", Members = [new(1), new(2)] });
        await raid.SetStateAsync(5, new SetRaidStateRequest { Id = 5, State = RaidState.Running });

        var requests = ResourceRequests(handler).ToArray();
        using var create = JsonDocument.Parse(requests[0].Body!);
        Assert.Equal(3, create.RootElement.EnumerateObject().Count());
        Assert.Equal("raid1", create.RootElement.GetProperty("level").GetString());
        Assert.Equal(1, create.RootElement.GetProperty("members")[0].GetProperty("id").GetInt64());
        Assert.Equal(HttpMethod.Put, requests[1].Method);
        using var state = JsonDocument.Parse(requests[1].Body!);
        Assert.Equal(2, state.RootElement.EnumerateObject().Count());
        Assert.Equal("running", state.RootElement.GetProperty("state").GetString());
    }

    [Fact]
    public async Task ExplicitVmActionsAndTaskReadDoNotIntroduceAutomaticPolling()
    {
        using var handler = FixtureHandler.Authenticated((request, _) => Task.FromResult(FixtureHandler.Json(
            request.Method == HttpMethod.Get ? """{"success":true,"result":{"id":42,"type":"create","done":false,"error":false}}""" : """{"success":true}""")));
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var vm = Create(client).VirtualMachines;

        await vm.StartAsync(1);
        var task = await vm.GetDiskTaskAsync(42);

        Assert.False(task.Done);
        Assert.Equal(new[] { "/api/v16/vm/1/start", "/api/v16/vm/disk/task/42" }, ResourceRequests(handler).Select(request => request.Address.AbsolutePath));
    }

    [Fact]
    public async Task RrdUsesPostAndNumericFieldsWithoutDeprecatedTemperatureNames()
    {
        using var handler = FixtureHandler.AuthenticatedConstant(
            """{"success":true,"result":{"date_start":10,"date_end":20,"data":[{"time":10,"cpum":4250,"hdd":null}]}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var statistics = Create(client).Statistics;

        var result = await statistics.FetchAsync(new FetchRrdRequest { Db = RrdDatabase.Temp, Fields = new[] { "cpum", "hdd" }, Precision = 100 });

        Assert.Equal(4250, result.Data![0]["cpum"]); // The documented precision multiplier stays on the wire value.
        Assert.Null(result.Data[0]["hdd"]);
        var request = Assert.Single(ResourceRequests(handler));
        Assert.Equal(HttpMethod.Post, request.Method);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.Equal("temp", body.RootElement.GetProperty("db").GetString());
        Assert.False(body.RootElement.TryGetProperty("date_start", out _));
        await Assert.ThrowsAsync<ArgumentException>(() => statistics.FetchAsync(new FetchRrdRequest { Db = RrdDatabase.Temp, Fields = new[] { "temp1" } }));
    }

    [Fact]
    public async Task BinaryFileUsesEscapedOpaquePathAndReturnsOwnedResponse()
    {
        var stream = new MemoryStream("binary fixture"u8.ToArray());
        using var handler = FixtureHandler.Authenticated((_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StreamContent(stream)
        }));
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);

        await using (var download = await Create(client).FileSystem.At(EncodedFreeboxPath.FromEncoded("L9+/w6k=")).DownloadAsync())
        {
            using var buffer = new MemoryStream();
            await download.Content.CopyToAsync(buffer);
            Assert.Equal("binary fixture", Encoding.UTF8.GetString(buffer.ToArray()));
        }

        Assert.False(stream.CanRead);
        Assert.Equal("https://fixture.example/api/v16/dl/L9%2B%2Fw6k%3D", Assert.Single(ResourceRequests(handler)).Address.AbsoluteUri);
    }

    private static FreeboxFilesApi Create(FreeboxClient client) => new(client.Transport, client.BinaryTransport);
    private static IEnumerable<CapturedRequest> ResourceRequests(FixtureHandler handler) => handler.Requests.Where(request =>
        !request.Address.AbsolutePath.Contains("/login/", StringComparison.Ordinal));
    private static Dictionary<string, string> DecodeForm(string body) => body.Split('&').Select(field => field.Split('=', 2))
        .ToDictionary(field => Uri.UnescapeDataString(field[0].Replace('+', ' ')), field => Uri.UnescapeDataString(field[1].Replace('+', ' ')));
}
