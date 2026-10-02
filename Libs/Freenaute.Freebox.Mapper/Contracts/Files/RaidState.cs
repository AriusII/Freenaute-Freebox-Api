using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-raid-id</summary>
[JsonConverter(typeof(JsonStringEnumConverter<RaidState>))]
public enum RaidState
{
    [JsonStringEnumMemberName("stopped")] Stopped,
    [JsonStringEnumMemberName("running")] Running,
}
