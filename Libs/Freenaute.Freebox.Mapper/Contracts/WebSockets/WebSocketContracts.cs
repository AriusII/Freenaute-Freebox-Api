using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.WebSockets;

/// <summary>RegisterAction and common WebSocketRequest. Outgoing events are validated by the event builder.</summary>
public sealed class RegisterEventsRequest
{
    [JsonPropertyName("action")]
    public string Action => "register";
    [JsonPropertyName("request_id")]
    public long RequestId { get; init; }
    [JsonPropertyName("events")]
    public string[] Events { get; init; } = [];
}

/// <summary>Common response/notification envelope. Result is explicitly extensible and may be omitted.</summary>
public sealed class FreeboxWebSocketMessage
{
    [JsonPropertyName("request_id")]
    public long? RequestId { get; init; }
    [JsonPropertyName("action")]
    public string? Action { get; init; }
    [JsonPropertyName("success")]
    public bool? Success { get; init; }
    [JsonPropertyName("source")]
    public string? Source { get; init; }
    [JsonPropertyName("event")]
    public string? Event { get; init; }
    [JsonPropertyName("result")]
    public JsonElement? Result { get; init; }
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; init; }
    [JsonPropertyName("msg")]
    public string? Message { get; init; }
}

/// <summary>FileUploadStartAction. Size is optional and supplied by the caller, including during resume.</summary>
public sealed class FileUploadStartRequest
{
    [JsonPropertyName("request_id")]
    public long RequestId { get; init; }
    [JsonPropertyName("action")]
    public string Action => "upload_start";
    [JsonPropertyName("size")]
    public long? Size { get; init; }
    [JsonPropertyName("dirname")]
    public EncodedFreeboxPath Directory { get; init; }
    [JsonPropertyName("filename")]
    public string Filename { get; init; } = "";
    [JsonPropertyName("force")]
    public string? Force { get; init; }
}

public sealed class FileUploadFinalizeRequest
{
    [JsonPropertyName("request_id")]
    public long RequestId { get; init; }
    [JsonPropertyName("action")]
    public string Action => "upload_finalize";
}

/// <summary>Explicit destructive cancellation. Unlike closing the connection, this deletes the partial file.</summary>
public sealed class FileUploadCancelRequest
{
    [JsonPropertyName("request_id")]
    public long RequestId { get; init; }
    [JsonPropertyName("action")]
    public string Action => "upload_cancel";
}

/// <summary>FileUploadChunkResponse; the final/cancel response has complete=true.</summary>
public sealed class FileUploadChunkResult
{
    [JsonPropertyName("total_len")]
    public long? TotalLength { get; init; }
    [JsonPropertyName("complete")]
    public bool? Complete { get; init; }
    [JsonPropertyName("cancelled")]
    public bool? Cancelled { get; init; }
}

/// <summary>Upload-specific envelope. file_size is documented on a destination-conflict start response.</summary>
public sealed class FileUploadControlResponse
{
    [JsonPropertyName("request_id")]
    public long? RequestId { get; init; }
    [JsonPropertyName("action")]
    public string? Action { get; init; }
    [JsonPropertyName("success")]
    public bool? Success { get; init; }
    [JsonPropertyName("result")]
    public FileUploadChunkResult? Result { get; init; }
    [JsonPropertyName("file_size")]
    public long? FileSize { get; init; }
    [JsonPropertyName("error_code")]
    public string? ErrorCode { get; init; }
    [JsonPropertyName("msg")]
    public string? Message { get; init; }
}
