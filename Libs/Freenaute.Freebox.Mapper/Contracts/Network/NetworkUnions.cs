using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

public enum StringOrBooleanKind : byte
{
    Undefined,
    String,
    Boolean
}

/// <summary>Preserves the documented string/boolean discrepancy without interpreting a string as a boolean.</summary>
[JsonConverter(typeof(StringOrBooleanJsonConverter))]
public readonly struct StringOrBoolean
{
    private readonly string? _string;
    private readonly bool _boolean;
    private StringOrBoolean(string value) { Kind = StringOrBooleanKind.String; _string = value; }
    private StringOrBoolean(bool value) { Kind = StringOrBooleanKind.Boolean; _boolean = value; }
    public StringOrBooleanKind Kind { get; }
    public string String => Kind == StringOrBooleanKind.String ? _string! : throw new InvalidOperationException("This value is not a string.");
    public bool Boolean => Kind == StringOrBooleanKind.Boolean ? _boolean : throw new InvalidOperationException("This value is not a boolean.");
    public static StringOrBoolean FromString(string value) => new(value ?? throw new ArgumentNullException(nameof(value)));
    public static StringOrBoolean FromBoolean(bool value) => new(value);
    public static implicit operator StringOrBoolean(string value) => FromString(value);
    public static implicit operator StringOrBoolean(bool value) => FromBoolean(value);
}

public sealed class StringOrBooleanJsonConverter : JsonConverter<StringOrBoolean>
{
    public override bool HandleNull => true;
    public override StringOrBoolean Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.String => StringOrBoolean.FromString(reader.GetString()!),
            JsonTokenType.True => StringOrBoolean.FromBoolean(true),
            JsonTokenType.False => StringOrBoolean.FromBoolean(false),
            _ => throw new JsonException("Expected a JSON string or boolean.")
        };
    public override void Write(Utf8JsonWriter writer, StringOrBoolean value, JsonSerializerOptions options)
    {
        switch (value.Kind)
        {
            case StringOrBooleanKind.String: writer.WriteStringValue(value.String); return;
            case StringOrBooleanKind.Boolean: writer.WriteBooleanValue(value.Boolean); return;
            default: throw new JsonException("An uninitialized string-or-boolean value cannot be written.");
        }
    }
}

public enum WifiBandCapabilitiesKind : byte
{
    Undefined,
    Integer,
    BooleanMap
}

/// <summary>The formal integer type and illustrated map of boolean radio capability flags are retained as separate kinds.</summary>
[JsonConverter(typeof(WifiBandCapabilitiesJsonConverter))]
public readonly struct WifiBandCapabilities
{
    private readonly long _integer;
    private readonly IReadOnlyDictionary<string, bool>? _flags;
    private WifiBandCapabilities(long value) { Kind = WifiBandCapabilitiesKind.Integer; _integer = value; }
    private WifiBandCapabilities(IReadOnlyDictionary<string, bool> flags) { Kind = WifiBandCapabilitiesKind.BooleanMap; _flags = flags; }
    public WifiBandCapabilitiesKind Kind { get; }
    public long Integer => Kind == WifiBandCapabilitiesKind.Integer ? _integer : throw new InvalidOperationException("These capabilities are not an integer.");
    public IReadOnlyDictionary<string, bool> Flags => Kind == WifiBandCapabilitiesKind.BooleanMap ? _flags! : throw new InvalidOperationException("These capabilities are not a boolean map.");
    public static WifiBandCapabilities FromInteger(long value) => new(value);
    public static WifiBandCapabilities FromFlags(IEnumerable<KeyValuePair<string, bool>> flags)
    {
        ArgumentNullException.ThrowIfNull(flags);
        return new(new ReadOnlyDictionary<string, bool>(new Dictionary<string, bool>(flags, StringComparer.Ordinal)));
    }
}

public sealed class WifiBandCapabilitiesJsonConverter : JsonConverter<WifiBandCapabilities>
{
    public override bool HandleNull => true;
    public override WifiBandCapabilities Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out var integer)) return WifiBandCapabilities.FromInteger(integer);
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException("Expected an integer or a map of boolean WiFi capabilities.");
        var flags = new Dictionary<string, bool>(StringComparer.Ordinal);
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject) return WifiBandCapabilities.FromFlags(flags);
            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException("Expected a WiFi capability name.");
            var name = reader.GetString()!;
            if (!reader.Read() || reader.TokenType is not (JsonTokenType.True or JsonTokenType.False)) throw new JsonException("WiFi capability map values must be booleans.");
            flags[name] = reader.GetBoolean();
        }
        throw new JsonException("Incomplete WiFi capability map.");
    }
    public override void Write(Utf8JsonWriter writer, WifiBandCapabilities value, JsonSerializerOptions options)
    {
        if (value.Kind == WifiBandCapabilitiesKind.Integer) { writer.WriteNumberValue(value.Integer); return; }
        if (value.Kind != WifiBandCapabilitiesKind.BooleanMap) throw new JsonException("Uninitialized WiFi capabilities cannot be written.");
        writer.WriteStartObject();
        foreach (var (key, enabled) in value.Flags) writer.WriteBoolean(key, enabled);
        writer.WriteEndObject();
    }
}
