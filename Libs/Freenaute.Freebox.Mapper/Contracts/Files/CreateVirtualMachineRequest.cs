using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Write contract from http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-. Unset patch fields are omitted; null clearing is not promised.</summary>
public sealed record CreateVirtualMachineRequest
{
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Name { get; init; }

    [JsonPropertyName("disk_path")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<EncodedFreeboxPath>))]
    public Optional<EncodedFreeboxPath> DiskPath { get; init; }

    [JsonPropertyName("disk_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<VirtualDiskType>))]
    public Optional<VirtualDiskType> DiskType { get; init; }

    [JsonPropertyName("cd_path")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<EncodedFreeboxPath>))]
    public Optional<EncodedFreeboxPath> CdPath { get; init; }

    [JsonPropertyName("memory")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Memory { get; init; }

    [JsonPropertyName("vcpus")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<long>))]
    public Optional<long> Vcpus { get; init; }

    [JsonPropertyName("enable_screen")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> EnableScreen { get; init; }

    [JsonPropertyName("bind_usb_ports")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string[]>))]
    public Optional<string[]> BindUsbPorts { get; init; }

    [JsonPropertyName("enable_cloudinit")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> EnableCloudinit { get; init; }

    [JsonPropertyName("cloudinit_hostname")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> CloudinitHostname { get; init; }

    [JsonPropertyName("cloudinit_userdata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> CloudinitUserdata { get; init; }

    [JsonPropertyName("os")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Os { get; init; }

}
