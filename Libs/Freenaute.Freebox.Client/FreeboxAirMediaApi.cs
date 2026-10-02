using Freenaute.Freebox.Mapper.ClientSide.Api.AirMedia;
using Freenaute.Freebox.Mapper.Serialization;
using Freenaute.Freebox.Mapper.ServerSide.Api.AirMedia;

namespace Freenaute.Freebox.Client;

internal sealed class FreeboxAirMediaApi(FreeboxHttpTransport transport) : IFreeboxAirMediaApi
{
    private static readonly FreeboxJsonSerializerContext Json = FreeboxJsonSerializerContext.Default;

    public Task<AirMediaConfigResponse> GetConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "airmedia/config/", Json.AirMediaConfigResponse, cancellationToken);

    public Task<AirMediaConfigResponse> UpdateConfigurationAsync(UpdateAirMediaConfigRequest request,
        CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Put, "airmedia/config/", request, Json.UpdateAirMediaConfigRequest,
            Json.AirMediaConfigResponse, cancellationToken);

    public async Task<IReadOnlyList<AirMediaReceiverResponse>> GetReceiversAsync(CancellationToken cancellationToken = default) =>
        await transport.SendAsync(HttpMethod.Get, "airmedia/receivers/", Json.AirMediaReceiverResponseArray,
            cancellationToken).ConfigureAwait(false);

    public Task SendToReceiverAsync(string receiverName, AirMediaReceiverRequest request,
        CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, $"airmedia/receivers/{FreeboxApiPath.EncodeSegment(receiverName)}/",
            request, Json.AirMediaReceiverRequest, cancellationToken);

    public FreeboxAirMediaReceiver Receiver(string receiverName) => new(this, receiverName);
}
