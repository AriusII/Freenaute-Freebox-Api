namespace Freenaute.Freebox.Client.Domains.WebSockets;

/// <summary>Structured server rejection. The exception message excludes untrusted server text.</summary>
public sealed class FreeboxWebSocketApiException : Exception
{
    public string? Action { get; }
    public long? RequestId { get; }
    public string? ErrorCode { get; }
    public string? ApiMessage { get; }
    public long? ExistingFileSize { get; }

    internal FreeboxWebSocketApiException(string? action, long? requestId, string? errorCode,
        string? apiMessage, long? existingFileSize = null) : base("The Freebox rejected a WebSocket operation.")
    {
        Action = action;
        RequestId = requestId;
        ErrorCode = errorCode;
        ApiMessage = apiMessage;
        ExistingFileSize = existingFileSize;
    }
}
