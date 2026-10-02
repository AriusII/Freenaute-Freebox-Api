using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Contracts.WebSockets;

namespace Freenaute.Freebox.Client.Domains.WebSockets;

/// <summary>Immutable upload selection. Resume appends the stream as supplied; no seek or size inference is performed.</summary>
public sealed class FreeboxUploadCommand
{
    private readonly IFreeboxWebSocketTransport _transport;
    private readonly EncodedFreeboxPath _directory;
    private readonly string _filename;
    private readonly string? _force;
    private readonly long? _size;
    private readonly int _chunkSize;

    public FreeboxUploadCommand(IFreeboxWebSocketTransport transport, EncodedFreeboxPath directory, string filename)
        : this(transport, directory, filename, null, null, 512 * 1024) { }

    private FreeboxUploadCommand(IFreeboxWebSocketTransport transport, EncodedFreeboxPath directory,
        string filename, string? force, long? size, int chunkSize)
    {
        ArgumentNullException.ThrowIfNull(transport);
        ArgumentException.ThrowIfNullOrEmpty(filename);
        if (!directory.IsInitialized) throw new ArgumentException("A destination requires an encoded Freebox path.", nameof(directory));
        if (filename is "." or ".." || filename.IndexOfAny(['/', '\\', '\0']) >= 0)
            throw new ArgumentException("Filename must be a single filename, separate from the destination directory.", nameof(filename));
        _transport = transport;
        _directory = directory;
        _filename = filename;
        _force = force;
        _size = size;
        _chunkSize = chunkSize;
    }

    public FreeboxUploadCommand Overwrite() => new(_transport, _directory, _filename, "overwrite", _size, _chunkSize);
    public FreeboxUploadCommand Resume() => new(_transport, _directory, _filename, "resume", _size, _chunkSize);

    /// <summary>Supplies the documented optional file size explicitly; zero is included. No Stream.Length is inspected.</summary>
    public FreeboxUploadCommand WithSize(long size)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(size);
        return new(_transport, _directory, _filename, _force, size, _chunkSize);
    }

    /// <summary>Controls local binary message size, bounded by the conservative one-million-byte WebSocket limit.</summary>
    public FreeboxUploadCommand WithChunkSize(int bytes)
    {
        if (bytes is < 1 or > 1_000_000) throw new ArgumentOutOfRangeException(nameof(bytes));
        return new(_transport, _directory, _filename, _force, _size, bytes);
    }

    public async Task<FreeboxUploadSession> OpenAsync(CancellationToken cancellationToken = default)
    {
        var connection = await _transport.ConnectAsync("ws/upload", cancellationToken).ConfigureAwait(false);
        return await FreeboxUploadSession.StartAsync(connection, new FileUploadStartRequest
        {
            RequestId = FreeboxUploadSession.StartRequestId,
            Directory = _directory,
            Filename = _filename,
            Force = _force,
            Size = _size
        }, _chunkSize, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Uploads from the stream's current position and awaits correlated completion. The caller owns the stream.</summary>
    public async Task<FreeboxUploadResult> SendAsync(Stream source, CancellationToken cancellationToken = default,
        IProgress<FreeboxUploadProgress>? progress = null)
    {
        ArgumentNullException.ThrowIfNull(source);
        if (!source.CanRead) throw new ArgumentException("The upload stream must be readable.", nameof(source));
        await using var session = await OpenAsync(cancellationToken).ConfigureAwait(false);
        return await session.SendAsync(source, cancellationToken, progress).ConfigureAwait(false);
    }
}

public sealed record FreeboxUploadProgress(long BytesSent, long? TotalFileLength, long? ExpectedFileLength, bool Complete);
public sealed record FreeboxUploadResult(long TotalFileLength, long BytesSent);

public enum FreeboxUploadState
{
    Starting,
    Ready,
    Sending,
    Completed,
    Cancelled,
    Faulted,
    Disposed
}
