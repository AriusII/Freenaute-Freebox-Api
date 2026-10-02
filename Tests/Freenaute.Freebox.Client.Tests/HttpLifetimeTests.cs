using System.Net;
using System.Text;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class HttpLifetimeTests
{
    [Fact]
    public async Task StandaloneClientAcceptsPreviouslyUsedHttpClientWithoutChangingItsConfiguration()
    {
        using var handler = FixtureHandler.Constant("""{"success":true,"result":{"value":"accepted"}}""");
        using var http = new HttpClient(handler)
        {
            BaseAddress = new Uri("http://caller.example/"),
            Timeout = TimeSpan.FromSeconds(13)
        };
        http.DefaultRequestHeaders.Add("X-Fixture", "caller-configuration");
        using var previousResponse = await http.GetAsync("probe/");

        using var client = new FreeboxClient(http, new FreeboxClientOptions
        {
            ServerAddress = new Uri("http://fixture.example/"),
            ApiVersion = 16,
            Timeout = TimeSpan.FromSeconds(3)
        });
        var result = await client.Transport.SendAsync(HttpMethod.Get, "fixture/",
            TestJsonContext.Default.FixtureResult, requiresAuthentication: false);

        Assert.Equal("accepted", result.Value);
        Assert.Equal(TimeSpan.FromSeconds(13), http.Timeout);
        Assert.Equal("http://caller.example/", http.BaseAddress.AbsoluteUri);
        Assert.Equal("caller-configuration", http.DefaultRequestHeaders.GetValues("X-Fixture").Single());
        Assert.Collection(handler.Requests,
            request => Assert.Equal("http://caller.example/probe/", request.Address.AbsoluteUri),
            request => Assert.Equal("http://fixture.example/api/v16/fixture/", request.Address.AbsoluteUri));
    }

    [Fact]
    public async Task DisposingStandaloneClientPreservesCallerOwnedHttpClient()
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        var client = new FreeboxClient(http, new FreeboxClientOptions { ApiVersion = 16 });

        client.Dispose();
        using var response = await http.GetAsync("http://fixture.example/probe/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Single(handler.Requests);
    }

    [Fact]
    public async Task ExplicitHttpClientOwnershipDisposesSuppliedHttpClient()
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        var client = new FreeboxClient(http, new FreeboxClientOptions { ApiVersion = 16 }, disposeHttpClient: true);

        client.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(() => http.GetAsync("http://fixture.example/probe/"));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task WrappersSharingHttpClientApplyIndependentTimeouts()
    {
        var bothEntered = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var release = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var enteredCount = 0;
        using var handler = new FixtureHandler(async (_, cancellationToken) =>
        {
            if (Interlocked.Increment(ref enteredCount) == 2) bothEntered.TrySetResult();
            await release.Task.WaitAsync(cancellationToken);
            return FixtureHandler.Json("""{"success":true,"result":{"value":"accepted"}}""");
        });
        using var http = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(30) };
        using var shortClient = Client(TimeSpan.FromMilliseconds(250));
        using var longClient = Client(TimeSpan.FromSeconds(20));

        var shortCall = shortClient.Transport.SendAsync(HttpMethod.Get, "short/",
            TestJsonContext.Default.FixtureResult, requiresAuthentication: false);
        var longCall = longClient.Transport.SendAsync(HttpMethod.Get, "long/",
            TestJsonContext.Default.FixtureResult, requiresAuthentication: false);
        try
        {
            await bothEntered.Task.WaitAsync(TimeSpan.FromSeconds(10));
            await Assert.ThrowsAnyAsync<OperationCanceledException>(() => shortCall.WaitAsync(TimeSpan.FromSeconds(10)));
            Assert.False(longCall.IsCompleted);
        }
        finally
        {
            release.TrySetResult();
        }

        Assert.Equal("accepted", (await longCall.WaitAsync(TimeSpan.FromSeconds(10))).Value);
        Assert.Equal(TimeSpan.FromSeconds(30), http.Timeout);
        Assert.Equal(2, handler.Requests.Count);

        FreeboxClient Client(TimeSpan timeout) => new(http, new FreeboxClientOptions
        {
            ServerAddress = new Uri("http://fixture.example/"),
            ApiVersion = 16,
            Timeout = timeout
        });
    }

    [Fact]
    public async Task ConfiguredTimeoutAlsoCancelsReadingResponseBodyAndDisposesIt()
    {
        var body = new BlockingReadStream();
        using var handler = new FixtureHandler((_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StreamContent(body)
        }));
        using var http = new HttpClient(handler);
        var client = new FreeboxClient(http, new FreeboxClientOptions
        {
            ServerAddress = new Uri("http://fixture.example/"),
            ApiVersion = 16,
            Timeout = TimeSpan.FromMilliseconds(250)
        });

        var call = client.Transport.SendAsync(HttpMethod.Get, "fixture/", TestJsonContext.Default.FixtureResult,
            requiresAuthentication: false);
        await body.ReadStarted.Task.WaitAsync(TimeSpan.FromSeconds(10));

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => call.WaitAsync(TimeSpan.FromSeconds(10)));
        Assert.True(body.IsDisposed);
        Assert.Single(handler.Requests);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ResponseContentIsDisposedAfterSuccessAndApiFailure(bool success)
    {
        var content = new TrackingContent(success
            ? """{"success":true,"result":{"value":"accepted"}}"""
            : """{"success":false,"error_code":"fixture_error","result":{"value":"diagnostic"}}""");
        using var handler = new FixtureHandler((_, _) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = content
        }));
        using var http = new HttpClient(handler);
        var client = HttpClientTests.CreateClient(http);

        if (success)
        {
            var result = await client.Transport.SendAsync(HttpMethod.Get, "fixture/",
                TestJsonContext.Default.FixtureResult, requiresAuthentication: false);
            Assert.Equal("accepted", result.Value);
        }
        else
        {
            var error = await Assert.ThrowsAsync<FreeboxApiException>(() => client.Transport.SendAsync(
                HttpMethod.Get, "fixture/", TestJsonContext.Default.FixtureResult, requiresAuthentication: false));
            Assert.Equal("diagnostic", error.Details?.GetProperty("value").GetString());
        }

        Assert.True(content.IsDisposed);
        Assert.Single(handler.Requests);
    }

    private sealed class TrackingContent(string body) : StringContent(body, Encoding.UTF8, "application/json")
    {
        public bool IsDisposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }

    private sealed class BlockingReadStream : Stream
    {
        public TaskCompletionSource ReadStarted { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public bool IsDisposed { get; private set; }
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => throw new NotSupportedException();
        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            ReadStarted.TrySetResult();
            await Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
            return 0;
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
            ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();

        public override void Flush() => throw new NotSupportedException();
        public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }
}
