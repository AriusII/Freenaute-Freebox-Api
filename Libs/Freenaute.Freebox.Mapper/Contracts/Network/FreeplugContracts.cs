using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#FreeplugNetwork.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class FreeplugNetwork
{
    /// <summary>Network unique id</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>List of freeplugs member of this network</summary>
    [JsonPropertyName("members")]
    public Freeplug[]? Members { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#Freeplug.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class Freeplug
{
    /// <summary>Freeplug unique id</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>if true the Freeplug is connected directly to the Freebox</summary>
    [JsonPropertyName("local")]
    public bool? Local { get; init; }

    /// <summary>Freeplug network role Type Description sta Freeplug Station pco Freeplug proxy coordinator cco Central coordinator</summary>
    [JsonPropertyName("net_role")]
    public string? NetRole { get; init; }

    /// <summary>Freebox Server netbios name</summary>
    [JsonPropertyName("model")]
    public string? Model { get; init; }

    /// <summary>Type Description up The ethernet port is up down The ethernet port is down unknown The ethernet port state is unknown</summary>
    [JsonPropertyName("eth_port_status")]
    public string? EthPortStatus { get; init; }

    /// <summary>ethernet link is full duplex</summary>
    [JsonPropertyName("eth_full_duplex")]
    public bool? EthFullDuplex { get; init; }

    /// <summary>is connected to the network</summary>
    [JsonPropertyName("has_network")]
    public bool? HasNetwork { get; init; }

    /// <summary>ethernet port speed</summary>
    [JsonPropertyName("eth_speed")]
    public long? EthSpeed { get; init; }

    /// <summary>seconds since last activity</summary>
    [JsonPropertyName("inactive")]
    public long? Inactive { get; init; }

    /// <summary>network id</summary>
    [JsonPropertyName("net_id")]
    public string? NetId { get; init; }

    /// <summary>rx rate (from the freeplugs to the “cco” freeplug) (in Mb/s) -1 if not available</summary>
    [JsonPropertyName("rx_rate")]
    public long? RxRate { get; init; }

    /// <summary>tx rate (from the “cco” freeplug to the freeplugs) (in Mb/s) -1 if not available</summary>
    [JsonPropertyName("tx_rate")]
    public long? TxRate { get; init; }

}
