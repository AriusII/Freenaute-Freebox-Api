<a id="dhcp"></a>

# DHCP

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#dhcp)

## Navigation

- [DHCP Errors](#dhcp-errors)
- [DHCP Config Object](#dhcp-config-object)
- [DHCP Option Object](#dhcp-option-object)
- [DHCP Configuration API](#dhcp-configuration-api)
- [DHCP Static Lease Object](#dhcp-static-lease-object)
- [DHCP Static Lease API](#dhcp-static-lease-api)
- [DHCP Dynamic Lease Object](#dhcp-dynamic-lease-object)


With the DHCP API you configure the Freebox dhcp server, and access
its status.

<a id="dhcp-errors"></a>

## DHCP Errors

When attempting to access the DHCP API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| inval | invalid argument |
| inval_netmask | invalid netmask |
| inval_ip_range | invalid IP range |
| inval_ip_range_net | IP range & netmask mismatch |
| inval_gw_net | gateway & netmask mismatch |
| exist | already exists |
| nodev | no such device |
| noent | no such entry |
| netdown | network is down |
| busy | device or resource busy |

<a id="dhcp-config-object"></a>

## DHCP Config Object

DHCP config has the following attributes:

<a id="DhcpConfig"></a>

### Objet DhcpConfig

<a id="DhcpConfig.enabled"></a>

**`enabled bool`**

Enable/Disable the DHCP server

<a id="DhcpConfig.sticky_assign"></a>

**`sticky_assign bool`**

Always assign the same IP to a given host

<a id="DhcpConfig.gateway"></a>

**`gateway string Read-only`**

Gateway IP address

<a id="DhcpConfig.netmask"></a>

**`netmask string Read-only`**

Gateway subnet netmask

<a id="DhcpConfig.ip_range_start"></a>

**`ip_range_start string`**

DHCP range start IP

<a id="DhcpConfig.ip_range_end"></a>

**`ip_range_end string`**

DHCP range end IP

<a id="DhcpConfig.always_broadcast"></a>

**`always_broadcast bool`**

Always broadcast DHCP responses

<a id="DhcpConfig.ignore_out_of_range_hint"></a>

**`ignore_out_of_range_hint bool`**

Ignore requested address if it is outside of the DHCP range

<a id="DhcpConfig.boot_server"></a>

**`boot_server string`**

Address of the TFTP server used when booting via TFTP.

<a id="DhcpConfig.boot_file"></a>

**`boot_file string`**

Boot file to download from the TFTP server when booting via TFTP.

<a id="DhcpConfig.dns"></a>

**`dns [] array of string`**

List of dns servers to include in DHCP reply

<a id="DhcpConfig.options"></a>

**`options [] array of DhcpOption`**

List of dns options to include in DHCP reply

<a id="dhcp-option-object"></a>

## DHCP Option Object

DHCP options have the following attributes

<a id="DhcpOption"></a>

### Objet DhcpOption

<a id="DhcpOption.id"></a>

**`id string Read-only`**

The valid option identifiers and types are:

| Identifier | Type | Description |
| --- | --- | --- |
| time_offset | s32 | Time offset |
| time_server | ip_list | Time server |
| log_server | ip_list | Log server |
| cookie_server | ip_list | Cookie server |
| lpr_server | ip_list | LPR server |
| impress_server | ip_list | Impress server |
| resource_location_server | ip_list | Resource location server |
| hostname | string | Hostname |
| merit_dump_file | string | Merit dump file |
| domain_name | string | Domain name |
| swap_server | ip_list | Swap server |
| root_path | string | Root path |
| extensions_path | string | Extensions path |
| ip_fwd | bool | IP forwarding |
| ip_fwd_non_local | bool | Non-local IP source routing |
| ip_max_reassembly_size | u16 | Maximum IP reassembly size |
| ip_ttl | u8 | Default IP TTL |
| ip_pmtu_timeout | u32 | IP Path MTU timeout |
| mtu | u16 | Interface MTU |
| local_subnets | bool | All subnets are local |
| mask_discovery | bool | Perform mask discovery |
| mask_supplier | bool | Mask supplier |
| perform_rd | bool | Perform router discovery |
| rs_address | ip | Router solicitation address |
| trailer_encapsulation | bool | Trailer encapsulation |
| arp_cache_timeout | u32 | ARP cache timeout |
| eth_encapsulation | bool | Ethernet encapsulation |
| tcp_ttl | u8 | Default TCP TTL |
| tcp_keepalive_interval | u32 | TCP keepalive interval |
| tcp_keepalive_garbage | bool | TCP keepalive garbage |
| nis_domain | string | NIS domain |
| nis_server | ip_list | NIS server |
| ntp_server | ip_list | NTP server |
| vendor_specific | hexstring | Vendor specific information |
| nis_plus_domain | string | NIS+ domain |
| nis_plus_server | ip_list | NIS+ server |
| tftp_server_name | string | TFTP server name |
| bootfile_name | string | Bootfile name |
| mobile_ip_agent | ip_list | Mobile IP home agent |
| smtp_server | ip_list | SMTP server |
| pop3_server | ip_list | POP3 server |
| nntp_server | ip_list | NNTP server |
| www_server | ip_list | Default WWW server |
| finger_server | ip_list | Default Finger server |
| irc_server | ip_list | Default IRC server |
| streettalk_server | ip_list | StreetTalk server |
| stda_server | ip_list | StreetTalk directory assistance server |
| slp_directory_agent | ip_list | SLP directory agent |
| slp_service_scope | hexstring | SLP service scope |
| nds_servers | ip_list | NDS servers |
| nds_tree_name | string | NDS tree name |
| nds_context | string | NDS context |
| ldap_servers | ip_list | LDAP servers |
| timezone_posix | string | Timezone POSIX |
| timezone_database | string | Timezone database |
| name_service | hexstring | Name service |
| domain_search | hexstring | Domain search |
| classless_static_route | hexstring | Classless static route |
| capwap_ac | ip_list | CAPWAP access controller |
| tftp_server_address | ip_list | TFTP server address |

<a id="DhcpOption.val"></a>

**`val string`**

The value sent by the DHCP server when this option is requested
by the client.

The formats depend on the option type:

- ip: A single IPv4 address (as described in RFC 791)
- ip_list: A comma-separated list of IPv4 addresses
- string: A string of ASCII characters
- hexstring: A string of ASCII hexadecimal characters [0-9a-fA-F] representing a binary value (example: C0A801FE)
- bool: one of [ ‘true’, ‘false’, ‘1’, ‘0’ ]
- s8, s16, s32: An n-bit signed integer value
- u8, u16, u32: An n-bit unsigned integer value

<a id="dhcp-configuration-api"></a>

## DHCP Configuration API

<a id="get-the-current-dhcp-configuration"></a>

### Get the current DHCP configuration

<a id="get--api-v16-dhcp-config-"></a>

**`GET /api/v16/dhcp/config/`**

Returns the current [`DhcpConfig`](dhcp.md#DhcpConfig "DhcpConfig")

**Example request**:

```http
GET /api/v16/dhcp/config/ HTTP/1.1
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
        "gateway": "192.168.1.254",
        "sticky_assign": true,
        "ip_range_end": "192.168.1.50",
        "netmask": "255.255.255.0",
        "boot_server": "",
        "boot_file": "",
        "dns": [
            "192.168.1.254",
            "",
            "",
            "",
            ""
        ],
        "always_broadcast": false,
        "ip_range_start": "192.168.1.2",
        "options": [
            {
               "id" : "ip_fwd",
               "val" : "true"
            },
            {
               "id" : "tcp_ttl",
               "val" : "64"
            },
            {
               "id" : "ntp_server",
               "val" : "192.168.1.38, 192.168.1.42"
            },
            {
               "id" : "log_server",
               "val" : "192.168.1.38"
            }
        ]
    }
}
```

<a id="update-the-current-dhcp-configuration"></a>

### Update the current DHCP configuration

<a id="put--api-v16-dhcp-config-"></a>

**`PUT /api/v16/dhcp/config/`**

Update the current [`DhcpConfig`](dhcp.md#DhcpConfig "DhcpConfig")

**Example request**:

```http
PUT /api/v16/dhcp/config/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "enabled": false,
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
        "gateway": "192.168.1.254",
        "sticky_assign": true,
        "ip_range_end": "192.168.1.50",
        "netmask": "255.255.255.0",
        "dns": [
            "192.168.1.254",
            "",
            "",
            "",
            ""
        ],
        "always_broadcast": false,
        "ip_range_start": "192.168.1.2",
        "options": [
            {
               "id" : "ip_fwd",
               "val" : "true"
            },
            {
               "id" : "tcp_ttl",
               "val" : "64"
            },
            {
               "id" : "ntp_server",
               "val" : "192.168.1.38, 192.168.1.42"
            },
            {
               "id" : "log_server",
               "val" : "192.168.1.38"
            }
        ]
    }
}
```

<a id="dhcp-static-lease-object"></a>

## DHCP Static Lease Object

DHCP static lease have the following attributes

<a id="DhcpStaticLease"></a>

### Objet DhcpStaticLease

<a id="DhcpStaticLease.id"></a>

**`id string`**

DHCP static lease object id

<a id="DhcpStaticLease.mac"></a>

**`mac string`**

Host mac address

<a id="DhcpStaticLease.comment"></a>

**`comment string`**

an optional comment

<a id="DhcpStaticLease.hostname"></a>

**`hostname string Read-only`**

hostname matching the mac address

<a id="DhcpStaticLease.ip"></a>

**`ip string`**

IPv4 to assign to the host

<a id="DhcpStaticLease.host"></a>

**`host LanHost Read-only`**

LAN host information from LAN browser (refer to
[`LanHost`](lan.md#LanHost "LanHost") documentation)

<a id="DhcpStaticLease.options"></a>

**`options [] array of DhcpOption`**

List of dns options to include in DHCP reply

<a id="dhcp-static-lease-api"></a>

## DHCP Static Lease API

<a id="get-the-list-of-dhcp-static-leases"></a>

### Get the list of DHCP static leases

You can get the list of [`DhcpStaticLease`](dhcp.md#DhcpStaticLease "DhcpStaticLease") using this
API

<a id="get--api-v16-dhcp-static_lease-"></a>

**`GET /api/v16/dhcp/static_lease/`**

**Example request**:

```http
GET /api/v16/dhcp/static_lease/ HTTP/1.1
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
            "mac": "00:DE:AD:B0:0B:55",
            "comment": "",
            "hostname": "Pc de r0ro",
            "id": "00:DE:AD:B0:0B:55",
            "host": {
               [ ... ]
            },
            "ip": "192.168.1.1",
            "options": [
                {
                   "id" : "log_server",
                   "val" : "192.168.1.38"
                }
            ]
        },
        {
            "mac": "00:DE:AD:B0:0B:69",
            "comment": "",
            "hostname": "Imprimante",
            "id": "00:DE:AD:B0:0B:69",
            "host": {
               [ ... ]
            },
            "ip": "192.168.1.2",
            options: []
        }
    ]
}
```

<a id="get-a-given-dhcp-static-lease"></a>

### Get a given DHCP static lease

You can get a specific [`DhcpStaticLease`](dhcp.md#DhcpStaticLease "DhcpStaticLease") with its id

<a id="get--api-v16-dhcp-static_lease-id"></a>

**`GET /api/v16/dhcp/static_lease/{id}`**

**Example request**:

```http
GET /api/v16/dhcp/static_lease/00:DE:AD:B0:0B:55 HTTP/1.1
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
         "mac": "00:DE:AD:B0:0B:55",
         "comment": "",
         "hostname": "Pc de r0ro",
         "id": "00:DE:AD:B0:0B:55",
         "host": {
            [ ... ]
         },
         "ip": "192.168.1.1",
         "options": [
             {
                "id" : "log_server",
                "val" : "192.168.1.38"
             }
         ]
     }
}
```

<a id="update-dhcp-static-lease"></a>

### Update DHCP static lease

You can update a [`DhcpStaticLease`](dhcp.md#DhcpStaticLease "DhcpStaticLease") with this method

<a id="put--api-v16-dhcp-static_lease-id"></a>

**`PUT /api/v16/dhcp/static_lease/{id}`**

**Example request**:

```http
PUT /api/v16/dhcp/static_lease/00:DE:AD:B0:0B:55 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "comment": "Mon PC"
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
         "mac": "00:DE:AD:B0:0B:55",
         "comment": "Mon PC",
         "hostname": "Pc de r0ro",
         "id": "00:DE:AD:B0:0B:55",
         "host": {
            [ ... ]
         },
         "ip": "192.168.1.1",
         "options": [
             {
                "id" : "log_server",
                "val" : "192.168.1.38"
             }
         ]
     }
}
```

<a id="delete-a-dhcp-static-lease"></a>

### Delete a DHCP static lease

Deletes the [`DhcpStaticLease`](dhcp.md#DhcpStaticLease "DhcpStaticLease") with this id

<a id="delete--api-v8-dhcp-static_lease-id"></a>

**`DELETE /api/v8/dhcp/static_lease/{id}`**

**Example request**:

```http
DELETE /api/v8/dhcp/static_lease/00:DE:AD:B0:0B:55 HTTP/1.1
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
}
```

<a id="add-a-dhcp-static-lease"></a>

### Add a DHCP static lease

<a id="post--api-v16-dhcp-static_lease-"></a>

**`POST /api/v16/dhcp/static_lease/`**

**Example request**:

```http
POST /api/v16/dhcp/static_lease/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "ip": "192.168.1.222",
   "mac": "00:00:00:11:11:11"
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
        "mac": "00:00:00:11:11:11",
        "comment": "",
        "hostname": "00:00:00:11:11:11",
        "id": "00:00:00:11:11:11",
        "ip": "192.168.1.222",
        "options": []
    }
}
```

<a id="dhcp-dynamic-lease-object"></a>

## DHCP Dynamic Lease Object

DHCP dynamic lease have the following attributes

<a id="DhcpDynamicLease"></a>

### Objet DhcpDynamicLease

<a id="DhcpDynamicLease.mac"></a>

**`mac string Read-only`**

Host mac address

<a id="DhcpDynamicLease.hostname"></a>

**`hostname string Read-only`**

hostname matching the mac address

<a id="DhcpDynamicLease.ip"></a>

**`ip string Read-only`**

IPv4 assigned to the host

<a id="DhcpDynamicLease.lease_remaining"></a>

**`lease_remaining int Read-only`**

time left before lease needs to be refreshed

<a id="DhcpDynamicLease.assign_time"></a>

**`assign_time timestamp Read-only`**

timestamp of the lease first assignment

<a id="DhcpDynamicLease.refresh_time"></a>

**`refresh_time timestamp Read-only`**

timestamp of the last lease refresh

<a id="DhcpDynamicLease.is_static"></a>

**`is_static bool Read-only`**

is the lease static

<a id="DhcpDynamicLease.host"></a>

**`host LanHost Read-only`**

LAN host information from LAN browser (refer to
[`LanHost`](lan.md#LanHost "LanHost") documentation)

<a id="get-the-list-of-dhcp-dynamic-leases"></a>

### Get the list of DHCP dynamic leases

You can get the list of [`DhcpDynamicLease`](dhcp.md#DhcpDynamicLease "DhcpDynamicLease") using this
API

<a id="get--api-v16-dhcp-dynamic_lease-"></a>

**`GET /api/v16/dhcp/dynamic_lease/`**

**Example request**:

```http
GET /api/v16/dhcp/dynamic_lease/ HTTP/1.1
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
            "mac": "13:37:00:00:01:03",
            "host": {
               "l2ident": {
               "id": "13:37:00:00:01:03",
                  "type": "mac_address"
               },
               "active": true,
               "id": "ether-13:37:00:00:01:03",
               "last_time_reachable": 1555555555,
               "persistent": false,
               "names": [],
               "vendor_name": "",
               "host_type": "",
               "primary_name": "",
               "l3connectivities": [
                  {
                     "addr": "192.168.1.1",
                     "active": true,
                     "reachable": true,
                     "last_activity": 1555555555,
                     "af": "ipv4",
                     "last_time_reachable": 1555555555
                  },
                  {
                     "addr": "fe80::ffff:3333:eeee:eee",
                     "active": false,
                     "reachable": false,
                     "last_activity": 1555585108,
                     "af": "ipv6",
                     "last_time_reachable": 1555585103
                  }
               ],
               "reachable": true,
               "last_activity": 1555555555,
               "primary_name_manual": false,
               "interface": "pub"
                            }
            "refresh_time": 1555555555,
            "hostname": "android r0ro",
            "assign_time": 1555555555,
            "lease_remaining": 123456,
            "is_static": false,
            "ip": "192.168.1.22",
            "options": [
                {
                   "id" : "ip_fwd",
                   "val" : "true"
                },
                {
                   "id" : "tcp_ttl",
                   "val" : "64"
                },
                {
                   "id" : "ntp_server",
                   "val" : "192.168.1.38, 192.168.1.42"
                },
                {
                   "id" : "log_server",
                   "val" : "192.168.1.38"
                }
            ]
        }
    ]
}
```
