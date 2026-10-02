using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxDhcpV6Api
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxDhcpV6Api(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }


    /// <summary>Get the current DHCPv6 configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-dhcpv6-config-</remarks>
    public Task<DHCPv6Config> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "dhcpv6/config/", Json.DHCPv6Config, cancellationToken);
    }

    /// <summary>Update the current DHCPv6 configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-dhcpv6-config-</remarks>
    public Task<DHCPv6Config> UpdateConfigurationAsync(DhcpV6ConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "dhcpv6/config/", request, Json.DhcpV6ConfigurationPatch, Json.DHCPv6Config, cancellationToken);
    }

}
