using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common;

/// <summary>
///     Represents a response indicating the success of an operation.
/// </summary>
/// <typeparam name="TResponse">The type of the result contained in the response.</typeparam>
/// <param name="Success">Indicates whether the operation was successful.</param>
/// <param name="Result">The result of the operation.</param>
public sealed record SuccessfulResponse<TResponse>(
    [property: JsonPropertyName("success")]
    bool Success,
    [property: JsonPropertyName("result")] TResponse Result);
