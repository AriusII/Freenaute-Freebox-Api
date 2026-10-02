using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DownloadFile. Unknown enum strings are preserved.</summary>
public sealed record DownloadFile
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.id</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.task_id</summary>
    [JsonPropertyName("task_id")]
    public StringOrInteger? TaskId { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.filepath</summary>
    [JsonPropertyName("filepath")]
    public EncodedFreeboxPath? Filepath { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.mimetype</summary>
    [JsonPropertyName("mimetype")]
    public string? Mimetype { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.size</summary>
    [JsonPropertyName("size")]
    public long? Size { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.rx</summary>
    [JsonPropertyName("rx")]
    public long? Rx { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.status</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.error</summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.priority</summary>
    [JsonPropertyName("priority")]
    public string? Priority { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadFile.preview_url</summary>
    [JsonPropertyName("preview_url")]
    public string? PreviewUrl { get; init; }

}
