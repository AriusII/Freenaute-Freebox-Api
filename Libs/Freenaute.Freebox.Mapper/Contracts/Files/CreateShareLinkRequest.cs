using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v8-share_link-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record CreateShareLinkRequest
{
    [JsonPropertyName("path")]
    public required string Path { get; init; }

    [JsonPropertyName("expire")]
    public required long Expire { get; init; }

}
