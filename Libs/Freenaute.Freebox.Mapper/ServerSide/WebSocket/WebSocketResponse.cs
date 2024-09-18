using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ServerSide.WebSocket;

/// <summary>
///     Represents a WebSocket response.
/// </summary>
/// <param name="RequestId">
///     The request ID. If set in the WebSocketRequest, the same ID will be returned in the associated
///     response.
/// </param>
/// <param name="Action">The action specified in the associated WebSocketRequest.</param>
/// <param name="Success">Indicates if the request was successful.</param>
/// <param name="Result">The result of the request. May be omitted if the request does not expect any result.</param>
/// <param name="ErrorCode">In case of request error, this error code provides information about the error.</param>
/// <param name="Message">In case of error, provides a French error message relative to the error.</param>
public sealed record WebSocketResponse(
    [property: JsonPropertyName("req_id")] int RequestId,
    [property: JsonPropertyName("action")] string Action,
    [property: JsonPropertyName("success")]
    bool Success,
    [property: JsonPropertyName("result")] object Result,
    [property: JsonPropertyName("error_code")]
    string ErrorCode,
    [property: JsonPropertyName("msg")] string Message);
