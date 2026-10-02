<a id="profile-management"></a>

# Profile management

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#profile-management)

## Navigation

- [Profile Object](#profile-object)
- [Profiles API](#profiles-api)
- [Network Control Object](#network-control-object)
- [Network Control API](#network-control-api)
- [Rule Object](#rule-object)
- [Rule API](#rule-api)


<a id="profile-object"></a>

## Profile Object

<a id="Profile"></a>

### Objet Profile

<a id="Profile.id"></a>

**`id int Read-only`**

unique id of this profile

<a id="Profile.name"></a>

**`name string`**

name of this profile

<a id="Profile.icon"></a>

**`icon string`**

URL of the icon relative to root of the API domain.

<a id="profiles-api"></a>

## Profiles API

<a id="get-the-list-of-profiles"></a>

### Get the list of profiles

<a id="get--api-v8-profile"></a>

**`GET /api/v8/profile`**

**Example request**:

```http
GET /api/v8/profile HTTP/1.1
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
          "id": 2,
          "name": "r0ro",
          "url": "/resources/images/profile/profile_04.png"
      },

        [ ... ]

      {
          "id": 7,
          "name": "Xav",
          "url": "/resources/images/profile/profile_02.png"
      }
    ]

}
```

<a id="get-a-profile"></a>

### Get a profile

<a id="get--api-v8-profile-id"></a>

**`GET /api/v8/profile/{id}`**

Get the [`Profile`](profile.md#Profile "Profile") with the given id

**Example request**:

```http
GET /api/v8/profile/2 HTTP/1.1
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
       "id": 2,
       "name": "r0ro",
       "url": "/resources/images/profile/profile_04.png"
   }
}
```

<a id="add-a-profile"></a>

### Add a profile

<a id="post--api-v8-profile-"></a>

**`POST /api/v8/profile/`**

**Example request**:

```http
POST /api/v8/profile HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "name": "Pierrot",
   "url": "/resources/images/profile/profile_04.png"
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
    "result":
       {
          "id": 3,
      }
}
```

<a id="delete-a-profile"></a>

### Delete a profile

<a id="delete--api-v8-profile-id"></a>

**`DELETE /api/v8/profile/{id}`**

**Example request**:

```http
DELETE /api/v8/profile/2 HTTP/1.1
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
}
```

<a id="update-a-profile"></a>

### Update a profile

<a id="put--api-v8-profile-3"></a>

**`PUT /api/v8/profile/3`**

**Example request**:

```http
PUT /api/v8/profile HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "name": "Pierrot",
   "url": "/resources/images/profile/profile_02.png"
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
    "result":
       {
          "id": 3,
          "name": "Pierrot",
          "url": "/resources/images/profile/profile_02.png"
      }
}
```

<a id="network-control-object"></a>

<a id="net-object"></a>

## Network Control Object

The different modes supported are :

| mode | Description |
| --- | --- |
| allowed | access is allowed |
| denied | access is denied |
| webonly | access is granted only for HTTP and HTTPS traffic; legacy mode, use not recommended. |

<a id="NetworkControl"></a>

### Objet NetworkControl

<a id="NetworkControl.profile_id"></a>

**`profile_id int Read-only`**

Id of the profile this network control is associated with. This is read-only, unless you use the POST api to add a network control.

<a id="NetworkControl.next_change"></a>

**`next_change int Read-only`**

UNIX timestamp of next rule change in seconds. 0 if no next change.

<a id="NetworkControl.override_mode"></a>

**`override_mode enum`**

mode of current override.

<a id="NetworkControl.current_mode"></a>

**`current_mode enum Read-only`**

mode in use. If override is true, it will be override_mode, otherwise it’s the mode from the rules attached to this NetworkControl.

<a id="NetworkControl.rule_mode"></a>

**`rule_mode enum Read-only`**

mode that would be in use if there was no override. Depends only on rules, and is useful to determine what will happen when override is lifted.

<a id="NetworkControl.override_until"></a>

**`override_until int`**

Unix timestamp in seconds when override ends. Relevant when override is true. Set at 0 for unlimited.

<a id="NetworkControl.override"></a>

**`override bool`**

Whether there’s an override at the moment.

<a id="NetworkControl.macs"></a>

**`macs [] array of string`**

List of mac adresses associated with this profile’s network control.

<a id="NetworkControl.hosts"></a>

**`hosts [] array of LanHost Read-only`**

List of [Lan Host objects](../reseau/lan.md#lan-host-object) associated with this profile’s network control. Derived from the macs array.

<a id="NetworkControl.resolution"></a>

**`resolution int Read-only`**

Control resolution per day of this network control. Currently at 288.

<a id="NetworkControl.cdayranges"></a>

**`cdayranges [] array of string`**

list of custom day range, each custom day range represents a
group of days for which you want to use a different planning
than other week days.

For instance a custom day range can contain the list of your children
holidays.

| cdayranges | Description |
| --- | --- |
| :fr_bank_holidays | French bank holidays |
| :fr_school_holidays_a | French school holidays - Zone A |
| :fr_school_holidays_b | French school holidays - Zone B |
| :fr_school_holidays_c | French school holidays - Zone C |
| :fr_school_holidays_corse | French school holidays - Corse |

each cdayranges can be a coma separated list of cdayranges, for
instance “:fr_bank_holidays,:fr_school_holidays_b”

<a id="network-control-api"></a>

## Network Control API

<a id="get-network-control-for-all-profiles"></a>

### Get Network Control for all profiles

<a id="get--api-v8-network_control"></a>

**`GET /api/v8/network_control`**

<a id="get-network-control-for-a-profile"></a>

### Get Network Control for a profile

<a id="get--api-v8-network_control-profile_id"></a>

**`GET /api/v8/network_control/{profile_id}`**

**Example request**:

```http
GET /api/v8/network_control/5 HTTP/1.1
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
    "result":
     {
         "profile_id": 5,
         "next_change": 0,
         "override": false,
         "override_mode": "denied",
         "current_mode": "allowed",
         "macs": [
             "D8:A2:CA:FE:BA:DF",
             "D0:23:BE:DE:AD:EF"
         ],
         "hosts": [
            "PC-de-mamie",
            "Cantal-chromebook"
         ],
         "resolution": 288,
         "cdayranges": []
     }
}
```

<a id="update-network-control-for-a-profile"></a>

### Update Network Control for a profile

<a id="put--api-v8-network_control-profile_id"></a>

**`PUT /api/v8/network_control/{profile_id}`**

**Example request**:

```http
PUT /api/v8/network_control/3 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
    "profile_id": 3,
    "next_change": 0,
    "override": false,
    "override_mode": "allowed",
    "current_mode": "denied",
    "macs": [
        "98:E8:FA:FE:BA:42",
        "2C:CC:44:D1:AD:4F"
    ],
    "hosts": [
       "3DS-Thibault",
       "Vita-Rodolphe"
    ],
    "resolution": 288,
    "cdayranges": []
}
```

**Example response**:

```json
{
    "success": true,
    "result":
     {
         "profile_id": 3,
         "next_change": 0,
         "override": false,
         "override_mode": "allowed",
         "current_mode": "denied",
         "macs": [
             "98:E8:FA:FE:BA:42",
             "2C:CC:44:D1:AD:4F"
         ],
         "hosts": [
            "3DS-Thibault",
            "Vita-Rodolphe"
         ],
         "resolution": 288,
         "cdayranges": []
     }
}
```

<a id="get-migration-to-new-default-mode-status"></a>

### Get migration to new default mode status

Verify if migration to new default mode has been done (“allowed” only) if default mode was modified.

<a id="get--api-v8-network_control-migrate"></a>

**`GET /api/v8/network_control/migrate`**

**Example request**:

```http
GET /api/v8/network_control/migrate HTTP/1.1
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
    "result":
     {
         "default_mode_migrated": false
     }
}
```

<a id="migrate-to-new-default-mode"></a>

### Migrate to new default mode

Do migration to new default mode (“allowed”) if it was modified previously.

<a id="post--api-v8-network_control-migrate"></a>

**`POST /api/v8/network_control/migrate`**

**Example request**:

```http
POST /api/v8/network_control/migrate HTTP/1.1
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
    "result":
     {
         "default_mode_migrated": true
     }
}
```

<a id="rule-object"></a>

## Rule Object

<a id="NetworkControlRule"></a>

### Objet NetworkControlRule

<a id="NetworkControlRule.id"></a>

**`id int Read-only`**

Unique rule identifier.

<a id="NetworkControlRule.profile_id"></a>

**`profile_id int Read-only`**

Id of profile this rule applies to.

<a id="NetworkControlRule.name"></a>

**`name string`**

Rule name

<a id="NetworkControlRule.mode"></a>

**`mode enum`**

Mode described in [Network Control Object](profile.md#net-object)

<a id="propriete-sans-ancre-profile-19"></a>

**`start_time`**

Seconds since start of day (00:00) when rule starts. Must be in increments
of the resolution. When resolution is 288, it means 5 minutes slots, so the
value must be a multiple of 300.

<a id="propriete-sans-ancre-profile-20"></a>

**`end_time`**

Time of day in seconds since start of day (00:00) when rule ends. end_time
modulo 300 must always be zero when resolution is 288.

<a id="NetworkControlRule.weekdays"></a>

**`weekdays [] array of bool`**

Array of days of weeks when this rule apply. 8th one is for cdayranges.

<a id="NetworkControlRule.enabled"></a>

**`enabled bool`**

Whether rule is enabled.

<a id="rule-api"></a>

## Rule API

<a id="get-network-control-rules-for-a-profile"></a>

### Get Network Control Rules for a profile

<a id="get--api-v8-network_control-profile_id-rules"></a>

**`GET /api/v8/network_control/{profile_id}/rules`**

Returns the list of rules for this profile

<a id="get-a-network-control-rule"></a>

### Get a Network Control Rule

<a id="get--api-v8-network_control-profile_id-rules-rule_id"></a>

**`GET /api/v8/network_control/{profile_id}/rules/{rule_id}`**

Returns one rule.

<a id="create-a-network-control-rule"></a>

### Create a Network Control Rule

<a id="post--api-v8-network_controlr-profile_id-rules-"></a>

**`POST /api/v8/network_controlr/{profile_id}/rules/`**

Create a rule given in parameter.

<a id="update-a-network-control-rule"></a>

### Update a Network Control Rule

<a id="put--api-v8-network_control-id-rules-rule_id"></a>

**`PUT /api/v8/network_control/{id}/rules/{rule_id}`**

Update rule.

<a id="delete-a-network-control-rule"></a>

### Delete a Network Control Rule

<a id="delete--api-v8-network_control-id-rules-rule_id"></a>

**`DELETE /api/v8/network_control/{id}/rules/{rule_id}`**

Delete rule.
