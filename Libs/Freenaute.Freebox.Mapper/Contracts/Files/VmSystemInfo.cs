using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo. Unknown enum strings are preserved.</summary>
public sealed record VmSystemInfo
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.total_memory</summary>
    [JsonPropertyName("total_memory")]
    public long? TotalMemory { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.used_memory</summary>
    [JsonPropertyName("used_memory")]
    public long? UsedMemory { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.total_cpus</summary>
    [JsonPropertyName("total_cpus")]
    public long? TotalCpus { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.used_cpus</summary>
    [JsonPropertyName("used_cpus")]
    public long? UsedCpus { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.usb_ports</summary>
    [JsonPropertyName("usb_ports")]
    public string[]? UsbPorts { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.usb_used</summary>
    [JsonPropertyName("usb_used")]
    public bool? UsbUsed { get; init; }

}
