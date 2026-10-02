using Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;
using Freenaute.Freebox.Mapper.ServerSide.General;

namespace Freenaute.Freebox.Client;

internal sealed class FreeboxClientState(FreeboxClientOptions options) : IDisposable
{
    internal FreeboxClientOptions Options { get; } = options;
    internal SemaphoreSlim DiscoveryGate { get; } = new(1, 1);
    internal SemaphoreSlim SessionGate { get; } = new(1, 1);
    internal ApiVersionResponse? ApiVersion;
    internal Uri? ApiAddress;
    internal SessionResponse? Session;

    public void Dispose()
    {
        DiscoveryGate.Dispose();
        SessionGate.Dispose();
    }
}
