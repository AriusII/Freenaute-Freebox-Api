<a id="vpn-client-unstable"></a>

<a id="vpn-client-api"></a>

# VPN Client [UNSTABLE]

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#vpn-client-unstable)

## Navigation

- [VPN Client Errors](#vpn-client-errors)
- [VPN Client Configuration](#vpn-client-configuration)
- [VPN Client Status](#vpn-client-status)


The VPN Client API allows you to control the Freebox VPN Client

<a id="vpn-client-errors"></a>

## VPN Client Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| inval | invalid parameters |
| nomem | internal error |
| ioerror | internal error |
| nodev | invalid device |
| noent | invalid id |
| netdown | network is not available |
| exist | entry already exists |
| busy | resource is busy |

<a id="vpn-client-configuration"></a>

## VPN Client Configuration

<a id="vpn-client-configuration-object"></a>

### VPN Client Configuration Object

<a id="VPNClientConfig"></a>

#### Objet VPNClientConfig

VPNClientConfig has the following attributes:

<a id="VPNClientConfig.id"></a>

**`id string Read-only`**

VPN config id

<a id="VPNClientConfig.description"></a>

**`description string`**

VPN description

<a id="VPNClientConfig.type"></a>

**`type enum`**

VPN server type

| type | Description |
| --- | --- |
| pptp | PPTP VPN server |
| openvpn | OpenVPN server |
| wireguard | WireGuard server |

<a id="VPNClientConfig.active"></a>

**`active bool`**

is this configuration active.
Only one configuration is active at a time.

<a id="VPNClientConfig.conf_pptp"></a>

**`conf_pptp VPNClientConfigPPTP`**

only available when type is PPTP

<a id="VPNClientConfig.conf_wireguard"></a>

**`conf_wireguard VPNClientConfigWireGuard`**

only available when type is WireGuard

<a id="VPNClientConfigPPTP"></a>

#### Objet VPNClientConfigPPTP

VPNClientConfigPPTP has the following attributes:

<a id="VPNClientConfigPPTP.remote_host"></a>

**`remote_host string`**

remote host IP or name

<a id="VPNClientConfigPPTP.username"></a>

**`username string`**

VPN username

<a id="VPNClientConfigPPTP.password"></a>

**`password string Write-only`**

VPN password

<a id="VPNClientConfigPPTP.mppe"></a>

**`mppe enum`**

| mppe | Description |
| --- | --- |
| disable | disable mppe |
| require | require mppe |
| require_128 | require 128 bits mppe |

<a id="VPNClientConfigPPTP.allowed_auth"></a>

**`allowed_auth dict`**

allowed authentication methods dictionary with following keys:

- eap
- pap
- chap
- mschap
- mschapv2

values are booleans.

<a id="VPNClientConfigWireGuard"></a>

#### Objet VPNClientConfigWireGuard

VPNClientConfigWireGuard has the following attributes:

<a id="VPNClientConfigWireGuard.remote_addr"></a>

**`remote_addr string`**

remote host IP

<a id="VPNClientConfigWireGuard.remote_port"></a>

**`remote_port int`**

remote host port

<a id="VPNClientConfigWireGuard.remote_public_key"></a>

**`remote_public_key string`**

remote host public key

<a id="VPNClientConfigWireGuard.remote_preshared_key"></a>

**`remote_preshared_key string`**

optional preshared key

<a id="VPNClientConfigWireGuard.local_priv_key"></a>

**`local_priv_key string`**

local private key

<a id="VPNClientConfigWireGuard.local_addr"></a>

**`local_addr [] array of VPNClientConfigWireGuardIP`**

IPs to assign to the local interface.

<a id="VPNClientConfigWireGuard.dns"></a>

**`dns [] array of string`**

list of strings containing IPs of DNS servers to use.
Both IPv4 and IPv6 are supported.

<a id="VPNClientConfigWireGuardIP"></a>

#### Objet VPNClientConfigWireGuardIP

<a id="VPNClientConfigWireGuardIP.ip"></a>

**`ip string`**

string representation of an IPv4 or IPv6 address

<a id="VPNClientConfigWireGuardIP.len"></a>

**`len int`**

prefix length associated with the IP address

<a id="get-vpn-client-configuration-list"></a>

### Get VPN Client configuration list

<a id="get--api-v8-vpn_client-config-"></a>

**`GET /api/v8/vpn_client/config/`**

Get the list of [`VPNClientConfig`](vpn_client.md#VPNClientConfig "VPNClientConfig")

**Example request**:

```http
GET /api/v8/vpn_client/config/ HTTP/1.1
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
            "type": "pptp",
            "description": "test vpn2",
            "active": true,
            "id": "vpn0",
            "conf_pptp": {
                "mppe": "require",
                "username": "freeuser",
                "remote_host": "vpnhost.example.org",
                "allowed_auth": {
                    "eap": false,
                    "mschap": false,
                    "mschapv2": true,
                    "chap": false,
                    "pap": false
                }
            }
        },
        {
            "type": "pptp",
            "description": "test vpn1",
            "active": false,
            "id": "vpn1",
            "conf_pptp": {
                "mppe": "require",
                "username": "testuser",
                "remote_host": "example.org",
                "allowed_auth": {
                    "eap": false,
                    "mschap": false,
                    "mschapv2": true,
                    "chap": false,
                    "pap": false
                }
            }
        }
        {
            "type": "wireguard",
            "description": "test vpn2",
            "active": false,
            "id": "vpn2",
            "conf_wireguard": {
                "local_addr": [{"ip":"198.51.100.10", "len":24}],
                "local_priv_key": "TdbS1Y0RHZ6rRNSxlEUssD/pnRDfrHMFfJPLl5icvQg=",
                "dns": ["198.51.100.53", "2001:db8:100::53"],
                "mtu": 1420,
                "remote_public_key": "QZnLR0TYPbPbhfVWeLVRf1zsPC0JXG/woVmsmEkgsw8=",
                "remote_addr": "192.0.2.1",
                "remote_port": 51820,
                "remote_preshared_key": ""
            }
        }
    ]
}
```

<a id="get-a-vpn-client-config"></a>

### Get a VPN client config

<a id="get--api-v8-vpn_client-config-id"></a>

**`GET /api/v8/vpn_client/config/{id}`**

Get the [`VPNClientConfig`](vpn_client.md#VPNClientConfig "VPNClientConfig")

**Example request**:

```http
GET /api/v8/vpn_client/config/vpn0 HTTP/1.1
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
         "type": "pptp",
         "description": "test vpn2",
         "active": true,
         "id": "vpn0",
         "conf_pptp": {
             "mppe": "require",
             "username": "freeuser",
             "remote_host": "vpnhost.example.org",
             "allowed_auth": {
                 "eap": false,
                 "mschap": false,
                 "mschapv2": true,
                 "chap": false,
                 "pap": false
             }
         }
    }
}
```

<a id="add-a-vpn-client-configuration"></a>

### Add a VPN client configuration

<a id="post--api-v8-vpn_client-config-"></a>

**`POST /api/v8/vpn_client/config/`**

Creates a new [`VPNClientConfig`](vpn_client.md#VPNClientConfig "VPNClientConfig").

**Example request**:

```http
POST /api/v8/vpn_client/config/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "type": "pptp",
   "description": "test pptp",
   "active": false,
   "conf_pptp": {
      "mppe": "require",
      "username": "fbxtest",
      "password": "",
      "remote_host": "test.example.org",
      "allowed_auth": {
         "mschapv2": true
      }
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
        "type": "pptp",
        "description": "test pptp",
        "active": false,
        "id": "vpn2",
        "conf_pptp": {
            "password": "",
            "mppe": "require",
            "username": "fbxtest",
            "remote_host": "test.example.org",
            "allowed_auth": {
                "eap": false,
                "mschap": false,
                "mschapv2": true,
                "chap": false,
                "pap": false
            }
        }
    }
}
```

<a id="delete-a-vpn-client-configuration"></a>

### Delete a VPN client Configuration

<a id="delete--api-v8-vpn_client-config-id"></a>

**`DELETE /api/v8/vpn_client/config/{id}`**

Deletes the [`VPNClientConfig`](vpn_client.md#VPNClientConfig "VPNClientConfig")

**Example request**:

```http
DELETE /api/v8/vpn_client/config/vpn2 HTTP/1.1
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

<a id="update-the-vpn-client-configuration"></a>

### Update the VPN client configuration

<a id="put--api-v8-vpn_client-config-id"></a>

**`PUT /api/v8/vpn_client/config/{id}`**

Update the [`VPNServerConfig`](vpn.md#VPNServerConfig "VPNServerConfig")

**Example request**:

```http
PUT /api/v8/vpn_client/config/vpn0 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "active": false
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
         "type": "pptp",
         "description": "test vpn2",
         "active": false,
         "id": "vpn0",
         "conf_pptp": {
             "mppe": "require",
             "username": "freeuser",
             "remote_host": "vpnhost.example.org",
             "allowed_auth": {
                 "eap": false,
                 "mschap": false,
                 "mschapv2": true,
                 "chap": false,
                 "pap": false
             }
         }
    }
}
```

<a id="vpn-client-status"></a>

## VPN Client Status

<a id="vpn-client-status-object"></a>

### VPN Client Status Object

<a id="VPNClientStatus"></a>

#### Objet VPNClientStatus

VPNClientStatus has the following attributes:

<a id="VPNClientStatus.enabled"></a>

**`enabled bool Read-only`**

is VPN client enabled

<a id="VPNClientStatus.active_vpn"></a>

**`active_vpn string Read-only`**

active VPN id

<a id="VPNClientStatus.active_vpn_description"></a>

**`active_vpn_description string Read-only`**

active VPN description

<a id="VPNClientStatus.type"></a>

**`type enum Read-only`**

active VPN type

| type | Description |
| --- | --- |
| pptp | PPTP VPN server |
| openvpn | OpenVPN server |
| wireguard | WireGuard server |

<a id="VPNClientStatus.state"></a>

**`state enum Read-only`**

| state | Description |
| --- | --- |
| waiting_wan | waiting for wan connection |
| going_up | connecting |
| up | connected |
| going_down | disconnecting |
| down | disconnected |

<a id="VPNClientStatus.last_up"></a>

**`last_up int Read-only`**

timestamp of last successful connection

<a id="VPNClientStatus.last_try"></a>

**`last_try int Read-only`**

timestamp of last connection attempt

<a id="VPNClientStatus.next_try"></a>

**`next_try int Read-only`**

seconds left until next connection attempt

<a id="VPNClientStatus.last_error"></a>

**`last_error enum Read-only`**

| last_error | Description |
| --- | --- |
| none | no error |
| internal | internal error |
| authentication_failed | wrong credentials |
| auth_failed | wrong credentials |
| resolv_failed | invalid host name |
| connect_timeout | connection timeout |
| connect_failed | connection failed |
| setup_control_failed | PPTP session negotiation failure |
| setup_call_failed | PPTP session failure |
| protocol | protocol error |
| remote_terminated | connection closed by remote peer |
| remote_disconnect | connection closed by remote peer |

<a id="VPNClientStatus.stats"></a>

**`stats VpnClientStats Read-only`**

connection statistics

<a id="VPNClientStatus.IPv4"></a>

**`IPv4 VpnClientIpInfo Read-only`**

connection IPv4 information

<a id="VpnClientStats"></a>

#### Objet VpnClientStats

<a id="VpnClientStats.rate_up"></a>

**`rate_up int Read-only`**

current upload rate (in byte/s)

<a id="VpnClientStats.rate_down"></a>

**`rate_down int Read-only`**

current download rate (in byte/s)

<a id="VpnClientStats.bytes_up"></a>

**`bytes_up int Read-only`**

total bytes uploaded

<a id="VpnClientStats.bytes_down"></a>

**`bytes_down int Read-only`**

total bytes downloaded

<a id="VpnClientIpInfo"></a>

#### Objet VpnClientIpInfo

<a id="VpnClientIpInfo.config_valid"></a>

**`config_valid bool Read-only`**

is the configuration valid

<a id="VpnClientIpInfo.ip_mask"></a>

**`ip_mask dict Read-only`**

assigned IP and netmask

<a id="VpnClientIpInfo.domain"></a>

**`domain string Read-only`**

provided domain

<a id="VpnClientIpInfo.gateway"></a>

**`gateway IPv4 Read-only`**

provided gateway

<a id="VpnClientIpInfo.dns"></a>

**`dns [] array of ipv4 Read-only`**

list of dns servers

<a id="VpnClientIpInfo.provider"></a>

**`provider enum Read-only`**

ip_mask source

| provider | Description |
| --- | --- |
| none | none |
| static | static IP configuration |
| ppp | ppp |
| dhcp | DHCP server |

<a id="VpnClientIpInfo.routes"></a>

**`routes list Read-only`**

list of provided routes

<a id="VpnClientIpInfo.dhcp"></a>

**`dhcp dict Read-only`**

DHCP status information

<a id="get-the-vpn-client-status"></a>

### Get the VPN client status

<a id="get--api-v8-vpn_client-status"></a>

**`GET /api/v8/vpn_client/status`**

Get the [`VPNClientStatus`](vpn_client.md#VPNClientStatus "VPNClientStatus")

**Example request**:

```http
GET /api/v8/vpn_client/status HTTP/1.1
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
        "type": "pptp",
        "last_error": "none",
        "active_vpn_description": "test vpn",
        "last_try": 1392904509,
        "state": "up",
        "stats": {
            "rate_up": 0,
            "bytes_down": 94,
            "bytes_up": 94,
            "rate_down": 0
        },
        "active_vpn": "vpn1",
        "next_try": 0,
        "last_up": 1392904510,
        "ipv4": {
            "routes": { },
            "config_valid": true,
            "ip_mask": {
                "ip": "192.168.27.65",
                "mask": "255.255.255.255"
            },
            "provider": "ppp",
            "dhcp": {
                "state": "down",
                "renew_remaining": 0,
                "dhcp_options": { },
                "lease_remaining": 0,
                "lease_time": 0,
                "rebind_remaining": 0,
                "server_id": 0
            },
            "dns": [
                "212.27.38.253"
            ],
            "domain": "",
            "gateway": "212.27.38.253"
        }
    }
}
```

<a id="get-the-vpn-client-logs"></a>

### Get the VPN client logs

<a id="get--api-v8-vpn_client-log"></a>

**`GET /api/v8/vpn_client/log`**

**Example request**:

```http
GET /api/v8/vpn_client/log HTTP/1.1
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
    "result": "2014-02-20 14:55:10 dbg: ppp: pppd: sent [ ... ] "
}
```
