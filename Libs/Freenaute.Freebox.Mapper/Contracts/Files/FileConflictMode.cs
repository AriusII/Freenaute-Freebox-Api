using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>http://mafreebox.freebox.fr/doc/index.html#conflict-resolution</summary>
[JsonConverter(typeof(JsonStringEnumConverter<FileConflictMode>))]
public enum FileConflictMode
{
    [JsonStringEnumMemberName("overwrite")] Overwrite,
    [JsonStringEnumMemberName("both")] Both,
    [JsonStringEnumMemberName("recent")] Recent,
    [JsonStringEnumMemberName("skip")] Skip,
}
