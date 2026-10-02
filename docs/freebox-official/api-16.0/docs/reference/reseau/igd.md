<a id="upnp-igd"></a>

# UPnP IGD

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#upnp-igd)

## Navigation

- [UPnP IGD Errors](#upnp-igd-errors)
- [UPnP IGD Config](#upnp-igd-config)
- [UPnP IGD config API](#upnp-igd-config-api)
- [UPnP IGD Redirection](#upnp-igd-redirection)
- [UPnP IGD Redirection API](#upnp-igd-redirection-api)


The UPnP IGD API allow you to control the settings of the Universal
Plug n’ Play Internet Gateway Device service. This service allow
hosts on your local network to manage nat redirections.

<a id="upnp-igd-errors"></a>

## UPnP IGD Errors

When attempting to access the UPnP IGD API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| disabled | the service is disabled |
| noent | invalid rule id |

<a id="upnp-igd-config"></a>

## UPnP IGD Config

UPnPIGDConfig has the following attributes:

<a id="UPnPIGDConfig"></a>

### Objet UPnPIGDConfig

<a id="UPnPIGDConfig.enabled"></a>

**`enabled bool`**

is the UPnP IGD service enabled

<a id="UPnPIGDConfig.version"></a>

**`version int`**

UPnP IGD protocol version
Supported values are 1 / 2

<a id="upnp-igd-config-api"></a>

## UPnP IGD config API

<a id="get-the-current-upnp-igd-configuration"></a>

### Get the current UPnP IGD configuration

<a id="get--api-v8-upnpigd-config-"></a>

**`GET /api/v8/upnpigd/config/`**

Get the [`UPnPIGDConfig`](igd.md#UPnPIGDConfig "UPnPIGDConfig")

**Example request**:

```http
GET /api/v8/upnpigd/config/ HTTP/1.1
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
        "version": 1
    }
}
```

<a id="update-the-upnp-igd-configuration"></a>

### Update the UPnP IGD configuration

<a id="put--api-v8-upnpigd-config-"></a>

**`PUT /api/v8/upnpigd/config/`**

Update the [`UPnPIGDConfig`](igd.md#UPnPIGDConfig "UPnPIGDConfig")

**Example request**:

```http
PUT /api/v8/upnpigd/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "enabled": true,
   "version": 2
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
        "version": 2
    }
}
```

<a id="upnp-igd-redirection"></a>

## UPnP IGD Redirection

UPnPRedir has the following attributes:

<a id="UPnPRedir"></a>

### Objet UPnPRedir

<a id="UPnPRedir.id"></a>

**`id string Read-only`**

the redirection id

<a id="UPnPRedir.enabled"></a>

**`enabled bool Read-only`**

is the redirection enabled

<a id="UPnPRedir.ext_src_ip"></a>

**`ext_src_ip string Read-only`**

source IP

<a id="UPnPRedir.ext_port"></a>

**`ext_port int Read-only`**

external port

<a id="UPnPRedir.int_ip"></a>

**`int_ip string Read-only`**

the target IP on your LAN

<a id="UPnPRedir.int_port"></a>

**`int_port int Read-only`**

the target port on your LAN

<a id="UPnPRedir.proto"></a>

**`proto string Read-only`**

the IP protocol to redirect

<a id="UPnPRedir.desc"></a>

**`desc string Read-only`**

a description

<a id="UPnPRedir.remaining"></a>

**`remaining int Read-only`**

seconds remaining before redirection expire

<a id="UPnPRedir.host"></a>

**`host LanHost Read-only`**

lan host if available

<a id="upnp-igd-redirection-api"></a>

## UPnP IGD Redirection API

<a id="get-the-list-of-current-redirection"></a>

### Get the list of current redirection

<a id="get--api-v8-upnpigd-redir-"></a>

**`GET /api/v8/upnpigd/redir/`**

Get the list of [`UPnPRedir`](igd.md#UPnPRedir "UPnPRedir") redirections

**Example request**:

```http
GET /api/v8/upnpigd/redir/ HTTP/1.1
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
            "proto": "udp",
            "id": "0.0.0.0-53644-udp",
            "desc": "iC53644",
            "remaining": 0,
            "ext_src_ip": "0.0.0.0",
            "int_port": 16402,
            "int_ip": "192.168.1.44",
            "ext_port": 53644
        }
    ]
}
```

<a id="delete-a-redirection"></a>

### Delete a redirection

<a id="delete--api-v8-upnpigd-redir-id"></a>

**`DELETE /api/v8/upnpigd/redir/{id}`**

Deletes the given [`UPnPRedir`](igd.md#UPnPRedir "UPnPRedir")

**Example request**:

```http
GET /api/v8/upnpigd/redir/0.0.0.0-53644-udp HTTP/1.1
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
