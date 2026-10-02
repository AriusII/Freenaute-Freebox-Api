namespace Freenaute.Freebox.Client.Domains.Network;

public interface IFreeboxNetworkApi
{
    FreeboxConnectionApi Connection { get; }
    FreeboxDhcpApi Dhcp { get; }
    FreeboxDhcpV6Api DhcpV6 { get; }
    FreeboxLanApi Lan { get; }
    FreeboxNatApi Nat { get; }
    FreeboxIgdApi Igd { get; }
    FreeboxFreeplugApi Freeplug { get; }
    FreeboxSfpApi Sfp { get; }
    FreeboxSwitchApi Switch { get; }
    FreeboxWifiApi Wifi { get; }
}

/// <summary>Freebox Server network domains. Every facade shares the supplied transport and its box-scoped session.</summary>
public sealed class FreeboxNetworkApi : IFreeboxNetworkApi
{
    public FreeboxConnectionApi Connection { get; }
    public FreeboxDhcpApi Dhcp { get; }
    public FreeboxDhcpV6Api DhcpV6 { get; }
    public FreeboxLanApi Lan { get; }
    public FreeboxNatApi Nat { get; }
    public FreeboxIgdApi Igd { get; }
    public FreeboxFreeplugApi Freeplug { get; }
    public FreeboxSfpApi Sfp { get; }
    public FreeboxSwitchApi Switch { get; }
    public FreeboxWifiApi Wifi { get; }

    public FreeboxNetworkApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        Connection = new(transport);
        Dhcp = new(transport);
        DhcpV6 = new(transport);
        Lan = new(transport);
        Nat = new(transport);
        Igd = new(transport);
        Freeplug = new(transport);
        Sfp = new(transport);
        Switch = new(transport);
        Wifi = new(transport);
    }
}
