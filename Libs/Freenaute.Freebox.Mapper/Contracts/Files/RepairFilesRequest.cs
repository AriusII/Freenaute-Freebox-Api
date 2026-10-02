using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-repair-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record RepairFilesRequest
{
    [JsonPropertyName("src")]
    public required EncodedFreeboxPath Src { get; init; }

    [JsonPropertyName("delete_archive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> DeleteArchive { get; init; }

}
