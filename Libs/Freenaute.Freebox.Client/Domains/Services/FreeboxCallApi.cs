using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxCallApi
{
    private readonly IFreeboxTransport _transport;
    private readonly IFreeboxBinaryTransport? _binary;
    internal FreeboxCallApi(IFreeboxTransport transport, IFreeboxBinaryTransport? binary)
    { _transport = transport; _binary = binary; }

    public Task<CallEntry[]> GetLogAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, "call/log/", ServicesJsonSerializerContext.Default.CallEntryArray, cancellationToken);
    public Task DeleteAllLogEntriesAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Post, "call/log/delete_all/", cancellationToken);
    public Task MarkAllLogEntriesAsReadAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Post, "call/log/mark_all_as_read/", cancellationToken);
    public FreeboxServiceResource<CallEntry, CallEntryPatch> LogEntry(long id) =>
        new(_transport, $"call/log/{ServicesPath.Id(id)}", ServicesJsonSerializerContext.Default.CallEntry,
            ServicesJsonSerializerContext.Default.CallEntryPatch, request => request.Validate());
    public Task<CallAccount> GetAccountAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, "call/account", ServicesJsonSerializerContext.Default.CallAccount, cancellationToken);
    public Task<VoicemailEntry[]> GetVoicemailsAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, "call/voicemail/", ServicesJsonSerializerContext.Default.VoicemailEntryArray, cancellationToken);
    public FreeboxVoicemail Voicemail(string id) => new(_transport, _binary, ServicesPath.Segment(id));
}

public sealed class FreeboxVoicemail
{
    private readonly FreeboxServiceResource<VoicemailEntry, VoicemailPatch> _resource;
    private readonly IFreeboxBinaryTransport? _binary;
    private readonly string _path;
    internal FreeboxVoicemail(IFreeboxTransport transport, IFreeboxBinaryTransport? binary, string id)
    {
        _binary = binary;
        _path = $"call/voicemail/{id}";
        _resource = new(transport, _path, ServicesJsonSerializerContext.Default.VoicemailEntry,
            ServicesJsonSerializerContext.Default.VoicemailPatch, request => request.Validate());
    }
    public Task<VoicemailEntry> GetAsync(CancellationToken cancellationToken = default) => _resource.GetAsync(cancellationToken);
    public Task<VoicemailEntry> UpdateAsync(VoicemailPatch request, CancellationToken cancellationToken = default) => _resource.UpdateAsync(request, cancellationToken);
    public Task DeleteAsync(CancellationToken cancellationToken = default) => _resource.DeleteAsync(cancellationToken);
    public FreeboxServiceCommand<VoicemailPatch, VoicemailEntry> Configure() => _resource.Configure();
    /// <summary>Downloads the documented WAV response; the caller owns and disposes the download.</summary>
    public Task<FreeboxDownload> DownloadAudioAsync(CancellationToken cancellationToken = default) =>
        (_binary ?? throw new NotSupportedException("The supplied transport does not support binary downloads."))
        .DownloadAsync(HttpMethod.Get, _path + "/audio_file", cancellationToken);
}
