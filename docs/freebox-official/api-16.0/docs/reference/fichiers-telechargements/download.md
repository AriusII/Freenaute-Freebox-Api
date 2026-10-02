<a id="download"></a>

# Download

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#download)

## Navigation

- [Download Errors](#download-errors)
- [Download Task / TaskFile Errors](#download-task-taskfile-errors)
- [Download object](#download-object)
- [Download API](#download-api)
- [Download Nzb configuration status Object](#download-nzb-configuration-status-object)
- [Download DHT stats Object](#download-dht-stats-object)
- [Download Stats Object](#download-stats-object)
- [Download Files Object](#download-files-object)
- [Download Files API](#download-files-api)
- [Download Tracker Object](#download-tracker-object)
- [Download Tracker API](#download-tracker-api)
- [Download Peer Object](#download-peer-object)
- [Get the pieces status a given download](#get-the-pieces-status-a-given-download)
- [Download Blacklist Object](#download-blacklist-object)


With the download API you can control the download queue of the
Freebox. The Freebox supports downloads from HTTP, FTP, Magnet link,
`.torrent` files and newsgroups (NNTP). Each download task is
represented by a [`Download`](download.md#Download "Download") object.

<a id="download-errors"></a>

## Download Errors

When attempting to access the download API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| task_not_found | No task was found with the given id |
| invalid_operation | Attempt to perform an invalid operation |
| invalid_file | Error with the download file (invalid format ?) |
| invalid_url | URL is invalid |
| not_implemented | Method not implemented |
| out_of_memory | No more memory available to perform the requested action |
| invalid_task_type | The task type is invalid |
| hibernating | The downloader is hibernating |
| need_bt_stopped_done | This action is only valid for Bittorrent task in stopped or done state |
| bt_tracker_not_found | Attempt to access an invalid tracker object |
| too_many_tasks | Too many tasks |
| invalid_address | Invalid peer address |
| port_conflict | Port conflict when setting config |
| invalid_priority | Invalid priority |
| internal_error | Internal error |
| ctx_file_error | Failed to initialize task context file (need to check disk) |
| exists | Same task already exists |
| port_outside_range | Incoming port is not available for this customer (see [`ConnectionStatus`](../reseau/connection.md#ConnectionStatus "ConnectionStatus") ipv4_port_range) |

<a id="download-task-taskfile-errors"></a>

## Download Task / TaskFile Errors

Each download task can encounter one of the following errors:

| Error | Description |
| --- | --- |
| none | No error |
| internal | Internal error |
| disk_full | The disk is full |
| unknown | Unknown error |
| parse_error | Parse error |
| http_301 | HTTP 301 error |
| http_400 | HTTP 400 error |
| http_401 |  |
| http_402 |  |
| http_403 |  |
| http_404 |  |
| http_405 |  |
| http_406 |  |
| http_407 |  |
| http_408 |  |
| http_409 |  |
| http_410 |  |
| http_411 |  |
| http_412 | [ … ] |
| http_413 |  |
| http_414 |  |
| http_415 |  |
| http_416 |  |
| http_417 |  |
| http_422 |  |
| http_423 |  |
| http_424 |  |
| http_425 |  |
| http_426 |  |
| http_427 |  |
| http_428 |  |
| http_429 |  |
| http_430 |  |
| http_431 |  |
| http_4xx | Other 4xx HTTP errors |
| http_500 | HTTP 500 error |
| http_501 |  |
| http_502 |  |
| http_503 |  |
| http_504 |  |
| http_505 |  |
| http_506 | [ … ] |
| http_507 |  |
| http_508 |  |
| http_509 |  |
| http_510 |  |
| http_511 |  |
| http_5xx | Other 5xx HTTP errors |
| http_redirections_exceeded | Too many HTTP redirections |
| nzb_no_group | Cannot find the requested group on server |
| nzb_not_found | Article not fount on the server |
| nzb_invalid_crc | Invalid article CRC |
| nzb_invalid_size | Invalid article size |
| nzb_invalid_filename | Invalid filename |
| nzb_open_failed | Error opening |
| nzb_write_failed | Error writing |
| nzb_missing_size | Missing article size |
| nzb_decode_error | Article decoding error |
| nzb_missing_segments | Missing article segments |
| nzb_error | Other nzb error |
| unknown_host | Unknown host |
| timeout | Timeout |
| bad_authentication | Invalid credentials |
| connection_refused | Remote host refused connection |
| nzb_authentication_required | Nzb server need authentication |
| bt_tracker_error | Unable to announce on tracker |
| bt_missing_files | Missing torrent files |
| bt_file_error | Error accessing torrent files |
| missing_ctx_file | Error accessing task context file |

<a id="download-object"></a>

## Download object

Download objects have the following attributes:

<a id="Download"></a>

### Objet Download

<a id="Download.id"></a>

**`id int Read-only`**

id

<a id="Download.type"></a>

**`type enum Read-only`**

The valid download types are:

| Type | Description |
| --- | --- |
| bt | bittorrent download |
| nzb | newsgroup download |
| http | HTTP download |
| ftp | FTP download |

<a id="Download.name"></a>

**`name string Read-only`**

<a id="Download.status"></a>

**`status enum`**

The valid download status are:

| Status | Description |
| --- | --- |
| stopped | task is stopped, can be resumed by setting the status to downloading |
| queued | task will start when a new download slot is available the queue position is stored in queue_pos attribute |
| starting | task is preparing to start download |
| downloading |  |
| stopping | task is gracefully stopping |
| error | there was a problem with the download, you can get an error code in the error field |
| done | the download is over. For bt you can resume seeding setting the status to seeding if the ratio is not reached yet |
| checking | (only valid for nzb) download is over, the downloaded files are being checked using par2 |
| repairing | (only valid for nzb) download is over, the downloaded files are being repaired using par2 |
| extracting | (only valid for nzb) download is over, the downloaded files are being extracted |
| seeding | (only valid for bt) download is over, the content is Change to being shared to other users. The task will automatically stop once the seed ratio has been reached |
| retry | You can set a task status to ‘retry’ to restart the download task. |

<a id="Download.size"></a>

**`size int Read-only`**

download size (in Bytes)

<a id="Download.queue_pos"></a>

**`queue_pos int`**

position in download queue (0 if not queued)

<a id="Download.io_priority"></a>

**`io_priority enum`**

The valid download priorities are:

| Priority | Description |
| --- | --- |
| low | low |
| normal | normal |
| high | high |

<a id="Download.tx_bytes"></a>

**`tx_bytes int Read-only`**

transmitted bytes (including protocol overhead)

<a id="Download.rx_bytes"></a>

**`rx_bytes int Read-only`**

received bytes (including protocol overhead)

<a id="Download.tx_rate"></a>

**`tx_rate int Read-only`**

current transmit rate (in byte/s)

<a id="Download.rx_rate"></a>

**`rx_rate int Read-only`**

current receive rate (in byte/s)

<a id="Download.tx_pct"></a>

**`tx_pct int Read-only`**

transmit percentage (without protocol overhead)

To improve precision the value as been scaled by 100 so that a
tx_pct of 123 means 1.23%

<a id="Download.rx_pct"></a>

**`rx_pct int Read-only`**

received percentage (without protocol overhead)

To improve precision the value as been scaled by 100 so that a
tx_pct of 123 means 1.23%

<a id="Download.error"></a>

**`error enum Read-only`**

An error code

<a id="Download.created_ts"></a>

**`created_ts timestamp Read-only`**

timestamp of the download creation time

<a id="Download.eta"></a>

**`eta int Read-only`**

estimated remaining download time (in seconds)

<a id="Download.download_dir"></a>

**`download_dir string Read-only`**

directory where the file(s) will be saved (base64 encoded)

<a id="Download.stop_ratio"></a>

**`stop_ratio int Read-only`**

Only relevant for bittorrent tasks. Once the transmit ration
has been reached the task will stop seeding.

The ratio is scaled by 100 to improve resolution.

A stop_ratio of 150 means that the task will stop seeding once
tx_bytes = 1.5 \* rx_bytes.

<a id="Download.archive_password"></a>

**`archive_password string`**

(**only relevant for nzb**) password for extracting downloaded
archives

<a id="Download.info_hash"></a>

**`info_hash string`**

(**only relevant for bt**) torrent info_hash encoded in hexa

<a id="Download.piece_length"></a>

**`piece_length int`**

(**only relevant for bt**) torrent piece length in bytes

<a id="download-api"></a>

<a id="dl-api"></a>

## Download API

<a id="retrieve-a-download-task"></a>

### Retrieve a Download task

<a id="get--api-v8-downloads-"></a>

**`GET /api/v8/downloads/`**

Returns the collection of all [`Download`](download.md#Download "Download") tasks

**Example request**:

```http
GET /api/v8/downloads/ HTTP/1.1
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
            "rx_bytes": 147450,
            "tx_bytes": 3460,
            "download_dir": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv",
            "archive_password": "",
            "eta": 60290,
            "status": "downloading",
            "io_priority": "normal",
            "type": "bt",
            "piece_length": 524288,
            "queue_pos": 2,
            "id": 1273,
            "info_hash": "A7055D06E5A8F7F816EC01AC7F7F5243D3CB008F",
            "created_ts": 1485513882,
            "stop_ratio": 150,
            "tx_rate": 202,
            "name": "debian-8.7.1-amd64-CD-1.iso",
            "tx_pct": 0,
            "rx_pct": 0,
            "rx_rate": 10950,
            "error": "none",
            "size": 660600000
      }
}
```

<a id="get--api-v8-downloads-id"></a>

**`GET /api/v8/downloads/{id}`**

Returns the [`Download`](download.md#Download "Download") task with the given id

**Example request**:

```http
GET /api/v8/downloads/16 HTTP/1.1
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
        "rx_bytes": 688005364,
        "tx_bytes": 3232055279,
        "download_dir": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv", /* /Disque dur/Téléchargements/ */
        "archive_password": "",
        "eta": 331896,
        "status": "seeding",
        "io_priority": "high",
        "size": 678428672,
        "type": "bt",
        "error": "none",
        "queue_pos": 0,
        "id": 14,
        "created_ts": 1349786169,
        "tx_rate": 0,
        "name": "debian-6.0.6-amd64-CD-1.iso",
        "rx_pct": 10000,
        "rx_rate": 0,
        "tx_pct": 0
    }

}
```

<a id="delete-a-download-task"></a>

### Delete a Download task

<a id="delete--api-v8-downloads-id"></a>

**`DELETE /api/v8/downloads/{id}`**

Deletes the [`Download`](download.md#Download "Download") task with the given id,
**without** erasing the downloaded files If the task was not done
it is stopped

You can call this method to remove done tasks from the task list.

**Example request**:

```http
DELETE /api/v8/downloads/16 HTTP/1.1
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

<a id="delete--api-v8-downloads-id-erase"></a>

**`DELETE /api/v8/downloads/{id}/erase`**

Same as previous, but **erases** the downloaded files

<a id="update-a-download-task"></a>

### Update a Download task

<a id="put--api-v8-downloads-id"></a>

**`PUT /api/v8/downloads/{id}`**

Updates the [`Download`](download.md#Download "Download") task with the given id

**Example request**:

```http
PUT /api/v8/downloads/16 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "io_priority": "high",
   "status": "stopped"
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
        "rx_bytes": 683407058,
        "tx_bytes": 17866436,
        "download_dir": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv", /* /Disque dur/Téléchargements/ */
        "eta": 1075260392,
        "status": "stopping",
        "io_priority": "high",
        "size": 678428672,
        "type": "bt",
        "error": "none",
        "queue_pos": 0,
        "id": 14,
        "created_ts": 1349786169,
        "tx_rate": 0,
        "name": "debian-6.0.6-amd64-CD-1.iso",
        "stop_ratio": 55936,
        "rx_pct": 10000,
        "rx_rate": 0,
        "tx_pct": 4
    }
}
```

<a id="get-download-log"></a>

### Get download log

<a id="get--api-v8-downloads-id-log"></a>

**`GET /api/v8/downloads/{id}/log`**

Get the log.

**Example request**:

```http
GET /api/v8/downloads/16/log HTTP/1.1
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
    "result": "log line\nanother log line\n"
}
```

<a id="adding-a-new-download-task"></a>

### Adding a new Download task

<a id="adding-by-url"></a>

#### Adding by URL

Supported URL scheme are `http://`, `ftp://`, `magnet:`

You can start a recursive download by setting the recursive parameter.
The downloader will then extract links from each donwloaded html page
and continue downloading files on the same domain and on the same root
path. This can be used to download all the files on a directory
index.

You can add multiple downloads at once by passing a list of URL
(separated by a new line delimiter) in download_url_list instead of
using download_url.

/!NOTE: for this API the request arguments must be encoded using
“application/x-www-form-urlencoded” (or “multipart/form-data” for file
upload) instead of “application/json”

<a id="post--api-v8-downloads-add"></a>

**`POST /api/v8/downloads/add`**

Parameters

- **download_url** (*string*) – The URL
- **download_url_list** (*string*) – A list of URL separated by a new
  line delimiter (use download_url
  or download_url_list)
- **download_dir** (*string*) – The download destination directory
  (optional: will use the
  configuration download_dir by
  default)
- **filename** (*string*) – Override the name of the destination file. Only
  valid with one, non-recursive download_url.
- **hash** (*string*) – Verify the hash of the downloaded file. The format is
  sha256:xxxxxx or sha512:xxxxxx; or the URL of a
  SHA256SUMS, SHA512SUMS, -CHECKSUM or .sha256 file.
  Only valid with one, non-recursive download_url.
- **recursive** (*bool*) – If true the download will be recursive
- **username** (*string*) – Auth username (optional)
- **password** (*string*) – Auth password (optional)
- **archive_password** (*string*) – The password required to extract
  downloaded content (only relevant
  for nzb)
- **cookies** (*string*) – The http cookies (to be able to pass
  session cookies along with url). This is the content
  of the HTTP Cookie header, for example: cookie1=value1;
  cookie2=value2

NOTE: instead of passing password and username you can include them in the URL.

**Example request : Single download add**:

```http
POST /api/v8/downloads/add HTTP/1.1
Host: mafreebox.freebox.fr

download_url=http%3A%2F%2Fcdimage.debian.org%2Fdebian-cd%2F6.0.6%2Famd64%2Fbt-cd%2Fdebian-6.0.6-amd64-CD-1.iso.torrent
&download_dir=L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv
```

**Example response**:

On success you’ll get the id of the new download task.

```json
{
   "result": {
      "id": 23
    },
   "success": true
}
```

**Example request : Multiple downloads at once**:

```http
POST /api/v8/downloads/add HTTP/1.1
Host: mafreebox.freebox.fr

download_url_list=ftp%3A%2F%2Ftest-debit.free.fr%2F1024.rnd
         %0Ahttp%3A%2F%2Ftest-debit.free.fr%2F4096.rnd
         %0Ahttp%3A%2F%2Ftest-debit.free.fr%2F32768.rnd
&download_dir=L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv
```

**Example response**:

On success you’ll get the list of id of the new download tasks.

```json
{
   "result": {
       "id": [
           32,
           33,
           34
       ]
    },
    "success": true
}
```

<a id="adding-by-file-upload"></a>

#### Adding by file upload

Supported files are .torrent, .nzb,

<a id="post--api-v8-downloads-add--variante-2"></a>

**`POST /api/v8/downloads/add`**

Parameters

- **download_file** (*string*) – The download file (must be uploaded
  using multipart/form-data)
- **download_dir** (*string*) – The download destination directory
  (optional: will use the
  configuration download_dir by
  default)
- **archive_password** (*string*) – The password required to extract
  downloaded content (only relevant
  for nzb)

**Example request**:

```http
POST /api/v8/downloads/add HTTP/1.1
Host: mafreebox.freebox.fr
Content-Type: multipart/form-data; boundary=---------------------------176791920111939857911845395343
Content-Length: 26651

-----------------------------176791920111939857911845395343
Content-Disposition: form-data; name="download_dir"

L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv
-----------------------------176791920111939857911845395343
Content-Disposition: form-data; name="archive_password"

-----------------------------176791920111939857911845395343
Content-Disposition: form-data; name="download_file"; filename="debian-6.0.6-amd64-CD-1.iso.torrent"
Content-Type: application/x-bittorrent

d8:announce41:http://bttracker.debian.org:6969/announce7:comment [ ... ]
```

**Example response**:

```json
{
   "result": {
      "id": 42
   },
   "success": true
}
```

<a id="download-stats"></a>

# Download Stats

If you just want to display synthetic information about downloader
this is the method to use.

<a id="download-nzb-configuration-status-object"></a>

## Download Nzb configuration status Object

<a id="NzbConfigStatus"></a>

### Objet NzbConfigStatus

<a id="NzbConfigStatus.status"></a>

**`status enum Read-only`**

The valid config status are:

| Type | Description |
| --- | --- |
| not_checked | config has not been checked yet |
| checking | test in progress |
| error | config is invalid, see error |
| ok | config is ok |

<a id="NzbConfigStatus.error"></a>

**`error enum Read-only`**

The valid config status are:

| Type | Description |
| --- | --- |
| none | test is ok |
| nzb_authentication_required | authentication is required |
| bad_authentication | incorrect credentials |
| connection_refused | unable to connect to NNTP server |

<a id="download-dht-stats-object"></a>

## Download DHT stats Object

<a id="DhtStats"></a>

### Objet DhtStats

<a id="DhtStats.enabled"></a>

**`enabled bool Read-only`**

is the dht enabled

<a id="DhtStats.node_count"></a>

**`node_count int Read-only`**

number of active nodes

<a id="DhtStats.enabled_ipv6"></a>

**`enabled_ipv6 bool Read-only`**

is the dht enabled on IPv6

<a id="DhtStats.node_count_ipv6"></a>

**`node_count_ipv6 int Read-only`**

number of active nodes on IPv6

<a id="download-stats-object"></a>

## Download Stats Object

<a id="DownloadStats"></a>

### Objet DownloadStats

<a id="DownloadStats.nb_tasks"></a>

**`nb_tasks int Read-only`**

total number of tasks

<a id="DownloadStats.nb_tasks_stopped"></a>

**`nb_tasks_stopped int Read-only`**

number of stopped tasks

<a id="DownloadStats.nb_tasks_checking"></a>

**`nb_tasks_checking int Read-only`**

number of checking tasks

<a id="DownloadStats.nb_tasks_queued"></a>

**`nb_tasks_queued int Read-only`**

number of queued tasks

<a id="DownloadStats.nb_tasks_extracting"></a>

**`nb_tasks_extracting int Read-only`**

number of extracting tasks

<a id="DownloadStats.nb_tasks_done"></a>

**`nb_tasks_done int Read-only`**

number of done tasks

<a id="DownloadStats.nb_tasks_repairing"></a>

**`nb_tasks_repairing int Read-only`**

number of repairing tasks

<a id="DownloadStats.nb_tasks_seeding"></a>

**`nb_tasks_seeding int Read-only`**

number of seeding tasks

<a id="DownloadStats.nb_tasks_downloading"></a>

**`nb_tasks_downloading int Read-only`**

number of downloading tasks

<a id="DownloadStats.nb_tasks_error"></a>

**`nb_tasks_error int Read-only`**

number of error tasks

<a id="DownloadStats.nb_tasks_stopping"></a>

**`nb_tasks_stopping int Read-only`**

number of stopping tasks

<a id="DownloadStats.nb_tasks_active"></a>

**`nb_tasks_active int Read-only`**

number of active tasks (checking + queued + extracting +
repairing + seeding + downloading)

<a id="DownloadStats.nb_rss"></a>

**`nb_rss int Read-only`**

number of RSS feed subscriptions

<a id="DownloadStats.nb_rss_items_unread"></a>

**`nb_rss_items_unread int Read-only`**

number of unread RSS items

<a id="DownloadStats.rx_rate"></a>

**`rx_rate int Read-only`**

current receive rate in bytes / second

<a id="DownloadStats.tx_rate"></a>

**`tx_rate int Read-only`**

current transmit rate in bytes / second

<a id="DownloadStats.throttling_mode"></a>

**`throttling_mode enum Read-only`**

active throttling_mode (see [`DlThrottlingConfig`](download_config.md#DlThrottlingConfig "DlThrottlingConfig"))

<a id="DownloadStats.throttling_is_scheduled"></a>

**`throttling_is_scheduled bool Read-only`**

if true, the current throttling mode has been computed using the
throttling schedule

if false, the current throttling mode has been manually forced

<a id="DownloadStats.throttling_rate"></a>

**`` throttling_rate :json:object:`DlRate` Read-only ``**

current rate for throttling

<a id="DownloadStats.nzb_config_status"></a>

**`` nzb_config_status :json:object:`NzbConfigStatus` Read-only ``**

current nzb configuration status

<a id="DownloadStats.conn_ready"></a>

**`conn_ready bool Read-only`**

is the connection ready

<a id="DownloadStats.nb_peer"></a>

**`nb_peer int Read-only`**

number of bittorrent peers

<a id="DownloadStats.blocklist_entries"></a>

**`blocklist_entries int Read-only`**

number of rules in blocklist

<a id="DownloadStats.blocklist_hits"></a>

**`blocklist_hits int Read-only`**

number of hits in blocklist

<a id="DownloadStats.dht_stats"></a>

**`` dht_stats :json:object:`DhtStats` Read-only ``**

dht stats

<a id="get-the-download-stats"></a>

### Get the Download Stats

<a id="get--api-v8-downloads-stats"></a>

**`GET /api/v8/downloads/stats`**

**Example request**:

```http
GET /api/v8/downloads/stats HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```json
{
    "success": true,
    "result": {
        "throttling_rate": {
            "rx_rate": 0,
            "tx_rate": 0
        },
        "nb_tasks_stopped": 1,
        "nb_tasks_checking": 0,
        "nb_tasks_queued": 0,
        "nb_tasks_extracting": 4,
        "nb_tasks_done": 1,
        "nb_tasks_repairing": 0,
        "throttling_mode": "normal",
        "nb_tasks_active": 11,
        "tx_rate": 4294,
        "nb_tasks_downloading": 4,
        "throttling_is_scheduled": true,
        "nb_tasks": 13,
        "nb_tasks_error": 0,
        "nb_tasks_stopping": 0,
        "nb_rss_items_unread": 5,
        "rx_rate": 14222,
        "nb_tasks_seeding": 3
    }
}
```

<a id="download-files"></a>

# Download Files

<a id="download-files-object"></a>

## Download Files Object

Each [`Download`](download.md#Download "Download") has one or more [`DownloadFile`](download.md#DownloadFile "DownloadFile").

<a id="DownloadFile"></a>

### Objet DownloadFile

<a id="DownloadFile.id"></a>

**`id string Read-only`**

opaque id

<a id="DownloadFile.task_id"></a>

**`task_id int Read-only`**

id of the download task

<a id="DownloadFile.filepath"></a>

**`filepath string Read-only`**

full filepath on the disk (encoded as in file system api)

<a id="DownloadFile.name"></a>

**`name string Read-only`**

file name

<a id="DownloadFile.mimetype"></a>

**`mimetype string Read-only`**

file mimetype

<a id="DownloadFile.size"></a>

**`size int Read-only`**

file size in bytes

<a id="DownloadFile.rx"></a>

**`rx int Read-only`**

received bytes

<a id="DownloadFile.status"></a>

**`status enum Read-only`**

file download status

| Status | Description |
| --- | --- |
| queued | file is queued for download |
| error | there was a problem with this file, see error to get the error code |
| done | file download is completed |

<a id="DownloadFile.error"></a>

**`error enum Read-only`**

file error code in case status is error

<a id="DownloadFile.priority"></a>

**`priority string`**

file download priority inside the download task

| Priority | Description |
| --- | --- |
| no_dl | this file will not be downloaded |
| low | low priority |
| normal | default priority |
| high | high priority |

<a id="DownloadFile.preview_url"></a>

**`preview_url string Read-only`**

url to preview downloaded file (only available for bittorrent)
as a share link, this url can be use without requiring any
form of authentication so that it can be passed as-is to any
software.

<a id="download-files-api"></a>

## Download Files API

<a id="get-the-list-of-files-for-a-given-download"></a>

### Get the list of files for a given Download

<a id="get--api-v8-downloads-task_id-files"></a>

**`GET /api/v8/downloads/{task_id}/files`**

**Example request**:

```http
GET /api/v8/downloads/37/files HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

Exemple adapté : les champs dépréciés ont été retirés ou remplacés selon la documentation officielle ; les chemins HTTP restent ceux de la source.

```json
{
    "success": true,
    "result": [
      {
          "id": "5-1",
          "task_id": "5",
          "filepath": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMvL3Rlc3QtZGViaXQuZnJlZS5mci5odG1s",
          "mimetype": "text/html",
          "name": "test-debit.free.fr.html",
          "rx": 0,
          "status": "done",
          "priority": "normal",
          "error": "none",
          "size": 0

      },
      {
          "id": "5-7",
          "task_id": "5",
          "filepath": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMvL3Rlc3QtZGViaXQuZnJlZS5mci8xMDI0LnJuZA==",
          "mimetype": "application/octet-stream",
          "name": "1024.rnd",
          "rx": 1048576,
          "status": "done",
          "priority": "low",
          "error": "none",
          "size": 1048576
      },

        [ ... ]

      {
          "id": "5-16",
          "task_id": "5",
          "filepath": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMvL3Rlc3QtZGViaXQuZnJlZS5mci9pbWFnZS5pc28=",
          "mimetype": "application/x-cd-image",
          "name": "image.iso",
          "rx": 678428672,
          "status": "done",
          "priority": "low",
          "error": "none",
          "size": 678428672
      }
    ]

}
```

<a id="change-the-priority-of-a-download-file"></a>

### Change the priority of a Download File

<a id="put--api-v8-downloads-task_id-files-file_id"></a>

**`PUT /api/v8/downloads/{task_id}/files/{file_id}`**

Parameters

- **task_id** (*string*) – The download task id
- **path** (*string*) – The file_id
- **priority** (*string*) – The new file download priority

**Example request**:

```http
PUT /api/v8/downloads/37/files/37-4 HTTP/1.1
Host: mafreebox.freebox.fr

{
  "priority" : "high"
}
```

**Example response**:

```json
{
   "success": true
}
```

<a id="download-trackers-unstable"></a>

# Download Trackers [UNSTABLE]

<a id="download-tracker-object"></a>

## Download Tracker Object

Each torrent [`Download`](download.md#Download "Download") task has one or more
[`DownloadTracker`](download.md#DownloadTracker "DownloadTracker").

Each tracker is identified by its announce URL.

<a id="DownloadTracker"></a>

### Objet DownloadTracker

<a id="DownloadTracker.announce"></a>

**`announce string Read-only`**

tracker announce URL

<a id="DownloadTracker.is_backup"></a>

**`is_backup bool Read-only`**

true if the tracker is a backup tracker (the downloader won’t
connect to this tracker unless the primary tracker fails)

<a id="DownloadTracker.status"></a>

**`status enum Read-only`**

tracker status

| Status | Description |
| --- | --- |
| unannounced | not announced |
| announcing | announcing |
| announce_failed | an error occurred while trying to announce |
| announced | announced |

<a id="DownloadTracker.interval"></a>

**`interval int Read-only`**

desired interval between two announces (in seconds)

<a id="DownloadTracker.min_interval"></a>

**`min_interval int Read-only`**

minimum interval between two announces (in seconds)

<a id="DownloadTracker.reannounce_in"></a>

**`reannounce_in int Read-only`**

time left before reannounce (in seconds)

<a id="DownloadTracker.nseeders"></a>

**`nseeders int Read-only`**

number of seeders announced on tracker

<a id="DownloadTracker.nleechers"></a>

**`nleechers int Read-only`**

number of leechers announced on tracker

<a id="DownloadTracker.is_enabled"></a>

**`is_enabled bool`**

is the tracker enabled

<a id="download-tracker-api"></a>

## Download Tracker API

<a id="get-the-list-of-trackers-for-a-given-download"></a>

### Get the list of trackers for a given Download

Attempting to call this method on a download other than bittorent will
fail

<a id="get--api-v8-downloads-task_id-trackers"></a>

**`GET /api/v8/downloads/{task_id}/trackers`**

**Example request**:

```http
GET /api/v8/downloads/35/tracker HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```json
{
    "success": true,
    "result": [
        {
            "nseeders": 0,
            "nleechers": 0,
            "reannounce_in": 790,
            "is_backup": false,
            "interval": 900,
            "min_interval": 60,
            "announce": "http://bttracker.debian.org:6969/announce",
            "status": "announced"
        }
    ]
}
```

<a id="add-a-new-tracker"></a>

### Add a new tracker

Attempting to call this method on a download other than bittorent will
fail

<a id="post--api-v8-downloads-task_id-trackers"></a>

**`POST /api/v8/downloads/{task_id}/trackers`**

**Example request**:

```http
POST /api/v8/downloads/35/tracker HTTP/1.1
Host: mafreebox.freebox.fr

{
  "announce": "udp://tracker.openbittorrent.com:80"
}
```

**Example response**:

```json
{
    "success": true
}
```

<a id="remove-a-tracker"></a>

### Remove a tracker

<a id="delete--api-v8-downloads-task_id-trackers-announce"></a>

**`DELETE /api/v8/downloads/{task_id}/trackers/{announce}`**

**Example request**:

```http
DELETE /api/v8/downloads/35/tracker/udp%3A%2F%2Ftracker.openbittorrent.com%3A80 HTTP/1.1
Host: mafreebox.freebox.fr

{
  "announce": "udp://tracker.openbittorrent.com:80"
}
```

**Example response**:

```json
{
    "success": true
}
```

<a id="update-a-tracker"></a>

### Update a tracker

<a id="put--api-v8-downloads-task_id-trackers-announce"></a>

**`PUT /api/v8/downloads/{task_id}/trackers/{announce}`**

**Example request**:

```http
PUT /api/v8/downloads/35/tracker/udp%3A%2F%2Ftracker.openbittorrent.com%3A80 HTTP/1.1
Host: mafreebox.freebox.fr

{
  "announce": "udp://tracker.openbittorrent.com:80",
  "is_enabled": true
}
```

**Example response**:

```json
{
    "success": true
}
```

<a id="download-peers-unstable"></a>

# Download Peers [UNSTABLE]

<a id="download-peer-object"></a>

## Download Peer Object

Each torrent [`Download`](download.md#Download "Download") task has one or more
[`DownloadPeer`](download.md#DownloadPeer "DownloadPeer").

<a id="DownloadPeer"></a>

### Objet DownloadPeer

<a id="DownloadPeer.host"></a>

**`host string Read-only`**

peer IP

<a id="DownloadPeer.port"></a>

**`port int Read-only`**

peer port

<a id="DownloadPeer.state"></a>

**`state enum Read-only`**

peer state

| State | Description |
| --- | --- |
| disconnected | not connected |
| connecting | trying to connect to the peer |
| handshaking | connected to the peer, negotiating capabilities |
| ready | ready to exchange data |

<a id="DownloadPeer.origin"></a>

**`origin enum Read-only`**

peer origin

| Origin | Description |
| --- | --- |
| tracker | got the peer from the tracker |
| incoming | incoming peer |
| dht | got the peer from DHT |
| pex | got the peer from Peer exchange protocol |
| user | manually added peer |

<a id="DownloadPeer.protocol"></a>

**`protocol enum Read-only`**

| Protocol | Description |
| --- | --- |
| tcp | TCP |
| tcp_obfuscated | Obfuscated TCP |
| udp | UDP |

<a id="DownloadPeer.client"></a>

**`client string Read-only`**

Bittorrent client name

<a id="DownloadPeer.country_code"></a>

**`country_code string Read-only`**

Peer country code (iso 3166)

If country code is not available it will have the value “??”

<a id="DownloadPeer.tx"></a>

**`tx int Read-only`**

transmitted bytes

<a id="DownloadPeer.rx"></a>

**`rx int Read-only`**

received bytes

<a id="DownloadPeer.tx_rate"></a>

**`tx_rate int Read-only`**

current transmit rate in byte/s

<a id="DownloadPeer.rx_rate"></a>

**`rx_rate int Read-only`**

current receive rate in byte/s

<a id="DownloadPeer.progress"></a>

**`progress int Read-only`**

peer current download progress

<a id="DownloadPeer.requests"></a>

**`requests [] array of int Read-only`**

current requested pieces

<a id="get-the-list-of-peers-for-a-given-download"></a>

### Get the list of peers for a given Download

Attempting to call this method on a download other than bittorent will fail

<a id="get--api-v8-downloads-task_id-peers"></a>

**`GET /api/v8/downloads/{task_id}/peers`**

**Example request**:

```http
GET /api/v8/downloads/42/peers HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```json
{
    "success": true,
    "result": [
        {
            "protocol": "tcp_obfuscated",
            "origin": "tracker",
            "progress": 91,
            "remote_choke": true,
            "requests": { },
            "host": "186.213.200.201",
            "port": 0,
            "client": "Azureus 4.7.2.0",
            "country_code": "BR",
            "local_interest": false,
            "state": "ready",
            "rx": 1617,
            "tx": 836670,
            "remote_interest": true,
            "tx_rate": 0,
            "rx_rate": 0,
            "local_choke": false
        },

        [ ... ]

        {
          "protocol": "tcp",
          "origin": "tracker",
          "progress": 11,
          "remote_choke": true,
          "requests": { },
          "host": "208.127.4.60",
          "port": 0,
          "client": "Transmission 2.51",
          "country_code": "US",
          "local_interest": false,
          "state": "ready",
          "rx": 8929,
          "tx": 7592234,
          "remote_interest": true,
          "tx_rate": 0,
          "rx_rate": 0,
          "local_choke": false
        }
    ]

}
```

<a id="download-pieces"></a>

<a id="dl-pieces-api"></a>

# Download Pieces

Each Torrent is split in ‘pieces’ of fixed size. The Download Piece Api allow
tracking the download state of each pieces of a Torrent

<a id="get-the-pieces-status-a-given-download"></a>

## Get the pieces status a given download

The result value is a string, with each character representing a piece status.
Piece status can be:

| Status | Description |
| --- | --- |
| X | piece is complete |
|  | piece is currently downloading |
| . | piece is wanted but not downloading yet |
|  | piece is not wanted and will not be downloaded |
| / | piece is downloading with high priority as it is needed for file preview |
| U | piece is scheduled with high priority as it is needed for file preview |

<a id="get--api-v8-downloads-task_id-pieces"></a>

**`GET /api/v8/downloads/{task_id}/pieces`**

**Example request**:

```http
GET /api/v8/downloads/5/pieces HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```json
{
    "success": true,
    "result": "XXXXX//++....-- [ ... ] XXX"
}
```

<a id="download-blacklist-unstable"></a>

<a id="dl-blaklist-api"></a>

# Download Blacklist [UNSTABLE]

For bittorrent downloads, we use a blacklist to store information about “useless” or broken peers.
For instance if a peer is complete and we are trying to seed data, there is no use attempting to connect
to this peer again.

The download blacklist api allow you to retrieve information about this blacklist, and remove, or add peers
to the blacklist.

Each [`DownloadBlacklistEntry`](download.md#DownloadBlacklistEntry "DownloadBlacklistEntry") can be specific to a torrent, or “global” and apply to any torrent.

<a id="download-blacklist-object"></a>

## Download Blacklist Object

<a id="DownloadBlacklistEntry"></a>

### Objet DownloadBlacklistEntry

<a id="DownloadBlacklistEntry.host"></a>

**`host string Read-only`**

entry ip

<a id="DownloadBlacklistEntry.reason"></a>

**`reason enum Read-only`**

blacklist reason

| State | Description |
| --- | --- |
| not_blacklisted |  |
| crypto_not_supported | peer does not support encrypted connection |
| connect_fail | failed to connect |
| hs_timeout | handshake timeout |
| hs_failed | handshake failed |
| hs_crypt_failed | handshake failed during crypto |
| hs_crypto_disabled | handshake failed because encryption is disabled |
| torrent_not_found | torrent not found |
| read_failed | failed to read from peer |
| write_failed | failed to send data to peer |
| crap_received | received invalid data from peer |
| conn_closed | connection closed by remote peer |
| timeout | timeout |
| blocklist | peer is in a blocked ip range |
| user | manually blacklisted |

<a id="DownloadBlacklistEntry.expire"></a>

**`expire int Read-only`**

time left before blacklist removal

<a id="DownloadBlacklistEntry.global"></a>

**`global bool Read-only`**

does this entry applies to all torrents

<a id="get-the-list-of-blacklist-entries-for-a-given-download"></a>

### Get the list of blacklist entries for a given download

Attempting to call this method on a download other than bittorent will fail.

<a id="get--api-v8-downloads-task_id-blacklist"></a>

**`GET /api/v8/downloads/{task_id}/blacklist`**

**Example request**:

```http
GET /api/v8/downloads/5/blacklist HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```json
{
    "success": true,
    "result": [
        {
            "host": "89.215.188.6",
            "expire": 90,
            "global": true,
            "reason": "torrent_not_found"
        },
        {
            "host": "94.23.0.89",
            "expire": 120,
            "global": true,
            "reason": "conn_closed"
        },
        {
            "host": "188.254.151.215",
            "expire": 150,
            "global": true,
            "reason": "timeout"
        },
        {
            "host": "201.25.54.26",
            "expire": 180,
            "global": true,
            "reason": "timeout"
        }
    ]
}
```

<a id="empty-the-blacklist-for-a-given-download"></a>

### Empty the blacklist for a given download

This call allow to remove all global entries, and entries related to the given download

<a id="delete--api-v8-downloads-task_id-blacklist-empty"></a>

**`DELETE /api/v8/downloads/{task_id}/blacklist/empty`**

**Example request**:

```http
DELETE /api/v8/downloads/5/blacklist/empty HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```json
{
   "success": true
}
```

<a id="delete-a-particular-blacklist-entry"></a>

### Delete a particular blacklist entry

<a id="delete--api-v8-downloads-blacklist-host"></a>

**`DELETE /api/v8/downloads/blacklist/{host}`**

**Example request**:

```http
DELETE /api/v8/downloads/blacklist/201.25.54.26 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```json
{
   "success": true
}
```

<a id="add-a-blacklist-entry"></a>

### Add a blacklist entry

<a id="post--api-v8-downloads-blacklist"></a>

**`POST /api/v8/downloads/blacklist`**

**Example request**:

```http
POST /api/v8/downloads/blacklist HTTP/1.1
Host: mafreebox.freebox.fr

{
  "host": "8.8.8.8",
  "expire": 3600
}
```

**Example response**:

```json
{
    "success": true,
    "result":
       {
          "host": "197.200.139.87",
          "expire": 300,
          "global": true,
          "reason": "user"
       }
}
```
