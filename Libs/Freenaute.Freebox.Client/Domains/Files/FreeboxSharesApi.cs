using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxSharesApi(IFreeboxTransport transport)
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-share_link-</summary>
    public Task<ShareLink[]> GetAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "share_link/", FilesJsonSerializerContext.Default.ShareLinkArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-share_link-</summary>
    public Task<ShareLink> CreateAsync(CreateShareLinkRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "share_link/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.CreateShareLinkRequest, FilesJsonSerializerContext.Default.ShareLink, cancellationToken);

    public Task<ShareLink> GetAsync(string token, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"share_link/{FilesPath.Opaque(token)}", FilesJsonSerializerContext.Default.ShareLink, cancellationToken);
    public Task DeleteAsync(string token, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"share_link/{FilesPath.Opaque(token)}", cancellationToken);
}
