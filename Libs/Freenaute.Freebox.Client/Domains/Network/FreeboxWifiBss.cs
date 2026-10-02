using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Network;

/// <summary>Typed network operations using the currently discovered outer API major.</summary>
public sealed class FreeboxWifiBss
{
    private readonly IFreeboxTransport _transport;
    private static NetworkJsonSerializerContext Json => NetworkJsonSerializerContext.Default;
    private readonly string _id;

    public FreeboxWifiBss(IFreeboxTransport transport, string id)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
        _id = FreeboxApiPath.EncodeSegment(id);
    }


    /// <summary>Get a particular BSS.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-bss-id</remarks>
    public Task<WifiBss> GetAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/bss/{_id}", Json.WifiBss, cancellationToken);
    }

    /// <summary>Update an BSS.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-bss-id</remarks>
    public Task<WifiBss> UpdateAsync(WifiBssPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Put, $"wifi/bss/{_id}", request, Json.WifiBssPatch, Json.WifiBss, cancellationToken);
    }

    /// <summary>Config reset value of a BSS.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-bss-id-default</remarks>
    public Task<WifiBssConfig> GetDefaultsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/bss/{_id}/default", Json.WifiBssConfig, cancellationToken);
    }

    /// <summary>Per AP/BSS diagnostic.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-diag &amp; -api-v9-wifi-bss-id-diag</remarks>
    public Task<WifiDiagItem[]> GetDiagnosticsAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/bss/{_id}/diag", Json.WifiDiagItemArray, cancellationToken);
    }

    /// <summary>Per AP/BSS diagnostic.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-ap-id-diag &amp; -api-v9-wifi-bss-id-diag</remarks>
    public Task FixDiagnosticsAsync(string[] request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        NetworkWriteValidation.Validate(request);
        return _transport.SendAsync(HttpMethod.Post, $"wifi/bss/{_id}/diag", request, Json.StringArray, cancellationToken);
    }

    /// <summary>Available partner.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v14-wifi-bss-id-mlo-allowed_comb</remarks>
    public Task<long[][]> GetAllowedMloPartnersAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/bss/{_id}/mlo/allowed_comb", Json.Int64ArrayArray, cancellationToken);
    }

    /// <summary>Getting the MLO config.</summary>
    /// <remarks>http://mafreebox.freebox.fr/doc/index.html#get--api-v14-wifi-bss-id-mlo-config</remarks>
    public Task<WifiMLOConfiguration> GetMloConfigurationAsync(CancellationToken cancellationToken = default)
    {
        return _transport.SendAsync(HttpMethod.Get, $"wifi/bss/{_id}/mlo/config", Json.WifiMLOConfiguration, cancellationToken);
    }

}
