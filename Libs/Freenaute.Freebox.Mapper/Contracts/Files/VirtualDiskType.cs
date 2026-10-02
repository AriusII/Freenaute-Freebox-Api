using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.disk_type</summary>
[JsonConverter(typeof(JsonStringEnumConverter<VirtualDiskType>))]
public enum VirtualDiskType
{
    [JsonStringEnumMemberName("raw")] Raw,
    [JsonStringEnumMemberName("qcow2")] Qcow2,
}
