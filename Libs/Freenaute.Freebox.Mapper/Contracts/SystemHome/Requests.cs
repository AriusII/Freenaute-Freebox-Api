using System.Collections.Immutable;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.SystemHome;

/// <summary>An immutable operation body with validation before any HTTP request is sent.</summary>
public interface ISystemHomeRequest { void Validate(); }

internal static class SystemHomeRequestValidation
{
    internal static void NoNull<T>(Optional<T> value, string name)
    {
        if (value.IsNull) throw new ArgumentException("Explicit null is not documented for this field.", name);
    }
    internal static void Array<T>(Optional<ImmutableArray<T>> value, string name)
    {
        NoNull(value, name);
        if (value.HasValue && value.Value.IsDefault) throw new ArgumentException("An array must be initialized.", name);
    }
}

public sealed record LcdConfigPatch : ISystemHomeRequest
{
    [JsonPropertyName("brightness")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalInt32JsonConverter))]
    public Optional<int> Brightness { get; init; }
    public LcdConfigPatch WithBrightness(int value) => this with { Brightness = Optional<int>.FromValue(value) };
    [JsonPropertyName("orientation_forced")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalBooleanJsonConverter))]
    public Optional<bool> OrientationForced { get; init; }
    public LcdConfigPatch WithOrientationForced(bool value) => this with { OrientationForced = Optional<bool>.FromValue(value) };
    [JsonPropertyName("orientation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalInt64JsonConverter))]
    public Optional<long> Orientation { get; init; }
    public LcdConfigPatch WithOrientation(long value) => this with { Orientation = Optional<long>.FromValue(value) };
    [JsonPropertyName("hide_wifi_key")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalBooleanJsonConverter))]
    public Optional<bool> HideWifiKey { get; init; }
    public LcdConfigPatch WithHideWifiKey(bool value) => this with { HideWifiKey = Optional<bool>.FromValue(value) };
    [JsonPropertyName("led_strip_enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalBooleanJsonConverter))]
    public Optional<bool> LedStripEnabled { get; init; }
    public LcdConfigPatch WithLedStripEnabled(bool value) => this with { LedStripEnabled = Optional<bool>.FromValue(value) };
    [JsonPropertyName("led_strip_brightness")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalInt32JsonConverter))]
    public Optional<int> LedStripBrightness { get; init; }
    public LcdConfigPatch WithLedStripBrightness(int value) => this with { LedStripBrightness = Optional<int>.FromValue(value) };
    [JsonPropertyName("led_strip_animation")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<LcdLedAnimation>))]
    public Optional<LcdLedAnimation> LedStripAnimation { get; init; }
    public LcdConfigPatch WithLedStripAnimation(LcdLedAnimation value) => this with { LedStripAnimation = Optional<LcdLedAnimation>.FromValue(value) };
    [JsonPropertyName("hide_status_led")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalBooleanJsonConverter))]
    public Optional<bool> HideStatusLed { get; init; }
    public LcdConfigPatch WithHideStatusLed(bool value) => this with { HideStatusLed = Optional<bool>.FromValue(value) };
    [JsonPropertyName("screensaver")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<LcdScreensaver>))]
    public Optional<LcdScreensaver> Screensaver { get; init; }
    public LcdConfigPatch WithScreensaver(LcdScreensaver value) => this with { Screensaver = Optional<LcdScreensaver>.FromValue(value) };
    public void Validate()
    {
        SystemHomeRequestValidation.NoNull(Brightness, nameof(Brightness));
        if (Brightness.HasValue && Brightness.Value is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(Brightness));
        SystemHomeRequestValidation.NoNull(OrientationForced, nameof(OrientationForced));
        SystemHomeRequestValidation.NoNull(Orientation, nameof(Orientation));
        SystemHomeRequestValidation.NoNull(HideWifiKey, nameof(HideWifiKey));
        SystemHomeRequestValidation.NoNull(LedStripEnabled, nameof(LedStripEnabled));
        SystemHomeRequestValidation.NoNull(LedStripBrightness, nameof(LedStripBrightness));
        if (LedStripBrightness.HasValue && LedStripBrightness.Value is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(LedStripBrightness));
        SystemHomeRequestValidation.NoNull(LedStripAnimation, nameof(LedStripAnimation));
        if (LedStripAnimation.HasValue && LedStripAnimation.Value.Value is null) throw new ArgumentException("Undefined token.", nameof(LedStripAnimation));
        SystemHomeRequestValidation.NoNull(HideStatusLed, nameof(HideStatusLed));
        SystemHomeRequestValidation.NoNull(Screensaver, nameof(Screensaver));
        if (Screensaver.HasValue && Screensaver.Value.Value is null) throw new ArgumentException("Undefined token.", nameof(Screensaver));
    }
}

public sealed record LedstripPlanningUpdate : ISystemHomeRequest
{
    [JsonPropertyName("use_planning")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalBooleanJsonConverter))]
    public Optional<bool> UsePlanning { get; init; }
    public LedstripPlanningUpdate WithUsePlanning(bool value) => this with { UsePlanning = Optional<bool>.FromValue(value) };
    [JsonPropertyName("planning_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<LedstripPlanningMode>))]
    public Optional<LedstripPlanningMode> PlanningMode { get; init; }
    public LedstripPlanningUpdate WithPlanningMode(LedstripPlanningMode value) => this with { PlanningMode = Optional<LedstripPlanningMode>.FromValue(value) };
    [JsonPropertyName("mapping")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ImmutableArray<bool>>))]
    public Optional<ImmutableArray<bool>> Mapping { get; init; }
    public LedstripPlanningUpdate WithMapping(IEnumerable<bool> values) { ArgumentNullException.ThrowIfNull(values); return this with { Mapping = Optional<ImmutableArray<bool>>.FromValue(values.ToImmutableArray()) }; }
    public void Validate()
    {
        SystemHomeRequestValidation.NoNull(UsePlanning, nameof(UsePlanning));
        SystemHomeRequestValidation.NoNull(PlanningMode, nameof(PlanningMode));
        if (PlanningMode.HasValue && PlanningMode.Value.Value is null) throw new ArgumentException("Undefined token.", nameof(PlanningMode));
        SystemHomeRequestValidation.Array(Mapping, nameof(Mapping));
    }
}

public sealed record StandbyConfigUpdate : ISystemHomeRequest
{
    [JsonPropertyName("use_planning")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalBooleanJsonConverter))]
    public Optional<bool> UsePlanning { get; init; }
    public StandbyConfigUpdate WithUsePlanning(bool value) => this with { UsePlanning = Optional<bool>.FromValue(value) };
    [JsonPropertyName("planning_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<StandbyPlanningMode>))]
    public Optional<StandbyPlanningMode> PlanningMode { get; init; }
    public StandbyConfigUpdate WithPlanningMode(StandbyPlanningMode value) => this with { PlanningMode = Optional<StandbyPlanningMode>.FromValue(value) };
    [JsonPropertyName("mapping")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ImmutableArray<bool>>))]
    public Optional<ImmutableArray<bool>> Mapping { get; init; }
    public StandbyConfigUpdate WithMapping(IEnumerable<bool> values) { ArgumentNullException.ThrowIfNull(values); return this with { Mapping = Optional<ImmutableArray<bool>>.FromValue(values.ToImmutableArray()) }; }
    public void Validate()
    {
        SystemHomeRequestValidation.NoNull(UsePlanning, nameof(UsePlanning));
        SystemHomeRequestValidation.NoNull(PlanningMode, nameof(PlanningMode));
        if (PlanningMode.HasValue && PlanningMode.Value.Value is null) throw new ArgumentException("Undefined token.", nameof(PlanningMode));
        SystemHomeRequestValidation.Array(Mapping, nameof(Mapping));
    }
}

public sealed record NetworkControlUpdate : ISystemHomeRequest
{
    [JsonPropertyName("override_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<NetworkControlMode>))]
    public Optional<NetworkControlMode> OverrideMode { get; init; }
    public NetworkControlUpdate WithOverrideMode(NetworkControlMode value) => this with { OverrideMode = Optional<NetworkControlMode>.FromValue(value) };
    [JsonPropertyName("override_until")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalInt64JsonConverter))]
    public Optional<long> OverrideUntil { get; init; }
    public NetworkControlUpdate WithOverrideUntil(long value) => this with { OverrideUntil = Optional<long>.FromValue(value) };
    [JsonPropertyName("override")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalBooleanJsonConverter))]
    public Optional<bool> Override { get; init; }
    public NetworkControlUpdate WithOverride(bool value) => this with { Override = Optional<bool>.FromValue(value) };
    [JsonPropertyName("macs")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ImmutableArray<string>>))]
    public Optional<ImmutableArray<string>> Macs { get; init; }
    public NetworkControlUpdate WithMacs(IEnumerable<string> values) { ArgumentNullException.ThrowIfNull(values); return this with { Macs = Optional<ImmutableArray<string>>.FromValue(values.ToImmutableArray()) }; }
    [JsonPropertyName("cdayranges")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<ImmutableArray<string>>))]
    public Optional<ImmutableArray<string>> CustomDayRanges { get; init; }
    public NetworkControlUpdate WithCustomDayRanges(IEnumerable<string> values) { ArgumentNullException.ThrowIfNull(values); return this with { CustomDayRanges = Optional<ImmutableArray<string>>.FromValue(values.ToImmutableArray()) }; }
    public void Validate()
    {
        SystemHomeRequestValidation.NoNull(OverrideMode, nameof(OverrideMode));
        if (OverrideMode.HasValue && OverrideMode.Value.Value is null) throw new ArgumentException("Undefined token.", nameof(OverrideMode));
        SystemHomeRequestValidation.NoNull(OverrideUntil, nameof(OverrideUntil));
        SystemHomeRequestValidation.NoNull(Override, nameof(Override));
        SystemHomeRequestValidation.Array(Macs, nameof(Macs));
        SystemHomeRequestValidation.Array(CustomDayRanges, nameof(CustomDayRanges));
    }
}

public sealed record SetLanguageRequest([property: JsonPropertyName("lang")] string Language) : ISystemHomeRequest
{
    public void Validate()
    {
        ArgumentNullException.ThrowIfNull(Language);
        if (Language.Length != 3 || Language.Any(static c => !char.IsAsciiLetter(c)))
            throw new ArgumentException("Use the ISO 639-3 language code advertised by the server.", nameof(Language));
    }
}

public sealed record HomeAdapterStatusRequest([property: JsonPropertyName("status")] HomeAdapterStatus Status) : ISystemHomeRequest
{
    public void Validate() { if (Status.Value is null) throw new ArgumentException("Status must be defined.", nameof(Status)); }
}

public sealed record RenameHomeNodeRequest([property: JsonPropertyName("label")] string Label) : ISystemHomeRequest
{
    public void Validate() => ArgumentNullException.ThrowIfNull(Label);
}

public sealed record HomeEndpointValueRequest([property: JsonPropertyName("value")] HomeIoValue Value) : ISystemHomeRequest
{
    public void Validate() => ArgumentNullException.ThrowIfNull(Value);
}

public sealed record HomePairingStartRequest : ISystemHomeRequest
{
    [JsonPropertyName("op")] public string Operation => "start";
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; init; }
    public void Validate() { }
}

public sealed record HomePairingNextRequest : ISystemHomeRequest
{
    [JsonPropertyName("op")] public string Operation => "next";
    [JsonPropertyName("session")] public StringOrInteger Session { get; init; }
    [JsonPropertyName("pageid")] public StringOrInteger PageId { get; init; }
    [JsonPropertyName("fields")] public ImmutableArray<HomeIoValue> Fields { get; init; } = [];
    public void Validate()
    {
        if (Session.Kind == default || PageId.Kind == default) throw new ArgumentException("Pairing identifiers must be defined.");
        if (Fields.IsDefault || Fields.Any(static value => value is null || value.Kind == HomeIoValueKind.Float))
            throw new ArgumentException("Pairing fields require null, Boolean, integer or string values.", nameof(Fields));
    }
}

public sealed record HomePairingStopRequest([property: JsonPropertyName("session")] long Session) : ISystemHomeRequest
{
    [JsonPropertyName("op")] public string Operation => "stop";
    public void Validate() { }
}

/// <summary>Creation fields shown by the official POST example; URL remains distinct from the read model's icon.</summary>
public sealed record ProfileCreateRequest([property: JsonPropertyName("name")] string Name) : ISystemHomeRequest
{
    [JsonPropertyName("url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Url { get; init; }
    public ProfileCreateRequest WithIconUrl(string value) { ArgumentNullException.ThrowIfNull(value); return this with { Url = value }; }
    public void Validate() => ArgumentNullException.ThrowIfNull(Name);
}
