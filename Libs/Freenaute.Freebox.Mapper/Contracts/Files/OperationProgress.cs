using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#OperationProgress. Unknown enum strings are preserved.</summary>
public sealed record OperationProgress
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#OperationProgress.done_steps</summary>
    [JsonPropertyName("done_steps")]
    public long? DoneSteps { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#OperationProgress.max_steps</summary>
    [JsonPropertyName("max_steps")]
    public long? MaxSteps { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#OperationProgress.percent</summary>
    [JsonPropertyName("percent")]
    public long? Percent { get; init; }

}
