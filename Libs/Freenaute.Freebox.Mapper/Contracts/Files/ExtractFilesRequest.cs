using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-extract-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record ExtractFilesRequest
{
    [JsonPropertyName("src")]
    public required EncodedFreeboxPath Src { get; init; }

    [JsonPropertyName("dst")]
    public required EncodedFreeboxPath Dst { get; init; }

    [JsonPropertyName("password")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Password { get; init; }

    [JsonPropertyName("delete_archive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> DeleteArchive { get; init; }

    [JsonPropertyName("overwrite")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Overwrite { get; init; }

}
