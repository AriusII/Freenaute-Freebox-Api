using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#RaidArray.level</summary>
[JsonConverter(typeof(JsonStringEnumConverter<RaidLevel>))]
public enum RaidLevel
{
    [JsonStringEnumMemberName("basic")] Basic,
    [JsonStringEnumMemberName("raid0")] Raid0,
    [JsonStringEnumMemberName("raid1")] Raid1,
    [JsonStringEnumMemberName("raid5")] Raid5,
    [JsonStringEnumMemberName("raid10")] Raid10,
}
