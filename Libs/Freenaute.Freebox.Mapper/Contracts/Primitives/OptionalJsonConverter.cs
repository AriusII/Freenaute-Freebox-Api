using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Freenaute.Freebox.Mapper.Contracts.Primitives;

/// <summary>
/// Converts one explicitly closed Optional type without a reflection-based converter factory.
/// Supply generated value metadata, or register that value type in the consuming generated context.
/// </summary>
public class OptionalJsonConverter<T> : JsonConverter<Optional<T>>
{
    private readonly JsonTypeInfo<T>? _valueTypeInfo;

    public OptionalJsonConverter() { }
    public OptionalJsonConverter(JsonTypeInfo<T> valueTypeInfo) =>
        _valueTypeInfo = valueTypeInfo ?? throw new ArgumentNullException(nameof(valueTypeInfo));

    public override bool HandleNull => true;

    public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TokenType == JsonTokenType.Null
            ? Optional<T>.Null
            : Optional<T>.FromValue(JsonSerializer.Deserialize(ref reader, ValueMetadata(options))!);

    public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
    {
        if (!value.IsSet)
        {
            throw new JsonException("An unset Optional field cannot be written. Mark its containing property WhenWritingDefault.");
        }
        if (value.IsNull)
        {
            writer.WriteNullValue();
            return;
        }
        JsonSerializer.Serialize(writer, value.Value, ValueMetadata(options));
    }

    private JsonTypeInfo<T> ValueMetadata(JsonSerializerOptions options) =>
        _valueTypeInfo ?? (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));
}

public sealed class OptionalBooleanJsonConverter : OptionalJsonConverter<bool>;
public sealed class OptionalInt32JsonConverter : OptionalJsonConverter<int>;
public sealed class OptionalInt64JsonConverter : OptionalJsonConverter<long>;
public sealed class OptionalStringJsonConverter : OptionalJsonConverter<string>;
public sealed class OptionalDecimalJsonConverter : OptionalJsonConverter<decimal>;
