using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Freenaute.Freebox.Mapper.Contracts.Primitives;

/// <summary>
/// A converter closed explicitly for one reviewed model type.
/// The generated context must include T; this converter never creates types or metadata by reflection.
/// </summary>
public class ObjectOrArrayJsonConverter<T> : JsonConverter<ObjectOrArray<T>>
{
    private readonly JsonTypeInfo<T>? _itemTypeInfo;
    public ObjectOrArrayJsonConverter() { }
    public ObjectOrArrayJsonConverter(JsonTypeInfo<T> itemTypeInfo) =>
        _itemTypeInfo = itemTypeInfo ?? throw new ArgumentNullException(nameof(itemTypeInfo));

    public override bool HandleNull => true;

    public override ObjectOrArray<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var metadata = ItemMetadata(options);
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            var item = JsonSerializer.Deserialize(ref reader, metadata);
            if (item is null) throw new JsonException("The object result cannot be null.");
            return ObjectOrArray<T>.FromObject(item);
        }
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException("Expected a JSON object or an array of typed objects.");

        List<T> items = [];
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                return ObjectOrArray<T>.FromArray(items);
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Each array item must be a JSON object.");
            var item = JsonSerializer.Deserialize(ref reader, metadata);
            if (item is null) throw new JsonException("An array item cannot be null.");
            items.Add(item);
        }
        throw new JsonException("The array result was not terminated.");
    }

    public override void Write(Utf8JsonWriter writer, ObjectOrArray<T> value, JsonSerializerOptions options)
    {
        if (!value.IsInitialized)
            throw new JsonException("An uninitialized object-or-array result cannot be written.");
        var metadata = ItemMetadata(options);
        if (value.IsObject)
        {
            WriteObject(writer, value.Object, metadata);
            return;
        }
        writer.WriteStartArray();
        foreach (var item in value.Items)
        {
            if (item is null) throw new JsonException("An array item cannot be null.");
            WriteObject(writer, item, metadata);
        }
        writer.WriteEndArray();
    }

    private JsonTypeInfo<T> ItemMetadata(JsonSerializerOptions options) =>
        _itemTypeInfo ?? (JsonTypeInfo<T>)options.GetTypeInfo(typeof(T));

    private static void WriteObject(Utf8JsonWriter writer, T item, JsonTypeInfo<T> metadata)
    {
        if (metadata.Kind == JsonTypeInfoKind.Object)
        {
            JsonSerializer.Serialize(writer, item, metadata);
            return;
        }

        // A custom model converter can emit any JSON kind. Validate that boundary,
        // while ordinary generated object contracts use the direct writer above.
        var json = JsonSerializer.SerializeToElement(item, metadata);
        if (json.ValueKind != JsonValueKind.Object)
            throw new JsonException("The typed result must serialize as a JSON object.");
        json.WriteTo(writer);
    }
}
