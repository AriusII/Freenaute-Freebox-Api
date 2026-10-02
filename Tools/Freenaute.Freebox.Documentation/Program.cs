using System.Text.Json.Nodes;

namespace Freenaute.Freebox.Documentation;

internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Length == 0 || args.Contains("--help", StringComparer.Ordinal))
        {
            Console.WriteLine("Freebox documentation: verify|context|coverage|refresh-evidence [--root <repository>] [--output <directory>] [--implementations <directory>]\nDefaults: root=.; context=docs/freebox-official/context-net10; coverage=docs/freebox-official/coverage-net10; implementations=docs/implementation.\nAll commands are offline. refresh-evidence explicitly updates declared hashes after authorized edits. Supplied source bytes and implementation declarations do not establish live API behavior.");
            return 0;
        }
        try
        {
            var options = new Dictionary<string, string>(StringComparer.Ordinal);
            for (var index = 1; index < args.Length; index += 2)
            {
                if (index + 1 >= args.Length || args[index] is not ("--root" or "--output" or "--implementations") ||
                    !options.TryAdd(args[index], args[index + 1]))
                    throw new InvalidDataException("Expected unique --root, --output or --implementations option/value pairs.");
            }
            var corpus = SnapshotCorpus.Load(options.GetValueOrDefault("--root", "."));
            JsonObject result = args[0] switch
            {
                "verify" => corpus.Verification(),
                "context" => DocumentationCommands.Context(corpus, options.GetValueOrDefault("--output", "docs/freebox-official/context-net10")),
                "coverage" => DocumentationCommands.Coverage(corpus, options.GetValueOrDefault("--output", "docs/freebox-official/coverage-net10"),
                    options.GetValueOrDefault("--implementations", "docs/implementation")),
                "refresh-evidence" => DocumentationCommands.RefreshEvidence(corpus.Root, options.GetValueOrDefault("--implementations", "docs/implementation")),
                _ => throw new InvalidDataException("Unknown command. Use --help.")
            };
            Console.WriteLine(result.ToJsonString());
            return 0;
        }
        catch (Exception error) when (error is IOException or InvalidDataException or UnauthorizedAccessException or ArgumentException or
                                      InvalidOperationException or System.Text.Json.JsonException)
        {
            Console.Error.WriteLine($"Documentation validation failed: {error.Message}");
            return 2;
        }
    }
}
