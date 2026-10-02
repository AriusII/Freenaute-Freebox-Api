using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;
using Freenaute.Freebox.Mapper.Contracts.Protocol;

namespace Freenaute.Freebox.Mapper.Serialization;

[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    Converters = [typeof(NotificationTargetResultJsonConverter)])]
[JsonSerializable(typeof(Camera))]
[JsonSerializable(typeof(Camera[]))]
[JsonSerializable(typeof(NotificationTarget))]
[JsonSerializable(typeof(NotificationTarget[]))]
[JsonSerializable(typeof(ObjectOrArray<NotificationTarget>))]
[JsonSerializable(typeof(NotificationTargetWrite))]
[JsonSerializable(typeof(NotificationRegistrationCallback))]
[JsonSerializable(typeof(NotificationDeliveryCallback))]
[JsonSerializable(typeof(NotificationDeliveryResult))]
public sealed partial class ProtocolJsonSerializerContext : JsonSerializerContext;
