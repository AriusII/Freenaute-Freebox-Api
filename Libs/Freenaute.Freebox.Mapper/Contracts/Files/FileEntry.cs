using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#FileInfo. Unknown enum strings are preserved.</summary>
public sealed record FileEntry
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.path</summary>
    [JsonPropertyName("path")]
    public EncodedFreeboxPath? Path { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.mimetype</summary>
    [JsonPropertyName("mimetype")]
    public string? Mimetype { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.type</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.size</summary>
    [JsonPropertyName("size")]
    public long? Size { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.modification</summary>
    [JsonPropertyName("modification")]
    public long? Modification { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.index</summary>
    [JsonPropertyName("index")]
    public long? Index { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.link</summary>
    [JsonPropertyName("link")]
    public bool? Link { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.target</summary>
    [JsonPropertyName("target")]
    public EncodedFreeboxPath? Target { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.hidden</summary>
    [JsonPropertyName("hidden")]
    public bool? Hidden { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.foldercount</summary>
    [JsonPropertyName("foldercount")]
    public long? Foldercount { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.filecount</summary>
    [JsonPropertyName("filecount")]
    public long? Filecount { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileInfo.exif</summary>
    [JsonPropertyName("exif")]
    public JsonElement? Exif { get; init; }

}
