using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#RaidDisk. Unknown enum strings are preserved.</summary>
public sealed record RaidDisk
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidDisk.model</summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidDisk.serial</summary>
    [JsonPropertyName("serial")]
    public string? Serial { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidDisk.firmware</summary>
    [JsonPropertyName("firmware")]
    public string? Firmware { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidDisk.temp</summary>
    [JsonPropertyName("temp")]
    public long? Temp { get; init; }

}
