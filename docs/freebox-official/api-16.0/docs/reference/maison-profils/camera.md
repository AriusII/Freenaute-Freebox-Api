<a id="cameras"></a>

# Cameras

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#cameras)

## Navigation

- [Camera Errors](#camera-errors)
- [Camera object](#camera-object)
- [Camera API](#camera-api)


The Camera API allows you to access features related to cameras.

<a id="camera-errors"></a>

## Camera Errors

When attempting to access the Camera API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| noent | no camera with this id |
| inval | invalid parameters |

<a id="camera-object"></a>

## Camera object

Camera object have the following properties

<a id="Camera"></a>

### Objet Camera

<a id="Camera.id"></a>

**`id string`**

camera id

<a id="Camera.node_id"></a>

**`node_id int`**

camera node id

<a id="Camera.name"></a>

**`name string`**

camera name

<a id="Camera.stream_url"></a>

**`stream_url string`**

camera stream url

<a id="Camera.lan_gid"></a>

**`lan_gid string`**

camera lan id

<a id="camera-api"></a>

## Camera API

<a id="get-list-of-cameras"></a>

### Get list of cameras

<a id="get--api-v8-camera-"></a>

**`GET /api/v8/camera/`**

Returns the collection of all [`Camera`](camera.md#Camera "Camera")

**Example request**:

```http
GET /api/v8/camera/ HTTP/1.1
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
          "id": "012345678901",
          "node_id": 0,
          "name": "Caméra du salon",
          "stream_url": "/camera/stream/012345678901/stream.m3u8",
          "lan_gid": "ether-3c:98:72:fa:36:15"
      },
      {
          "id": "012345678902",
          "node_id": 1,
          "name": "Caméra du bureau",
          "stream_url": "/camera/stream/012345678902/stream.m3u8",
          "lan_gid": "ether-3c:98:72:fa:42:58"
      }
    ]
}
```

<a id="access-a-given-camera"></a>

### Access a given camera

<a id="get--api-v8-camera-id"></a>

**`GET /api/v8/camera/{id}`**

Returns the [`Camera`](camera.md#Camera "Camera") with the given id

**Example request**:

```http
GET /api/v8/camera/012345678901 HTTP/1.1
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
       "id": "012345678901",
       "node_id": 0,
       "name": "Caméra du salon",
       "stream_url": "/camera/stream/012345678901/stream.m3u8",
       "lan_gid": "ether-3c:98:72:fa:36:15"
    }
}
```

<a id="delete-a-camera"></a>

### Delete a camera

Use Home Node Api to delete camera (like a node) with its node id
