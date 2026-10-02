using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxRaidApi(IFreeboxTransport transport)
{
    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-raid-</summary>
    public Task<RaidArray[]> GetAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "storage/raid/", FilesJsonSerializerContext.Default.RaidArrayArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-raid-id</summary>
    public Task<RaidArray> GetAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, $"storage/raid/{FilesPath.Id(id)}", FilesJsonSerializerContext.Default.RaidArray, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-storage-raid-</summary>
    public Task CreateAsync(CreateRaidRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "storage/raid/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.CreateRaidRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-storage-raid-id</summary>
    public Task DeleteAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"storage/raid/{FilesPath.Id(id)}", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-raid-id</summary>
    public Task SetStateAsync(long id, SetRaidStateRequest request, CancellationToken cancellationToken = default)
    {
        FilesRequestValidator.Validate(request);
        if (request.Id != id) throw new ArgumentException("The route and body array identifiers must agree.", nameof(request));
        return transport.SendAsync(HttpMethod.Put, $"storage/raid/{FilesPath.Id(id)}", request,
            FilesJsonSerializerContext.Default.SetRaidStateRequest, cancellationToken);
    }

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-storage-raid-id-forcestart</summary>
    public Task ForceStartAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"storage/raid/{FilesPath.Id(id)}/forcestart", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-storage-raid-id-members-faulty</summary>
    public Task RemoveFaultyMembersAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, $"storage/raid/{FilesPath.Id(id)}/members/faulty", cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-raid-id-members</summary>
    public Task UpdateMembersAsync(long id, UpdateRaidMembersRequest request, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, $"storage/raid/{FilesPath.Id(id)}/members", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.UpdateRaidMembersRequest, cancellationToken);

    /// <summary>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-storage-raid-id-members-addspares</summary>
    public Task AddSpareMembersAsync(long id, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"storage/raid/{FilesPath.Id(id)}/members/addspares", cancellationToken);

}
