using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-config-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record UpdateDownloadConfiguration
{
    [JsonPropertyName("max_downloading_tasks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> MaxDownloadingTasks { get; init; }

    [JsonPropertyName("download_dir")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<EncodedFreeboxPath>))]
    public Optional<EncodedFreeboxPath> DownloadDir { get; init; }

    [JsonPropertyName("watch_dir")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<EncodedFreeboxPath>))]
    public Optional<EncodedFreeboxPath> WatchDir { get; init; }

    [JsonPropertyName("use_watch_dir")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> UseWatchDir { get; init; }

    [JsonPropertyName("throttling")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<UpdateDlThrottlingConfig>))]
    public Optional<UpdateDlThrottlingConfig> Throttling { get; init; }

    [JsonPropertyName("news")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<UpdateDlNewsConfig>))]
    public Optional<UpdateDlNewsConfig> News { get; init; }

    [JsonPropertyName("bt")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<UpdateDlBtConfig>))]
    public Optional<UpdateDlBtConfig> Bt { get; init; }

    [JsonPropertyName("feed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<UpdateDlFeedConfig>))]
    public Optional<UpdateDlFeedConfig> Feed { get; init; }

    [JsonPropertyName("dns1")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Dns1 { get; init; }

    [JsonPropertyName("dns2")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Dns2 { get; init; }

}
