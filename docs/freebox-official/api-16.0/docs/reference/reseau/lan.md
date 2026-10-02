<a id="lan"></a>

# Lan

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#lan)

## Navigation

- [Lan Errors](#lan-errors)
- [Lan Config](#lan-config)
- [Route](#route)
- [Lan Config API](#lan-config-api)
- [Routing Config API](#routing-config-api)
- [Errors](#errors)
- [Lan Browser API](#lan-browser-api)
- [Wake on LAN](#wake-on-lan)


With the LAN API you get information and modify the Freebox Server
network configuration.

<a id="lan-errors"></a>

## Lan Errors

When attempting to access the LAN API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| noent | Invalid id |
| internal_error | Internal error |
| ioerror | Internal error |
| inval | Invalid parameter |
| invalid_gateway_ip | Invalid Gateway IP |
| invalid_route | Invalid static route |
| exists | Duplicate route prefix |

<a id="lan-config"></a>

## Lan Config

Lan config has the following attributes:

<a id="LanConfig"></a>

### Objet LanConfig

<a id="LanConfig.ip"></a>

**`ip string`**

Freebox Server IPv4 address

<a id="LanConfig.name"></a>

**`name string`**

Freebox Server name

<a id="LanConfig.name_dns"></a>

**`name_dns string`**

Freebox Server DNS name

<a id="LanConfig.name_mdns"></a>

**`name_mdns string`**

Freebox Server mDNS name

<a id="LanConfig.name_netbios"></a>

**`name_netbios string`**

Freebox Server netbios name

<a id="LanConfig.type"></a>

**`type enum`**

The valid LAN modes are:

| Type | Description |
| --- | --- |
| router | The Freebox acts as a network router |
| bridge | The Freebox acts as a network bridge |

NOTE: in bridge mode, most of Freebox services are disabled. It
is recommended to use the router mode, and third party apps
should not change this setting

<a id="route"></a>

## Route

A route has the following attributes:

<a id="Route"></a>

### Objet Route

<a id="Route.prefix"></a>

**`prefix string`**

Destination network IPv4 prefix in CIDR format (e.g. 192.168.1.0/24).

A prefix is considered invalid if it is a subprefix of any reserved network listed below.

| Network | Description |
| --- | --- |
| 127.0.0.0/8 | Loopback network |
| 169.254.0.0/16 | Link-local addresses |
| 224.0.0.0/4 | IANA: multicast |
| 192.168.27.0/24 | Used for VPN and guest WIFI addresses |

Only one *enabled* route may exist for a given prefix. An `exists` error will be returned
if multiple active routes share the same prefix.

<a id="Route.gateway"></a>

**`gateway string`**

IP address of the next-hop gateway.

<a id="Route.enabled"></a>

**`enabled bool`**

If false the route is not added to the routing table.

<a id="Route.description"></a>

**`description string`**

Optional text describing the route.

<a id="lan-config-api"></a>

## Lan Config API

<a id="get-the-current-lan-configuration"></a>

### Get the current Lan configuration

<a id="get--api-v8-lan-config-"></a>

**`GET /api/v8/lan/config/`**

Returns the current [`LanConfig`](lan.md#LanConfig "LanConfig")

**Example request**:

```http
GET /api/v8/lan/config/ HTTP/1.1
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
        "name_dns": "freebox-r0ro",
        "name_mdns": "Freebox-r0ro",
        "name": "Freebox r0ro",
        "mode": "router",
        "name_netbios": "Freebox_r0ro",
        "ip": "192.168.1.254"
    }
}
```

<a id="update-the-current-lan-configuration"></a>

### Update the current Lan configuration

<a id="put--api-v8-lan-config-"></a>

**`PUT /api/v8/lan/config/`**

Update the current [`LanConfig`](lan.md#LanConfig "LanConfig")

**Example request**:

```http
PUT /api/v8/lan/config/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "mode":"router",
   "ip":"192.168.69.254",
   "name":"Freebox de r0ro",
   "name_dns":"freebox-de-r0ro",
   "name_mdns":"Freebox-de-r0ro",
   "name_netbios":"Freebox_de_r0ro"
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success":true,
   "result": {
      "name_dns":"freebox-de-r0ro",
      "name_mdns":"Freebox-de-r0ro",
      "name":"Freebox de r0ro",
      "mode":"router",
      "name_netbios":"Freebox_de_r0ro",
      "ip":"192.168.69.254"
   }
}
```

<a id="routing-config-api"></a>

## Routing Config API

<a id="get-the-current-routing-configuration"></a>

### Get the current routing configuration

<a id="get--api-v16-lan-routes"></a>

**`GET /api/v16/lan/routes`**

Returns the current list of [`Route`](lan.md#Route "Route") objects

**Example request**:

```http
GET /api/v16/lan/routes/ HTTP/1.1
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
      "prefix": "192.168.42.0/24",
      "gateway": "192.168.1.38",
      "enabled": true,
      "description": "My first route"
    },
    {
      "prefix": "192.168.24.240/28",
      "gateway": "192.168.1.38",
      "enabled": false,
      "description": ""
    }
  ]
}
```

<a id="update-the-current-routing-configuration"></a>

### Update the current routing configuration

<a id="put--api-v16-lan-routes-"></a>

**`PUT /api/v16/lan/routes/`**

Update the current list of [`Route`](lan.md#Route "Route") objects

**Example request**:

```http
PUT /api/v16/lan/routes/ HTTP/1.1
Host: mafreebox.freebox.fr

[
  {
    "prefix": "192.168.42.0/24",
    "gateway": "192.168.1.38",
    "enabled": true,
    "description": "My first and only route"
  },
]
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
      "prefix": "192.168.42.0/24",
      "gateway": "192.168.1.38",
      "enabled": true,
      "description": "My first and only route"
    },
  ]
}
```

<a id="lan-browser-232"></a>

# Lan Browser

With the LAN browser API you get information on hosts on the Freebox
Server local network.

<a id="errors"></a>

## Errors

When attempting to access the LAN browser API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| inval | Invalid parameter |
| nodev | Invalid interface |
| nohost | Invalid host id |
| nomem | Internal error |
| netdown | Network is down |

<a id="lan-browser-api"></a>

## Lan Browser API

Lan browser API allow you to discover hosts on the local network

<a id="getting-the-list-of-browsable-lan-interfaces"></a>

### Getting the list of browsable LAN interfaces

<a id="get--api-v8-lan-browser-interfaces-"></a>

**`GET /api/v8/lan/browser/interfaces/`**

**Example request**:

```http
GET /api/v8/lan/browser/interfaces/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   success: true,
   result: [
      {
         name: "pub",
         host_count: 3
      }
   ]
}
```

<a id="lan-host-object"></a>

<a id="id1"></a>

### Lan Host object

Lan Host has the following attributes:

<a id="LanHost"></a>

#### Objet LanHost

<a id="LanHost.id"></a>

**`id string Read-only`**

Host id (unique on this interface)

<a id="LanHost.primary_name"></a>

**`primary_name string`**

Host primary name (chosen from the list of available names, or
manually set by user)

<a id="LanHost.domain_name"></a>

**`domain_name string`**

Host domain name on the local network (manually set by user,
or automatically configured during device registration).

The string must respect the following rules:

- Must end with ‘.home’
- 63 characters long at max
- Only alphabetical characters are accepted
- Digits are accepted provided they are not placed at the beginning of the string, nor after another dot character.
- Hyphens and dots are accepted provided they are not placed at the beginning or the end of the string, nor after or before another dot character.

It is also possible to use an empty string. This special value
means no local domain should be registered for this host.

<a id="LanHost.host_type"></a>

**`host_type enum`**

When possible, the Freebox will try to guess the host_type, but
you can manually override this to the correct value

Possible values are:

| source | Description |
| --- | --- |
| workstation | Workstation |
| laptop | Laptop |
| smartphone | Smartphone |
| tablet | Tablet |
| printer | Printer |
| vg_console | Video game console |
| television | TV |
| nas | Nas |
| ip_camera | IP Camera |
| ip_phone | IP Phone |
| freebox_player | Freebox Player |
| freebox_hd | Freebox HD |
| freebox_crystal | Freebox Crystal |
| freebox_mini | Freebox Mini 4k |
| freebox_delta | Freebox Delta |
| freebox_one | Freebox One |
| freebox_wifi | Freebox Wi-Fi Pop |
| freebox_pop | Freebox Pop |
| networking_device | Networking device |
| multimedia_device | Multimedia device |
| car | Connected car |
| watch | Smartwatch |
| light | Light |
| outlet | Connected outlet |
| appliances | Household appliances |
| thermostat | Thermostat |
| shutter | Electric shutter |
| other | Other |

<a id="LanHost.primary_name_manual"></a>

**`primary_name_manual bool Read-only`**

If true the primary name has been set manually

<a id="LanHost.l2ident"></a>

**`l2ident [] array of LanHostL2Ident Read-only`**

Layer 2 network id and its type

<a id="LanHost.vendor_name"></a>

**`vendor_name string Read-only`**

Host vendor name (from the mac address)

<a id="LanHost.persistent"></a>

**`persistent bool`**

If true the host is always shown even if it has not been active
since the Freebox startup

<a id="LanHost.reachable"></a>

**`reachable bool Read-only`**

If true the host can receive traffic from the Freebox

<a id="LanHost.last_time_reachable"></a>

**`last_time_reachable timestamp Read-only`**

Last time the host was reached

<a id="LanHost.active"></a>

**`active bool Read-only`**

If true the host sends traffic to the Freebox

<a id="LanHost.last_activity"></a>

**`last_activity timestamp Read-only`**

Last time the host sent traffic

<a id="LanHost.first_activity"></a>

**`first_activity timestamp Read-only`**

First time the host sent traffic, or 0 (Unix Epoch) if it wasn’t seen before this field was added.

<a id="LanHost.names"></a>

**`names [] array of LanHostName Read-only`**

List of available names, and their source

<a id="LanHost.l3connectivities"></a>

**`l3connectivities [] array of LanHostL3Connectivity Read-only`**

List of available layer 3 network connections

<a id="LanHost.network_control"></a>

**`network_control LanHostNetworkControl Read-only`**

If device is associated with a profile, contains profile summary.

<a id="LanHost.info"></a>

**`info dict Read-only`**

Contains detailed information that could be gathered about the device.

<a id="LanHostName"></a>

#### Objet LanHostName

<a id="LanHostName.name"></a>

**`name string Read-only`**

Host name

<a id="LanHostName.source"></a>

**`source enum Read-only`**

source of the name

<a id="LanHostL2Ident"></a>

#### Objet LanHostL2Ident

<a id="LanHostL2Ident.id"></a>

**`id string Read-only`**

Layer 2 id

<a id="LanHostL2Ident.type"></a>

**`type string Read-only`**

Type of layer 2 address

| source | Description |
| --- | --- |
| dhcp | DHCP |
| netbios | Netbios |
| mdns | mDNS hostname |
| mdns_srv | mDNS service |
| upnp | UPnP |
| wsd | WS-Discovery |

<a id="LanHostL3Connectivity"></a>

#### Objet LanHostL3Connectivity

<a id="LanHostL3Connectivity.addr"></a>

**`addr string Read-only`**

Layer 3 address

<a id="LanHostL3Connectivity.af"></a>

**`af enum Read-only`**

| af | Description |
| --- | --- |
| ipv4 | IPv4 |
| ipv6 | IPv6 |

<a id="LanHostL3Connectivity.active"></a>

**`active bool Read-only`**

is the connection active

<a id="LanHostL3Connectivity.reachable"></a>

**`reachable bool Read-only`**

is the connection reachable

<a id="LanHostL3Connectivity.last_activity"></a>

**`last_activity timestamp Read-only`**

last activity timestamp

<a id="LanHostL3Connectivity.last_time_reachable"></a>

**`last_time_reachable timestamp Read-only`**

last reachable timestamp

<a id="LanHostL3Connectivity.model"></a>

**`model string Read-only`**

device model if known

<a id="LanHostNetworkControl"></a>

#### Objet LanHostNetworkControl

<a id="LanHostNetworkControl.profile_id"></a>

**`profile_id int Read-only`**

Id of profile this device is associated with.

<a id="LanHostNetworkControl.name"></a>

**`name string Read-only`**

Name of profile this device is associated with.

<a id="LanHostNetworkControl.current_mode"></a>

**`current_mode enum Read-only`**

Mode described in [Network Control Object](../maison-profils/profile.md#net-object)

<a id="getting-the-list-of-hosts-on-a-given-interface"></a>

### Getting the list of hosts on a given interface

<a id="get--api-v16-lan-browser-interface-"></a>

**`GET /api/v16/lan/browser/{interface}/`**

Returns the list of [`LanHost`](lan.md#LanHost "LanHost") on this interface

**Example request**:

```http
GET /api/v16/lan/browser/pub/ HTTP/1.1
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
            "l2ident": {
                "id": "d0:23:db:36:15:aa",
                "type": "mac_address"
            },
            "active": true,
            "id": "ether-d0:23:db:36:15:aa",
            "last_time_reachable": 1360669498,
            "persistent": true,
            "names": [
                {
                    "name": "iPhone-r0ro",
                    "source": "dhcp"
                }
            ],
            "vendor_name": "Apple, Inc.",
            "l3connectivities": [
                {
                    "addr": "192.168.69.20",
                    "active": true,
                    "af": "ipv4",
                    "reachable": true,
                    "last_activity": 1360669498,
                    "last_time_reachable": 1360669498
                }
            ],
            "reachable": true,
            "last_activity": 1360669498,
            "primary_name_manual": true,
            "primary_name": "iPhone r0ro",
            "domain_name": "iphone-r0ro",
            "info": { }
        },
        {
            "l2ident": {
                "id": "00:24:d4:7e:00:4c",
                "type": "mac_address"
            },
            "active": true,
            "id": "ether-00:24:d4:7e:00:4c",
            "last_time_reachable": 1360669491,
            "persistent": false,
            "names": [
                {
                    "name": "Freebox Player",
                    "source": "dhcp"
                }
            ],
            "vendor_name": "FREEBOX SA",
            "l3connectivities": [
                {
                    "addr": "192.168.69.30",
                    "active": true,
                    "af": "ipv4",
                    "reachable": true,
                    "last_activity": 1360669491,
                    "last_time_reachable": 1360669491
                }
            ],
            "reachable": true,
            "last_activity": 1360669491,
            "primary_name_manual": false,
            "primary_name": "Freebox Player",
            "domain_name": "",
            "info": {
                "upnp": {
                    "modelName": "Freebox Player",
                    "friendlyName": "Freebox Player",
                    "manufacturer": "Freebox",
                    "service[0]": "urn:dial-multiscreen-org:serviceId:dial",
                    "deviceType": "urn:dial-multiscreen-org:device:dial:1"
                },
                "mdns": {
                    "Service: raop": "192.168.1.91:5000 (tcp)",
                    "Service: hid": "192.168.1.91:24322 (udp)",
                    "Service: airplay": "192.168.1.91:7000 (tcp)",
                    "Service: amzn-alexa": "192.168.1.91 (tcp)"
                },
                "dhcp": {
                    "Host Name": "Freebox Player"
                }
            }
        }
    ]
}
```

<a id="getting-an-host-information"></a>

### Getting an host information

<a id="get--api-v16-lan-browser-interface-hostid-"></a>

**`GET /api/v16/lan/browser/{interface}/{hostid}/`**

Returns the requested [`LanHost`](lan.md#LanHost "LanHost") properties

**Example request**:

```http
GET /api/v16/lan/browser/pub/ether-00:24:d4:7e:00:4c/ HTTP/1.1
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
        "l2ident": {
            "id": "00:24:d4:7e:00:4c",
            "type": "mac_address"
        },
        "active": true,
        "id": "ether-00:24:d4:7e:00:4c",
        "last_time_reachable": 1360669611,
        "persistent": false,
        "names": [
            {
                "name": "Freebox Player",
                "source": "dhcp"
            }
        ],
        "vendor_name": "FREEBOX SA",
        "l3connectivities": [
            {
                "addr": "192.168.69.30",
                "active": true,
                "af": "ipv4",
                "reachable": true,
                "last_activity": 1360669611,
                "last_time_reachable": 1360669611
            }
        ],
        "reachable": true,
        "last_activity": 1360669611,
        "primary_name_manual": false,
        "primary_name": "Freebox Player",
        "domain_name": "",
        "info": {
            "upnp": {
                "modelName": "Freebox Player",
                "friendlyName": "Freebox Player",
                "manufacturer": "Freebox",
                "service[0]": "urn:dial-multiscreen-org:serviceId:dial",
                "deviceType": "urn:dial-multiscreen-org:device:dial:1"
            },
            "mdns": {
                "Service: raop": "192.168.1.91:5000 (tcp)",
                "Service: hid": "192.168.1.91:24322 (udp)",
                "Service: airplay": "192.168.1.91:7000 (tcp)",
                "Service: amzn-alexa": "192.168.1.91 (tcp)"
            },
            "dhcp": {
                "Host Name": "Freebox Player"
            }
        }
    }
}
```

<a id="updating-an-host-information"></a>

### Updating an host information

<a id="put--api-v16-lan-browser-interface-hostid-"></a>

**`PUT /api/v16/lan/browser/{interface}/{hostid}/`**

Update a [`LanHost`](lan.md#LanHost "LanHost") properties

**Example request**:

```http
PUT /api/v16/lan/browser/pub/ether-00:24:d4:7e:00:4c/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "id":"ether-00:24:d4:7e:00:4c",
   "primary_name":"Freebox Tv"
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
        "l2ident": {
            "id": "00:24:d4:7e:00:4c",
            "type": "mac_address"
        },
        "active": true,
        "id": "ether-00:24:d4:7e:00:4c",
        "last_time_reachable": 1360669851,
        "persistent": true,
        "names": [
            {
                "name": "Freebox Player",
                "source": "dhcp"
            }
        ],
        "vendor_name": "FREEBOX SA",
        "l3connectivities": [
            {
                "addr": "192.168.69.30",
                "active": true,
                "af": "ipv4",
                "reachable": true,
                "last_activity": 1360669851,
                "last_time_reachable": 1360669851
            }
        ],
        "reachable": true,
        "last_activity": 1360669851,
        "primary_name_manual": true,
        "primary_name": "Freebox Tv",
        "domain_name": "",
        "info": {
            "upnp": {
                "modelName": "Freebox Player",
                "friendlyName": "Freebox Player",
                "manufacturer": "Freebox",
                "service[0]": "urn:dial-multiscreen-org:serviceId:dial",
                "deviceType": "urn:dial-multiscreen-org:device:dial:1"
            },
            "mdns": {
                "Service: raop": "192.168.1.91:5000 (tcp)",
                "Service: hid": "192.168.1.91:24322 (udp)",
                "Service: airplay": "192.168.1.91:7000 (tcp)",
                "Service: amzn-alexa": "192.168.1.91 (tcp)"
            },
            "dhcp": {
                "Host Name": "Freebox Player"
            }
        }
    }
}
```

<a id="getting-available-lan-host-types"></a>

### Getting available lan host types

<a id="get--api-v8-lan-browser-types-"></a>

**`GET /api/v8/lan/browser/types/`**

Get available [`LanHost`](lan.md#LanHost "LanHost") types

**Example request**:

```http
GET /api/v8/lan/browser/types/ HTTP/1.1
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
          "icon": "/resources/images/lan/ic_device_computer.png",
          "type": "workstation",
          "name": "Ordinateur",
          "category": "personal_device"
      },
      {
          "icon": "/resources/images/lan/ic_device_printer.png",
          "type": "printer",
          "name": "Imprimante",
          "category": "network"
      },
      ...
    ]
}
```

<a id="wake-on-lan"></a>

## Wake on LAN

<a id="send-wake-ok-lan-packet-to-an-host"></a>

### Send Wake ok Lan packet to an host

<a id="post--api-v8-lan-wol-interface-"></a>

**`POST /api/v8/lan/wol/{interface}/`**

Send a wake on LAN packet to the specified host with an optional password

**Example request**:

```http
POST /api/v8/lan/wol/pub/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "mac": "00:24:d4:7e:00:4c",
   "password": ""
}
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
