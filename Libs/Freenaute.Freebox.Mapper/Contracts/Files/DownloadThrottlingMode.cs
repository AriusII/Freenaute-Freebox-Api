using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.mode</summary>
[JsonConverter(typeof(JsonStringEnumConverter<DownloadThrottlingMode>))]
public enum DownloadThrottlingMode
{
    [JsonStringEnumMemberName("normal")] Normal,
    [JsonStringEnumMemberName("slow")] Slow,
    [JsonStringEnumMemberName("hibernate")] Hibernate,
    [JsonStringEnumMemberName("schedule")] Schedule,
}
