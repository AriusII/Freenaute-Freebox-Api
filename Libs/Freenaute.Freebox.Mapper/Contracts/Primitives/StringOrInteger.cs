using System.Text.Json;
using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Primitives;

public enum StringOrIntegerKind : byte
{
    Undefined,
    String,
    Integer
}

/// <summary>
/// Preserves the JSON kind for a documented string-or-integer contract.
/// A string such as "001" is never normalized to the integer 1.
/// </summary>
[JsonConverter(typeof(StringOrIntegerJsonConverter))]
public readonly struct StringOrInteger : IEquatable<StringOrInteger>
{
    private readonly string? _string;
    private readonly long _integer;

    private StringOrInteger(string value)
    {
        Kind = StringOrIntegerKind.String;
        _string = value;
    }

    private StringOrInteger(long value)
    {
        Kind = StringOrIntegerKind.Integer;
        _integer = value;
    }

    public StringOrIntegerKind Kind { get; }
    public bool IsInitialized => Kind != StringOrIntegerKind.Undefined;
    public string String => Kind == StringOrIntegerKind.String ? _string! : throw new InvalidOperationException("This JSON value is not a string.");
    public long Integer => Kind == StringOrIntegerKind.Integer ? _integer : throw new InvalidOperationException("This JSON value is not an integer.");

    public static StringOrInteger FromString(string value) => new(value ?? throw new ArgumentNullException(nameof(value)));
    public static StringOrInteger FromInteger(long value) => new(value);
    public static implicit operator StringOrInteger(string value) => FromString(value);
    public static implicit operator StringOrInteger(long value) => FromInteger(value);
    public static implicit operator StringOrInteger(int value) => FromInteger(value);

    public bool Equals(StringOrInteger other) => Kind == other.Kind && Kind switch
    {
        StringOrIntegerKind.String => string.Equals(_string, other._string, StringComparison.Ordinal),
        StringOrIntegerKind.Integer => _integer == other._integer,
        _ => true
    };
    public override bool Equals(object? obj) => obj is StringOrInteger other && Equals(other);
    public override int GetHashCode() => Kind switch
    {
        StringOrIntegerKind.String => HashCode.Combine(Kind, _string),
        StringOrIntegerKind.Integer => HashCode.Combine(Kind, _integer),
        _ => 0
    };
    public static bool operator ==(StringOrInteger left, StringOrInteger right) => left.Equals(right);
    public static bool operator !=(StringOrInteger left, StringOrInteger right) => !left.Equals(right);
}

public sealed class StringOrIntegerJsonConverter : JsonConverter<StringOrInteger>
{
    public override bool HandleNull => true;
    public override StringOrInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType switch
        {
            JsonTokenType.String => StringOrInteger.FromString(reader.GetString()!),
            JsonTokenType.Number when reader.TryGetInt64(out var integer) => StringOrInteger.FromInteger(integer),
            _ => throw new JsonException("Expected a JSON string or a signed 64-bit integer.")
        };

    public override void Write(Utf8JsonWriter writer, StringOrInteger value, JsonSerializerOptions options)
    {
        switch (value.Kind)
        {
            case StringOrIntegerKind.String:
                writer.WriteStringValue(value.String);
                return;
            case StringOrIntegerKind.Integer:
                writer.WriteNumberValue(value.Integer);
                return;
            default:
                throw new JsonException("An uninitialized string-or-integer value cannot be written.");
        }
    }
}
