using System.Text;
using System.Text.Json;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;
using Xunit;

namespace Freenaute.Freebox.Client.Tests;

public sealed class PrimitiveContractTests
{
    [Fact]
    public void OptionalStatesDoNotCollapseUnsetNullFalseAndZero()
    {
        Assert.False(default(Optional<bool>).IsSet);
        Assert.False(Optional<bool>.Null.HasValue);
        Assert.True(Optional<bool>.Null.IsSet);
        Assert.True(Optional<bool>.Null.IsNull);
        Assert.NotEqual(default, Optional<bool>.Null);
        Assert.NotEqual(default, Optional<bool>.FromValue(false));
        Assert.NotEqual(Optional<bool>.Null, Optional<bool>.FromValue(false));
        Assert.False(Optional<bool>.FromValue(false).Value);
        Assert.Equal(0, Optional<int>.Set(0).Value);
        Assert.Throws<InvalidOperationException>(() => Optional<int>.Unset.Value);
        Assert.Throws<InvalidOperationException>(() => Optional<bool>.Null.Value);
        Assert.False(Optional<int>.Unset.TryGetValue(out _));
        Assert.True(Optional<int>.Set(0).TryGetValue(out var zero));
        Assert.Equal(0, zero);
    }

    [Fact]
    public void PatchOmitsOnlyUnsetFieldsAndWritesSuppliedFalseZeroAndNull()
    {
        Assert.False(JsonSerializer.IsReflectionEnabledByDefault);
        var metadata = PrimitiveTestJsonContext.Default.PrimitivePatch;
        Assert.Equal("{}", JsonSerializer.Serialize(new PrimitivePatch(), metadata));
        var patch = new PrimitivePatch { Enabled = false, Count = 0, Bytes = 0L, Name = Optional<string>.Null, Rate = 0m };
        using var json = JsonDocument.Parse(JsonSerializer.Serialize(patch, metadata));
        Assert.False(json.RootElement.GetProperty("enabled").GetBoolean());
        Assert.Equal(0, json.RootElement.GetProperty("count").GetInt32());
        Assert.Equal(0, json.RootElement.GetProperty("bytes").GetInt64());
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("name").ValueKind);
        Assert.Equal(0m, json.RootElement.GetProperty("rate").GetDecimal());
        Assert.Equal(5, json.RootElement.EnumerateObject().Count());
        var roundtrip = JsonSerializer.Deserialize(json.RootElement.GetRawText(), metadata)!;
        Assert.Equal(patch, roundtrip);
    }

    [Fact]
    public void MissingAndNullPatchFieldsReadAsSeparateStates()
    {
        var metadata = PrimitiveTestJsonContext.Default.PrimitivePatch;
        var missing = JsonSerializer.Deserialize("{}", metadata)!;
        var explicitNull = JsonSerializer.Deserialize("""{"name":null,"enabled":null}""", metadata)!;
        Assert.False(missing.Name.IsSet);
        Assert.False(missing.Enabled.IsSet);
        Assert.True(explicitNull.Name.IsNull);
        Assert.True(explicitNull.Enabled.IsNull);
        Assert.Equal("{\"enabled\":null,\"name\":null}", JsonSerializer.Serialize(explicitNull, metadata));
    }

    [Fact]
    public void NestedAndArrayOptionalConvertersUseOnlyGeneratedValueMetadata()
    {
        var metadata = PrimitiveTestJsonContext.Default.PrimitiveNestedPatch;
        var patch = new PrimitiveNestedPatch { Item = new PrimitiveItem(7), Values = Array.Empty<int>() };
        Assert.Equal("{\"item\":{\"value\":7},\"values\":[]}", JsonSerializer.Serialize(patch, metadata));
        var read = JsonSerializer.Deserialize("""{"item":{"value":7},"values":[]}""", metadata)!;
        Assert.Equal(new PrimitiveItem(7), read.Item.Value);
        Assert.True(read.Values.IsSet);
        Assert.Empty(read.Values.Value);
        Assert.Equal("{}", JsonSerializer.Serialize(new PrimitiveNestedPatch(), metadata));
    }

    [Fact]
    public void UnsetOptionalAtRootFailsInsteadOfBecomingAnAccidentalNull()
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(default(Optional<bool>),
            SharedJsonSerializerContext.Default.OptionalBoolean));
        Assert.Equal("false", JsonSerializer.Serialize(Optional<bool>.Set(false),
            SharedJsonSerializerContext.Default.OptionalBoolean));
        Assert.Equal("null", JsonSerializer.Serialize(Optional<bool>.Null,
            SharedJsonSerializerContext.Default.OptionalBoolean));
        Assert.Equal("42.50", JsonSerializer.Serialize(Optional<decimal>.Set(42.50m),
            SharedJsonSerializerContext.Default.OptionalDecimal));
    }

    [Theory]
    [InlineData("\"001\"", StringOrIntegerKind.String)]
    [InlineData("\"\"", StringOrIntegerKind.String)]
    [InlineData("0", StringOrIntegerKind.Integer)]
    [InlineData("-1", StringOrIntegerKind.Integer)]
    [InlineData("9223372036854775807", StringOrIntegerKind.Integer)]
    public void ScalarUnionPreservesWireKindAndLeadingZeroStrings(string json, StringOrIntegerKind kind)
    {
        var metadata = SharedJsonSerializerContext.Default.StringOrInteger;
        var result = JsonSerializer.Deserialize(json, metadata);
        Assert.Equal(kind, result.Kind);
        Assert.Equal(json, JsonSerializer.Serialize(result, metadata));
        Assert.NotEqual(StringOrInteger.FromString("0"), StringOrInteger.FromInteger(0));
        if (kind == StringOrIntegerKind.String) Assert.Throws<InvalidOperationException>(() => result.Integer);
        else Assert.Throws<InvalidOperationException>(() => result.String);
    }

    [Theory]
    [InlineData("null")]
    [InlineData("true")]
    [InlineData("1.5")]
    [InlineData("9223372036854775808")]
    [InlineData("{}")]
    [InlineData("[]")]
    public void ScalarUnionRejectsUndocumentedKinds(string json) =>
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(json,
            SharedJsonSerializerContext.Default.StringOrInteger));

    [Fact]
    public void UndefinedScalarUnionCannotSilentlyBecomeIntegerZero()
    {
        Assert.Equal(StringOrIntegerKind.Undefined, default(StringOrInteger).Kind);
        Assert.False(default(StringOrInteger).IsInitialized);
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(default(StringOrInteger),
            SharedJsonSerializerContext.Default.StringOrInteger));
    }

    [Theory]
    [InlineData("{\"value\":4}", ObjectOrArrayKind.Object, 1)]
    [InlineData("[{\"value\":4}]", ObjectOrArrayKind.Array, 1)]
    [InlineData("[{\"value\":4},{\"value\":8}]", ObjectOrArrayKind.Array, 2)]
    [InlineData("[]", ObjectOrArrayKind.Array, 0)]
    public void TypedObjectArrayUnionPreservesShapeWhileOfferingAReadOnlyList(string json, ObjectOrArrayKind kind, int count)
    {
        var metadata = PrimitiveTestJsonContext.Default.ItemObjectOrArray;
        var result = JsonSerializer.Deserialize(json, metadata);
        Assert.Equal(kind, result.Kind);
        Assert.Equal(count, result.Items.Count);
        Assert.Equal(json, JsonSerializer.Serialize(result, metadata));
        if (count == 1) Assert.Equal(new PrimitiveItem(4), result.GetSingle());
        else Assert.Throws<InvalidOperationException>(() => result.GetSingle());
        if (kind == ObjectOrArrayKind.Array) Assert.Throws<InvalidOperationException>(() => result.Object);
    }

    [Theory]
    [InlineData("null")]
    [InlineData("42")]
    [InlineData("[null]")]
    [InlineData("[1]")]
    [InlineData("[[]]")]
    public void TypedObjectArrayUnionRejectsOtherKinds(string json) =>
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(json,
            PrimitiveTestJsonContext.Default.ItemObjectOrArray));

    [Fact]
    public void CollectionUnionCopiesInputAndDoesNotExposeAMutableArray()
    {
        PrimitiveItem[] original = [new(1)];
        var result = ObjectOrArray<PrimitiveItem>.FromArray(original);
        original[0] = new(2);
        Assert.Equal(new PrimitiveItem(1), result.Items[0]);
        var collection = Assert.IsAssignableFrom<ICollection<PrimitiveItem>>(result.Items);
        Assert.Throws<NotSupportedException>(() => collection.Add(new PrimitiveItem(3)));
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(default(ObjectOrArray<PrimitiveItem>),
            PrimitiveTestJsonContext.Default.ItemObjectOrArray));
    }

    [Fact]
    public void ObjectUnionAlsoEnforcesObjectShapeDuringWriting()
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(ObjectOrArray<int>.FromObject(7),
            PrimitiveTestJsonContext.Default.IntegerObjectOrArray));
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(ObjectOrArray<int>.FromArray([7]),
            PrimitiveTestJsonContext.Default.IntegerObjectOrArray));
        Assert.Throws<ArgumentException>(() => ObjectOrArray<PrimitiveItem>.FromArray([null!]));
    }

    [Theory]
    [InlineData("{}", EmptyObjectOrInt32ArrayKind.EmptyObject)]
    [InlineData("[]", EmptyObjectOrInt32ArrayKind.Array)]
    [InlineData("[0,7,-1]", EmptyObjectOrInt32ArrayKind.Array)]
    public void PeerRequestsUnionPreservesEmptyObjectAndIntegerArray(string json, EmptyObjectOrInt32ArrayKind kind)
    {
        var metadata = SharedJsonSerializerContext.Default.EmptyObjectOrInt32Array;
        var result = JsonSerializer.Deserialize(json, metadata);
        Assert.Equal(kind, result.Kind);
        Assert.Equal(json, JsonSerializer.Serialize(result, metadata));
    }

    [Theory]
    [InlineData("{\"piece\":1}")]
    [InlineData("null")]
    [InlineData("[null]")]
    [InlineData("[\"1\"]")]
    [InlineData("[2147483648]")]
    public void PeerRequestsUnionRejectsArbitraryMapsAndNonIntegers(string json) =>
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize(json,
            SharedJsonSerializerContext.Default.EmptyObjectOrInt32Array));

    [Fact]
    public void EncodedPathKeepsComposedAndDecomposedUnicodeDistinct()
    {
        var composed = EncodedFreeboxPath.FromUtf8Path("/Disque dur/Sp\u00e9cial");
        var decomposed = EncodedFreeboxPath.FromUtf8Path("/Disque dur/Spe\u0301cial");
        Assert.NotEqual(composed, decomposed);
        Assert.NotEqual(composed.Value, decomposed.Value);
        Assert.Equal("/Disque dur/Sp\u00e9cial", composed.DecodeUtf8Path());
        Assert.Equal("/Disque dur/Spe\u0301cial", decomposed.DecodeUtf8Path());
        Assert.Equal(Encoding.UTF8.GetBytes("/Disque dur/Spe\u0301cial"), decomposed.DecodeBytes());
        var metadata = SharedJsonSerializerContext.Default.EncodedFreeboxPath;
        var read = JsonSerializer.Deserialize(JsonSerializer.Serialize(decomposed, metadata), metadata);
        Assert.Equal(decomposed.Value, read.Value);
    }

    [Fact]
    public void OpaquePathPreservesWireEncodingAndEscapesReservedBase64CharactersOnce()
    {
        var path = EncodedFreeboxPath.FromEncoded("+/8=");
        Assert.Equal("+/8=", path.Value);
        Assert.Equal("%2B%2F8%3D", path.ToEscapedSegment());
        Assert.Throws<DecoderFallbackException>(() => path.DecodeUtf8Path());
        var whitespace = EncodedFreeboxPath.FromEncoded("L2E=\n");
        Assert.Equal("L2E=\n", whitespace.Value);
        Assert.Equal("/a", whitespace.DecodeUtf8Path());
        var metadata = SharedJsonSerializerContext.Default.EncodedFreeboxPath;
        Assert.Equal(whitespace.Value, JsonSerializer.Deserialize(JsonSerializer.Serialize(whitespace, metadata), metadata).Value);
    }

    [Fact]
    public void PathRejectsMalformedBase64AndInvalidUtf16InsteadOfReplacingBytes()
    {
        Assert.Throws<FormatException>(() => EncodedFreeboxPath.FromEncoded("%bad%"));
        Assert.Throws<EncoderFallbackException>(() => EncodedFreeboxPath.FromUtf8Path("/\ud800"));
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize("\"%bad%\"",
            SharedJsonSerializerContext.Default.EncodedFreeboxPath));
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize("null",
            SharedJsonSerializerContext.Default.EncodedFreeboxPath));
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(default(EncodedFreeboxPath),
            SharedJsonSerializerContext.Default.EncodedFreeboxPath));
    }
}
