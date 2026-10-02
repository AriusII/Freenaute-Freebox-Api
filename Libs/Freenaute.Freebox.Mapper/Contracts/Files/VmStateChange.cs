using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#VmStateChange. Unknown enum strings are preserved.</summary>
public sealed record VmStateChange
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmStateChange.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmStateChange.status</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

}
