using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxIgdRedirection
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;
    private readonly string _id;

    public FreeboxIgdRedirection(IFreeboxTransport transport, string id)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        _id = FreeboxApiPath.EncodeSegment(id);
    }


    /// <summary>Delete a redirection.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-upnpigd-redir-id</remarks>
    public Task DeleteAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Delete, $"upnpigd/redir/{_id}", cancellationToken);
    }

}
