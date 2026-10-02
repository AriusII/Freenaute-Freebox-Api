<a id="raid-api-unstable"></a>

<a id="raid-api"></a>

# RAID API [UNSTABLE]

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#raid-api-unstable)

## Navigation

- [RAID API Errors](#raid-api-errors)
- [RAID API objects](#raid-api-objects)
- [RAID API actions](#raid-api-actions)


This API allows you to manage the Freebox internal raid arrays for disks
connected to the Freebox

This API is unstable, it can be modified without notice in next
releases.

<a id="raid-api-errors"></a>

## RAID API Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| inval | Invalid parameters(s) |
| no_sys | Function not available |
| member_not_found | No member found |
| members_too_many | Too many members |
| array_not_found | RAID array not found |
| array_stop_failed | Error when stopping the RAID array |
| array_start_failed | Error when starting the RAID array |
| array_destroy_failed | Error when destroying the RAID array |
| array_not_running | The RAID array is not active |
| array_not_stopped | The RAID array is not stopped |
| array_degraded | The RAID array is degraded |
| array_not_degraded | The RAID array is not degraded |
| array_complete | The RAID array is full |
| already_member | The specified disks are already members of a RAID array |
| disk_more_than_once | The same disk has been specified more than once |
| disks_missing | Insufficient number of disks |
| bad_disk_location | Only internal drives can be used in a RAID array |
| disk_internal | This disk cannot be used in a RAID array |
| disk_busy | Disk is busy |
| create_failed | RAID array creation failed |
| create_too_many_members | The number of disks is too high (basic) |
| create_not_enough_members | The number of disks is too small |
| create_bad_member_count | The number of disks is incorrect (raid10) |
| sync_action_bad_level | This type of RAID array does not support synchronization |
| sync_action_array_busy | This RAID array is being resynchronized/restored |
| sync_action_bad_action | It is not possible to force resynchronization manually |
| sync_action_failed | This action has been denied |
| check_interval_too_large | Check interval is too long |
| check_interval_not_supported | This check interval is not supported |
| remove_bad_level | This type of RAID array does not allow member removal |
| remove_not_enough_active | Not enough active members to allow removal of a member |
| remove_failed | Failure to remove a member |
| add_too_many | Too many new members |
| add_member_too_small | One of the members is too small to be added to this array |
| add_failed | Failed to add member |
| member_examine_data_failed | Unable to examine member data |
| sync_speed_min_greaterthan_max | Minimum sync speed is more important than maximum speed |
| sync_speed_min_toohigh | The minimum sync speed is too high |
| sync_speed_max_toohigh | The maximum sync speed it too high |
| sync_speed_min_toolow | The minimum sync speed is too low |
| sync_speed_max_toolow | The maximum sync speed is too low |
| sync_speed_set_failed | Error changing synchronization speed |
| grow_bad_level | RAID level migration not possible |
| grow_not_enough_disks | Not enough disks for expansion |
| grow_failed | Expansion failed |
| grow_array_busy | Cannot extend a busy RAID array |
| grow_member_too_small | One of the members is too small to expand the raid array |
| rescan_member_failed | One or more members could not be rescanned |
| add_spares_busy | Cannot add out-of-sync disks when the array is busy |
| add_spares_nospares | No out-of-sync member detected |
| add_spares_complete | The RAID Array is full and cannot add an out of sync member |
| add_spares_failed | Failed to add out-of-sync disks |

<a id="raid-api-objects"></a>

## RAID API objects

<a id="raid-array-object"></a>

### RAID Array object

<a id="RaidArray"></a>

#### Objet RaidArray

<a id="RaidArray.id"></a>

**`id int Read-only`**

unique id of this array. Used as a reference for API calls.

<a id="RaidArray.state"></a>

**`state enum`**

| state | Description |
| --- | --- |
| stopped | Array is stopped |
| running | Array is running |
| error | Array is in error |

<a id="RaidArray.name"></a>

**`name string`**

The array name

<a id="RaidArray.level"></a>

**`level enum`**

| level | Description |
| --- | --- |
| basic | Basic RAID level, like a single drive raid1 array |
| raid0 | RAID 0 |
| raid1 | RAID 1 |
| raid5 | RAID 5 |
| raid10 | RAID 10 |

<a id="RaidArray.disk_id"></a>

**`disk_id int Read-only`**

The disk id of the array, for use with the disk format API.

<a id="RaidArray.uuid"></a>

**`uuid string Read-only`**

The array unique id. Only this id is guaranteed to stay stable across reboots.

<a id="RaidArray.sync_action"></a>

**`sync_action enum Read-only`**

| sync_action | Description |
| --- | --- |
| idle | Array is idle |
| resync | Sync operation in progress |
| recover | Recover operation in progress |
| check | Array is being checked |
| repair | Repair operation in progress |
| reshape | Array growth in progress |
| frozen | Array is frozen |

<a id="RaidArray.sysfs_state"></a>

**`sysfs_state enum Read-only`**

Low-level Linux-specific md state value read in sysfs [array_state property](https://www.kernel.org/doc/html/v5.10/admin-guide/md.html#md-devices-in-sysfs).

| sysfs_state |
| --- |
| clear |
| inactive |
| suspended |
| readonly |
| read_auto |
| clean |
| active |
| write_pending |
| active_idle |

<a id="RaidArray.array_size"></a>

**`array_size int Read-only`**

Size of array in bytes.

<a id="RaidArray.raid_disks"></a>

**`raid_disks int Read-only`**

Number of members that should be in this array.

<a id="RaidArray.sync_speed"></a>

**`sync_speed int Read-only`**

Sync speed in bytes per second

<a id="RaidArray.sync_completed_pos"></a>

**`sync_completed_pos int Read-only`**

Current position of sync process.

<a id="RaidArray.sync_completed_end"></a>

**`sync_completed_end int Read-only`**

End position of sync process: total of bytes to sync.

<a id="RaidArray.sync_completed_percent"></a>

**`sync_completed_percent int Read-only`**

Percentage of sync completion.

<a id="RaidArray.check_interval"></a>

**`check_interval int Read-only`**

Check interval in seconds.

<a id="RaidArray.last_check"></a>

**`last_check int Read-only`**

Unix timestamp of last check in seconds.

<a id="RaidArray.next_check"></a>

**`next_check int Read-only`**

Unix timestamp of next check in seconds. Might be 0 if check_interval is 0.

<a id="RaidArray.degraded"></a>

**`degraded bool Read-only`**

Whether the array is degraded or not.

<a id="RaidArray.members"></a>

**`members [] array of RaidMember`**

List of members of this array

<a id="raid-member-object"></a>

### RAID Member object

<a id="RaidMember"></a>

#### Objet RaidMember

<a id="RaidMember.id"></a>

**`id int Read-only`**

unique id of this member. This corresponds to the disk id, usable with the Storage Disk API.

<a id="RaidMember.array_id"></a>

**`array_id int Read-only`**

id of the array this member is in

<a id="RaidMember.role"></a>

**`role enum Read-only`**

| role | Description |
| --- | --- |
| active | Active member of the array |
| faulty | Faulty member |
| spare | Member kept as spare |
| missing | Missing (removed or dead) member of the array |

<a id="RaidMember.set_name"></a>

**`set_name string Read-only`**

name of the array this member is into

<a id="RaidMember.set_uuid"></a>

**`set_uuid string Read-only`**

uuid of the array this member is into

<a id="RaidMember.dev_uuid"></a>

**`dev_uuid string Read-only`**

uuid of this member

<a id="RaidMember.device_location"></a>

**`device_location enum Read-only`**

internal location of this member. Possible slot values: sata-internal-p0, sata-internal-p1, sata-internal-p2, sata-internal-p4

<a id="RaidMember.total_bytes"></a>

**`total_bytes int Read-only`**

size of this member in bytes

<a id="RaidMember.active_device"></a>

**`active_device int Read-only`**

device number inside the array

<a id="RaidMember.corrected_read_errors"></a>

**`corrected_read_errors int Read-only`**

Device read errors count

<a id="RaidMember.sct_erc_supported"></a>

**`sct_erc_supported bool Read-only`**

Whether SCT_ERC is supported by the device according to its S.M.A.R.T. data.

<a id="RaidMember.sct_erc_enabled"></a>

**`sct_erc_enabled bool Read-only`**

Whether SCT_ERC is enabled on the device according to its S.M.A.R.T. data.

<a id="RaidMember.disk"></a>

**`disk RaidDisk Read-only`**

A few properties of the disk.

<a id="raid-disk-object"></a>

### RAID Disk object

<a id="RaidDisk"></a>

#### Objet RaidDisk

<a id="RaidDisk.model"></a>

**`model string Read-only`**

Disk model.

<a id="RaidDisk.serial"></a>

**`serial string Read-only`**

Disk serial number.

<a id="RaidDisk.firmware"></a>

**`firmware string Read-only`**

Disk firmware revision

<a id="RaidDisk.temp"></a>

**`temp int Read-only`**

Disk temperature in °C.

<a id="raid-api-actions"></a>

## RAID API actions

<a id="get-the-list-of-raid-arrays"></a>

### Get the list of RAID arrays

<a id="get--api-v8-storage-raid-"></a>

**`GET /api/v8/storage/raid/`**

Returns the collection of all [`RaidArray`](raid.md#RaidArray "RaidArray")

**Example request**:

```http
GET /api/v8/storage/raid/ HTTP/1.1
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
            "degraded": false,
            "raid_disks": 4,
            "next_check": 0,
            "sync_action": "idle",
            "level": "raid5",
            "uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895",
            "sysfs_state": "clear",
            "id": 0,
            "sync_completed_pos": 0,
            "members": [
                {
                    "total_bytes": 1000000000000,
                    "active_device": 0,
                    "id": 1000,
                    "corrected_read_errors": 0,
                    "array_id": 0,
                    "disk": {
                        "firmware": "02.01A02",
                        "temp": 43,
                        "serial": "WD-WX91A42F69NE",
                        "model": "WDC WD10JUCX-56WPNY0"
                    },
                    "role": "active",
                    "sct_erc_supported": false,
                    "sct_erc_enabled": false,
                    "dev_uuid": "666793c9-2d04-9d9e-5c8a-2f13eb7f2e9e",
                    "device_location": "sata-internal-p1",
                    "set_name": "Freebox",
                    "set_uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895"
                },
                {
                    "total_bytes": 1000000000000,
                    "active_device": 1,
                    "id": 2000,
                    "corrected_read_errors": 0,
                    "array_id": 0,
                    "disk": {
                        "firmware": "02.01A02",
                        "temp": 47,
                        "serial": "WD-WX91A42F1337",
                        "model": "WDC WD10JUCX-56WPNY0"
                    },
                    "role": "active",
                    "sct_erc_supported": false,
                    "sct_erc_enabled": false,
                    "dev_uuid": "231b35d0-c37f-9d3c-be7a-b7b8485341ce",
                    "device_location": "sata-internal-p0",
                    "set_name": "Freebox",
                    "set_uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895"
                },
                {
                    "total_bytes": 1000000000000,
                    "active_device": 2,
                    "id": 3000,
                    "corrected_read_errors": 0,
                    "array_id": 0,
                    "disk": {
                        "firmware": "02.01A02",
                        "temp": 46,
                        "serial": "WD-WX91A42FZ3I9",
                        "model": "WDC WD10JUCX-56WPNY0"
                    },
                    "role": "active",
                    "sct_erc_supported": false,
                    "sct_erc_enabled": false,
                    "dev_uuid": "d28e5fd8-5e2a-baf3-fd24-6fe5ff2593d6",
                    "device_location": "sata-internal-p2",
                    "set_name": "Freebox",
                    "set_uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895"
                },
                {
                    "total_bytes": 1000000000000,
                    "active_device": 3,
                    "id": 4000,
                    "corrected_read_errors": 0,
                    "array_id": 0,
                    "disk": {
                        "firmware": "02.01A02",
                        "temp": 46,
                        "serial": "WD-WX91A42F1333",
                        "model": "WDC WD10JUCX-56WPNY0"
                    },
                    "role": "active",
                    "sct_erc_supported": false,
                    "sct_erc_enabled": false,
                    "dev_uuid": "fdf5a84a-c427-e1ef-aa12-1732d2cf689f",
                    "device_location": "sata-internal-p3",
                    "set_name": "Freebox",
                    "set_uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895"
                }
            ],
            "array_size": 3000000000000,
            "state": "running",
            "sync_speed": 0,
            "name": "Freebox",
            "check_interval": 0,
            "disk_id": 6000,
            "last_check": 1576082428,
            "sync_completed_end": 0,
            "sync_completed_percent": 0
        }
   ]
}
```

<a id="get-a-given-raid-array-info"></a>

### Get a given RAID array info

<a id="get--api-v8-storage-raid-id"></a>

**`GET /api/v8/storage/raid/{id}`**

Returns a single [`RaidArray`](raid.md#RaidArray "RaidArray")

**Example request**:

```http
GET /api/v8/storage/raid/0 HTTP/1.1
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
        "degraded": false,
        "raid_disks": 4,
        "next_check": 0,
        "sync_action": "idle",
        "level": "raid5",
        "uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895",
        "sysfs_state": "clear",
        "id": 0,
        "sync_completed_pos": 0,
        "members": [
            {
                "total_bytes": 1000000000000,
                "active_device": 0,
                "id": 1000,
                "corrected_read_errors": 0,
                "array_id": 0,
                "disk": {
                    "firmware": "02.01A02",
                    "temp": 43,
                    "serial": "WD-WX91A42F69NE",
                    "model": "WDC WD10JUCX-56WPNY0"
                },
                "role": "active",
                "sct_erc_supported": false,
                "sct_erc_enabled": false,
                "dev_uuid": "666793c9-2d04-9d9e-5c8a-2f13eb7f2e9e",
                "device_location": "sata-internal-p1",
                "set_name": "Freebox",
                "set_uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895"
            },
            {
                "total_bytes": 1000000000000,
                "active_device": 1,
                "id": 2000,
                "corrected_read_errors": 0,
                "array_id": 0,
                "disk": {
                    "firmware": "02.01A02",
                    "temp": 47,
                    "serial": "WD-WX91A42F1337",
                    "model": "WDC WD10JUCX-56WPNY0"
                },
                "role": "active",
                "sct_erc_supported": false,
                "sct_erc_enabled": false,
                "dev_uuid": "231b35d0-c37f-9d3c-be7a-b7b8485341ce",
                "device_location": "sata-internal-p0",
                "set_name": "Freebox",
                "set_uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895"
            },
            {
                "total_bytes": 1000000000000,
                "active_device": 2,
                "id": 3000,
                "corrected_read_errors": 0,
                "array_id": 0,
                "disk": {
                    "firmware": "02.01A02",
                    "temp": 46,
                    "serial": "WD-WX91A42FZ3I9",
                    "model": "WDC WD10JUCX-56WPNY0"
                },
                "role": "active",
                "sct_erc_supported": false,
                "sct_erc_enabled": false,
                "dev_uuid": "d28e5fd8-5e2a-baf3-fd24-6fe5ff2593d6",
                "device_location": "sata-internal-p2",
                "set_name": "Freebox",
                "set_uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895"
            },
            {
                "total_bytes": 1000000000000,
                "active_device": 3,
                "id": 4000,
                "corrected_read_errors": 0,
                "array_id": 0,
                "disk": {
                    "firmware": "02.01A02",
                    "temp": 46,
                    "serial": "WD-WX91A42F1333",
                    "model": "WDC WD10JUCX-56WPNY0"
                },
                "role": "active",
                "sct_erc_supported": false,
                "sct_erc_enabled": false,
                "dev_uuid": "fdf5a84a-c427-e1ef-aa12-1732d2cf689f",
                "device_location": "sata-internal-p3",
                "set_name": "Freebox",
                "set_uuid": "a4f1fbf3-f8e7-453f-19ec-842d6f4e2895"
            }
        ],
        "array_size": 3000000000000,
        "state": "running",
        "sync_speed": 0,
        "name": "Freebox",
        "check_interval": 0,
        "disk_id": 6000,
        "last_check": 1576082428,
        "sync_completed_end": 0,
        "sync_completed_percent": 0
    }
}
```

<a id="create-a-raid-array"></a>

### Create a RAID array

<a id="post--api-v8-storage-raid-"></a>

**`POST /api/v8/storage/raid/`**

Send a [`RaidArray`](raid.md#RaidArray "RaidArray") with the following members:

- level
- name
- members

Each member should have the following property:

- id

<a id="delete-a-raid-array"></a>

### Delete a RAID array

<a id="delete--api-v8-storage-raid-id"></a>

**`DELETE /api/v8/storage/raid/{id}`**

<a id="start-or-stop-a-raid-array"></a>

### Start or stop a RAID array

Send a [`RaidArray`](raid.md#RaidArray "RaidArray") with properties “id” and “state”.

This is used to start and stop an array by changing the state to “stopped” or “running”. These are the only two supported operations. Any change to other fields is ignored.

<a id="put--api-v8-storage-raid-id"></a>

**`PUT /api/v8/storage/raid/{id}`**

<a id="force-start-a-raid-array"></a>

### Force start a RAID array

In case an array is incomplete, but has enough data to start in degraded mode, it won’t start automatically at boot, and the force start can be used. Can only be done if array state is “error”.

<a id="post--api-v8-storage-raid-id-forcestart"></a>

**`POST /api/v8/storage/raid/{id}/forcestart`**

<a id="remove-faulty-members-from-raid-array"></a>

### Remove faulty members from RAID array

In case an array has faulty members, it might be desirable to delete them to add others members instead. Can only be done if array is not running.

<a id="delete--api-v8-storage-raid-id-members-faulty"></a>

**`DELETE /api/v8/storage/raid/{id}/members/faulty`**

<a id="add-members-to-an-existing-array-that-has-missing-members"></a>

### Add members to an existing array that has missing members

In case an array is incomplete (has missing members), either because they were removed physically, or after becoming faulty, it’s possible to add new members to let the reconstruction happen. Can only be done if array is not running.

<a id="put--api-v8-storage-raid-id-members"></a>

**`PUT /api/v8/storage/raid/{id}/members`**

Send a json object containing a “members” property, which is array of [`RaidMember`](raid.md#RaidMember "RaidMember") objects. Only the “id” property of each member is required.

<a id="re-add-out-of-sync-members-that-appear-as-spares"></a>

### Re-add out-of-sync members that appear as spares

In case an array has been force-started without a member, and then said member is physically plugged, it won’t be added automatically and will appear with the “spare” role, this operation must be used. Can only be done if the array has a member with the “spare” role, and is not running.

<a id="post--api-v8-storage-raid-id-members-addspares"></a>

**`POST /api/v8/storage/raid/{id}/members/addspares`**
