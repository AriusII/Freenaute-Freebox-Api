using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

public enum VpnIpSecAuthModesKind : byte { Undefined, Map, Array }

/// <summary>Preserves the named authentication-mode map or the declared array without discarding map keys.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#VPNIPSecConfig.auth_modes" />
[JsonConverter(typeof(VpnIpSecAuthModesJsonConverter))]
public readonly struct VpnIpSecAuthModes
{
    private readonly Dictionary<string, VpnIpSecAuthMode>? _map;
    private readonly VpnIpSecAuthMode[]? _items;
    private VpnIpSecAuthModes(Dictionary<string, VpnIpSecAuthMode> map) { Kind = VpnIpSecAuthModesKind.Map; _map = map; _items = null; }
    private VpnIpSecAuthModes(VpnIpSecAuthMode[] items) { Kind = VpnIpSecAuthModesKind.Array; _items = items; _map = null; }
    public VpnIpSecAuthModesKind Kind { get; }
    public IReadOnlyDictionary<string, VpnIpSecAuthMode> Map => Kind == VpnIpSecAuthModesKind.Map
        ? new ReadOnlyDictionary<string, VpnIpSecAuthMode>(_map!) : throw new InvalidOperationException("The authentication modes are not a map.");
    public IReadOnlyList<VpnIpSecAuthMode> Items => Kind == VpnIpSecAuthModesKind.Array
        ? System.Array.AsReadOnly(_items!) : throw new InvalidOperationException("The authentication modes are not an array.");
    public static VpnIpSecAuthModes FromMap(Dictionary<string, VpnIpSecAuthMode> value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new(new Dictionary<string, VpnIpSecAuthMode>(value, StringComparer.Ordinal));
    }
    public static VpnIpSecAuthModes FromArray(VpnIpSecAuthMode[] value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new((VpnIpSecAuthMode[])value.Clone());
    }
    internal Dictionary<string, VpnIpSecAuthMode> WireMap => _map!;
    internal VpnIpSecAuthMode[] WireItems => _items!;
    public override string ToString() => nameof(VpnIpSecAuthModes);
}

public sealed class VpnIpSecAuthModesJsonConverter : JsonConverter<VpnIpSecAuthModes>
{
    public override VpnIpSecAuthModes Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType switch
    {
        JsonTokenType.StartObject => VpnIpSecAuthModes.FromMap(JsonSerializer.Deserialize(ref reader, ServicesJsonSerializerContext.Default.VpnAuthModeMap)!),
        JsonTokenType.StartArray => VpnIpSecAuthModes.FromArray(JsonSerializer.Deserialize(ref reader, ServicesJsonSerializerContext.Default.VpnIpSecAuthModeArray)!),
        _ => throw new JsonException("Expected a named authentication-mode map or an array.")
    };
    public override void Write(Utf8JsonWriter writer, VpnIpSecAuthModes value, JsonSerializerOptions options)
    {
        switch (value.Kind)
        {
            case VpnIpSecAuthModesKind.Map: JsonSerializer.Serialize(writer, value.WireMap, ServicesJsonSerializerContext.Default.VpnAuthModeMap); break;
            case VpnIpSecAuthModesKind.Array: JsonSerializer.Serialize(writer, value.WireItems, ServicesJsonSerializerContext.Default.VpnIpSecAuthModeArray); break;
            default: throw new JsonException("Uninitialized authentication modes cannot be serialized.");
        }
    }
}
