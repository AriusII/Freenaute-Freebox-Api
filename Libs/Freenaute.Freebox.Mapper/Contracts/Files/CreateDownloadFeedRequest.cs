using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record CreateDownloadFeedRequest
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }

}
