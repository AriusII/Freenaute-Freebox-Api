# WebSocket API

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#ws-api)

## Navigation

- [WebSocket API conventions](#websocket-api-conventions)
- [WebSocket event API](#websocket-event-api)


<a id="ws-api"></a>

WebSocket allow bidirectional communication between your api client
and the Freebox. This allow more interactivity without the need of
frequently polling data from the Freebox.

For WebSocket access, you must use the same Authentication mechanism as
for regular http api request. This means that you must include a proper
**X-Fbx-App-Auth** header when you open the WebSocket connection.

Once the connection is established, most of messages sent via the WebSocket
are text based (using utf-8 as per WebSocket specifications) and encoded
as JSON objects.

The WebSocket frames maximum accepted size is 1 MB

<a id="websocket-api-conventions"></a>

## WebSocket API conventions

As for HTTP api, the client can make requests to the Freebox (the available
requests are specified per api).

The requests use the following format:

<a id="WebSocketRequest"></a>

### Objet WebSocketRequest

<a id="WebSocketRequest.request_id"></a>

**`request_id int Optionnal`**

if you specify a request_id in your request, it will be added
in the corresponding reply, so that you can correlate responses
to the request

<a id="WebSocketRequest.action"></a>

**`action string`**

the request ‘action’

(available actions are described in each api)

Other fields, related to a specific action, will be used as ‘action’ parameters

Responses to such requests will have the following format:

<a id="WebSocketResponse"></a>

### Objet WebSocketResponse

<a id="WebSocketResponse.request_id"></a>

**`request_id int`**

if you set a request_id in your WebSocketRequest, the same request_id
will be returned in the associated response

<a id="WebSocketResponse.action"></a>

**`action string`**

the action specified in the associated WebSocketRequest

<a id="WebSocketResponse.success"></a>

**`success boolean Read-only`**

indicates if the request was successful

<a id="WebSocketResponse.result"></a>

**`result object Read-only`**

the result of the request.

(It may be omitted if the request does not expect any result)

<a id="WebSocketResponse.error_code"></a>

**`error_code string Read-only`**

In case of request error, this error_code provides information
about the error.

The possible error_code values are documented for each API.

<a id="WebSocketResponse.msg"></a>

**`msg string Read-only`**

In cas of error, provides a French error message relative to the
error

When the Freebox wants to send a notification on WebSocket it will have
the following format:

<a id="WebSocketNotification"></a>

### Objet WebSocketNotification

<a id="WebSocketNotification.action"></a>

**`action enum Read-only`**

The action will have the value ‘notification’

<a id="WebSocketNotification.success"></a>

**`success boolean Read-only`**

will be True

<a id="WebSocketNotification.source"></a>

**`source string Read-only`**

The name of the source of the notification

<a id="WebSocketNotification.event"></a>

**`event string Read-only`**

The name of event that generated the notification

<a id="WebSocketNotification.result"></a>

**`result object Read-only`**

the content of the notification (may be omitted if no data
has to be transferred along with the notification)

<a id="websocket-event-api"></a>

<a id="ws-event-api"></a>

## WebSocket event API

This API is used to send events to an application, removing the need to poll when waiting for a long operation to complete. It follows the conventions of the [WebSocket API](websocket.md#ws-api).

This is a text websocket that sends json, one per line.

<a id="get--api-v8-ws-event"></a>

**`GET /api/v8/ws/event`**

The application sends `RegisterACtion` to subscribe to an event channel. It will subsequently receive events on this websocket.

<a id="register-action"></a>

### Register Action

<a id="RegisterAction"></a>

#### Objet RegisterAction

<a id="RegisterAction.action"></a>

**`action enum`**

Value should be “register”

<a id="RegisterAction.events"></a>

**`events [] array of enum`**

List of events to subscribe for. Possible values:

| event name | event result object type | Description |
| --- | --- | --- |
| vm_state_changed | [`VmStateChange`](../stockage-vm/vm.md#VmStateChange "VmStateChange") | VM status has changed |
| vm_disk_task_done | [`VmDiskTask`](../stockage-vm/vm.md#VmDiskTask "VmDiskTask") | VM disk task done |
| lan_host_l3addr_reachable | [`LanHost`](../reseau/lan.md#LanHost "LanHost") | LAN machine had an L3 address (IPv4 or IPv6) become reachable. Usually when a machine appears on the network, or changes IP. |
| lan_host_l3addr_unreachable | [`LanHost`](../reseau/lan.md#LanHost "LanHost") | LAN machine had an L3 address (IPv4 or IPv6) become unreachable. Usually when a machine disappears from the network (after a timeout), or changes IP. |

Response is usually `{"success": true, "action": "register"}`

Events will be sent as [`WebSocketNotification`](websocket.md#WebSocketNotification "WebSocketNotification") ; the event name will be split in source (prefix) and event (suffix). For example, vm_disk_task_done will have source “vm”, and event “disk_task_done”:

```json
{
   "action" : "notification",
   "success" : true,
   "source" : "vm"
   "event" : "disk_task_done",
   "result" : {
      "done" : true,
      "error" : false,
      "id" : 1
   }
}
```
