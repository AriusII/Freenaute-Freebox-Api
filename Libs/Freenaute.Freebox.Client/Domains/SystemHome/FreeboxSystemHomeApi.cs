using System.Collections.Immutable;
using System.Globalization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Contracts.SystemHome;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Client.Domains.SystemHome;

public interface IFreeboxSystemHomeApi
{
    FreeboxSystemApi System { get; }
    FreeboxLanguageApi Language { get; }
    FreeboxLcdApi Lcd { get; }
    FreeboxLedstripApi Ledstrip { get; }
    FreeboxStandbyApi Standby { get; }
    FreeboxUpdatesApi Updates { get; }
    FreeboxHomeApi Home { get; }
    FreeboxProfilesApi Profiles { get; }
}

/// <summary>System and Home facades sharing one injected transport and its session coordinator.</summary>
public sealed class FreeboxSystemHomeApi : IFreeboxSystemHomeApi
{
    public FreeboxSystemHomeApi(IFreeboxTransport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        System = new(transport); Language = new(transport); Lcd = new(transport); Ledstrip = new(transport);
        Standby = new(transport); Updates = new(transport); Home = new(transport); Profiles = new(transport);
    }

    public FreeboxSystemApi System { get; }
    public FreeboxLanguageApi Language { get; }
    public FreeboxLcdApi Lcd { get; }
    public FreeboxLedstripApi Ledstrip { get; }
    public FreeboxStandbyApi Standby { get; }
    public FreeboxUpdatesApi Updates { get; }
    public FreeboxHomeApi Home { get; }
    public FreeboxProfilesApi Profiles { get; }
}

internal static class SystemHomeWire
{
    internal static SystemHomeJsonSerializerContext Json => SystemHomeJsonSerializerContext.Default;
    internal static string Id(long value) => value.ToString(CultureInfo.InvariantCulture);
}

public sealed class FreeboxSystemApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxSystemApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<SystemConfig> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "system/", SystemHomeWire.Json.SystemConfig, cancellationToken);
    public Task RebootAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "system/reboot/", cancellationToken);
    public Task ShutdownAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "system/shutdown/", cancellationToken);
}

public sealed class FreeboxLanguageApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxLanguageApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<LanguageSupport> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "lang/", SystemHomeWire.Json.LanguageSupport, cancellationToken);
    public FreeboxSystemHomeCommand<SetLanguageRequest> Set(string language) =>
        new(transport, HttpMethod.Post, "lang/", new(language), SystemHomeWire.Json.SetLanguageRequest);
}

public sealed class FreeboxLcdApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxLcdApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<LcdConfig> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "lcd/config/", SystemHomeWire.Json.LcdConfig, cancellationToken);
    public FreeboxSystemHomeCommand<LcdConfigPatch, LcdConfig> Configure() =>
        new(transport, HttpMethod.Put, "lcd/config/", new(), SystemHomeWire.Json.LcdConfigPatch, SystemHomeWire.Json.LcdConfig);
    public FreeboxSystemHomeCommand<LcdConfigPatch, LcdConfig> WithBrightness(int brightness) =>
        Configure().UseFields(new LcdConfigPatch().WithBrightness(brightness));
}

public sealed class FreeboxLedstripApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxLedstripApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<LedstripStatus> GetStatusAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "ledstrip/status", SystemHomeWire.Json.LedstripStatus, cancellationToken);
    public Task<LedstripPlanning> GetPlanningAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "ledstrip/planning/", SystemHomeWire.Json.LedstripPlanning, cancellationToken);
    public FreeboxSystemHomeCommand<LedstripPlanningUpdate, LedstripPlanning> ConfigurePlanning() =>
        new(transport, HttpMethod.Put, "ledstrip/planning", new(), SystemHomeWire.Json.LedstripPlanningUpdate, SystemHomeWire.Json.LedstripPlanning);
}

public sealed class FreeboxStandbyApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxStandbyApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<StandbyStatus> GetStatusAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "standby/status", SystemHomeWire.Json.StandbyStatus, cancellationToken);
    public Task<StandbyConfig> GetConfigurationAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "standby/config/", SystemHomeWire.Json.StandbyConfig, cancellationToken);
    public FreeboxSystemHomeCommand<StandbyConfigUpdate, StandbyConfig> Configure() =>
        new(transport, HttpMethod.Put, "standby/config", new(), SystemHomeWire.Json.StandbyConfigUpdate, SystemHomeWire.Json.StandbyConfig);
}

public sealed class FreeboxUpdatesApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxUpdatesApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<UpdateStatus> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "update/", SystemHomeWire.Json.UpdateStatus, cancellationToken);
}

public sealed class FreeboxHomeApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxHomeApi(IFreeboxTransport transport)
    { this.transport = transport; Adapters = new(transport); Nodes = new(transport); Tiles = new(transport); }
    public FreeboxHomeAdaptersApi Adapters { get; }
    public FreeboxHomeNodesApi Nodes { get; }
    public FreeboxHomeTilesApi Tiles { get; }
    public FreeboxHomeAdapterResource Adapter(long id) => new(transport, id);
    public FreeboxHomeNodeResource Node(long id) => new(transport, id);
}

public sealed class FreeboxHomeAdaptersApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxHomeAdaptersApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<ImmutableArray<HomeAdapter>> ListAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "home/adapters", SystemHomeWire.Json.HomeAdapters, cancellationToken);
}

public sealed class FreeboxHomeAdapterResource
{
    private readonly IFreeboxTransport transport;
    private readonly string path;
    internal FreeboxHomeAdapterResource(IFreeboxTransport transport, long id)
    { this.transport = transport; path = "home/adapters/" + SystemHomeWire.Id(id); Pairing = new(transport, id); }
    public FreeboxHomePairingResource Pairing { get; }
    public Task<HomeAdapter> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, path, SystemHomeWire.Json.HomeAdapter, cancellationToken);
    public FreeboxSystemHomeCommand<HomeAdapterStatusRequest> SetStatus(HomeAdapterStatus status) =>
        new(transport, HttpMethod.Put, path, new(status), SystemHomeWire.Json.HomeAdapterStatusRequest);
    public FreeboxSystemHomeCommand<HomeAdapterStatusRequest> Activate() => SetStatus(HomeAdapterStatus.Active);
    public FreeboxSystemHomeCommand<HomeAdapterStatusRequest> Disable() => SetStatus(HomeAdapterStatus.Disabled);
}

public sealed class FreeboxHomePairingResource
{
    private readonly IFreeboxTransport transport;
    private readonly string path;
    internal FreeboxHomePairingResource(IFreeboxTransport transport, long id)
    { this.transport = transport; path = "home/pairing/" + SystemHomeWire.Id(id); }
    public Task<HomePairingStep> GetStepAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, path, SystemHomeWire.Json.HomePairingStep, cancellationToken);
    public FreeboxSystemHomeCommand<HomePairingStartRequest> Start(string? type = null) =>
        new(transport, HttpMethod.Post, path, new() { Type = type }, SystemHomeWire.Json.HomePairingStartRequest);
    public FreeboxSystemHomeCommand<HomePairingNextRequest, HomePairingStep> Next(
        StringOrInteger session, StringOrInteger pageId, IEnumerable<HomeIoValue> fields)
    {
        ArgumentNullException.ThrowIfNull(fields);
        return new(transport, HttpMethod.Post, path,
            new() { Session = session, PageId = pageId, Fields = fields.ToImmutableArray() },
            SystemHomeWire.Json.HomePairingNextRequest, SystemHomeWire.Json.HomePairingStep);
    }
    public FreeboxSystemHomeCommand<HomePairingStopRequest> Stop(long session) =>
        new(transport, HttpMethod.Post, path, new(session), SystemHomeWire.Json.HomePairingStopRequest);
}

public sealed class FreeboxHomeNodesApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxHomeNodesApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<ImmutableArray<HomeNode>> ListAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "home/nodes", SystemHomeWire.Json.HomeNodes, cancellationToken);
}

public sealed class FreeboxHomeNodeResource
{
    private readonly IFreeboxTransport transport;
    private readonly long id;
    private readonly string path;
    internal FreeboxHomeNodeResource(IFreeboxTransport transport, long id)
    { this.transport = transport; this.id = id; path = "home/nodes/" + SystemHomeWire.Id(id); }
    public Task<HomeNode> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, path, SystemHomeWire.Json.HomeNode, cancellationToken);
    public FreeboxSystemHomeCommand<RenameHomeNodeRequest> Rename(string label) =>
        new(transport, HttpMethod.Put, path, new(label), SystemHomeWire.Json.RenameHomeNodeRequest);
    public Task DeleteAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, path, cancellationToken);
    public FreeboxHomeEndpointResource Endpoint(long endpointId) => new(transport, id, endpointId);
}

public sealed class FreeboxHomeEndpointResource
{
    private readonly IFreeboxTransport transport;
    private readonly string path;
    internal FreeboxHomeEndpointResource(IFreeboxTransport transport, long nodeId, long endpointId)
    { this.transport = transport; path = "home/endpoints/" + SystemHomeWire.Id(nodeId) + "/" + SystemHomeWire.Id(endpointId); }
    public Task<HomeNodeEndpointValue> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, path, SystemHomeWire.Json.HomeNodeEndpointValue, cancellationToken);
    public FreeboxSystemHomeCommand<HomeEndpointValueRequest> Set(HomeIoValue value) =>
        new(transport, HttpMethod.Put, path, new(value), SystemHomeWire.Json.HomeEndpointValueRequest);
    public FreeboxSystemHomeCommand<HomeEndpointValueRequest> Trigger() => Set(HomeIoValue.Null);
    public FreeboxSystemHomeCommand<HomeEndpointValueRequest> SetBoolean(bool value) => Set(HomeIoValue.FromBoolean(value));
    public FreeboxSystemHomeCommand<HomeEndpointValueRequest> SetInteger(long value) => Set(HomeIoValue.FromInteger(value));
    public FreeboxSystemHomeCommand<HomeEndpointValueRequest> SetFloat(double value) => Set(HomeIoValue.FromFloat(value));
    public FreeboxSystemHomeCommand<HomeEndpointValueRequest> SetString(string value) => Set(HomeIoValue.FromString(value));
}

public sealed class FreeboxHomeTilesApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxHomeTilesApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<ImmutableArray<HomeTile>> ListAllAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "home/tileset/all", SystemHomeWire.Json.HomeTiles, cancellationToken);
    public Task<ImmutableArray<HomeTile>> ForNodeAsync(long nodeId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "home/tileset/" + SystemHomeWire.Id(nodeId), SystemHomeWire.Json.HomeTiles, cancellationToken);
}

public sealed class FreeboxProfilesApi
{
    private readonly IFreeboxTransport transport;
    internal FreeboxProfilesApi(IFreeboxTransport transport) => this.transport = transport;
    public Task<ImmutableArray<Profile>> ListAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "profile", SystemHomeWire.Json.Profiles, cancellationToken);
    public FreeboxProfileResource For(long id) => new(transport, id);
    public FreeboxSystemHomeCommand<ProfileCreateRequest, ProfileCreated> Create(string name) =>
        new(transport, HttpMethod.Post, "profile/", new(name), SystemHomeWire.Json.ProfileCreateRequest, SystemHomeWire.Json.ProfileCreated);
    public Task<DefaultModeMigrationStatus> GetMigrationStatusAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, "network_control/migrate", SystemHomeWire.Json.DefaultModeMigrationStatus, cancellationToken);
    public Task<DefaultModeMigrationStatus> MigrateDefaultModeAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Post, "network_control/migrate", SystemHomeWire.Json.DefaultModeMigrationStatus, cancellationToken);
}

public sealed class FreeboxProfileResource
{
    private readonly IFreeboxTransport transport;
    private readonly string path;
    internal FreeboxProfileResource(IFreeboxTransport transport, long id)
    { this.transport = transport; path = "profile/" + SystemHomeWire.Id(id); NetworkControl = new(transport, id); }
    public FreeboxNetworkControlResource NetworkControl { get; }
    public Task<Profile> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, path, SystemHomeWire.Json.Profile, cancellationToken);
    public Task DeleteAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, path, cancellationToken);
}

public sealed class FreeboxNetworkControlResource
{
    private readonly IFreeboxTransport transport;
    private readonly string path;
    internal FreeboxNetworkControlResource(IFreeboxTransport transport, long id)
    { this.transport = transport; path = "network_control/" + SystemHomeWire.Id(id); Rules = new(transport, id); }
    public FreeboxNetworkControlRulesApi Rules { get; }
    public Task<NetworkControl> GetAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, path, SystemHomeWire.Json.NetworkControl, cancellationToken);
    public FreeboxSystemHomeCommand<NetworkControlUpdate, NetworkControl> Configure() =>
        new(transport, HttpMethod.Put, path, new(), SystemHomeWire.Json.NetworkControlUpdate, SystemHomeWire.Json.NetworkControl);
}

public sealed class FreeboxNetworkControlRulesApi
{
    private readonly IFreeboxTransport transport;
    private readonly string path;
    internal FreeboxNetworkControlRulesApi(IFreeboxTransport transport, long id)
    { this.transport = transport; path = "network_control/" + SystemHomeWire.Id(id) + "/rules"; }
    public Task<ImmutableArray<NetworkControlRule>> ListAsync(CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, path, SystemHomeWire.Json.NetworkControlRules, cancellationToken);
    public Task<NetworkControlRule> GetAsync(long ruleId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Get, path + "/" + SystemHomeWire.Id(ruleId), SystemHomeWire.Json.NetworkControlRule, cancellationToken);
    public Task DeleteAsync(long ruleId, CancellationToken cancellationToken = default) =>
        transport.SendAsync(HttpMethod.Delete, path + "/" + SystemHomeWire.Id(ruleId), cancellationToken);
}
