namespace Freenaute.Freebox.Mapper.Contracts.Files;

/// <summary>Optional query parameters of GET fs/ls; cursor is opaque and never decoded.</summary>
public sealed record FileListingOptions
{
    public bool? OnlyFolders { get; init; }
    public bool? CountSubFolders { get; init; }
    public bool? RemoveHidden { get; init; }
    public string? ExifMode { get; init; }
    public long? Limit { get; init; }
    public string? Cursor { get; init; }
}
