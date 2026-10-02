<a id="airmedia-api"></a>

# AirMedia API

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#airmedia-api)

## Navigation

- [AirMedia Errors](#airmedia-errors)
- [AirMedia Config Object](#airmedia-config-object)
- [AirMedia Configuration API](#airmedia-configuration-api)
- [AirMedia Receiver Object](#airmedia-receiver-object)


This API allows you to multimedia stream to any airmedia device
reachable by the Freebox, as well as configuring the airmedia
server hosted on the Freebox Server.

<a id="airmedia-errors"></a>

## AirMedia Errors

When attempting to access the file airmedia API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| unknown_target | No airmedia device with this name in range |
| no_client | No airmedia client connected |
| set_pass | Unable to update password |
| set_onscreen_code | Unable to activate onscreen code |
| no_ctrl | Remote control is unavailable |
| http | Internal HTTP error |
| bad_session | No stream session found |
| bad_name | Invalid airmedia name |
| bad_device_id | No device with this id |
| bad_remote_id | No remote control with this id |
| req_in_progress | You should try again, another request is still processing |
| fetch | Unable to get slideshow information |
| no_display | No screen available |
| playback_state | Invalid playback state |
| no_slideshow_srv | Slideshow is not supported |
| no_mem | Internal error |
| inout_file | Unable to read input file |
| no_volume_control | Volume control is not available |
| connect | Error connecting to the airmedia device |
| unauthorized | This device requests a password |
| unsupported_media | The device does not support this format |
| bad_type | Invalid file type |
| unimplemented | Unimplemented |

<a id="airmedia-config-object"></a>

## AirMedia Config Object

AirMedia config has the following attributes:

<a id="AirMediaConfig"></a>

### Objet AirMediaConfig

<a id="AirMediaConfig.enabled"></a>

**`enabled bool`**

Enable/Disable the airmedia server

<a id="AirMediaConfig.password"></a>

**`password string Write-only`**

If not empty, the client will have to enter a password to be
able to use this airmedia server

<a id="airmedia-configuration-api"></a>

## AirMedia Configuration API

<a id="get-the-current-airmedia-configuration"></a>

### Get the current AirMedia configuration

<a id="get--api-v8-airmedia-config-"></a>

**`GET /api/v8/airmedia/config/`**

Returns the current [`AirMediaConfig`](airmedia.md#AirMediaConfig "AirMediaConfig")

**Example request**:

```http
GET /api/v8/airmedia/config/ HTTP/1.1
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
   result: {
      enabled: true
   }
}
```

<a id="update-the-current-airmedia-configuration"></a>

### Update the current AirMedia configuration

<a id="put--api-v8-airmedia-config-"></a>

**`PUT /api/v8/airmedia/config/`**

Update the current [`AirMediaConfig`](airmedia.md#AirMediaConfig "AirMediaConfig")

**Example request**:

```http
PUT /api/v8/airmedia/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "enabled": true,
   "password": "3615"
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   success: true,
   result: {
      enabled: true
   }
}
```

<a id="airmedia-receiver-object"></a>

## AirMedia Receiver Object

AirMedia receivers have the following attributes

<a id="AirMediaReceiver"></a>

### Objet AirMediaReceiver

<a id="AirMediaReceiver.name"></a>

**`name string Read-only`**

AirMedia name

<a id="AirMediaReceiver.password_protected"></a>

**`password_protected bool Read-only`**

Is set to true the receiver is protected by a password

<a id="AirMediaReceiver.capabilities"></a>

**`capabilities map Read-only`**

List of receiver capabilities from the following list

| Capability | Description |
| --- | --- |
| photo | can display photos |
| audio | can play audio files |
| video | can play video files |
| screen | can display remote screen |

<a id="get-the-list-of-available-airmedia-receivers"></a>

### Get the list of available AirMedia receivers

You can get the list of [`AirMediaReceiver`](airmedia.md#AirMediaReceiver "AirMediaReceiver") connected to
the Freebox Server using this API

<a id="get--api-v8-airmedia-receivers-"></a>

**`GET /api/v8/airmedia/receivers/`**

Get the list of [`AirMediaReceiver`](airmedia.md#AirMediaReceiver "AirMediaReceiver") connected to the Freebox Server

**Example request**:

```http
GET /api/v8/airmedia/receivers/ HTTP/1.1
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
         capabilities: {
            photo: true,
            screen: false,
            audio: true,
            video: true
         },
         name: "Freebox Player",
         password_protected: true
      },
      {
         capabilities: {
            photo: false,
            screen: false,
            audio: true,
            video: false
         },
         name: "Freebox Server",
         password_protected: false
      }
   ]
}
```

<a id="interacting-with-an-airmedia-receiver"></a>

### Interacting with an AirMedia receiver

Once you have selected an available [`AirMediaReceiver`](airmedia.md#AirMediaReceiver "AirMediaReceiver")
you can start interacting with it by sending media with the following
API.

<a id="airmedia-receiver-request"></a>

#### AirMedia receiver request

<a id="AirMediaReceiverRequest"></a>

##### Objet AirMediaReceiverRequest

<a id="AirMediaReceiverRequest.action"></a>

**`action enum`**

| Action | Description |
| --- | --- |
| start | start playing a media |
| stop | stop playing a media |

<a id="AirMediaReceiverRequest.media_type"></a>

**`media_type string`**

| Media Type | Description |
| --- | --- |
| photo | display a photo |
| video | display a video |

<a id="AirMediaReceiverRequest.password"></a>

**`password string`**

Optional receiver password.

<a id="AirMediaReceiverRequest.position"></a>

**`position int`**

Start position for a video.

The start position is expressed in percent \* 1000, for instance
50000 means 50% of the video

<a id="AirMediaReceiverRequest.media"></a>

**`media string`**

The media to play.

- For video media, you have to specify the media URL, for instance
  <http://anon.nasa-global.edgesuite.net/HD_downloads/GRAIL_launch_480.mov>
- For photo media, you have to specify the file path on the
  Freebox Server (base64 encoded as returned in fs/ls call), for
  instance L0Rpc3F1ZSBkdXIvUGhvdG9zL1JvY2tldHMvRFNDXzM0OTEuanBn

<a id="sending-a-new-request-to-an-airmedia-receiver"></a>

#### Sending a new request to an AirMedia receiver

<a id="post--api-v8-airmedia-receviers-receiver_name-"></a>

**`POST /api/v8/airmedia/receviers/{receiver_name}/`**

**Example: display a photo on the Freebox Player**:

```http
POST /api/v8/airmedia/receivers/Freebox%20Player/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "action": "start",
   "media_type": "photo",
   "media": "L0Rpc3F1ZSBkdXIvUGhvdG9zL1JvY2tldHMvRFNDXzM0OTEuanBn",
   "password": "1111"
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
}
```

**Example: play a video the Freebox Player**:

```http
POST /api/v8/airmedia/receivers/Freebox%20Player/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "action": "start",
   "media_type": "video",
   "media": "http://anon.nasa-global.edgesuite.net/HD_downloads/GRAIL_launch_480.mov",
   "password": "1111"
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
}
```

**Example: stop the current AirMedia video on Freebox Player**:

```http
POST /api/v8/airmedia/receivers/Freebox%20Player/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "action": "stop",
   "media_type": "video"
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
}
```
