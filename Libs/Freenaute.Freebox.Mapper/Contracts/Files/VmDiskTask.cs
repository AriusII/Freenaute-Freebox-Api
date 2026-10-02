using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#VmDiskTask. Unknown enum strings are preserved.</summary>
public sealed record VmDiskTask
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.type</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.done</summary>
    [JsonPropertyName("done")]
    public bool? Done { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.error</summary>
    [JsonPropertyName("error")]
    public bool? Error { get; init; }

}
