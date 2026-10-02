using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Services;

public enum PvrRecordTimeKind : byte { Undefined, Integer, Map }

/// <summary>Preserves the integer declaration or the per-source/per-quality map in the official response example.</summary>
/// <seealso href="http://mafreebox.freebox.fr/doc/index.html#Media.record_time" />
[JsonConverter(typeof(PvrRecordTimeJsonConverter))]
public readonly struct PvrRecordTime
{
    private readonly long _integer;
    private readonly Dictionary<string, Dictionary<string, long>>? _map;

    private PvrRecordTime(long value) { Kind = PvrRecordTimeKind.Integer; _integer = value; _map = null; }
    private PvrRecordTime(Dictionary<string, Dictionary<string, long>> value) { Kind = PvrRecordTimeKind.Map; _integer = default; _map = value; }
    public PvrRecordTimeKind Kind { get; }
    public long Integer => Kind == PvrRecordTimeKind.Integer ? _integer : throw new InvalidOperationException("The recording time is not an integer.");
    public IReadOnlyDictionary<string, IReadOnlyDictionary<string, long>> Map => Kind == PvrRecordTimeKind.Map
        ? _map!.ToDictionary(p => p.Key, p => (IReadOnlyDictionary<string, long>)new System.Collections.ObjectModel.ReadOnlyDictionary<string, long>(p.Value), StringComparer.Ordinal)
        : throw new InvalidOperationException("The recording time is not a map.");
    public static PvrRecordTime FromInteger(long value) => new(value);
    public static PvrRecordTime FromMap(Dictionary<string, Dictionary<string, long>> value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new(value.ToDictionary(p => p.Key, p => new Dictionary<string, long>(p.Value, StringComparer.Ordinal), StringComparer.Ordinal));
    }
    internal Dictionary<string, Dictionary<string, long>> WireMap => _map!;
    public override string ToString() => nameof(PvrRecordTime);
}

public sealed class PvrRecordTimeJsonConverter : JsonConverter<PvrRecordTime>
{
    public override PvrRecordTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => reader.TokenType switch
    {
        JsonTokenType.Number => PvrRecordTime.FromInteger(reader.GetInt64()),
        JsonTokenType.StartObject => PvrRecordTime.FromMap(JsonSerializer.Deserialize(ref reader, ServicesJsonSerializerContext.Default.RecordingTimeMap)!),
        _ => throw new JsonException("Expected an integer or a per-source recording-time map.")
    };
    public override void Write(Utf8JsonWriter writer, PvrRecordTime value, JsonSerializerOptions options)
    {
        switch (value.Kind)
        {
            case PvrRecordTimeKind.Integer: writer.WriteNumberValue(value.Integer); break;
            case PvrRecordTimeKind.Map: JsonSerializer.Serialize(writer, value.WireMap, ServicesJsonSerializerContext.Default.RecordingTimeMap); break;
            default: throw new JsonException("An uninitialized recording time cannot be serialized.");
        }
    }
}
