<a id="player-unstable"></a>

<a id="player-api"></a>

# Player [UNSTABLE]

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#player-unstable)

## Navigation

- [Player Errors](#player-errors)
- [Player Objects](#player-objects)
- [Player API](#id1)


**\*** INTERNAL USE ONLY **\***

With the player API you access and control a Freebox Player connected on the
same local network as the Freebox Server. Available players can be enumerated,
and the listed player identifier can be used to dispatch commands.

<a id="player-errors"></a>

## Player Errors

When attempting to access the player API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| internal_error | Internal error |
| inval | Invalid parameters |
| noent | no player with this id |

<a id="player-objects"></a>

## Player Objects

<a id="player"></a>

### Player

<a id="Player"></a>

#### Objet Player

<a id="Player.id"></a>

**`id int`**

<a id="Player.device_name"></a>

**`device_name string`**

<a id="Player.uid"></a>

**`uid string`**

<a id="Player.reachable"></a>

**`reachable bool`**

<a id="Player.api_version"></a>

**`api_version string`**

<a id="Player.api_available"></a>

**`api_available bool`**

<a id="player-status-foreground-app"></a>

### Player Status Foreground App

<a id="PlayerStatusForegroundApp"></a>

#### Objet PlayerStatusForegroundApp

<a id="PlayerStatusForegroundApp.package_id"></a>

**`package_id id`**

<a id="PlayerStatusForegroundApp.cur_url"></a>

**`cur_url string`**

<a id="PlayerStatusForegroundApp.context"></a>

**`context object`**

<a id="PlayerStatusForegroundApp.package"></a>

**`package string`**

<a id="player-status-capabilities"></a>

### Player Status Capabilities

Capabilities of a media player.

<a id="PlayerStatusCapabilities"></a>

#### Objet PlayerStatusCapabilities

<a id="PlayerStatusCapabilities.play"></a>

**`play bool`**

<a id="PlayerStatusCapabilities.pause"></a>

**`pause bool`**

<a id="PlayerStatusCapabilities.stop"></a>

**`stop bool`**

<a id="PlayerStatusCapabilities.next"></a>

**`next bool`**

<a id="PlayerStatusCapabilities.prev"></a>

**`prev bool`**

<a id="PlayerStatusCapabilities.record"></a>

**`record bool`**

<a id="PlayerStatusCapabilities.record_stop"></a>

**`record_stop bool`**

<a id="PlayerStatusCapabilities.seek_forward"></a>

**`seek_forward bool`**

<a id="PlayerStatusCapabilities.seek_backward"></a>

**`seek_backward bool`**

<a id="PlayerStatusCapabilities.seek_to"></a>

**`seek_to bool`**

<a id="PlayerStatusCapabilities.shuffle"></a>

**`shuffle bool`**

<a id="PlayerStatusCapabilities.repeat_all"></a>

**`repeat_all bool`**

<a id="PlayerStatusCapabilities.repeat_one"></a>

**`repeat_one bool`**

<a id="PlayerStatusCapabilities.select_stream"></a>

**`select_stream bool`**

<a id="PlayerStatusCapabilities.select_audio_track"></a>

**`select_audio_track bool`**

<a id="PlayerStatusCapabilities.select_srt_track"></a>

**`select_srt_track bool`**

<a id="player-status-informations"></a>

### Player Status Informations

<a id="PlayerStatusInformations"></a>

#### Objet PlayerStatusInformations

<a id="PlayerStatusInformations.name"></a>

**`name string`**

<a id="PlayerStatusInformations.last_activity"></a>

**`last_activity long`**

<a id="PlayerStatusInformations.capabilities"></a>

**`capabilities PlayerStatusCapabilities`**

<a id="player-status"></a>

### Player Status

<a id="PlayerStatus"></a>

#### Objet PlayerStatus

<a id="PlayerStatus.power_state"></a>

**`power_state string`**

| state | Description |
| --- | --- |
| standby | freebox player is currently in standby mode |
| running | freebox player is on |

<a id="PlayerStatus.player"></a>

**`player PlayerStatusInformations`**

State of the active media player on the device.

<a id="PlayerStatus.foreground_app"></a>

**`foreground_app PlayerStatusForegroundApp`**

The context of the currently running application. The fields exposed in
this object are left to the discretion of the application author, and
thus subject to change at any time.

<a id="id1"></a>

## Player API

<a id="list-every-player-devices"></a>

### List every player devices

<a id="get--api-v8-player"></a>

**`GET /api/v8/player`**

Returns the list of all player devices registered on the local network
([[`Player`](player.md#Player "Player")]).

**Example request**:

```http
GET /api/v8/player HTTP/1.1
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
         "device_name": "Freebox Player",
         "stb_type": "stb_v7",
         "uid": "123456789012345678911234567892123",
         "reachable": true,
         "api_version": "6.0",
         "id": 11,
         "api_available": true
      }
   ]
}
```

<a id="get-player-device-status"></a>

### Get player device status

<a id="get--api-v8-player-id_player-api-v6-status-"></a>

**`GET /api/v8/player/{id_player}/api/v6/status/`**

Returns the current state of a player device ([`Player`](player.md#Player "Player")).

**Example request**:

```http
GET /api/v8/player/11/api/v6/status/ HTTP/1.1
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
      "power_state": "standby"
   }
}
```

<a id="control-the-active-media-player-of-a-device"></a>

### Control the active media player of a device

<a id="post--api-v8-player-id_player-api-v6-control-mediactrl-"></a>

**`POST /api/v8/player/{id_player}/api/v6/control/mediactrl/`**

Parameters

- **cmd** (*string*) – Command to execute

Send a command to the active media player of a device. Not all commands are
always available, the capabilities of the active media player can be
retrieved in the device status to determine which commands ca be used.

| command | Description |
| --- | --- |
| play_pause | toggle play pause |
| stop | stop |
| prev | previous |
| next | next |
| select_stream | select quality of the stream |
| select_audio_track | select audio track |
| select_srt_track | select subtitle track |

**Example request**:

```http
POST /api/v8/player/11/api/v6/control/mediactrl/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "cmd": "play_pause"
}
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

<a id="control-the-playback-volume-of-the-device"></a>

### Control the playback volume of the device

<a id="get--api-v8-player-id_player-api-v6-control-volume-"></a>

**`GET /api/v8/player/{id_player}/api/v6/control/volume/`**

**Example request**:

```http
GET /api/v8/player/11/api/v6/control/volume/ HTTP/1.1
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
      "mute": false
      "volume": 25
   }
}
```

<a id="put--api-v8-player-id_player-api-v6-control-volume-"></a>

**`PUT /api/v8/player/{id_player}/api/v6/control/volume/`**

Parameters

- **volume** (*integer*) – Master volume from 0 to 100
- **mute** (*boolean*) – Mute

**Example request**:

```http
PUT /api/v8/player/{id_player}/api/v6/control/volume/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "volume": 50
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
      "mute": false
      "volume": 50
   }
}
```

<a id="open-a-url-on-a-player-device"></a>

### Open a url on a player device

<a id="post--api-v8-player-id_player-api-v6-control-open"></a>

**`POST /api/v8/player/{id_player}/api/v6/control/open`**

Parameters

- **url** (*string*) – Url to open on the Freebox Player
- **type** (*string*) – Mime type of the content to open on the Freebox Player
  (optional: default is empty)

**Here are some useful examples calls**:

Open the video player:

```json
{ "url": "http://jell.yfish.us/media/jellyfish-3-mbps-hd-h264.mkv",
  "type": "video/x-matroska" }
```

Open the web browser:

```json
{ "url": "https://www.google.com",
  "type": "text/html" }
```

Open TV on channel 2:

```json
{ "url": "tv:?channel=2" }
```

Open a YouTube video:

```json
{ "url": "https://www.youtube.com/watch?v=pltY5vS-aOY" }
```

**Example request**:

```http
POST /api/v8/player/11/api/v6/control/open HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "url": "tv:?channel=123"
}
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
