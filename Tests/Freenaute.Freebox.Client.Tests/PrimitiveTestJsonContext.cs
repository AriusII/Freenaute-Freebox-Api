using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Client.Tests;

public sealed record PrimitivePatch
{
    [JsonPropertyName("enabled"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Optional<bool> Enabled { get; init; }
    [JsonPropertyName("count"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Optional<int> Count { get; init; }
    [JsonPropertyName("bytes"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Optional<long> Bytes { get; init; }
    [JsonPropertyName("name"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Optional<string> Name { get; init; }
    [JsonPropertyName("rate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Optional<decimal> Rate { get; init; }
}

public sealed record PrimitiveItem([property: JsonPropertyName("value")] int Value);

public sealed record PrimitiveNestedPatch
{
    [JsonPropertyName("item"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<PrimitiveItem>))]
    public Optional<PrimitiveItem> Item { get; init; }

    [JsonPropertyName("values"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<int[]>))]
    public Optional<int[]> Values { get; init; }
}

[JsonSourceGenerationOptions(
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Converters = [typeof(OptionalBooleanJsonConverter), typeof(OptionalInt32JsonConverter),
        typeof(OptionalInt64JsonConverter), typeof(OptionalStringJsonConverter), typeof(OptionalDecimalJsonConverter),
        typeof(ObjectOrArrayJsonConverter<PrimitiveItem>), typeof(ObjectOrArrayJsonConverter<int>)])]
[JsonSerializable(typeof(bool))]
[JsonSerializable(typeof(int))]
[JsonSerializable(typeof(long))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(decimal))]
[JsonSerializable(typeof(int[]))]
[JsonSerializable(typeof(PrimitivePatch))]
[JsonSerializable(typeof(PrimitiveNestedPatch))]
[JsonSerializable(typeof(PrimitiveItem))]
[JsonSerializable(typeof(ObjectOrArray<PrimitiveItem>), TypeInfoPropertyName = "ItemObjectOrArray")]
[JsonSerializable(typeof(ObjectOrArray<int>), TypeInfoPropertyName = "IntegerObjectOrArray")]
internal sealed partial class PrimitiveTestJsonContext : JsonSerializerContext;
