using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Protocol;

public sealed record NotificationTarget
{
    [JsonPropertyName("id")] public string? Id { get; init; }
    [JsonPropertyName("last_use")] public long LastUse { get; init; }
    [JsonPropertyName("type")] public string? Type { get; init; }
    [JsonPropertyName("name")] public string? Name { get; init; }
    [JsonPropertyName("api_url")] public string? ApiUrl { get; init; }
    [JsonPropertyName("message_type")] public string? MessageType { get; init; }
    [JsonPropertyName("subscriptions")] public string[]? Subscriptions { get; init; }
}

/// <summary>The fields attested by notification target creation and update examples. Values are snapshotted by the command.</summary>
public sealed record NotificationTargetWrite
{
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("type")] public required string Type { get; init; }
    [JsonPropertyName("token")] public required string Token { get; init; }
    [JsonPropertyName("api_url")] public required string ApiUrl { get; init; }
    [JsonPropertyName("message_type")] public required string MessageType { get; init; }
    [JsonPropertyName("subscriptions")] public required string[] Subscriptions { get; init; }
}

/// <summary>A callback contract implemented by a consumer push server, never an outbound Freebox operation.</summary>
public sealed record NotificationRegistrationCallback
{
    [JsonPropertyName("box_id")] public string? BoxId { get; init; }
    [JsonPropertyName("device_type")] public string? DeviceType { get; init; }
    [JsonPropertyName("token")] public string? Token { get; init; }
    [JsonPropertyName("device_name")] public string? DeviceName { get; init; }
    [JsonPropertyName("device_id")] public string? DeviceId { get; init; }
}

public sealed record NotificationDeliveryCallback
{
    [JsonPropertyName("box_id")] public string? BoxId { get; init; }
    [JsonPropertyName("devices")] public string[]? Devices { get; init; }
    [JsonPropertyName("title")] public string? Title { get; init; }
    [JsonPropertyName("body")] public string? Body { get; init; }
    [JsonPropertyName("payload")] public JsonElement Payload { get; init; }
}

public sealed record NotificationDeliveryResult
{
    [JsonPropertyName("failureIds")] public string[]? FailureIds { get; init; }
    [JsonPropertyName("successIds")] public string[]? SuccessIds { get; init; }
}

public sealed class NotificationTargetResultJsonConverter : ObjectOrArrayJsonConverter<NotificationTarget>;
