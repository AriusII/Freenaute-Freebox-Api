using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxPvrApi(IFreeboxTransport transport)
{
    public Task<PvrConfig> GetConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "pvr/config/", ServicesJsonSerializerContext.Default.PvrConfig, cancellationToken);
    /// <summary>Updates documented configuration fields and verifies the common success envelope; no undocumented result shape is assumed.</summary>
    public Task UpdateConfigurationAsync(PvrConfigPatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return transport.SendAsync(HttpMethod.Put, "pvr/config/", request, ServicesJsonSerializerContext.Default.PvrConfigPatch, cancellationToken);
    }
    public FreeboxServiceCommand<PvrConfigPatch> Configure() => new(new(), UpdateConfigurationAsync);
    public Task<PvrQuota> GetQuotaAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "pvr/quota/", ServicesJsonSerializerContext.Default.PvrQuota, cancellationToken);
    public Task<PvrQuota> RecalculateQuotaAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, "pvr/quota/", new PvrQuotaRecalculationRequest(),
            ServicesJsonSerializerContext.Default.PvrQuotaRecalculationRequest, ServicesJsonSerializerContext.Default.PvrQuota, cancellationToken);
    public Task<ProgrammedRecording[]> GetProgrammedAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "pvr/programmed/", ServicesJsonSerializerContext.Default.ProgrammedRecordingArray, cancellationToken);
    public FreeboxServiceResource<ProgrammedRecording, ProgrammedRecordingWriteRequest> Programmed(string id) =>
        new(transport, $"pvr/programmed/{ServicesPath.Segment(id)}", ServicesJsonSerializerContext.Default.ProgrammedRecording,
            ServicesJsonSerializerContext.Default.ProgrammedRecordingWriteRequest, request => request.Validate());
    public FreeboxServiceResource<ProgrammedRecording, ProgrammedRecordingWriteRequest> Programmed(long id) => Programmed(ServicesPath.Id(id));
    public Task<ProgrammedRecording> CreateProgrammedAsync(ProgrammedRecordingWriteRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return transport.SendAsync(HttpMethod.Post, "pvr/programmed/", request, ServicesJsonSerializerContext.Default.ProgrammedRecordingWriteRequest,
            ServicesJsonSerializerContext.Default.ProgrammedRecording, cancellationToken);
    }
    public FreeboxServiceCommand<ProgrammedRecordingWriteRequest, ProgrammedRecording> CreateProgrammed() => new(new(), CreateProgrammedAsync);
    public Task<FinishedRecording[]> GetFinishedAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "pvr/finished/", ServicesJsonSerializerContext.Default.FinishedRecordingArray, cancellationToken);
    public FreeboxServiceResource<FinishedRecording, FinishedRecordingPatch> Finished(string id) =>
        new(transport, $"pvr/finished/{ServicesPath.Segment(id)}", ServicesJsonSerializerContext.Default.FinishedRecording,
            ServicesJsonSerializerContext.Default.FinishedRecordingPatch, request => request.Validate());
    public FreeboxServiceResource<FinishedRecording, FinishedRecordingPatch> Finished(long id) => Finished(ServicesPath.Id(id));
    public Task<PvrMedia[]> GetMediaAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "pvr/media/", ServicesJsonSerializerContext.Default.PvrMediaArray, cancellationToken);
}
