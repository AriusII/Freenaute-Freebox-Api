using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Protocol;

public sealed record Camera
{
    [JsonPropertyName("id")] public string? Id { get; init; }
    [JsonPropertyName("node_id")] public long NodeId { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("stream_url")] public string? StreamUrl { get; init; }
    [JsonPropertyName("lan_gid")] public string? LanId { get; init; }
}
