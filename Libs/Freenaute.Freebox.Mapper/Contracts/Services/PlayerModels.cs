using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

/// <summary>Read projection of the documented Player wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Player" />
public sealed record Player
{
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    [JsonPropertyName("device_name")]
    public string? DeviceName { get; init; }

    [JsonPropertyName("uid")]
    public string? Uid { get; init; }

    [JsonPropertyName("reachable")]
    public bool? Reachable { get; init; }

    [JsonPropertyName("api_version")]
    public string? ApiVersion { get; init; }

    [JsonPropertyName("api_available")]
    public bool? ApiAvailable { get; init; }

    [JsonPropertyName("stb_type")]
    public string? StbType { get; init; }

    public override string ToString() => nameof(Player);
}

/// <summary>Read projection of the documented PlayerStatusForegroundApp wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#PlayerStatusForegroundApp" />
public sealed record PlayerStatusForegroundApp
{
    [JsonPropertyName("cur_url")]
    public string? CurUrl { get; init; }

    [JsonPropertyName("context")]
    public JsonElement? Context { get; init; }

    [JsonPropertyName("package")]
    public string? Package { get; init; }

    public override string ToString() => nameof(PlayerStatusForegroundApp);
}

/// <summary>Read projection of the documented PlayerStatusCapabilities wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities" />
public sealed record PlayerStatusCapabilities
{
    [JsonPropertyName("play")]
    public bool? Play { get; init; }

    [JsonPropertyName("pause")]
    public bool? Pause { get; init; }

    [JsonPropertyName("stop")]
    public bool? Stop { get; init; }

    [JsonPropertyName("next")]
    public bool? Next { get; init; }

    [JsonPropertyName("prev")]
    public bool? Prev { get; init; }

    [JsonPropertyName("record")]
    public bool? Record { get; init; }

    [JsonPropertyName("record_stop")]
    public bool? RecordStop { get; init; }

    [JsonPropertyName("seek_forward")]
    public bool? SeekForward { get; init; }

    [JsonPropertyName("seek_backward")]
    public bool? SeekBackward { get; init; }

    [JsonPropertyName("seek_to")]
    public bool? SeekTo { get; init; }

    [JsonPropertyName("shuffle")]
    public bool? Shuffle { get; init; }

    [JsonPropertyName("repeat_all")]
    public bool? RepeatAll { get; init; }

    [JsonPropertyName("repeat_one")]
    public bool? RepeatOne { get; init; }

    [JsonPropertyName("select_stream")]
    public bool? SelectStream { get; init; }

    [JsonPropertyName("select_audio_track")]
    public bool? SelectAudioTrack { get; init; }

    [JsonPropertyName("select_srt_track")]
    public bool? SelectSrtTrack { get; init; }

    public override string ToString() => nameof(PlayerStatusCapabilities);
}

/// <summary>Read projection of the documented PlayerStatusInformations wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#PlayerStatusInformations" />
public sealed record PlayerStatusInformations
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("last_activity")]
    public long? LastActivity { get; init; }

    [JsonPropertyName("capabilities")]
    public PlayerStatusCapabilities? Capabilities { get; init; }

    public override string ToString() => nameof(PlayerStatusInformations);
}

/// <summary>Read projection of the documented PlayerStatus wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#PlayerStatus" />
public sealed record PlayerStatus
{
    [JsonPropertyName("power_state")]
    public string? PowerState { get; init; }

    [JsonPropertyName("player")]
    public PlayerStatusInformations? Player { get; init; }

    [JsonPropertyName("foreground_app")]
    public PlayerStatusForegroundApp? ForegroundApp { get; init; }

    public override string ToString() => nameof(PlayerStatus);
}

/// <summary>Read projection of the documented PlayerVolume wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#put--api-v8-player-id_player-api-v6-control-volume-" />
public sealed record PlayerVolume
{
    [JsonPropertyName("volume")]
    public int? Volume { get; init; }

    [JsonPropertyName("mute")]
    public bool? Mute { get; init; }

    public override string ToString() => nameof(PlayerVolume);
}

/// <summary>Read projection of the documented PlayerMediaCommand wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-mediactrl-" />
public sealed record PlayerMediaCommand
{
    [JsonPropertyName("cmd")]
    public PlayerMediaCommandName? Cmd { get; init; }

    public override string ToString() => nameof(PlayerMediaCommand);
}

/// <summary>Read projection of the documented PlayerOpenRequest wire object.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-open" />
public sealed record PlayerOpenRequest
{
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    public override string ToString() => nameof(PlayerOpenRequest);
}
