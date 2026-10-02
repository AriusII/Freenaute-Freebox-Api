using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxWifiMacFilter
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;
    private readonly string _filter_id;

    public FreeboxWifiMacFilter(IFreeboxTransport transport, string filter_id)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        _filter_id = FreeboxApiPath.EncodeSegment(filter_id);
    }


    /// <summary>Getting a particular MAC filter.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-mac_filter-filter_id</remarks>
    public Task<WifiMacFilter> GetAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/mac_filter/{_filter_id}", Json.WifiMacFilter, cancellationToken);
    }

    /// <summary>Updating a MAC filter.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-mac_filter-filter_id</remarks>
    public Task<WifiMacFilter> UpdateAsync(WifiMacFilterPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, $"wifi/mac_filter/{_filter_id}", request, Json.WifiMacFilterPatch, Json.WifiMacFilter, cancellationToken);
    }

    /// <summary>Delete a MAC filter.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#delete--api-v9-wifi-mac_filter-filter_id</remarks>
    public Task DeleteAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Delete, $"wifi/mac_filter/{_filter_id}", cancellationToken);
    }

}
