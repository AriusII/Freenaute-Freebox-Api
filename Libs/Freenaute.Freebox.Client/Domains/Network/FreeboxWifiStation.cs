using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxWifiStation
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;
    private readonly string _id;
    private readonly string _mac;

    public FreeboxWifiStation(IFreeboxTransport transport, long id, string mac)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        _id = FreeboxApiPath.EncodeSegment(NetworkPath.Number(id));
        _mac = FreeboxApiPath.EncodeSegment(mac);
    }


    /// <summary>Get Wi-Fi Station.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-stations-mac</remarks>
    public Task<WifiStation> GetAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/ap/{_id}/stations/{_mac}", Json.WifiStation, cancellationToken);
    }

}
