<a id="sfp-788"></a>

<a id="sfp-api"></a>

# SFP

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#sfp-788)

## Navigation

- [SFP Errors](#sfp-errors)
- [SFP config object](#sfp-config-object)
- [SFP status object](#sfp-status-object)
- [SFP API](#id1-792)


On boxes that have has_lan_sfp set to true in their [`SystemConfig`](../systeme/system.md#SystemConfig "SystemConfig") information, it is possible to configure the LAN SFP port.

<a id="sfp-errors"></a>

## SFP Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| inval | invalid parameters |
| noent | invalid id |
| internal | system internal error |

<a id="sfp-config-object"></a>

## SFP config object

SFP config object has the following properties:

<a id="SfpConfig"></a>

### Objet SfpConfig

<a id="SfpConfig.sfp_type_forced"></a>

**`sfp_type_forced bool`**

Indicate whether the SFP type is forced

<a id="SfpConfig.sfp_type_forced_value"></a>

**`sfp_type_forced_value enum`**

What SFP type is forced (valid only when sfp_type_forced
is true). Valid values are provided in available_sfp_types

<a id="SfpConfig.available_sfp_types"></a>

**`available_sfp_types [] array of enum Read-only`**

array containing what SFP types can be configured on the LAN SFP port.
Possible values are listed in the following table:

| Type | Description |
| --- | --- |
| p2p_1g | 1000BASE-X |
| p2p_2d5g_no_aneg | 2500BASE-X |
| p2p_10g | 10GBASE-R |
| copper_1g | 1000BASE-T |
| copper_sgmii_1g | SGMII |
| copper_sgmii_10g | USXGMII |

<a id="sfp-status-object"></a>

## SFP status object

SFP status object has the following properties:

<a id="SfpStatus"></a>

### Objet SfpStatus

<a id="SfpStatus.present"></a>

**`present bool Read-only`**

Indicates whether an SFP module present in the port

<a id="SfpStatus.eeprom_valid"></a>

**`eeprom_valid bool Read-only`**

Indicates whether the SFP module has a valid EEPROM

<a id="SfpStatus.supported"></a>

**`supported bool Read-only`**

Indicates whether the SFP module is supported

<a id="SfpStatus.type"></a>

**`type enum Read-only`**

SFP type read from EEPROM

<a id="SfpStatus.power_good"></a>

**`power_good bool Read-only`**

SFP port is powered

<a id="SfpStatus.link"></a>

**`link bool Read-only`**

link status

<a id="SfpStatus.vendor_name"></a>

**`vendor_name string Read-only`**

vendor name

<a id="SfpStatus.part_number"></a>

**`part_number string Read-only`**

part number

<a id="SfpStatus.hardware_rev"></a>

**`hardware_rev string Read-only`**

hardware revision

<a id="SfpStatus.serial_number"></a>

**`serial_number string Read-only`**

serial number

<a id="id1-792"></a>

## SFP API

<a id="get-sfp-status"></a>

### Get SFP status

<a id="get--api-v11-sfp-status"></a>

**`GET /api/v11/sfp/status`**

Returns the `SFP status object`

**Example request**:

```http
GET /api/v11/sfp/status HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "success": true,
  "result": {
    "type": "copper_1g",
    "present": true,
    "link": true,
    "supported": true,
    "vendor_name": "SFP Vendor",
    "serial_number": "1122334455",
    "part_number": "SFP-V-Part-01R",
    "power_good": true,
    "hardware_rev": "A",
    "eeprom_valid": true
  }
}
```

<a id="get-sfp-config"></a>

### Get SFP config

Get the [`SfpConfig`](sfp.md#SfpConfig "SfpConfig")

**Example request**:

```http
GET /api/v11/sfp/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "success": true,
  "result": {
    "sfp_type_forced": false,
    "sfp_type_forced_value": "",
    "available_sfp_types": [
      "p2p_1g",
      "p2p_10g",
      "copper_1g",
      "copper_sgmii_1g",
      "copper_usxgmii_10g"
    ]
  }
}
```

<a id="update-sfp-config"></a>

### Update SFP config

<a id="put--api-v11-sfp-config"></a>

**`PUT /api/v11/sfp/config`**

**Example request**:

```http
PUT /api/v11/sfp/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "sfp_type_forced": true,
  "sfp_type_forced_value": "copper_1g"
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "success": true,
  "result": {
    "sfp_type_forced_value": "copper_1g",
    "sfp_type_forced": true,
    "available_sfp_types": [
      "p2p_1g",
      "p2p_10g",
      "copper_1g",
      "copper_sgmii_1g",
      "copper_usxgmii_10g"
    ]
  }
}
```
