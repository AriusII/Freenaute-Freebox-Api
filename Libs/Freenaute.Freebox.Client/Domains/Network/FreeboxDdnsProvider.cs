using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxDdnsProvider
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;
    private readonly string _provider;

    public FreeboxDdnsProvider(IFreeboxTransport transport, string provider)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        _provider = FreeboxApiPath.EncodeSegment(provider);
    }


    /// <summary>Get the status of a DynDNS service.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-ddns-provider-status-</remarks>
    public Task<DDNSStatus> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"connection/ddns/{_provider}/status/", Json.DDNSStatus, cancellationToken);
    }

    /// <summary>Get the config of a DynDNS service.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-ddns-provider-</remarks>
    public Task<DDNSConfig> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"connection/ddns/{_provider}/", Json.DDNSConfig, cancellationToken);
    }

    /// <summary>Set the config of a DynDNS service.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-ddns-provider-</remarks>
    public Task<DDNSConfig> UpdateConfigurationAsync(DdnsConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, $"connection/ddns/{_provider}/", request, Json.DdnsConfigurationPatch, Json.DDNSConfig, cancellationToken);
    }

}
