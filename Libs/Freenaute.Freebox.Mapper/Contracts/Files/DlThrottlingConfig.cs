using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig. Unknown enum strings are preserved.</summary>
public sealed record DlThrottlingConfig
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.normal</summary>
    [JsonPropertyName("normal")]
    public DlRate? Normal { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.slow</summary>
    [JsonPropertyName("slow")]
    public DlRate? Slow { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.schedule</summary>
    [JsonPropertyName("schedule")]
    public string[]? Schedule { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.mode</summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; init; }

}
