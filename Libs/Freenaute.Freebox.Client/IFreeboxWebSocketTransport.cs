namespace Freenaute.Freebox.Client;

/// <summary>Opens authenticated WebSockets using the configured HTTP handler, origin and session.</summary>
public interface IFreeboxWebSocketTransport
{
    Task<FreeboxWebSocketConnection> ConnectAsync(string relativePath, CancellationToken cancellationToken = default);
}
