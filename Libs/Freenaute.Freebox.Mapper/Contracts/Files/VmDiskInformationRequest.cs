using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-disk-info. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record VmDiskInformationRequest
{
    [JsonPropertyName("disk_path")]
    public required EncodedFreeboxPath DiskPath { get; init; }

}
