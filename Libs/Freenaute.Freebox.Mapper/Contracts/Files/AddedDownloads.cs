using System.Text.Json;
using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>downloads/add result: id is one integer or an array of integers; the original shape is preserved.</summary>
[JsonConverter(typeof(AddedDownloadsJsonConverter))]
public sealed record AddedDownloads(IReadOnlyList<long> Ids, bool WasMultiple);

public sealed class AddedDownloadsJsonConverter : JsonConverter<AddedDownloads>
{
    public override AddedDownloads Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException("Expected a download-add result object.");
        List<long>? ids = null;
        var multiple = false;
        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();
            var isId = reader.ValueTextEquals("id"u8);
            if (!reader.Read()) throw new JsonException();
            if (!isId) { reader.Skip(); continue; }
            if (ids is not null) throw new JsonException("Duplicate download-add identifier.");
            ids = [];
            if (reader.TokenType == JsonTokenType.Number) ids.Add(ReadId(ref reader));
            else if (reader.TokenType == JsonTokenType.StartArray)
            {
                multiple = true;
                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                {
                    if (reader.TokenType != JsonTokenType.Number) throw new JsonException("Expected an integer download identifier.");
                    ids.Add(ReadId(ref reader));
                }
                if (reader.TokenType != JsonTokenType.EndArray) throw new JsonException();
            }
            else throw new JsonException("Expected one integer or an array of download identifiers.");
        }
        if (reader.TokenType != JsonTokenType.EndObject || ids is null) throw new JsonException("Missing download-add identifier.");
        return new AddedDownloads(ids.AsReadOnly(), multiple);
    }

    private static long ReadId(ref Utf8JsonReader reader) => reader.TryGetInt64(out var id)
        ? id : throw new JsonException("Expected a signed 64-bit integer download identifier.");

    public override void Write(Utf8JsonWriter writer, AddedDownloads value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("id");
        if (value.WasMultiple)
        {
            writer.WriteStartArray();
            foreach (var id in value.Ids) writer.WriteNumberValue(id);
            writer.WriteEndArray();
        }
        else
        {
            if (value.Ids.Count != 1) throw new JsonException("A scalar result must contain exactly one identifier.");
            writer.WriteNumberValue(value.Ids[0]);
        }
        writer.WriteEndObject();
    }
}
