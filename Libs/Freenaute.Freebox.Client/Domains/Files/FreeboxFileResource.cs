using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Client.Domains.Files;

/// <summary>An immutable selector retaining the exact encoded path returned by Freebox.</summary>
public sealed class FreeboxFileResource
{
    private readonly FreeboxFileSystemApi api;
    private readonly EncodedFreeboxPath path;

    internal FreeboxFileResource(FreeboxFileSystemApi api, EncodedFreeboxPath path)
    {
        _ = path.Value;
        this.api = api;
        this.path = path;
    }

    public Task<FileEntry> GetInformationAsync(CancellationToken cancellationToken = default) => api.GetInformationAsync(path, cancellationToken);
    public Task<FileListing> ListAsync(FileListingOptions? options = null, CancellationToken cancellationToken = default) => api.ListAsync(path, options, cancellationToken);
    public Task<FreeboxDownload> DownloadAsync(CancellationToken cancellationToken = default) => api.DownloadAsync(path, cancellationToken);
}
