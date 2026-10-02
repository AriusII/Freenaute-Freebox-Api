using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace Freenaute.Freebox.Documentation;

/// <summary>Checks the selected supplied snapshot and its recorded review chain without network access.</summary>
public sealed partial class SnapshotCorpus
{
    public const string SourceUrl = "http://mafreebox.freebox.fr/doc/index.html";
    private const string Official = "docs/freebox-official";
    private readonly Dictionary<string, JsonNode> records = new(StringComparer.Ordinal);
    private readonly string rawHtml;
    public string Root { get; }
    public string Snapshot { get; }
    public string ApiVersion { get; }
    public string RawHash { get; }
    public string RawFile { get; }
    public JsonNode Current { get; }
    public JsonNode Report { get; }
    public JsonNode ReviewCoverage { get; }
    public Dictionary<string, JsonArray> Catalogues { get; } = new(StringComparer.Ordinal);
    public Dictionary<string, JsonNode> Reviews { get; } = new(StringComparer.Ordinal);
    public Dictionary<string, string> ReviewHashes { get; } = new(StringComparer.Ordinal);
    public string ReportHash { get; }

    public static SnapshotCorpus Load(string root) => new(root);

    private SnapshotCorpus(string root)
    {
        Root = Path.GetFullPath(root);
        Current = DocumentFiles.Json(Root, Official + "/current.json");
        Snapshot = Official + "/" + DocumentFiles.Text(Current, "snapshot");
        var reportName = Official + "/" + DocumentFiles.Text(Current, "import_report");
        var reportBytes = DocumentFiles.Read(Root, reportName);
        ReportHash = DocumentFiles.Hash(reportBytes);
        Report = JsonNode.Parse(reportBytes)!;
        Require(DocumentFiles.Text(Report, "generator") == "freebox-sdk-snapshot-import" &&
            Report["integrity_passed"]?.GetValue<bool>() == true, "A successful supplied snapshot import is required.");
        Require(DocumentFiles.Text(Current, "source_kind") == "user_supplied_archive" &&
            Current["origin_independently_verified"]?.GetValue<bool>() == false &&
            Report["provenance"]?["origin_independently_verified"]?.GetValue<bool>() == false,
            "Supplied snapshot provenance must retain its unverified remote-origin classification.");
        foreach (var record in DocumentFiles.Array(Report, "files"))
        {
            var name = DocumentFiles.Text(record!, "path");
            Require(records.TryAdd(name, record!), "Duplicate import record.");
            var path = DocumentFiles.Resolve(Root, Snapshot + "/" + name);
            var action = DocumentFiles.Text(record!, "action");
            if (action == "copied") ReadChecked(name);
            else Require(action == "omitted" && name.StartsWith("tools/", StringComparison.Ordinal) &&
                !File.Exists(path) && !Directory.Exists(path), "Invalid omitted uploaded-tool record.");
        }
        Require(records.Values.Count(record => DocumentFiles.Text(record, "action") == "copied") ==
            Report["counts"]?["copied_files"]?.GetValue<int>(), "Copied file count disagrees with the import report.");
        var delivery = JsonNode.Parse(ReadChecked("LIVRAISON.json"))!;
        var listed = new HashSet<string>(StringComparer.Ordinal);
        foreach (var entry in DocumentFiles.Array(delivery, "files"))
        {
            var name = DocumentFiles.Text(entry!, "path");
            Require(name != "LIVRAISON.json" && listed.Add(name) && records.TryGetValue(name, out var record) &&
                DocumentFiles.Text(record, "sha256") == DocumentFiles.Text(entry!, "sha256") &&
                record["bytes"]!.GetValue<long>() == entry!["bytes"]!.GetValue<long>(), "Delivery inventory differs from imported records.");
        }
        Require(listed.SetEquals(records.Keys.Where(name => name != "LIVRAISON.json")), "Delivery inventory is incomplete.");
        Require(DocumentFiles.Hash(ReadChecked("LIVRAISON.json")) == DocumentFiles.Text(Report["source_snapshot"]!, "delivery_manifest_sha256"),
            "Delivery manifest digest mismatch.");
        RawFile = Snapshot + "/sources/raw/embedded/doc/index.html";
        Require(DocumentFiles.Text(Current, "raw_document") == RawFile[Official.Length..].TrimStart('/'), "Current raw source path mismatch.");
        var raw = ReadChecked("sources/raw/embedded/doc/index.html");
        RawHash = DocumentFiles.Hash(raw);
        Require(RawHash == DocumentFiles.Text(Current, "raw_document_sha256") &&
            RawHash == DocumentFiles.Text(Report["audit"]!["primary_source"]!, "sha256"), "Selected raw source digest mismatch.");
        rawHtml = Encoding.UTF8.GetString(raw);
        var html = WebUtility.HtmlDecode(rawHtml);
        var match = VersionRegex().Match(html);
        Require(match.Success, "Raw source does not declare its current API version.");
        ApiVersion = match.Groups[1].Value;
        var discovery = JsonNode.Parse(ReadChecked("sources/api-version.json"))!;
        Require(new[] { DocumentFiles.Text(Current, "api_version"), DocumentFiles.Text(delivery, "api_version"),
            DocumentFiles.Text(Report["source_snapshot"]!, "api_version"), DocumentFiles.Text(discovery["result"]!, "api_version") }
            .All(value => value == ApiVersion), "Current, report, delivery, discovery and HTML versions disagree.");
        var build = BuildRegex().Match(html);
        Require(build.Success && build.Groups[1].Value == DocumentFiles.Text(delivery, "source_build") &&
            build.Groups[1].Value == DocumentFiles.Text(Report["source_snapshot"]!, "source_build"), "HTML/delivery/report source builds disagree.");
        Require(DocumentFiles.Text(Current, "archive_sha256") == DocumentFiles.Text(Report["archive"]!, "sha256"), "Archive digest metadata mismatch.");
        foreach (var name in new[] { "modules", "endpoints", "operations", "objects", "properties", "events" })
            Catalogues[name] = JsonNode.Parse(ReadChecked($"catalogue/{name}.json")) as JsonArray ??
                throw new InvalidDataException("Catalogue must be a JSON array.");
        var modules = Catalogues["modules"].Select(module => DocumentFiles.Text(module!, "module")).ToArray();
        Require(modules.Distinct(StringComparer.Ordinal).Count() == modules.Length, "Duplicate module identity.");
        foreach (var kind in new[] { "endpoints", "objects", "properties", "events" })
            Require(Catalogues[kind].All(item => modules.Contains(Owner(item!), StringComparer.Ordinal)),
                "Catalogue record has an unknown module.");
        var coverageName = Official + "/" + DocumentFiles.Text(Current, "review_coverage");
        ReviewCoverage = DocumentFiles.Json(Root, coverageName);
        Require(DocumentFiles.Text(ReviewCoverage, "api_version") == ApiVersion &&
            DocumentFiles.Text(ReviewCoverage["source"]!, "import_report_sha256") == ReportHash &&
            DocumentFiles.Text(ReviewCoverage["source"]!, "raw_document_sha256") == RawHash,
            "Review coverage is not bound to this import report and source.");
        foreach (var (kind, items) in Catalogues)
            Require(ReviewCoverage["counts"]?[kind]?.GetValue<int>() == items.Count, "Coverage catalogue count mismatch.");
        var assigned = new HashSet<string>(StringComparer.Ordinal);
        foreach (var group in DocumentFiles.Array(ReviewCoverage, "groups"))
        {
            var name = DocumentFiles.Text(group!, "group");
            Require(DocumentationCommands.Groups.ContainsKey(name), "Unknown review group.");
            var path = DocumentFiles.Text(group!, "review_file");
            var bytes = DocumentFiles.Read(Root, Official + "/" + path);
            var hash = DocumentFiles.Hash(bytes);
            Require(hash == DocumentFiles.Text(group!, "review_sha256"), "Review digest mismatch.");
            Require(Reviews.TryAdd(name, JsonNode.Parse(bytes)!), "Duplicate review group.");
            ReviewHashes[name] = hash;
            foreach (var module in DocumentFiles.Array(group!, "modules"))
            {
                var identity = module!.GetValue<string>();
                Require(assigned.Add(identity) && modules.Contains(identity, StringComparer.Ordinal) &&
                    DocumentationCommands.Groups[name].Contains(identity), "Unknown, duplicated or incorrectly assigned review module.");
            }
            ValidateReviewSource(Reviews[name]);
        }
        Require(assigned.SetEquals(modules), "Review coverage omits modules.");
        var integrity = DocumentFiles.Json(Root, Official + "/reviews/source-integrity.json");
        Require(integrity["integrity_passed"]?.GetValue<bool>() == true &&
            DocumentFiles.Text(integrity, "source_archive_sha256") == DocumentFiles.Text(Current, "archive_sha256") &&
            DocumentFiles.Text(integrity["audit"]!["primary_source"]!, "sha256") == RawHash, "Independent source integrity review mismatch.");
    }

    private void ValidateReviewSource(JsonNode review)
    {
        var matching = DocumentFiles.Array(review, "sources").Where(source => DocumentFiles.Text(source!, "url") == SourceUrl).ToArray();
        Require(matching.Length > 0 && matching.All(source => DocumentFiles.Text(source!, "sha256") == RawHash),
            "Review does not cite the verified raw source digest.");
    }

    public byte[] ReadChecked(string name)
    {
        Require(records.TryGetValue(name, out var record) && DocumentFiles.Text(record, "action") == "copied", "Missing copied source record.");
        var body = DocumentFiles.Read(Root, Snapshot + "/" + name);
        Require(DocumentFiles.Hash(body) == DocumentFiles.Text(record!, "sha256") && body.LongLength == record!["bytes"]!.GetValue<long>(),
            $"Source integrity mismatch: {name}");
        return body;
    }

    public string Owner(JsonNode record)
    {
        if (record["module"] is JsonValue module) return module.GetValue<string>();
        // The supplied events catalogue omits module: its citation identifies the declaring property.
        var source = DocumentFiles.Text(record, "source");
        var owners = Catalogues["properties"].Where(item => DocumentFiles.Text(item!, "source") == source)
            .Select(item => DocumentFiles.Text(item!, "module")).Distinct(StringComparer.Ordinal).ToArray();
        Require(owners.Length == 1, "Unassigned catalogue record lacks an unambiguous declaring property.");
        return owners[0];
    }

    /// <summary>Checks a complete request line within the specifically cited Sphinx section.</summary>
    public bool ContainsRequestExample(string source, string method, string path, string module)
    {
        if (!SourceMatchesModule(source, module)) return false;
        var anchor = Uri.UnescapeDataString(source[(SourceUrl.Length + 1)..]);
        var start = -1;
        foreach (Match tag in DivRegex().Matches(rawHtml))
        {
            if (tag.Value.StartsWith("</", StringComparison.Ordinal)) continue;
            var identity = IdRegex().Match(tag.Value);
            if (identity.Success && WebUtility.HtmlDecode(identity.Groups[1].Value) == anchor &&
                ClassRegex().Match(tag.Value).Groups[1].Value.Split(' ').Contains("section", StringComparer.Ordinal))
            {
                start = tag.Index;
                break;
            }
        }
        if (start < 0) return false;
        var owner = DocumentUnitRegex().Matches(rawHtml[..start]).LastOrDefault();
        if (owner is null || owner.Groups[1].Value != module) return false;
        var depth = 0;
        var end = -1;
        foreach (Match tag in DivRegex().Matches(rawHtml, start))
        {
            depth += tag.Value.StartsWith("</", StringComparison.Ordinal) ? -1 : 1;
            if (depth == 0) { end = tag.Index + tag.Length; break; }
        }
        if (end < 0) return false;
        foreach (Match block in PreRegex().Matches(rawHtml[start..end]))
        {
            var text = WebUtility.HtmlDecode(MarkupRegex().Replace(block.Groups[1].Value, ""));
            if (RequestRegex().Matches(text).Any(line => line.Groups[1].Value == method && line.Groups[2].Value == path))
                return true;
        }
        return false;
    }

    public bool SourceMatchesModule(string source, string module)
    {
        if (!source.StartsWith(SourceUrl + "#", StringComparison.Ordinal)) return false;
        var anchor = Uri.UnescapeDataString(source[(SourceUrl.Length + 1)..]);
        foreach (Match tag in TagRegex().Matches(rawHtml))
        {
            var id = IdRegex().Match(tag.Value);
            if (!id.Success || WebUtility.HtmlDecode(id.Groups[1].Value) != anchor) continue;
            var owner = DocumentUnitRegex().Matches(rawHtml[..tag.Index]).LastOrDefault();
            if (owner?.Groups[1].Value != module) continue;
            var excluded = false;
            foreach (var rule in Report["audit"]?["deprecation_rules"] as JsonArray ?? [])
            {
                if (rule?["verified_removed_nodes"]?.GetValue<int>() is not > 0 || rule["id"] is not JsonValue value ||
                    !value.TryGetValue<string>(out var retired)) continue;
                if (retired == anchor) { excluded = true; break; }
                if (rule["kind"]?.GetValue<string>() != "section") continue;
                var start = DivRegex().Matches(rawHtml).FirstOrDefault(candidate =>
                    WebUtility.HtmlDecode(IdRegex().Match(candidate.Value).Groups[1].Value) == retired)?.Index;
                if (start is null || start > tag.Index) continue;
                var depth = 0;
                foreach (Match boundary in DivRegex().Matches(rawHtml, start.Value))
                {
                    depth += boundary.Value.StartsWith("</", StringComparison.Ordinal) ? -1 : 1;
                    if (depth != 0) continue;
                    excluded = tag.Index < boundary.Index + boundary.Length;
                    break;
                }
                if (excluded) break;
            }
            if (!excluded) return true;
        }
        return false;
    }

    public JsonObject Verification() => new()
    {
        ["generator"] = OwnedOutput.Generator,
        ["api_version"] = ApiVersion,
        ["verified_copied_files"] = records.Values.Count(record => DocumentFiles.Text(record, "action") == "copied"),
        ["raw_document_sha256"] = RawHash,
        ["import_report_sha256"] = ReportHash,
        ["review_groups_verified"] = Reviews.Count,
        ["origin_independently_verified"] = false,
        ["full_sdk_implemented"] = false,
        ["declared_reference_closure"] = Report["audit"]!["closure"]!.DeepClone(),
        ["declared_markdown_defects"] = Report["audit"]!["markdown_links"]!["defects"]!.DeepClone(),
        ["declared_measurement_discrepancies"] = Report["audit"]!["supplied_measurement_discrepancies"]!.DeepClone(),
        ["scope"] = "Byte/hash and recorded review-chain verification; static crawl results are recorded evidence, not a new crawl or contract approval."
    };

    internal static void Require(bool condition, string message)
    {
        if (!condition) throw new InvalidDataException(message);
    }

    [GeneratedRegex("Current API version is\\s*[\"“']?(\\d+\\.\\d+)", RegexOptions.CultureInvariant)]
    private static partial Regex VersionRegex();

    [GeneratedRegex("api\\s+b['\"]([0-9a-f]+)['\"]", RegexOptions.CultureInvariant)]
    private static partial Regex BuildRegex();

    [GeneratedRegex("</?div\\b[^>]*>", RegexOptions.CultureInvariant)]
    private static partial Regex DivRegex();
    [GeneratedRegex("\\sid=[\"']([^\"']+)[\"']", RegexOptions.CultureInvariant)]
    private static partial Regex IdRegex();
    [GeneratedRegex("\\sclass=[\"']([^\"']+)[\"']", RegexOptions.CultureInvariant)]
    private static partial Regex ClassRegex();
    [GeneratedRegex("\\sid=[\"']document-api/([^\"']+)[\"']", RegexOptions.CultureInvariant)]
    private static partial Regex DocumentUnitRegex();
    [GeneratedRegex("<[A-Za-z][^>]*>", RegexOptions.CultureInvariant)]
    private static partial Regex TagRegex();
    [GeneratedRegex("<pre\\b[^>]*>(.*?)</pre>", RegexOptions.Singleline | RegexOptions.CultureInvariant)]
    private static partial Regex PreRegex();
    [GeneratedRegex("<[^>]*>", RegexOptions.CultureInvariant)]
    private static partial Regex MarkupRegex();
    [GeneratedRegex("^\\s*(GET|POST|PUT|DELETE|PATCH|HEAD|OPTIONS)\\s+(\\S+)\\s+HTTP/1\\.[01]\\s*$", RegexOptions.Multiline | RegexOptions.CultureInvariant)]
    private static partial Regex RequestRegex();
}
