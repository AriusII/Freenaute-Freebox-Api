using Freenaute.Freebox.Mapper.Contracts.Files;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Files;

public sealed class FreeboxStatisticsApi(IFreeboxTransport transport)
{
    /// <summary>Fetches numeric RRD samples through the documented POST body. Scaling is left intact.</summary>
    public Task<RrdResult> FetchAsync(FetchRrdRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (request.Db == RrdDatabase.Temp && request.Fields.HasValue &&
            request.Fields.Value.Any(field => field is "temp1" or "temp2" or "temp3"))
            throw new ArgumentException("Use current temperature fields cpum, cpub and sw; temp1/temp2/temp3 are deprecated.", nameof(request));
        return transport.SendAsync(HttpMethod.Post, "rrd/", FilesRequestValidator.Validate(request), FilesJsonSerializerContext.Default.FetchRrdRequest,
            FilesJsonSerializerContext.Default.RrdResult, cancellationToken);
    }
}
