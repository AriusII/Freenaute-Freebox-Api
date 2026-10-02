using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Read projection of the documented TftpConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#TftpConfig" />
public sealed record TftpConfig
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("root")]
    public string? Root { get; init; }

    public override string ToString() => nameof(TftpConfig);
}
