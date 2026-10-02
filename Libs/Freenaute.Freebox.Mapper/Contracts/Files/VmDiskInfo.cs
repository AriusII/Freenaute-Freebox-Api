using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo. Unknown enum strings are preserved.</summary>
public sealed record VmDiskInfo
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo.type</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo.actual_size</summary>
    [JsonPropertyName("actual_size")]
    public long? ActualSize { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo.virtual_size</summary>
    [JsonPropertyName("virtual_size")]
    public long? VirtualSize { get; init; }

}
