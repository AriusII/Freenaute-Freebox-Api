using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Form fields documented by POST downloads/add; exactly one URL source must be supplied.</summary>
public sealed record DownloadUrlRequest
{
    public string? DownloadUrl { get; init; }
    public string[]? DownloadUrls { get; init; }
    public EncodedFreeboxPath? DownloadDirectory { get; init; }
    public string? FileName { get; init; }
    public string? Hash { get; init; }
    public bool? Recursive { get; init; }
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string? ArchivePassword { get; init; }
    public string? Cookies { get; init; }
}
