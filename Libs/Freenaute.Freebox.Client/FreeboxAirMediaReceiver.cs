using Freenaute.Freebox.Mapper.ClientSide.Api.AirMedia;
using Freenaute.Freebox.Mapper.Common.Types;

namespace Freenaute.Freebox.Client;

/// <summary>An immutable fluent resource selector for one AirMedia receiver.</summary>
public sealed class FreeboxAirMediaReceiver
{
    private readonly IFreeboxAirMediaApi _api;
    private readonly string _receiverName;

    internal FreeboxAirMediaReceiver(IFreeboxAirMediaApi api, string receiverName)
    {
        FreeboxApiPath.EncodeSegment(receiverName);
        _api = api;
        _receiverName = receiverName;
    }

    public FreeboxAirMediaCommand PlayVideo(string media) => CreateCommand(AirMediaMediaType.Video, media);
    public FreeboxAirMediaCommand ShowPhoto(string media) => CreateCommand(AirMediaMediaType.Photo, media);
    public FreeboxAirMediaCommand StopVideo() => CreateStopCommand(AirMediaMediaType.Video);
    public FreeboxAirMediaCommand StopPhoto() => CreateStopCommand(AirMediaMediaType.Photo);

    private FreeboxAirMediaCommand CreateStopCommand(AirMediaMediaType mediaType) =>
        new(_api, _receiverName, new AirMediaReceiverRequest(AirMediaAction.Stop, mediaType));

    private FreeboxAirMediaCommand CreateCommand(AirMediaMediaType mediaType, string media)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(media);
        return new FreeboxAirMediaCommand(_api, _receiverName,
            new AirMediaReceiverRequest(AirMediaAction.Start, mediaType, media));
    }
}

/// <summary>An immutable AirMedia command. HTTP I/O occurs only when <see cref="SendAsync"/> is called.</summary>
public sealed class FreeboxAirMediaCommand
{
    private readonly IFreeboxAirMediaApi _api;
    private readonly string _receiverName;
    private readonly AirMediaReceiverRequest _request;

    internal FreeboxAirMediaCommand(IFreeboxAirMediaApi api, string receiverName, AirMediaReceiverRequest request)
    {
        _api = api;
        _receiverName = receiverName;
        _request = request;
    }

    /// <summary>Sets the wire position: percentage multiplied by 1,000 (50,000 means 50%).</summary>
    public FreeboxAirMediaCommand At(int position)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(position);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(position, 100_000);
        return new FreeboxAirMediaCommand(_api, _receiverName, _request with { Position = position });
    }

    /// <summary>Sets a percentage from zero to 100, with at most three fractional digits.</summary>
    public FreeboxAirMediaCommand AtPercent(decimal percentage)
    {
        if (percentage is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(percentage));
        }

        var position = percentage * 1_000;
        if (decimal.Truncate(position) != position)
        {
            throw new ArgumentException("The percentage must be representable with three fractional digits.", nameof(percentage));
        }

        return At(decimal.ToInt32(position));
    }

    public FreeboxAirMediaCommand WithPassword(string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        return new FreeboxAirMediaCommand(_api, _receiverName, _request with { Password = password });
    }

    public Task SendAsync(CancellationToken cancellationToken = default) =>
        _api.SendToReceiverAsync(_receiverName, _request, cancellationToken);
}
