using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Network;
using Freenaute.Freebox.Mapper.Serialization;

namespace Freenaute.Freebox.Mapper.Contracts.SystemHome;

public enum NetworkControlHostsKind { Empty, Names, Hosts }

/// <summary>The declared LAN host array and the host-name array shown by the profile examples.</summary>
[JsonConverter(typeof(NetworkControlHostsJsonConverter))]
public sealed class NetworkControlHosts
{
    private NetworkControlHosts(NetworkControlHostsKind kind, ImmutableArray<string> names,
        ImmutableArray<LanHost> hosts)
    { Kind = kind; Names = names; Hosts = hosts; }

    public NetworkControlHostsKind Kind { get; }
    public ImmutableArray<string> Names { get; }
    public ImmutableArray<LanHost> Hosts { get; }
    public static NetworkControlHosts Empty { get; } = new(NetworkControlHostsKind.Empty, [], []);
    public static NetworkControlHosts FromNames(IEnumerable<string> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        var names = values.ToImmutableArray();
        if (names.Any(static value => value is null)) throw new ArgumentException("Host names cannot be null.", nameof(values));
        return names.IsEmpty ? Empty : new(NetworkControlHostsKind.Names, names, []);
    }

    public static NetworkControlHosts FromHosts(IEnumerable<LanHost> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        var hosts = values.ToImmutableArray();
        if (hosts.Any(static value => value is null)) throw new ArgumentException("LAN hosts cannot be null.", nameof(values));
        return hosts.IsEmpty ? Empty : new(NetworkControlHostsKind.Hosts, [], hosts);
    }
}

public sealed class NetworkControlHostsJsonConverter : JsonConverter<NetworkControlHosts>
{
    public override NetworkControlHosts Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray) throw new JsonException("Network-control hosts must be an array.");
        var names = ImmutableArray.CreateBuilder<string>();
        var hosts = ImmutableArray.CreateBuilder<LanHost>();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                return hosts.Count > 0 ? NetworkControlHosts.FromHosts(hosts) : NetworkControlHosts.FromNames(names);
            if (reader.TokenType == JsonTokenType.String && hosts.Count == 0) names.Add(reader.GetString()!);
            else if (reader.TokenType == JsonTokenType.StartObject && names.Count == 0)
                hosts.Add(JsonSerializer.Deserialize(ref reader, NetworkJsonSerializerContext.Default.LanHost)
                          ?? throw new JsonException("LAN host cannot be null."));
            else throw new JsonException("Network-control hosts cannot mix names and host objects.");
        }
        throw new JsonException("Unterminated hosts array.");
    }

    public override void Write(Utf8JsonWriter writer, NetworkControlHosts value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (var name in value.Names) writer.WriteStringValue(name);
        foreach (var host in value.Hosts) JsonSerializer.Serialize(writer, host, NetworkJsonSerializerContext.Default.LanHost);
        writer.WriteEndArray();
    }
}
