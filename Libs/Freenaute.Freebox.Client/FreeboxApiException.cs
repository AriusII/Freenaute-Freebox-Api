using System.Net;
using System.Text.Json;

namespace Freenaute.Freebox.Client;

/// <summary>An HTTP failure or a Freebox envelope with <c>success: false</c>.</summary>
public sealed class FreeboxApiException : Exception
{
    public HttpStatusCode? StatusCode { get; }
    public string? ErrorCode { get; }
    public string? ApiMessage { get; }
    public string? ErrorUid { get; }
    public JsonElement? Details { get; }

    internal FreeboxApiException(HttpStatusCode statusCode, string? errorCode = null,
        string? apiMessage = null, string? errorUid = null, JsonElement? details = null)
        : base($"The Freebox request failed (HTTP {(int)statusCode}).")
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
        ApiMessage = apiMessage;
        ErrorUid = errorUid;
        Details = details;
    }
}

/// <summary>An authenticated operation requires configured credentials or an open session.</summary>
public sealed class FreeboxAuthenticationException(string message) : InvalidOperationException(message);
