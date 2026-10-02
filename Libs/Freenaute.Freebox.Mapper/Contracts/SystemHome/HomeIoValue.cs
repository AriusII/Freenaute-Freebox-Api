using System.Buffers;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.SystemHome;

public enum HomeIoValueKind { Null, Boolean, Integer, Float, String }

/// <summary>A Home scalar retaining its documented JSON representation, including explicit null.</summary>
[JsonConverter(typeof(HomeIoValueJsonConverter))]
public sealed class HomeIoValue
{
    private readonly bool boolean;
    private readonly long integer;
    private readonly double floating;
    private readonly string? text;
    internal string? NumberText { get; }

    private HomeIoValue(HomeIoValueKind kind, bool boolean = false, long integer = 0,
        double floating = 0, string? text = null, string? numberText = null)
    {
        Kind = kind;
        this.boolean = boolean;
        this.integer = integer;
        this.floating = floating;
        this.text = text;
        NumberText = numberText;
    }

    public HomeIoValueKind Kind { get; }
    public static HomeIoValue Null { get; } = new(HomeIoValueKind.Null);
    public bool Boolean => Kind == HomeIoValueKind.Boolean ? boolean : throw WrongKind();
    public long Integer => Kind == HomeIoValueKind.Integer ? integer : throw WrongKind();
    public double Float => Kind == HomeIoValueKind.Float ? floating : throw WrongKind();
    public string String => Kind == HomeIoValueKind.String ? text! : throw WrongKind();
    public static HomeIoValue FromBoolean(bool value) => new(HomeIoValueKind.Boolean, boolean: value);
    public static HomeIoValue FromInteger(long value) => new(HomeIoValueKind.Integer, integer: value);
    public static HomeIoValue FromString(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return new(HomeIoValueKind.String, text: value);
    }

    public static HomeIoValue FromFloat(double value)
    {
        if (!double.IsFinite(value)) throw new ArgumentOutOfRangeException(nameof(value));
        var number = value.ToString("R", CultureInfo.InvariantCulture);
        if (!number.Contains('.') && !number.Contains('E') && !number.Contains('e')) number += ".0";
        return new(HomeIoValueKind.Float, floating: value, numberText: number);
    }

    internal static HomeIoValue ReadFloat(double value, string lexeme)
    {
        if (!double.IsFinite(value)) throw new JsonException("A Home float must be finite.");
        return new(HomeIoValueKind.Float, floating: value, numberText: lexeme);
    }

    private static InvalidOperationException WrongKind() => new("The Home scalar has a different JSON kind.");
}

public sealed class HomeIoValueJsonConverter : JsonConverter<HomeIoValue>
{
    public override bool HandleNull => true;

    public override HomeIoValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Null: return HomeIoValue.Null;
            case JsonTokenType.True: return HomeIoValue.FromBoolean(true);
            case JsonTokenType.False: return HomeIoValue.FromBoolean(false);
            case JsonTokenType.String: return HomeIoValue.FromString(reader.GetString()!);
            case JsonTokenType.Number:
                var number = reader.HasValueSequence
                    ? Encoding.UTF8.GetString(reader.ValueSequence.ToArray())
                    : Encoding.UTF8.GetString(reader.ValueSpan);
                if (!number.Contains('.') && !number.Contains('e') && !number.Contains('E'))
                {
                    if (!reader.TryGetInt64(out var integer)) throw new JsonException("The Home integer exceeds Int64.");
                    return HomeIoValue.FromInteger(integer);
                }
                return HomeIoValue.ReadFloat(reader.GetDouble(), number);
            default: throw new JsonException("Home values accept only null, Boolean, integer, float or string.");
        }
    }

    public override void Write(Utf8JsonWriter writer, HomeIoValue value, JsonSerializerOptions options)
    {
        if (value is null) { writer.WriteNullValue(); return; }
        switch (value.Kind)
        {
            case HomeIoValueKind.Null: writer.WriteNullValue(); break;
            case HomeIoValueKind.Boolean: writer.WriteBooleanValue(value.Boolean); break;
            case HomeIoValueKind.Integer: writer.WriteNumberValue(value.Integer); break;
            case HomeIoValueKind.Float: writer.WriteRawValue(value.NumberText!); break;
            case HomeIoValueKind.String: writer.WriteStringValue(value.String); break;
            default: throw new JsonException("Unsupported Home scalar kind.");
        }
    }
}
