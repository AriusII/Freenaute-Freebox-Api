using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxDhcpApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxDhcpApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }

    public FreeboxDhcpStaticLease StaticLease(string id) => new(_transport, id);

    /// <summary>Get the current DHCP configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v16-dhcp-config-</remarks>
    public Task<DhcpConfig> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "dhcp/config/", Json.DhcpConfig, cancellationToken);
    }

    /// <summary>Update the current DHCP configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v16-dhcp-config-</remarks>
    public Task<DhcpConfig> UpdateConfigurationAsync(DhcpConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "dhcp/config/", request, Json.DhcpConfigurationPatch, Json.DhcpConfig, cancellationToken);
    }

    /// <summary>Get the list of DHCP static leases.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v16-dhcp-static_lease-</remarks>
    public Task<DhcpStaticLease[]> GetStaticLeasesAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "dhcp/static_lease/", Json.DhcpStaticLeaseArray, cancellationToken);
    }

    /// <summary>Add a DHCP static lease.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v16-dhcp-static_lease-</remarks>
    public Task<DhcpStaticLease> CreateStaticLeaseAsync(CreateDhcpStaticLeaseRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, "dhcp/static_lease/", request, Json.CreateDhcpStaticLeaseRequest, Json.DhcpStaticLease, cancellationToken);
    }

    /// <summary>Get the list of DHCP dynamic leases.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v16-dhcp-dynamic_lease-</remarks>
    public Task<DhcpDynamicLease[]> GetDynamicLeasesAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "dhcp/dynamic_lease/", Json.DhcpDynamicLeaseArray, cancellationToken);
    }

}
