using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common;

/// <summary>
///     Represents an error response from the API.
/// </summary>
/// <param name="Uid">The unique identifier for the error response.</param>
/// <param name="Success">Indicates whether the request was successful.</param>
/// <param name="ErrorCode">The error code associated with the response.</param>
/// <param name="Message">The error message associated with the response.</param>
/// <param name="Result">The result object of type <typeparamref name="TResponse" />.</param>
/// <typeparam name="TResponse">The type of the result object.</typeparam>
public sealed record ErrorResponse<TResponse>(
    [property: JsonPropertyName("success")]
    bool Success,
    [property: JsonPropertyName("error_code")]
    string ErrorCode,
    [property: JsonPropertyName("msg")] string Message,
    [property: JsonPropertyName("result")] TResponse? Result = default,
    [property: JsonPropertyName("uid")] string? Uid = null);
