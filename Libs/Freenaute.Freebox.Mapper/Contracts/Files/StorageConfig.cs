using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#StorageConfig. Unknown enum strings are preserved.</summary>
public sealed record StorageConfig
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageConfig.external_pm_enabled</summary>
    [JsonPropertyName("external_pm_enabled")]
    public bool? ExternalPmEnabled { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageConfig.external_pm_idle_before_spindown</summary>
    [JsonPropertyName("external_pm_idle_before_spindown")]
    public long? ExternalPmIdleBeforeSpindown { get; init; }

}
