using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-partition-id-check-</summary>
[JsonConverter(typeof(JsonStringEnumConverter<PartitionCheckMode>))]
public enum PartitionCheckMode
{
    [JsonStringEnumMemberName("ro")] Ro,
    [JsonStringEnumMemberName("rw")] Rw,
}
