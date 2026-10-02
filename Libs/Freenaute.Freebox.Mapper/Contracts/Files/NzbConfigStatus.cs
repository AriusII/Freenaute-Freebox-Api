using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#NzbConfigStatus. Unknown enum strings are preserved.</summary>
public sealed record NzbConfigStatus
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#NzbConfigStatus.status</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#NzbConfigStatus.error</summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }

}
