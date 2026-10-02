<a id="storage-api-unstable"></a>

# Storage API [UNSTABLE]

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#storage-api-unstable)

## Navigation

- [Storage API Errors](#storage-api-errors)
- [Disk Partition object](#disk-partition-object)
- [Storage Disk object](#storage-disk-object)
- [Storage Disk API](#storage-disk-api)
- [Storage Partition API](#storage-partition-api)
- [Storage Config](#storage-config)
- [Storage config API](#storage-config-api)


This API allows you to manage the Freebox internal disk and disks
connected to the Freebox

This API is unstable, it can be modified without notice in next
releases.

<a id="storage-api-errors"></a>

## Storage API Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| not_found | No disk/partition with this id |
| invalid_disk | No such disk |
| is_a_partition | This is not a disk but a partition |
| is_internal | This action is not permitted on internal disk |
| op_not_supported | Operation not supported |
| op_failed | Operation failed |
| disk_busy | Disk is busy |
| partition_not_found | Partition not found |
| partition_needed | Partition needed |

<a id="disk-partition-object"></a>

## Disk Partition object

Operation progress has the following attributes:

<a id="OperationProgress"></a>

### Objet OperationProgress

<a id="OperationProgress.done_steps"></a>

**`done_steps int Read-only`**

number of steps done

<a id="OperationProgress.max_steps"></a>

**`max_steps int Read-only`**

total number of steps

<a id="OperationProgress.percent"></a>

**`percent int Read-only`**

current step progress

Disk partitions have the following attributes:

<a id="DiskPartition"></a>

### Objet DiskPartition

<a id="DiskPartition.id"></a>

**`id int Read-only`**

unique partition id

<a id="DiskPartition.disk_id"></a>

**`disk_id int Read-only`**

related disk id

<a id="DiskPartition.state"></a>

**`state enum`**

| state | Description |
| --- | --- |
| error | Partition has error |
| checking | Partition check in progress |
| formatting | Partition format in progress |
| mounting | Partition mount in progress |
| maintenance | Partition is in maintenance mode |
| mounted | Partition is ready |
| umounting | Partition umount in progress |
| umounted | Partition is umounted |
| ejecting | Partition ejection in progress |

<a id="DiskPartition.fstype"></a>

**`fstype enum Read-only`**

| fstype |  |
| --- | --- |
| empty |  |
| unknown |  |
| xfs |  |
| ext4 |  |
| vfat |  |
| ntfs |  |
| hf |  |
| hfsplus |  |
| swap |  |
| exfat |  |

<a id="DiskPartition.label"></a>

**`label string`**

partition name

<a id="DiskPartition.path"></a>

**`path string Read-only`**

partition mount point (encoded in base64 as explained in fs API)

<a id="DiskPartition.total_bytes"></a>

**`total_bytes int Read-only`**

partition size (in bytes)

<a id="DiskPartition.used_bytes"></a>

**`used_bytes int Read-only`**

partition used space (in bytes)

<a id="DiskPartition.free_bytes"></a>

**`free_bytes int Read-only`**

partition free space (in bytes)

<a id="DiskPartition.fsck_result"></a>

**`fsck_result enum Read-only`**

fsck result

| state | Description |
| --- | --- |
| no_run_yet | Partition has not been checked yet |
| running | Check is in progress |
| fs_clean | File system is ok |
| fs_corrected | File system was corrected |
| fs_needs_correction | File system need correction |
| failed | File system has unrecoverable error |

<a id="DiskPartition.operation_pct"></a>

**`operation_pct OperationProgress Read-only`**

partition operation progress

<a id="storage-disk-object"></a>

## Storage Disk object

Storage disks have the following attributes:

<a id="StorageDisk"></a>

### Objet StorageDisk

<a id="StorageDisk.id"></a>

**`id int Read-only`**

the disk id

<a id="StorageDisk.type"></a>

**`type enum Read-only`**

| type | Description |
| --- | --- |
| internal | Freebox internal disk |
| usb | usb disk |
| sata | sata disk |
| nvme | nvme disk |

<a id="StorageDisk.state"></a>

**`state enum`**

| state | Description |
| --- | --- |
| error | Disk has error |
| disabled | Disk is disabled |
| enabled | Disk is enabled |
| formatting | Disk is formatting |

<a id="StorageDisk.connector"></a>

**`connector int Read-only`**

Disk physical connector id

<a id="StorageDisk.total_bytes"></a>

**`total_bytes int Read-only`**

Disk size (in bytes)

<a id="StorageDisk.table_type"></a>

**`table_type int Read-only`**

| table_type |  |
| --- | --- |
| msdos |  |
| gpt |  |
| superfloppy |  |
| empty |  |

<a id="StorageDisk.model"></a>

**`model string Read-only`**

Disk model

<a id="StorageDisk.serial"></a>

**`serial string Read-only`**

Disk serial number

<a id="StorageDisk.firmware"></a>

**`firmware string Read-only`**

Disk firmware version

<a id="StorageDisk.temp"></a>

**`temp int Read-only`**

Disk temperature (when supported) in °C

<a id="StorageDisk.operation_pct"></a>

**`operation_pct OperationProgress Read-only`**

partition operation progress

<a id="StorageDisk.partitions"></a>

**`partitions [] array of DiskPartition Read-only`**

list of disk partitions

<a id="StorageDisk.idle"></a>

**`idle bool Read-only`**

is disk idle (when available)

<a id="StorageDisk.idle_duration"></a>

**`idle_duration int Read-only`**

disk idle duration (in seconds) (when available)

<a id="StorageDisk.spinning"></a>

**`spinning bool Read-only`**

is disk spinning (when available)

<a id="StorageDisk.active_duration"></a>

**`active_duration int Read-only`**

disk activity duration (in seconds) (when available)

<a id="StorageDisk.time_before_spindown"></a>

**`time_before_spindown int Read-only`**

seconds left before disk spin down (in seconds) (when available)

<a id="StorageDisk.read_requests"></a>

**`read_requests int Read-only`**

Number of read requests sent since to disk since boot (when available)

<a id="StorageDisk.read_error_requests"></a>

**`read_error_requests int Read-only`**

Number of read requests in error since boot. Might indicate disk failure (when available)

<a id="StorageDisk.write_requests"></a>

**`write_requests int Read-only`**

Number of write requests sent since to disk since boot (when available)

<a id="StorageDisk.write_error_requests"></a>

**`write_error_requests int Read-only`**

Number of write requests in error since boot. Might indicate disk failure (when available)

<a id="storage-disk-api"></a>

## Storage Disk API

<a id="get-the-list-of-disks"></a>

### Get the list of disks

<a id="get--api-v8-storage-disk-"></a>

**`GET /api/v8/storage/disk/`**

Returns the collection of all [`StorageDisk`](storage.md#StorageDisk "StorageDisk")

**Example request**:

```http
GET /api/v8/storage/disk/ HTTP/1.1
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
            "idle_duration": 368,
            "spinning": true,
            "table_type": "msdos",
            "firmware": "PB2ICC0E",
            "type": "internal",
            "idle": true,
            "connector": 0,
            "id": 1,
            "state": "enabled",
            "time_before_spindown": 232,
            "total_bytes": 250059350016,
            "model": "Hitachi HCC545025B9A300",
            "active_duration": 0,
            "temp": 51,
            "serial": "GSCH35VC",
            "partitions": [
                {
                    "fstype": "ext4",
                    "total_bytes": 245091500032,
                    "label": "Disque dur",
                    "id": 3,
                    "fsck_result": "no_run_yet",
                    "state": "mounted",
                    "disk_id": 1,
                    "free_bytes": 68120969216,
                    "used_bytes": 164520534016,
                    "path": "L0Rpc3F1ZSBkdXI="
                }
            ]
        },
        {
            "type": "usb",
            "total_bytes": 125435904,
            "connector": 1,
            "id": 1001,
            "active_duration": 0,
            "partitions": [
                {
                    "fstype": "ext4",
                    "total_bytes": 121418752,
                    "label": "Disque 1",
                    "id": 1002,
                    "fsck_result": "no_run_yet",
                    "state": "mounted",
                    "disk_id": 1001,
                    "free_bytes": 108904448,
                    "used_bytes": 6245376,
                    "path": "L0Rpc3F1ZSAx"
                }
            ],
            "idle_duration": 0,
            "state": "enabled",
            "idle": false,
            "spinning": false,
            "model": "",
            "table_type": "gpt",
            "temp": 0,
            "serial": "",
            "firmware": ""
        }
    ]
}
```

<a id="get-a-given-disk-info"></a>

### Get a given disk info

<a id="get--api-v8-storage-disk-id"></a>

**`GET /api/v8/storage/disk/{id}`**

Returns the [`StorageDisk`](storage.md#StorageDisk "StorageDisk") with the given id

**Example request**:

```http
GET /api/v8/storage/disk/1 HTTP/1.1
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
        "idle_duration": 464,
        "spinning": true,
        "table_type": "msdos",
        "firmware": "PB2ICC0E",
        "type": "internal",
        "idle": true,
        "connector": 0,
        "id": 1,
        "state": "enabled",
        "time_before_spindown": 136,
        "total_bytes": 250059350016,
        "model": "Hitachi HCC545025B9A300",
        "active_duration": 0,
        "temp": 51,
        "serial": "GSCH35VC",
        "partitions": [
            {
                "fstype": "ext4",
                "total_bytes": 245091500032,
                "label": "Disque dur",
                "id": 3,
                "fsck_result": "no_run_yet",
                "state": "mounted",
                "disk_id": 1,
                "free_bytes": 68120969216,
                "used_bytes": 164520534016,
                "path": "L0Rpc3F1ZSBkdXI="
            }
        ]
    }
}
```

<a id="update-a-disk-state"></a>

### Update a disk state

<a id="put--api-v8-storage-disk-id"></a>

**`PUT /api/v8/storage/disk/{id}`**

Enable/Disable a disk

**Example request**:

```http
PUT /api/v8/storage/disk/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "state": "disabled"
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
        "type": "usb",
        "total_bytes": 125435904,
        "connector": 1,
        "id": 1001,
        "active_duration": 0,
        "partitions": [
            {
                "fstype": "ext4",
                "total_bytes": 121418752,
                "label": "Disque 1",
                "id": 1002,
                "fsck_result": "no_run_yet",
                "state": "umounted",
                "disk_id": 1001,
                "free_bytes": 108904448,
                "used_bytes": 6245376,
                "path": "L0Rpc3F1ZSAx"
            }
        ],
        "idle_duration": 0,
        "state": "disabled",
        "idle": false,
        "spinning": false,
        "model": "",
        "table_type": "gpt",
        "temp": 0,
        "serial": "",
        "firmware": ""
    }
}
```

<a id="get-fs-advices"></a>

### Get FS advices

<a id="get--api-v8-storage-disk-disk_id-fsadvice?partition_id=partition_id&amp;dedicated_disk=bool"></a>

**`GET /api/v8/storage/disk/{disk_id}/fsadvice?partition_id={partition_id}&dedicated_disk={bool}`**

Check disk FS and get formatting advices.

To be able to get FS advice for a disk you need to provide the
disk_id. Specify dedicated_disk for a disk that will only be
used with the Freebox server (no need to specify it for a SATA
internal disk). If the disk is empty do not specify partition_id
in order to get advice for creating a new one. If the disk
contains a partition specify the partition_id that needs to be
checked.

**Example request**:

```http
GET /api/v8/storage/disk/1000/fsadvice?partition_id=1003&dedicated_disk=false HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true
    "result":
    {
        "fstype": "exfat",
        "table_type": "gpt",
        "reason": "max_file_size",
        "partitions_to_delete": [
        {
            "fstype": "exfat",
            "total_bytes": 1000000000000,
            "label": "EFI",
            "id": 1001,
            "internal": false,
            "fsck_result": "no_run_yet",
            "state": "mounted",
            "disk_id": 1000,
            "free_bytes": 1000000000000,
            "used_bytes": 1310000,
            "path": "L0Rpc3F1ZSAxIDE="
        },
        {
            "fstype": "exfat",
            "total_bytes": 1000000000000,
            "label": "DATA",
            "id": 1002,
            "internal": false,
            "fsck_result": "no_run_yet",
            "state": "mounted",
            "disk_id": 1000,
            "free_bytes": 1000000000000,
            "used_bytes": 1310000,
            "path": "L1ZvbHVtZSAxMDAwR28="
        },
        ]
    },
}
```

Reasons can be one of the following:

| Reason | Description |
| --- | --- |
| max_file_size | Performance and bigger that 4GB files support |
| perf_and_compat | Performance and device compatibility |
| sata_performance | Performance for SATA disk |
| nvme_performance | Performance for NVMe disk |
| no_partition | Missing partition id on already formatted disk |
| partition_error | Partition is in error state |

<a id="format-a-disk"></a>

### Format a disk

<a id="put--api-v8-storage-disk-id-format-"></a>

**`PUT /api/v8/storage/disk/{id}/format/`**

Format the disk with the given id

To be able to format a disk you need to provide the following
parameters (JSON encoded). There will be one partition using all
the available space on disk. All previous data will be lost.

This parameters will be ignored if you format the Freebox internal
disk

Parameters

- **table_type** (*string*) – The partition table format
- **fs_type** (*string*) – The partition type
- **label** (*string*) – The partition label

NOTE: once started you can monitor the format process getting the
disk information (see [`StorageDisk`](storage.md#StorageDisk "StorageDisk") operation_pct
field)

**Example request**:

```http
PUT /api/v8/storage/disk/1001/format HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "label": "freebox",
   "fs_type": "vfat",
   "table_type": "msdos"
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

<a id="storage-partition-api"></a>

## Storage Partition API

<a id="get-the-list-of-partitions"></a>

### Get the list of partitions

<a id="get--api-v8-storage-partition-"></a>

**`GET /api/v8/storage/partition/`**

Returns the collection of all [`DiskPartition`](storage.md#DiskPartition "DiskPartition")

**Example request**:

```http
GET /api/v8/storage/partition/ HTTP/1.1
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
            "fstype": "ext4",
            "total_bytes": 245091500032,
            "label": "Disque dur",
            "id": 3,
            "fsck_result": "no_run_yet",
            "state": "umounted",
            "disk_id": 1,
            "free_bytes": 68120969216,
            "used_bytes": 164520534016,
            "path": "L0Rpc3F1ZSBkdXI="
        },
        {
            "fstype": "vfat",
            "total_bytes": 123485184,
            "label": "freebox",
            "id": 1002,
            "fsck_result": "no_run_yet",
            "state": "mounted",
            "disk_id": 1001,
            "free_bytes": 123484672,
            "used_bytes": 512,
            "path": "L2ZyZWVib3g="
        }
    ]
}
```

<a id="get-a-given-partition-info"></a>

### Get a given partition info

<a id="get--api-v8-storage-partition-id"></a>

**`GET /api/v8/storage/partition/{id}`**

Returns the [`DiskPartition`](storage.md#DiskPartition "DiskPartition") with the given id

**Example request**:

```http
GET /api/v8/storage/partition/1002 HTTP/1.1
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
        "fstype": "vfat",
        "total_bytes": 123485184,
        "label": "freebox",
        "id": 1002,
        "fsck_result": "no_run_yet",
        "state": "mounted",
        "disk_id": 1001,
        "free_bytes": 123484672,
        "used_bytes": 512,
        "path": "L2ZyZWVib3g="
    }
}
```

<a id="update-a-partition-state"></a>

### Update a partition state

<a id="put--api-v8-storage-partition-id"></a>

**`PUT /api/v8/storage/partition/{id}`**

Enable/Disable a partition

**Example request**:

```http
PUT /api/v8/storage/partition/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "state" : "umounted"
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
        "fstype": "vfat",
        "total_bytes": 123485184,
        "label": "freebox",
        "id": 1002,
        "fsck_result": "no_run_yet",
        "state": "umounted",
        "disk_id": 1001,
        "free_bytes": 123484672,
        "used_bytes": 512,
        "path": "L2ZyZWVib3g="
    }
}
```

<a id="check-a-partition"></a>

### Check a partition

<a id="put--api-v8-storage-partition-id-check-"></a>

**`PUT /api/v8/storage/partition/{id}/check/`**

Checks the partition with the given id

To be able to check a partition you need to provide the following
parameters (JSON encoded):

Parameters

- **checkmode** (*enum*) – ‘ro’ for read only check, ‘rw’ to attempt to
  repair errors

NOTE: once started you can monitor the fsck process getting the
partition information (see [`DiskPartition`](storage.md#DiskPartition "DiskPartition")
operation_pct field)

**Example request**:

```http
PUT /api/v8/storage/partition/1002/check HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "checkmode": "ro"
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

<a id="storage-config"></a>

## Storage Config

StorageConfig has the following attributes:

<a id="StorageConfig"></a>

### Objet StorageConfig

<a id="StorageConfig.external_pm_enabled"></a>

**`external_pm_enabled bool`**

enable/disable external disk power management

<a id="StorageConfig.external_pm_idle_before_spindown"></a>

**`external_pm_idle_before_spindown int`**

idle time in minutes to wait before spinning down an external disk

<a id="storage-config-api"></a>

## Storage config API

<a id="get-the-current-storage-configuration"></a>

### Get the current storage configuration

<a id="get--api-v8-storage-config-"></a>

**`GET /api/v8/storage/config/`**

Get the [`StorageConfig`](storage.md#StorageConfig "StorageConfig")

**Example request**:

```http
GET /api/v8/storage/config/ HTTP/1.1
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
     "external_pm_idle_before_spindown": 10,
     "external_pm_enabled": true
  }
}
```

<a id="update-the-external-storage-configuration"></a>

### Update the External Storage configuration

<a id="put--api-v8-storage-config-"></a>

**`PUT /api/v8/storage/config/`**

Update the [`StorageConfig`](storage.md#StorageConfig "StorageConfig")

**Example request**:

```http
PUT /api/v8/storage/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "external_pm_enabled": false
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
        "external_pm_idle_before_spindown": 10,
        "external_pm_enabled": false
    }
}
```
