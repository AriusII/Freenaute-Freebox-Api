using Freenaute.Freebox.Mapper.Contracts.Protocol;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.Protocol;

public interface IFreeboxCamerasApi
{
    Task<Camera[]> ListAsync(CancellationToken cancellationToken = default);
    FreeboxCamera Camera(string id);
}

public sealed class FreeboxCamerasApi(IFreeboxTransport transport) : IFreeboxCamerasApi
{
    public Task<Camera[]> ListAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "camera/", ProtocolJsonSerializerContext.Default.CameraArray, cancellationToken);

    public FreeboxCamera Camera(string id) => new(transport, id);
}

public sealed class FreeboxCamera
{
    private readonly IFreeboxTransport _transport;
    private readonly string _path;

    internal FreeboxCamera(IFreeboxTransport transport, string id)
    {
        _transport = transport;
        _path = $"camera/{FreeboxApiPath.EncodeSegment(id)}";
    }

    public Task<Camera> GetAsync(CancellationToken cancellationToken = default) =>
        _transport.SendAsync(HttpMethod.Get, _path, ProtocolJsonSerializerContext.Default.Camera, cancellationToken);
}
