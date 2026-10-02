using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

[JsonConverter(typeof(JsonStringEnumConverter<AirMediaErrorCodeEnum>))]
public enum AirMediaErrorCodeEnum
{
    [JsonStringEnumMemberName("unknown_target")] UnknownTarget,
    [JsonStringEnumMemberName("no_client")] NoClient,
    [JsonStringEnumMemberName("set_pass")] SetPassError,

    [JsonStringEnumMemberName("set_onscreen_code")]
    SetOnscreenCodeError,
    [JsonStringEnumMemberName("no_ctrl")] NoControl,
    [JsonStringEnumMemberName("http")] HttpError,
    [JsonStringEnumMemberName("bad_session")] BadSession,
    [JsonStringEnumMemberName("bad_name")] BadName,
    [JsonStringEnumMemberName("bad_device_id")] BadDeviceId,
    [JsonStringEnumMemberName("bad_remote_id")] BadRemoteId,
    [JsonStringEnumMemberName("req_in_progress")] RequestInProgress,
    [JsonStringEnumMemberName("fetch")] FetchError,
    [JsonStringEnumMemberName("no_display")] NoDisplay,
    [JsonStringEnumMemberName("playback_state")] InvalidPlaybackState,
    [JsonStringEnumMemberName("no_slideshow_srv")] NoSlideshowServer,
    [JsonStringEnumMemberName("no_mem")] NoMemory,
    [JsonStringEnumMemberName("inout_file")] InputOutputFileError,

    [JsonStringEnumMemberName("no_volume_control")]
    NoVolumeControl,
    [JsonStringEnumMemberName("connect")] ConnectionError,
    [JsonStringEnumMemberName("unauthorized")] Unauthorized,

    [JsonStringEnumMemberName("unsupported_media")]
    UnsupportedMedia,
    [JsonStringEnumMemberName("bad_type")] BadFileType,
    [JsonStringEnumMemberName("unimplemented")] Unimplemented
}
