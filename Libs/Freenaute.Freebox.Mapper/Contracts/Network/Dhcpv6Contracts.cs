using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DHCPv6Config.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class DHCPv6Config
{
    /// <summary>Enable/Disable the DHCPv6 server NOTE: on some Android devices, enabling the DHCPv6 server may cause IPv6 to stop working on those devices</summary>
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    /// <summary>if set to true, the user provided IPv6 dns servers will be used instead of Free default IPv6 dns servers NOTE: even if DHCPv6 server is disabled the custom dns can be used to replace Free dns in RA RDNSS</summary>
    [JsonPropertyName("use_custom_dns")]
    public bool? UseCustomDns { get; init; }

    /// <summary>list of ipv6 dns servers to use instead of Free dns servers in case use_custom_dns is set to true</summary>
    [JsonPropertyName("dns")]
    public string[]? Dns { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#DHCPv6Config.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class DhcpV6ConfigurationPatch
{
    /// <summary>Enable/Disable the DHCPv6 server NOTE: on some Android devices, enabling the DHCPv6 server may cause IPv6 to stop working on those devices</summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> Enabled { get; init; }

    /// <summary>if set to true, the user provided IPv6 dns servers will be used instead of Free default IPv6 dns servers NOTE: even if DHCPv6 server is disabled the custom dns can be used to replace Free dns in RA RDNSS</summary>
    [JsonPropertyName("use_custom_dns")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> UseCustomDns { get; init; }

}
