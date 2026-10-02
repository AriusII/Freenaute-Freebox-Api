using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Client.Tests;

public sealed record FixtureCommand([property: JsonPropertyName("value")] string Value);
public sealed record FixtureResult([property: JsonPropertyName("value")] string Value);

[JsonSerializable(typeof(FixtureCommand))]
[JsonSerializable(typeof(FixtureResult))]
internal partial class TestJsonContext : JsonSerializerContext;
