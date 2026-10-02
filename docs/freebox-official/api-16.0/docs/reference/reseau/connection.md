<a id="connection-api"></a>

# Connection API

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#connection-api)

## Navigation

- [Connection Errors](#connection-errors)
- [Connection status](#connection-status)
- [Connection configuration](#connection-configuration)
- [Connection IPv6 configuration](#connection-ipv6-configuration)
- [Connection xDSL status [UNSTABLE]](#connection-xdsl-status-unstable)
- [Connection LTE status [UNSTABLE]](#connection-lte-status-unstable)
- [Connection FTTH status [UNSTABLE]](#connection-ftth-status-unstable)
- [Connection DynDNS status](#connection-dyndns-status)
- [Connection DynDNS configuration](#connection-dyndns-configuration)


This API provides Freebox connection settings information.

<a id="connection-errors"></a>

## Connection Errors

When attempting to access the file connection API, you may encounter
the following errors:

| error_code | Description |
| --- | --- |
| inval | invalid request |
| nodev | no device found with this name |
| noent | no entity found with this name |
| netdown | network is down |
| busy | device is busy |
| invalid_port | invalid port |
| insecure_password | the password is too weak to enable remote access |
| invalid_provider | invalid ddns provider name |
| invalid_next_hop | invalid next hop address (should be a link local address) |

<a id="connection-status"></a>

## Connection status

<a id="connection-status-object"></a>

### Connection status object

<a id="ConnectionStatus"></a>

#### Objet ConnectionStatus

<a id="ConnectionStatus.state"></a>

**`state enum Read-only`**

| State | Description |
| --- | --- |
| going_up | connection is initializing |
| up | connection is active |
| going_down | connection is about to become inactive |
| down | connection is inactive |

<a id="ConnectionStatus.type"></a>

**`type enum Read-only`**

| Type | Description |
| --- | --- |
| ethernet | FTTH/ethernet |
| rfc2684 | xDSL (unbundled) |
| pppoatm | xDSL |

<a id="ConnectionStatus.media"></a>

**`media enum Read-only`**

| Media | Description |
| --- | --- |
| ftth | FTTH |
| ethernet | ethernet |
| xdsl | xDSL |
| backup_4g | Internet Backup |

<a id="ConnectionStatus.ipv4"></a>

**`ipv4 string Read-only`**

Freebox IPv4 address

NOTE: this field is only available when connection state is up

<a id="ConnectionStatus.ipv6"></a>

**`ipv6 string Read-only`**

Freebox IPv6 address

NOTE: this field is only available when connection state is up

<a id="ConnectionStatus.rate_up"></a>

**`rate_up int Read-only`**

current upload rate in byte/s

<a id="ConnectionStatus.rate_down"></a>

**`rate_down int Read-only`**

current download rate in byte/s

<a id="ConnectionStatus.bandwidth_up"></a>

**`bandwidth_up int Read-only`**

available upload bandwidth in bit/s

<a id="ConnectionStatus.bandwidth_down"></a>

**`bandwidth_down int Read-only`**

available download bandwidth in bit/s

<a id="ConnectionStatus.bytes_up"></a>

**`bytes_up int Read-only`**

total uploaded bytes since last connection

<a id="ConnectionStatus.bytes_down"></a>

**`bytes_down int Read-only`**

total downloaded bytes since last connection

<a id="ConnectionStatus.ipv4_port_range"></a>

**`ipv4_port_range int[2] Read-only`**

Some customers share the same IPv4 and each customer is then
assigned a port range. The first value is the first port of the assigned
range and the second value is the last port (inclusive).

All [`PortForwardingConfig`](nat.md#PortForwardingConfig "PortForwardingConfig") must use ports in this range
to be effective.

<a id="get-the-current-connection-status"></a>

### Get the current Connection status

<a id="get--api-v11-connection-"></a>

**`GET /api/v11/connection/`**

Returns the current [`ConnectionStatus`](connection.md#ConnectionStatus "ConnectionStatus")

**Example request**:

```http
GET /api/v11/connection/ HTTP/1.1
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
        "type": "ethernet",
        "rate_down": 61,
        "bytes_up": 5489542,
        "rate_up": 0,
        "bandwidth_up": 100000000,
        "ipv4": "13.37.42.42",
        "ipv4_port_range": [
            0,
            65535
        ],
        "ipv6": "2a01:e30:d252:a2a0::1",
        "bandwidth_down": 100000000,
        "state": "up",
        "bytes_down": 13332830,
        "media": "ftth"
    }
}
```

<a id="connection-configuration"></a>

## Connection configuration

<a id="connection-configuration-object"></a>

### Connection configuration object

<a id="ConnectionConfiguration"></a>

#### Objet ConnectionConfiguration

<a id="ConnectionConfiguration.ping"></a>

**`ping bool`**

should the Freebox respond to external ping requests

<a id="ConnectionConfiguration.is_secure_pass"></a>

**`is_secure_pass bool Read-only`**

is the admin password secure enough to enable remote access

<a id="ConnectionConfiguration.remote_access"></a>

**`remote_access bool`**

enable/disable HTTP remote access

<a id="ConnectionConfiguration.remote_access_port"></a>

**`remote_access_port int`**

port number to use for remote HTTP access

<a id="ConnectionConfiguration.remote_access_min_port"></a>

**`remote_access_min_port int Read-only`**

This field indicate the minimum possible value for
remote_access_port (see [`ConnectionStatus`](connection.md#ConnectionStatus "ConnectionStatus") ipv4_port_range)

<a id="ConnectionConfiguration.remote_access_max_port"></a>

**`remote_access_max_port int Read-only`**

This field indicate the maximum possible value for
remote_access_port (see [`ConnectionStatus`](connection.md#ConnectionStatus "ConnectionStatus") ipv4_port_range)

<a id="ConnectionConfiguration.remote_access_ip"></a>

**`remote_access_ip string Read-only`**

IPv4 to use for remote access (can be missing if connection is down)

<a id="ConnectionConfiguration.api_remote_access"></a>

**`api_remote_access bool Read-only`**

is remote access enabled for apps, or share link

<a id="ConnectionConfiguration.wol"></a>

**`wol bool`**

enable/disable Wake-on-lan proxy

<a id="ConnectionConfiguration.adblock"></a>

**`adblock bool`**

is ads blocking feature enabled

<a id="ConnectionConfiguration.adblock_not_set"></a>

**`adblock_not_set bool Read-only`**

if set to true adblock setting has never been set by the user

<a id="ConnectionConfiguration.allow_token_request"></a>

**`allow_token_request bool`**

if false, user has disabled new token request.
New apps can’t request a new token.
Apps that already have a token are still allowed

<a id="ConnectionConfiguration.sip_alg"></a>

**`sip_alg enum`**

| Status | Description |
| --- | --- |
| disabled | Fully disable SIP ALG |
| direct_media | Enable SIP ALG, RTP only allowed between SIP UA |
| any_media | Enable SIP ALG, RTP allowed between any host (dangerous for untrusted hosts) |

<a id="get-the-current-connection-configuration"></a>

### Get the current Connection configuration

<a id="get--api-v11-connection-config-"></a>

**`GET /api/v11/connection/config/`**

Returns the current [`ConnectionConfiguration`](connection.md#ConnectionConfiguration "ConnectionConfiguration")

**Example request**:

```http
GET /api/v11/connection/config/ HTTP/1.1
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
        "ping": true,
        "is_secure_pass": false,
        "remote_access_port": 80,
        "remote_access": false,
        "wol": false,
        "adblock": false,
        "adblock_not_set": false,
        "api_remote_access": true,
        "allow_token_request": true,
        "remote_access_ip": "312.13.37.42"
    }
}
```

<a id="update-the-connection-configuration"></a>

### Update the Connection configuration

<a id="put--api-v11-connection-config-"></a>

**`PUT /api/v11/connection/config/`**

Updates the [`ConnectionConfiguration`](connection.md#ConnectionConfiguration "ConnectionConfiguration")

**Example request**:

```http
PUT /api/v11/connection/config/ HTTP/1.1
Host: mafreebox.freebox.fr

{
  "ping": true,
  "wol": false
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
        "ping": true,
        "is_secure_pass": false,
        "remote_access_port": 80,
        "remote_access": false,
        "wol": false,
        "adblock": false,
        "adblock_not_set": false,
        "api_remote_access": true,
        "allow_token_request": true,
        "remote_access_ip": "312.13.37.42"
    }
}
```

<a id="connection-ipv6-configuration"></a>

## Connection IPv6 configuration

<a id="connection-ipv6-configuration-object"></a>

### Connection IPv6 configuration object

<a id="ConnectionIpv6Delegation"></a>

#### Objet ConnectionIpv6Delegation

<a id="ConnectionIpv6Delegation.prefix"></a>

**`prefix string`**

IPv6 prefix

<a id="ConnectionIpv6Delegation.next_hop"></a>

**`next_hop ipv6`**

the next hop for the prefix

<a id="ConnectionIpv6Configuration"></a>

#### Objet ConnectionIpv6Configuration

<a id="ConnectionIpv6Configuration.ipv6_enabled"></a>

**`ipv6_enabled bool`**

is IPv6 enabled

<a id="ConnectionIpv6Configuration.ipv6_firewall"></a>

**`ipv6_firewall bool`**

is IPv6 firewall enabled

<a id="ConnectionIpv6Configuration.ipv6_prefix_firewall"></a>

**`ipv6_prefix_firewall bool`**

is IPv6 firewall enabled on secondary prefixes

<a id="ConnectionIpv6Configuration.ipv6ll"></a>

**`ipv6ll string Read-only`**

Freebox IPv6 link local address

<a id="propriete-sans-ancre-connection-32"></a>

**`ipv6_prefix_firewall bool`**

is IPv6 firewall enabled for delegated prefixes

<a id="ConnectionIpv6Configuration.delegations"></a>

**`delegations ConnectionIpv6Delegation[8]`**

list of IPv6 delegations

<a id="get-the-current-ipv6-connection-configuration"></a>

### Get the current IPv6 Connection configuration

<a id="get--api-v11-connection-ipv6-config-"></a>

**`GET /api/v11/connection/ipv6/config/`**

Returns the current [`ConnectionIpv6Configuration`](connection.md#ConnectionIpv6Configuration "ConnectionIpv6Configuration")

**Example request**:

```http
GET /api/v11/connection/ipv6/config/ HTTP/1.1
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
        "ipv6_enabled": true,
        "ipv6_firewall": false,
        "ipv6_prefix_firewall": true,
        "delegations": [
            {
                "prefix": "2a01:e30:d252:a2a0::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a1::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a2::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a3::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a4::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a5::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a6::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a7::/64",
                "next_hop": ""
            }
        ]
    }
}
```

<a id="update-the-ipv6-connection-configuration"></a>

### Update the IPv6 Connection configuration

<a id="put--api-v11-connection-ipv6-config-"></a>

**`PUT /api/v11/connection/ipv6/config/`**

Updates the [`ConnectionIpv6Configuration`](connection.md#ConnectionIpv6Configuration "ConnectionIpv6Configuration")

**Example request**:

```http
PUT /api/v11/connection/config/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "delegations": [
      {
         "prefix": "2a01:e30:d252:a2a2::/64",
         "next_hop": "fe80::be30:5bff:feb5:fcc7"
      }
   ]
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
        "ipv6_enabled": true,
        "ipv6_firewall": false,
        "ipv6_prefix_firewall": false,
        "ipv6ll": "fe80::224:d4ff:acac:ecec",
        "delegations": [
            {
                "prefix": "2a01:e30:d252:a2a0::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a1::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a2::/64",
                "next_hop": "fe80::d252:5bff:feb5:fcc7"
            },
            {
                "prefix": "2a01:e30:d252:a2a3::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a4::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a5::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a6::/64",
                "next_hop": ""
            },
            {
                "prefix": "2a01:e30:d252:a2a7::/64",
                "next_hop": ""
            }
        ]
    }
}
```

<a id="connection-xdsl-status-unstable"></a>

## Connection xDSL status [UNSTABLE]

<a id="xdsl-status-object-unstable"></a>

### xDSL status object [UNSTABLE]

<a id="XdslStatus"></a>

#### Objet XdslStatus

<a id="XdslStatus.status"></a>

**`status enum Read-only`**

| Status | Description |
| --- | --- |
| down | unsynchronized |
| training | synchronizing step 1/4 |
| started | synchronizing step 2/4 |
| chan_analysis | synchronizing step 3/4 |
| msg_exchange | synchronizing step 4/4 |
| showtime | Ready |
| disabled | Disabled |

<a id="XdslStatus.protocol"></a>

**`protocol enum Read-only`**

| Protocol | Description |
| --- | --- |
| t1413 | T1.413 |
| adsl1_a | ADSL |
| adsl2_a | ADSL2 |
| adsl2plus_a | ADSL2+ |
| readsl2 | ReachDSL |
| adsl2_m | ADSL2 annex M |
| adsl2plus_m | ADSL2+ annex M |
| unknown | Unknown |

<a id="XdslStatus.modulation"></a>

**`modulation enum Read-only`**

| Protocol | Description |
| --- | --- |
| adsl | ADSL |
| vdsl | VDSL |

<a id="XdslStatus.uptime"></a>

**`uptime int Read-only`**

uptime in seconds

<a id="xdsl-stats-object-unstable"></a>

### xDSL stats object [UNSTABLE]

<a id="XdslStats"></a>

#### Objet XdslStats

<a id="XdslStats.maxrate"></a>

**`maxrate int Read-only`**

ATM max rate in kbit/s

<a id="XdslStats.rate"></a>

**`rate int Read-only`**

ATM rate in kbit/s

<a id="XdslStats.snr"></a>

**`snr int Read-only`**

in dB

<a id="XdslStats.attn"></a>

**`attn int Read-only`**

in dB

<a id="XdslStats.snr_10"></a>

**`snr_10 int Read-only`**

in dB/10

<a id="XdslStats.attn_10"></a>

**`attn_10 int Read-only`**

in dB/10

<a id="XdslStats.fec"></a>

**`fec int Read-only`**

<a id="XdslStats.crc"></a>

**`crc int Read-only`**

<a id="XdslStats.hec"></a>

**`hec int Read-only`**

<a id="XdslStats.es"></a>

**`es int Read-only`**

<a id="XdslStats.ses"></a>

**`ses int Read-only`**

<a id="XdslStats.phyr"></a>

**`phyr bool Read-only`**

<a id="XdslStats.ginp"></a>

**`ginp bool Read-only`**

<a id="XdslStats.nitro"></a>

**`nitro bool Read-only`**

<a id="XdslStats.rxmt"></a>

**`rxmt int Read-only`**

only available when phyr is on

<a id="XdslStats.rxmt_corr"></a>

**`rxmt_corr int Read-only`**

only available when phyr is on

<a id="XdslStats.rxmt_uncorr"></a>

**`rxmt_uncorr int Read-only`**

only available when phyr is on

<a id="XdslStats.rtx_tx"></a>

**`rtx_tx int Read-only`**

only available when ginp is on

<a id="XdslStats.rtx_c"></a>

**`rtx_c int Read-only`**

only available when ginp is on

<a id="XdslStats.rtx_uc"></a>

**`rtx_uc int Read-only`**

only available when ginp is on

<a id="xdsl-infos-object-unstable"></a>

### xDSL infos object [UNSTABLE]

<a id="XdslInfos"></a>

#### Objet XdslInfos

<a id="XdslInfos.status"></a>

**`status XdslStatus`**

<a id="XdslInfos.down"></a>

**`down XdslStats`**

<a id="XdslInfos.up"></a>

**`up XdslStats`**

<a id="get-the-current-xdsl-infos"></a>

### Get the current xDSL infos

<a id="get--api-v11-connection-xdsl-"></a>

**`GET /api/v11/connection/xdsl/`**

Returns the current [`XdslInfos`](connection.md#XdslInfos "XdslInfos")

**Example request**:

```http
GET /api/v11/connection/xdsl/ HTTP/1.1
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
        "status": {
            "status": "showtime",
            "protocol": "adsl2plus_a",
            "uptime": 5017,
            "modulation": "adsl"
        },
        "down": {
            "es": 43,
            "phyr": true,
            "attn": 0,
            "snr": 7,
            "nitro": true,
            "rate": 28031,
            "hec": 0,
            "crc": 0,
            "rxmt_uncorr": 0,
            "rxmt_corr": 0,
            "ses": 43,
            "fec": 0,
            "maxrate": 30636,
            "rxmt": 0
        },
        "up": {
            "es": 0,
            "phyr": false,
            "attn": 23,
            "snr": 15,
            "nitro": true,
            "rate": 1022,
            "hec": 0,
            "crc": 0,
            "rxmt_uncorr": 0,
            "rxmt_corr": 0,
            "ses": 0,
            "fec": 0,
            "maxrate": 1022,
            "rxmt": 0
        }
    }

}
```

<a id="connection-lte-status-unstable"></a>

## Connection LTE status [UNSTABLE]

<a id="lte-radio-band-object"></a>

### LTE radio band object

<a id="LteRadioBand"></a>

#### Objet LteRadioBand

<a id="LteRadioBand.enabled"></a>

**`enabled bool`**

<a id="LteRadioBand.bandwidth"></a>

**`bandwidth int`**

<a id="LteRadioBand.rsrq"></a>

**`rsrq int`**

<a id="LteRadioBand.rsrp"></a>

**`rsrp int`**

<a id="LteRadioBand.rssi"></a>

**`rssi int`**

<a id="LteRadioBand.band"></a>

**`band int`**

<a id="LteRadioBand.pci"></a>

**`pci int`**

<a id="lte-radio-object"></a>

### LTE radio object

<a id="LteRadio"></a>

#### Objet LteRadio

<a id="LteRadio.associated"></a>

**`associated bool`**

<a id="LteRadio.plmn"></a>

**`plmn int`**

<a id="LteRadio.signal_level"></a>

**`signal_level int`**

<a id="LteRadio.gcid"></a>

**`gcid string`**

<a id="LteRadio.bands"></a>

**`bands [ro]`**

<a id="LteRadio.ue_active"></a>

**`ue_active bool`**

<a id="lte-network-object"></a>

### LTE network object

<a id="LteNetwork"></a>

#### Objet LteNetwork

<a id="LteNetwork.pdn_up"></a>

**`pdn_up bool`**

<a id="LteNetwork.has_ipv6"></a>

**`has_ipv6 bool`**

<a id="LteNetwork.ipv6_dns"></a>

**`ipv6_dns string`**

<a id="LteNetwork.ipv6"></a>

**`ipv6 string`**

<a id="LteNetwork.ipv6_netmask"></a>

**`ipv6_netmask string`**

<a id="LteNetwork.has_ipv4"></a>

**`has_ipv4 bool`**

<a id="LteNetwork.ipv4_dns"></a>

**`ipv4_dns string`**

<a id="LteNetwork.ipv4"></a>

**`ipv4 string`**

<a id="LteNetwork.ipv4_netmask"></a>

**`ipv4_netmask string`**

<a id="lte-sim-object"></a>

### LTE sim object

<a id="LteSim"></a>

#### Objet LteSim

<a id="LteSim.present"></a>

**`present bool`**

<a id="LteSim.pin_locked"></a>

**`pin_locked bool`**

<a id="LteSim.puk_remaining"></a>

**`puk_remaining int`**

<a id="LteSim.iccid"></a>

**`iccid string`**

<a id="LteSim.puk_locked"></a>

**`puk_locked bool`**

<a id="LteSim.pin_remaining"></a>

**`pin_remaining int`**

<a id="lte-tunnel-details-object"></a>

### LTE tunnel details object

<a id="LteTunnelDetails"></a>

#### Objet LteTunnelDetails

<a id="LteTunnelDetails.connected"></a>

**`connected bool`**

<a id="LteTunnelDetails.last_error"></a>

**`last_error string`**

<a id="LteTunnelDetails.tx_flows_rate"></a>

**`tx_flows_rate int`**

<a id="LteTunnelDetails.tx_max_rate"></a>

**`tx_max_rate int`**

<a id="LteTunnelDetails.tx_used_rate"></a>

**`tx_used_rate int`**

<a id="LteTunnelDetails.rx_flows_rate"></a>

**`rx_flows_rate int`**

<a id="LteTunnelDetails.rx_max_rate"></a>

**`rx_max_rate int`**

<a id="LteTunnelDetails.rx_used_rate"></a>

**`rx_used_rate int`**

<a id="lte-tunnel-object"></a>

### LTE tunnel object

<a id="LteTunnel"></a>

#### Objet LteTunnel

<a id="LteTunnel.lte"></a>

**`lte LteTunnelDetails`**

<a id="LteTunnel.xdsl"></a>

**`xdsl LteTunnelDetails`**

<a id="lte-configuration-object"></a>

### LTE configuration object

<a id="LteConfiguration"></a>

#### Objet LteConfiguration

<a id="LteConfiguration.enabled"></a>

**`enabled bool`**

<a id="LteConfiguration.radio"></a>

**`radio LteRadio`**

<a id="LteConfiguration.state"></a>

**`state string`**

<a id="LteConfiguration.network"></a>

**`network LteNetwork`**

<a id="LteConfiguration.fsm_state"></a>

**`fsm_state string`**

<a id="LteConfiguration.sim"></a>

**`sim LteSim`**

<a id="get-the-current-lte-infos"></a>

### Get the current LTE infos

<a id="get--api-v11-connection-lte-id"></a>

**`GET /api/v11/connection/lte/{id}`**

Returns the current [`LteConfiguration`](connection.md#LteConfiguration "LteConfiguration") for the given id.
Possible ids are:

- aggregation
- backup

**Example request**:

```http
GET /api/v11/connection/lte/aggregation HTTP/1.1
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
        "radio": {
            "associated": true,
            "plmn": 20202,
            "signal_level": 5,
            "gcid": "202020202020",
            "bands": [],
            "ue_active": false
        },
        "state": "connected",
        "network": {
            "ipv6_dns": "",
            "ipv6": "2a2a:e0e:beeb:eded::1",
            "ipv4_netmask": "0.0.0.0",
            "has_ipv6": true,
            "ipv4_dns": "0.0.0.0",
            "has_ipv4": alse,
            "pdn_up": true,
            "ipv6_netmask": "ffff:ffff:ffff:ffff:ffff:ffff:ffff:ff00",
            "ipv4": "0.0.0.0"
        },
        "fsm_state": "poll_network",
        "sim": {
            "present": true,
            "pin_locked": alse,
            "puk_remaining": 10,
            "iccid": "1234567890123456789",
            "puk_locked":f alse,
            "pin_remaining": 3
        },
    }
}
```

<a id="get-the-current-xdsl-lte-aggregation-infos"></a>

### Get the current xDSL/LTE aggregation infos

<a id="get--api-v11-connection-aggregation"></a>

**`GET /api/v11/connection/aggregation`**

Returns the current [`LteTunnel`](connection.md#LteTunnel "LteTunnel")

**Example request**:

```http
GET /api/v11/connection/aggregation HTTP/1.1
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
        "tunnel": {
            "lte": {
                "tx_flows_rate": 0,
                "connected": true,
                "last_error": "no_error",
                "rx_flows_rate": 0,
                "tx_max_rate": 0,
                "tx_used_rate": 0,
                "rx_max_rate": 0,
                "rx_used_rate": 0
            },
            "xdsl": {
                "tx_flows_rate": 0,
                "connected": true,
                "last_error": "no_error",
                "rx_flows_rate": 0,
                "tx_max_rate": 4428750,
                "tx_used_rate": 134,
                "rx_max_rate": 12502000,
                "rx_used_rate": 120
            }
        }
    }
}
```

<a id="update-the-xdsl-lte-aggregation-configuration"></a>

### Update the xDSL/LTE aggregation configuration

<a id="put--api-v11-connection-aggregation"></a>

**`PUT /api/v11/connection/aggregation`**

Updates the [`LteConfiguration`](connection.md#LteConfiguration "LteConfiguration")

**Example request**:

```http
PUT /api/v11/connection/aggregation/ HTTP/1.1
Host: mafreebox.freebox.fr

{
  "enabled": true
}
```

<a id="connection-ftth-status-unstable"></a>

## Connection FTTH status [UNSTABLE]

<a id="ftth-status-object-unstable"></a>

### FTTH status object [UNSTABLE]

<a id="FtthStatus"></a>

#### Objet FtthStatus

<a id="FtthStatus.sfp_present"></a>

**`sfp_present boolean Read-only`**

<a id="FtthStatus.sfp_alim_ok"></a>

**`sfp_alim_ok boolean Read-only`**

<a id="FtthStatus.sfp_has_power_report"></a>

**`sfp_has_power_report boolean Read-only`**

<a id="FtthStatus.sfp_has_signal"></a>

**`sfp_has_signal boolean Read-only`**

<a id="FtthStatus.link"></a>

**`link boolean Read-only`**

<a id="FtthStatus.sfp_serial"></a>

**`sfp_serial string Read-only`**

<a id="FtthStatus.sfp_model"></a>

**`sfp_model string Read-only`**

<a id="FtthStatus.sfp_vendor"></a>

**`sfp_vendor string Read-only`**

<a id="propriete-sans-ancre-connection-113"></a>

**`sfp_vendor string Read-only`**

<a id="FtthStatus.sfp_pwr_tx"></a>

**`sfp_pwr_tx int Read-only`**

scaled by 100 (in dBm)

<a id="FtthStatus.sfp_pwr_rx"></a>

**`sfp_pwr_rx int Read-only`**

scaled by 100 (in dBm)

<a id="get-the-current-ftth-status"></a>

### Get the current FTTH status

<a id="get--api-v11-connection-ftth-"></a>

**`GET /api/v11/connection/ftth/`**

Returns the current [`FtthStatus`](connection.md#FtthStatus "FtthStatus")

**Example request**:

```http
GET /api/v11/connection/ftth/ HTTP/1.1
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
        "sfp_has_power_report": true,
        "sfp_has_signal": false,
        "sfp_model": "SPBD-1250E4H2RDB",
        "sfp_vendor": "DELTA",
        "sfp_pwr_tx": -1172,
        "sfp_pwr_rx": -3698,
        "link": false,
        "sfp_alim_ok": true,
        "sfp_serial": "DE104900000471",
        "sfp_present": true
    }
}
```

<a id="connection-dyndns-status"></a>

## Connection DynDNS status

<a id="dyndnsprovider-status-object"></a>

### DynDnsProvider status object

<a id="DDNSStatus"></a>

#### Objet DDNSStatus

<a id="DDNSStatus.status"></a>

**`status enum`**

| Status | Description |
| --- | --- |
| disabled | Disabled |
| ok | Ok |
| wait | Updating |
| reqfail | Request failed |
| authfail | Authentication error |
| nocredential | Invalid credential |
| ipinval | Invalid IP |
| hostinval | Invalid hostname |
| abuse | Blocked because of abuse |
| dnserror | DNS error |
| unavailable | Service unavailable |
| nowan | Unable to get wan IP |
| unknown | Unknown |

<a id="DDNSStatus.next_refresh"></a>

**`next_refresh int`**

next refresh timestamp

<a id="DDNSStatus.last_refresh"></a>

**`last_refresh int`**

last refresh timestamp

<a id="DDNSStatus.next_retry"></a>

**`next_retry int`**

next retry timestamp

<a id="DDNSStatus.last_error"></a>

**`last_error int`**

last error timestamp

<a id="get-the-status-of-a-dyndns-service"></a>

### Get the status of a DynDNS service

Right now the supported dynamic dns providers are:

- ovh
- dyndns
- noip

<a id="get--api-v11-connection-ddns-provider-status-"></a>

**`GET /api/v11/connection/ddns/{provider}/status/`**

Returns the current [`DDNSStatus`](connection.md#DDNSStatus "DDNSStatus")

**Example request**:

```http
GET /api/v11/connection/ddns/dyndns/status/ HTTP/1.1
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
        "last_error": 1354127350,
        "status": "hostinval",
        "next_refresh": 0,
        "last_refresh": 0,
        "next_retry": 0
    }

}
```

<a id="connection-dyndns-configuration"></a>

## Connection DynDNS configuration

<a id="dyndns-config-object"></a>

### DynDns config object

<a id="DDNSConfig"></a>

#### Objet DDNSConfig

<a id="DDNSConfig.enabled"></a>

**`enabled bool`**

<a id="DDNSConfig.hostname"></a>

**`hostname string`**

dns name to use to register

<a id="DDNSConfig.password"></a>

**`password string Write-only`**

password to use to register

<a id="DDNSConfig.user"></a>

**`user string`**

username to use to register

<a id="get-the-config-of-a-dyndns-service"></a>

### Get the config of a DynDNS service

<a id="get--api-v11-connection-ddns-provider-"></a>

**`GET /api/v11/connection/ddns/{provider}/`**

Returns the current [`DDNSConfig`](connection.md#DDNSConfig "DDNSConfig")

**Example request**:

```http
GET /api/v11/connection/ddns/dyndns/ HTTP/1.1
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
        "hostname": "test",
        "user": "test"
    }

}
```

<a id="set-the-config-of-a-dyndns-service"></a>

### Set the config of a DynDNS service

<a id="put--api-v11-connection-ddns-provider-"></a>

**`PUT /api/v11/connection/ddns/{provider}/`**

Set the [`DDNSConfig`](connection.md#DDNSConfig "DDNSConfig")

**Example request**:

```http
PUT /api/v11/connection/ddns/dyndns/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "enabled": false,
   "user": "test",
   "password": "ssss",
   "hostname": "ttt"
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
        "hostname": "ttt",
        "user": "test"
    }

}
```
