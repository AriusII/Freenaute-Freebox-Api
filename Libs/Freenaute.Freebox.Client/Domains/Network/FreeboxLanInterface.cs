using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxLanInterface
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;
    private readonly string _interface;
    private readonly string _interfaceValue;

    public FreeboxLanInterface(IFreeboxTransport transport, string @interface)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        _interface = FreeboxApiPath.EncodeSegment(@interface);
        _interfaceValue = @interface;
    }

    public FreeboxLanHost Host(string id) => new(_transport, _interfaceValue, id);

    /// <summary>Getting the list of hosts on a given interface.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v16-lan-browser-interface-</remarks>
    public Task<LanHost[]> GetHostsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"lan/browser/{_interface}/", Json.LanHostArray, cancellationToken);
    }

    /// <summary>Send Wake ok Lan packet to an host.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v8-lan-wol-interface-</remarks>
    public Task WakeAsync(WakeOnLanRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, $"lan/wol/{_interface}/", request, Json.WakeOnLanRequest, cancellationToken);
    }

}
