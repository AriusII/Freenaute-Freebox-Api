using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxFileSystemApi(IFreeboxTransport transport, IFreeboxBinaryTransport binaryTransport)
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-tasks-</summary>
    public Task<FsTask[]> GetTasksAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "fs/tasks/", FilesJsonSerializerContext.Default.FsTaskArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-tasks-id</summary>
    public Task<FsTask> GetTaskAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"fs/tasks/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v15-fs-tasks-id</summary>
    public Task DeleteTaskAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"fs/tasks/{FilesPath.Id(id)}", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v15-fs-tasks-id</summary>
    public Task<FsTask> SetTaskStateAsync(long id, SetFileTaskStateRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"fs/tasks/{FilesPath.Id(id)}", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.SetFileTaskStateRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-info</summary>
    public Task<FileEntry[]> GetInformationAsync(EncodedFreeboxPath[] request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/info", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.EncodedFreeboxPathArray, FilesJsonSerializerContext.Default.FileEntryArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-mv-</summary>
    public Task<FsTask> MoveAsync(FileTransferRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/mv/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.FileTransferRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-cp-</summary>
    public Task<FsTask> CopyAsync(FileTransferRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/cp/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.FileTransferRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-rm-</summary>
    public Task<FsTask> RemoveAsync(RemoveFilesRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/rm/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.RemoveFilesRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-cat-</summary>
    public Task<FsTask> ConcatenateAsync(ConcatenateFilesRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/cat/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.ConcatenateFilesRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-archive-</summary>
    public Task<FsTask> ArchiveAsync(ArchiveFilesRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/archive/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.ArchiveFilesRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-extract-</summary>
    public Task<FsTask> ExtractAsync(ExtractFilesRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/extract/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.ExtractFilesRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-repair-</summary>
    public Task<FsTask> RepairAsync(RepairFilesRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/repair/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.RepairFilesRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-hash-</summary>
    public Task<FsTask> HashAsync(HashFileRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/hash/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.HashFileRequest, FilesJsonSerializerContext.Default.FsTask, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-tasks-id-hash</summary>
    public Task<FileHash> GetHashAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"fs/tasks/{FilesPath.Id(id)}/hash", FilesJsonSerializerContext.Default.FileHash, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-mkdir-</summary>
    public Task CreateDirectoryAsync(CreateDirectoryRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/mkdir/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.CreateDirectoryRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-rename-</summary>
    public Task<string> RenameAsync(RenameFileRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "fs/rename/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.RenameFileRequest, FilesJsonSerializerContext.Default.String, cancellationToken);

    /// <summary>Returns an owned binary response. Dispose it after consuming Content; no range/retry is inferred.</summary>
    public Task<FreeboxDownload> DownloadAsync(EncodedFreeboxPath path, CancellationToken cancellationToken = default) =>
        binaryTransport.DownloadAsync(HttpMethod.Get, $"dl/{path.ToEscapedSegment()}", cancellationToken);

    public FreeboxFileResource At(EncodedFreeboxPath path) => new(this, path);

    public Task<FileListing> ListAsync(EncodedFreeboxPath path, FileListingOptions? options = null,
        CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, FilesPath.Listing(path, options), FilesJsonSerializerContext.Default.FileListing, cancellationToken);

    public Task<FileEntry> GetInformationAsync(EncodedFreeboxPath path, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"fs/info/{path.ToEscapedSegment()}", FilesJsonSerializerContext.Default.FileEntry, cancellationToken);
}
