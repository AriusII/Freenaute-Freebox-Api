using System.Text.Json;
using System.Text.Json.Serialization;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

namespace Freenaute.Freebox.Mapper.Contracts.Network;

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#SfpConfig.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class SfpConfig
{
    /// <summary>Indicate whether the SFP type is forced</summary>
    [JsonPropertyName("sfp_type_forced")]
    public bool? SfpTypeForced { get; init; }

    /// <summary>What SFP type is forced (valid only when sfp_type_forced is true). Valid values are provided in available_sfp_types</summary>
    [JsonPropertyName("sfp_type_forced_value")]
    public string? SfpTypeForcedValue { get; init; }

    /// <summary>array containing what SFP types can be configured on the LAN SFP port. Possible values are listed in the following table: Type Description p2p_1g 1000BASE-X p2p_2d5g_no_aneg 2500BASE-X p2p_10g 10GBASE-R copper_1g 1000BASE-T copper_sgmii_1g SGMII copper_sgmii_10g USXGMII</summary>
    [JsonPropertyName("available_sfp_types")]
    public string[]? AvailableSfpTypes { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#SfpStatus.
/// Missing read values are represented by nullable properties; this does not assert server acceptance of JSON null.
/// </summary>
public sealed class SfpStatus
{
    /// <summary>Indicates whether an SFP module present in the port</summary>
    [JsonPropertyName("present")]
    public bool? Present { get; init; }

    /// <summary>Indicates whether the SFP module has a valid EEPROM</summary>
    [JsonPropertyName("eeprom_valid")]
    public bool? EepromValid { get; init; }

    /// <summary>Indicates whether the SFP module is supported</summary>
    [JsonPropertyName("supported")]
    public bool? Supported { get; init; }

    /// <summary>SFP type read from EEPROM</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>SFP port is powered</summary>
    [JsonPropertyName("power_good")]
    public bool? PowerGood { get; init; }

    /// <summary>link status</summary>
    [JsonPropertyName("link")]
    public bool? Link { get; init; }

    /// <summary>vendor name</summary>
    [JsonPropertyName("vendor_name")]
    public string? VendorName { get; init; }

    /// <summary>part number</summary>
    [JsonPropertyName("part_number")]
    public string? PartNumber { get; init; }

    /// <summary>hardware revision</summary>
    [JsonPropertyName("hardware_rev")]
    public string? HardwareRev { get; init; }

    /// <summary>serial number</summary>
    [JsonPropertyName("serial_number")]
    public string? SerialNumber { get; init; }

}

/// <summary>Wire contract documented at http://mafreebox.freebox.fr/doc/index.html#SfpConfig.
/// Unset values are omitted. Explicit false and zero remain present. JSON null is not used as a patch operation.
/// </summary>
public sealed class SfpConfigurationPatch
{
    /// <summary>Indicate whether the SFP type is forced</summary>
    [JsonPropertyName("sfp_type_forced")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<bool>))]
    public Optional<bool> SfpTypeForced { get; init; }

    /// <summary>What SFP type is forced (valid only when sfp_type_forced is true). Valid values are provided in available_sfp_types</summary>
    [JsonPropertyName("sfp_type_forced_value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonConverter(typeof(OptionalJsonConverter<string>))]
    public Optional<string> SfpTypeForcedValue { get; init; }

}
