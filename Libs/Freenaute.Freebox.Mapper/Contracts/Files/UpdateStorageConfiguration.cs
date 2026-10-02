using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-config-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record UpdateStorageConfiguration
{
    [JsonPropertyName("external_pm_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> ExternalPmEnabled { get; init; }

    [JsonPropertyName("external_pm_idle_before_spindown")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> ExternalPmIdleBeforeSpindown { get; init; }

}
