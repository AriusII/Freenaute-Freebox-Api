<a id="file-upload"></a>

# File Upload

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#file-upload)

## Navigation

- [File Upload Errors](#file-upload-errors)
- [File Upload object](#file-upload-object)
- [WebSocket File Upload API](#websocket-file-upload-api)
- [Upload Progress tracking API](#upload-progress-tracking-api)


This API allows you to upload files to the Freebox Server.

NOTE: for large transfer files, you should prefer FTP over HTTP
transfer

*WARNING* the previous http upload method is now deprecated since api v4,
you must now use the new WebSocket upload Api. If you can’t support WebSocket,
you must use ftp for file transfer

<a id="file-upload-errors"></a>

## File Upload Errors

When attempting to access the file upload API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| invalid_request | Invalid request |
| path_not_found | File or folder not found |
| access_denied | Write permission denied in the destination folder |
| destination_conflict | A file with same name already exists |
| invalid_id | Invalid file upload id |
| cancelled | Someone on a side channel as cancelled the upload |
| noent | No upload with this id |

<a id="file-upload-object"></a>

## File Upload object

File uploads have the following attributes:

<a id="FileUpload"></a>

### Objet FileUpload

<a id="FileUpload.id"></a>

**`id int Read-only`**

upload id

<a id="FileUpload.size"></a>

**`size int Read-only`**

Upload file size in bytes

<a id="FileUpload.uploaded"></a>

**`uploaded int Read-only`**

Uploaded bytes

<a id="FileUpload.status"></a>

**`status enum Read-only`**

upload status can have the following values

| status | Description |
| --- | --- |
| authorized | Upload authorization is valid, upload has not started yet |
| in_progress | Upload in progress |
| done | Upload done |
| failed | Upload failed |
| conflict | Destination file conflict |
| timeout | Upload authorization is no longer valid |
| cancelled | Upload cancelled by user |

<a id="FileUpload.start_date"></a>

**`start_date timestamp Read-only`**

upload start date

<a id="FileUpload.last_update"></a>

**`last_update timestamp Read-only`**

last update of file upload object

<a id="FileUpload.upload_name"></a>

**`upload_name string Read-only`**

name of the file uploaded

<a id="FileUpload.dirname"></a>

**`dirname string Read-only`**

upload destination directory

<a id="websocket-file-upload-api"></a>

<a id="ws-upload-api"></a>

## WebSocket File Upload API

The file upload WebSocket path is /api/v8/ws/upload

With this new API, the need for creating a ‘file upload authorization’
has now been removed.

To be able to upload a file to the Freebox, you must open a WebSocket
connection to the upload api, then for each file you want to upload
you must :

- send a [`FileUploadStartAction`](upload.md#FileUploadStartAction "FileUploadStartAction") with the action ‘upload_start’
- wait for the associated [`WebSocketResponse`](../fondamentaux/websocket.md#WebSocketResponse "WebSocketResponse") that indicates
  success, then start transferring the file content by chunks,
  each chunk being a binary WebSocket frame.

  For each chunk you send, you’ll get a WsUploadProgress response indicating that
  the associated chunk has been received and processed. Note that you should not
  wait for this response before sending the next data chunk in order to get
  good bandwidth performance.
- once all chunks have been transferred, you should send a
  [`FileUploadFinalizeAction`](upload.md#FileUploadFinalizeAction "FileUploadFinalizeAction") with the action ‘upload_finalize’ and wait
  for the associated [`WebSocketResponse`](../fondamentaux/websocket.md#WebSocketResponse "WebSocketResponse") indicating success

Note that if you have multiple files to send, you should reuse the same
WebSocket connection, and repeat the upload steps again.

If for any reason the WebSocket is closed during upload, the partially sent
file will be left as-is on the Freebox to allow resuming upload at a later point.

If you want to cancel an ongoing upload ou can send a
[`FileUploadCancelAction`](upload.md#FileUploadCancelAction "FileUploadCancelAction"). The partially uploaded
file will then be deleted

<a id="file-upload-start-action"></a>

### File Upload Start Action

<a id="FileUploadStartAction"></a>

#### Objet FileUploadStartAction

<a id="FileUploadStartAction.request_id"></a>

**`request_id int`**

optional request_id

<a id="FileUploadStartAction.action"></a>

**`action string`**

must be ‘upload_start’

<a id="FileUploadStartAction.size"></a>

**`size int`**

optional file size

<a id="FileUploadStartAction.dirname"></a>

**`dirname string`**

the destination directory (encoded value)

<a id="FileUploadStartAction.filename"></a>

**`filename string`**

the destination filename

<a id="FileUploadStartAction.force"></a>

**`force enum`**

select the way conflicts are handled

| Force mode | Description |
| --- | --- |
| *missing* | The response to the FileUploadStartAction will be an error with ‘destination_conflict’ if the destination file already exists. The response will also contain a file_size attribute containing the existing file length (useful for resuming upload) |
| overwrite | If the target file already exists it will be overridden |
| resume | The upload will resume, all sent chunks will then be appended to the existing file. |

<a id="file-upload-finalize-action"></a>

### File Upload Finalize action

<a id="FileUploadFinalizeAction"></a>

#### Objet FileUploadFinalizeAction

<a id="FileUploadFinalizeAction.request_id"></a>

**`request_id int`**

optional request_id

<a id="FileUploadFinalizeAction.action"></a>

**`action string`**

must be ‘upload_finalize’

<a id="file-upload-cancel-action"></a>

### File Upload Cancel action

<a id="FileUploadCancelAction"></a>

#### Objet FileUploadCancelAction

<a id="FileUploadCancelAction.request_id"></a>

**`request_id int`**

optional request_id

<a id="FileUploadCancelAction.action"></a>

**`action string`**

must be ‘upload_cancel’

<a id="file-upload-chunk"></a>

### File Upload Chunk

File upload chunk are just Binary WebSocket frames containing raw file
content.

<a id="file-upload-chunk-response"></a>

### File Upload Chunk Response

For each received chunk, the Freebox will send a chunk response containing
upload progress information the request_id used in response will be
the one from the [`FileUploadStartAction`](upload.md#FileUploadStartAction "FileUploadStartAction"), and ‘action’ value
will be ‘upload_data’

<a id="FileUploadChunkResponse"></a>

#### Objet FileUploadChunkResponse

<a id="FileUploadChunkResponse.total_len"></a>

**`total_len int`**

target file current length

<a id="FileUploadChunkResponse.complete"></a>

**`complete bool`**

will be true in a reply to [`FileUploadFinalizeAction`](upload.md#FileUploadFinalizeAction "FileUploadFinalizeAction")
or [`FileUploadCancelAction`](upload.md#FileUploadCancelAction "FileUploadCancelAction")

<a id="FileUploadChunkResponse.cancelled"></a>

**`cancelled bool`**

will be true in a reply [`FileUploadCancelAction`](upload.md#FileUploadCancelAction "FileUploadCancelAction")

<a id="file-upload-example"></a>

### File Upload example

<a id="get--api-v8-ws-upload"></a>

**`GET /api/v8/ws/upload`**

**Start the WebSocket handshake**:

Client ==> Freebox

```http
GET ws://mafreebox.freebox.fr/api/v8/ws/upload HTTP/1.1
Host: mafreebox.freebox.fr
Connection: Upgrade
Upgrade: websocket
Sec-WebSocket-Version: 13
Sec-WebSocket-Key: LhYCx4FBJE6pqrIL3tDC3g==
X-Fbx-App-Auth: 35JYdQSvkcBYK84IFMU7H86clfhS75OzwlQrKlQN1gBch\/Dd62RGzDpgC7YB9jB2
```

**Handshake response**:

Client <== Freebox

```http
HTTP/1.1 101 Switching Protocols
Connection: upgrade
Upgrade: websocket
Sec-WebSocket-Accept: IqwCz8z8sON/eWQqkYKLu6iLkzo=
```

**Start upload**:

Client ==> Freebox

```json
{
  "action": "upload_start",
  "request_id": 3615,
  "size": 8526224,
  "dirname": "L0Rpc3F1ZSBkdXIvMF91cGxvYWRfdGVzdA==",
  "filename": "test_file.bin"
}
```

**Start upload response**:

Client <== Freebox

```json
{
  "success": false,
  "action": "upload_start",
  "request_id": 3615,
  "msg": "Le fichier existe déjà",
  "file_size": 8526224,
  "error_code": "conflict"
}
```

**Start upload with overwrite force mode**:

Client ==> Freebox

```json
{
  "action": "upload_start",
  "request_id": 6969,
  "size": 8526224,
  "dirname": "L0Rpc3F1ZSBkdXIvMF91cGxvYWRfdGVzdA==",
  "filename": "test_file.bin",
  "force": "overwrite"
}
```

**Start upload response**:

Client <== Freebox

```json
{
  "action": "upload_start",
  "success": true,
  "request_id": 6969
}
```

**Send data chunk**:

Client ==> Freebox

[ BINARY WEBSOCKET FRAME MESSAGE containing file offset: 0, length: 512k ]

[ BINARY WEBSOCKET FRAME MESSAGE containing file offset: 512k, length: 512k ]

[ BINARY WEBSOCKET FRAME MESSAGE containing file offset: 1024k, length: 512k ]

[ … ]

**Receive upload response**:

Client <== Freebox

```json
{
  "request_id": 6969
  "action": "upload_data",
  "success": true,
  "result": {
          "total_len": 524288,
          "complete": false
  },
}

{
  "request_id": 6969,
  "action": "upload_data",
  "success": true,
  "result": {
          "total_len": 1048576,
          "complete": false
  }
}

[ ... ]
```

This will be received for each sent data chunk

**Send upload finalize**:

Client ==> Freebox

```json
{
  "action": "upload_finalize",
  "request_id":3615
}
```

**Receive upload finalize confirmation**:

Client <== Freebox

```json
{
  "request_id": 3615,
  "action": "upload_finalize",
  "success": true,
  "result": {
    "total_len": 8526224,
    "complete": true
  }
}
```

At this point you can start uploading a new file by repeating the previous
steps starting from *Start upload* step

<a id="upload-progress-tracking-api"></a>

## Upload Progress tracking API

<a id="get-the-list-of-uploads"></a>

### Get the list of uploads

<a id="get--api-v8-upload-"></a>

**`GET /api/v8/upload/`**

**Example request**:

```http
GET /api/v8/upload/ HTTP/1.1
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
            "id": 1678139709,
            "size": 54960,
            "uploaded": 54960,
            "status": "done",
            "last_update": 1361465608,
            "start_date": 1361465608,
            "upload_name": "playlist.m3u",
            "dirname": "/Disque 1"
        }
    ]
}
```

<a id="track-an-upload-status"></a>

### Track an upload status

<a id="get--api-v8-upload-id"></a>

**`GET /api/v8/upload/{id}`**

With this API you can track the progress of your
[`FileUpload`](upload.md#FileUpload "FileUpload") task

**Example request**:

```http
GET /api/v8/upload/1678139709 HTTP/1.1
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
        "id": 1678139709,
        "size": 54960,
        "uploaded": 54960,
        "status": "done",
        "last_update": 1361465608,
        "start_date": 1361465608,
        "upload_name": "playlist.m3u",
        "dirname": "/Disque 1"
    }
}
```

<a id="cancel-an-upload"></a>

### Cancel an upload

<a id="delete--api-v8-upload-id-cancel"></a>

**`DELETE /api/v8/upload/{id}/cancel`**

Cancel the given [`FileUpload`](upload.md#FileUpload "FileUpload") closing the connection
The upload status must be in_progress

**Example request**:

```http
DELETE /api/v8/upload/136419941/cancel HTTP/1.1
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

<a id="delete-an-upload"></a>

### Delete an upload

<a id="delete--api-v8-upload-id"></a>

**`DELETE /api/v8/upload/{id}`**

Delete the given [`FileUpload`](upload.md#FileUpload "FileUpload") closing the connection
if needed

**Example request**:

```http
DELETE /api/v8/upload/136419941 HTTP/1.1
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
