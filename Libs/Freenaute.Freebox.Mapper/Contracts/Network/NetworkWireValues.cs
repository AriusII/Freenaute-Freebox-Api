namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Documented wire tokens. String-valued contracts preserve tokens introduced by newer firmware.</summary>
public static class NetworkWireValues
{
    public static class ConnectionStatusState
    {
        public const string GoingUp = "going_up";
        public const string Up = "up";
        public const string GoingDown = "going_down";
        public const string Down = "down";
    }

    public static class ConnectionStatusType
    {
        public const string Ethernet = "ethernet";
        public const string Rfc2684 = "rfc2684";
        public const string Pppoatm = "pppoatm";
    }

    public static class ConnectionStatusMedia
    {
        public const string Ftth = "ftth";
        public const string Ethernet = "ethernet";
        public const string Xdsl = "xdsl";
        public const string Backup4g = "backup_4g";
    }

    public static class ConnectionConfigurationSipAlg
    {
        public const string Disabled = "disabled";
        public const string DirectMedia = "direct_media";
        public const string AnyMedia = "any_media";
    }

    public static class XdslStatusStatus
    {
        public const string Down = "down";
        public const string Training = "training";
        public const string Started = "started";
        public const string ChanAnalysis = "chan_analysis";
        public const string MsgExchange = "msg_exchange";
        public const string Showtime = "showtime";
        public const string Disabled = "disabled";
    }

    public static class XdslStatusProtocol
    {
        public const string T1413 = "t1413";
        public const string Adsl1A = "adsl1_a";
        public const string Adsl2A = "adsl2_a";
        public const string Adsl2plusA = "adsl2plus_a";
        public const string Readsl2 = "readsl2";
        public const string Adsl2M = "adsl2_m";
        public const string Adsl2plusM = "adsl2plus_m";
        public const string Unknown = "unknown";
    }

    public static class XdslStatusModulation
    {
        public const string Adsl = "adsl";
        public const string Vdsl = "vdsl";
    }

    public static class DDNSStatusStatus
    {
        public const string Disabled = "disabled";
        public const string Ok = "ok";
        public const string Wait = "wait";
        public const string Reqfail = "reqfail";
        public const string Authfail = "authfail";
        public const string Nocredential = "nocredential";
        public const string Ipinval = "ipinval";
        public const string Hostinval = "hostinval";
        public const string Abuse = "abuse";
        public const string Dnserror = "dnserror";
        public const string Unavailable = "unavailable";
        public const string Nowan = "nowan";
        public const string Unknown = "unknown";
    }

    public static class DhcpOptionId
    {
        public const string TimeOffset = "time_offset";
        public const string TimeServer = "time_server";
        public const string LogServer = "log_server";
        public const string CookieServer = "cookie_server";
        public const string LprServer = "lpr_server";
        public const string ImpressServer = "impress_server";
        public const string ResourceLocationServer = "resource_location_server";
        public const string Hostname = "hostname";
        public const string MeritDumpFile = "merit_dump_file";
        public const string DomainName = "domain_name";
        public const string SwapServer = "swap_server";
        public const string RootPath = "root_path";
        public const string ExtensionsPath = "extensions_path";
        public const string IpFwd = "ip_fwd";
        public const string IpFwdNonLocal = "ip_fwd_non_local";
        public const string IpMaxReassemblySize = "ip_max_reassembly_size";
        public const string IpTtl = "ip_ttl";
        public const string IpPmtuTimeout = "ip_pmtu_timeout";
        public const string Mtu = "mtu";
        public const string LocalSubnets = "local_subnets";
        public const string MaskDiscovery = "mask_discovery";
        public const string MaskSupplier = "mask_supplier";
        public const string PerformRd = "perform_rd";
        public const string RsAddress = "rs_address";
        public const string TrailerEncapsulation = "trailer_encapsulation";
        public const string ArpCacheTimeout = "arp_cache_timeout";
        public const string EthEncapsulation = "eth_encapsulation";
        public const string TcpTtl = "tcp_ttl";
        public const string TcpKeepaliveInterval = "tcp_keepalive_interval";
        public const string TcpKeepaliveGarbage = "tcp_keepalive_garbage";
        public const string NisDomain = "nis_domain";
        public const string NisServer = "nis_server";
        public const string NtpServer = "ntp_server";
        public const string VendorSpecific = "vendor_specific";
        public const string NisPlusDomain = "nis_plus_domain";
        public const string NisPlusServer = "nis_plus_server";
        public const string TftpServerName = "tftp_server_name";
        public const string BootfileName = "bootfile_name";
        public const string MobileIpAgent = "mobile_ip_agent";
        public const string SmtpServer = "smtp_server";
        public const string Pop3Server = "pop3_server";
        public const string NntpServer = "nntp_server";
        public const string WwwServer = "www_server";
        public const string FingerServer = "finger_server";
        public const string IrcServer = "irc_server";
        public const string StreettalkServer = "streettalk_server";
        public const string StdaServer = "stda_server";
        public const string SlpDirectoryAgent = "slp_directory_agent";
        public const string SlpServiceScope = "slp_service_scope";
        public const string NdsServers = "nds_servers";
        public const string NdsTreeName = "nds_tree_name";
        public const string NdsContext = "nds_context";
        public const string LdapServers = "ldap_servers";
        public const string TimezonePosix = "timezone_posix";
        public const string TimezoneDatabase = "timezone_database";
        public const string NameService = "name_service";
        public const string DomainSearch = "domain_search";
        public const string ClasslessStaticRoute = "classless_static_route";
        public const string CapwapAc = "capwap_ac";
        public const string TftpServerAddress = "tftp_server_address";
    }

    public static class FreeplugNetRole
    {
        public const string Sta = "sta";
        public const string Pco = "pco";
        public const string Cco = "cco";
    }

    public static class FreeplugEthPortStatus
    {
        public const string Up = "up";
        public const string Down = "down";
        public const string Unknown = "unknown";
    }

    public static class LanConfigType
    {
        public const string Router = "router";
        public const string Bridge = "bridge";
    }

    public static class LanHostHostType
    {
        public const string Workstation = "workstation";
        public const string Laptop = "laptop";
        public const string Smartphone = "smartphone";
        public const string Tablet = "tablet";
        public const string Printer = "printer";
        public const string VgConsole = "vg_console";
        public const string Television = "television";
        public const string Nas = "nas";
        public const string IpCamera = "ip_camera";
        public const string IpPhone = "ip_phone";
        public const string FreeboxPlayer = "freebox_player";
        public const string FreeboxHd = "freebox_hd";
        public const string FreeboxCrystal = "freebox_crystal";
        public const string FreeboxMini = "freebox_mini";
        public const string FreeboxDelta = "freebox_delta";
        public const string FreeboxOne = "freebox_one";
        public const string FreeboxWifi = "freebox_wifi";
        public const string FreeboxPop = "freebox_pop";
        public const string NetworkingDevice = "networking_device";
        public const string MultimediaDevice = "multimedia_device";
        public const string Car = "car";
        public const string Watch = "watch";
        public const string Light = "light";
        public const string Outlet = "outlet";
        public const string Appliances = "appliances";
        public const string Thermostat = "thermostat";
        public const string Shutter = "shutter";
        public const string Other = "other";
    }

    public static class LanHostL2IdentType
    {
        public const string Dhcp = "dhcp";
        public const string Netbios = "netbios";
        public const string Mdns = "mdns";
        public const string MdnsSrv = "mdns_srv";
        public const string Upnp = "upnp";
        public const string Wsd = "wsd";
    }

    public static class LanHostL3ConnectivityAf
    {
        public const string Ipv4 = "ipv4";
        public const string Ipv6 = "ipv6";
    }

    public static class PortForwardingConfigIpProto
    {
        public const string Tcp = "tcp";
        public const string Udp = "udp";
    }

    public static class IncomingPortConfigId
    {
        public const string Http = "http";
        public const string Https = "https";
        public const string OpenvpnRouted = "openvpn_routed";
        public const string OpenvpnBridge = "openvpn_bridge";
        public const string IpsecIke = "ipsec_ike";
        public const string IpsecNat = "ipsec_nat";
        public const string Pptp = "pptp";
        public const string Ftp = "ftp";
        public const string FtpPasv = "ftp_pasv";
    }

    public static class IncomingPortConfigType
    {
        public const string Tcp = "tcp";
        public const string Udp = "udp";
        public const string TcpUdp = "tcp_udp";
    }

    public static class SfpConfigAvailableSfpTypes
    {
        public const string P2p1g = "p2p_1g";
        public const string P2p2d5gNoAneg = "p2p_2d5g_no_aneg";
        public const string P2p10g = "p2p_10g";
        public const string Copper1g = "copper_1g";
        public const string CopperSgmii1g = "copper_sgmii_1g";
        public const string CopperSgmii10g = "copper_sgmii_10g";
    }

    public static class SwitchPortStatusLink
    {
        public const string Up = "up";
        public const string Down = "down";
    }

    public static class SwitchPortStatusDuplex
    {
        public const string Half = "half";
        public const string Full = "full";
    }

    public static class SwitchPortStatusSpeed
    {
        public const string Value10 = "10";
        public const string Value100 = "100";
        public const string Value1000 = "1000";
    }

    public static class SwitchPortConfigDuplex
    {
        public const string Auto = "auto";
        public const string Half = "half";
        public const string Full = "full";
    }

    public static class SwitchPortConfigSpeed
    {
        public const string Auto = "auto";
        public const string Value10 = "10";
        public const string Value100 = "100";
        public const string Value1000 = "1000";
    }

    public static class WifiGlobalConfigMacFilterState
    {
        public const string Disabled = "disabled";
        public const string Whitelist = "whitelist";
        public const string Blacklist = "blacklist";
    }

    public static class WifiSteeringConfigSteeringLevel
    {
        public const string Value0 = "0";
        public const string Value1 = "1";
        public const string Value2 = "2";
    }

    public static class WifiGlobalStateState
    {
        public const string Enabled = "enabled";
        public const string Disabled = "disabled";
        public const string DisabledPlanning = "disabled_planning";
    }

    public static class ExpectedPhyBand
    {
        public const string Value2d4g = "2d4g";
        public const string Value5g = "5g";
        public const string Value6g = "6g";
        public const string Value60g = "60g";
    }

    public static class WifiApStatusState
    {
        /// <summary>Explicitly established by the API 12.0 change note.</summary>
        public const string Stopping = "stopping";
        public const string Scanning = "scanning";
        public const string NoParam = "no_param";
        public const string BadParam = "bad_param";
        public const string Disabled = "disabled";
        public const string DisabledPlanning = "disabled_planning";
        public const string DisabledPowerSaving = "disabled_power_saving";
        public const string DisabledTemp = "disabled_temp";
        public const string NoActiveBss = "no_active_bss";
        public const string Starting = "starting";
        public const string Acs = "acs";
        public const string HtScan = "ht_scan";
        public const string Dfs = "dfs";
        public const string Active = "active";
        public const string Failed = "failed";
    }

    public static class WifiApConfigBand
    {
        public const string Value2d4g = "2d4g";
        public const string Value5g = "5g";
        public const string Value6g = "6g";
        public const string Value60g = "60g";
    }

    public static class WifiAllowedCombBand
    {
        public const string Value2d4g = "2d4g";
        public const string Value5g = "5g";
        public const string Value60g = "60g";
    }

    public static class WifiStationState
    {
        public const string Associated = "associated";
        public const string Authenticated = "authenticated";
    }

    public static class WifiBssStatusState
    {
        public const string PhyStopped = "phy_stopped";
        public const string NoParam = "no_param";
        public const string BadParam = "bad_param";
        public const string Disabled = "disabled";
        public const string TempDisabled = "temp_disabled";
        public const string Starting = "starting";
        public const string Active = "active";
        public const string Failed = "failed";
    }

    public static class WifiBssConfigEncryption
    {
        public const string Wep = "wep";
        public const string WpaPskAuto = "wpa_psk_auto";
        public const string WpaPskTkip = "wpa_psk_tkip";
        public const string WpaPskCcmp = "wpa_psk_ccmp";
        public const string Wpa12PskAuto = "wpa12_psk_auto";
        public const string Wpa2PskAuto = "wpa2_psk_auto";
        public const string Wpa2PskTkip = "wpa2_psk_tkip";
        public const string Wpa2PskCcmp = "wpa2_psk_ccmp";
        public const string Wpa23PskCcmp = "wpa23_psk_ccmp";
        public const string Wpa23PskCcmpMrsno = "wpa23_psk_ccmp_mrsno";
        public const string Wpa3PskCcmp = "wpa3_psk_ccmp";
    }

    public static class WifiNeighborBand
    {
        public const string Value2d4g = "2d4g";
        public const string Value5g = "5g";
        public const string Value60g = "60g";
    }

    public static class WifiChannelUsageBand
    {
        public const string Value2d4g = "2d4g";
        public const string Value5g = "5g";
        public const string Value60g = "60g";
    }

    public static class WifiMacFilterType
    {
        public const string Whitelist = "whitelist";
        public const string Blacklist = "blacklist";
    }

    public static class WifiDiagItemCode
    {
        public const string All = "all";
        public const string NetworkDisabled = "network_disabled";
        public const string NetworkSecurity = "network_security";
        public const string NetworkVisibility = "network_visibility";
        public const string ChannelWidth = "channel_width";
        public const string ChannelValue = "channel_value";
    }

    public static class WifiDiagItemSeverity
    {
        public const string Minor = "minor";
        public const string Major = "major";
    }

    public static class WifiWpsCandidateBand
    {
        public const string Value2d4g = "2d4g";
        public const string Value5g = "5g";
        public const string Value60g = "60g";
    }

    public static class WifiWpsSessionResult
    {
        public const string Success = "success";
        public const string UserCanceled = "user_canceled";
        public const string SelfCanceled = "self_canceled";
        public const string FailedTimeout = "failed_timeout";
        public const string FailedOverlap = "failed_overlap";
        public const string FailedUnknown = "failed_unknown";
    }

    public static class WifiCustomKeyParamsAccessType
    {
        public const string Full = "full";
        public const string NetOnly = "net_only";
    }

    public static class TemporaryWifiDisableKeep
    {
        public const string Value2d4g = "2d4g";
        public const string Value5g = "5g";
        public const string Value6g = "6g";
    }

}
