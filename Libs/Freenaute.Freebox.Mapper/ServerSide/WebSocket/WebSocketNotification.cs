using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.ServerSide.WebSocket;

/// <summary>
///     Represents a WebSocket notification.
/// </summary>
/// <param name="Action">The action of the notification. Will have the value 'notification'.</param>
/// <param name="Success">Will be true.</param>
/// <param name="Source">The name of the source of the notification.</param>
/// <param name="Event">The name of the event that generated the notification.</param>
/// <param name="Result">
///     The content of the notification. May be omitted if no data has to be transferred along with the
///     notification.
/// </param>
public sealed record WebSocketNotification(
    [property: JsonPropertyName("action")] string Action,
    [property: JsonPropertyName("success")]
    bool Success,
    [property: JsonPropertyName("source")] string Source,
    [property: JsonPropertyName("event")] string Event,
    [property: JsonPropertyName("result")] object Result);
