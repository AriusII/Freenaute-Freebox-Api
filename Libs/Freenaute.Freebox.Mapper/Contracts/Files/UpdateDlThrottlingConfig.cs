using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record UpdateDlThrottlingConfig
{
    [JsonPropertyName("normal")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<UpdateDlRate>))]
    public Optional<UpdateDlRate> Normal { get; init; }

    [JsonPropertyName("slow")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<UpdateDlRate>))]
    public Optional<UpdateDlRate> Slow { get; init; }

    [JsonPropertyName("schedule")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<DownloadScheduleMode[]>))]
    public Optional<DownloadScheduleMode[]> Schedule { get; init; }

    [JsonPropertyName("mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<DownloadThrottlingMode>))]
    public Optional<DownloadThrottlingMode> Mode { get; init; }

}
