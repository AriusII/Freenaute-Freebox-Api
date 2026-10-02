using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxDownloadFeedsApi(IFreeboxTransport transport)
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-feeds-</summary>
    public Task<DownloadFeed[]> GetAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "downloads/feeds/", FilesJsonSerializerContext.Default.DownloadFeedArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-feeds-id</summary>
    public Task<DownloadFeed> GetAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/feeds/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.DownloadFeed, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-</summary>
    public Task<DownloadFeedMutation> CreateAsync(CreateDownloadFeedRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "downloads/feeds/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.CreateDownloadFeedRequest, FilesJsonSerializerContext.Default.DownloadFeedMutation, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-feeds-id</summary>
    public Task DeleteAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"downloads/feeds/{FilesPath.Id(id)}", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-feeds-id</summary>
    public Task<DownloadFeedMutation> UpdateAsync(long id, UpdateDownloadFeedRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"downloads/feeds/{FilesPath.Id(id)}", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.UpdateDownloadFeedRequest, FilesJsonSerializerContext.Default.DownloadFeedMutation, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-id-fetch</summary>
    public Task FetchAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"downloads/feeds/{FilesPath.Id(id)}/fetch", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-fetch</summary>
    public Task FetchAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "downloads/feeds/fetch", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-feeds-feed_id-items-</summary>
    public Task<DownloadFeedItem[]> GetItemsAsync(long feedId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"downloads/feeds/{FilesPath.Id(feedId)}/items/", FilesJsonSerializerContext.Default.DownloadFeedItemArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-feeds-feed_id-items-item_id</summary>
    public Task MarkItemAsync(long feedId, long itemId, MarkDownloadFeedItemRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"downloads/feeds/{FilesPath.Id(feedId)}/items/{FilesPath.Id(itemId)}", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.MarkDownloadFeedItemRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-feed_id-items-item_id-download</summary>
    public Task DownloadItemAsync(long feedId, long itemId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"downloads/feeds/{FilesPath.Id(feedId)}/items/{FilesPath.Id(itemId)}/download", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-feed_id-items-mark_all_as_read</summary>
    public Task MarkAllAsReadAsync(long feedId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"downloads/feeds/{FilesPath.Id(feedId)}/items/mark_all_as_read", cancellationToken);

}
