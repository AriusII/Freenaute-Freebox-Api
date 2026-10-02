using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxNatApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxNatApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }

    public FreeboxPortForwarding Forwarding(long id) => new(_transport, id);
    public FreeboxIncomingPort IncomingPort(string id) => new(_transport, id);

    /// <summary>Get the current Dmz configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-fw-dmz-</remarks>
    public Task<DmzConfig> GetDmzAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "fw/dmz/", Json.DmzConfig, cancellationToken);
    }

    /// <summary>Update the current Dmz configuration.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v8-fw-dmz-</remarks>
    public Task<DmzConfig> UpdateDmzAsync(DmzConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "fw/dmz/", request, Json.DmzConfigurationPatch, Json.DmzConfig, cancellationToken);
    }

    /// <summary>Getting the list of port forwarding.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-fw-redir-</remarks>
    public Task<PortForwardingConfig[]> GetForwardingsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "fw/redir/", Json.PortForwardingConfigArray, cancellationToken);
    }

    /// <summary>Add a port forwarding.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-fw-redir-</remarks>
    public Task<PortForwardingConfig> CreateForwardingAsync(CreatePortForwardingRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, "fw/redir/", request, Json.CreatePortForwardingRequest, Json.PortForwardingConfig, cancellationToken);
    }

    /// <summary>Getting the list of incoming ports.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-fw-incoming-</remarks>
    public Task<IncomingPortConfig[]> GetIncomingPortsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "fw/incoming/", Json.IncomingPortConfigArray, cancellationToken);
    }

}
