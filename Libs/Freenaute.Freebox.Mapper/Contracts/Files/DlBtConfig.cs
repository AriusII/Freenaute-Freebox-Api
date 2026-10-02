using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DlBtConfig. Unknown enum strings are preserved.</summary>
public sealed record DlBtConfig
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.max_peers</summary>
    [JsonPropertyName("max_peers")]
    public long? MaxPeers { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.stop_ratio</summary>
    [JsonPropertyName("stop_ratio")]
    public long? StopRatio { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.crypto_support</summary>
    [JsonPropertyName("crypto_support")]
    public string? CryptoSupport { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.enable_dht</summary>
    [JsonPropertyName("enable_dht")]
    public bool? EnableDht { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.enable_pex</summary>
    [JsonPropertyName("enable_pex")]
    public bool? EnablePex { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.announce_timeout</summary>
    [JsonPropertyName("announce_timeout")]
    public long? AnnounceTimeout { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.main_port</summary>
    [JsonPropertyName("main_port")]
    public long? MainPort { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.dht_port</summary>
    [JsonPropertyName("dht_port")]
    public long? DhtPort { get; init; }

}
