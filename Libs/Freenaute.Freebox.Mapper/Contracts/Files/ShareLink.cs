using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#ShareLink. Unknown enum strings are preserved.</summary>
public sealed record ShareLink
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#ShareLink.token</summary>
    [JsonPropertyName("token")]
    public string? Token { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#ShareLink.path</summary>
    [JsonPropertyName("path")]
    public string? Path { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#ShareLink.name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#ShareLink.expire</summary>
    [JsonPropertyName("expire")]
    public long? Expire { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#ShareLink.fullurl</summary>
    [JsonPropertyName("fullurl")]
    public string? Fullurl { get; init; }

}
