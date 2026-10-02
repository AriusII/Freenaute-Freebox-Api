using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Client.Domains.Files;

internal static class FilesForm
{
    public static HttpContent Create(DownloadUrlRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if ((request.DownloadUrl is null) == (request.DownloadUrls is null))
            throw new ArgumentException("Supply one URL or a list of URLs, exclusively.", nameof(request));
        List<KeyValuePair<string, string>> fields = [];
        if (request.DownloadUrl is not null)
        {
            ValidateUrl(request.DownloadUrl);
            fields.Add(new("download_url", request.DownloadUrl));
        }
        else
        {
            if (request.DownloadUrls!.Length == 0) throw new ArgumentException("The URL list must not be empty.", nameof(request));
            foreach (var url in request.DownloadUrls) ValidateUrl(url);
            fields.Add(new("download_url_list", string.Join('\n', request.DownloadUrls)));
        }
        if ((request.FileName is not null || request.Hash is not null) &&
            (request.DownloadUrls is not null || request.Recursive == true))
            throw new ArgumentException("File name and hash overrides require one non-recursive download URL.", nameof(request));
        if (request.DownloadDirectory is not null) fields.Add(new("download_dir", request.DownloadDirectory.Value.Value));
        Add(fields, "filename", request.FileName);
        Add(fields, "hash", request.Hash);
        if (request.Recursive is not null) fields.Add(new("recursive", request.Recursive.Value ? "true" : "false"));
        Add(fields, "username", request.Username); Add(fields, "password", request.Password);
        Add(fields, "archive_password", request.ArchivePassword); Add(fields, "cookies", request.Cookies);
        return new FormUrlEncodedContent(fields);
    }

    public static HttpContent CreateMultipart(Stream file, string fileName, EncodedFreeboxPath? directory, string? password, bool leaveOpen)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);
        if (!file.CanRead) throw new ArgumentException("A readable download descriptor stream is required.", nameof(file));
        var multipart = new MultipartFormDataContent();
        var fileContent = new StreamContent(leaveOpen ? new NonOwningReadStream(file) : file);
        try
        {
            multipart.Add(fileContent, "download_file", fileName);
            if (directory is not null) multipart.Add(new StringContent(directory.Value.Value), "download_dir");
            if (password is not null) multipart.Add(new StringContent(password), "archive_password");
            return multipart;
        }
        catch { fileContent.Dispose(); multipart.Dispose(); throw; }
    }

    private static void Add(List<KeyValuePair<string, string>> fields, string name, string? value)
    {
        if (value is not null) fields.Add(new(name, value));
    }
    private static void ValidateUrl(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value.Contains('\n') || value.Contains('\r')) throw new ArgumentException("Each download URL must occupy one line.", nameof(value));
    }

    private sealed class NonOwningReadStream(Stream inner) : Stream
    {
        public override bool CanRead => inner.CanRead;
        public override bool CanSeek => inner.CanSeek;
        public override bool CanWrite => false;
        public override long Length => inner.Length;
        public override long Position { get => inner.Position; set => inner.Position = value; }
        public override int Read(byte[] buffer, int offset, int count) => inner.Read(buffer, offset, count);
        public override int Read(Span<byte> buffer) => inner.Read(buffer);
        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => inner.ReadAsync(buffer, offset, count, cancellationToken);
        public override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default) => inner.ReadAsync(buffer, cancellationToken);
        public override long Seek(long offset, SeekOrigin origin) => inner.Seek(offset, origin);
        public override void Flush() => throw new NotSupportedException();
        public override void SetLength(long value) => throw new NotSupportedException();
        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }
}
