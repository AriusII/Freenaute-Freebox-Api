using System.Text.Json.Nodes;
using Freenaute.Freebox.Documentation;

namespace Freenaute.Freebox.Documentation.Tests;

internal sealed class SnapshotFixture : IDisposable
{
    public string Root { get; } = Path.Combine(Path.GetTempPath(), "freebox-doc-tests-" + Guid.NewGuid().ToString("N"));
    public const string Official = "docs/freebox-official/";
    public const string Snapshot = Official + "api-16.0/";
    public const string Raw = "sources/raw/embedded/doc/index.html";
    public JsonObject Report { get; }
    public JsonObject Coverage { get; }
    public JsonObject Review { get; }

    public SnapshotFixture(bool includeUnassignedEvent = false, bool includeRequestExample = false, bool includeCompoundPath = false)
    {
        var bodies = new Dictionary<string, byte[]>
        {
            [Raw] = DocumentFiles.Utf8("<title>Freebox api b'abcdef12'</title><p>Current API version is “16.0”</p><span id=\"document-api/login\"></span><h1 id=\"login\">Login</h1>" +
                (includeRequestExample ? "<div class=\"section\" id=\"example\"><h2>Example</h2><div><pre><span>POST</span> /api/v8/login/example HTTP/1.1\nContent-Type: application/json\n</pre></div></div><div class=\"section\" id=\"other\"><pre>DELETE /api/v8/incorrect HTTP/1.1\n</pre></div>" : "")),
            ["sources/api-version.json"] = DocumentFiles.Utf8("{\"result\":{\"api_version\":\"16.0\"}}"),
            ["docs/reference/login.md"] = DocumentFiles.Utf8("# Login\n")
        };
        var endpoint = new JsonObject
        {
            ["module"] = "login",
            ["method"] = "GET",
            ["documented_path"] = "/api/v8/login/",
            ["source"] = SnapshotCorpus.SourceUrl + "#get-login",
            ["referenced_objects"] = new JsonArray("Challenge")
        };
        if (includeCompoundPath) endpoint["documented_path"] = "/api/v8/login/ & /api/v8/login/alternate";
        var catalogues = new Dictionary<string, JsonArray>
        {
            ["modules"] = new((JsonNode)new JsonObject { ["module"] = "login", ["title"] = "Login", ["path"] = "docs/reference/login.md", ["source"] = SnapshotCorpus.SourceUrl + "#login" }),
            ["endpoints"] = new(endpoint.DeepClone()),
            ["operations"] = new(endpoint.DeepClone()),
            ["objects"] = new((JsonNode)new JsonObject { ["module"] = "login", ["name"] = "Challenge", ["source"] = SnapshotCorpus.SourceUrl + "#Challenge" }),
            ["properties"] = new((JsonNode)new JsonObject { ["module"] = "login", ["source"] = SnapshotCorpus.SourceUrl + "#Challenge.challenge" }),
            ["events"] = includeUnassignedEvent ? new JsonArray((JsonNode)new JsonObject
            {
                ["name"] = "value_changed",
                ["result_object"] = "Challenge",
                ["source"] = SnapshotCorpus.SourceUrl + "#Challenge.challenge"
            }) : []
        };
        foreach (var (name, value) in catalogues) bodies[$"catalogue/{name}.json"] = DocumentFiles.Encode(value);
        var originals = new JsonArray(bodies.Select(pair => (JsonNode?)new JsonObject
        {
            ["path"] = pair.Key,
            ["sha256"] = DocumentFiles.Hash(pair.Value),
            ["bytes"] = pair.Value.Length
        }).ToArray());
        bodies["LIVRAISON.json"] = DocumentFiles.Encode(new JsonObject { ["api_version"] = "16.0", ["source_build"] = "abcdef12", ["files"] = originals });
        foreach (var (name, body) in bodies) Write(Snapshot + name, body);
        var rawHash = DocumentFiles.Hash(bodies[Raw]);
        var archiveHash = new string('a', 64);
        Report = new JsonObject
        {
            ["generator"] = "freebox-sdk-snapshot-import",
            ["integrity_passed"] = true,
            ["provenance"] = new JsonObject { ["origin_independently_verified"] = false },
            ["source_snapshot"] = new JsonObject { ["api_version"] = "16.0", ["source_build"] = "abcdef12", ["delivery_manifest_sha256"] = DocumentFiles.Hash(bodies["LIVRAISON.json"]) },
            ["archive"] = new JsonObject { ["sha256"] = archiveHash },
            ["counts"] = new JsonObject { ["copied_files"] = bodies.Count },
            ["files"] = new JsonArray(bodies.Select(pair => (JsonNode?)new JsonObject { ["path"] = pair.Key, ["action"] = "copied", ["sha256"] = DocumentFiles.Hash(pair.Value), ["bytes"] = pair.Value.Length }).ToArray()),
            ["audit"] = new JsonObject
            {
                ["primary_source"] = new JsonObject { ["sha256"] = rawHash },
                ["closure"] = new JsonObject { ["reference_closure_complete"] = true, ["all_resources_available"] = false },
                ["markdown_links"] = new JsonObject { ["defects"] = new JsonArray((JsonNode)new JsonObject { ["reason"] = "known_markdown_defect" }) },
                ["supplied_measurement_discrepancies"] = new JsonArray((JsonNode)new JsonObject { ["supplied"] = 1, ["independently_measured"] = 2 })
            }
        };
        Review = new JsonObject
        {
            ["sources"] = new JsonArray((JsonNode)new JsonObject
            {
                ["url"] = SnapshotCorpus.SourceUrl,
                ["sha256"] = rawHash,
                ["anchors"] = new JsonArray("login", "get-login", "Challenge", "Challenge.challenge")
            })
        };
        var counts = new JsonObject();
        foreach (var (kind, data) in catalogues) counts[kind] = data.Count;
        Coverage = new JsonObject
        {
            ["api_version"] = "16.0",
            ["counts"] = counts,
            ["source"] = new JsonObject { ["raw_document_sha256"] = rawHash },
            ["groups"] = new JsonArray((JsonNode)new JsonObject { ["group"] = "protocol", ["modules"] = new JsonArray("login"), ["review_file"] = "reviews/protocol.json" })
        };
        SaveReviewChain();
        Write(Official + "current.json", new JsonObject
        {
            ["source_kind"] = "user_supplied_archive",
            ["snapshot"] = "api-16.0",
            ["api_version"] = "16.0",
            ["import_report"] = "import-report.json",
            ["review_coverage"] = "coverage.json",
            ["raw_document"] = "api-16.0/" + Raw,
            ["raw_document_sha256"] = rawHash,
            ["archive_sha256"] = archiveHash,
            ["origin_independently_verified"] = false
        });
        Write(Official + "reviews/source-integrity.json", new JsonObject
        {
            ["integrity_passed"] = true,
            ["source_archive_sha256"] = archiveHash,
            ["audit"] = new JsonObject { ["primary_source"] = new JsonObject { ["sha256"] = rawHash } }
        });
    }

    public void SaveReviewChain()
    {
        Write(Official + "import-report.json", Report);
        Write(Official + "reviews/protocol.json", Review);
        Coverage["source"]!["import_report_sha256"] = DocumentFiles.Hash(DocumentFiles.Read(Root, Official + "import-report.json"));
        Coverage["groups"]![0]!["review_sha256"] = DocumentFiles.Hash(DocumentFiles.Read(Root, Official + "reviews/protocol.json"));
        Write(Official + "coverage.json", Coverage);
    }

    public JsonObject Manifest() => new()
    {
        ["format_version"] = 1,
        ["group"] = "protocol",
        ["modules"] = new JsonArray("login"),
        ["implemented_operations"] = new JsonArray((JsonNode)new JsonObject
        {
            ["module"] = "login",
            ["method"] = "GET",
            ["documented_path"] = "/api/v8/login/",
            ["relative_path"] = "login/",
            ["source"] = SnapshotCorpus.SourceUrl + "#get-login",
            ["client_member"] = "Authentication.GetChallengeAsync"
        }),
        ["unsupported_operations"] = new JsonArray(),
        ["contract_models"] = new JsonArray(),
        ["evidence_files"] = new JsonArray()
    };

    public void Write(string name, JsonNode data) => Write(name, DocumentFiles.Encode(data));
    public void Write(string name, byte[] bytes)
    {
        var path = DocumentFiles.Resolve(Root, name);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllBytes(path, bytes);
    }
    public void Dispose() => Directory.Delete(Root, recursive: true);
}
