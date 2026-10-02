<a id="ftp"></a>

# Ftp

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#ftp)

## Navigation

- [Ftp Errors](#ftp-errors)
- [Ftp Config](#ftp-config)
- [Ftp config API](#ftp-config-api)


The FTP API allow you to control the Freebox ftp server settings

<a id="ftp-errors"></a>

## Ftp Errors

When attempting to access the FTP API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| internal_error | Internal error |
| weak_password | Password is too weak for remote access |

<a id="ftp-config"></a>

## Ftp Config

FtpConfig has the following attributes:

<a id="FtpConfig"></a>

### Objet FtpConfig

<a id="FtpConfig.enabled"></a>

**`enabled bool`**

is the FTP server enabled

<a id="FtpConfig.allow_anonymous"></a>

**`allow_anonymous bool`**

can anonymous user log in

<a id="FtpConfig.allow_anonymous_write"></a>

**`allow_anonymous_write bool`**

can anonymous user write data

<a id="FtpConfig.username"></a>

**`username string Read-only`**

default user name to use. Cannot be changed

<a id="FtpConfig.password"></a>

**`password string Write-only`**

user password

<a id="FtpConfig.allow_remote_access"></a>

**`allow_remote_access bool`**

enable ftp server remote access

NOTE: to be able to enable the remote
access the password must be strong enough

<a id="FtpConfig.weak_password"></a>

**`weak_password bool Read-only`**

is the ftp password weak (in this case
remote access is disabled)

<a id="FtpConfig.port_ctrl"></a>

**`port_ctrl int`**

ftp control port to use for remote access

<a id="FtpConfig.port_data"></a>

**`port_data int`**

ftp data port to use for remote access

<a id="FtpConfig.remote_domain"></a>

**`remote_domain string`**

domain name to use for remote access

<a id="ftp-config-api"></a>

## Ftp config API

<a id="get-the-current-ftp-configuration"></a>

### Get the current Ftp configuration

<a id="get--api-v8-ftp-config-"></a>

**`GET /api/v8/ftp/config/`**

Get the [`FtpConfig`](ftp.md#FtpConfig "FtpConfig")

**Example request**:

```http
GET /api/v8/ftp/config/ HTTP/1.1
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
        "allow_anonymous": false,
        "allow_remote_access": false,
        "port_ctrl": 3615,
        "port_data": 1337,
        "weak_password": true,
        "allow_anonymous_write": false
    }
}
```

<a id="update-the-ftp-configuration"></a>

### Update the FTP configuration

<a id="put--api-v8-ftp-config-"></a>

**`PUT /api/v8/ftp/config/`**

Update the [`FtpConfig`](ftp.md#FtpConfig "FtpConfig")

**Example request**:

```http
PUT /api/v8/ftp/config/ HTTP/1.1
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
        "allow_anonymous": false,
        "allow_anonymous_write": false
    }
}
```
