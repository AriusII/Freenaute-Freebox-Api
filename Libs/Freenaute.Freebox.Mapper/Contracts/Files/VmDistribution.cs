using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#VmDistribution. Unknown enum strings are preserved.</summary>
public sealed record VmDistribution
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDistribution.name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDistribution.url</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDistribution.hash</summary>
    [JsonPropertyName("hash")]
    public string? Hash { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDistribution.os</summary>
    [JsonPropertyName("os")]
    public string? Os { get; init; }

}
