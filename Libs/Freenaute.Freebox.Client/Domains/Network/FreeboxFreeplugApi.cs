using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxFreeplugApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxFreeplugApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }

    public FreeboxFreeplug Plug(string id) => new(_transport, id);

    /// <summary>Get the current Freeplugs networks.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v8-freeplug-</remarks>
    public Task<FreeplugNetwork[]> GetNetworksAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "freeplug/", Json.FreeplugNetworkArray, cancellationToken);
    }

}
