<a id="freeplug"></a>

# Freeplug

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#freeplug)

## Navigation

- [Freeplug Errors](#freeplug-errors)
- [Freeplug Network](#freeplug-network)
- [Freeplug Object](#freeplug-object)
- [Freeplug API](#freeplug-api)


The freeplug API allow you to list the freeplugs on the Freebox
network and get stats

<a id="freeplug-errors"></a>

## Freeplug Errors

When attempting to access the freeplug API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| inval | Invalid request |
| nomem | Internal error |
| nosta | No freeplug with this id |
| nopeer | No freeplug with this id |

<a id="freeplug-network"></a>

## Freeplug Network

FreeplugNetwork has the following attributes:

<a id="FreeplugNetwork"></a>

### Objet FreeplugNetwork

<a id="FreeplugNetwork.id"></a>

**`id string Read-only`**

Network unique id

<a id="FreeplugNetwork.members"></a>

**`members [] array of Freeplug Read-only`**

List of freeplugs member of this network

<a id="freeplug-object"></a>

## Freeplug Object

Freeplug has the following attributes:

<a id="Freeplug"></a>

### Objet Freeplug

<a id="Freeplug.id"></a>

**`id string Read-only`**

Freeplug unique id

<a id="Freeplug.local"></a>

**`local bool Read-only`**

if true the Freeplug is connected directly to the Freebox

<a id="Freeplug.net_role"></a>

**`net_role enum Read-only`**

Freeplug network role

| Type | Description |
| --- | --- |
| sta | Freeplug Station |
| pco | Freeplug proxy coordinator |
| cco | Central coordinator |

<a id="Freeplug.model"></a>

**`model string Read-only`**

Freebox Server netbios name

<a id="Freeplug.eth_port_status"></a>

**`eth_port_status enum Read-only`**

| Type | Description |
| --- | --- |
| up | The ethernet port is up |
| down | The ethernet port is down |
| unknown | The ethernet port state is unknown |

<a id="Freeplug.eth_full_duplex"></a>

**`eth_full_duplex bool Read-only`**

ethernet link is full duplex

<a id="Freeplug.has_network"></a>

**`has_network bool Read-only`**

is connected to the network

<a id="Freeplug.eth_speed"></a>

**`eth_speed int Read-only`**

ethernet port speed

<a id="Freeplug.inactive"></a>

**`inactive int Read-only`**

seconds since last activity

<a id="Freeplug.net_id"></a>

**`net_id string Read-only`**

network id

<a id="Freeplug.rx_rate"></a>

**`rx_rate int Read-only`**

rx rate (from the freeplugs to the “cco” freeplug) (in Mb/s)
-1 if not available

<a id="Freeplug.tx_rate"></a>

**`tx_rate int Read-only`**

tx rate (from the “cco” freeplug to the freeplugs) (in Mb/s)
-1 if not available

<a id="freeplug-api"></a>

## Freeplug API

<a id="get-the-current-freeplugs-networks"></a>

### Get the current Freeplugs networks

<a id="get--api-v8-freeplug-"></a>

**`GET /api/v8/freeplug/`**

Returns the list of [`FreeplugNetwork`](freeplug.md#FreeplugNetwork "FreeplugNetwork")

**Example request**:

```http
GET /api/v8/freeplug/ HTTP/1.1
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
            "id": "c8:f7:b9:83:f5:10:01",
            "members": [
                {
                    "id": "00:24:D4:36:4C:CF",
                    "tx_rate": 148,
                    "eth_port_status": "up",
                    "rx_rate": 148,
                    "net_role": "sta",
                    "inactive": 1,
                    "net_id": "c8:f7:b9:83:f5:10:01",
                    "model": "int6400",
                    "eth_speed": 100,
                    "local": true,
                    "eth_full_duplex": true,
                    "has_network": true
                },
                {
                    "id": "F4:CA:E5:1D:46:AE",
                    "tx_rate": 149,
                    "eth_port_status": "up",
                    "rx_rate": 148,
                    "net_role": "sta",
                    "inactive": 1,
                    "net_id": "c8:f7:b9:83:f5:10:01",
                    "model": "int6400",
                    "eth_speed": 100,
                    "local": true,
                    "eth_full_duplex": true,
                    "has_network": true
                },
                {
                    "id": "00:24:D4:1B:15:D0",
                    "tx_rate": -1,
                    "eth_port_status": "up",
                    "rx_rate": -1,
                    "net_role": "cco",
                    "inactive": 1,
                    "net_id": "c8:f7:b9:83:f5:10:01",
                    "model": "int6400",
                    "eth_speed": 100,
                    "local": false,
                    "eth_full_duplex": true,
                    "has_network": true
                }
            ]
        }
    ]
}
```

<a id="get-a-particular-freeplug-information"></a>

### Get a particular Freeplug information

<a id="get--api-v8-freeplug-id-"></a>

**`GET /api/v8/freeplug/{id}/`**

Returns the list of [`Freeplug`](freeplug.md#Freeplug "Freeplug")

**Example request**:

```http
GET /api/v8/freeplug/F4:CA:E5:1D:46:AE/ HTTP/1.1
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
        "id": "00:24:D4:36:4C:CF",
        "tx_rate": -1,
        "eth_port_status": "up",
        "rx_rate": -1,
        "net_role": "sta",
        "inactive": 1,
        "net_id": "c8:f7:b9:83:f5:10:01",
        "model": "int6400",
        "eth_speed": 100,
        "local": true,
        "eth_full_duplex": true,
        "has_network": true
    }
}
```

<a id="reset-a-freeplug"></a>

### Reset a Freeplug

<a id="post--api-v8-freeplug-id-reset-"></a>

**`POST /api/v8/freeplug/{id}/reset/`**

reset the given [`Freeplug`](freeplug.md#Freeplug "Freeplug")

**Example request**:

```http
POST /api/v8/freeplug/F4:CA:E5:1D:46:AE/reset/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success":true,
}
```
