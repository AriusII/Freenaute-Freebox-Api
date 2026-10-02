using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxDhcpStaticLease
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;
    private readonly string _id;

    public FreeboxDhcpStaticLease(IFreeboxTransport transport, string id)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        _id = FreeboxApiPath.EncodeSegment(id);
    }


    /// <summary>Get a given DHCP static lease.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v16-dhcp-static_lease-id</remarks>
    public Task<DhcpStaticLease> GetAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"dhcp/static_lease/{_id}", Json.DhcpStaticLease, cancellationToken);
    }

    /// <summary>Update DHCP static lease.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v16-dhcp-static_lease-id</remarks>
    public Task<DhcpStaticLease> UpdateAsync(DhcpStaticLeasePatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, $"dhcp/static_lease/{_id}", request, Json.DhcpStaticLeasePatch, Json.DhcpStaticLease, cancellationToken);
    }

    /// <summary>Delete a DHCP static lease.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-dhcp-static_lease-id</remarks>
    public Task DeleteAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Delete, $"dhcp/static_lease/{_id}", cancellationToken);
    }

}
