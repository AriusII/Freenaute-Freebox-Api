<a id="notif"></a>

<a id="notif-api"></a>

# Notif

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#notif)

## Navigation

- [Notification Errors](#notification-errors)
- [Notification Target object](#notification-target-object)
- [Notification API](#notification-api)


The Notification API allows you to access features related with notification,

<a id="notification-errors"></a>

## Notification Errors

When attempting to access the Notification API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| noent | no device with this id |
| inval | invalid parameters |

<a id="notification-target-object"></a>

## Notification Target object

Target Notification Target object have the following properties

<a id="NotificationTarget"></a>

### Objet NotificationTarget

<a id="NotificationTarget.id"></a>

**`id string`**

device unique id

<a id="NotificationTarget.last_use"></a>

**`last_use int`**

<a id="NotificationTarget.type"></a>

**`type string`**

ios | android | firebase

<a id="NotificationTarget.name"></a>

**`name string`**

device name

<a id="NotificationTarget.api_url"></a>

**`api_url string`**

url of the notification server used to handle communication with the devices

<a id="NotificationTarget.message_type"></a>

**`message_type string`**

notification message type

| Type | Description |
| --- | --- |
| data | only send the notification payload to the device |
| notification | send the notification payload along a notification title and body to the device |

<a id="NotificationTarget.subscriptions"></a>

**`subscriptions array`**

permission list array

| Type | Description |
| --- | --- |
| phone | notification when missing call |
| download | notification when download is finished |
| security | notification when alarm is on |
| box_state | notification when box state changed |
| lan_host | notification related to lan events |
| password_change | notification when admin password is changed |

<a id="notification-api"></a>

## Notification API

<a id="get-list-of-notification-target"></a>

### Get list of notification target

<a id="get--api-v11-notif-targets"></a>

**`GET /api/v11/notif/targets`**

Returns the collection of all `Notification Target`

**Example request**:

```http
GET /api/v11/notif/targets HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success":true,
   "result":[
      {
         "last_use":0,
         "type":"ios",
         "name":"iPhone de Xavier",
         "id":"11111111-2222-3333-4444-555555555555",
         "subscriptions":[
            "security",
                              "downloader",
                              "phone",
         ],
         "api_url": "https://monserver.example.com/mon_app",
         "message_type": "notification"
      },
      {
         "last_use":0,
         "type":"android",
         "name":"mamy",
         "id":"22222222-1111-3333-4444-555555555555",
         "subscriptions":[
                              "phone"
         ],
         "api_url": "https://monserver.example.com/mon_app",
         "message_type": "notification"
   ]
}
```

<a id="get-a-given-notification-target-by-this-id"></a>

### Get a given notification target by this id

<a id="get--api-v11-notif-targets-id"></a>

**`GET /api/v11/notif/targets/{id}`**

Returns the `Notification Target` with the given id

**Example request**:

```http
GET /api/v11/notif/targets/11111111-2222-3333-4444-555555555555 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success":true,
   "result":[
      {
         "last_use":0,
         "type":"ios",
         "name":"iPhone de Xavier",
         "id":"11111111-2222-3333-4444-555555555555",
         "subscriptions":[
            "security",
                              "downloader",
                              "phone",
         ],
         "api_url": "https://monserver.example.com/mon_app",
         "message_type": "notification"
      }
   ]
}
```

<a id="delete-a-notification-target"></a>

### Delete a notification target

<a id="delete--api-v11-notif-targets-id"></a>

**`DELETE /api/v11/notif/targets/{id}`**

Deletes the `Notification Target` with the given id.

**Example request**:

```http
DELETE /api/v11/notif/targets/22222222-1111-3333-4444-555555555555 HTTP/1.1
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

<a id="update-a-notification-target"></a>

### Update a notification target

<a id="put--api-v11-notif-targets-id"></a>

**`PUT /api/v11/notif/targets/{id}`**

Update the `Notification Target` with the given id.

**Example request**:

```http
PUT /api/v11/notif/targets/22222222-1111-3333-4444-555555555555 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
               "name": "iPhone de Xavier",
               "type": "ios",
               "token": "token_token_token_token_token_token_token",
               "subscriptions": ["download", "phone"],
               "api_url": "https://monserver.example.com/mon_app",
   "message_type": "notification"
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

<a id="add-a-notification-target"></a>

### Add a notification target

<a id="post--api-v11-notif-targets-"></a>

**`POST /api/v11/notif/targets/`**

Create an new `Notification Target`.

**Example request**:

```http
POST /api/v11/notif/targets/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
               "name": "iPhone de Xavier",
               "type": "ios",
               "token": "token_token_token_token_token_token_token",
               "subscriptions": ["download", "phone"],
               "api_url": "https://monserver.example.com/mon_app",
   "message_type": "notification"
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

<a id="notification-server-specification"></a>

# Notification server specification

When a notification should be sent, the Freebox will use this API on the address specified in the notification target.
Your server must implement this API contract :

<a id="post--register"></a>

**`POST /register`**

A new target has been registered

```json
{
  "box_id":"", //uuid of the box that is sending the request
  "device_type":"ios|android|firebase", //notification service type of the target
  "token":"", //the notification service token
  "device_name":"",
  "device_id":"" //the target id
}
```

<a id="delete--register-box_id-device_id"></a>

**`DELETE /register/{box_id}/{device_id}`**

A target has been deleted

<a id="post--send"></a>

**`POST /send`**

Send a notification

```json
{
  "devices":["", "", "", ...], //an array of target id
  "title":"", //notification title (optional - only sent if target message type is "notification")
  "body":"", //notification body (optional - only sent if target message type is "notification")
  "payload": {}, //json payload to send as notification data
  "box_id":"" //uuid of the box that is sending the request
}
```

Response :
This API send back the device ids in two lists : failure and success

```json
{
  "failureIds": ["device_id_1", "device_id_2", ...],
  "successIds": ["device_id_3", ...]
}
```

<a id="notifications-specification"></a>

# Notifications specification

Notifications sent to registered devices has a payload depending on notification type :

<a id="downloader"></a>

## Objet downloader

<a id="downloader.box_id"></a>

**`box_id string`**

ID of the box that sent the notification

<a id="downloader.type"></a>

**`type string`**

Notification type : downloader

<a id="downloader.data"></a>

**`data int`**

ID of the download task that triggered the notification

<a id="downloader.event"></a>

**`event enum`**

Downloader event that triggered the notification

| event | Description |
| --- | --- |
| task_done | The download task is complete |
| task_error | The download task has failed |
| task_seeding_done | The download task seeding is complete |

<a id="phone"></a>

## Objet phone

<a id="phone.box_id"></a>

**`box_id string`**

ID of the box that sent the notification

<a id="phone.type"></a>

**`type string`**

Notification type : phone

<a id="phone.data"></a>

**`data CallEntry`**

Call object that triggered the notification

<a id="phone.event"></a>

**`event enum`**

Phone event that triggered the notification

| event | Description |
| --- | --- |
| missed_call | A call has been missed |

<a id="box_state"></a>

## Objet box_state

<a id="box_state.box_id"></a>

**`box_id string`**

ID of the box that sent the notification

<a id="box_state.type"></a>

**`type string`**

Notification type : box_state

<a id="box_state.event"></a>

**`event enum`**

Box state event that triggered the notification

| event | Description |
| --- | --- |
| pub_up | Wan public connection went up |
| enter_sleep | Box will enter sleep mode |
| shut_down | Box will shut down |
| reboot | Box will reboot |

<a id="lan_host"></a>

## Objet lan_host

<a id="lan_host.box_id"></a>

**`box_id string`**

ID of the box that sent the notification

<a id="lan_host.type"></a>

**`type string`**

Notification type : lan

<a id="lan_host.host_id"></a>

**`host_id string`**

ID of the host that triggered the notification

<a id="lan_host.interface"></a>

**`interface string`**

The LAN interface the host is connected to

<a id="lan_host.event"></a>

**`event enum`**

LAN host event that triggered the notification

| event | Description |
| --- | --- |
| first_connection | The device is connected for the first time |

<a id="password_change"></a>

## Objet password_change

<a id="password_change.box_id"></a>

**`box_id string`**

ID of the box that sent the notification

<a id="password_change.type"></a>

**`type string`**

Notification type : password_change

<a id="password_change.ip"></a>

**`ip string`**

IP of the lan host that requested password change
