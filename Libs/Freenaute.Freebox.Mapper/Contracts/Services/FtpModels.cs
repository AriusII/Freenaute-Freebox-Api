using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Read projection of the documented FtpConfig wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#FtpConfig" />
public sealed record FtpConfig
{
    [JsonPropertyName("enabled")]
    public bool? Enabled { get; init; }

    [JsonPropertyName("allow_anonymous")]
    public bool? AllowAnonymous { get; init; }

    [JsonPropertyName("allow_anonymous_write")]
    public bool? AllowAnonymousWrite { get; init; }

    [JsonPropertyName("username")]
    public string? Username { get; init; }

    [JsonPropertyName("allow_remote_access")]
    public bool? AllowRemoteAccess { get; init; }

    [JsonPropertyName("weak_password")]
    public bool? WeakPassword { get; init; }

    [JsonPropertyName("port_ctrl")]
    public long? PortCtrl { get; init; }

    [JsonPropertyName("port_data")]
    public long? PortData { get; init; }

    [JsonPropertyName("remote_domain")]
    public string? RemoteDomain { get; init; }

    public override string ToString() => nameof(FtpConfig);
}
