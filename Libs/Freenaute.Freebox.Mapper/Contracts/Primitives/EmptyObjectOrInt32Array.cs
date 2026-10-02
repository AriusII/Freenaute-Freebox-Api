using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Primitives;

public enum EmptyObjectOrInt32ArrayKind : byte
{
    Undefined,
    EmptyObject,
    Array
}

/// <summary>A documented empty object or an integer array; a nonempty object is never accepted as an arbitrary map.</summary>
[JsonConverter(typeof(EmptyObjectOrInt32ArrayJsonConverter))]
public readonly struct EmptyObjectOrInt32Array : IEquatable<EmptyObjectOrInt32Array>
{
    private readonly ReadOnlyCollection<int>? _values;
    private EmptyObjectOrInt32Array(EmptyObjectOrInt32ArrayKind kind, int[] values)
    {
        Kind = kind;
        _values = Array.AsReadOnly(values);
    }

    public EmptyObjectOrInt32ArrayKind Kind { get; }
    public bool IsInitialized => Kind != EmptyObjectOrInt32ArrayKind.Undefined;
    public IReadOnlyList<int> Values => _values ?? throw new InvalidOperationException("This integer collection is not initialized.");
    public static EmptyObjectOrInt32Array EmptyObject => new(EmptyObjectOrInt32ArrayKind.EmptyObject, []);
    public static EmptyObjectOrInt32Array FromArray(IEnumerable<int> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        return new(EmptyObjectOrInt32ArrayKind.Array, values.ToArray());
    }

    public bool Equals(EmptyObjectOrInt32Array other) => Kind == other.Kind &&
        (!IsInitialized || Values.SequenceEqual(other.Values));
    public override bool Equals(object? obj) => obj is EmptyObjectOrInt32Array other && Equals(other);
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Kind);
        if (IsInitialized)
            foreach (var value in Values) hash.Add(value);
        return hash.ToHashCode();
    }
    public static bool operator ==(EmptyObjectOrInt32Array left, EmptyObjectOrInt32Array right) => left.Equals(right);
    public static bool operator !=(EmptyObjectOrInt32Array left, EmptyObjectOrInt32Array right) => !left.Equals(right);
}

public sealed class EmptyObjectOrInt32ArrayJsonConverter : JsonConverter<EmptyObjectOrInt32Array>
{
    public override bool HandleNull => true;
    public override EmptyObjectOrInt32Array Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
                throw new JsonException("Only an empty object is documented for this integer-array union.");
            return EmptyObjectOrInt32Array.EmptyObject;
        }
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected an empty JSON object or an integer array.");
        List<int> values = [];
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                return EmptyObjectOrInt32Array.FromArray(values);
            if (reader.TokenType != JsonTokenType.Number || !reader.TryGetInt32(out var value))
                throw new JsonException("Expected a signed 32-bit integer array item.");
            values.Add(value);
        }
        throw new JsonException("The integer array was not terminated.");
    }

    public override void Write(Utf8JsonWriter writer, EmptyObjectOrInt32Array value, JsonSerializerOptions options)
    {
        if (!value.IsInitialized)
            throw new JsonException("An uninitialized empty-object-or-integer-array value cannot be written.");
        if (value.Kind == EmptyObjectOrInt32ArrayKind.EmptyObject)
        {
            writer.WriteStartObject();
            writer.WriteEndObject();
            return;
        }
        writer.WriteStartArray();
        foreach (var item in value.Values) writer.WriteNumberValue(item);
        writer.WriteEndArray();
    }
}
