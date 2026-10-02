using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.SystemHome;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#LanguageSupport; absent metadata remains nullable.</summary>
public sealed record LanguageSupport
{
    [JsonPropertyName("lang")] public string? Lang { get; init; }
    [JsonPropertyName("avalaible")] public ImmutableArray<string>? AvailableLanguages { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#LcdConfig; absent metadata remains nullable.</summary>
public sealed record LcdConfig
{
    [JsonPropertyName("brightness")] public long? Brightness { get; init; }
    [JsonPropertyName("orientation_forced")] public bool? OrientationForced { get; init; }
    [JsonPropertyName("orientation")] public long? Orientation { get; init; }
    [JsonPropertyName("hide_wifi_key")] public bool? HideWifiKey { get; init; }
    [JsonPropertyName("led_strip_enabled")] public bool? LedStripEnabled { get; init; }
    [JsonPropertyName("led_strip_brightness")] public long? LedStripBrightness { get; init; }
    [JsonPropertyName("led_strip_animation")] public LcdLedAnimation? LedStripAnimation { get; init; }
    [JsonPropertyName("available_led_strip_animations")] public ImmutableArray<LcdLedAnimation>? AvailableLedStripAnimations { get; init; }
    [JsonPropertyName("hide_status_led")] public bool? HideStatusLed { get; init; }
    [JsonPropertyName("screensaver")] public LcdScreensaver? Screensaver { get; init; }
    // Example key retained separately; no automatic alias rewrite.
    [JsonPropertyName("hide_led")] public bool? HideLed { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#LedstripPlanning; absent metadata remains nullable.</summary>
public sealed record LedstripPlanning
{
    [JsonPropertyName("use_planning")] public bool? UsePlanning { get; init; }
    [JsonPropertyName("planning_mode")] public LedstripPlanningMode? PlanningMode { get; init; }
    [JsonPropertyName("resolution")] public long? Resolution { get; init; }
    [JsonPropertyName("mapping")] public ImmutableArray<bool>? Mapping { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#LedstripStatus; absent metadata remains nullable.</summary>
public sealed record LedstripStatus
{
    [JsonPropertyName("use_planning")] public bool? UsePlanning { get; init; }
    [JsonPropertyName("next_change")] public long? NextChange { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#StandbyConfig; absent metadata remains nullable.</summary>
public sealed record StandbyConfig
{
    [JsonPropertyName("use_planning")] public bool? UsePlanning { get; init; }
    [JsonPropertyName("planning_mode")] public StandbyPlanningMode? PlanningMode { get; init; }
    [JsonPropertyName("resolution")] public long? Resolution { get; init; }
    [JsonPropertyName("mapping")] public ImmutableArray<bool>? Mapping { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#StandbyStatus; absent metadata remains nullable.</summary>
public sealed record StandbyStatus
{
    [JsonPropertyName("use_planning")] public bool? UsePlanning { get; init; }
    [JsonPropertyName("planning_mode")] public StandbyPlanningMode? PlanningMode { get; init; }
    [JsonPropertyName("next_change")] public long? NextChange { get; init; }
    [JsonPropertyName("available_planning_modes")] public ImmutableArray<StandbyPlanningMode>? AvailablePlanningModes { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#SystemConfig; absent metadata remains nullable.</summary>
public sealed record SystemConfig
{
    [JsonPropertyName("firmware_version")] public string? FirmwareVersion { get; init; }
    [JsonPropertyName("mac")] public string? Mac { get; init; }
    [JsonPropertyName("serial")] public string? Serial { get; init; }
    [JsonPropertyName("uptime")] public string? Uptime { get; init; }
    [JsonPropertyName("uptime_val")] public long? UptimeVal { get; init; }
    [JsonPropertyName("board_name")] public string? BoardName { get; init; }
    [JsonPropertyName("box_authenticated")] public bool? BoxAuthenticated { get; init; }
    [JsonPropertyName("disk_status")] public SystemDiskStatus? DiskStatus { get; init; }
    [JsonPropertyName("usb3_enable")] public bool? Usb3Enable { get; init; }
    [JsonPropertyName("user_main_storage")] public string? UserMainStorage { get; init; }
    [JsonPropertyName("user_storage_powered")] public bool? UserStoragePowered { get; init; }
    [JsonPropertyName("sensors")] public ImmutableArray<SystemConfigSensor>? Sensors { get; init; }
    [JsonPropertyName("model_info")] public SystemModelInfo? ModelInfo { get; init; }
    [JsonPropertyName("fans")] public ImmutableArray<SystemConfigFan>? Fans { get; init; }
    [JsonPropertyName("expansions")] public ImmutableArray<SystemConfigExpansion>? Expansions { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#SystemModelInfo; absent metadata remains nullable.</summary>
public sealed record SystemModelInfo
{
    // Example-only metadata: retain raw counters/tokens without inventing units or enums.
    [JsonPropertyName("internal_hdd_size")] public long? InternalHddSize { get; init; }
    [JsonPropertyName("wifi_type")] public string? WifiType { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("pretty_name")] public string? PrettyName { get; init; }
    [JsonPropertyName("has_expansions")] public bool? HasExpansions { get; init; }
    [JsonPropertyName("has_lan_sfp")] public bool? HasLanSfp { get; init; }
    [JsonPropertyName("has_dect")] public bool? HasDect { get; init; }
    [JsonPropertyName("has_home_automation")] public bool? HasHomeAutomation { get; init; }
    [JsonPropertyName("has_femtocell_exp")] public bool? HasFemtocellExp { get; init; }
    [JsonPropertyName("has_fixed_femtocell")] public bool? HasFixedFemtocell { get; init; }
    [JsonPropertyName("has_vm")] public bool? HasVm { get; init; }
    [JsonPropertyName("has_dsl")] public bool? HasDsl { get; init; }
    [JsonPropertyName("has_standby")] public bool? HasStandby { get; init; }
    [JsonPropertyName("has_eco_wifi")] public bool? HasEcoWifi { get; init; }
    [JsonPropertyName("has_wop")] public bool? HasWop { get; init; }
    [JsonPropertyName("has_led_strip")] public bool? HasLedStrip { get; init; }
    [JsonPropertyName("has_status_led")] public bool? HasStatusLed { get; init; }
    [JsonPropertyName("has_usb3_enable")] public bool? HasUsb3Enable { get; init; }
    [JsonPropertyName("has_lcd_screensaver")] public bool? HasLcdScreensaver { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#SystemConfigSensor; absent metadata remains nullable.</summary>
public sealed record SystemConfigSensor
{
    [JsonPropertyName("id")] public string? Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("value")] public long? Value { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#SystemConfigFan; absent metadata remains nullable.</summary>
public sealed record SystemConfigFan
{
    [JsonPropertyName("id")] public string? Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("value")] public long? Value { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#SystemConfigExpansion; absent metadata remains nullable.</summary>
public sealed record SystemConfigExpansion
{
    [JsonPropertyName("slot")] public long? Slot { get; init; }
    [JsonPropertyName("probe_done")] public bool? ProbeDone { get; init; }
    [JsonPropertyName("present")] public bool? Present { get; init; }
    [JsonPropertyName("supported")] public bool? Supported { get; init; }
    [JsonPropertyName("bundle")] public string? Bundle { get; init; }
    [JsonPropertyName("type")] public SystemExpansionType? Type { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#UpdateStatus; absent metadata remains nullable.</summary>
public sealed record UpdateStatus
{
    [JsonPropertyName("state")] public SystemUpdateState? State { get; init; }
    [JsonPropertyName("upgrade_state")] public UpgradeState? UpgradeState { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#UpgradeState; absent metadata remains nullable.</summary>
public sealed record UpgradeState
{
    [JsonPropertyName("state")] public SystemUpgradeState? State { get; init; }
    [JsonPropertyName("old_version")] public string? OldVersion { get; init; }
    [JsonPropertyName("new_version")] public string? NewVersion { get; init; }
    [JsonPropertyName("percent")] public long? Percent { get; init; }
    [JsonPropertyName("error_string")] public string? ErrorString { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeAdapter; absent metadata remains nullable.</summary>
public sealed record HomeAdapter
{
    [JsonPropertyName("id")] public long? Id { get; init; }
    [JsonPropertyName("icon_url")] public string? IconUrl { get; init; }
    [JsonPropertyName("label")] public string? Label { get; init; }
    [JsonPropertyName("status")] public HomeAdapterStatus? Status { get; init; }
    [JsonPropertyName("type")] public HomeAdapterType? Type { get; init; }
    [JsonPropertyName("props")] public ImmutableDictionary<string, JsonElement>? Props { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomePairingStep; absent metadata remains nullable.</summary>
public sealed record HomePairingStep
{
    [JsonPropertyName("fields")] public ImmutableArray<HomePairingStepField>? Fields { get; init; }
    [JsonPropertyName("icon_url")] public string? IconUrl { get; init; }
    [JsonPropertyName("pageid")] public long? PageId { get; init; }
    [JsonPropertyName("refresh")] public long? Refresh { get; init; }
    [JsonPropertyName("session")] public long? Session { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomePairingStepField; absent metadata remains nullable.</summary>
public sealed record HomePairingStepField
{
    [JsonPropertyName("widget")] public HomePairingWidget? Widget { get; init; }
    [JsonPropertyName("text")] public string? Text { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeNode; absent metadata remains nullable.</summary>
public sealed record HomeNode
{
    [JsonPropertyName("adapter")] public long? Adapter { get; init; }
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("id")] public long? Id { get; init; }
    [JsonPropertyName("label")] public string? Label { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("show_endpoints")] public ImmutableArray<HomeNodeEndpoint>? ShowEndpoints { get; init; }
    [JsonPropertyName("status")] public HomeNodeStatus? Status { get; init; }
    [JsonPropertyName("type")] public HomeNodeType? Type { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeNodeEndpoint; absent metadata remains nullable.</summary>
public sealed record HomeNodeEndpoint
{
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("ep_type")] public HomeEndpointKind? EndpointKind { get; init; }
    [JsonPropertyName("id")] public long? Id { get; init; }
    [JsonPropertyName("visibility")] public HomeEndpointVisibility? Visibility { get; init; }
    [JsonPropertyName("access")] public HomeEndpointAccess? Access { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeNodeType; absent metadata remains nullable.</summary>
public sealed record HomeNodeType
{
    [JsonPropertyName("icon")] public string? Icon { get; init; }
    [JsonPropertyName("label")] public string? Label { get; init; }
    [JsonPropertyName("physical")] public bool? Physical { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeNodeEndpointUi; absent metadata remains nullable.</summary>
public sealed record HomeNodeEndpointUi
{
    // The UI access field is present in the tileset examples.
    [JsonPropertyName("access")] public HomeEndpointAccess? Access { get; init; }
    [JsonPropertyName("display")] public HomeEndpointDisplay? Display { get; init; }
    [JsonPropertyName("icon_url")] public string? IconUrl { get; init; }
    [JsonPropertyName("unit")] public string? Unit { get; init; }
    [JsonPropertyName("icon_color")] public string? IconColor { get; init; }
    [JsonPropertyName("text_color")] public string? TextColor { get; init; }
    [JsonPropertyName("value_color")] public string? ValueColor { get; init; }
    [JsonPropertyName("range")] public ImmutableArray<double>? Range { get; init; }
    [JsonPropertyName("icon_color_range")] public ImmutableArray<string>? IconColorRange { get; init; }
    [JsonPropertyName("text_color_range")] public ImmutableArray<string>? TextColorRange { get; init; }
    [JsonPropertyName("value_color_range")] public ImmutableArray<string>? ValueColorRange { get; init; }
    [JsonPropertyName("status_text_range")] public ImmutableArray<string>? StatusTextRange { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeNodeEndpointValue; absent metadata remains nullable.</summary>
public sealed record HomeNodeEndpointValue
{
    [JsonPropertyName("value")] public HomeIoValue? Value { get; init; }
    [JsonPropertyName("unit")] public string? Unit { get; init; }
    [JsonPropertyName("refresh")] public long? Refresh { get; init; }
    [JsonPropertyName("value_type")] public HomeEndpointValueType? ValueType { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeTile; absent metadata remains nullable.</summary>
public sealed record HomeTile
{
    [JsonPropertyName("ep_type")] public HomeEndpointKind? EndpointKind { get; init; }
    [JsonPropertyName("node_id")] public long? NodeId { get; init; }
    [JsonPropertyName("label")] public string? Label { get; init; }
    [JsonPropertyName("action")] public HomeTileAction? Action { get; init; }
    [JsonPropertyName("type")] public HomeTileType? Type { get; init; }
    [JsonPropertyName("group")] public HomeNodeGroup? Group { get; init; }
    [JsonPropertyName("data")] public ImmutableArray<HomeTileData>? Data { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeNodeGroup; absent metadata remains nullable.</summary>
public sealed record HomeNodeGroup
{
    [JsonPropertyName("label")] public string? Label { get; init; }
    [JsonPropertyName("icon_url")] public string? IconUrl { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#HomeTileData; absent metadata remains nullable.</summary>
public sealed record HomeTileData
{
    [JsonPropertyName("category")] public string? Category { get; init; }
    [JsonPropertyName("refresh")] public long? Refresh { get; init; }
    [JsonPropertyName("label")] public string? Label { get; init; }
    [JsonPropertyName("ep_id")] public long? EpId { get; init; }
    [JsonPropertyName("value_type")] public HomeTileValueType? ValueType { get; init; }
    [JsonPropertyName("value")] public HomeIoValue? Value { get; init; }
    [JsonPropertyName("history")] public string? History { get; init; }
    [JsonPropertyName("ui")] public HomeNodeEndpointUi? Ui { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#Profile; absent metadata remains nullable.</summary>
public sealed record Profile
{
    [JsonPropertyName("id")] public long? Id { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("icon")] public string? Icon { get; init; }
    // Request/response examples use url while the declaration says icon.
    [JsonPropertyName("url")] public string? Url { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#NetworkControl; absent metadata remains nullable.</summary>
public sealed record NetworkControl
{
    [JsonPropertyName("profile_id")] public long? ProfileId { get; init; }
    [JsonPropertyName("next_change")] public long? NextChange { get; init; }
    [JsonPropertyName("override_mode")] public NetworkControlMode? OverrideMode { get; init; }
    [JsonPropertyName("current_mode")] public NetworkControlMode? CurrentMode { get; init; }
    [JsonPropertyName("rule_mode")] public NetworkControlMode? RuleMode { get; init; }
    [JsonPropertyName("override_until")] public long? OverrideUntil { get; init; }
    [JsonPropertyName("override")] public bool? Override { get; init; }
    [JsonPropertyName("macs")] public ImmutableArray<string>? Macs { get; init; }
    [JsonPropertyName("hosts")] public NetworkControlHosts? Hosts { get; init; }
    [JsonPropertyName("resolution")] public long? Resolution { get; init; }
    [JsonPropertyName("cdayranges")] public ImmutableArray<string>? CustomDayRanges { get; init; }
}

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#NetworkControlRule; absent metadata remains nullable.</summary>
public sealed record NetworkControlRule
{
    [JsonPropertyName("id")] public long? Id { get; init; }
    [JsonPropertyName("profile_id")] public long? ProfileId { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("mode")] public string? Mode { get; init; }
    [JsonPropertyName("start_time")] public long? StartTime { get; init; }
    [JsonPropertyName("end_time")] public long? EndTime { get; init; }
    [JsonPropertyName("weekdays")] public ImmutableArray<bool>? Weekdays { get; init; }
    [JsonPropertyName("enabled")] public bool? Enabled { get; init; }
}

/// <summary>The name field is attested by the adapter examples; other type members are not defined.</summary>
public sealed record HomeAdapterType
{
    [JsonPropertyName("name")] public string? Name { get; init; }
}

public sealed record DefaultModeMigrationStatus
{
    [JsonPropertyName("default_mode_migrated")] public bool? DefaultModeMigrated { get; init; }
}

public sealed record ProfileCreated
{
    [JsonPropertyName("id")] public long? Id { get; init; }
}
