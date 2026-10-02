<a id="pvr-unstable"></a>

# PVR [UNSTABLE]

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#pvr-unstable)

## Navigation

- [PVR Errors](#pvr-errors)
- [PVR Config](#pvr-config)
- [PVR Config API](#pvr-config-api)
- [PVR Quota](#pvr-quota)
- [PVR Quota API](#pvr-quota-api)
- [Precord](#precord)
- [Precord API](#precord-api)
- [Frecord](#frecord)
- [Frecord API](#frecord-api)
- [Media](#media)
- [Media API](#media-api)


**\*** INTERNAL USE ONLY **\***

<a id="pvr-errors"></a>

## PVR Errors

| error_code | Description |
| --- | --- |
| noent | wrong id |
| inval | invalid params |
| inval_date_fmt | invalid date format |
| inval_end_before_start | start time must be before end time |
| system_time_incorrect | system time not available |
| record_duration_too_long | record duration is too long |
| record_date_in_past | record date is already passed |
| unknown_channel | unknown channel |
| no_channel_svc | no service for this channel |
| only_auto_disable | can’t disable manual precord |
| cannot_change_en_state | can’t change enabled state |
| cannot_disable_has_data | can’t disable started record |
| internal_error | internal error |

<a id="pvr-config"></a>

## PVR Config

PVR config has the following attributes:

<a id="PvrConfig"></a>

### Objet PvrConfig

<a id="PvrConfig.margin_before"></a>

**`margin_before int`**

default margin before recording start time

<a id="PvrConfig.margin_after"></a>

**`margin_after int`**

default margin after recording end time

<a id="pvr-config-api"></a>

## PVR Config API

<a id="get-the-current-pvr-configuration"></a>

### Get the current PVR configuration

<a id="get--api-v8-pvr-config-"></a>

**`GET /api/v8/pvr/config/`**

Returns the current [`PvrConfig`](pvr.md#PvrConfig "PvrConfig")

**Example request**:

```http
GET /api/v8/pvr/config/ HTTP/1.1
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
        "margin_before": 10,
        "margin_after": 5
    }
}
```

<a id="update-the-current-pvr-configuration"></a>

### Update the current PVR configuration

<a id="put--api-v8-pvr-config-"></a>

**`PUT /api/v8/pvr/config/`**

Update the current [`PvrConfig`](pvr.md#PvrConfig "PvrConfig")

<a id="pvr-quota"></a>

## PVR Quota

PVR Quota has the following attributes:

<a id="PvrQuota"></a>

### Objet PvrQuota

<a id="PvrQuota.quota_exceeded"></a>

**`quota_exceeded bool`**

is quota exceeded

<a id="PvrQuota.needed_tresh"></a>

**`needed_tresh int`**

needed quota threshold

<a id="PvrQuota.cur_tresh"></a>

**`cur_tresh int`**

current quota threshold

<a id="pvr-quota-api"></a>

## PVR Quota API

<a id="getting-the-current-quota-info"></a>

### Getting the current quota info

<a id="get--api-v8-pvr-quota-"></a>

**`GET /api/v8/pvr/quota/`**

**Example request**:

```http
GET /api/v8/pvr/quota/ HTTP/1.1
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
        "quota_exceeded": true,
        "needed_tresh": 80,
        "cur_tresh": 40
    }
}
```

<a id="request-next-quota-threshold"></a>

### Request next quota threshold

<a id="put--api-v8-pvr-quota-"></a>

**`PUT /api/v8/pvr/quota/`**

Request next quota threshold. You don’t have to provide any arguments,
the quota will be adjusted automatically if needed.

**Example request**:

```http
PUT /api/v8/pvr/quota/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{ }
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
        "quota_exceeded": false,
        "needed_tresh": 80,
        "cur_tresh": 80
    }
}
```

<a id="pvr-programmed-records"></a>

# PVR Programmed records

Precords (Programmed records) are records that are planned. Precords can be
manual, or generated using a PVR Generator (see below). Only manual Precords
can be edited directly.

<a id="precord"></a>

## Precord

Precord has the following attributes:

<a id="Precord"></a>

### Objet Precord

<a id="Precord.id"></a>

**`id string Read-only`**

precord id

<a id="Precord.media"></a>

**`media string`**

media name on which the record will be written to. See the [Media API](pvr.md#media-api)
for more info. This property and can be empty when the file backing the
record is not available, for example when secure is set.

<a id="Precord.path"></a>

**`path string`**

destination directory on the media storage where the record will be
written to

<a id="Precord.has_record_gen"></a>

**`has_record_gen bool Read-only`**

if true, this precord has been generated using a Generator

<a id="Precord.record_gen_id"></a>

**`record_gen_id int Read-only`**

if has_record_gen, this is the id of the generator

<a id="Precord.conflict"></a>

**`conflict bool Read-only`**

if true this record may conflict with another record

<a id="Precord.overlap_list"></a>

**`overlap_list [] array of int Read-only`**

in case of conflict, this will contain the list of records id that may
conflict with this record

<a id="Precord.enabled"></a>

**`enabled bool`**

it only applies to generated records. If false the generated precord will
be skipped.

<a id="Precord.altered"></a>

**`altered bool Read-only`**

a precord is altered when some part of the recording may be missing.
This can be the case if a conflict occurred during the recording
(or connection was down)

<a id="Precord.state"></a>

**`state enum Read-only`**

| State | Description |
| --- | --- |
| disabled | disabled |
| start_error | failed to start |
| waiting_start_time | scheduled |
| starting | starting |
| running | running |
| running_error | running with error |
| failed | failed |
| finished | finished |

<a id="Precord.error"></a>

**`error enum Read-only`**

| Error |  |
| --- | --- |
| none |  |
| file_access_error |  |
| disk_full |  |
| private_but_no_private_dir |  |
| network_problem |  |
| resource_problem |  |
| no_stream_available |  |
| no_data_received |  |
| missed |  |
| stopped |  |
| internal_error |  |
| unknown_error |  |

<a id="Precord.channel_uuid"></a>

**`channel_uuid string`**

channel uuid

<a id="Precord.channel_name"></a>

**`channel_name string`**

optional channel name

<a id="Precord.channel_quality"></a>

**`channel_quality enum`**

| channel_quality |  |
| --- | --- |
| auto |  |
| hd |  |
| sd |  |
| ld |  |
| 3d |  |

<a id="Precord.channel_type"></a>

**`channel_type enum`**

| channel_type | Description |
| --- | --- |
| ‘’ (empty string) | auto |
| iptv | use only iptv streams |
| dvb | use only dvb streams |

<a id="Precord.name"></a>

**`name string`**

record name

<a id="Precord.subname"></a>

**`subname string`**

record subname

<a id="Precord.broadcast_type"></a>

**`broadcast_type enum`**

| broadcast_type |  |
| --- | --- |
| tv |  |
| radio |  |

<a id="Precord.start"></a>

**`start int`**

record start timestamp

<a id="Precord.end"></a>

**`end int`**

record end timestamp

<a id="Precord.legacy_uri"></a>

**`legacy_uri string`**

only used for legacy apps. Use channel_uuid instead when available
NOTE: only visible when called from player

<a id="Precord.force_channel_name"></a>

**`force_channel_name string`**

only used for legacy apps. Use channel_uuid instead when available
NOTE: only visible when called from player

<a id="precord-api"></a>

## Precord API

<a id="getting-the-list-of-precords"></a>

### Getting the list of precords

<a id="get--api-v8-pvr-programmed-"></a>

**`GET /api/v8/pvr/programmed/`**

**Example request**:

```http
GET /api/v8/pvr/programmed/ HTTP/1.1
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
            "has_record_gen": true,
            "channel_name": "France 2",
            "overlap_list": [
                195
            ],
            "end": 1403755697,
            "media": "Disque dur",
            "path": "Enregistrements",
            "record_gen_id": 10,
            "enabled": true,
            "id": 190,
            "start": 1403755628,
            "broadcast_type": "tv",
            "subname": "",
            "state": "waiting_start_time",
            "channel_type": "",
            "name": "Test Repeat",
            "channel_quality": "auto",
            "conflict": true,
            "channel_uuid": "uuid-webtv-201",
            "error": "none",
            "altered": false
        }

        [ ... ]

        {
            "has_record_gen": false,
            "channel_name": "France 2",
            "overlap_list": [ ],
            "end": 1403541511,
            "media": "NO NAME",
            "path": "Enregistrements",
            "record_gen_id": 0,
            "enabled": true,
            "id": 236,
            "start": 1403541361,
            "broadcast_type": "tv",
            "subname": "Sub Test",
            "state": "finished",
            "channel_type": "iptv",
            "name": "Test",
            "channel_quality": "auto",
            "conflict": false,
            "channel_uuid": "uuid-webtv-201",
            "error": "none",
            "altered": true
        }
    ]
}
```

<a id="getting-a-specific-precord"></a>

### Getting a specific precord

<a id="get--api-v8-pvr-programmed-id"></a>

**`GET /api/v8/pvr/programmed/{id}`**

Returns the requested [`Precord`](pvr.md#Precord "Precord")

**Example request**:

```http
GET /api/v8/pvr/programmed/236 HTTP/1.1
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
        "has_record_gen": false,
        "channel_name": "France 2",
        "overlap_list": [ ],
        "end": 1403541511,
        "media": "NO NAME",
        "path": "Enregistrements",
        "record_gen_id": 0,
        "enabled": true,
        "id": 236,
        "start": 1403541361,
        "broadcast_type": "tv",
        "subname": "Sub Test",
        "state": "finished",
        "channel_type": "iptv",
        "name": "Test",
        "channel_quality": "auto",
        "conflict": false,
        "channel_uuid": "uuid-webtv-201",
        "error": "none",
        "altered": true
    }
}
```

<a id="updating-a-precord"></a>

### Updating a precord

<a id="put--api-v8-pvr-programmed-id"></a>

**`PUT /api/v8/pvr/programmed/{id}`**

Update a [`Precord`](pvr.md#Precord "Precord") properties

**Example request**:

```http
PUT /api/v8/pvr/programmed/236 HTTP/1.1
Host: mafreebox.freebox.fr

{
  "name": "test 2"
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
        "has_record_gen": false,
        "channel_name": "France 2",
        "overlap_list": [ ],
        "end": 1403541511,
        "media": "NO NAME",
        "path": "Enregistrements",
        "record_gen_id": 0,
        "enabled": true,
        "id": 236,
        "start": 1403541361,
        "broadcast_type": "tv",
        "subname": "Sub Test",
        "state": "finished",
        "channel_type": "iptv",
        "name": "test 2",
        "channel_quality": "auto",
        "conflict": false,
        "channel_uuid": "uuid-webtv-201",
        "error": "none",
        "altered": true
    }
}
```

<a id="delete-a-precord"></a>

### Delete a precord

<a id="delete--api-v8-pvr-programmed-id"></a>

**`DELETE /api/v8/pvr/programmed/{id}`**

Delete a [`Precord`](pvr.md#Precord "Precord")

**Example request**:

```http
DELETE /api/v8/pvr/programmed/236 HTTP/1.1
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

<a id="create-a-precord"></a>

### Create a precord

<a id="post--api-v8-pvr-programmed-"></a>

**`POST /api/v8/pvr/programmed/`**

Create a new [`Precord`](pvr.md#Precord "Precord")

\*\* Example request\*\*:

```http
POST /api/v8/pvr/programmed/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
    "start": 1444240500,
    "end": 1444244100,
    "channel_uuid": "uuid-webtv-374",
    "name": "Secret Story",
    "subname: "La soirée des habitants"
}
```

\*\* Example response\*\*:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true,
    "result": {
        "id": 63,
        "media": "Disque dur",
        "path": "Enregistrements",
        "channel_uuid": "uuid-webtv-374",
        "channel_name": "NT1",
        "channel_type": "",
        "channel_quality": "auto",
        "broadcast_type": "tv",
        "start": 1444240500,
        "end": 1444244100,
        "name": "Secret Story",
        "subname": "La soirée des habitants",
        "state": "starting",
        "error": "none",
        "enabled": true,
        "altered": false,
        "conflict": false,
        "overlap_list": [],
        "margin_before": 0,
        "margin_after": 0,
        "has_record_gen": false,
        "record_gen_id": 0
    }
}
```

<a id="pvr-finished-records"></a>

# PVR Finished records

Frecords (Finished records) are records that are finished or in progress.
An Frecord object is created automatically when a Precord start time is
reached.

<a id="frecord"></a>

## Frecord

Frecord has the following attributes:

<a id="Frecord"></a>

### Objet Frecord

<a id="Frecord.id"></a>

**`id string Read-only`**

frecord id

<a id="Frecord.media"></a>

**`media string Read-only`**

media name on which the record is written. See the [Media API](pvr.md#media-api) for more
info. This property and can be empty when the file backing the record is
not available, for example when secure is set.

<a id="Frecord.path"></a>

**`path string Read-only`**

destination directory on the media storage

<a id="Frecord.filename"></a>

**`filename string Read-only`**

filename of the record

<a id="Frecord.byte_size"></a>

**`byte_size int Read-only`**

size of the record file in bytes

<a id="Frecord.has_record_gen"></a>

**`has_record_gen bool Read-only`**

if true, this frecord has been generated using a Generator

<a id="Frecord.record_gen_id"></a>

**`record_gen_id int Read-only`**

if has_record_gen, this is the id of the generator

<a id="Frecord.altered"></a>

**`altered bool Read-only`**

an frecord is altered when some part of the recording may be missing.
This can be the case if a conflict occurred during the recording
(or connection was down)

<a id="Frecord.state"></a>

**`state enum Read-only`**

| State | Description |
| --- | --- |
| disabled | disabled |
| start_error | failed to start |
| waiting_start_time | scheduled |
| starting | starting |
| running | running |
| running_error | running with error |
| failed | failed |
| finished | finished |

<a id="Frecord.error"></a>

**`error enum Read-only`**

| Error |  |
| --- | --- |
| none |  |
| file_access_error |  |
| disk_full |  |
| private_but_no_private_dir |  |
| network_problem |  |
| resource_problem |  |
| no_stream_available |  |
| no_data_received |  |
| missed |  |
| stopped |  |
| internal_error |  |
| unknown_error |  |

<a id="Frecord.channel_uuid"></a>

**`channel_uuid string Read-only`**

channel uuid

<a id="Frecord.channel_name"></a>

**`channel_name string Read-only`**

optional channel name

<a id="Frecord.channel_quality"></a>

**`channel_quality enum Read-only`**

| channel_quality |  |
| --- | --- |
| auto |  |
| hd |  |
| sd |  |
| ld |  |
| 3d |  |

<a id="Frecord.channel_type"></a>

**`channel_type enum Read-only`**

| channel_type | Description |
| --- | --- |
| ‘’ (empty string) | auto |
| iptv | use only iptv streams |
| dvb | use only dvb streams |

<a id="Frecord.name"></a>

**`name string`**

record name

<a id="Frecord.subname"></a>

**`subname string`**

record subname

<a id="Frecord.broadcast_type"></a>

**`broadcast_type enum Read-only`**

| broadcast_type |  |
| --- | --- |
| tv |  |
| radio |  |

<a id="Frecord.start"></a>

**`start int Read-only`**

record start timestamp

<a id="Frecord.end"></a>

**`end int Read-only`**

record end timestamp

<a id="Frecord.secure"></a>

**`secure bool Read-only`**

flag set when the record is protected by DRM

<a id="frecord-api"></a>

## Frecord API

<a id="getting-the-list-of-frecords"></a>

### Getting the list of frecords

<a id="get--api-v8-pvr-finished-"></a>

**`GET /api/v8/pvr/finished/`**

**Example request**:

```http
GET /api/v8/pvr/finished/ HTTP/1.1
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
            "id": 5,
            "media": "Disque dur",
            "path": "Enregistrements",
            "filename": "M6 - Fier de ma maison - 27-06-2013 16h35 01h15 (5).m2ts",
            "byte_size": 4433869440,
            "has_record_gen": false,
            "record_gen_id": 0,
            "broadcast_type": "tv",
            "channel_uuid": "uuid-webtv-613",
            "channel_name": "M6",
            "channel_type": "dvb",
            "channel_quality": "hd",
            "name": "Fier de ma maison",
            "subname": "",
            "start": 1372343700,
            "end": 1372348200,
            "state": "finished",
            "error": "none",
            "enabled": true,
            "altered": true,
            "secure": false
        },

        [ ... ]

        {
            "id": 22,
            "media": "",
            "path": "",
            "filename": "TF1 - Nos chers voisins - 17-09-2014 15h23 01h (22).m2ts",
            "byte_size": 2421095040,
            "has_record_gen": false,
            "record_gen_id": 0,
            "broadcast_type": "tv",
            "channel_uuid": "uuid-webtv-612",
            "channel_name": "TF1",
            "channel_type": "",
            "channel_quality": "auto",
            "name": "Nos chers voisins",
            "subname": "",
            "start": 1410960180,
            "end": 1410963780,
            "state": "finished",
            "error": "none",
            "enabled": true,
            "altered": true,
            "secure": true
        }
    ]
}
```

<a id="getting-a-specific-frecord"></a>

### Getting a specific frecord

<a id="get--api-v8-pvr-finished-id"></a>

**`GET /api/v8/pvr/finished/{id}`**

Returns the requested [`Frecord`](pvr.md#Frecord "Frecord")

**Example request**:

```http
GET /api/v8/pvr/finished/236 HTTP/1.1
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
        "id": 236,
        "media": "NO NAME",
        "path": "",
        "filename": "France 3 - Tout le sport - 10-04-2015 20h00 10m (24).m2ts",
        "byte_size": 341752320,
        "has_record_gen": false,
        "record_gen_id": 0,
        "broadcast_type": "tv",
        "channel_uuid": "uuid-webtv-202",
        "channel_name": "France 3",
        "channel_type": "",
        "channel_quality": "auto",
        "name": "Tout le sport",
        "subname": "",
        "start": 1428688800,
        "end": 1428689400,
        "state": "finished",
        "error": "none",
        "enabled": true,
        "altered": true,
        "secure": false
    }
}
```

<a id="updating-an-frecord"></a>

### Updating an frecord

<a id="put--api-v8-pvr-finished-id"></a>

**`PUT /api/v8/pvr/finished/{id}`**

Update a [`Frecord`](pvr.md#Frecord "Frecord") properties

**Example request**:

```http
PUT /api/v8/pvr/finished/236 HTTP/1.1
Host: mafreebox.freebox.fr

{
  "name": "Tout le sport",
  "subname": "On est les champions"
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
        "id": 236,
        "media": "NO NAME",
        "path": "",
        "filename": "France 3 - Tout le sport - 10-04-2015 20h00 10m (24).m2ts",
        "byte_size": 341752320,
        "has_record_gen": false,
        "record_gen_id": 0,
        "broadcast_type": "tv",
        "channel_uuid": "uuid-webtv-202",
        "channel_name": "France 3",
        "channel_type": "",
        "channel_quality": "auto",
        "name": "Tout le sport",
        "subname": "On est les champions",
        "start": 1428688800,
        "end": 1428689400,
        "state": "finished",
        "error": "none",
        "enabled": true,
        "altered": true,
        "secure": false
    }
}
```

<a id="delete-an-frecord"></a>

### Delete an frecord

<a id="delete--api-v8-pvr-finished-id"></a>

**`DELETE /api/v8/pvr/finished/{id}`**

Delete a [`Frecord`](pvr.md#Frecord "Frecord") and associated files

**Example request**:

```http
DELETE /api/v8/pvr/finished/236 HTTP/1.1
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

<a id="storage-media"></a>

# Storage media

Media objects represent a storage on which records can be written to, typically
a disk.

<a id="media"></a>

## Media

Media has the following attributes:

<a id="Media"></a>

### Objet Media

<a id="Media.media"></a>

**`media string Read-only`**

name of the storage medium

<a id="Media.free_bytes"></a>

**`free_bytes int Read-only`**

number of free bytes on the medium

<a id="propriete-sans-ancre-pvr-50"></a>

**`total bytes int [ro]`**

total number of bytes on the medium

<a id="Media.record_time"></a>

**`record_time int Read-only`**

estimated record time in seconds for multiple channel types and qualities

<a id="media-api"></a>

## Media API

<a id="getting-the-list-of-media"></a>

### Getting the list of media

<a id="get--api-v8-pvr-media-"></a>

**`GET /api/v8/pvr/media/`**

**Example request**:

```http
GET /api/v8/pvr/media/ HTTP/1.1
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
            "media": "Disque dur",
            "free_bytes": 39700000000,
            "total_bytes": 244950000000,
            "record_time": {
                "dvb":  { "sd": 48461, "hd": 35245, "3d": 35245 },
                "iptv":  { "ld": 155078, "sd": 110770, "hd": 51012, "3d": 51012 }
            }
        },

        [ ... ]

        {
            "media":  "NO NAME",
            "free_bytes": 873930000,
            "total_bytes":  7790000000,
            "record_time":  {
                "dvb":  { "sd": 1066, "hd": 775, "3d": 775 },
                "iptv":  { "ld": 3413, "sd": 2438, "hd": 1122, "3d": 1122 }
            }
        }
    ]
}
```
