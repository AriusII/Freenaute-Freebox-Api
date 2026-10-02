using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig. Unknown enum strings are preserved.</summary>
public sealed record DlNewsConfig
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.server</summary>
    [JsonPropertyName("server")]
    public string? Server { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.port</summary>
    [JsonPropertyName("port")]
    public StringOrInteger? Port { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.ssl</summary>
    [JsonPropertyName("ssl")]
    public bool? Ssl { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.user</summary>
    [JsonPropertyName("user")]
    public string? User { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.nthreads</summary>
    [JsonPropertyName("nthreads")]
    public long? Nthreads { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.auto_repair</summary>
    [JsonPropertyName("auto_repair")]
    public bool? AutoRepair { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.lazy_par2</summary>
    [JsonPropertyName("lazy_par2")]
    public bool? LazyPar2 { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.auto_extract</summary>
    [JsonPropertyName("auto_extract")]
    public bool? AutoExtract { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.erase_tmp</summary>
    [JsonPropertyName("erase_tmp")]
    public bool? EraseTmp { get; init; }

}
