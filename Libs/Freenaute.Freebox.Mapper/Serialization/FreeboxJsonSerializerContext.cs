using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.ClientSide.Api.AirMedia;
using Freenaute.Freebox.Mapper.ClientSide.Authentication.Login;
using Freenaute.Freebox.Mapper.ClientSide.WebSocket;
using Freenaute.Freebox.Mapper.Common;
using Freenaute.Freebox.Mapper.Common.Types;
using Freenaute.Freebox.Mapper.ServerSide.Api.AirMedia;
using Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;
using Freenaute.Freebox.Mapper.ServerSide.General;
using Freenaute.Freebox.Mapper.ServerSide.Models;
using Freenaute.Freebox.Mapper.ServerSide.WebSocket;

namespace Freenaute.Freebox.Mapper.Serialization;

/// <summary>
///     Supplies generated JSON metadata for the Freebox wire contracts without runtime reflection.
/// </summary>
[JsonSourceGenerationOptions(
    GenerationMode = JsonSourceGenerationMode.Default,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(ApiVersionResponse))]
[JsonSerializable(typeof(TokenRequest))]
[JsonSerializable(typeof(SessionStartRequest))]
[JsonSerializable(typeof(AuthorizeResponse))]
[JsonSerializable(typeof(AuthorizeTrackResponse))]
[JsonSerializable(typeof(LoginResponse))]
[JsonSerializable(typeof(SessionResponse))]
[JsonSerializable(typeof(SessionPermissionModel))]
[JsonSerializable(typeof(SessionFailedModel))]
[JsonSerializable(typeof(AirMediaReceiverRequest))]
[JsonSerializable(typeof(UpdateAirMediaConfigRequest))]
[JsonSerializable(typeof(AirMediaCapabilities))]
[JsonSerializable(typeof(AirMediaConfigResponse))]
[JsonSerializable(typeof(AirMediaErrorResponse))]
[JsonSerializable(typeof(AirMediaReceiverResponse))]
[JsonSerializable(typeof(List<AirMediaReceiverResponse>))]
[JsonSerializable(typeof(AirMediaReceiverResponse[]))]
[JsonSerializable(typeof(WebSocketRequest))]
[JsonSerializable(typeof(WebSocketResponse))]
[JsonSerializable(typeof(WebSocketNotification))]
[JsonSerializable(typeof(AirMediaAction))]
[JsonSerializable(typeof(AirMediaMediaType))]
[JsonSerializable(typeof(AirMediaErrorCodeEnum))]
[JsonSerializable(typeof(AuthorizationTrackEnum))]
[JsonSerializable(typeof(AuthenticationErrorEnum))]
[JsonSerializable(typeof(BoxModels))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(SuccessfulResponse<AuthorizeResponse>))]
[JsonSerializable(typeof(SuccessfulResponse<AuthorizeTrackResponse>))]
[JsonSerializable(typeof(SuccessfulResponse<LoginResponse>))]
[JsonSerializable(typeof(SuccessfulResponse<SessionResponse>))]
[JsonSerializable(typeof(SuccessfulResponse<AirMediaConfigResponse>))]
[JsonSerializable(typeof(SuccessfulResponse<AirMediaReceiverResponse>))]
[JsonSerializable(typeof(SuccessfulResponse<List<AirMediaReceiverResponse>>))]
[JsonSerializable(typeof(SuccessfulResponse<AirMediaReceiverResponse[]>))]
[JsonSerializable(typeof(SuccessfulResponse<JsonElement>))]
[JsonSerializable(typeof(ErrorResponse<SessionFailedModel>))]
[JsonSerializable(typeof(ErrorResponse<JsonElement>))]
public sealed partial class FreeboxJsonSerializerContext : JsonSerializerContext;
