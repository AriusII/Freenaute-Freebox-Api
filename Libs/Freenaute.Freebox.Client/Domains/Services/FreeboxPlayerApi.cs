using Freenaute.Freebox.Mapper.Contracts.Services;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Services;

public sealed class FreeboxPlayerApi(IFreeboxTransport transport)
{
    public Task<Player[]> GetAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "player", ServicesJsonSerializerContext.Default.PlayerArray, cancellationToken);
    public FreeboxPlayer Device(long id) => new(transport, ServicesPath.Id(id));
}

public sealed class FreeboxPlayer
{
    private readonly IFreeboxTransport _transport;
    private readonly string _path;
    internal FreeboxPlayer(IFreeboxTransport transport, string id)
    { _transport = transport; _path = $"player/{id}/api/v6/"; }
    public Task<PlayerStatus> GetStatusAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, _path + "status/", ServicesJsonSerializerContext.Default.PlayerStatus, cancellationToken);
    public Task<PlayerVolume> GetVolumeAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, _path + "control/volume/", ServicesJsonSerializerContext.Default.PlayerVolume, cancellationToken);
    public Task<PlayerVolume> UpdateVolumeAsync(PlayerVolumePatch request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();
        return _transport.SendAsync(HttpMethod.Put, _path + "control/volume/", request,
            ServicesJsonSerializerContext.Default.PlayerVolumePatch, ServicesJsonSerializerContext.Default.PlayerVolume, cancellationToken);
    }
    public FreeboxPlayerVolumeCommand Volume() => new(new(), UpdateVolumeAsync);
    public Task SendMediaCommandAsync(PlayerMediaCommandName command, CancellationToken cancellationToken = default)
    {
        if (command.Value is not ("play_pause" or "stop" or "prev" or "next" or "select_stream" or "select_audio_track" or "select_srt_track"))
            throw new ArgumentException("Only documented player media commands are supported.", nameof(command));
        return _transport.SendAsync(HttpMethod.Post, _path + "control/mediactrl/", new PlayerMediaCommand { Cmd = command },
            ServicesJsonSerializerContext.Default.PlayerMediaCommand, cancellationToken);
    }
    public Task OpenAsync(string url, string? type = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url);
        return _transport.SendAsync(HttpMethod.Post, _path + "control/open", new PlayerOpenRequest { Url = url, Type = type },
            ServicesJsonSerializerContext.Default.PlayerOpenRequest, cancellationToken);
    }
}

public sealed class FreeboxPlayerVolumeCommand
{
    private readonly PlayerVolumePatch _request;
    private readonly Func<PlayerVolumePatch, CancellationToken, Task<PlayerVolume>> _send;
    internal FreeboxPlayerVolumeCommand(PlayerVolumePatch request, Func<PlayerVolumePatch, CancellationToken, Task<PlayerVolume>> send)
    { _request = request; _send = send; }
    public FreeboxPlayerVolumeCommand Level(int volume) => new(_request with { Volume = volume }, _send);
    public FreeboxPlayerVolumeCommand Muted(bool mute = true) => new(_request with { Mute = mute }, _send);
    public Task<PlayerVolume> SendAsync(CancellationToken cancellationToken = default) => _send(_request, cancellationToken);
}
