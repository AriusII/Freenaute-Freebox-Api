using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v8-storage-raid-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record CreateRaidRequest
{
    [JsonPropertyName("level")]
    public required RaidLevel Level { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("members")]
    public required RaidMemberId[] Members { get; init; }

}
