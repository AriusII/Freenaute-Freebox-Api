using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxUploadsApi(IFreeboxTransport transport)
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upload-</summary>
    public Task<FileUpload[]> GetAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "upload/", FilesJsonSerializerContext.Default.FileUploadArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upload-id</summary>
    public Task<FileUpload> GetAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"upload/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.FileUpload, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-upload-id-cancel</summary>
    public Task CancelAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"upload/{FilesPath.Id(id)}/cancel", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-upload-id</summary>
    public Task DeleteAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"upload/{FilesPath.Id(id)}", cancellationToken);

}
