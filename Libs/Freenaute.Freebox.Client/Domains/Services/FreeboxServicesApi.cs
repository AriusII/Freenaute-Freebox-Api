namespace Freenaute.Freebox.Client.Domains.Services;

public interface IFreeboxServicesApi
{
    FreeboxCallApi Call { get; }
    FreeboxContactsApi Contacts { get; }
    FreeboxFtpApi Ftp { get; }
    FreeboxTftpApi Tftp { get; }
    FreeboxNetworkShareApi NetworkShare { get; }
    FreeboxUPnpAvApi UPnpAv { get; }
    FreeboxVpnApi Vpn { get; }
    FreeboxVpnClientApi VpnClient { get; }
    FreeboxPlayerApi Player { get; }
    FreeboxPvrApi Pvr { get; }
}

/// <summary>Freebox Server services using the discovered server API major. Player proxy paths retain their documented internal v6 API.</summary>
public sealed class FreeboxServicesApi : IFreeboxServicesApi
{
    public FreeboxServicesApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        var binary = transport as IFreeboxBinaryTransport;
        Call = new(transport, binary);
        Contacts = new(transport);
        Ftp = new(transport);
        Tftp = new(transport);
        NetworkShare = new(transport);
        UPnpAv = new(transport);
        Vpn = new(transport, binary);
        VpnClient = new(transport);
        Player = new(transport);
        Pvr = new(transport);
    }
    public FreeboxCallApi Call { get; }
    public FreeboxContactsApi Contacts { get; }
    public FreeboxFtpApi Ftp { get; }
    public FreeboxTftpApi Tftp { get; }
    public FreeboxNetworkShareApi NetworkShare { get; }
    public FreeboxUPnpAvApi UPnpAv { get; }
    public FreeboxVpnApi Vpn { get; }
    public FreeboxVpnClientApi VpnClient { get; }
    public FreeboxPlayerApi Player { get; }
    public FreeboxPvrApi Pvr { get; }
}
