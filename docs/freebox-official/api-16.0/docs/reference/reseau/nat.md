<a id="nat"></a>

# NAT

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#nat)

## Navigation

- [NAT Errors](#nat-errors)
- [Dmz Config](#dmz-config)
- [Dmz Config API](#dmz-config-api)
- [Port Forwarding Config](#port-forwarding-config)
- [Port Forwarding API](#port-forwarding-api)
- [Incoming port Config](#incoming-port-config)
- [Incoming port API](#incoming-port-api)


With the nat API you control port forwarding on your network

<a id="nat-errors"></a>

## NAT Errors

When attempting to access the LAN API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| noent | Invalid id |
| internal_error | Internal error |
| exist | Conflict with an existing redirection |

<a id="dmz-config"></a>

## Dmz Config

Dmz config has the following attributes:

<a id="DmzConfig"></a>

### Objet DmzConfig

<a id="DmzConfig.ip"></a>

**`ip string`**

dmz host IP

<a id="DmzConfig.enabled"></a>

**`enabled bool`**

is dmz enabled

<a id="dmz-config-api"></a>

## Dmz Config API

<a id="get-the-current-dmz-configuration"></a>

### Get the current Dmz configuration

<a id="get--api-v8-fw-dmz-"></a>

**`GET /api/v8/fw/dmz/`**

Returns the current [`DmzConfig`](nat.md#DmzConfig "DmzConfig")

**Example request**:

```http
GET /api/v8/fw/dmz/ HTTP/1.1
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
        "enabled": false,
        "ip": ""
    }
}
```

<a id="update-the-current-dmz-configuration"></a>

### Update the current Dmz configuration

<a id="put--api-v8-fw-dmz-"></a>

**`PUT /api/v8/fw/dmz/`**

Update the current [`LanConfig`](lan.md#LanConfig "LanConfig")

**Example request**:

```http
PUT /api/v8/lan/config/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "enabled": true,
   "ip": "192.168.1.42"
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
        "enabled": true,
        "ip": "192.168.1.42"
    }
}
```

<a id="port-forwarding"></a>

# Port Forwarding

<a id="port-forwarding-config"></a>

## Port Forwarding Config

Port forwarding config has the following attributes:

<a id="PortForwardingConfig"></a>

### Objet PortForwardingConfig

<a id="PortForwardingConfig.id"></a>

**`id int`**

forwarding id

<a id="PortForwardingConfig.enabled"></a>

**`enabled bool`**

is forwarding enabled

<a id="PortForwardingConfig.ip_proto"></a>

**`ip_proto enum`**

| ip_proto | Description |
| --- | --- |
| tcp | TCP |
| udp | UDP |

<a id="PortForwardingConfig.wan_port_start"></a>

**`wan_port_start string`**

forwarding range start

<a id="PortForwardingConfig.wan_port_end"></a>

**`wan_port_end int`**

forwarding range end

<a id="PortForwardingConfig.lan_ip"></a>

**`lan_ip string`**

forwarding target on LAN

<a id="PortForwardingConfig.lan_port"></a>

**`lan_port int`**

forwarding target start port on LAN, (last port is lan_port +
wan_port_end - wan_port_start)

<a id="PortForwardingConfig.hostname"></a>

**`hostname string Read-only`**

forwarding target host name

<a id="PortForwardingConfig.host"></a>

**`host LanHost Read-only`**

forwarding target host information
(see: [`LanHost`](lan.md#LanHost "LanHost"))

<a id="PortForwardingConfig.src_ip"></a>

**`src_ip string`**

if src_ip == 0.0.0.0 this rule will apply to any src ip
otherwise it will only apply to the specified ip address

<a id="PortForwardingConfig.comment"></a>

**`comment string`**

comment

<a id="port-forwarding-api"></a>

## Port Forwarding API

<a id="getting-the-list-of-port-forwarding"></a>

### Getting the list of port forwarding

<a id="get--api-v8-fw-redir-"></a>

**`GET /api/v8/fw/redir/`**

**Example request**:

```http
GET /api/v8/fw/redir/ HTTP/1.1
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
            "enabled": true,
            "comment": "",
            "id": 1,
            "host": {
                [ ... ]
            },
            "hostname": "android-c5fe44a2c27be1e2",
            "lan_port": 69,
            "wan_port_end": 69,
            "wan_port_start": 69,
            "lan_ip": "192.168.1.22",
            "ip_proto": "tcp",
            "src_ip": "8.8.8.8"
        },
        {
            "enabled": true,
            "comment": "",
            "id": 2,
            "host": {
                [ ... ]
            },
            "hostname": "android-c5fe44a2c27be1e2",
            "lan_port": 1337,
            "wan_port_end": 1340,
            "wan_port_start": 1337,
            "lan_ip": "192.168.1.22",
            "ip_proto": "udp",
            "src_ip": "0.0.0.0"
        }
    ]
}
```

<a id="getting-a-specific-port-forwarding"></a>

### Getting a specific port forwarding

<a id="get--api-v8-fw-redir-redir_id"></a>

**`GET /api/v8/fw/redir/{redir_id}`**

Returns the requested [`PortForwardingConfig`](nat.md#PortForwardingConfig "PortForwardingConfig")
properties

**Example request**:

```http
GET /api/v8/fw/redir/1 HTTP/1.1
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
        "enabled": true,
        "comment": "",
        "id": 1,
        "host": {
            [ ... ]
        },
        "hostname": "android-c5fe44a2c27be1e2",
        "lan_port": 69,
        "wan_port_end": 69,
        "wan_port_start": 69,
        "lan_ip": "192.168.1.22",
        "ip_proto": "tcp",
        "src_ip": "0.0.0.0"
    }

}
```

<a id="updating-a-port-forwarding"></a>

### Updating a port forwarding

<a id="put--api-v8-fw-redir-redir_id"></a>

**`PUT /api/v8/fw/redir/{redir_id}`**

Update a [`PortForwardingConfig`](nat.md#PortForwardingConfig "PortForwardingConfig") properties

**Example request**:

```http
PUT /api/v8/fw/redir/1 HTTP/1.1
Host: mafreebox.freebox.fr

{
  "enabled": false
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
        "enabled": false,
        "comment": "",
        "id": 1,
        "host": {
            [ ... ]
        },
        "hostname": "android-c5fe44a2c27be1e2",
        "lan_port": 69,
        "wan_port_end": 69,
        "wan_port_start": 69,
        "lan_ip": "192.168.1.22",
        "ip_proto": "tcp",
        "src_ip": "0.0.0.0"
    }

}
```

<a id="add-a-port-forwarding"></a>

### Add a port forwarding

<a id="post--api-v8-fw-redir-"></a>

**`POST /api/v8/fw/redir/`**

Create a [`PortForwardingConfig`](nat.md#PortForwardingConfig "PortForwardingConfig")

**Example request**:

```http
POST /api/v8/fw/redir/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
    "enabled": true,
    "comment": "test",
    "lan_port": 4242,
    "wan_port_end": 4242,
    "wan_port_start": 4242,
    "lan_ip": "192.168.1.42",
    "ip_proto": "tcp",
    "src_ip": "0.0.0.0"
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
        "enabled": true,
        "comment": "test",
        "id": 3,
        "host": {
            [ ... ]
        },
        "hostname": "Mac-mini-de-Romain",
        "lan_port": 4242,
        "wan_port_end": 4242,
        "wan_port_start": 4242,
        "lan_ip": "192.168.1.42",
        "ip_proto": "tcp",
        "src_ip": "0.0.0.0"
    }
}
```

<a id="delete-a-port-forwarding"></a>

### Delete a port forwarding

<a id="delete--api-v8-fw-redir-redir_id"></a>

**`DELETE /api/v8/fw/redir/{redir_id}`**

Delete a [`PortForwardingConfig`](nat.md#PortForwardingConfig "PortForwardingConfig")

**Example request**:

```http
DELETE /api/v8/fw/redir/3 HTTP/1.1
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

<a id="incoming-port-configuration"></a>

# Incoming port configuration

Some services hosted on the Freebox Server need to listen to public ip address port.
Incoming port api allow to enable/disable incoming port binding, and select the bind port to
prevent conflit with your own nat port forwarding rules.

NOTE: you can’t add or remove incoming ports, this ports are managed by Freebox services.

NOTE: in case of conflict with a nat port forwarding rule, this rule will have a higher priority and
override the port forwarding rule.

<a id="incoming-port-config"></a>

## Incoming port Config

Incoming port config has the following attributes:

<a id="IncomingPortConfig"></a>

### Objet IncomingPortConfig

<a id="IncomingPortConfig.id"></a>

**`id string Read-only`**

incoming port id

| id | Description |
| --- | --- |
| http | http port for remote access to Freebox OS |
| https | https port for tls remote access to Freebox OS |
| bittorrent-main | main bittorrent port for Freebox downloader |
| bittorrent-dht | bittorrent port for DHT |
| openvpn_routed | routed openvpn port |
| openvpn_bridge | bridged openvpn port |
| ipsec_ike | ipsec ikev2 vpn port |
| ipsec_nat | ipsec nat vpn port |
| pptp | pptp vpn server port |
| ftp | ftp control port for FTP remote access |
| ftp_pasv | ftp data port for FTP remote access |

<a id="IncomingPortConfig.enabled"></a>

**`enabled bool`**

is the port binding allowed

<a id="IncomingPortConfig.active"></a>

**`active bool Read-only`**

is the port binding currently active

<a id="IncomingPortConfig.type"></a>

**`type enum Read-only`**

| ip_proto | Description |
| --- | --- |
| tcp | TCP |
| udp | UDP |
| tcp_udp | both TCP and UDP |

<a id="IncomingPortConfig.in_port"></a>

**`in_port int`**

binding port

<a id="IncomingPortConfig.netns"></a>

**`netns string Read-only`**

network namespace. The service may be running on a different namespace (for instance
if the service uses the vpn client).

<a id="propriete-sans-ancre-nat-20"></a>

**`in_port int`**

binding port

<a id="IncomingPortConfig.min_port"></a>

**`min_port int Read-only`**

This field indicate the minimum possible value for in_port
(see [`ConnectionStatus`](connection.md#ConnectionStatus "ConnectionStatus") ipv4_port_range)

<a id="IncomingPortConfig.max_port"></a>

**`max_port int Read-only`**

This field indicate the maximum possible value for in_port
(see [`ConnectionStatus`](connection.md#ConnectionStatus "ConnectionStatus") ipv4_port_range)

<a id="IncomingPortConfig.readonly"></a>

**`readonly bool Read-only`**

If set to true, the in_port field cannot be changed because
of the underlying protocol does not allow it

<a id="incoming-port-api"></a>

<a id="id1"></a>

## Incoming port API

<a id="getting-the-list-of-incoming-ports"></a>

### Getting the list of incoming ports

<a id="get--api-v8-fw-incoming-"></a>

**`GET /api/v8/fw/incoming/`**

**Example request**:

```http
GET /api/v8/fw/incoming/ HTTP/1.1
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
            "enabled": false,
            "type": "tcp",
            "in_port": 80,
            "id": "http",
            "netns": "init",
            "max_port": 65535,
            "min_port": 0
        },
        {
            "enabled": true,
            "type": "tcp",
            "in_port": 17591,
            "id": "bittorrent-main",
            "netns": "vpn",
            "max_port": 65535,
            "min_port": 0
        },
        {
            "enabled": true,
            "type": "udp",
            "in_port": 28946,
            "id": "bittorrent-dht",
            "netns": "vpn",
            "max_port": 65535,
            "min_port": 0
        }
    ]
}
```

<a id="getting-a-specific-incoming-port"></a>

### Getting a specific incoming port

<a id="get--api-v8-fw-incoming-port_id"></a>

**`GET /api/v8/fw/incoming/{port_id}`**

Returns the requested [`IncomingPortConfig`](nat.md#IncomingPortConfig "IncomingPortConfig")
properties

**Example request**:

```http
GET /api/v8/fw/incoming/bittorrent-main HTTP/1.1
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
        "enabled": true,
        "type": "tcp",
        "in_port": 17591,
        "id": "bittorrent-main",
        "netns": "vpn",
        "max_port": 65535,
        "min_port": 0
    }
}
```

<a id="updating-an-incoming-port"></a>

### Updating an incoming port

<a id="put--api-v8-fw-incoming-port_id"></a>

**`PUT /api/v8/fw/incoming/{port_id}`**

Update a [`IncomingPortConfig`](nat.md#IncomingPortConfig "IncomingPortConfig") properties

**Example request**:

```http
PUT /api/v8/lan/fw/incoming/bittorrent-main HTTP/1.1
Host: mafreebox.freebox.fr

{
  "in_port": 3615
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
        "enabled": true,
        "type": "tcp",
        "in_port": 3615,
        "id": "bittorrent-main",
        "netns": "vpn",
        "max_port": 65535,
        "min_port": 0
    }
}
```
