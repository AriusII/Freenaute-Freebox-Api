using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxConnectionApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxConnectionApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }

    public FreeboxDdnsProvider Ddns(string provider) => new(_transport, provider);

    /// <summary>Get the current Connection status.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-</remarks>
    public Task<ConnectionStatus> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "connection/", Json.ConnectionStatus, cancellationToken);
    }

    /// <summary>Get the current Connection configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-config-</remarks>
    public Task<ConnectionConfiguration> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "connection/config/", Json.ConnectionConfiguration, cancellationToken);
    }

    /// <summary>Update the Connection configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-config-</remarks>
    public Task<ConnectionConfiguration> UpdateConfigurationAsync(ConnectionConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "connection/config/", request, Json.ConnectionConfigurationPatch, Json.ConnectionConfiguration, cancellationToken);
    }

    /// <summary>Get the current IPv6 Connection configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-ipv6-config-</remarks>
    public Task<ConnectionIpv6Configuration> GetIpv6ConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "connection/ipv6/config/", Json.ConnectionIpv6Configuration, cancellationToken);
    }

    /// <summary>Update the IPv6 Connection configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-ipv6-config-</remarks>
    public Task<ConnectionIpv6Configuration> UpdateIpv6ConfigurationAsync(ConnectionIpv6ConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "connection/ipv6/config/", request, Json.ConnectionIpv6ConfigurationPatch, Json.ConnectionIpv6Configuration, cancellationToken);
    }

    /// <summary>Get the current xDSL infos.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-xdsl-</remarks>
    public Task<XdslInfos> GetXdslAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "connection/xdsl/", Json.XdslInfos, cancellationToken);
    }

    /// <summary>Get the current xDSL/LTE aggregation infos.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-aggregation</remarks>
    public Task<LteAggregationResult> GetAggregationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "connection/aggregation", Json.LteAggregationResult, cancellationToken);
    }

    /// <summary>Update the xDSL/LTE aggregation configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-aggregation</remarks>
    public Task UpdateAggregationAsync(LteAggregationUpdate request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "connection/aggregation", request, Json.LteAggregationUpdate, cancellationToken);
    }

    /// <summary>Get the current FTTH status.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-ftth-</remarks>
    public Task<FtthStatus> GetFtthAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "connection/ftth/", Json.FtthStatus, cancellationToken);
    }

}
