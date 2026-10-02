using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#FileUpload. Unknown enum strings are preserved.</summary>
public sealed record FileUpload
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileUpload.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileUpload.size</summary>
    [JsonPropertyName("size")]
    public long? Size { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileUpload.uploaded</summary>
    [JsonPropertyName("uploaded")]
    public long? Uploaded { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileUpload.status</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileUpload.start_date</summary>
    [JsonPropertyName("start_date")]
    public long? StartDate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileUpload.last_update</summary>
    [JsonPropertyName("last_update")]
    public long? LastUpdate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileUpload.upload_name</summary>
    [JsonPropertyName("upload_name")]
    public string? UploadName { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FileUpload.dirname</summary>
    [JsonPropertyName("dirname")]
    public string? Dirname { get; init; }

}
