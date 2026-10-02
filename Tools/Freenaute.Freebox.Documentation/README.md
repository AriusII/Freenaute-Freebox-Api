# Freebox documentation tooling

A .NET 10 / C# 14 command-line tool for the supplied Freebox Server documentation snapshot.
It uses the .NET base class library, parses JSON without reflection, and performs no network
requests or execution of documentation examples. Python and Node.js are not required.

Run commands from the repository root:

```sh
dotnet run --project Tools/Freenaute.Freebox.Documentation -- verify
dotnet run --project Tools/Freenaute.Freebox.Documentation -- context
dotnet run --project Tools/Freenaute.Freebox.Documentation -- coverage
dotnet test Tests/Freenaute.Freebox.Documentation.Tests
```

`--root <repository>` selects a checkout. `--output <directory>` selects a repository-relative
output directory for `context` or `coverage`. `--implementations <directory>` selects the
implementation manifest directory for `coverage`, defaulting to `docs/implementation`.
Exit code 0 means the command completed; invalid inputs or integrity failures return 2.
Missing review citations are retained in coverage output and do not imply implementation approval.

`verify` reads `docs/freebox-official/current.json`, rehashes every copied original listed in
the import report, reconciles the original delivery inventory, and checks agreement between
the selected version, HTML, sanitized discovery, import metadata and recorded review hashes.
Paths must be contained and portable; symbolic links and reparse points are rejected.
Known unavailable resources, Markdown defects and source measurement discrepancies remain
visible. Recorded crawl results are preserved as evidence; this command does not repeat a crawl.

`context` generates autonomous module tasks, structured projections, raw/reference/review
hashes and reading dependencies under `docs/freebox-official/context-net10`. Paths in JSON
and task text are relative to the repository root. Events without an explicit module are
assigned through their cited declaring property. Model names with multiple owners retain
all reading dependencies.

`coverage` reconciles catalogue citations with their assigned reviews, then matches operation
claims from `docs/implementation/*.json` against the supplied method, literal path and source.
It verifies any declared evidence file hashes and records unclaimed signatures under
`docs/freebox-official/coverage-net10`. These are traceable implementation declarations;
the tool does not inspect C# method bodies or establish live server behavior. Empty evidence
lists are explicitly marked unverified, and complete SDK implementation is never inferred.

An explicit compound signature such as `A & B` supports separate concrete claims. Its literal
signature is reported covered only when every stated alternative is claimed. Operations
documented only by an HTTP example require `documentation_kind: "example_only_documented"`;
the tool checks the exact `METHOD /path HTTP/1.x` line in the cited module's raw section.
Such examples are counted separately and never enlarge the formal signature inventory.

The implementation manifest format is:

```json
{
  "format_version": 1,
  "group": "protocol",
  "modules": ["login"],
  "implemented_operations": [
    {
      "module": "login",
      "method": "GET",
      "documented_path": "/api/v8/login/",
      "relative_path": "login/",
      "source": "http://mafreebox.freebox.fr/doc/index.html#get--api-v8-login-",
      "client_member": "Authentication.GetLoginAsync"
    }
  ],
  "unsupported_operations": [],
  "contract_models": [],
  "evidence_files": []
}
```

Model and unsupported-operation entries contain `module` and `source`; evidence entries
contain a repository-relative `path` and `sha256`. Each source must exist in that module's
catalogue, or cite an active raw section already cited in its assigned review. Exact duplicate
member/operation claims and changed evidence files fail validation. Multiple client bindings
for one HTTP method/path, such as URL and multipart upload variants, remain separate bindings
and count once as an HTTP operation.

After deliberately changing declared evidence files, refresh their existing hash records:

```sh
dotnet run --project Tools/Freenaute.Freebox.Documentation -- refresh-evidence
dotnet run --project Tools/Freenaute.Freebox.Documentation -- coverage
```

This explicit maintenance command changes only `evidence_files[].sha256`, keeps contract fields
and paths, validates every input before writing, uses an exclusive maintenance lock, and refuses
manifests edited during refresh. A lock left by an interrupted process must be reviewed before removal.
It performs no approval of the changed code. Normal coverage validation remains strict.

The `websockets` implementation group explicitly spans `websocket`, `upload` and `vm`.
Review ownership remains assigned to each module's canonical review group.

Generated directories contain `.documentation-output.json` with ownership and SHA-256
records. Repeated generation checks previous bytes before updating tool-owned files.
Existing unowned directories, new file collisions and edited generated files are preserved
by refusing the update. Use a new output directory to retain a manually edited context.
Individual writes use exclusive temporary files and atomic replacement; interrupted
multi-file generation may require a new output directory.

The public API v4 archive remains historical. API 16.0 describes this supplied target
snapshot; source hashes establish the supplied bytes, not independent remote authenticity
or the latest version on every Freebox.

Native AOT publication is supported:

```sh
dotnet publish Tools/Freenaute.Freebox.Documentation -c Release -r linux-x64 -p:PublishAot=true
```
