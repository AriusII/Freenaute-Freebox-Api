using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-raid-id. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record SetRaidStateRequest
{
    [JsonPropertyName("id")]
    public required long Id { get; init; }

    [JsonPropertyName("state")]
    public required RaidState State { get; init; }

}
