using System.Text.Json;
using System.Text.Json.Nodes;
using Freenaute.Freebox.Documentation;
using Xunit;

namespace Freenaute.Freebox.Documentation.Tests;

public sealed class DocumentationTests
{
    [Fact]
    public void VerifyKeepsSuppliedProvenanceAndDeclaredDefects()
    {
        using var fixture = new SnapshotFixture();
        var result = SnapshotCorpus.Load(fixture.Root).Verification();
        Assert.False(JsonSerializer.IsReflectionEnabledByDefault);
        Assert.False(result["origin_independently_verified"]!.GetValue<bool>());
        Assert.False(result["full_sdk_implemented"]!.GetValue<bool>());
        Assert.Single(result["declared_markdown_defects"]!.AsArray());
        Assert.Single(result["declared_measurement_discrepancies"]!.AsArray());
    }

    [Fact]
    public void TamperedSourceFailsBeforeContextOutput()
    {
        using var fixture = new SnapshotFixture();
        fixture.Write(SnapshotFixture.Snapshot + SnapshotFixture.Raw, DocumentFiles.Utf8("tampered"));
        Assert.Throws<InvalidDataException>(() => SnapshotCorpus.Load(fixture.Root));
        Assert.False(Directory.Exists(Path.Combine(fixture.Root, "context")));
    }

    [Fact]
    public void CurrentVersionAndReviewDigestMustMatch()
    {
        using var fixture = new SnapshotFixture();
        var current = DocumentFiles.Json(fixture.Root, SnapshotFixture.Official + "current.json");
        current["api_version"] = "15.0";
        fixture.Write(SnapshotFixture.Official + "current.json", current);
        Assert.Throws<InvalidDataException>(() => SnapshotCorpus.Load(fixture.Root));
        current["api_version"] = "16.0";
        fixture.Write(SnapshotFixture.Official + "current.json", current);
        fixture.Write(SnapshotFixture.Official + "reviews/protocol.json", DocumentFiles.Utf8("{}"));
        Assert.Throws<InvalidDataException>(() => SnapshotCorpus.Load(fixture.Root));
    }

    [Theory]
    [InlineData("../outside")]
    [InlineData("/absolute")]
    [InlineData("directory\\windows")]
    [InlineData("directory/../outside")]
    [InlineData("directory//file")]
    [InlineData("C:/outside")]
    public void PathsMustBePortableAndContained(string path)
    {
        Assert.Throws<InvalidDataException>(() => DocumentFiles.Resolve(Path.GetTempPath(), path));
    }

    [Fact]
    public void SourceSymlinkIsRejectedEvenIfBytesMatch()
    {
        using var fixture = new SnapshotFixture();
        var source = DocumentFiles.Resolve(fixture.Root, SnapshotFixture.Snapshot + SnapshotFixture.Raw);
        var outside = Path.Combine(fixture.Root, "outside.html");
        File.Move(source, outside);
        File.CreateSymbolicLink(source, outside);
        Assert.Throws<InvalidDataException>(() => SnapshotCorpus.Load(fixture.Root));
    }

    [Fact]
    public void ContextIsPortableRepeatableAndProtectsUserEdits()
    {
        using var fixture = new SnapshotFixture();
        var corpus = SnapshotCorpus.Load(fixture.Root);
        Assert.Equal(1, DocumentationCommands.Context(corpus, "context")["task_count"]!.GetValue<int>());
        var first = DocumentFiles.Read(fixture.Root, "context/index.json");
        DocumentationCommands.Context(corpus, "context");
        Assert.Equal(first, DocumentFiles.Read(fixture.Root, "context/index.json"));
        Assert.DoesNotContain(fixture.Root, File.ReadAllText(Path.Combine(fixture.Root, "context/index.json")), StringComparison.Ordinal);
        fixture.Write("context/index.json", DocumentFiles.Utf8("User edited context"));
        Assert.Throws<InvalidDataException>(() => DocumentationCommands.Context(corpus, "context"));
        Assert.Equal("User edited context", File.ReadAllText(Path.Combine(fixture.Root, "context/index.json")));
    }

    [Fact]
    public void EventWithoutModuleUsesItsDeclaringPropertyCitation()
    {
        using var fixture = new SnapshotFixture(includeUnassignedEvent: true);
        var corpus = SnapshotCorpus.Load(fixture.Root);
        DocumentationCommands.Context(corpus, "context");
        var projection = DocumentFiles.Json(fixture.Root, "context/sources/review16-login.json");
        Assert.Single(projection["events"]!.AsArray());
        var result = DocumentationCommands.Coverage(corpus, "coverage-output", "implementations");
        Assert.Equal(0, result["missing_review_citations"]!.GetValue<int>());
    }

    [Fact]
    public void UnownedDirectoryAndFixedTemporaryFileArePreserved()
    {
        using var fixture = new SnapshotFixture();
        var corpus = SnapshotCorpus.Load(fixture.Root);
        fixture.Write("unowned/index.json", DocumentFiles.Utf8("User content"));
        Assert.Throws<InvalidDataException>(() => DocumentationCommands.Context(corpus, "unowned"));
        fixture.Write("owned/index.json.tmp", DocumentFiles.Utf8("User temporary file"));
        File.Delete(Path.Combine(fixture.Root, "owned/index.json.tmp"));
        Directory.Delete(Path.Combine(fixture.Root, "owned"));
        DocumentationCommands.Context(corpus, "owned");
        fixture.Write("owned/index.json.tmp", DocumentFiles.Utf8("User temporary file"));
        DocumentationCommands.Context(corpus, "owned");
        Assert.Equal("User temporary file", File.ReadAllText(Path.Combine(fixture.Root, "owned/index.json.tmp")));
    }

    [Fact]
    public void MissingReviewCitationIsReportedWithoutImplementationApproval()
    {
        using var fixture = new SnapshotFixture();
        fixture.Review["sources"]![0]!["anchors"] = new JsonArray("login", "Challenge", "Challenge.challenge");
        fixture.SaveReviewChain();
        var result = DocumentationCommands.Coverage(SnapshotCorpus.Load(fixture.Root), "coverage-output", "implementations");
        Assert.Equal(1, result["missing_review_citations"]!.GetValue<int>());
        Assert.False(result["full_sdk_implemented"]!.GetValue<bool>());
    }

    [Fact]
    public void ImplementationClaimsAreReconciledAndEvidenceHashesAreRequiredWhenSupplied()
    {
        using var fixture = new SnapshotFixture();
        var manifest = fixture.Manifest();
        fixture.Write("Libs/Auth.cs", DocumentFiles.Utf8("// synthetic evidence, never executed"));
        manifest["evidence_files"] = new JsonArray((JsonNode)new JsonObject
        {
            ["path"] = "Libs/Auth.cs",
            ["sha256"] = DocumentFiles.Hash(DocumentFiles.Read(fixture.Root, "Libs/Auth.cs"))
        });
        fixture.Write("implementations/protocol.json", manifest);
        var corpus = SnapshotCorpus.Load(fixture.Root);
        var result = DocumentationCommands.Coverage(corpus, "coverage-output", "implementations");
        Assert.Equal(1, result["reported_implemented_operations"]!.GetValue<int>());
        Assert.False(result["full_sdk_implemented"]!.GetValue<bool>());
        fixture.Write("Libs/Auth.cs", DocumentFiles.Utf8("user changes"));
        Assert.Throws<InvalidDataException>(() => DocumentationCommands.Coverage(corpus, "new-coverage", "implementations"));
        Assert.False(Directory.Exists(Path.Combine(fixture.Root, "new-coverage")));
    }

    [Fact]
    public void GuessedRouteAndDuplicateImplementationClaimsFailClosed()
    {
        using var fixture = new SnapshotFixture();
        var manifest = fixture.Manifest();
        manifest["implemented_operations"]![0]!["documented_path"] = "/api/v16/guessed";
        fixture.Write("implementations/protocol.json", manifest);
        var corpus = SnapshotCorpus.Load(fixture.Root);
        Assert.Throws<InvalidDataException>(() => DocumentationCommands.Coverage(corpus, "coverage-output", "implementations"));
        manifest["implemented_operations"]![0]!["documented_path"] = "/api/v8/login/";
        var operations = manifest["implemented_operations"]!.AsArray();
        operations.Add(operations[0]!.DeepClone());
        fixture.Write("implementations/protocol.json", manifest);
        Assert.Throws<InvalidDataException>(() => DocumentationCommands.Coverage(corpus, "coverage-output", "implementations"));
    }

    [Fact]
    public void ExampleOnlyOperationRequiresItsOwnExactRequestAndModule()
    {
        using var fixture = new SnapshotFixture(includeRequestExample: true);
        var corpus = SnapshotCorpus.Load(fixture.Root);
        var source = SnapshotCorpus.SourceUrl + "#example";
        Assert.True(corpus.ContainsRequestExample(source, "POST", "/api/v8/login/example", "login"));
        Assert.False(corpus.ContainsRequestExample(source, "DELETE", "/api/v8/incorrect", "login"));
        Assert.False(corpus.ContainsRequestExample(source, "POST", "/api/v8/login/example", "wifi"));
        var manifest = fixture.Manifest();
        var operation = manifest["implemented_operations"]![0]!;
        operation["method"] = "POST";
        operation["documented_path"] = "/api/v8/login/example";
        operation["source"] = source;
        operation["relative_path"] = "login/example";
        fixture.Write("implementations/protocol.json", manifest);
        Assert.Throws<InvalidDataException>(() => DocumentationCommands.Coverage(corpus, "coverage-output", "implementations"));
        operation["documentation_kind"] = "example_only_documented";
        fixture.Write("implementations/protocol.json", manifest);
        var result = DocumentationCommands.Coverage(corpus, "coverage-output", "implementations");
        Assert.Equal(1, result["reported_example_only_operations"]!.GetValue<int>());
        var coverage = DocumentFiles.Json(fixture.Root, "coverage-output/coverage.json");
        Assert.Equal(0, coverage["modules"]![0]!["claimed_literal_operations"]!.GetValue<int>());
    }

    [Fact]
    public void BothCompoundAlternativesAreNeededToClaimTheirLiteralSignature()
    {
        using var fixture = new SnapshotFixture(includeCompoundPath: true);
        var manifest = fixture.Manifest();
        fixture.Write("implementations/protocol.json", manifest);
        var corpus = SnapshotCorpus.Load(fixture.Root);
        DocumentationCommands.Coverage(corpus, "coverage-output", "implementations");
        var first = DocumentFiles.Json(fixture.Root, "coverage-output/coverage.json");
        Assert.Equal(0, first["modules"]![0]!["claimed_literal_operations"]!.GetValue<int>());
        var alternative = manifest["implemented_operations"]![0]!.DeepClone();
        alternative["documented_path"] = "/api/v8/login/alternate";
        alternative["relative_path"] = "login/alternate";
        manifest["implemented_operations"]!.AsArray().Add(alternative);
        fixture.Write("implementations/protocol.json", manifest);
        DocumentationCommands.Coverage(corpus, "coverage-output", "implementations");
        var second = DocumentFiles.Json(fixture.Root, "coverage-output/coverage.json");
        Assert.Equal(2, second["modules"]![0]!["reported_implemented_operations"]!.GetValue<int>());
        Assert.Equal(1, second["modules"]![0]!["claimed_literal_operations"]!.GetValue<int>());
    }

    [Fact]
    public void TwoClientBindingsDoNotInflateOneHttpOperation()
    {
        using var fixture = new SnapshotFixture();
        var manifest = fixture.Manifest();
        var second = manifest["implemented_operations"]![0]!.DeepClone();
        second["client_member"] = "Authentication.GetChallengeAlternativeAsync";
        manifest["implemented_operations"]!.AsArray().Add(second);
        fixture.Write("implementations/protocol.json", manifest);
        var result = DocumentationCommands.Coverage(SnapshotCorpus.Load(fixture.Root), "coverage-output", "implementations");
        Assert.Equal(1, result["reported_implemented_operations"]!.GetValue<int>());
        Assert.Equal(2, result["reported_client_bindings"]!.GetValue<int>());
    }

    [Fact]
    public void ExplicitEvidenceRefreshChangesOnlyExistingHashRecords()
    {
        using var fixture = new SnapshotFixture();
        var manifest = fixture.Manifest();
        fixture.Write("Libs/Auth.cs", DocumentFiles.Utf8("updated code"));
        manifest["evidence_files"] = new JsonArray((JsonNode)new JsonObject { ["path"] = "Libs/Auth.cs", ["sha256"] = new string('0', 64) });
        fixture.Write("implementations/protocol.json", manifest);
        var originalOperations = manifest["implemented_operations"]!.ToJsonString();
        var result = DocumentationCommands.RefreshEvidence(fixture.Root, "implementations");
        Assert.Equal(1, result["refreshed_evidence_hashes"]!.GetValue<int>());
        var changed = DocumentFiles.Json(fixture.Root, "implementations/protocol.json");
        Assert.Equal(originalOperations, changed["implemented_operations"]!.ToJsonString());
        Assert.Equal(DocumentFiles.Hash(DocumentFiles.Read(fixture.Root, "Libs/Auth.cs")), changed["evidence_files"]![0]!["sha256"]!.GetValue<string>());
        Assert.Equal(0, DocumentationCommands.RefreshEvidence(fixture.Root, "implementations")["updated_manifests"]!.GetValue<int>());
        Assert.Throws<InvalidDataException>(() => DocumentFiles.ReplaceUnchanged(fixture.Root, "implementations/protocol.json", [], new string('0', 64)));
        fixture.Write("implementations/.documentation-refresh.lock", DocumentFiles.Utf8("User file or another active refresh"));
        Assert.Throws<IOException>(() => DocumentationCommands.RefreshEvidence(fixture.Root, "implementations"));
        Assert.Equal("User file or another active refresh", File.ReadAllText(Path.Combine(fixture.Root, "implementations/.documentation-refresh.lock")));
    }

    [Fact]
    public void InlineModelRequiresActiveRawSourceAndAssignedReviewCitation()
    {
        using var fixture = new SnapshotFixture(includeRequestExample: true);
        var manifest = fixture.Manifest();
        manifest["contract_models"] = new JsonArray((JsonNode)new JsonObject { ["module"] = "login", ["name"] = "ExampleRequest", ["source"] = SnapshotCorpus.SourceUrl + "#example" });
        fixture.Write("implementations/protocol.json", manifest);
        Assert.Throws<InvalidDataException>(() => DocumentationCommands.Coverage(SnapshotCorpus.Load(fixture.Root), "coverage-output", "implementations"));
        fixture.Review["sources"]![0]!["anchors"]!.AsArray().Add((JsonNode)JsonValue.Create("example")!);
        fixture.SaveReviewChain();
        DocumentationCommands.Coverage(SnapshotCorpus.Load(fixture.Root), "coverage-output", "implementations");
        fixture.Report["audit"]!["deprecation_rules"] = new JsonArray((JsonNode)new JsonObject { ["kind"] = "section", ["id"] = "example", ["verified_removed_nodes"] = 1 });
        fixture.SaveReviewChain();
        Assert.Throws<InvalidDataException>(() => DocumentationCommands.Coverage(SnapshotCorpus.Load(fixture.Root), "new-coverage", "implementations"));
    }
}
