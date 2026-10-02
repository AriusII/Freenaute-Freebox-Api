using System.Globalization;
using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Client.Domains.Network;

internal static class NetworkWriteValidation
{
    private static void RejectNull<T>(Optional<T> value, string field)
    {
        if (value.IsNull) throw new ArgumentException($"JSON null is not documented for network field {field}.", field);
    }

    internal static void Validate(ConnectionConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Ping, "ping");
        RejectNull(request.RemoteAccess, "remote_access");
        RejectNull(request.RemoteAccessPort, "remote_access_port");
        RejectNull(request.Wol, "wol");
        RejectNull(request.Adblock, "adblock");
        RejectNull(request.AllowTokenRequest, "allow_token_request");
        RejectNull(request.SipAlg, "sip_alg");
    }

    internal static void Validate(ConnectionIpv6ConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Ipv6Enabled, "ipv6_enabled");
        RejectNull(request.Ipv6Firewall, "ipv6_firewall");
        RejectNull(request.Ipv6PrefixFirewall, "ipv6_prefix_firewall");
        RejectNull(request.Delegations, "delegations");
        if (request.Delegations.HasValue) foreach (var value in request.Delegations.Value) Validate(value);
        if (request.Delegations.HasValue && request.Delegations.Value.Length != 8) throw new ArgumentException("IPv6 delegation configuration is a documented array of eight entries.", nameof(request));
    }

    internal static void Validate(ConnectionIpv6DelegationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Prefix, "prefix");
        RejectNull(request.NextHop, "next_hop");
    }

    internal static void Validate(LteAggregationUpdate request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
    }

    internal static void Validate(DdnsConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.Hostname, "hostname");
        RejectNull(request.Password, "password");
        RejectNull(request.User, "user");
    }

    internal static void Validate(DhcpConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.StickyAssign, "sticky_assign");
        RejectNull(request.IpRangeStart, "ip_range_start");
        RejectNull(request.IpRangeEnd, "ip_range_end");
        RejectNull(request.AlwaysBroadcast, "always_broadcast");
        RejectNull(request.IgnoreOutOfRangeHint, "ignore_out_of_range_hint");
        RejectNull(request.BootServer, "boot_server");
        RejectNull(request.BootFile, "boot_file");
        RejectNull(request.Dns, "dns");
        if (request.Dns.HasValue) foreach (var value in request.Dns.Value) ArgumentNullException.ThrowIfNull(value);
        RejectNull(request.Options, "options");
        if (request.Options.HasValue) foreach (var value in request.Options.Value) Validate(value);
    }

    internal static void Validate(DhcpOptionWrite request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Id, "id");
        RejectNull(request.Val, "val");
    }

    internal static void Validate(DhcpStaticLeasePatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Mac, "mac");
        RejectNull(request.Comment, "comment");
        RejectNull(request.Ip, "ip");
        RejectNull(request.Options, "options");
        if (request.Options.HasValue) foreach (var value in request.Options.Value) Validate(value);
    }

    internal static void Validate(CreateDhcpStaticLeaseRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Mac, "mac");
        RejectNull(request.Comment, "comment");
        RejectNull(request.Ip, "ip");
        RejectNull(request.Options, "options");
        if (request.Options.HasValue) foreach (var value in request.Options.Value) Validate(value);
    }

    internal static void Validate(DhcpV6ConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.UseCustomDns, "use_custom_dns");
    }

    internal static void Validate(IgdConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.Version, "version");
    }

    internal static void Validate(LanConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Ip, "ip");
        RejectNull(request.Name, "name");
        RejectNull(request.NameDns, "name_dns");
        RejectNull(request.NameMdns, "name_mdns");
        RejectNull(request.NameNetbios, "name_netbios");
    }

    internal static void Validate(LanRouteWrite request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Prefix, "prefix");
        RejectNull(request.Gateway, "gateway");
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.Description, "description");
    }

    internal static void Validate(LanHostPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.PrimaryName, "primary_name");
        RejectNull(request.DomainName, "domain_name");
        RejectNull(request.HostType, "host_type");
        RejectNull(request.Persistent, "persistent");
    }

    internal static void Validate(WakeOnLanRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Mac, "mac");
        RejectNull(request.Password, "password");
    }

    internal static void Validate(DmzConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Ip, "ip");
        RejectNull(request.Enabled, "enabled");
    }

    internal static void Validate(PortForwardingPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.IpProto, "ip_proto");
        RejectNull(request.WanPortStart, "wan_port_start");
        RejectNull(request.WanPortEnd, "wan_port_end");
        RejectNull(request.LanIp, "lan_ip");
        RejectNull(request.LanPort, "lan_port");
        RejectNull(request.SrcIp, "src_ip");
        RejectNull(request.Comment, "comment");
    }

    internal static void Validate(CreatePortForwardingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.IpProto, "ip_proto");
        RejectNull(request.WanPortStart, "wan_port_start");
        RejectNull(request.WanPortEnd, "wan_port_end");
        RejectNull(request.LanIp, "lan_ip");
        RejectNull(request.LanPort, "lan_port");
        RejectNull(request.SrcIp, "src_ip");
        RejectNull(request.Comment, "comment");
    }

    internal static void Validate(IncomingPortPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.InPort, "in_port");
    }

    internal static void Validate(SfpConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.SfpTypeForced, "sfp_type_forced");
        RejectNull(request.SfpTypeForcedValue, "sfp_type_forced_value");
    }

    internal static void Validate(SwitchPortConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Duplex, "duplex");
        RejectNull(request.Speed, "speed");
    }

    internal static void Validate(WifiGlobalConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.MacFilterState, "mac_filter_state");
    }

    internal static void Validate(WifiSteeringConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.SteeringLevel, "steering_level");
    }

    internal static void Validate(WifiApHtConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.AcEnabled, "ac_enabled");
        RejectNull(request.HtEnabled, "ht_enabled");
    }

    internal static void Validate(WifiApHeConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
    }

    internal static void Validate(WifiApConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Band, "band");
        RejectNull(request.ChannelWidth, "channel_width");
        RejectNull(request.PrimaryChannel, "primary_channel");
        RejectNull(request.SecondaryChannel, "secondary_channel");
        RejectNull(request.DfsEnabled, "dfs_enabled");
        RejectNull(request.Ht, "ht");
        if (request.Ht.HasValue) Validate(request.Ht.Value);
        RejectNull(request.He, "he");
        if (request.He.HasValue) Validate(request.He.Value);
    }

    internal static void Validate(WifiAccessPointPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Config, "config");
        if (request.Config.HasValue) Validate(request.Config.Value);
    }

    internal static void Validate(WifiBssConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
        RejectNull(request.Ssid, "ssid");
        RejectNull(request.HideSsid, "hide_ssid");
        RejectNull(request.Gcmp256, "gcmp256");
        RejectNull(request.Encryption, "encryption");
        RejectNull(request.Key, "key");
        RejectNull(request.WpsEnabled, "wps_enabled");
    }

    internal static void Validate(WifiBssPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.UseSharedParams, "use_shared_params");
        RejectNull(request.Config, "config");
        if (request.Config.HasValue) Validate(request.Config.Value);
    }

    internal static void Validate(WifiPlanningPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.UsePlanning, "use_planning");
        RejectNull(request.Mapping, "mapping");
        if (request.Mapping.HasValue) foreach (var value in request.Mapping.Value) ArgumentNullException.ThrowIfNull(value);
    }

    internal static void Validate(WifiMacFilterPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Comment, "comment");
        RejectNull(request.Type, "type");
    }

    internal static void Validate(CreateWifiMacFilterRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Mac, "mac");
        RejectNull(request.Comment, "comment");
        RejectNull(request.Type, "type");
    }

    internal static void Validate(WifiDiagnosticFix request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Aps, "aps");
        if (request.Aps.HasValue) foreach (var value in request.Aps.Value) Validate(value);
        RejectNull(request.Bsss, "bsss");
        if (request.Bsss.HasValue) foreach (var value in request.Bsss.Value) Validate(value);
    }

    internal static void Validate(WifiApDiagnosticFix request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.ApId, "ap_id");
        RejectNull(request.Code, "code");
    }

    internal static void Validate(WifiBssDiagnosticFix request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Bssid, "bssid");
        RejectNull(request.Code, "code");
    }

    internal static void Validate(WifiWpsConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Enabled, "enabled");
    }

    internal static void Validate(WifiWpsStartRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Bssid, "bssid");
    }

    internal static void Validate(WifiWpsStopRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.SessionId, "session_id");
    }

    internal static void Validate(WifiCustomKeyConfigurationPatch request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Ssid, "ssid");
    }

    internal static void Validate(CreateWifiCustomKeyRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Description, "description");
        RejectNull(request.Key, "key");
        RejectNull(request.MaxUseCount, "max_use_count");
        RejectNull(request.Duration, "duration");
        RejectNull(request.AccessType, "access_type");
        if (request.MaxUseCount.HasValue)
        {
            var value = request.MaxUseCount.Value;
            var count = value.Kind == StringOrIntegerKind.Integer ? value.Integer :
                value.Kind == StringOrIntegerKind.String && long.TryParse(value.String, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed) ? parsed :
                throw new ArgumentException("Guest access max_use_count must contain an integer or its documented string representation.", nameof(request));
            if (count is < 0 or > 127) throw new ArgumentOutOfRangeException(nameof(request), "Guest access max_use_count must be between zero (unlimited) and 127.");
        }
    }

    internal static void Validate(TemporaryWifiDisableRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        RejectNull(request.Duration, "duration");
        RejectNull(request.Keep, "keep");
    }

    internal static void Validate(LanRouteWrite[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        foreach (var value in values) Validate(value);
    }

    internal static void Validate(string[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        foreach (var value in values) ArgumentNullException.ThrowIfNull(value);
    }

}
