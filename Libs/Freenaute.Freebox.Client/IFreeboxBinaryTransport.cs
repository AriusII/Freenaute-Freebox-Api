namespace Freenaute.Freebox.Client;

/// <summary>Streams a raw response using the same configured origin and session as the JSON transport.</summary>
public interface IFreeboxBinaryTransport
{
    /// <summary>The caller must dispose the returned download. Cancellation and the configured timeout cover body reads.</summary>
    Task<FreeboxDownload> DownloadAsync(HttpMethod method, string relativePath,
        CancellationToken cancellationToken = default, bool requiresAuthentication = true);
}
