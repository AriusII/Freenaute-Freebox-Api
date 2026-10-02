using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#RRDFetch.db</summary>
[JsonConverter(typeof(JsonStringEnumConverter<RrdDatabase>))]
public enum RrdDatabase
{
    [JsonStringEnumMemberName("net")] Net,
    [JsonStringEnumMemberName("temp")] Temp,
    [JsonStringEnumMemberName("dsl")] Dsl,
    [JsonStringEnumMemberName("switch")] Switch,
}
