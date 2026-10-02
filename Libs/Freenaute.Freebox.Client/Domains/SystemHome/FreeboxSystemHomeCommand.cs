using System.Text.Json.Serialization.Metadata;
using Freenaute.Freebox.Mapper.Contracts.SystemHome;

namespace Freenaute.Freebox.Client.Domains.SystemHome;

/// <summary>An immutable success-only command; construction performs no network I/O.</summary>
public sealed class FreeboxSystemHomeCommand<TRequest> where TRequest : ISystemHomeRequest
{
    private readonly IFreeboxTransport transport;
    private readonly HttpMethod method;
    private readonly string path;
    private readonly JsonTypeInfo<TRequest> requestType;
    public TRequest Fields { get; }

    internal FreeboxSystemHomeCommand(IFreeboxTransport transport, HttpMethod method, string path,
        TRequest fields, JsonTypeInfo<TRequest> requestType)
    { this.transport = transport; this.method = method; this.path = path; Fields = fields; this.requestType = requestType; }

    public FreeboxSystemHomeCommand<TRequest> UseFields(TRequest fields)
    {
        ArgumentNullException.ThrowIfNull(fields);
        return new(transport, method, path, fields, requestType);
    }

    public Task SendAsync(CancellationToken cancellationToken = default)
    {
        Fields.Validate();
        return transport.SendAsync(method, path, Fields, requestType, cancellationToken);
    }
}

/// <summary>An immutable command with a source-generated result contract.</summary>
public sealed class FreeboxSystemHomeCommand<TRequest, TResponse> where TRequest : ISystemHomeRequest
{
    private readonly IFreeboxTransport transport;
    private readonly HttpMethod method;
    private readonly string path;
    private readonly JsonTypeInfo<TRequest> requestType;
    private readonly JsonTypeInfo<TResponse> responseType;
    public TRequest Fields { get; }

    internal FreeboxSystemHomeCommand(IFreeboxTransport transport, HttpMethod method, string path,
        TRequest fields, JsonTypeInfo<TRequest> requestType, JsonTypeInfo<TResponse> responseType)
    { this.transport = transport; this.method = method; this.path = path; Fields = fields; this.requestType = requestType; this.responseType = responseType; }

    public FreeboxSystemHomeCommand<TRequest, TResponse> UseFields(TRequest fields)
    {
        ArgumentNullException.ThrowIfNull(fields);
        return new(transport, method, path, fields, requestType, responseType);
    }

    public Task<TResponse> SendAsync(CancellationToken cancellationToken = default)
    {
        Fields.Validate();
        return transport.SendAsync(method, path, Fields, requestType, responseType, cancellationToken);
    }
}
