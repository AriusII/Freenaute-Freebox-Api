using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#SwitchPortStatus.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class SwitchPortStatus
{
    /// <summary>switch port id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>link Description up port is up down port is down</summary>
    [JsonPropertyName("link")]
    public string? Link { get; init; }

    /// <summary>duplex Description half force in half duplex mode full force in full duplex mode</summary>
    [JsonPropertyName("duplex")]
    public string? Duplex { get; init; }

    /// <summary>duplex Description 10 10Base-T 100 100Base-TX 1000 1000Base-T</summary>
    [JsonPropertyName("speed")]
    public string? Speed { get; init; }

    /// <summary>display form of speed and duplex mode</summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; init; }

    /// <summary>list of { mac, name } of hosts connected to this port</summary>
    [JsonPropertyName("mac_list")]
    public SwitchMacEntry[]? MacList { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#SwitchPortConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class SwitchPortConfig
{
    /// <summary>switch port id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>duplex Description auto auto negotiate duplex mode half force in half duplex mode full force in full duplex mode</summary>
    [JsonPropertyName("duplex")]
    public string? Duplex { get; init; }

    /// <summary>duplex Description auto auto negotiate speed 10 10Base-T 100 100Base-TX 1000 1000Base-T</summary>
    [JsonPropertyName("speed")]
    public string? Speed { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#SwitchPortStats.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class SwitchPortStats
{
    [JsonPropertyName("rx_bad_bytes")]
    public long? RxBadBytes { get; init; }

    [JsonPropertyName("rx_broadcast_packets")]
    public long? RxBroadcastPackets { get; init; }

    [JsonPropertyName("rx_bytes_rate")]
    public long? RxBytesRate { get; init; }

    [JsonPropertyName("rx_err_packets")]
    public long? RxErrPackets { get; init; }

    [JsonPropertyName("rx_fcs_packets")]
    public long? RxFcsPackets { get; init; }

    [JsonPropertyName("rx_fragments_packets")]
    public long? RxFragmentsPackets { get; init; }

    [JsonPropertyName("rx_good_bytes")]
    public long? RxGoodBytes { get; init; }

    [JsonPropertyName("rx_good_packets")]
    public long? RxGoodPackets { get; init; }

    [JsonPropertyName("rx_jabber_packets")]
    public long? RxJabberPackets { get; init; }

    [JsonPropertyName("rx_multicast_packets")]
    public long? RxMulticastPackets { get; init; }

    [JsonPropertyName("rx_oversize_packets")]
    public long? RxOversizePackets { get; init; }

    [JsonPropertyName("rx_packets_rate")]
    public long? RxPacketsRate { get; init; }

    [JsonPropertyName("rx_pause")]
    public long? RxPause { get; init; }

    [JsonPropertyName("rx_undersize_packets")]
    public long? RxUndersizePackets { get; init; }

    [JsonPropertyName("rx_unicast_packets")]
    public long? RxUnicastPackets { get; init; }

    [JsonPropertyName("tx_broadcast_packets")]
    public long? TxBroadcastPackets { get; init; }

    [JsonPropertyName("tx_bytes")]
    public long? TxBytes { get; init; }

    [JsonPropertyName("tx_bytes_rate")]
    public long? TxBytesRate { get; init; }

    [JsonPropertyName("tx_collisions")]
    public long? TxCollisions { get; init; }

    [JsonPropertyName("tx_deferred")]
    public long? TxDeferred { get; init; }

    [JsonPropertyName("tx_excessive")]
    public long? TxExcessive { get; init; }

    [JsonPropertyName("tx_fcs")]
    public long? TxFcs { get; init; }

    [JsonPropertyName("tx_late")]
    public long? TxLate { get; init; }

    [JsonPropertyName("tx_multicast_packets")]
    public long? TxMulticastPackets { get; init; }

    [JsonPropertyName("tx_multiple")]
    public long? TxMultiple { get; init; }

    [JsonPropertyName("tx_packets")]
    public long? TxPackets { get; init; }

    [JsonPropertyName("tx_packets_rate")]
    public long? TxPacketsRate { get; init; }

    [JsonPropertyName("tx_pause")]
    public long? TxPause { get; init; }

    [JsonPropertyName("tx_single")]
    public long? TxSingle { get; init; }

    [JsonPropertyName("tx_unicast_packets")]
    public long? TxUnicastPackets { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#get--api-v8-switch-status-.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class SwitchMacEntry
{
    [JsonPropertyName("mac")]
    public string? Mac { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("hostname")]
    public string? Hostname { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#SwitchPortConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class SwitchPortConfigurationPatch
{
    /// <summary>duplex Description auto auto negotiate duplex mode half force in half duplex mode full force in full duplex mode</summary>
    [JsonPropertyName("duplex")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Duplex { get; init; }

    /// <summary>duplex Description auto auto negotiate speed 10 10Base-T 100 100Base-TX 1000 1000Base-T</summary>
    [JsonPropertyName("speed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> Speed { get; init; }

}
