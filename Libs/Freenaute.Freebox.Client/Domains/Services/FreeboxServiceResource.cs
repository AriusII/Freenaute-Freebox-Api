using System.Text.Json.Serialization.Metadata;

namespace Freenaute.Freebox.Client.Domains.Services;

/// <summary>An immutable selector for a documented resource with read, update, and delete operations.</summary>
public sealed class FreeboxServiceResource<TRead, TWrite> where TWrite : class, new()
{
    private readonly IFreeboxTransport _transport;
    private readonly string _path;
    private readonly JsonTypeInfo<TRead> _readType;
    private readonly JsonTypeInfo<TWrite> _writeType;
    private readonly Action<TWrite> _validate;

    internal FreeboxServiceResource(IFreeboxTransport transport, string path, JsonTypeInfo<TRead> readType,
        JsonTypeInfo<TWrite> writeType, Action<TWrite> validate)
    { _transport = transport; _path = path; _readType = readType; _writeType = writeType; _validate = validate; }

    public Task<TRead> GetAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, _path, _readType, cancellationToken);
    public Task<TRead> UpdateAsync(TWrite request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        _validate(request);
        return _transport.SendAsync(HttpMethod.Put, _path, request, _writeType, _readType, cancellationToken);
    }
    public Task DeleteAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Delete, _path, cancellationToken);
    public FreeboxServiceCommand<TWrite, TRead> Configure() => new(new(), UpdateAsync);
}
