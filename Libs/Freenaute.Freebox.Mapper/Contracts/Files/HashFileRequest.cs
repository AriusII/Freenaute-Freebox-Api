using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-hash-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record HashFileRequest
{
    [JsonPropertyName("src")]
    public required EncodedFreeboxPath Src { get; init; }

    [JsonPropertyName("hash_type")]
    public required string HashType { get; init; }

}
