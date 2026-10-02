using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Freenaute.Freebox.Documentation;

public static class DocumentFiles
{
    public static string Hash(ReadOnlySpan<byte> bytes) => Convert.ToHexStringLower(SHA256.HashData(bytes));

    public static string Resolve(string root, string relative)
    {
        if (string.IsNullOrEmpty(relative) || relative.Contains('\\') || relative.Contains(':') ||
            relative.Any(character => character < ' ' || "<>\"|?*".Contains(character, StringComparison.Ordinal)) ||
            Path.IsPathRooted(relative) || relative.Split('/').Any(part => part is "" or "." or ".." || part.EndsWith(' ') || part.EndsWith('.')))
            throw new InvalidDataException($"Expected a canonical contained portable path: {relative}");
        var basePath = Path.GetFullPath(root);
        var current = basePath;
        RejectLink(current);
        foreach (var part in relative.Split('/'))
        {
            current = Path.Combine(current, part);
            RejectLink(current);
        }
        return current;
    }

    public static void RejectLink(string path)
    {
        var info = new FileInfo(path);
        if (info.LinkTarget is not null || Directory.Exists(path) && new DirectoryInfo(path).LinkTarget is not null ||
            (File.Exists(path) || Directory.Exists(path)) && File.GetAttributes(path).HasFlag(FileAttributes.ReparsePoint))
            throw new InvalidDataException($"Symbolic links/reparse points are not supported: {path}");
    }

    public static byte[] Read(string root, string relative) => File.ReadAllBytes(Resolve(root, relative));
    public static JsonNode Json(string root, string relative) => JsonNode.Parse(Read(root, relative)) ??
        throw new InvalidDataException($"JSON document is null: {relative}");
    public static string Text(JsonNode? node, string key) => node?[key]?.GetValue<string>() ??
        throw new InvalidDataException($"Missing string field: {key}");
    public static JsonArray Array(JsonNode? node, string key) => node?[key] as JsonArray ??
        throw new InvalidDataException($"Missing array field: {key}");
    public static JsonArray Strings(IEnumerable<string> values) => new(values.Select(value => (JsonNode?)JsonValue.Create(value)).ToArray());

    public static byte[] Encode(JsonNode node)
    {
        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
            node.WriteTo(writer);
        return [.. stream.ToArray(), (byte)'\n'];
    }

    public static byte[] Utf8(string text) => Encoding.UTF8.GetBytes(text.Replace("\r\n", "\n", StringComparison.Ordinal));

    public static void ReplaceUnchanged(string root, string relative, byte[] bytes, string expectedPreviousHash)
    {
        var path = Resolve(root, relative);
        if (Hash(File.ReadAllBytes(path)) != expectedPreviousHash)
            throw new InvalidDataException($"Metadata changed during hash refresh: {relative}");
        var temporary = Path.Combine(Path.GetDirectoryName(path)!, $".documentation-{Guid.NewGuid():N}.tmp");
        try
        {
            using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None)) stream.Write(bytes);
            if (Hash(Read(root, relative)) != expectedPreviousHash)
                throw new InvalidDataException($"Metadata changed during hash refresh: {relative}");
            File.Move(temporary, path, overwrite: true);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
}

/// <summary>Only updates files recorded by this tool, after verifying their previous bytes.</summary>
public static class OwnedOutput
{
    public const string Generator = "freenaute-freebox-documentation-net10";
    private const string Ownership = ".documentation-output.json";

    public static void Write(string root, string output, IReadOnlyDictionary<string, byte[]> files)
    {
        var directory = DocumentFiles.Resolve(root, output);
        var marker = DocumentFiles.Resolve(directory, Ownership);
        var previous = new Dictionary<string, string>(StringComparer.Ordinal);
        if (Directory.Exists(directory))
        {
            if (!File.Exists(marker))
                throw new InvalidDataException("Existing output directory lacks this tool's ownership manifest. Choose a new directory.");
            var old = DocumentFiles.Json(directory, Ownership);
            if (DocumentFiles.Text(old, "generator") != Generator)
                throw new InvalidDataException("Output belongs to another generator.");
            foreach (var item in DocumentFiles.Array(old, "files"))
            {
                var name = DocumentFiles.Text(item!, "path");
                if (name == Ownership || !previous.TryAdd(name, DocumentFiles.Text(item!, "sha256")))
                    throw new InvalidDataException("Invalid ownership inventory.");
                if (DocumentFiles.Hash(DocumentFiles.Read(directory, name)) != previous[name])
                    throw new InvalidDataException($"User-edited generated file will not be overwritten: {name}");
            }
        }
        // Complete every preflight before creating or updating output files.
        foreach (var name in files.Keys)
        {
            var path = DocumentFiles.Resolve(directory, name);
            if (name == Ownership || Directory.Exists(path) || File.Exists(path) && !previous.ContainsKey(name))
                throw new InvalidDataException($"Refusing to overwrite an unowned output: {name}");
        }
        Directory.CreateDirectory(directory);
        foreach (var (name, bytes) in files)
            AtomicWrite(DocumentFiles.Resolve(directory, name), bytes, previous.ContainsKey(name));
        foreach (var stale in previous.Keys.Except(files.Keys, StringComparer.Ordinal))
            File.Delete(DocumentFiles.Resolve(directory, stale));
        var records = new JsonArray(files.OrderBy(pair => pair.Key, StringComparer.Ordinal).Select(pair => (JsonNode?)new JsonObject
        { ["path"] = pair.Key, ["sha256"] = DocumentFiles.Hash(pair.Value), ["bytes"] = pair.Value.Length }).ToArray());
        AtomicWrite(marker, DocumentFiles.Encode(new JsonObject { ["format_version"] = 1, ["generator"] = Generator, ["files"] = records }), File.Exists(marker));
    }

    private static void AtomicWrite(string path, byte[] bytes, bool overwrite)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var temporary = Path.Combine(Path.GetDirectoryName(path)!, $".documentation-{Guid.NewGuid():N}.tmp");
        try
        {
            using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                stream.Write(bytes);
            DocumentFiles.RejectLink(path);
            File.Move(temporary, path, overwrite);
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
}
