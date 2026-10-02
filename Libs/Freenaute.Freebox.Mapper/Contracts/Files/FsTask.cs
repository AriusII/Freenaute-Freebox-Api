using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Read contract from http://mafreebox.freebox.fr/doc/index.html#FsTask. Unknown enum strings are preserved.</summary>
public sealed record FsTask
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.id</summary>
    [JsonPropertyName("id")]
    public long? Id { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.type</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.state</summary>
    [JsonPropertyName("state")]
    public string? State { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.error</summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.created_ts</summary>
    [JsonPropertyName("created_ts")]
    public long? CreatedTs { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.started_ts</summary>
    [JsonPropertyName("started_ts")]
    public long? StartedTs { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.done_ts</summary>
    [JsonPropertyName("done_ts")]
    public long? DoneTs { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.duration</summary>
    [JsonPropertyName("duration")]
    public long? Duration { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.progress</summary>
    [JsonPropertyName("progress")]
    public long? Progress { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.eta</summary>
    [JsonPropertyName("eta")]
    public long? Eta { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.from</summary>
    [JsonPropertyName("from")]
    public string? From { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.to</summary>
    [JsonPropertyName("to")]
    public string? To { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.nfiles</summary>
    [JsonPropertyName("nfiles")]
    public long? Nfiles { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.nfiles_done</summary>
    [JsonPropertyName("nfiles_done")]
    public long? NfilesDone { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.total_bytes</summary>
    [JsonPropertyName("total_bytes")]
    public long? TotalBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.total_bytes_done</summary>
    [JsonPropertyName("total_bytes_done")]
    public long? TotalBytesDone { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.curr_bytes</summary>
    [JsonPropertyName("curr_bytes")]
    public long? CurrBytes { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.curr_bytes_done</summary>
    [JsonPropertyName("curr_bytes_done")]
    public long? CurrBytesDone { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.rate</summary>
    [JsonPropertyName("rate")]
    public long? Rate { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.src</summary>
    [JsonPropertyName("src")]
    public string[]? Src { get; init; }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#FsTask.dst</summary>
    [JsonPropertyName("dst")]
    public string? Dst { get; init; }

}
