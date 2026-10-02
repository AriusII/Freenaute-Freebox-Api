<a id="system-438"></a>

# System

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#system-438)

## Navigation

- [System Config](#system-config)
- [System API](#system-api)


<a id="system-config"></a>

## System Config

SystemConfig has the following attributes:

<a id="SystemConfig"></a>

### Objet SystemConfig

<a id="SystemConfig.firmware_version"></a>

**`firmware_version string Read-only`**

freebox firmware version

<a id="SystemConfig.mac"></a>

**`mac string Read-only`**

freebox mac address

<a id="SystemConfig.serial"></a>

**`serial string Read-only`**

freebox serial number

<a id="SystemConfig.uptime"></a>

**`uptime string Read-only`**

readable freebox uptime

<a id="SystemConfig.uptime_val"></a>

**`uptime_val int Read-only`**

freebox uptime (in seconds)

<a id="SystemConfig.board_name"></a>

**`board_name string Read-only`**

freebox hardware revision

<a id="SystemConfig.box_authenticated"></a>

**`box_authenticated bool Read-only`**

is the box authenticated (“étape 6”)

<a id="SystemConfig.disk_status"></a>

**`disk_status enum Read-only`**

the internal disk status

| Value | Description |
| --- | --- |
| not_detected | The disk as not been detected |
| disabled | The disk is disabled |
| initializing | The disk is initializing |
| error | The disk failed to mount |
| active | The disk is ready |

<a id="SystemConfig.usb3_enable"></a>

**`usb3_enable bool`**

enable USB3 (on supported platforms)

<a id="SystemConfig.user_main_storage"></a>

**`user_main_storage string`**

The label of the storage partition to use
for user data. (Matches the label of the [`DiskPartition`](../stockage-vm/storage.md#DiskPartition "DiskPartition"))
In case of ‘light’ box flavor, it must be set by to
a permanently attached external storage

<a id="SystemConfig.user_storage_powered"></a>

**`user_storage_powered bool Read-only`**

Indicate whether the user storage is powered or not

<a id="SystemConfig.expansions"></a>

**`expansions [] array of SystemConfigSensor Read-only`**

List of thermal sensors on the system

<a id="SystemConfig.model_info"></a>

**`model_info SystemModelInfo Read-only`**

Device informations

<a id="SystemConfig.fans"></a>

**`fans [] array of SystemConfigFan Read-only`**

List of fans on the system

<a id="propriete-sans-ancre-system-15"></a>

**`expansions [] array of SystemConfigExpansion Read-only`**

List of expansions slots modules

<a id="SystemModelInfo"></a>

### Objet SystemModelInfo

<a id="SystemModelInfo.name"></a>

**`name enum Read-only`**

| name | Description |
| --- | --- |
| fbxgw-r1/full | Freebox Server (v6) revision 1 |
| fbxgw-r2/full | Freebox Server (v6) revision 2 |
| fbxgw-r1/mini | Freebox Mini revision 1 |
| fbxgw-r2/mini | Freebox Mini revision 2 |
| fbxgw-r1/one | Freebox One revision 1 |
| fbxgw-r2/one | Freebox One revision 2 |
| fbxgw7-r1/full | Freebox v7 revision 1 |
| fbxgw8-r1/full | Freebox v8 revision 1 |
| fbxgw9-r1/full | Freebox v9 revision 1 |

<a id="SystemModelInfo.pretty_name"></a>

**`pretty_name string Read-only`**

Display name for the box model

<a id="SystemModelInfo.has_expansions"></a>

**`has_expansions bool Read-only`**

if present and true, the box has expansions

<a id="SystemModelInfo.has_lan_sfp"></a>

**`has_lan_sfp bool Read-only`**

if present and true, the box has an SFP port for lan

<a id="SystemModelInfo.has_dect"></a>

**`has_dect bool Read-only`**

if present and true, the box has a DECT base station

<a id="SystemModelInfo.has_home_automation"></a>

**`has_home_automation bool Read-only`**

if present and true, the box has a Home automation module

<a id="SystemModelInfo.has_femtocell_exp"></a>

**`has_femtocell_exp bool Read-only`**

if present and true, the box has a femtocell expansion slot

<a id="SystemModelInfo.has_fixed_femtocell"></a>

**`has_fixed_femtocell bool Read-only`**

if present and true, the box has an internal femtocell

<a id="SystemModelInfo.has_vm"></a>

**`has_vm bool Read-only`**

if present and true, the box supports virtual machines

<a id="SystemModelInfo.has_dsl"></a>

**`has_dsl bool Read-only`**

if present and true, the box supports DSL

<a id="SystemModelInfo.has_standby"></a>

**`has_standby bool Read-only`**

if present and true, the box supports standby

<a id="SystemModelInfo.has_eco_wifi"></a>

**`has_eco_wifi bool Read-only`**

if present and true, the box supports Eco-WiFi

<a id="SystemModelInfo.has_wop"></a>

**`has_wop bool Read-only`**

if present and true, the box supports Wake-On-PON

<a id="SystemModelInfo.has_led_strip"></a>

**`has_led_strip bool Read-only`**

if present and true, the box has a LED strip

<a id="SystemModelInfo.has_status_led"></a>

**`has_status_led bool Read-only`**

if present and true, the box has a status LED

<a id="SystemModelInfo.has_usb3_enable"></a>

**`has_usb3_enable bool Read-only`**

if present and true, the box supports disabling USB3

<a id="SystemModelInfo.has_lcd_screensaver"></a>

**`has_lcd_screensaver [ro] Optionnal`**

if present and true, the box supports enabling a screensaver animation on its LCD display

<a id="SystemConfigSensor"></a>

### Objet SystemConfigSensor

<a id="SystemConfigSensor.id"></a>

**`id string Read-only`**

sensor id

<a id="SystemConfigSensor.name"></a>

**`name string Read-only`**

sensor display name

<a id="SystemConfigSensor.value"></a>

**`value int Read-only`**

sensor current value (in celsius degree)

<a id="SystemConfigFan"></a>

### Objet SystemConfigFan

<a id="SystemConfigFan.id"></a>

**`id string Read-only`**

fan id

<a id="SystemConfigFan.name"></a>

**`name string Read-only`**

fan display name

<a id="SystemConfigFan.value"></a>

**`value int Read-only`**

fan current speed (RPM)

<a id="SystemConfigExpansion"></a>

### Objet SystemConfigExpansion

<a id="SystemConfigExpansion.slot"></a>

**`slot int Read-only`**

expansion slot id

<a id="SystemConfigExpansion.probe_done"></a>

**`probe_done bool Read-only`**

has the module presence been probed yet

<a id="SystemConfigExpansion.present"></a>

**`present bool Read-only`**

has an expansion module been detected in the slot

<a id="SystemConfigExpansion.supported"></a>

**`supported bool Read-only`**

is the module supported in this slot

<a id="SystemConfigExpansion.bundle"></a>

**`bundle string Read-only`**

module serial number

<a id="SystemConfigExpansion.type"></a>

**`type enum Read-only`**

module type

| Value | Description |
| --- | --- |
| unknown | unknown module |
| dsl_lte | xDSL + LTE |
| dsl_lte_external_antennas | xDSL + LTE with external antennas switch |
| ftth_p2p | FTTH P2P |
| ftth_pon | FTTH PON |
| security | Security module |

<a id="system-api"></a>

## System API

<a id="get-the-current-system-info-unstable"></a>

### Get the current system info [UNSTABLE]

<a id="current-version-api-v6"></a>

#### Current version (api >= v6)

<a id="get--api-v8-system-"></a>

**`GET /api/v8/system/`**

Get the [`SystemConfig`](system.md#SystemConfig "SystemConfig")

**Example request**:

```http
GET /api/v8/system/ HTTP/1.1
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
        "mac": "34:27:92:60:0B:9E",
        "sensors": [{
                        "id": "t2",
                        "name": "Température 2",
                        "value": 47
                },
                {
                        "id": "t1",
                        "name": "Température 1",
                        "value": 45
                },
                {
                        "id": "t3",
                        "name": "Température 3",
                        "value": 42
                },
                {
                        "id": "cpu_cp_slave",
                        "name": "Température CPU CP Slave",
                        "value": 72
                },
                {
                        "id": "cpu_cp_master",
                        "name": "Température CPU CP Master",
                        "value": 72
                },
                {
                        "id": "cpu_ap",
                        "name": "Température CPU",
                        "value": 64
                }
        ],
        "model_info": {
                "pretty_name": "Freebox v7 (r1)",
                "has_expansions": true,
                "name": "fbxgw7-r1/full",
                "has_lan_sfp": true,
                "has_dect": true,
                "internal_hdd_size": 0,
                "has_home_automation": true,
                "wifi_type": "2d4_5g_5g"
        },
        "fans": [{
                        "id": "secondary-fan",
                        "name": "Ventilateur 2",
                        "value": 1725
                },
                {
                        "id": "main",
                        "name": "Ventilateur 1",
                        "value": 1739
                }
        ],
        "expansions": [{
                        "type": "security",
                        "present": true,
                        "slot": 1,
                        "probe_done": true,
                        "supported": true,
                        "bundle": "985700J183900112"
                },
                {
                        "type": "ftth_p2p",
                        "present": true,
                        "slot": 2,
                        "probe_done": true,
                        "supported": true,
                        "bundle": "959300V181500003"
                }
        ],
        "box_authenticated": true,
        "disk_status": "active",
        "uptime": "2 heures 11 minutes 32 secondes",
        "uptime_val": 7892,
        "user_main_storage": "Disque 1",
        "board_name": "fbxgw7r",
        "serial": "957601J183400107",
        "firmware_version": "6.6.6"
    }
}
```

<a id="reboot-the-system"></a>

### Reboot the system

<a id="post--api-v8-system-reboot-"></a>

**`POST /api/v8/system/reboot/`**

Reboot the Freebox

**Example request**:

```http
POST /api/v8/system/reboot/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true
}
```

<a id="shutdown-the-system"></a>

### Shutdown the system

<a id="post--api-v11-system-shutdown-"></a>

**`POST /api/v11/system/shutdown/`**

Shutdown the Freebox

**Example request**:

```http
POST /api/v11/system/shutdown/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true
}
```
