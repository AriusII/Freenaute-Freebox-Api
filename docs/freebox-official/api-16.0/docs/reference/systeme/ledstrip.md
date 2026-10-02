<a id="ledstrip"></a>

# Ledstrip

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#ledstrip)

## Navigation

- [Ledstrip errors](#ledstrip-errors)
- [Ledstrip planning object](#ledstrip-planning-object)
- [Ledstrip status object](#ledstrip-status-object)
- [Ledstrip API](#ledstrip-api)


This API allows ledstrip scheduling on boxes that have has_led_strip to true in their [`SystemConfig`](system.md#SystemConfig "SystemConfig") information.

<a id="ledstrip-errors"></a>

## Ledstrip errors

When attempting to access the ledstrip API, you may encounter the following errors

| error_code | Description |
| --- | --- |
| inval | Invalid parameters |

<a id="ledstrip-planning-object"></a>

## Ledstrip planning object

Ledstrip planning object have the following properties:

<a id="LedstripPlanning"></a>

### Objet LedstripPlanning

<a id="LedstripPlanning.use_planning"></a>

**`use_planning bool`**

is the planning enabled

<a id="LedstripPlanning.planning_mode"></a>

**`planning_mode enum`**

current planning mode

| Type | Description |
| --- | --- |
| ledstrip_off | ledstrip disabled |

<a id="LedstripPlanning.resolution"></a>

**`resolution int Read-only`**

planning resolution (number of slots per day)

<a id="LedstripPlanning.mapping"></a>

**`mapping [] array of bool`**

mapping for planning : true or false

mapping[0] is monday at 0:0

mapping[7 \* resolution - 1] is sunday last slot

(each slot has a duration of 60 \* 24 / resolution minutes)

The boolean value indicates whether the planning is in effect (i.e: ledstrip disabled)

<a id="ledstrip-status-object"></a>

## Ledstrip status object

Ledstrip status object has the following properties:

<a id="LedstripStatus"></a>

### Objet LedstripStatus

<a id="LedstripStatus.use_planning"></a>

**`use_planning bool Read-only`**

is the planning enabled

<a id="LedstripStatus.next_change"></a>

**`next_change timestamp Read-only`**

timestamp of the scheduled next change, according to planning

<a id="ledstrip-api"></a>

## Ledstrip API

<a id="get-ledstrip-status"></a>

### Get ledstrip status

<a id="get--api-v16-ledstrip-status"></a>

**`GET /api/v16/ledstrip/status`**

Returns the `Ledstrip status object`

**Example request**:

```http
GET /api/v16/ledstrip/status HTTP/1.1
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
    "next_change": 1651135474996,
  }
}
```

<a id="get-ledstrip-planning"></a>

### Get ledstrip planning

Get the [`LedstripPlanning`](ledstrip.md#LedstripPlanning "LedstripPlanning")

**Example request**:

```http
GET /api/v16/ledstrip/planning/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```javascript
{
  "success": true,
  "result": {
    "use_planning": false,
    "planning_mode": "ledstrip_off",
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

<a id="update-ledstrip-planning"></a>

### Update ledstrip planning

<a id="put--api-v16-ledstrip-planning"></a>

**`PUT /api/v16/ledstrip/planning`**

**Example request**:

```http
PUT /api/v16/ledstrip/planning/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```javascript
{
  "use_planning": true,
  "planning_mode": "ledstrip_off",
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

```javascript
{
  "success": true,
  "result": {
    "use_planning": false,
    "planning_mode": "ledstrip_off",
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
