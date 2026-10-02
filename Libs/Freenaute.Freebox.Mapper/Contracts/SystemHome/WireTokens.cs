using System.Text.Json;
using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.SystemHome;

public interface ISystemHomeWireToken<TSelf> where TSelf : struct, ISystemHomeWireToken<TSelf>
{
    string Value { get; }
    static abstract TSelf FromWire(string value);
}

public sealed class SystemHomeWireTokenJsonConverter<TToken> : JsonConverter<TToken>
    where TToken : struct, ISystemHomeWireToken<TToken>
{
    public override TToken Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.String
            ? TToken.FromWire(reader.GetString()!)
            : throw new JsonException("A system/home token must be a string.");

    public override void Write(Utf8JsonWriter writer, TToken value, JsonSerializerOptions options)
    {
        if (value.Value is null) throw new JsonException("An undefined token cannot be written.");
        writer.WriteStringValue(value.Value);
    }
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#LcdConfig.screensaver; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<LcdScreensaver>))]
public readonly record struct LcdScreensaver(string Value) : ISystemHomeWireToken<LcdScreensaver>
{
    public static LcdScreensaver FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "disabled" or "on" or "night";
    public static LcdScreensaver Disabled { get; } = new("disabled");
    public static LcdScreensaver On { get; } = new("on");
    public static LcdScreensaver Night { get; } = new("night");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#LedstripPlanning.planning_mode; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<LedstripPlanningMode>))]
public readonly record struct LedstripPlanningMode(string Value) : ISystemHomeWireToken<LedstripPlanningMode>
{
    public static LedstripPlanningMode FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "ledstrip_off";
    public static LedstripPlanningMode LedstripOff { get; } = new("ledstrip_off");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#StandbyConfig.planning_mode; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<StandbyPlanningMode>))]
public readonly record struct StandbyPlanningMode(string Value) : ISystemHomeWireToken<StandbyPlanningMode>
{
    public static StandbyPlanningMode FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "wifi_off" or "standby";
    public static StandbyPlanningMode WifiOff { get; } = new("wifi_off");
    public static StandbyPlanningMode Standby { get; } = new("standby");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#SystemConfig.disk_status; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<SystemDiskStatus>))]
public readonly record struct SystemDiskStatus(string Value) : ISystemHomeWireToken<SystemDiskStatus>
{
    public static SystemDiskStatus FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "not_detected" or "disabled" or "initializing" or "error" or "active";
    public static SystemDiskStatus NotDetected { get; } = new("not_detected");
    public static SystemDiskStatus Disabled { get; } = new("disabled");
    public static SystemDiskStatus Initializing { get; } = new("initializing");
    public static SystemDiskStatus Error { get; } = new("error");
    public static SystemDiskStatus Active { get; } = new("active");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#SystemConfigExpansion.type; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<SystemExpansionType>))]
public readonly record struct SystemExpansionType(string Value) : ISystemHomeWireToken<SystemExpansionType>
{
    public static SystemExpansionType FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "unknown" or "dsl_lte" or "dsl_lte_external_antennas" or "ftth_p2p" or "ftth_pon" or "security";
    public static SystemExpansionType Unknown { get; } = new("unknown");
    public static SystemExpansionType DslLte { get; } = new("dsl_lte");
    public static SystemExpansionType DslLteExternalAntennas { get; } = new("dsl_lte_external_antennas");
    public static SystemExpansionType FtthP2p { get; } = new("ftth_p2p");
    public static SystemExpansionType FtthPon { get; } = new("ftth_pon");
    public static SystemExpansionType Security { get; } = new("security");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#UpdateStatus.state; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<SystemUpdateState>))]
public readonly record struct SystemUpdateState(string Value) : ISystemHomeWireToken<SystemUpdateState>
{
    public static SystemUpdateState FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "initializing" or "upgrading" or "up_to_date" or "error";
    public static SystemUpdateState Initializing { get; } = new("initializing");
    public static SystemUpdateState Upgrading { get; } = new("upgrading");
    public static SystemUpdateState UpToDate { get; } = new("up_to_date");
    public static SystemUpdateState Error { get; } = new("error");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#UpgradeState.state; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<SystemUpgradeState>))]
public readonly record struct SystemUpgradeState(string Value) : ISystemHomeWireToken<SystemUpgradeState>
{
    public static SystemUpgradeState FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "downloading" or "download_failed" or "checking" or "check_failed" or "prepare_write" or "prepare_write_failed" or "writing" or "write_failed" or "reread" or "reread_failed" or "commit" or "commit_failed";
    public static SystemUpgradeState Downloading { get; } = new("downloading");
    public static SystemUpgradeState DownloadFailed { get; } = new("download_failed");
    public static SystemUpgradeState Checking { get; } = new("checking");
    public static SystemUpgradeState CheckFailed { get; } = new("check_failed");
    public static SystemUpgradeState PrepareWrite { get; } = new("prepare_write");
    public static SystemUpgradeState PrepareWriteFailed { get; } = new("prepare_write_failed");
    public static SystemUpgradeState Writing { get; } = new("writing");
    public static SystemUpgradeState WriteFailed { get; } = new("write_failed");
    public static SystemUpgradeState Reread { get; } = new("reread");
    public static SystemUpgradeState RereadFailed { get; } = new("reread_failed");
    public static SystemUpgradeState Commit { get; } = new("commit");
    public static SystemUpgradeState CommitFailed { get; } = new("commit_failed");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeAdapter.status; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeAdapterStatus>))]
public readonly record struct HomeAdapterStatus(string Value) : ISystemHomeWireToken<HomeAdapterStatus>
{
    public static HomeAdapterStatus FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "unplugged" or "disabled" or "active";
    public static HomeAdapterStatus Unplugged { get; } = new("unplugged");
    public static HomeAdapterStatus Disabled { get; } = new("disabled");
    public static HomeAdapterStatus Active { get; } = new("active");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomePairingStepField.widget; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomePairingWidget>))]
public readonly record struct HomePairingWidget(string Value) : ISystemHomeWireToken<HomePairingWidget>
{
    public static HomePairingWidget FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "label" or "select" or "button" or "display_qrcode" or "input" or "checkbox" or "progress" or "bar_button_left" or "bar_button_right";
    public static HomePairingWidget Label { get; } = new("label");
    public static HomePairingWidget Select { get; } = new("select");
    public static HomePairingWidget Button { get; } = new("button");
    public static HomePairingWidget DisplayQrcode { get; } = new("display_qrcode");
    public static HomePairingWidget Input { get; } = new("input");
    public static HomePairingWidget Checkbox { get; } = new("checkbox");
    public static HomePairingWidget Progress { get; } = new("progress");
    public static HomePairingWidget BarButtonLeft { get; } = new("bar_button_left");
    public static HomePairingWidget BarButtonRight { get; } = new("bar_button_right");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeNode.status; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeNodeStatus>))]
public readonly record struct HomeNodeStatus(string Value) : ISystemHomeWireToken<HomeNodeStatus>
{
    public static HomeNodeStatus FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "unreachable" or "disabled" or "active" or "unpaired";
    public static HomeNodeStatus Unreachable { get; } = new("unreachable");
    public static HomeNodeStatus Disabled { get; } = new("disabled");
    public static HomeNodeStatus Active { get; } = new("active");
    public static HomeNodeStatus Unpaired { get; } = new("unpaired");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeNodeEndpoint.ep_type; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeEndpointKind>))]
public readonly record struct HomeEndpointKind(string Value) : ISystemHomeWireToken<HomeEndpointKind>
{
    public static HomeEndpointKind FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "signal" or "slot";
    public static HomeEndpointKind Signal { get; } = new("signal");
    public static HomeEndpointKind Slot { get; } = new("slot");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeNodeEndpoint.visibility; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeEndpointVisibility>))]
public readonly record struct HomeEndpointVisibility(string Value) : ISystemHomeWireToken<HomeEndpointVisibility>
{
    public static HomeEndpointVisibility FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "internal" or "normal" or "dashboard";
    public static HomeEndpointVisibility Internal { get; } = new("internal");
    public static HomeEndpointVisibility Normal { get; } = new("normal");
    public static HomeEndpointVisibility Dashboard { get; } = new("dashboard");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeNodeEndpoint.access; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeEndpointAccess>))]
public readonly record struct HomeEndpointAccess(string Value) : ISystemHomeWireToken<HomeEndpointAccess>
{
    public static HomeEndpointAccess FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "r" or "w" or "rw";
    public static HomeEndpointAccess R { get; } = new("r");
    public static HomeEndpointAccess W { get; } = new("w");
    public static HomeEndpointAccess Rw { get; } = new("rw");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeNodeEndpointUi.display; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeEndpointDisplay>))]
public readonly record struct HomeEndpointDisplay(string Value) : ISystemHomeWireToken<HomeEndpointDisplay>
{
    public static HomeEndpointDisplay FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "text" or "icon" or "button" or "slider" or "toggle" or "color" or "warning";
    public static HomeEndpointDisplay Text { get; } = new("text");
    public static HomeEndpointDisplay Icon { get; } = new("icon");
    public static HomeEndpointDisplay Button { get; } = new("button");
    public static HomeEndpointDisplay Slider { get; } = new("slider");
    public static HomeEndpointDisplay Toggle { get; } = new("toggle");
    public static HomeEndpointDisplay Color { get; } = new("color");
    public static HomeEndpointDisplay Warning { get; } = new("warning");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeNodeEndpointValue.value_type; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeEndpointValueType>))]
public readonly record struct HomeEndpointValueType(string Value) : ISystemHomeWireToken<HomeEndpointValueType>
{
    public static HomeEndpointValueType FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "bool" or "int" or "float" or "void";
    public static HomeEndpointValueType Bool { get; } = new("bool");
    public static HomeEndpointValueType Int { get; } = new("int");
    public static HomeEndpointValueType Float { get; } = new("float");
    public static HomeEndpointValueType Void { get; } = new("void");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeTile.action; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeTileAction>))]
public readonly record struct HomeTileAction(string Value) : ISystemHomeWireToken<HomeTileAction>
{
    public static HomeTileAction FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "tileset" or "graph" or "store" or "store_slider" or "color_picker" or "heat_picker" or "intensity_picker" or "none";
    public static HomeTileAction Tileset { get; } = new("tileset");
    public static HomeTileAction Graph { get; } = new("graph");
    public static HomeTileAction Store { get; } = new("store");
    public static HomeTileAction StoreSlider { get; } = new("store_slider");
    public static HomeTileAction ColorPicker { get; } = new("color_picker");
    public static HomeTileAction HeatPicker { get; } = new("heat_picker");
    public static HomeTileAction IntensityPicker { get; } = new("intensity_picker");
    public static HomeTileAction None { get; } = new("none");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeTile.type; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeTileType>))]
public readonly record struct HomeTileType(string Value) : ISystemHomeWireToken<HomeTileType>
{
    public static HomeTileType FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "action" or "info" or "light" or "alarm_sensor" or "alarm_control" or "camera";
    public static HomeTileType Action { get; } = new("action");
    public static HomeTileType Info { get; } = new("info");
    public static HomeTileType Light { get; } = new("light");
    public static HomeTileType AlarmSensor { get; } = new("alarm_sensor");
    public static HomeTileType AlarmControl { get; } = new("alarm_control");
    public static HomeTileType Camera { get; } = new("camera");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#HomeTileData.value_type; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<HomeTileValueType>))]
public readonly record struct HomeTileValueType(string Value) : ISystemHomeWireToken<HomeTileValueType>
{
    public static HomeTileValueType FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "bool" or "int" or "float" or "string";
    public static HomeTileValueType Bool { get; } = new("bool");
    public static HomeTileValueType Int { get; } = new("int");
    public static HomeTileValueType Float { get; } = new("float");
    public static HomeTileValueType String { get; } = new("string");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#net-object; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<NetworkControlMode>))]
public readonly record struct NetworkControlMode(string Value) : ISystemHomeWireToken<NetworkControlMode>
{
    public static NetworkControlMode FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    public bool IsKnown => Value is "allowed" or "denied" or "webonly";
    public static NetworkControlMode Allowed { get; } = new("allowed");
    public static NetworkControlMode Denied { get; } = new("denied");
    public static NetworkControlMode Webonly { get; } = new("webonly");
    public override string ToString() => Value ?? string.Empty;
}

/// <summary>Wire token from http://mafreebox.freebox.fr/doc/index.html#LcdConfig.led_strip_animation; unknown values are retained.</summary>
[JsonConverter(typeof(SystemHomeWireTokenJsonConverter<LcdLedAnimation>))]
public readonly record struct LcdLedAnimation(string Value) : ISystemHomeWireToken<LcdLedAnimation>
{
    public static LcdLedAnimation FromWire(string value) { ArgumentNullException.ThrowIfNull(value); return new(value); }
    // Supported animation values are advertised dynamically by the device.
    public override string ToString() => Value ?? string.Empty;
}
