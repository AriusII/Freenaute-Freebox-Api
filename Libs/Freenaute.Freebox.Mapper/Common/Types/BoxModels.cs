using System.Text.Json.Serialization;

namespace Freenaute.Freebox.Mapper.Common.Types;

/// <summary>
///     Represents the model of a Freebox.
/// </summary>
/// <remarks>
///     The model of a Freebox is used to determine the capabilities of the device.
/// </remarks>
/// <seealso href="https://dev.freebox.fr/sdk/os/#get__system__device">
///     Freebox API documentation
/// </seealso>
[JsonConverter(typeof(JsonStringEnumConverter<BoxModels>))]
public enum BoxModels
{
    [JsonStringEnumMemberName("fbxgw-r1/full")] FreeboxServerV6R1,
    [JsonStringEnumMemberName("fbxgw-r2/full")] FreeboxServerV6R2,
    [JsonStringEnumMemberName("fbxgw-r1/mini")] FreeboxMiniR1,
    [JsonStringEnumMemberName("fbxgw-r2/mini")] FreeboxMiniR2,
    [JsonStringEnumMemberName("fbxgw-r1/one")] FreeboxOneR1,
    [JsonStringEnumMemberName("fbxgw-r2/one")] FreeboxOneR2,
    [JsonStringEnumMemberName("fbxgw7-r1/full")] FreeboxV7R1,
    [JsonStringEnumMemberName("fbxgw8-r1/full")] FreeboxV8R1,
    [JsonStringEnumMemberName("fbxgw9-r1/full")] FreeboxV9R1
}

/// <summary>Classifies known discovery model names while preserving the server's original string.</summary>
public static class BoxModelNames
{
    public static BoxModels? Classify(string? value) => value switch
    {
        "fbxgw-r1/full" => BoxModels.FreeboxServerV6R1,
        "fbxgw-r2/full" => BoxModels.FreeboxServerV6R2,
        "fbxgw-r1/mini" => BoxModels.FreeboxMiniR1,
        "fbxgw-r2/mini" => BoxModels.FreeboxMiniR2,
        "fbxgw-r1/one" => BoxModels.FreeboxOneR1,
        "fbxgw-r2/one" => BoxModels.FreeboxOneR2,
        "fbxgw7-r1/full" => BoxModels.FreeboxV7R1,
        "fbxgw8-r1/full" => BoxModels.FreeboxV8R1,
        "fbxgw9-r1/full" or "fbxgw9-r1" => BoxModels.FreeboxV9R1,
        _ => null
    };

    public static string GetWireName(this BoxModels value) => value switch
    {
        BoxModels.FreeboxServerV6R1 => "fbxgw-r1/full",
        BoxModels.FreeboxServerV6R2 => "fbxgw-r2/full",
        BoxModels.FreeboxMiniR1 => "fbxgw-r1/mini",
        BoxModels.FreeboxMiniR2 => "fbxgw-r2/mini",
        BoxModels.FreeboxOneR1 => "fbxgw-r1/one",
        BoxModels.FreeboxOneR2 => "fbxgw-r2/one",
        BoxModels.FreeboxV7R1 => "fbxgw7-r1/full",
        BoxModels.FreeboxV8R1 => "fbxgw8-r1/full",
        BoxModels.FreeboxV9R1 => "fbxgw9-r1/full",
        _ => throw new ArgumentOutOfRangeException(nameof(value))
    };
}
