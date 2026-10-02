using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DlFeedConfig. Unknown enum strings are preserved.</summary>
public sealed record DlFeedConfig
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlFeedConfig.fetch_interval</summary>
    [JsonPropertyName("fetch_interval")]
    public long? FetchInterval { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlFeedConfig.max_items</summary>
    [JsonPropertyName("max_items")]
    public long? MaxItems { get; init; }

}
