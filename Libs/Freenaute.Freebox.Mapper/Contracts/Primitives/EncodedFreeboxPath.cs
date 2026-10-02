using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.Primitives;

/// <summary>
/// Preserves the exact Base64 path returned by Freebox, without Unicode or Base64 normalization.
/// Clear filenames and plaintext task descriptions are separate contracts.
/// </summary>
[JsonConverter(typeof(EncodedFreeboxPathJsonConverter))]
public readonly struct EncodedFreeboxPath : IEquatable<EncodedFreeboxPath>
{
    private static readonly UTF8Encoding StrictUtf8 = new(false, true);
    private readonly string? _encoded;
    private EncodedFreeboxPath(string encoded) => _encoded = encoded;

    public bool IsInitialized => _encoded is not null;
    public string Value => _encoded ?? throw new InvalidOperationException("This Freebox path is not initialized.");

    /// <summary>Validates Base64 syntax and retains the original wire string exactly.</summary>
    public static EncodedFreeboxPath FromEncoded(string encoded)
    {
        ArgumentNullException.ThrowIfNull(encoded);
        _ = Convert.FromBase64String(encoded);
        return new(encoded);
    }

    /// <summary>Encodes the supplied UTF8 path without NFC/NFD normalization or replacement of invalid characters.</summary>
    public static EncodedFreeboxPath FromUtf8Path(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        return new(Convert.ToBase64String(StrictUtf8.GetBytes(path)));
    }

    /// <summary>Decodes UTF8 strictly; never rewrites the stored wire value.</summary>
    public string DecodeUtf8Path() => StrictUtf8.GetString(Convert.FromBase64String(Value));
    public byte[] DecodeBytes() => Convert.FromBase64String(Value);
    public string ToEscapedSegment() => Uri.EscapeDataString(Value);

    public bool Equals(EncodedFreeboxPath other) => string.Equals(_encoded, other._encoded, StringComparison.Ordinal);
    public override bool Equals(object? obj) => obj is EncodedFreeboxPath other && Equals(other);
    public override int GetHashCode() => _encoded is null ? 0 : StringComparer.Ordinal.GetHashCode(_encoded);
    public static bool operator ==(EncodedFreeboxPath left, EncodedFreeboxPath right) => left.Equals(right);
    public static bool operator !=(EncodedFreeboxPath left, EncodedFreeboxPath right) => !left.Equals(right);
}

public sealed class EncodedFreeboxPathJsonConverter : JsonConverter<EncodedFreeboxPath>
{
    public override bool HandleNull => true;
    public override EncodedFreeboxPath Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException("Expected a Base64-encoded Freebox path string.");
        try
        {
            return EncodedFreeboxPath.FromEncoded(reader.GetString()!);
        }
        catch (FormatException exception)
        {
            throw new JsonException("The Freebox path has invalid Base64 syntax.", exception);
        }
    }

    public override void Write(Utf8JsonWriter writer, EncodedFreeboxPath value, JsonSerializerOptions options)
    {
        if (!value.IsInitialized)
            throw new JsonException("An uninitialized Freebox path cannot be written.");
        writer.WriteStringValue(value.Value);
    }
}
