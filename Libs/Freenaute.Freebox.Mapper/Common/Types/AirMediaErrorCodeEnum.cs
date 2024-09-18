using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

public enum AirMediaErrorCodeEnum
{
    [JsonPropertyName("unknown_target")] UnknownTarget,
    [JsonPropertyName("no_client")] NoClient,
    [JsonPropertyName("set_pass")] SetPassError,

    [JsonPropertyName("set_onscreen_code")]
    SetOnscreenCodeError,
    [JsonPropertyName("no_ctrl")] NoControl,
    [JsonPropertyName("http")] HttpError,
    [JsonPropertyName("bad_session")] BadSession,
    [JsonPropertyName("bad_name")] BadName,
    [JsonPropertyName("bad_device_id")] BadDeviceId,
    [JsonPropertyName("bad_remote_id")] BadRemoteId,
    [JsonPropertyName("req_in_progress")] RequestInProgress,
    [JsonPropertyName("fetch")] FetchError,
    [JsonPropertyName("no_display")] NoDisplay,
    [JsonPropertyName("playback_state")] InvalidPlaybackState,
    [JsonPropertyName("no_slideshow_srv")] NoSlideshowServer,
    [JsonPropertyName("no_mem")] NoMemory,
    [JsonPropertyName("inout_file")] InputOutputFileError,

    [JsonPropertyName("no_volume_control")]
    NoVolumeControl,
    [JsonPropertyName("connect")] ConnectionError,
    [JsonPropertyName("unauthorized")] Unauthorized,

    [JsonPropertyName("unsupported_media")]
    UnsupportedMedia,
    [JsonPropertyName("bad_type")] BadFileType,
    [JsonPropertyName("unimplemented")] Unimplemented
}
