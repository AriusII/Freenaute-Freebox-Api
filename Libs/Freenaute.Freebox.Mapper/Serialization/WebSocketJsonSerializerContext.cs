using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.WebSockets;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Serialization;

/// <summary>Source-generated control-message metadata; file content remains binary.</summary>
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(RegisterEventsRequest))]
[JsonSerializable(typeof(FreeboxWebSocketMessage))]
[JsonSerializable(typeof(FileUploadStartRequest))]
[JsonSerializable(typeof(FileUploadFinalizeRequest))]
[JsonSerializable(typeof(FileUploadCancelRequest))]
[JsonSerializable(typeof(FileUploadChunkResult))]
[JsonSerializable(typeof(FileUploadControlResponse))]
[JsonSerializable(typeof(EncodedFreeboxPath))]
[JsonSerializable(typeof(JsonElement))]
public sealed partial class WebSocketJsonSerializerContext : JsonSerializerContext;
