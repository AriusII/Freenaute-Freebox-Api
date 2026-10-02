using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-cat-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record ConcatenateFilesRequest
{
    [JsonPropertyName("files")]
    public required EncodedFreeboxPath[] Files { get; init; }

    [JsonPropertyName("dst")]
    public required EncodedFreeboxPath Dst { get; init; }

    [JsonPropertyName("multi_volumes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> MultiVolumes { get; init; }

    [JsonPropertyName("delete_files")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> DeleteFiles { get; init; }

    [JsonPropertyName("overwrite")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Overwrite { get; init; }

    [JsonPropertyName("append")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Append { get; init; }

}
