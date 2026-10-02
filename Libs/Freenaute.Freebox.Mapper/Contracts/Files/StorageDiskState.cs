using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#StorageDisk.state</summary>
[JsonConverter(typeof(JsonStringEnumConverter<StorageDiskState>))]
public enum StorageDiskState
{
    [JsonStringEnumMemberName("error")] Error,
    [JsonStringEnumMemberName("disabled")] Disabled,
    [JsonStringEnumMemberName("enabled")] Enabled,
    [JsonStringEnumMemberName("formatting")] Formatting,
}
