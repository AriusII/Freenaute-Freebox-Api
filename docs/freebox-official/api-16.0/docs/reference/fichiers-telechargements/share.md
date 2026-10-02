<a id="file-sharing-link"></a>

# File Sharing Link

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#file-sharing-link)

## Navigation

- [File Sharing Errors](#file-sharing-errors)
- [File Sharing Link object](#file-sharing-link-object)
- [File Sharing Link API](#file-sharing-link-api)


This API allows you to create a unique link to share content hosted on
you Freebox.

NOTE: this feature is available only if you enable HTTP remote access
to your Freebox.

<a id="file-sharing-errors"></a>

## File Sharing Errors

When attempting to access the file sharing API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| invalid_id | Invalid object id |
| path_not_found | File or folder not found |
| internal_error | Internal error |

<a id="file-sharing-link-object"></a>

## File Sharing Link object

Share link have the following attributes:

<a id="ShareLink"></a>

### Objet ShareLink

<a id="ShareLink.token"></a>

**`token string Read-only`**

The link unique sharing token

<a id="ShareLink.path"></a>

**`path string Read-only`**

The root path of the share, if the path is a regular file, only
this file will be shared

<a id="ShareLink.name"></a>

**`name string Read-only`**

The readable name of the shared file/folder

<a id="ShareLink.expire"></a>

**`expire timestamp Read-only`**

Link expiration timestamp, 0 means no expiration.

<a id="ShareLink.fullurl"></a>

**`fullurl string Read-only`**

Full URL to use for remote access.
If remote access is disabled, the field will be empty.

<a id="file-sharing-link-api"></a>

## File Sharing Link API

<a id="retrieve-a-file-sharing-link"></a>

### Retrieve a File Sharing link

<a id="get--api-v8-share_link-"></a>

**`GET /api/v8/share_link/`**

Returns the collection of all [`ShareLink`](share.md#ShareLink "ShareLink")

**Example request**:

```http
GET /api/v8/share_link/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   success: true,
   result: [
      {
          "path": "L0Rpc3F1ZSBkdXIvUGhvdG9zL01lcyB2YWNhbmNlcyBlbiByb3Vsb3R0ZQ==" /* /Disque dur/Photos/Mes vacances en roulotte */
          "name": "Mes vacances en roulotte",
          "token": "gAnweF2Xg5OwcJWn",
          "expire": 1355852344,
          "fullurl": "http://13.37.42.69/api/v8/share/gAnweF2Xg5OwcJWn/"
      },
      {
          "path": "L0Rpc3F1ZSBkdXIvc2hhcmVk", /* /Disque dur/shared */
          "name": "shared",
          "token": "s8a+4VtOQNkkQ55f",
          "expire": 1355866268,
          "fullurl": "http://13.37.42.69/api/v8/share/s8a+4VtOQNkkQ55f/"
      }
   ]
}
```

<a id="get--api-v8-share_link-token"></a>

**`GET /api/v8/share_link/{token}`**

Returns the [`ShareLink`](share.md#ShareLink "ShareLink") task with the given id

**Example request**:

```http
GET /api/v8/share_link/gAnweF2Xg5OwcJWn HTTP/1.1
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
        "path": "L0Rpc3F1ZSBkdXIvUGhvdG9zL01lcyB2YWNhbmNlcyBlbiByb3Vsb3R0ZQ==" /* /Disque dur/Photos/Mes vacances en roulotte */
        "name": "Mes vacances en roulotte",
        "token": "gAnweF2Xg5OwcJWn",
        "expire": 1355852344,
        "fullurl": "http://13.37.42.69/api/v8/share/gAnweF2Xg5OwcJWn/"
    }
}
```

<a id="delete-a-file-sharing-link"></a>

### Delete a File Sharing link

<a id="delete--api-v8-share_link-token"></a>

**`DELETE /api/v8/share_link/{token}`**

Deletes the [`ShareLink`](share.md#ShareLink "ShareLink") task with the given token, if
the task was running, stop it.

No rollback is done, if a file as already been processed it will be
left as is.

**Example request**:

```http
DELETE /api/v8/share_link/gAnweF2Xg5OwcJWn HTTP/1.1
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

<a id="create-a-file-sharing-link"></a>

### Create a File Sharing link

<a id="post--api-v8-share_link-"></a>

**`POST /api/v8/share_link/`**

Create a new [`ShareLink`](share.md#ShareLink "ShareLink")

**Example request**:

```http
POST /api/v8/share_link/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "path": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHM=", /* /Disque dur/Téléchargements */
   "expire": 1355932880,
   "fullurl": ""
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
        "path": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHM=", /* /Disque dur/Téléchargements */
        "name": "Téléchargements",
        "token": "6Hj57zgTfoQqb_vH",
        "expire": 1355932880,
        "fullurl": "http://13.37.42.69/api/v8/share/6Hj57zgTfoQqb_vH/"
    }
}
```
