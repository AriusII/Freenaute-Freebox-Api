using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxWifiGuestAccessApi
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;

    public FreeboxWifiGuestAccessApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }

    public FreeboxWifiGuestKey Key(long id) => new(_transport, id);

    /// <summary>Get or change the dedicated ap config.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v14-wifi-custom_keys-config-</remarks>
    public Task<WifiCustomKeyConfig> GetConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/custom_keys/config/", Json.WifiCustomKeyConfig, cancellationToken);
    }

    /// <summary>Get or change the dedicated ap config.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v14-wifi-custom_keys-config-</remarks>
    public Task<WifiCustomKeyConfig> UpdateConfigurationAsync(WifiCustomKeyConfigurationPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, "wifi/custom_keys/config/", request, Json.WifiCustomKeyConfigurationPatch, Json.WifiCustomKeyConfig, cancellationToken);
    }

    /// <summary>Get the list of wifi custom key.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-custom_key-</remarks>
    public Task<WifiCustomKey[]> GetKeysAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, "wifi/custom_key/", Json.WifiCustomKeyArray, cancellationToken);
    }

    /// <summary>Create a new wifi custom key.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-custom_key-</remarks>
    public Task<WifiCustomKey> CreateKeyAsync(CreateWifiCustomKeyRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, "wifi/custom_key/", request, Json.CreateWifiCustomKeyRequest, Json.WifiCustomKey, cancellationToken);
    }

}
