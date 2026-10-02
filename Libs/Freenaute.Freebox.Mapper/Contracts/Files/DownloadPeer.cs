using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DownloadPeer. Unknown enum strings are preserved.</summary>
public sealed record DownloadPeer
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.host</summary>
    [JsonPropertyName("host")]
    public string? Host { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.port</summary>
    [JsonPropertyName("port")]
    public long? Port { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.state</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.origin</summary>
    [JsonPropertyName("origin")]
    public string? Origin { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.protocol</summary>
    [JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.client</summary>
    [JsonPropertyName("client")]
    public string? Client { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.country_code</summary>
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.tx</summary>
    [JsonPropertyName("tx")]
    public long? Tx { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.rx</summary>
    [JsonPropertyName("rx")]
    public long? Rx { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.tx_rate</summary>
    [JsonPropertyName("tx_rate")]
    public long? TxRate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.rx_rate</summary>
    [JsonPropertyName("rx_rate")]
    public long? RxRate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.progress</summary>
    [JsonPropertyName("progress")]
    public long? Progress { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.requests</summary>
    [JsonPropertyName("requests")]
    public EmptyObjectOrInt32Array? Requests { get; init; }

}
