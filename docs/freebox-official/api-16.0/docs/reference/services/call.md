<a id="call"></a>

# Call

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#call)

## Navigation

- [Call Errors](#call-errors)
- [Call Object](#call-object)
- [Call API](#call-api)
- [Voicemail Errors](#voicemail-errors)
- [Voicemail Object](#voicemail-object)
- [Voicemail API](#voicemail-api)


With the call API you access the Freebox call logs.

<a id="call-errors"></a>

## Call Errors

When attempting to access the call API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| internal_error | Internal error |
| invalid_id | No call with this id |
| invalid_category | Invalid call category |

<a id="call-object"></a>

## Call Object

Call entries have the following properties

<a id="CallEntry"></a>

### Objet CallEntry

<a id="CallEntry.id"></a>

**`id int Read-only`**

id

<a id="CallEntry.type"></a>

**`type enum Read-only`**

The valid call types are:

| Type | Description |
| --- | --- |
| missed | Missed incoming call |
| accepted | Incoming call |
| outgoing | Outgoing call |

<a id="CallEntry.datetime"></a>

**`datetime timestamp Read-only`**

Call creation timestamp.

<a id="CallEntry.number"></a>

**`number string Read-only`**

Callee number for outgoing calls.
Caller number for incoming calls.

<a id="CallEntry.name"></a>

**`name string Read-only`**

Callee name for outgoing calls.
Caller name for incoming calls.

For incoming call if the network does not provide a contact
name, we try to use the contact database to find a suitable name

<a id="CallEntry.duration"></a>

**`duration int Read-only`**

Call duration in seconds.

<a id="CallEntry.new"></a>

**`new bool`**

Call entry has not been acknowledged yet.

<a id="CallEntry.contact_id"></a>

**`contact_id int Read-only`**

If the number matches an entry in the contact database, the id
of the matching contact.

<a id="call-api"></a>

## Call API

This is the call API

<a id="list-every-calls"></a>

### List every calls

<a id="get--api-v10-call-log-"></a>

**`GET /api/v10/call/log/`**

Returns the collection of all [`CallEntry`](call.md#CallEntry "CallEntry") call entries

**Example request**:

```http
GET /api/v10/call/log/ HTTP/1.1
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
         number: "0102030405",
         type: "missed",
         id: 69,
         duration: 1,
         datetime: 1359546363,
         contact_id: 56,
         line_id: 0,
         name: "r0ro (Freebox)",
         new: true
      },
      {
         number: "**1",
         type: "outgoing",
         id: 68,
         duration: 5,
         datetime: 1359545960,
         contact_id: 0,
         line_id: 0,
         name: "**1",
         new: false
      }
   ]
}
```

<a id="delete-all-calls"></a>

### Delete all calls

<a id="post--api-v10-call-log-delete_all-"></a>

**`POST /api/v10/call/log/delete_all/`**

Remove all [`CallEntry`](call.md#CallEntry "CallEntry") call entries

**Example request**:

```http
GET /api/v10/call/log/delete_all HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

<a id="mark-all-calls-as-read"></a>

### Mark all calls as read

<a id="post--api-v10-call-log-mark_all_as_read-"></a>

**`POST /api/v10/call/log/mark_all_as_read/`**

Mark all [`CallEntry`](call.md#CallEntry "CallEntry") call entries as read

**Example request**:

```http
GET /api/v10/call/log/mark_all_as_read HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

<a id="access-a-given-call-entry"></a>

### Access a given call entry

<a id="get--api-v10-call-log-id"></a>

**`GET /api/v10/call/log/{id}`**

Returns the [`CallEntry`](call.md#CallEntry "CallEntry") task with the given id

**Example request**:

```http
GET /api/v10/call/log/69 HTTP/1.1
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
      number: "0102030405",
      type: "missed",
      id: 69,
      duration: 1,
      datetime: 1359546363,
      contact_id: 56,
      line_id: 0,
      name: "Romain Bureau",
      new: true
   }
}
```

<a id="delete-a-call"></a>

### Delete a call

<a id="delete--api-v10-call-log-id"></a>

**`DELETE /api/v10/call/log/{id}`**

Deletes the [`CallEntry`](call.md#CallEntry "CallEntry") with the given id.

**Example request**:

```http
DELETE /api/v10/call/log/69 HTTP/1.1
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

<a id="update-a-call-entry"></a>

### Update a call entry

<a id="put--api-v10-call-log-id"></a>

**`PUT /api/v10/call/log/{id}`**

Updates the [`CallEntry`](call.md#CallEntry "CallEntry") task with the given id

**Example request**:

```http
PUT /api/v10/call/log/69 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "new": "false"
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
      number: "0102030405",
      type: "missed",
      id: 69,
      duration: 1,
      datetime: 1359546363,
      contact_id: 56,
      line_id: 0,
      name: "Romain Bureau",
      new: false
   }
}
```

<a id="account"></a>

# Account

The account API returns the phone number associated with the subscription.

<a id="get--api-v10-call-account"></a>

**`GET /api/v10/call/account`**

Returns an object containing the phone number associated with the subscription.

**Example request**:

```http
GET /api/v10/call/account/ HTTP/1.1
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
      "phone_number": "0999999999",
  }
}
```

<a id="voicemail"></a>

# Voicemail

The voicemail API lets one access voicemail messages.

<a id="voicemail-errors"></a>

## Voicemail Errors

The following errors may be encountered with the voicemail API:

| error_code | Description |
| --- | --- |
| internal_error | Internal error |
| invalid_id | No voicemail with this id |

<a id="voicemail-object"></a>

## Voicemail Object

Voicemail entries have the following properties

<a id="VoicemailEntry"></a>

### Objet VoicemailEntry

<a id="VoicemailEntry.id"></a>

**`id string Read-only`**

id

<a id="VoicemailEntry.country_code"></a>

**`country_code string Read-only`**

Country code part of the caller number. May be empty.

<a id="VoicemailEntry.phone_number"></a>

**`phone_number string Read-only`**

Caller number. May be empty.

<a id="VoicemailEntry.date"></a>

**`date timestamp Read-only`**

Voicemail creation timestamp.

<a id="VoicemailEntry.read"></a>

**`read bool`**

Voicemail read status

<a id="VoicemailEntry.duration"></a>

**`duration int Read-only`**

Voicemail duration in seconds

<a id="voicemail-api"></a>

## Voicemail API

<a id="list-voicemails"></a>

### List voicemails

<a id="get--api-v10-call-voicemail-"></a>

**`GET /api/v10/call/voicemail/`**

Returns a collection of all [`VoicemailEntry`](call.md#VoicemailEntry "VoicemailEntry") voicemail entries

**Example request**:

```http
GET /api/v10/call/voicemail/ HTTP/1.1
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
      "phone_number": "699999999",
      "read": false,
      "id": "20221215_154135_r0334371508.au",
      "duration": 8,
      "country_code": 33,
      "date": 1671115295
    }
  ]
}
```

<a id="access-a-specific-voicemail-entry"></a>

### Access a specific voicemail entry

<a id="get--api-v10-call-voicemail-id"></a>

**`GET /api/v10/call/voicemail/{id}`**

Returns the [`VoicemailEntry`](call.md#VoicemailEntry "VoicemailEntry") task with the given id

**Example request**:

```http
GET /api/v10/call/voicemail/20221215_154135_r0334371508.au HTTP/1.1
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
      "phone_number": "699999999",
      "read": false,
      "id": "20221215_154135_r0334371508.au",
      "duration": 8,
      "country_code": 33,
      "date": 1671115295
  }
}
```

<a id="delete-a-voicemail"></a>

### Delete a voicemail

<a id="delete--api-v10-call-voicemail-id"></a>

**`DELETE /api/v10/call/voicemail/{id}`**

Deletes the [`VoicemailEntry`](call.md#VoicemailEntry "VoicemailEntry") with the given id.

**Example request**:

```http
DELETE /api/v10/call/voicemail/20221215_154135_r0334371508.au HTTP/1.1
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

<a id="update-a-voicemail-entry"></a>

### Update a voicemail entry

<a id="put--api-v10-call-voicemail-id"></a>

**`PUT /api/v10/call/voicemail/{id}`**

Updates the [`VoicemailEntry`](call.md#VoicemailEntry "VoicemailEntry") with the given id

**Example request**:

```http
PUT /api/v10/call/voicemail/20221215_154135_r0334371508.au HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "phone_number": "699999999",
  "read": true,
  "id": "20221215_154135_r0334371508.au",
  "duration": 8,
  "country_code": 33,
  "date": 1671115295
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
    "phone_number": "699999999",
    "read": true,
    "id": "20221215_154135_r0334371508.au",
    "duration": 8,
    "country_code": 33,
    "date": 1671115295
  }
}
```

<a id="retrieve-a-voicemail"></a>

### Retrieve a voicemail

<a id="get--api-v10-call-voicemail-id-audio_file"></a>

**`GET /api/v10/call/voicemail/{id}/audio_file`**

Download voicemail message in WAV format.

**Example request**:

```http
GET /api/v10/call/voicemail/20221215_154135_r0334371508.au/audio_file HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: audio/wav; charset=utf-8
Content-Length: 60218
Content-Disposition: inline; filename="20221215_154135_r0334371508.wav"

/* binary data */
```
