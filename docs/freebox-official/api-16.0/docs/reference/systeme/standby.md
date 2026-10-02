<a id="standby-743"></a>

<a id="standby-api"></a>

# Standby

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#standby-743)

## Navigation

- [Standby Errors](#standby-errors)
- [Standby config object](#standby-config-object)
- [Standby status object](#standby-status-object)
- [Standby API](#id1-747)


The Standby API allows you to configure Wi-Fi schedule. On boxes that have has_standby set to true in their [`SystemConfig`](system.md#SystemConfig "SystemConfig") information, it is possible to configure box standby and wake-up.

<a id="standby-errors"></a>

## Standby Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| inval | invalid parameters |

<a id="standby-config-object"></a>

## Standby config object

Standby config object have the following properties:

<a id="StandbyConfig"></a>

### Objet StandbyConfig

<a id="StandbyConfig.use_planning"></a>

**`use_planning bool`**

is the planning enabled

<a id="StandbyConfig.planning_mode"></a>

**`planning_mode enum`**

current planning mode

| Type | Description |
| --- | --- |
| wifi_off | Wi-Fi disabled |
| standby | Freebox standby |

<a id="StandbyConfig.resolution"></a>

**`resolution int Read-only`**

planning resolution (number of slots per day)

<a id="StandbyConfig.mapping"></a>

**`mapping [] array of bool`**

mapping for planning : true or false

mapping[0] is monday at 0:0

mapping[7 \* resolution - 1] is sunday last slot

(each slot has a duration of 60 \* 24 / resolution minutes)

The boolean value indicates whether the planning is in effect (i.e: Wi-Fi disabled, or box standing by)

<a id="standby-status-object"></a>

## Standby status object

Standby status object have the following properties:

<a id="StandbyStatus"></a>

### Objet StandbyStatus

<a id="StandbyStatus.use_planning"></a>

**`use_planning bool Read-only`**

is the planning enabled

<a id="StandbyStatus.planning_mode"></a>

**`planning_mode enum Read-only`**

Type of planning that is configured, just like in [`StandbyConfig`](standby.md#StandbyConfig "StandbyConfig")

<a id="StandbyStatus.next_change"></a>

**`next_change timestamp Read-only`**

timestamp of the scheduled next change, according to planning

<a id="StandbyStatus.available_planning_modes"></a>

**`available_planning_modes array Read-only`**

array of available planning modes. Individual array elements are enum
values just like planning_mode in [`StandbyConfig`](standby.md#StandbyConfig "StandbyConfig")

<a id="id1-747"></a>

## Standby API

<a id="get-standby-status"></a>

### Get standby status

<a id="get--api-v11-standby-status"></a>

**`GET /api/v11/standby/status`**

Returns the `Standby status object`

**Example request**:

```http
GET /api/v11/standby/status HTTP/1.1
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
    "use_planning": true,
    "planning_mode": "standby",
    "next_change": 1651135474996,
    "available_planning_modes": [ "wifi_off", "standby" ]
  }
}
```

<a id="get-standby-config"></a>

### Get standby config

Get the [`StandbyConfig`](standby.md#StandbyConfig "StandbyConfig")

**Example request**:

```http
GET /api/v11/standby/config/ HTTP/1.1
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
    "use_planning": false,
    "planning_mode": "suspend",
    "mapping": [
      false,
      false,
      false,
      false,

      [ ... ]

      false,
      false,
      false,
      false
    ],
    "resolution": 48
  }
}
```

<a id="update-standby-config"></a>

### Update standby config

<a id="put--api-v11-standby-config"></a>

**`PUT /api/v11/standby/config`**

**Example request**:

```http
PUT /api/v11/standby/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "use_planning": true,
  "planning_mode": "suspend",
  "mapping": [
    false,
    false,
    false,
    false,

    [ ... ],

    false,
    false,
    false,
    false
  ],
  "resolution": 48
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
    "use_planning": false,
    "planning_mode": "suspend",
    "mapping": [
      false,
      false,
      false,
      false,
      false,

      [ ... ]

      false,
      false,
      false,
      false
    ],
    "resolution": 48
  }
}
```
