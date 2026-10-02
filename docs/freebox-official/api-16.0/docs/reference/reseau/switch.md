<a id="switch"></a>

# Switch

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#switch)

## Navigation

- [Switch Errors](#switch-errors)
- [Switch Port Status Object](#switch-port-status-object)
- [Switch Port Configuration Object](#switch-port-configuration-object)
- [Switch Port Stats Object [UNSTABLE]](#switch-port-stats-object-unstable)
- [Switch API](#switch-api)


The Switch API allow you to control the settings of the Freebox
integrated switch.

<a id="switch-errors"></a>

## Switch Errors

When attempting to access the switch API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| bad_port | invalid port number |
| bad_speed | unable to set port speed |
| bad_link | unable to set port link mode |
| bad_mac_entry_type | invalid mac entry type |

<a id="switch-port-status-object"></a>

## Switch Port Status Object

SwitchPortStatus has the following attributes:

<a id="SwitchPortStatus"></a>

### Objet SwitchPortStatus

<a id="SwitchPortStatus.id"></a>

**`id int Read-only`**

switch port id

<a id="SwitchPortStatus.link"></a>

**`link enum Read-only`**

| link | Description |
| --- | --- |
| up | port is up |
| down | port is down |

<a id="SwitchPortStatus.duplex"></a>

**`duplex enum`**

| duplex | Description |
| --- | --- |
| half | force in half duplex mode |
| full | force in full duplex mode |

<a id="SwitchPortStatus.speed"></a>

**`speed enum`**

| duplex | Description |
| --- | --- |
| 10 | 10Base-T |
| 100 | 100Base-TX |
| 1000 | 1000Base-T |

<a id="SwitchPortStatus.mode"></a>

**`mode string Read-only`**

display form of speed and duplex mode

<a id="SwitchPortStatus.mac_list"></a>

**`mac_list [] array of object Read-only`**

list of { mac, name } of hosts connected to this port

<a id="switch-port-configuration-object"></a>

## Switch Port Configuration Object

SwitchPortConfig has the following attributes:

<a id="SwitchPortConfig"></a>

### Objet SwitchPortConfig

<a id="SwitchPortConfig.id"></a>

**`id int Read-only`**

switch port id

<a id="SwitchPortConfig.duplex"></a>

**`duplex enum`**

| duplex | Description |
| --- | --- |
| auto | auto negotiate duplex mode |
| half | force in half duplex mode |
| full | force in full duplex mode |

<a id="SwitchPortConfig.speed"></a>

**`speed enum`**

| duplex | Description |
| --- | --- |
| auto | auto negotiate speed |
| 10 | 10Base-T |
| 100 | 100Base-TX |
| 1000 | 1000Base-T |

<a id="switch-port-stats-object-unstable"></a>

## Switch Port Stats Object [UNSTABLE]

SwitchPortStats has the following attributes:

<a id="SwitchPortStats"></a>

### Objet SwitchPortStats

<a id="SwitchPortStats.rx_bad_bytes"></a>

**`rx_bad_bytes int Read-only`**

<a id="SwitchPortStats.rx_broadcast_packets"></a>

**`rx_broadcast_packets int Read-only`**

<a id="SwitchPortStats.rx_bytes_rate"></a>

**`rx_bytes_rate int Read-only`**

<a id="SwitchPortStats.rx_err_packets"></a>

**`rx_err_packets int Read-only`**

<a id="SwitchPortStats.rx_fcs_packets"></a>

**`rx_fcs_packets int Read-only`**

<a id="SwitchPortStats.rx_fragments_packets"></a>

**`rx_fragments_packets int Read-only`**

<a id="SwitchPortStats.rx_good_bytes"></a>

**`rx_good_bytes int Read-only`**

<a id="SwitchPortStats.rx_good_packets"></a>

**`rx_good_packets int Read-only`**

<a id="SwitchPortStats.rx_jabber_packets"></a>

**`rx_jabber_packets int Read-only`**

<a id="SwitchPortStats.rx_multicast_packets"></a>

**`rx_multicast_packets int Read-only`**

<a id="SwitchPortStats.rx_oversize_packets"></a>

**`rx_oversize_packets int Read-only`**

<a id="SwitchPortStats.rx_packets_rate"></a>

**`rx_packets_rate int Read-only`**

<a id="SwitchPortStats.rx_pause"></a>

**`rx_pause int Read-only`**

<a id="SwitchPortStats.rx_undersize_packets"></a>

**`rx_undersize_packets int Read-only`**

<a id="SwitchPortStats.rx_unicast_packets"></a>

**`rx_unicast_packets int Read-only`**

<a id="SwitchPortStats.tx_broadcast_packets"></a>

**`tx_broadcast_packets int Read-only`**

<a id="SwitchPortStats.tx_bytes"></a>

**`tx_bytes int Read-only`**

<a id="SwitchPortStats.tx_bytes_rate"></a>

**`tx_bytes_rate int Read-only`**

<a id="SwitchPortStats.tx_collisions"></a>

**`tx_collisions int Read-only`**

<a id="SwitchPortStats.tx_deferred"></a>

**`tx_deferred int Read-only`**

<a id="SwitchPortStats.tx_excessive"></a>

**`tx_excessive int Read-only`**

<a id="SwitchPortStats.tx_fcs"></a>

**`tx_fcs int Read-only`**

<a id="SwitchPortStats.tx_late"></a>

**`tx_late int Read-only`**

<a id="SwitchPortStats.tx_multicast_packets"></a>

**`tx_multicast_packets int Read-only`**

<a id="SwitchPortStats.tx_multiple"></a>

**`tx_multiple int Read-only`**

<a id="SwitchPortStats.tx_packets"></a>

**`tx_packets int Read-only`**

<a id="SwitchPortStats.tx_packets_rate"></a>

**`tx_packets_rate int Read-only`**

<a id="SwitchPortStats.tx_pause"></a>

**`tx_pause int Read-only`**

<a id="SwitchPortStats.tx_single"></a>

**`tx_single int Read-only`**

<a id="SwitchPortStats.tx_unicast_packets"></a>

**`tx_unicast_packets int Read-only`**

<a id="switch-api"></a>

## Switch API

<a id="get-the-current-switch-status"></a>

### Get the current switch status

<a id="get--api-v8-switch-status-"></a>

**`GET /api/v8/switch/status/`**

Return the list of swith port status [`SwitchPortStatus`](switch.md#SwitchPortStatus "SwitchPortStatus")

**Example request**:

```http
GET /api/v8/switch/status/ HTTP/1.1
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
    "result": [
        {
            "duplex": "half",
            "link": "down",
            "id": 3,
            "mode": "10BaseT-HD",
            "speed": "10"
        },
        {
            "duplex": "full",
            "link": "up",
            "id": 1,
            "mode": "1000BaseT-FD",
            "speed": "1000"
        },
        {
            "duplex": "half",
            "link": "down",
            "id": 2,
            "mode": "10BaseT-HD",
            "speed": "10"
        },
        {
            "duplex": "full",
            "mac_list": [
                {
                    "mac": "00:24:D4:7E:00:4C",
                    "hostname": "r0ro's player"
                }
            ],
            "link": "up",
            "id": 4,
            "mode": "1000BaseT-FD",
            "speed": "1000"
        }
    ]
}
```

<a id="get-a-port-configuration"></a>

### Get a port configuration

<a id="get--api-v8-switch-port-id"></a>

**`GET /api/v8/switch/port/{id}`**

Get the [`SwitchPortConfig`](switch.md#SwitchPortConfig "SwitchPortConfig") for the given port id

**Example request**:

```http
GET /api/v8/switch/port/1 HTTP/1.1
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
        "id": 1,
        "speed": "auto",
        "duplex": "auto"
    }
}
```

<a id="update-a-port-configuration"></a>

### Update a port configuration

<a id="put--api-v8-switch-port-id"></a>

**`PUT /api/v8/switch/port/{id}`**

Update the [`SwitchPortConfig`](switch.md#SwitchPortConfig "SwitchPortConfig") for the given port id

**Example request**:

```http
PUT /api/v8/switch/port/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "speed": "10"
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
        "id": 4,
        "speed": "10",
        "duplex": "auto"
    }
}
```

<a id="get-a-port-stats"></a>

### Get a port stats

<a id="get--api-v8-switch-port-id-stats"></a>

**`GET /api/v8/switch/port/{id}/stats`**

Get the [`SwitchPortStats`](switch.md#SwitchPortStats "SwitchPortStats") for the given port id

**Example request**:

```http
GET /api/v8/switch/port/4/stats HTTP/1.1
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
        "rx_packets_rate": 4,
        "rx_good_bytes": 20018805,
        "rx_oversize_packets": 0,
        "rx_unicast_packets": 113034,
        "tx_bytes_rate": 736,
        "tx_unicast_packets": 112409,
        "rx_bytes_rate": 608,
        "tx_packets": 166266,
        "tx_collisions": 0,
        "tx_packets_rate": 6,
        "tx_fcs": 0,
        "tx_bytes": 25316860,
        "rx_jabber_packets": 0,
        "tx_single": 0,
        "tx_excessive": 0,
        "rx_pause": 0,
        "rx_multicast_packets": 1217,
        "tx_pause": 0,
        "rx_good_packets": 114296,
        "rx_broadcast_packets": 45,
        "tx_multiple": 0,
        "tx_deferred": 0,
        "tx_late": 0,
        "tx_multicast_packets": 27962,
        "rx_fcs_packets": 0,
        "tx_broadcast_packets": 25895,
        "rx_err_packets": 0,
        "rx_fragments_packets": 0,
        "rx_bad_bytes": 0,
        "rx_undersize_packets": 0
    }
}
```
