<a id="vpn-server-unstable"></a>

<a id="vpn-server-api"></a>

# VPN Server [UNSTABLE]

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#vpn-server-unstable)

## Navigation

- [VPN Server Errors](#vpn-server-errors)
- [VPN Server List](#vpn-server-list)
- [VPN Server Config](#vpn-server-config)
- [VPN Server Config API](#vpn-server-config-api)
- [VPN Server User API](#vpn-server-user-api)
- [VPN IP Pool](#vpn-ip-pool)
- [VPN Server Connection API](#vpn-server-connection-api)
- [VPN User configuration file API](#vpn-user-configuration-file-api)


The VPN Server API allows you to control the Freebox VPN Server

<a id="vpn-server-errors"></a>

## VPN Server Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| inval | invalid parameters |
| exist | entry already exists |
| noent | invalid id |
| nomem | internal error |
| unsupp | not supported |
| inuse | resource in use |
| busy | resource is busy |
| ioerror | internal error |
| size | too many elements |

<a id="vpn-server-list"></a>

## VPN Server List

<a id="vpn-server-object"></a>

### VPN Server Object

<a id="VPNServer"></a>

#### Objet VPNServer

VPNServer has the following attributes:

<a id="VPNServer.name"></a>

**`name string Read-only`**

VPN server name (id)

<a id="VPNServer.type"></a>

**`type enum Read-only`**

VPN server type

| type | Description |
| --- | --- |
| ipsec | IPsec IKEv2 server |
| pptp | PPTP VPN server |
| openvpn | OpenVPN server |
| wireguard | WireGuard server |

<a id="VPNServer.state"></a>

**`state enum Read-only`**

server state

| state |  |
| --- | --- |
| stopped |  |
| starting |  |
| started |  |
| stopping |  |
| error |  |

<a id="VPNServer.connection_count"></a>

**`connection_count int Read-only`**

number of active connections

<a id="VPNServer.auth_connection_count"></a>

**`auth_connection_count int Read-only`**

number of active connections that have passed authentication

<a id="vpn-server-list-api"></a>

### VPN Server List API

<a id="get--api-v8-vpn-"></a>

**`GET /api/v8/vpn/`**

Get the list of [`VPNServer`](vpn.md#VPNServer "VPNServer")

**Example request**:

```http
GET /api/v8/vpn/ HTTP/1.1
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
            "state": "stopped",
            "type": "pptp",
            "name": "pptp",
            "connection_count": 0,
            "auth_connection_count": 0
        },
        {
            "state": "stopped",
            "type": "openvpn",
            "name": "openvpn_routed",
            "connection_count": 0,
            "auth_connection_count": 0
        },
        {
            "state": "stopped",
            "type": "openvpn",
            "name": "openvpn_bridge",
            "connection_count": 0,
            "auth_connection_count": 0
        },
        {
            "state": "stopped",
            "type": "wireguard",
            "name": "wireguard",
            "connection_count": 0,
            "auth_connection_count": 0
        }
    ]
}
```

<a id="vpn-server-config"></a>

## VPN Server Config

<a id="VPNPPTPConfig"></a>

### Objet VPNPPTPConfig

VPNServerConfig has the following attributes:

<a id="VPNPPTPConfig.mppe"></a>

**`mppe enum`**

| mppe | Description |
| --- | --- |
| disable | disable mppe |
| require | require mppe |
| require_128 | require 128 bits mppe |

<a id="VPNPPTPConfig.allowed_auth"></a>

**`allowed_auth dict`**

allowed authentication methods dictionnary with following entries:

- pap
- chap
- mschapv2

values are booleans.

<a id="VPNOpenVpnConfig"></a>

### Objet VPNOpenVpnConfig

<a id="VPNOpenVpnConfig.cipher"></a>

**`cipher enum`**

| cipher |  |
| --- | --- |
| blowfish |  |
| aes128 |  |
| aes256 |  |
| chacha20poly1305 |  |

<a id="VPNOpenVpnConfig.disable_fragment"></a>

**`disable_fragment bool`**

disable fragment configuration option

<a id="VPNOpenVpnConfig.use_tcp"></a>

**`use_tcp bool`**

use TCP instead of UDP

<a id="VPNWireGuardConfig"></a>

### Objet VPNWireGuardConfig

<a id="VPNWireGuardConfig.mtu"></a>

**`mtu int`**

wireguard device MTU. Value must be between 512 and 1420.

<a id="VPNIPSecAuthMode"></a>

### Objet VPNIPSecAuthMode

<a id="VPNIPSecAuthMode.id_source"></a>

**`id_source enum`**

source of the connection id

| id_source |  |
| --- | --- |
| custom |  |

<a id="VPNIPSecAuthMode.id_custom"></a>

**`id_custom string`**

value of the source id when id_source is custom

<a id="VPNIPSecConfig"></a>

### Objet VPNIPSecConfig

<a id="VPNIPSecConfig.ike_version"></a>

**`ike_version int Read-only`**

IKE protocol version

<a id="VPNIPSecConfig.auth_modes"></a>

**`auth_modes [] array of VPNIPSecAuthMode Read-only`**

map of supported auth modes, currently only psk is supported

<a id="VPNServerConfig"></a>

### Objet VPNServerConfig

<a id="VPNServerConfig.id"></a>

**`id string Read-only`**

VPN server id

<a id="VPNServerConfig.type"></a>

**`type enum Read-only`**

VPN server type

| type | Description |
| --- | --- |
| pptp | PPTP VPN server |
| openvpn | OpenVPN server |
| ipsec | IPsec IKEv2 server |
| wireguard | WireGuard server |

<a id="VPNServerConfig.enabled"></a>

**`enabled bool`**

is the VPN server enabled

<a id="VPNServerConfig.enable_ipv4"></a>

**`enable_ipv4 bool`**

enable IPv4 on this server

NOTE: Not relevant for openvpn_bridge, pptp and wireguard

<a id="VPNServerConfig.enable_ipv6"></a>

**`enable_ipv6 bool`**

enable IPv6 on this server

NOTE: Not relevant for openvpn_bridge, pptp and wireguard

<a id="VPNServerConfig.port"></a>

**`port int`**

the server port

NOTE: you can only edit the server port when type is openvpn or wireguard

<a id="VPNServerConfig.min_port"></a>

**`min_port int Read-only`**

This field indicate the minimum possible value for port
(see [`ConnectionStatus`](../reseau/connection.md#ConnectionStatus "ConnectionStatus") ipv4_port_range)

<a id="VPNServerConfig.max_port"></a>

**`max_port int Read-only`**

This field indicate the maximum possible value for port
(see [`ConnectionStatus`](../reseau/connection.md#ConnectionStatus "ConnectionStatus") ipv4_port_range)

<a id="VPNServerConfig.port_ike"></a>

**`port_ike int`**

IPSec ike server port

NOTE: only present for ipsec server

<a id="VPNServerConfig.port_nat"></a>

**`port_nat int`**

IPSec nat server port

NOTE: only present for ipsec server

<a id="VPNServerConfig.conf_pptp"></a>

**`conf_pptp VPNPPTPConfig`**

only available when type is PPTP

<a id="VPNServerConfig.conf_openvpn"></a>

**`conf_openvpn VPNOpenVpnConfig`**

only available when type is OpenVPN

<a id="VPNServerConfig.conf_ipsec"></a>

**`conf_ipsec VPNIPSecConfig`**

only available when type is IPsec

<a id="VPNServerConfig.conf_wireguard"></a>

**`conf_wireguard VPNWireGuardConfig`**

only available when type is WireGuard

<a id="VPNServerConfig.ip_start"></a>

**`ip_start string Read-only`**

start of the IP range that will be used to give clients an IP

<a id="VPNServerConfig.ip_end"></a>

**`ip_end string Read-only`**

end of the IP range that will be used to give clients an IP

<a id="VPNServerConfig.ip6_start"></a>

**`ip6_start string Read-only`**

start of the IPv6 range that will be used to give clients an IPv6

<a id="VPNServerConfig.ip6_end"></a>

**`ip6_end string Read-only`**

end of the IPv6 range that will be used to give clients an IPv6

<a id="vpn-server-config-api"></a>

## VPN Server Config API

<a id="get-a-vpn-config"></a>

### Get a VPN config

<a id="get--api-v8-vpn-vpn_id-config-"></a>

**`GET /api/v8/vpn/{vpn_id}/config/`**

Get the [`VPNServerConfig`](vpn.md#VPNServerConfig "VPNServerConfig")

**Example request**:

```http
GET /api/v8/vpn/openvpn_routed/config/ HTTP/1.1
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
        "port": 1194,
        "conf_openvpn": {
            "cipher": "aes128"
        },
        "id": "openvpn_routed",
        "ip_start": "192.168.27.65",
        "ip_end": "192.168.27.95",
        "type": "openvpn"
    }
}
```

<a id="update-the-vpn-configuration"></a>

### Update the VPN configuration

<a id="put--api-v8-vpn-openvpn_routed-config-"></a>

**`PUT /api/v8/vpn/openvpn_routed/config/`**

Update the [`VPNServerConfig`](vpn.md#VPNServerConfig "VPNServerConfig")

**Example request**:

```http
PUT /api/v8/vpn/openvpn_routed/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "conf_openvpn": {
      "cipher": "blowfish"
    }
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
        "port": 1194,
        "conf_openvpn": {
            "cipher": "blowfish"
        },
        "id": "openvpn_routed",
        "ip_start": "192.168.27.65",
        "ip_end": "192.168.27.95",
        "type": "openvpn"
    }
}
```

<a id="vpn-server-user-api"></a>

## VPN Server User API

VPN users are common to all VPN servers.

<a id="vpn-server-user-object"></a>

### VPN Server User Object

<a id="VPNUser"></a>

#### Objet VPNUser

VPNUser has the following attributes:

<a id="VPNUser.login"></a>

**`login string`**

VPN user login

<a id="VPNUser.type"></a>

**`type enum`**

VPN user type

| type |  |
| --- | --- |
| standard |  |
| wireguard |  |

<a id="VPNUser.password"></a>

**`password string Write-only`**

VPN user password (length must be between 8 and 32)

<a id="VPNUser.password_set"></a>

**`password_set bool Read-only`**

True if a password was provided for this user

<a id="VPNUser.ip_reservation"></a>

**`ip_reservation ipv4`**

You can specify the IP you want to assign to this user.
If you don’t want to use a specific IP pass an empty string or omit this
property. This field is required if the type property is set to
‘wireguard’.

The IP must be in the VPN range (see ip_start, ip_end).

<a id="VPNUser.conf_wireguard"></a>

#### Objet conf_wireguard

This field is present only if the type property is set to
‘wireguard’.

<a id="VPNUser.conf_wireguard.keepalive"></a>

**`keepalive int`**

Interval in seconds at which keepalive packets are sent.

<a id="VPNUser.conf_wireguard.psk"></a>

**`psk bool`**

Enable optional preshared-key.

<a id="vpn-server-user-list"></a>

### VPN Server User List

<a id="get--api-v8-vpn-user-"></a>

**`GET /api/v8/vpn/user/`**

Get the list of [`VPNUser`](vpn.md#VPNUser "VPNUser")

**Example request**:

```http
GET /api/v8/vpn/user/ HTTP/1.1
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
            "ip_reservation": "",
            "type": "standard",
            "login": "test-1392677633-np",
            "password_set": false
        },
        {
            "ip_reservation": "",
            "type": "standard",
            "login": "test-1392677633",
            "password_set": true
        },
        {
            "ip_reservation": "192.168.27.68",
            "type": "wireguard",
            "login": "test-1392677633-wg",
            "password_set": false,
            "conf_wireguard": {
                "keepalive": 10,
                "psk": false
            }
        }
    ]
}
```

<a id="get-a-vpn-user"></a>

### Get a VPN user

<a id="get--api-v8-vpn-user-login"></a>

**`GET /api/v8/vpn/user/{login}`**

Gets the [`VPNUser`](vpn.md#VPNUser "VPNUser") with the given login

**Example request**:

```http
GET /api/v8/vpn/user/test-1392677633-np HTTP/1.1
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
        "ip_reservation": "",
        "login": "test-1392677633-np",
        "type": "standard",
        "password_set": false
    }
}
```

<a id="add-a-vpn-user"></a>

### Add a VPN User

<a id="post--api-v8-vpn-user-"></a>

**`POST /api/v8/vpn/user/`**

Creates a new [`VPNUser`](vpn.md#VPNUser "VPNUser").

**Example request**:

```http
POST /api/v8/vpn/user/ HTTP/1.1
Host: mafreebox.freebox.fr

{
  "login": "vpnuser01",
  "type": "standard",
  "password": "thisisasecret",
  "ip_reservation": "192.168.27.69"
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
        "ip_reservation": "192.168.27.69",
        "login": "vpnuser01",
        "password_set": true
    }
}
```

<a id="delete-a-vpn-user"></a>

### Delete a VPN User

<a id="delete--api-v8-vpn-user-login"></a>

**`DELETE /api/v8/vpn/user/{login}`**

Deletes the [`VPNUser`](vpn.md#VPNUser "VPNUser")

**Example request**:

```http
DELETE /api/v8/vpn/user/vpnuser01 HTTP/1.1
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

<a id="update-a-vpn-user"></a>

### Update a VPN User

<a id="put--api-v8-vpn-user-login"></a>

**`PUT /api/v8/vpn/user/{login}`**

Updates the [`VPNUser`](vpn.md#VPNUser "VPNUser") task with the given login

**Example request**:

```http
PUT /api/v8/vpn/user/test-1392677633-np HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
    "password": "donttellanyone"
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
        "ip_reservation": "",
        "login": "test-1392677633-np",
        "password_set": true
    }
}
```

<a id="vpn-ip-pool"></a>

## VPN IP Pool

<a id="get-the-vpn-server-ip-pool-reservations"></a>

### Get the VPN server IP pool reservations

<a id="get--api-v8-vpn-ip_pool-"></a>

**`GET /api/v8/vpn/ip_pool/`**

Gets the [`VPNUser`](vpn.md#VPNUser "VPNUser") with the given login

**Example request**:

```http
GET /api/v8/vpn/ip_pool/ HTTP/1.1
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
        "ip_start": "192.168.27.65",
        "ip_end": "192.168.27.95",
        "reservations": [
            {
                "login": "test",
                "ip": "192.168.27.69"
            }
        ]
    }
}
```

<a id="vpn-server-connection-api"></a>

## VPN Server Connection API

This API allows listing the active connections to the VPN server

<a id="vpn-connection-object"></a>

### VPN Connection Object

<a id="VPNConnection"></a>

#### Objet VPNConnection

VPNConnection has the following attributes:

<a id="VPNConnection.id"></a>

**`id string Read-only`**

connection id

<a id="VPNConnection.vpn"></a>

**`vpn strong Read-only`**

related VPN server id

<a id="VPNConnection.user"></a>

**`user string Read-only`**

user login

<a id="VPNConnection.authenticated"></a>

**`authenticated bool Read-only`**

is the connection authenticated

<a id="VPNConnection.auth_time"></a>

**`auth_time int Read-only`**

timestamp of the authentication

<a id="VPNConnection.src_ip"></a>

**`src_ip ipv4 Read-only`**

connection source IP address

<a id="VPNConnection.src_port"></a>

**`src_port int Read-only`**

connection source port

<a id="VPNConnection.local_ip"></a>

**`local_ip int Read-only`**

attributed IP address from VPN adress pool

<a id="VPNConnection.rx_bytes"></a>

**`rx_bytes int Read-only`**

rx bytes

<a id="VPNConnection.tx_bytes"></a>

**`tx_bytes int Read-only`**

tx bytes

<a id="get-the-list-of-connections"></a>

### Get the list of connections

<a id="get--api-v8-vpn-connection-"></a>

**`GET /api/v8/vpn/connection/`**

Get the list of [`VPNUser`](vpn.md#VPNUser "VPNUser")

**Example request**:

```http
GET /api/v8/vpn/user/ HTTP/1.1
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
            "rx_bytes": 94,
            "authenticated": true,
            "tx_bytes": 94,
            "user": "test",
            "id": "pptp-2",
            "vpn": "pptp",
            "src_ip": "93.184.216.119",
            "auth_time": 1392895603,
            "local_ip": "192.168.27.65"
        }
    ]
}
```

<a id="close-a-given-connection"></a>

### Close a given connection

<a id="delete--api-v8-vpn-connection-id"></a>

**`DELETE /api/v8/vpn/connection/{id}`**

Deletes the [`VPNUser`](vpn.md#VPNUser "VPNUser")

**Example request**:

```http
DELETE /api/v8/vpn/connection/pptp-2 HTTP/1.1
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

<a id="vpn-user-configuration-file-api"></a>

## VPN User configuration file API

For OpenVPN and WireGuard servers, you can download a configuration file that
will be used to configure the VPN client

<a id="donwload-a-user-configuration-file"></a>

### Donwload a user configuration file

<a id="get--api-v8-vpn-download_config-server_name-login-fmt"></a>

**`GET /api/v8/vpn/download_config/{server_name}/{login}/{fmt}`**

Download an configuration file for the given server and login
The “fmt” field must be set to either “plain” or “json”.

WARNING: each time you download a new OpenVPN configuration file for a
given user, you invalidate previous configuration file emitted for this user

WARNING: This api will not be available if you are missing the ‘settings’
permission

**Example request**:

```http
GET /api/v8/vpn/download_config/openvpn_routed/test/plain HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Date: Thu, 20 Feb 2014 13:14:01 GMT
Server: nginx
Content-Type: application/x-openvpn-profile
Content-Disposition: attachment; filename="config_openvpn_routed_test.ovpn"
Keep-Alive: timeout=5, max=99
Connection: Keep-Alive
Transfer-Encoding: chunked

[ ... ]
```
