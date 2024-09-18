using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Common.Types;

namespace Freenaute.Freebox.Mapper.ServerSide.Authentication.Login;

public sealed record AuthorizeTrackResponse(
    [property: JsonPropertyName("status")] AuthorizationTrackEnum Status,
    [property: JsonPropertyName("challenge")]
    string Challenge
);
