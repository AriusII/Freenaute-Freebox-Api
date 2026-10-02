<a id="tftp"></a>

# TFTP

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#tftp)

## Navigation

- [TFTP Errors](#tftp-errors)
- [TFTP Config](#tftp-config)
- [TFTP Config API](#tftp-config-api)


The TFTP API allow you to control the Freebox tftp server settings

<a id="tftp-errors"></a>

## TFTP Errors

When attempting to access the TFTP API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| absolute | The path must be absolute |

<a id="tftp-config"></a>

## TFTP Config

TftpConfig has the following attributes:

<a id="TftpConfig"></a>

### Objet TftpConfig

<a id="TftpConfig.enabled"></a>

**`enabled bool`**

is the TFTP server enabled

<a id="TftpConfig.root"></a>

**`root string`**

is the base64 encoded absolute path to the root directory exposed by the server.
This path points to a folder inside the storage device (My Freebox).

<a id="tftp-config-api"></a>

## TFTP Config API

<a id="get-the-current-tftp-configuration"></a>

### Get the current TFTP configuration

<a id="get--api-v16-tftp-config-"></a>

**`GET /api/v16/tftp/config/`**

Get the [`TftpConfig`](tftp.md#TftpConfig "TftpConfig")

**Example request**:

```http
GET /api/v16/tftp/config/ HTTP/1.1
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
        "root": "/ssd2"
    }
}
```

<a id="update-the-tftp-configuration"></a>

### Update the TFTP configuration

<a id="put--api-latest-tftp-config-"></a>

**`PUT /api/latest/tftp/config/`**

Update the [`TftpConfig`](tftp.md#TftpConfig "TftpConfig")

**Example request**:

```http
PUT /api/v16/tftp/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "enabled": true
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
        "root": "/ssd2"
    }
}
```
