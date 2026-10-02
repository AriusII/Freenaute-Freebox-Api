using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.schedule</summary>
[JsonConverter(typeof(JsonStringEnumConverter<DownloadScheduleMode>))]
public enum DownloadScheduleMode
{
    [JsonStringEnumMemberName("normal")] Normal,
    [JsonStringEnumMemberName("slow")] Slow,
    [JsonStringEnumMemberName("hibernate")] Hibernate,
}
