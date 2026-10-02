using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DhtStats. Unknown enum strings are preserved.</summary>
public sealed record DhtStats
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DhtStats.enabled</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DhtStats.node_count</summary>
    [JsonPropertyName("node_count")]
    public long? NodeCount { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DhtStats.enabled_ipv6</summary>
    [JsonPropertyName("enabled_ipv6")]
    public bool? EnabledIpv6 { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DhtStats.node_count_ipv6</summary>
    [JsonPropertyName("node_count_ipv6")]
    public long? NodeCountIpv6 { get; init; }

}
