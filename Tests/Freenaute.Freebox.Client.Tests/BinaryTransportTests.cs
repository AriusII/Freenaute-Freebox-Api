using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class BinaryTransportTests
{
    [Fact]
    public async Task DownloadPreservesBytesMetadataAndSessionUntilCallerDisposesIt()
    {
        var body = new TrackingStream([0, 255, 128, 1]);
        using var handler = FixtureHandler.Authenticated((_, _) =>
        {
            var content = new StreamContent(body);
            content.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");
            content.Headers.ContentLength = 4;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
        });
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var download = await client.BinaryTransport.DownloadAsync(HttpMethod.Get, "call/voicemail/1/download/");
        Assert.False(body.IsDisposed);
        Assert.Equal("audio/wav", download.ContentType);
        Assert.Equal(4, download.ContentLength);
        using var destination = new MemoryStream();
        await download.Content.CopyToAsync(destination);
        Assert.Equal(new byte[] { 0, 255, 128, 1 }, destination.ToArray());
        var request = handler.Requests.Last();
        Assert.Equal("https://fixture.example/api/v16/call/voicemail/1/download/", request.Address.AbsoluteUri);
        Assert.Equal("fixture-session", request.SessionToken);
        Assert.False(body.IsDisposed);
        await download.DisposeAsync();
        download.Dispose();
        Assert.True(body.IsDisposed);
    }

    [Fact]
    public async Task JsonFileDownloadIsReturnedAsRawBytes()
    {
        const string file = """{"success":false,"user_file":true}""";
        using var handler = FixtureHandler.Constant(file);
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http, authenticate: false);
        await using var download = await client.BinaryTransport.DownloadAsync(HttpMethod.Get, "dl/L3RtcC9maWxl/",
            requiresAuthentication: false);
        using var reader = new StreamReader(download.Content, Encoding.UTF8, leaveOpen: true);
        Assert.Equal(file, await reader.ReadToEndAsync());
    }

    [Theory]
    [InlineData("{\"success\":false,\"error_code\":\"denied\",\"msg\":\"Forbidden\"}", "denied")]
    [InlineData("upstream unavailable", null)]
    public async Task FailedHttpDownloadThrowsAndDisposesItsBody(string payload, string? errorCode)
    {
        var body = new TrackingStream(Encoding.UTF8.GetBytes(payload));
        using var handler = new FixtureHandler((_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.Forbidden)
        { Content = new StreamContent(body) }));
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http, authenticate: false);
        var error = await Assert.ThrowsAsync<FreeboxApiException>(() => client.BinaryTransport.DownloadAsync(
            HttpMethod.Get, "fixture/", requiresAuthentication: false));
        Assert.Equal(HttpStatusCode.Forbidden, error.StatusCode);
        Assert.Equal(errorCode, error.ErrorCode);
        Assert.True(body.IsDisposed);
    }

    [Fact]
    public async Task DownloadTimeoutCoversBodyReadsAfterHeaders()
    {
        var body = new BlockingStream();
        using var handler = new FixtureHandler((_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        { Content = new StreamContent(body) }));
        using var http = new HttpClient(handler);
        using var client = new FreeboxClient(http, new FreeboxClientOptions
        { ServerAddress = new Uri("http://fixture.example/"), ApiVersion = 16, Timeout = TimeSpan.FromMilliseconds(250) });
        await using var download = await client.BinaryTransport.DownloadAsync(HttpMethod.Get, "fixture/",
            requiresAuthentication: false);
        var read = download.Content.ReadAsync(new byte[16]).AsTask();
        await body.ReadStarted.Task.WaitAsync(TimeSpan.FromSeconds(10));
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => read.WaitAsync(TimeSpan.FromSeconds(10)));
        Assert.True(body.IsDisposed);
    }

    [Fact]
    public async Task CallerCancellationAlsoCancelsSubsequentBodyReads()
    {
        var body = new TrackingStream([1]);
        using var handler = new FixtureHandler((_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        { Content = new StreamContent(body) }));
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http, authenticate: false);
        using var cancellation = new CancellationTokenSource();
        await using var download = await client.BinaryTransport.DownloadAsync(HttpMethod.Get, "fixture/",
            cancellation.Token, requiresAuthentication: false);
        cancellation.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => download.Content.ReadAsync(new byte[1]).AsTask());
        Assert.True(body.IsDisposed);
    }

    [Fact]
    public async Task FormsPreserveWireContentTypeAndDisposeTransferredContent()
    {
        using var handler = FixtureHandler.Constant("""{"success":true,"result":{"value":"accepted"}}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http, authenticate: false);
        var content = new FormUrlEncodedContent(new Dictionary<string, string> { ["download_url_list"] = "https://a/\nhttps://b/" });
        var result = await client.Transport.SendContentAsync(HttpMethod.Post, "downloads/add", content,
            TestJsonContext.Default.FixtureResult, requiresAuthentication: false);
        Assert.Equal("accepted", result.Value);
        Assert.Equal("application/x-www-form-urlencoded", handler.Requests.Single().ContentType);
        Assert.Contains("%0A", handler.Requests.Single().Body);
        await Assert.ThrowsAsync<ObjectDisposedException>(() => content.ReadAsStringAsync());
    }

    [Theory]
    [InlineData("dl/L3RtcC8%2F/")]
    [InlineData("downloads/1/trackers/https%3A%2F%2Ftracker.example%2Fannounce/")]
    public async Task OpaqueParametersKeepEscapedSeparatorsWithoutEscapingTheApiRoot(string path)
    {
        using var handler = FixtureHandler.Constant("""{"success":true}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http, authenticate: false);
        await client.Transport.SendAsync(HttpMethod.Get, path, requiresAuthentication: false);
        Assert.Equal("https://fixture.example/api/v16/" + path, handler.Requests.Single().Address.AbsoluteUri);
    }

    private sealed class TrackingStream(byte[] bytes) : MemoryStream(bytes)
    {
        public bool IsDisposed { get; private set; }
        protected override void Dispose(bool disposing) { IsDisposed = true; base.Dispose(disposing); }
    }

    private sealed class BlockingStream : Stream
    {
        public TaskCompletionSource ReadStarted { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public bool IsDisposed { get; private set; }
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }
        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        { ReadStarted.TrySetResult(); await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken); return 0; }
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();
        public override void Flush() => throw new NotSupportedException();
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        protected override void Dispose(bool disposing) { IsDisposed = true; base.Dispose(disposing); }
    }
}
