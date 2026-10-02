using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#VM. Unknown enum strings are preserved.</summary>
public sealed record VirtualMachine
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.name</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.disk_path</summary>
    [JsonPropertyName("disk_path")]
    public EncodedFreeboxPath? DiskPath { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.disk_type</summary>
    [JsonPropertyName("disk_type")]
    public string? DiskType { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.cd_path</summary>
    [JsonPropertyName("cd_path")]
    public EncodedFreeboxPath? CdPath { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.memory</summary>
    [JsonPropertyName("memory")]
    public long? Memory { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.vcpus</summary>
    [JsonPropertyName("vcpus")]
    public long? Vcpus { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.status</summary>
    [JsonPropertyName("status")]
    public string? Status { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.enable_screen</summary>
    [JsonPropertyName("enable_screen")]
    public bool? EnableScreen { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.bind_usb_ports</summary>
    [JsonPropertyName("bind_usb_ports")]
    public string[]? BindUsbPorts { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.enable_cloudinit</summary>
    [JsonPropertyName("enable_cloudinit")]
    public bool? EnableCloudinit { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.cloudinit_hostname</summary>
    [JsonPropertyName("cloudinit_hostname")]
    public string? CloudinitHostname { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.cloudinit_userdata</summary>
    [JsonPropertyName("cloudinit_userdata")]
    public string? CloudinitUserdata { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.mac</summary>
    [JsonPropertyName("mac")]
    public string? Mac { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VM.os</summary>
    [JsonPropertyName("os")]
    public string? Os { get; init; }

}
