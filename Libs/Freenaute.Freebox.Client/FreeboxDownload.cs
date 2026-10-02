namespace Freenaute.Freebox.Client;

/// <summary>Owns a streamed HTTP response. Dispose it after consuming <see cref="Content"/>.</summary>
public sealed class FreeboxDownload : IDisposable, IAsyncDisposable
{
    private HttpResponseMessage? _response;
    private readonly CancellationTokenSource _lifetime;
    private readonly CancellationTokenRegistration _cancellation;

    public Stream Content { get; }
    public string? ContentType { get; }
    public long? ContentLength { get; }

    internal FreeboxDownload(HttpResponseMessage response, Stream stream, CancellationTokenSource lifetime)
    {
        _response = response;
        _lifetime = lifetime;
        ContentType = response.Content.Headers.ContentType?.MediaType;
        ContentLength = response.Content.Headers.ContentLength;
        Content = new LifetimeReadStream(stream, lifetime.Token);
        _cancellation = lifetime.Token.Register(static state => ((HttpResponseMessage)state!).Dispose(), response);
    }

    public void Dispose()
    {
        var response = Interlocked.Exchange(ref _response, null);
        if (response is null) return;
        _cancellation.Dispose();
        Content.Dispose();
        response.Dispose();
        _lifetime.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }

    private sealed class LifetimeReadStream(Stream stream, CancellationToken lifetime) : Stream
    {
        public override bool CanRead => stream.CanRead;
        public override bool CanSeek => stream.CanSeek;
        public override bool CanWrite => false;
        public override long Length => stream.Length;
        public override long Position { get => stream.Position; set => stream.Position = value; }
        public override void Flush() { }
        public override long Seek(long offset, SeekOrigin origin) => stream.Seek(offset, origin);
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        public override int Read(byte[] buffer, int offset, int count)
        {
            lifetime.ThrowIfCancellationRequested();
            try { return stream.Read(buffer, offset, count); }
            catch (Exception error) when (lifetime.IsCancellationRequested && error is IOException or ObjectDisposedException)
            { throw new OperationCanceledException("The download was canceled.", error, lifetime); }
        }

        public override int Read(Span<byte> buffer)
        {
            lifetime.ThrowIfCancellationRequested();
            try { return stream.Read(buffer); }
            catch (Exception error) when (lifetime.IsCancellationRequested && error is IOException or ObjectDisposedException)
            { throw new OperationCanceledException("The download was canceled.", error, lifetime); }
        }

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) =>
            ReadAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();

        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            lifetime.ThrowIfCancellationRequested();
            using var linked = cancellationToken.CanBeCanceled
                ? CancellationTokenSource.CreateLinkedTokenSource(lifetime, cancellationToken)
                : null;
            var effectiveCancellation = linked?.Token ?? lifetime;
            try { return await stream.ReadAsync(buffer, effectiveCancellation).ConfigureAwait(false); }
            catch (Exception error) when (effectiveCancellation.IsCancellationRequested && error is IOException or ObjectDisposedException)
            { throw new OperationCanceledException("The download was canceled.", error, effectiveCancellation); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) stream.Dispose();
            base.Dispose(disposing);
        }
    }
}
