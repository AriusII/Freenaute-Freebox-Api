<a id="upnp-av"></a>

# UPnP AV

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#upnp-av)

## Navigation

- [UPnP AV Errors](#upnp-av-errors)
- [UPnP AV Config](#upnp-av-config)
- [UPnP AV config API](#upnp-av-config-api)


The UPnP AV API allow you to control the settings of the Freebox UPnP
AV service.

<a id="upnp-av-errors"></a>

## UPnP AV Errors

When attempting to access the UPnP AV API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| internal_error | internal error |

<a id="upnp-av-config"></a>

## UPnP AV Config

UPnPAVConfig has the following attributes:

<a id="UPnPAVConfig"></a>

### Objet UPnPAVConfig

<a id="UPnPAVConfig.enabled"></a>

**`enabled bool`**

is the UPnP AV service enabled

<a id="upnp-av-config-api"></a>

## UPnP AV config API

<a id="get-the-current-upnp-av-configuration"></a>

### Get the current UPnP AV configuration

<a id="get--api-v8-upnpav-config-"></a>

**`GET /api/v8/upnpav/config/`**

Get the [`UPnPAVConfig`](upnpav.md#UPnPAVConfig "UPnPAVConfig")

**Example request**:

```http
GET /api/v8/upnpav/config/ HTTP/1.1
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
        "enabled": true
    }
}
```

<a id="update-the-upnp-av-configuration"></a>

### Update the UPnP AV configuration

<a id="put--api-v8-upnpav-config-"></a>

**`PUT /api/v8/upnpav/config/`**

Update the [`UPnPAVConfig`](upnpav.md#UPnPAVConfig "UPnPAVConfig")

**Example request**:

```http
PUT /api/v8/upnpigd/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
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
        "enabled": false
    }
}
```
