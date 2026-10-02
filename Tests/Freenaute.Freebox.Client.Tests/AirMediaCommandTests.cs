using System.Globalization;
using System.Text.Json;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class AirMediaCommandTests
{
    [Theory]
    [InlineData(true, "video")]
    [InlineData(false, "photo")]
    public async Task StopCommandOmitsMediaAndPostsOnlyRequiredFields(bool video, string mediaType)
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var receiver = client.AirMedia.Receiver("Séjour");
        var command = video ? receiver.StopVideo() : receiver.StopPhoto();

        Assert.Empty(handler.Requests);
        await command.SendAsync();

        var request = handler.Requests.Last();
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("https://fixture.example/api/v16/airmedia/receivers/S%C3%A9jour/", request.Address.AbsoluteUri);
        using var body = JsonDocument.Parse(request.Body!);
        Assert.Equal("stop", body.RootElement.GetProperty("action").GetString());
        Assert.Equal(mediaType, body.RootElement.GetProperty("media_type").GetString());
        Assert.Equal(2, body.RootElement.EnumerateObject().Count());
    }

    [Theory]
    [InlineData("0", 0)]
    [InlineData("0.001", 1)]
    [InlineData("50", 50_000)]
    [InlineData("50.123", 50_123)]
    [InlineData("99.999", 99_999)]
    [InlineData("100", 100_000)]
    public async Task PercentagePositionIsConvertedExactlyToThousandths(string percentage, int expectedWirePosition)
    {
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var original = client.AirMedia.Receiver("TV").PlayVideo("https://media.example/video.mp4");
        var positioned = original.AtPercent(decimal.Parse(percentage, CultureInfo.InvariantCulture));

        Assert.Empty(handler.Requests);
        await positioned.SendAsync();
        using var positionedBody = JsonDocument.Parse(handler.Requests.Last().Body!);
        Assert.Equal(expectedWirePosition, positionedBody.RootElement.GetProperty("position").GetInt32());

        await original.SendAsync();
        using var originalBody = JsonDocument.Parse(handler.Requests.Last().Body!);
        Assert.False(originalBody.RootElement.TryGetProperty("position", out _));
    }

    [Theory]
    [InlineData("-0.001")]
    [InlineData("100.001")]
    public void PercentageOutsideDocumentedRangeFailsBeforeHttp(string percentage)
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var command = client.AirMedia.Receiver("TV").PlayVideo("https://media.example/video.mp4");

        Assert.Throws<ArgumentOutOfRangeException>(() => command.AtPercent(decimal.Parse(percentage, CultureInfo.InvariantCulture)));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public void PercentageCannotSilentlyRoundAnUnrepresentablePosition()
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var command = client.AirMedia.Receiver("TV").PlayVideo("https://media.example/video.mp4");

        Assert.Throws<ArgumentException>(() => command.AtPercent(50.1234m));
        Assert.Empty(handler.Requests);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(100_001)]
    public void RawPositionOutsideDocumentedRangeFailsBeforeHttp(int position)
    {
        using var handler = FixtureHandler.Constant("{}");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);
        var command = client.AirMedia.Receiver("TV").PlayVideo("https://media.example/video.mp4");

        Assert.Throws<ArgumentOutOfRangeException>(() => command.At(position));
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task PhotoCommandPreservesEncodedFilesystemPath()
    {
        // The SDK accepts the exact Base64 filesystem path returned by its filesystem API.
        const string encodedPath = "L0Rpc3F1ZSBkdXIvcGhvdG9zL8OpdMOpLmpwZw==";
        using var handler = FixtureHandler.AuthenticatedConstant("""{"success":true}""");
        using var http = new HttpClient(handler);
        using var client = HttpClientTests.CreateClient(http);

        await client.AirMedia.Receiver("TV").ShowPhoto(encodedPath).SendAsync();

        using var body = JsonDocument.Parse(handler.Requests.Last().Body!);
        Assert.Equal("photo", body.RootElement.GetProperty("media_type").GetString());
        Assert.Equal(encodedPath, body.RootElement.GetProperty("media").GetString());
    }
}
