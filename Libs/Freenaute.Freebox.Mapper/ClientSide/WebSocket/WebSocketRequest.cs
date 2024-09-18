using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ClientSide.WebSocket;

/// <summary>
///     Represents a WebSocket request.
/// </summary>
/// <param name="RequestId">
///     The optional request ID. If specified, it will be added in the corresponding reply to correlate
///     responses to the request.
/// </param>
/// <param name="Action">The action of the request.</param>
public sealed record WebSocketRequest(
    [property: JsonPropertyName("req_id")] int RequestId,
    [property: JsonPropertyName("action")] string Action);
