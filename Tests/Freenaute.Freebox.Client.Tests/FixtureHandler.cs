using System.Collections.Concurrent;
using System.Net;
using System.Text;

namespace Freenaute.Freebox.Client.Tests;

internal sealed record CapturedRequest(HttpMethod Method, Uri Address, string? SessionToken,
    string? Body, string? ContentType);

internal sealed class FixtureHandler(
    Func<CapturedRequest, CancellationToken, Task<HttpResponseMessage>> respond) : HttpMessageHandler
{
    public ConcurrentQueue<CapturedRequest> Requests { get; } = new();

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var capture = new CapturedRequest(request.Method, request.RequestUri!,
            request.Headers.TryGetValues("X-Fbx-App-Auth", out var tokens) ? tokens.Single() : null,
            request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken),
            request.Content?.Headers.ContentType?.MediaType);
        Requests.Enqueue(capture);
        return await respond(capture, cancellationToken);
    }

    public static HttpResponseMessage Json(string json, HttpStatusCode status = HttpStatusCode.OK) =>
        new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

    public static FixtureHandler Constant(string json, HttpStatusCode status = HttpStatusCode.OK) =>
        new((_, _) => Task.FromResult(Json(json, status)));

    public static FixtureHandler Authenticated(
        Func<CapturedRequest, CancellationToken, Task<HttpResponseMessage>> respond) => new((request, cancellationToken) =>
        request.Address.AbsolutePath.EndsWith("/login/", StringComparison.Ordinal)
            ? Task.FromResult(Json("""{"success":true,"result":{"logged_in":false,"challenge":"fixture-challenge"}}"""))
            : request.Address.AbsolutePath.EndsWith("/login/session/", StringComparison.Ordinal)
                ? Task.FromResult(Json("""{"success":true,"result":{"session_token":"fixture-session","challenge":"next","permissions":{"settings":true}}}"""))
                : respond(request, cancellationToken));

    public static FixtureHandler AuthenticatedConstant(string json, HttpStatusCode status = HttpStatusCode.OK) =>
        Authenticated((_, _) => Task.FromResult(Json(json, status)));
}
