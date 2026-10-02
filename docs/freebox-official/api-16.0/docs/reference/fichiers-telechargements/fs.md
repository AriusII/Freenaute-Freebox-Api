<a id="file-system-558"></a>

# File System

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#file-system-558)

## Navigation

- [Path encoding](#path-encoding)
- [File System Errors](#file-system-errors)
- [Task](#task)
- [Listing](#listing)
- [Operations](#operations)


With the file system API you can access files on Freebox
internal disk and disks connected to the Freebox.

<a id="path-encoding"></a>

## Path encoding

<a id="id1"></a>

`NOTE:`

For maximum compatibility issues path are encoded in base64, you
*should* use the path as it is returned by the ls API call.

For instance this will solve problems with [unicode equivalence](http://en.wikipedia.org/wiki/Unicode_equivalence) .

Although “Spécial” (0x53 0x70 **0xc3 0xa9** 0x63 0x69 0x61 0x6c) and
“Spécial” (0x53 0x70 **0x65 0xcc 0x81** 0x63 0x69 0x61 0x6c) are utf8
equivalent, it represents two different paths.

Some software/libraries will replace the original string with its
normalized form, causing issues. The use of base64 encoded path will
ensure the original path will be preserved.

<a id="file-system-errors"></a>

## File System Errors

When attempting to access the file system API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| invalid_id | Invalid object id |
| path_not_found | File or folder not found |
| internal_error | Internal error |
| disk_unavailable | The disk is not mounted |
| invalid_request | Invalid request |
| invalid_conflict_mode | The conflict mode specified is invalid (see below) |
| exec_failed | Internal error |
| out_of_memory | Out of memory |
| task_not_found | Invalid task id |
| invalid_state | You tried to set an invalid state |
| invalid_task_type | This operation cannot be performed on this task |
| destination_conflict | The destination file/folder already exists |
| access_denied | Access to this file is denied |
| disk_full | The destination disk is full |

<a id="task"></a>

## Task

File system tasks have the following attributes:

<a id="FsTask"></a>

### Objet FsTask

<a id="FsTask.id"></a>

**`id int Read-only`**

id

<a id="FsTask.type"></a>

**`type enum Read-only`**

The valid task types are:

| Type | Description |
| --- | --- |
| cat | Concatenate multiple files |
| cp | Copy files |
| mv | Move files |
| rm | Remove files |
| archive | Creates an archive |
| extract | Extract an archive |
| repair | Check and repair files |

<a id="FsTask.state"></a>

**`state enum`**

| State | Description |
| --- | --- |
| queued | Queued (only one task is active at a given time) |
| running | Running |
| paused | Paused (user suspended) |
| done | Done |
| failed | Failed (see error) |

<a id="FsTask.error"></a>

**`error enum Read-only`**

| Error | Description |
| --- | --- |
| none | No error |
| archive_read_failed | Error reading archive |
| archive_open_failed | Error opening archive |
| archive_write_failed | Error writing archive |
| chdir_failed | Error changing directory |
| dest_is_not_dir | The destination is not a directory |
| file_exists | File already exists |
| file_not_found | File not found |
| mkdir_failed | Unable to create directory |
| open_input_failed | Error opening input file |
| open_output_failed | Error opening output file |
| opendir_failed | Error opening directory |
| overwrite_failed | Error overwriting file |
| path_too_big | Path is too long |
| repair_failed | Failed to repair corrupted files |
| rmdir_failed | Error removing directory |
| same_file | Source and Destination are the same file |
| unlink_failed | Error removing file |
| unsupported_file_type | This file type is not supported |
| write_failed | Error writing file |
| disk_full | Disk is full |
| internal | Internal error |
| invalid_format | Invalid file format (corrupted ?) |
| incorrect_password | Invalid or missing password for extraction |
| permission_denied | Permission denied |
| readlink_failed | Failed to read the target of a symbolic link |
| symlink_failed | Failed to create a symbolic link |
| copy_into_itself | Attempted to copy a directory to a subdirectory of itself |
| truncate_failed | Failed to truncate file |

<a id="FsTask.created_ts"></a>

**`created_ts timestamp Read-only`**

task creation timestamp

<a id="FsTask.started_ts"></a>

**`started_ts timestamp Read-only`**

task start timestamp

<a id="FsTask.done_ts"></a>

**`done_ts timestamp Read-only`**

task end timestamp

<a id="FsTask.duration"></a>

**`duration int Read-only`**

task duration in seconds

<a id="FsTask.progress"></a>

**`progress int Read-only`**

task progress in percent (scaled by 100)

<a id="FsTask.eta"></a>

**`eta int Read-only`**

estimated time remaining before the task completion (in seconds)

<a id="FsTask.from"></a>

**`from string Read-only`**

current source file (if available)

<a id="FsTask.to"></a>

**`to string Read-only`**

current destination file (if available)

<a id="FsTask.nfiles"></a>

**`nfiles int Read-only`**

number of files to process

<a id="FsTask.nfiles_done"></a>

**`nfiles_done int Read-only`**

number of files processed

<a id="FsTask.total_bytes"></a>

**`total_bytes int Read-only`**

total bytes to process

<a id="FsTask.total_bytes_done"></a>

**`total_bytes_done int Read-only`**

number of bytes processed

<a id="FsTask.curr_bytes"></a>

**`curr_bytes int Read-only`**

size of the file currently processed

<a id="FsTask.curr_bytes_done"></a>

**`curr_bytes_done int Read-only`**

number of bytes processed for the current file

<a id="FsTask.rate"></a>

**`rate int Read-only`**

processing rate in byte/s

<a id="FsTask.src"></a>

**`src [] array of string Read-only`**

task source files

<a id="FsTask.dst"></a>

**`dst string Read-only`**

task destination path

<a id="list-every-tasks"></a>

### List every tasks

<a id="get--api-v15-fs-tasks-"></a>

**`GET /api/v15/fs/tasks/`**

Returns the collection of all [`FsTask`](fs.md#FsTask "FsTask") tasks

**Example request**:

```http
GET /api/v15/fs/tasks/ HTTP/1.1
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
         curr_bytes_done: 0,
         total_bytes: 0,
         nfiles_done: 0,
         started_ts: 1355834253,
         duration: 3,
         done_ts: 0,
         curr_bytes: 0,
         type: "extract",
         to: "oxygennosvg/128x128/mimetypes/application_x_nzb.png",
         id: 12,
         nfiles: 0,
         created_ts: 1355834253,
         state: "paused",
         total_bytes_done: 0,
         from: "/Disque dur/tests/oxygennosvg.tar.gz",
         rate: 0,
         eta: 0,
         error: "none",
         progress: 0
         src: [
           "/Disque dur/tests/oxygennosvg.tar.gz"
         ],
         dst: "/Disque dur/tests/oxygennosvg"
      },
      {
         id: 11,
         curr_bytes_done: 0,
         total_bytes: 0,
         nfiles_done: 0,
         started_ts: 1355834187,
         duration: 0,
         done_ts: 1355834187,
         curr_bytes: 0,
         type: "rm",
         to: "",
         nfiles: 0,
         created_ts: 1355834187,
         state: "done",
         total_bytes_done: 0,
         from: "/Disque dur/test/testiso.1.iso",
         rate: 0,
         eta: 0,
         error: "none",
         progress: 100,
         src: [
           "/Disque dur/test/testiso.1.iso"
         ]
      }
   ]
}
```

<a id="list-a-task"></a>

### List a task

<a id="get--api-v15-fs-tasks-id"></a>

**`GET /api/v15/fs/tasks/{id}`**

Returns the [`FsTask`](fs.md#FsTask "FsTask") task with the given id

**Example request**:

```http
GET /api/v15/fs/tasks/12 HTTP/1.1
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
      curr_bytes_done: 0,
      total_bytes: 0,
      nfiles_done: 0,
      started_ts: 1355834253,
      duration: 268,
      done_ts: 0,
      curr_bytes: 0,
      type: "extract",
      to: "oxygennosvg/16x16/actions/format_stroke_color.png",
      id: 12,
      nfiles: 0,
      created_ts: 1355834253,
      state: "running",
      total_bytes_done: 0,
      from: "/Disque dur/tests/oxygennosvg.tar.gz",
      rate: 0,
      eta: 0,
      error: "none",
      progress: 0,
      src: [
        "/Disque dur/tests/oxygennosvg.tar.gz"
      ],
      dst: "/Disque dur/tests/oxygennosvg"
   }
}
```

<a id="delete-a-task"></a>

### Delete a task

<a id="delete--api-v15-fs-tasks-id"></a>

**`DELETE /api/v15/fs/tasks/{id}`**

Deletes the [`FsTask`](fs.md#FsTask "FsTask") task with the given id, if the
task was running, stop it.

No rollback is done, if a file as already been processed it will be
left as is.

**Example request**:

```http
DELETE /api/v15/fs/tasks/12 HTTP/1.1
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

<a id="update-a-task"></a>

### Update a task

<a id="put--api-v15-fs-tasks-id"></a>

**`PUT /api/v15/fs/tasks/{id}`**

Updates the [`FsTask`](fs.md#FsTask "FsTask") task with the given id

**Example request**:

```http
PUT /api/v15/fs/tasks/15 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "state": "paused"
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
        "curr_bytes_done": 0,
        "total_bytes": 2410125312,
        "nfiles_done": 0,
        "started_ts": 1355835094,
        "duration": 27,
        "done_ts": 0,
        "curr_bytes": 0,
        "type": "cp",
        "to": "/Disque dur/old_hdd/testiso.1.iso",
        "id": 15,
        "nfiles": 1,
        "created_ts": 1355835094,
        "state": "paused",
        "total_bytes_done": 595591168,
        "from": "/Disque dur/old_hdd/testiso.iso",
        "rate": 0,
        "eta": 85,
        "error": "none",
        "progress": 24,
        "src": [
          "/Disque dur/old_hdd/testiso.iso"
        ],
        "dst": "/Disque dur/old_hdd"
    }
}
```

<a id="listing"></a>

## Listing

<a id="file-info"></a>

### File info

<a id="FileInfo"></a>

#### Objet FileInfo

<a id="FileInfo.path"></a>

**`path string Read-only`**

file path (encoded in base64 as explained in [Path Encoding](../fondamentaux/00_index.md#id1))

<a id="FileInfo.name"></a>

**`name string Read-only`**

file name (in clear text)

<a id="FileInfo.mimetype"></a>

**`mimetype string Read-only`**

file mimetype

<a id="FileInfo.type"></a>

**`type enum`**

| Type | Description |
| --- | --- |
| dir | Directory |
| file | Regular file |

<a id="FileInfo.size"></a>

**`size int Read-only`**

file size in bytes

<a id="FileInfo.modification"></a>

**`modification int Read-only`**

file modification timestamp

<a id="FileInfo.index"></a>

**`index int Read-only`**

display order for natural sort

<a id="FileInfo.link"></a>

**`link boolean Read-only`**

is this file a link

<a id="FileInfo.target"></a>

**`target string Read-only`**

symlink target path (encoded in base64 as explained in [Path Encoding](../fondamentaux/00_index.md#id1))
(only present when link is set to true)

<a id="FileInfo.hidden"></a>

**`hidden boolean Read-only`**

should the file be hidden to user

<a id="FileInfo.foldercount"></a>

**`foldercount int Read-only`**

number of subfolders

only relevant for dir, only provided if “countSubFolder”
parameter is set

<a id="FileInfo.filecount"></a>

**`filecount int Read-only`**

number of files inside directory

only relevant for dir, only provided if “countSubFolder”
parameter is set

<a id="FileInfo.exif"></a>

**`exif object Read-only`**

EXIF metadada if available.

only relevant for supported image files (JPEG, HEIC), when the “exifMode” parameter is set

<a id="list-files"></a>

### List files

<a id="get--api-v15-fs-ls-path"></a>

**`GET /api/v15/fs/ls/{path}`**

Returns the list of `FileInfos` for the given path

Parameters

- **onlyFolder** (*bool*) – Only list folders
- **countSubFolder** (*bool*) – Return files and subfolder count for folders
- **removeHidden** (*bool*) – Don’t return hidden files in directory listing
- **exifMode** (*string*) – Return EXIF metadata for supported image files (JPEG, HEIC).
  Value can be “light” (basic metadata), “full” (all metadata) or “base64” (all metadata encoded in base64)
- **limit** (*integer*) – Maximum number of entries in response [optional]
- **cursor** (*string*) – Opaque value to include in next request to continue path listing [optional]

**Example request**:

```http
GET /api/v15/fs/ls/L0Rpc3F1ZSBkdXI=&limit=100 HTTP/1.1
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
      "entries": [
        {
            "path": "L0Rpc3F1ZSBkdXIvRW5yZWdpc3RyZW1lbnRz",
            "filecount": 0,
            "link": false,
            "modification": 1362005535,
            "foldercount": 0,
            "name": "Enregistrements",
            "index": 1,
            "mimetype": "inode/directory",
            "hidden": false,
            "type": "dir",
            "size": 4096
        },

        /* Note: for the two following folders path are different, but name is utf8 equivalent */

        {
            "path": "L0Rpc3F1ZSBkdXIvTGUgU3DDqWNpYWwgMg==",
            "filecount": 0,
            "link": false,
            "modification": 1362492511,
            "foldercount": 0,
            "name": "Le Spécial 2",
            "index": 3,
            "mimetype": "inode/directory",
            "hidden": false,
            "type": "dir",
            "size": 4096
        },
        {
            "path": "L0Rpc3F1ZSBkdXIvTGUgU3BlzIFjaWFsIDI=",
            "filecount": 4,
            "link": false,
            "modification": 1361995307,
            "foldercount": 1,
            "name": "Le Spécial 2",
            "index": 4,
            "mimetype": "inode/directory",
            "hidden": false,
            "type": "dir",
            "size": 4096
        },

         [ ... ]

        {
            "path": "L0Rpc3F1ZSBkdXIvVmlkw6lvcw==",
            "filecount": 8,
            "link": false,
            "modification": 1361887598,
            "foldercount": 2,
            "name": "Vidéos",
            "index": 16,
            "mimetype": "inode/directory",
            "hidden": false,
            "type": "dir",
            "size": 4096
        }
    ],
    "cursor": "eyJvZmZzZXQiOjIwMTMwMzk5MTQ2NzU5MzM4OTR9"
  }

}
```

<a id="get-file-information"></a>

### Get file information

<a id="get--api-v15-fs-info-path"></a>

**`GET /api/v15/fs/info/{path}`**

Returns the `FileInfos` for the given path

**Example request**:

```http
GET /api/v15/fs/info/L0Rpc3F1ZSBkdXIvdG90bw== HTTP/1.1
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
        "type": "dir",
        "link": true,
        "parent": "L0Rpc3F1ZSBkdXI=",
        "modification": 1370354349,
        "hidden": false,
        "mimetype": "inode/directory",
        "name": "toto",
        "target": "L0Rpc3F1ZSBkdXIvUGhvdG9z",
        "path": "L0Rpc3F1ZSBkdXIvdG90bw==",
        "size": 4096
    }
}
```

<a id="batch-file-information"></a>

### Batch file information

<a id="post--api-v15-fs-info"></a>

**`POST /api/v15/fs/info`**

Returns a `FileInfos` list for a given path list. Invalid paths are ignored.

**Example request**:

```http
POST /api/v15/fs/info HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
[ "L0Rpc3F1ZSBkdXIvRW5yZWdpc3RyZW1lbnRz", "L0Rpc3F1ZSBkdXIvTGUgU3DDqWNpYWwgMg==", "L0Rpc3F1ZSBkdXIvVmlkw6lvcw==" ]
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
              "path": "L0Rpc3F1ZSBkdXIvRW5yZWdpc3RyZW1lbnRz",
              "filecount": 0,
              "link": false,
              "modification": 1362005535,
              "foldercount": 0,
              "name": "Enregistrements",
              "index": 1,
              "mimetype": "inode/directory",
              "hidden": false,
              "type": "dir",
              "size": 4096
          },
          {
              "path": "L0Rpc3F1ZSBkdXIvTGUgU3DDqWNpYWwgMg==",
              "filecount": 0,
              "link": false,
              "modification": 1362492511,
              "foldercount": 0,
              "name": "Le Spécial 2",
              "index": 3,
              "mimetype": "inode/directory",
              "hidden": false,
              "type": "dir",
              "size": 4096
          },
          {
              "path": "L0Rpc3F1ZSBkdXIvVmlkw6lvcw==",
              "filecount": 8,
              "link": false,
              "modification": 1361887598,
              "foldercount": 2,
              "name": "Vidéos",
              "index": 16,
              "mimetype": "inode/directory",
              "hidden": false,
              "type": "dir",
              "size": 4096
          }
    ]
}
```

<a id="operations"></a>

## Operations

Each time you want to perform a modification on the file system you
will have to create a new [`FsTask`](fs.md#FsTask "FsTask") that you will be
able to monitor.

NOTE: The requested operation may be en-queued to avoid performance
drop because of excessive disk io

<a id="conflict-resolution"></a>

### Conflict resolution

For certain file operations where a file name conflict can happen,
you must specify a conflict resolution mode.

Valid resolution modes are:

| Conflict mode | Description |
| --- | --- |
| overwrite | Overwrite the destination file |
| both | Keep both files (rename the file adding a suffix) |
| recent | Only overwrite if newer than destination file |
| skip | Keep the destination file |

<a id="move-files"></a>

### Move files

<a id="post--api-v15-fs-mv-"></a>

**`POST /api/v15/fs/mv/`**

Parameters

- **files** (*string[]*) – The list of files to move
- **dst** (*string*) – The destination
- **mode** (*enum*) – The conflict resolution mode

**Example request for moving files**:

```http
POST /api/v15/fs/mv/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "files":
      [
         "L0Rpc3F1ZSBkdXIvUGhvdG9zL0RTQ18zNDkxLmpwZw==", /* /Disque dur/Photos/DSC_3491.jpg */
         "L0Rpc3F1ZSBkdXIvUGhvdG9zL0RTQ18zNTAwLmpwZw==" /* /Disque dur/Photos/DSC_3500.jpg */
      ],
    "dst": "L0Rpc3F1ZSBkdXIvUGhvdG9zL0xhdW5jaHBhZA==", /* /Disque dur/Photos/Launchpad */
    "mode": "overwrite"
}
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "curr_bytes_done": 0,
        "total_bytes": 0,
        "nfiles_done": 0,
        "started_ts": 1355840585,
        "duration": 0,
        "done_ts": 0,
        "curr_bytes": 0,
        "type": "mv",
        "to": "",
        "id": 39,
        "nfiles": 0,
        "created_ts": 1355840585,
        "state": "running",
        "total_bytes_done": 0,
        "from": "",
        "rate": 0,
        "eta": 0,
        "error": "none",
        "progress": 0,
        "src": [
          "/Disque dur/Photos/DSC_3491.jpg",
          "/Disque dur/Photos/DSC_3500.jpg"
        ],
        "dst": "/Disque dur/Photos/Launchpad"
    }
}
```

<a id="copy-files"></a>

### Copy files

<a id="post--api-v15-fs-cp-"></a>

**`POST /api/v15/fs/cp/`**

Parameters

- **files** (*string[]*) – The list of files to copy
- **dst** (*string*) – The destination
- **mode** (*enum*) – The conflict resolution mode

**Example request**:

```http
POST /api/v15/fs/cp/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "files":
      [
         "L0Rpc3F1ZSBkdXIvUGhvdG9zL0xhdW5jaHBhZC9EU0NfMzQ5MS5qcGcK", /* /Disque dur/Photos/Launchpad/DSC_3491.jpg */
         "L0Rpc3F1ZSBkdXIvUGhvdG9zL0xhdW5jaHBhZC9EU0NfMzUwMC5qcGcK", /* /Disque dur/Photos/Launchpad/DSC_3500.jpg */
      ],
    "dst": "L0Rpc3F1ZSBkdXIvUGhvdG9zL1JvY2tldHMK", /* /Disque dur/Photos/Rockets */
    "mode": "both"
 }
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "curr_bytes_done": 0,
        "total_bytes": 0,
        "nfiles_done": 0,
        "started_ts": 1355840943,
        "duration": 0,
        "done_ts": 0,
        "curr_bytes": 0,
        "type": "cp",
        "to": "",
        "id": 43,
        "nfiles": 0,
        "created_ts": 1355840943,
        "state": "running",
        "total_bytes_done": 0,
        "from": "",
        "rate": 0,
        "eta": 0,
        "error": "none",
        "progress": 0,
        "src": [
          "/Disque dur/Photos/Launchpad/DSC_3491.jpg",
          "/Disque dur/Photos/Launchpad/DSC_3500.jpg"
        ],
        "dst": "/Disque dur/Photos/Rockets"
    }
}
```

<a id="remove-files"></a>

### Remove files

<a id="post--api-v15-fs-rm-"></a>

**`POST /api/v15/fs/rm/`**

Parameters

- **files** (*string[]*) – The list of files to remove

**Example request**:

```http
POST /api/v15/fs/rm/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "files":
      [
         "L0Rpc3F1ZSBkdXIvUGhvdG9zL1JvY2tldHMvRFNDXzM0OTEuanBnCg==", /* /Disque dur/Photos/Rockets/DSC_3491.jpg */
         "L0Rpc3F1ZSBkdXIvUGhvdG9zL1JvY2tldHMvRFNDXzM1MDAuanBnCg==" /* /Disque dur/Photos/Rockets/DSC_3500.jpg */
      ]
 }
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "curr_bytes_done": 0,
        "total_bytes": 0,
        "nfiles_done": 0,
        "started_ts": 1355841064,
        "duration": 0,
        "done_ts": 0,
        "curr_bytes": 0,
        "type": "rm",
        "to": "",
        "id": 45,
        "nfiles": 0,
        "created_ts": 1355841064,
        "state": "running",
        "total_bytes_done": 0,
        "from": "/Disque dur/Photos/Rockets/DSC_3491.jpg",
        "rate": 0,
        "eta": 0,
        "error": "none",
        "progress": 0,
        "src": [
          "/Disque dur/Photos/Rockets/DSC_3491.jpg",
          "/Disque dur/Photos/Rockets/DSC_3500.jpg"
        ]
    }
}
```

<a id="cat-files"></a>

### Cat files

<a id="post--api-v15-fs-cat-"></a>

**`POST /api/v15/fs/cat/`**

Parameters

- **files** (*string[]*) – The list of files to concatenate
- **dst** (*string*) – The destination
- **multi_volumes** (*bool*) – Enable multi-volumes mode, it will start at XXX001 and concatenate XXX002, XXX003, …
- **delete_files** (*bool*) – Deletes source files
- **overwrite** (*bool*) – Overwrites the destination
- **append** (*bool*) – Append to the destination

**Example request**:

```http
POST /api/v15/fs/cat/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "files":
      [
         "L0Rpc3F1ZSBkdXIvZmlsZTE=", /* /Disque dur/file1 */
         "L0Rpc3F1ZSBkdXIvZmlsZTI="  /* /Disque dur/file2 */
      ],
    "dst": "L0Rpc3F1ZSBkdXIvZmlsZTEy", /* /Disque dur/file12 */
    "multi_volumes": false,
    "delete_files": false,
    "append": true,
    "overwrite": false
}
```

Or if you want to do a multi-volumes concatenation:

```json
{
   "files":
      [
         // You don't need to specify file002, file003, ...
         // They'll be found by cat.
         "L0Rpc3F1ZSBkdXIvZmlsZTAwMQ==", /* /Disque dur/file001 */
      ],
    "dst": "L0Rpc3F1ZSBkdXIvZmlsZQ==", /* /Disque dur/file */
    "multi_volumes": true,
    "delete_files": true,
    "append": false,
    "overwrite": true
}
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "curr_bytes_done": 0,
        "total_bytes": 0,
        "nfiles_done": 0,
        "started_ts": 1355840943,
        "duration": 0,
        "done_ts": 0,
        "curr_bytes": 0,
        "type": "cat",
        "to": "",
        "id": 43,
        "nfiles": 0,
        "created_ts": 1355840943,
        "state": "running",
        "total_bytes_done": 0,
        "from": "",
        "rate": 0,
        "eta": 0,
        "error": "none",
        "progress": 0
    }
}
```

<a id="create-an-archive"></a>

### Create an archive

<a id="post--api-v15-fs-archive-"></a>

**`POST /api/v15/fs/archive/`**

Parameters

- **files** (*string[]*) – The list of files to archive
- **dst** (*string*) – The destination

**Example request**:

```http
POST /api/v15/fs/archive/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "files":
      [
         "L0Rpc3F1ZSBkdXIvUGhvdG9zL0xhdW5jaHBhZC9EU0NfMzQ5MS5qcGc=", /* /Disque dur/Photos/Launchpad/DSC_3491.jpg */
         "L0Rpc3F1ZSBkdXIvUGhvdG9zL0xhdW5jaHBhZC9EU0NfMzUwMC5qcGc="  /* /Disque dur/Photos/Launchpad/DSC_3500.jpg */
      ],
    "dst": "L0Rpc3F1ZSBkdXIvUGhvdG9zL3JvY2tldHMuemlw" /* /Disque dur/Photos/rockets.zip */
 }
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "curr_bytes_done": 0,
        "total_bytes": 0,
        "nfiles_done": 0,
        "started_ts": 1355840943,
        "duration": 0,
        "done_ts": 0,
        "curr_bytes": 0,
        "type": "archive",
        "to": "",
        "id": 42,
        "nfiles": 0,
        "created_ts": 1355840943,
        "state": "running",
        "total_bytes_done": 0,
        "from": "",
        "rate": 0,
        "eta": 0,
        "error": "none",
        "progress": 0,
        "src": [
          "/Disque dur/Photos/Launchpad/DSC_3491.jpg",
          "/Disque dur/Photos/Launchpad/DSC_3500.jpg"
        ],
        "dst": "/Disque dur/Photos/rockets.zip"
    }
}
```

<a id="extract-a-file"></a>

### Extract a file

<a id="post--api-v15-fs-extract-"></a>

**`POST /api/v15/fs/extract/`**

Parameters

- **src** (*string*) – The archive file
- **dst** (*string*) – The destination folder
- **password** (*string*) – The archive password
- **delete_archive** (*boolean*) – Delete archive after extraction
- **overwrite** (*boolean*) – Overwrite files on conflict

**Example request**:

```http
POST /api/v15/fs/extract/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "src": "L0Rpc3F1ZSBkdXIvb2xkX2hkZC90ZXN0aXNvLjEuaXNv", /* /Disque dur/old_hdd/testiso.1.iso */
   "dst": "L0Rpc3F1ZSBkdXIvb2xkX2hkZA==" /* /Disque dur/old_hdd */
   "password": "",
   "delete_archive": false,
   "overwrite": true
}
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "curr_bytes_done": 0,
        "total_bytes": 0,
        "nfiles_done": 0,
        "started_ts": 1355842252,
        "duration": 0,
        "done_ts": 0,
        "curr_bytes": 0,
        "type": "extract",
        "to": "/Disque dur/old_hdd",
        "id": 48,
        "nfiles": 0,
        "created_ts": 1355842252,
        "state": "running",
        "total_bytes_done": 0,
        "from": "/Disque dur/old_hdd/testiso.1.iso",
        "rate": 0,
        "eta": 0,
        "error": "none",
        "progress": 0,
        "src": [
          "/Disque dur/old_hdd/testiso.1.iso"
        ],
        "dst": "/Disque dur/old_hdd"
    }
}
```

<a id="repair-a-file"></a>

### Repair a file

<a id="post--api-v15-fs-repair-"></a>

**`POST /api/v15/fs/repair/`**

Parameters

- **src** (*string*) – The .par2 file
- **delete_archive** (*boolean*) – Delete par2 files after repair

**Example request**:

```http
POST /api/v15/fs/repair/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "src": "L0Rpc3F1ZSBkdXIvdGVzdHMvcGFyMi9saWNlbnNlLnR4dC5wYXIy", /* /Disque dur/tests/par2/license.txt.par2 */
   "delete_archive": false
}
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "curr_bytes_done": 0,
        "total_bytes": 0,
        "nfiles_done": 0,
        "started_ts": 1355842559,
        "duration": 0,
        "done_ts": 0,
        "curr_bytes": 0,
        "type": "repair",
        "to": "",
        "id": 50,
        "nfiles": 0,
        "created_ts": 1355842559,
        "state": "running",
        "total_bytes_done": 0,
        "from": "",
        "rate": 0,
        "eta": 0,
        "error": "none",
        "progress": 0
    }
}
```

<a id="hash-a-file"></a>

### Hash a file

<a id="post--api-v15-fs-hash-"></a>

**`POST /api/v15/fs/hash/`**

Parameters

- **src** (*string*) – The file to hash
- **hash_type** (*string*) – The type of hash (md5, sha1, …)

**Example request**:

```http
POST /api/v15/fs/hash/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "src": "L0Rpc3F1ZSBkdXIvbXlfZmlsZQ==", /* /Disque dur/my_file */
   "hash_type": "md5"
}
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "curr_bytes_done": 0,
        "total_bytes": 4242,
        "nfiles_done": 0,
        "started_ts": 1355842559,
        "duration": 0,
        "done_ts": 0,
        "curr_bytes": 4242,
        "type": "hash",
        "to": "",
        "id": 50,
        "nfiles": 1,
        "created_ts": 1355842559,
        "state": "running",
        "total_bytes_done": 0,
        "from": "/Disque dur/my_file",
        "rate": 0,
        "eta": 0,
        "error": "none",
        "progress": 0
    }
}
```

<a id="get-the-hash-value"></a>

#### Get the hash value

To get the hash, the task must have succeed and be in the state
“done”.

<a id="get--api-v15-fs-tasks-id-hash"></a>

**`GET /api/v15/fs/tasks/{id}/hash`**

**Example request**:

```http
GET /api/v15/fs/tasks/50/hash HTTP/1.1
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
        "hash": "94baaad4d1347ec6e15ae35c88ee8bc8"
    }
}
```

<a id="create-a-directory"></a>

### Create a directory

Contrary to other file system tasks, this operation is done
synchronously.

Instead of a returning a [`FsTask`](fs.md#FsTask "FsTask") a call to this API
will only return success status

<a id="post--api-v15-fs-mkdir-"></a>

**`POST /api/v15/fs/mkdir/`**

Parameters

- **parent** (*string*) – The parent directory path (base64 encoded)
- **dirname** (*string*) – The name of the directory to create

**Example request**:

```http
POST /api/v15/fs/mkdir/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "parent": "L0Rpc3F1ZSBkdXI=", /* /Disque dur */
   "dirname": "Test"
}
```

**Example response**:

```json
{
    "success": true
}
```

<a id="rename-a-file-folder"></a>

### Rename a file/folder

Contrary to other file system tasks, this operation is done
synchronously.

Instead of a returning a [`FsTask`](fs.md#FsTask "FsTask") a call to this API
will only return success status and the new path as a result

<a id="post--api-v15-fs-rename-"></a>

**`POST /api/v15/fs/rename/`**

Parameters

- **src** (*string*) – The source file path (base64 encoded)
- **dst** (*string*) – The new name of the file (clear text, without path)

**Example request**:

```http
POST /api/v15/fs/rename/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "src": "L0Rpc3F1ZSBkdXIvdGVzdC50eHQ=", /* /Disque dur/test.txt */
   "dst": "plop.txt"
}
```

**Example response**:

```json
{
    "success": true,
    "result": "L0Rpc3F1ZSBkdXIvcGxvcC50eHQ=" /* /Disque dur/plop.txt */
}
```

<a id="download-a-file"></a>

### Download a file

<a id="get--api-v15-dl-path"></a>

**`GET /api/v15/dl/{path}`**

**Example request**:

```http
GET /api/v15/dl/L0Rpc3F1ZSBkdXIvUGhvdG9zL1BsYW5zIHNlY3JldHMuanBn HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: image/jpeg
Content-Length: 600864
Content-Disposition: attachment; filename="Plans secrets.jpg"

[ ... ]
```
