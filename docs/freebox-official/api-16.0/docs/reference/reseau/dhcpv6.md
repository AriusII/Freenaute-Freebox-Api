<a id="dhcpv6"></a>

# DHCPv6

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#dhcpv6)

## Navigation

- [DHCPv6 Errors](#dhcpv6-errors)
- [DHCPv6 Config Object](#dhcpv6-config-object)
- [DHCPv6 Configuration API](#dhcpv6-configuration-api)


With the DHCPv6 API you configure the Freebox DHCPv6 server, and access
its status.

<a id="dhcpv6-errors"></a>

## DHCPv6 Errors

When attempting to access the DHCPv6 API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| inval | invalid parameter |
| noent | no such entry |
| nospc | too many entries |
| exist | already exists |
| conflict | conflict with another rule |
| nomem | internal error |

<a id="dhcpv6-config-object"></a>

## DHCPv6 Config Object

DHCPv6 config has the following attributes:

<a id="DHCPv6Config"></a>

### Objet DHCPv6Config

<a id="DHCPv6Config.enabled"></a>

**`enabled bool`**

Enable/Disable the DHCPv6 server

NOTE: on some Android devices, enabling the DHCPv6 server may cause IPv6
to stop working on those devices

<a id="DHCPv6Config.use_custom_dns"></a>

**`use_custom_dns bool`**

if set to true, the user provided IPv6 dns servers will be used instead
of Free default IPv6 dns servers

NOTE: even if DHCPv6 server is disabled the custom dns can be used to
replace Free dns in RA RDNSS

<a id="DHCPv6Config.dns"></a>

**`dns [] array of ipv6 Read-only`**

list of ipv6 dns servers to use instead of Free dns servers in case
use_custom_dns is set to true

<a id="dhcpv6-configuration-api"></a>

## DHCPv6 Configuration API

<a id="get-the-current-dhcpv6-configuration"></a>

### Get the current DHCPv6 configuration

<a id="get--api-v8-dhcpv6-config-"></a>

**`GET /api/v8/dhcpv6/config/`**

Returns the current [`DHCPv6Config`](dhcpv6.md#DHCPv6Config "DHCPv6Config")

**Example request**:

```http
GET /api/v8/dhcpv6/config/ HTTP/1.1
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
            "use_custom_dns": false,
            "dns": [
                  "2620:0:ccc::a",
                  "2620:0:ccc::1"
            ]
      }
}
```

<a id="update-the-current-dhcpv6-configuration"></a>

### Update the current DHCPv6 configuration

<a id="put--api-v8-dhcpv6-config-"></a>

**`PUT /api/v8/dhcpv6/config/`**

Update the current [`DHCPv6Config`](dhcpv6.md#DHCPv6Config "DHCPv6Config")

**Example request**:

```http
PUT /api/v8/dhcpv6/config/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "use_custom_dns": true,
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
            "use_custom_dns": true,
            "dns": [
                  "2620:0:ccc::a",
                  "2620:0:ccc::1"
            ]
      }
}
```
