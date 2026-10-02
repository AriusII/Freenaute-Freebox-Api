<a id="network-share"></a>

# Network Share

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#network-share)

## Navigation

- [Network Share Errors](#network-share-errors)
- [Samba Config](#samba-config)
- [Samba config API](#samba-config-api)
- [Afp Config](#afp-config)
- [Afp config API](#afp-config-api)


The network share API allow you to control the file sharing services
running on the Freebox.

<a id="network-share-errors"></a>

## Network Share Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| invalid_workgroup_name | Invalid workgroup name |
| invalid_logon_user | Invalid samba user name |
| invalid_logon_password | Invalid samba user password |
| invalid_afp_login_name | Invalid AFP user name |
| invalid_afp_login_password | Invalid AFP user password |

<a id="samba-config"></a>

## Samba Config

SambaConfig has the following attributes:

<a id="SambaConfig"></a>

### Objet SambaConfig

<a id="SambaConfig.file_share_enabled"></a>

**`file_share_enabled bool`**

is file sharing enabled

<a id="SambaConfig.print_share_enabled"></a>

**`print_share_enabled bool`**

is printer sharing enabled

<a id="SambaConfig.logon_enabled"></a>

**`logon_enabled bool`**

is login/password required to access shares

<a id="SambaConfig.logon_user"></a>

**`logon_user string`**

samba user name

<a id="SambaConfig.logon_password"></a>

**`logon_password string Write-only`**

samba user password

<a id="SambaConfig.workgroup"></a>

**`workgroup string`**

name of the workgroup

<a id="SambaConfig.smbv2_enabled"></a>

**`smbv2_enabled bool`**

Set to true to enable SMBv2/v3

<a id="samba-config-api"></a>

## Samba config API

<a id="get-the-current-samba-configuration"></a>

### Get the current Samba configuration

<a id="get--api-v8-netshare-samba-"></a>

**`GET /api/v8/netshare/samba/`**

Get the [`SambaConfig`](network_share.md#SambaConfig "SambaConfig")

**Example request**:

```http
GET /api/v8/netshare/samba/ HTTP/1.1
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
        "workgroup": "WORKGROUP",
        "print_share_enabled": true,
        "file_share_enabled": true,
        "logon_enabled": false,
        "logon_user": "freebox"
    }
}
```

<a id="update-the-samba-configuration"></a>

### Update the Samba configuration

<a id="put--api-v8-netshare-samba-"></a>

**`PUT /api/v8/netshare/samba/`**

Update the [`SambaConfig`](network_share.md#SambaConfig "SambaConfig")

**Example request**:

```http
PUT /api/v8/netshare/samba/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "print_share_enabled": false
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
        "workgroup": "WORKGROUP",
        "print_share_enabled": false,
        "file_share_enabled": true,
        "logon_enabled": false,
        "logon_user": "freebox"
    }
}
```

<a id="afp-config"></a>

## Afp Config

AfpConfig has the following attributes:

<a id="AfpConfig"></a>

### Objet AfpConfig

<a id="AfpConfig.enabled"></a>

**`enabled bool`**

is afp service enabled

<a id="AfpConfig.guest_allow"></a>

**`guest_allow bool`**

allow guest to access shared files

<a id="AfpConfig.server_type"></a>

**`server_type enum`**

Afp server type (to display proper icon) in MacOS

valid server types are:

| server_type |  |
| --- | --- |
| powerbook |  |
| powermac |  |
| macmini |  |
| imac |  |
| macbook |  |
| macbookpro |  |
| macbookair |  |
| macpro |  |
| appletv |  |
| airport |  |
| xserve |  |

<a id="AfpConfig.login_name"></a>

**`login_name string`**

Afp user name

<a id="AfpConfig.login_password"></a>

**`login_password string Write-only`**

Afp user password

<a id="afp-config-api"></a>

## Afp config API

<a id="get-the-current-afp-configuration"></a>

### Get the current Afp configuration

<a id="get--api-v8-netshare-afp-"></a>

**`GET /api/v8/netshare/afp/`**

Get the [`AfpConfig`](network_share.md#AfpConfig "AfpConfig")

**Example request**:

```http
GET /api/v8/netshare/afp/ HTTP/1.1
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
        "guest_allow": true,
        "login_name": "freebox",
        "server_type": "airport"
    }
}
```

<a id="update-the-afp-configuration"></a>

### Update the Afp configuration

<a id="put--api-v8-netshare-afp-"></a>

**`PUT /api/v8/netshare/afp/`**

Update the [`AfpConfig`](network_share.md#AfpConfig "AfpConfig")

**Example request**:

```http
PUT /api/v8/netshare/afp/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "guest_allow": false
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
        "guest_allow": false,
        "login_name": "freebox",
        "server_type": "airport"
    }
}
```
