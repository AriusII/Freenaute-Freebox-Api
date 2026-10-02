using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxDownloadsApi(IFreeboxTransport transport)
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-</summary>
    public Task<ObjectOrArray<Download>> GetAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "downloads/", FilesJsonSerializerContext.Default.ObjectOrArrayDownload, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-id</summary>
    public Task<Download> GetAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.Download, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-id</summary>
    public Task DeleteAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"downloads/{FilesPath.Id(id)}", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-id-erase</summary>
    public Task EraseAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"downloads/{FilesPath.Id(id)}/erase", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-id</summary>
    public Task<Download> UpdateAsync(long id, UpdateDownloadRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"downloads/{FilesPath.Id(id)}", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.UpdateDownloadRequest, FilesJsonSerializerContext.Default.Download, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-id-log</summary>
    public Task<string> GetLogAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/{FilesPath.Id(id)}/log", FilesJsonSerializerContext.Default.String, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-stats</summary>
    public Task<DownloadStats> GetStatisticsAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "downloads/stats", FilesJsonSerializerContext.Default.DownloadStats, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-files</summary>
    public Task<DownloadFile[]> GetFilesAsync(long taskId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/{FilesPath.Id(taskId)}/files", FilesJsonSerializerContext.Default.DownloadFileArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-task_id-files-file_id</summary>
    public Task SetFilePriorityAsync(long taskId, string fileId, DownloadFilePriorityRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"downloads/{FilesPath.Id(taskId)}/files/{FilesPath.Opaque(fileId)}", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.DownloadFilePriorityRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-trackers</summary>
    public Task<DownloadTracker[]> GetTrackersAsync(long taskId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/{FilesPath.Id(taskId)}/trackers", FilesJsonSerializerContext.Default.DownloadTrackerArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-task_id-trackers</summary>
    public Task AddTrackerAsync(long taskId, AddDownloadTrackerRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"downloads/{FilesPath.Id(taskId)}/trackers", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.AddDownloadTrackerRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-peers</summary>
    public Task<DownloadPeer[]> GetPeersAsync(long taskId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/{FilesPath.Id(taskId)}/peers", FilesJsonSerializerContext.Default.DownloadPeerArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-pieces</summary>
    public Task<string> GetPiecesAsync(long taskId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/{FilesPath.Id(taskId)}/pieces", FilesJsonSerializerContext.Default.String, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-blacklist</summary>
    public Task<DownloadBlacklistEntry[]> GetBlacklistAsync(long taskId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/{FilesPath.Id(taskId)}/blacklist", FilesJsonSerializerContext.Default.DownloadBlacklistEntryArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-task_id-blacklist-empty</summary>
    public Task ClearBlacklistAsync(long taskId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"downloads/{FilesPath.Id(taskId)}/blacklist/empty", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-blacklist</summary>
    public Task<DownloadBlacklistEntry> AddToBlacklistAsync(AddDownloadBlacklistRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "downloads/blacklist", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.AddDownloadBlacklistRequest, FilesJsonSerializerContext.Default.DownloadBlacklistEntry, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-config-</summary>
    public Task<DownloadConfiguration> GetConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "downloads/config/", FilesJsonSerializerContext.Default.DownloadConfiguration, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-config-</summary>
    public Task<DownloadConfiguration> UpdateConfigurationAsync(UpdateDownloadConfiguration request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, "downloads/config/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.UpdateDownloadConfiguration, FilesJsonSerializerContext.Default.DownloadConfiguration, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-throttling</summary>
    public Task<DownloadThrottlingStatus> SetThrottlingAsync(SetDownloadThrottlingRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, "downloads/throttling", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.SetDownloadThrottlingRequest, FilesJsonSerializerContext.Default.DownloadThrottlingStatus, cancellationToken);

    public FreeboxDownloadFeedsApi Feeds { get; } = new(transport);
    public FreeboxDownloadSubmission FromUrl(string url) => new(this, new DownloadUrlRequest { DownloadUrl = url });
    public FreeboxDownloadSubmission FromUrls(params string[] urls)
    {
        ArgumentNullException.ThrowIfNull(urls);
        return new(this, new DownloadUrlRequest { DownloadUrls = urls.ToArray() });
    }

    public Task DeleteTrackerAsync(long taskId, string announce, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"downloads/{FilesPath.Id(taskId)}/trackers/{FilesPath.Opaque(announce)}", cancellationToken);

    public Task UpdateTrackerAsync(long taskId, string announce, UpdateDownloadTrackerRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"downloads/{FilesPath.Id(taskId)}/trackers/{FilesPath.Opaque(announce)}", FilesRequestValidator.Validate(request),
            FilesJsonSerializerContext.Default.UpdateDownloadTrackerRequest, cancellationToken);

    public Task RemoveFromBlacklistAsync(string host, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"downloads/blacklist/{FilesPath.Opaque(host)}", cancellationToken);

    // The HTTP transport consumes and disposes submitted content; bodies cannot be reused.
    public Task<AddedDownloads> AddUrlAsync(DownloadUrlRequest request, CancellationToken cancellationToken = default) =>
        transport.SendContentAsync(HttpMethod.Post, "downloads/add", FilesForm.Create(request),
            FilesJsonSerializerContext.Default.AddedDownloads, cancellationToken);

    /// <summary>Upload a torrent/NZB download descriptor, not a filesystem file-upload operation.</summary>
    public Task<AddedDownloads> AddFileAsync(Stream file, string fileName, EncodedFreeboxPath? downloadDirectory = null,
        string? archivePassword = null, bool leaveOpen = true, CancellationToken cancellationToken = default) =>
        transport.SendContentAsync(HttpMethod.Post, "downloads/add",
            FilesForm.CreateMultipart(file, fileName, downloadDirectory, archivePassword, leaveOpen),
            FilesJsonSerializerContext.Default.AddedDownloads, cancellationToken);
}
