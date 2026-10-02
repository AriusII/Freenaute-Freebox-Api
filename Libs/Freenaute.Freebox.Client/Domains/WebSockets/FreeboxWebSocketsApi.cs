using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Client.Domains.WebSockets;

public interface IFreeboxWebSocketsApi
{
    FreeboxEventsApi Events { get; }
    FreeboxUploadsApi Uploads { get; }
}

/// <summary>Event subscriptions and modern binary file uploads over authenticated WebSockets.</summary>
public sealed class FreeboxWebSocketsApi : IFreeboxWebSocketsApi
{
    public FreeboxEventsApi Events { get; }
    public FreeboxUploadsApi Uploads { get; }
    public FreeboxWebSocketsApi(IFreeboxWebSocketTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        Events = new(transport);
        Uploads = new(transport);
    }
}

public sealed class FreeboxUploadsApi
{
    private readonly IFreeboxWebSocketTransport _transport;
    public FreeboxUploadsApi(IFreeboxWebSocketTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        _transport = transport;
    }
    public FreeboxUploadDirectory To(EncodedFreeboxPath directory) => new(_transport, directory);
}

public sealed class FreeboxUploadDirectory
{
    private readonly IFreeboxWebSocketTransport _transport;
    private readonly EncodedFreeboxPath _directory;
    public FreeboxUploadDirectory(IFreeboxWebSocketTransport transport, EncodedFreeboxPath directory)
    {
        ArgumentNullException.ThrowIfNull(transport);
        if (!directory.IsInitialized) throw new ArgumentException("A destination requires an encoded Freebox path.", nameof(directory));
        _transport = transport;
        _directory = directory;
    }
    public FreeboxUploadCommand File(string filename) => new(_transport, _directory, filename);
}
