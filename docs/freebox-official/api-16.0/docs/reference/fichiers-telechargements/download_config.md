<a id="download-configuration"></a>

# Download Configuration

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#download-configuration)

## Navigation

- [Download configuration object](#download-configuration-object)
- [Get the current Download configuration](#get-the-current-download-configuration)
- [Update the Download configuration](#update-the-download-configuration)


<a id="download-configuration-object"></a>

## Download configuration object

The download configuration is a singleton used to store the downloader
preferences.

<a id="global-config"></a>

### Global config

<a id="DownloadConfiguration"></a>

#### Objet DownloadConfiguration

<a id="DownloadConfiguration.max_downloading_tasks"></a>

**`max_downloading_tasks int`**

max concurrent download tasks

<a id="DownloadConfiguration.download_dir"></a>

**`download_dir string`**

the default path where downloads will be stored (base64 encoded)

<a id="DownloadConfiguration.watch_dir"></a>

**`watch_dir string`**

special folder that will be monitored. When a new supported file
(.nzb, .torrent) is copied in that folder, the task is
automatically added to the download queue.

(base64 encoded)

<a id="DownloadConfiguration.use_watch_dir"></a>

**`use_watch_dir bool`**

if set to false, the watch_dir will not be monitored

<a id="DownloadConfiguration.throttling"></a>

**`throttling DlThrottlingConfig`**

throttling configuration

<a id="DownloadConfiguration.news"></a>

**`news DlNewsConfig`**

newsgroups configuration

<a id="DownloadConfiguration.bt"></a>

**`bt DlBtConfig`**

bittorrent configuration

<a id="DownloadConfiguration.feed"></a>

**`feed DlFeedConfig`**

RSS feed configuration

<a id="DownloadConfiguration.blocklist"></a>

**`blocklist DlBlockListConfig`**

block list configuration

<a id="DownloadConfiguration.dns1"></a>

**`dns1 string`**

dns server ip to use for downloader (leave blank for default dns server)

<a id="DownloadConfiguration.dns2"></a>

**`dns2 string`**

dns server ip to use for downloader

<a id="throttling-config"></a>

### Throttling config

<a id="DlThrottlingConfig"></a>

#### Objet DlThrottlingConfig

<a id="DlThrottlingConfig.normal"></a>

**`normal DlRate`**

download rate for normal time slot (in B/s)

<a id="DlThrottlingConfig.slow"></a>

**`slow DlRate`**

download rate for normal slow slot (in B/s)

<a id="DlThrottlingConfig.schedule"></a>

**`schedule enum[168]`**

The schedule array represent the list of week hours timeslot,
starting on monday a midnight. Therefore the complete week is
represented in a array of 168 elements (24 \* 7)

Each slot can have the following value:

| Type | Description |
| --- | --- |
| normal | downloads will use normal DlRate config for this timeslot |
| slow | downloads will use slow DlRate config for this timeslot |
| hibernate | downloads will be paused for this timeslot |

<a id="DlThrottlingConfig.mode"></a>

**`mode enum`**

Throttling mode can have to following values

| Type | Description |
| --- | --- |
| normal | force use of normal rate limits (not using the scheduler) |
| slow | force use of slow rate limits (not using the scheduler) |
| hibernate | force hibernate (not using the scheduler) |
| schedule | use scheduded rate limit |

<a id="DlRate"></a>

#### Objet DlRate

<a id="DlRate.tx_rate"></a>

**`tx_rate int`**

maximum transmit rate (in byte/s)
0 means no limit

<a id="DlRate.rx_rate"></a>

**`rx_rate int`**

maximum receive rate (in byte/s)
0 means no limit

<a id="newsgroups-config"></a>

### Newsgroups config

<a id="DlNewsConfig"></a>

#### Objet DlNewsConfig

<a id="DlNewsConfig.server"></a>

**`server string`**

NNTP server hostname

<a id="DlNewsConfig.port"></a>

**`port int`**

NNTP server port

<a id="DlNewsConfig.ssl"></a>

**`ssl bool`**

Use SSL to connect to server if set to true

<a id="DlNewsConfig.user"></a>

**`user string`**

NNTP auth username (can be empty if no auth is required)

<a id="DlNewsConfig.password"></a>

**`password string Write-only`**

NNTP auth password (can be empty if no auth is required)

<a id="DlNewsConfig.nthreads"></a>

**`nthreads int`**

maximum concurrent connections to the NNTP server

<a id="DlNewsConfig.auto_repair"></a>

**`auto_repair bool`**

automatically check and repair downloaded files using the
provided par2 files

<a id="DlNewsConfig.lazy_par2"></a>

**`lazy_par2 bool`**

if set to true the downloader will download the par2 files only
if the download is corrupted

<a id="DlNewsConfig.auto_extract"></a>

**`auto_extract bool`**

automatically attempt to extract downloaded files

<a id="DlNewsConfig.erase_tmp"></a>

**`erase_tmp bool`**

if auto_extract is enabled, delete archive files once
successfully extracted

<a id="bittorrent-config"></a>

### Bittorrent config

<a id="DlBtConfig"></a>

#### Objet DlBtConfig

<a id="DlBtConfig.max_peers"></a>

**`max_peers int`**

maximum number of peers at a given time

<a id="DlBtConfig.stop_ratio"></a>

**`stop_ratio int`**

default stop_ratio for bt [`Download`](download.md#Download "Download") tasks

**This value is scaled by a factor 100**, for instance a
stop_ratio of 200 means that the task will stop once
tx_bytes = 2 \* size

A value of 0 means that the task will continue seeding until it
is manually stopped

<a id="DlBtConfig.crypto_support"></a>

**`crypto_support enum`**

The crypto_support can have the following values

| Type | Description |
| --- | --- |
| unsupported | will never use bittorrent crypto |
| allowed | will select plain during handshake |
| preferred | will select crypto during handshake |
| required | will allow plain bittorrent |

<a id="DlBtConfig.enable_dht"></a>

**`enable_dht bool`**

enable the dht protocol

<a id="DlBtConfig.enable_pex"></a>

**`enable_pex bool`**

enable the peer exchange protocol

<a id="DlBtConfig.announce_timeout"></a>

**`announce_timeout int`**

timeout in seconds for announcing to tracker

<a id="DlBtConfig.main_port"></a>

**`main_port int`**

main bittorrent port

<a id="DlBtConfig.dht_port"></a>

**`dht_port int`**

bittorrent dht port

<a id="rss-feeds-config"></a>

### Rss Feeds config

<a id="DlFeedConfig"></a>

#### Objet DlFeedConfig

<a id="DlFeedConfig.fetch_interval"></a>

**`fetch_interval int`**

interval between automatic RSS refresh (in minutes)

<a id="DlFeedConfig.max_items"></a>

**`max_items int`**

maximum feed item to keep

<a id="blocklist-config"></a>

### BlockList config

<a id="DlBlockListConfig"></a>

#### Objet DlBlockListConfig

<a id="DlBlockListConfig.sources[]"></a>

**`sources[] string`**

list of block list URL source

The block list should be in cidr format

e.g.: <http://list.iblocklist.com/?list=bt_level1&fileformat=cidr&archiveformat=>

<a id="get-the-current-download-configuration"></a>

## Get the current Download configuration

<a id="get--api-v8-downloads-config-"></a>

**`GET /api/v8/downloads/config/`**

Returns the current [`DownloadConfiguration`](download_config.md#DownloadConfiguration "DownloadConfiguration")

**Example request**:

```http
GET /api/v8/downloads/config/ HTTP/1.1
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
       "feed": {
           "max_items": 0,
           "fetch_interval": 60
       },
       "use_watch_dir": true,
       "watch_dir": "L0Rpc3F1ZSBkdXIvLnF1ZXVl", /* /Disque dur/.queue */
       "news": {
           "user": "",
           "erase_tmp": true,
           "port": 119,
           "nthreads": 1,
           "auto_repair": true,
           "ssl": false,
           "auto_extract": true,
           "lazy_par2": true,
           "server": "news.free.fr"
       },
       "bt": {
           "max_peers": 50,
           "stop_ratio": 150,
           "crypto_support": "allowed"
       },
       "max_downloading_tasks": 5,
       "download_dir": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv", /* /Disque dur/Téléchargements/ */
       "throttling": {
           "normal": {
               "rx_rate": 0,
               "tx_rate": 0
           },
           "slow": {
               "rx_rate": 512,
               "tx_rate": 42
           },
           "schedule": [
               "slow",
               "normal",
               "normal",

                [ ... ]

               "normal",
               "normal",
               "normal",
               "slow"
           ],
           "mode": "normal"
       }
   }
}
```

<a id="update-the-download-configuration"></a>

## Update the Download configuration

<a id="put--api-v8-downloads-config-"></a>

**`PUT /api/v8/downloads/config/`**

Updates the [`DownloadConfiguration`](download_config.md#DownloadConfiguration "DownloadConfiguration")

**Example request**:

```http
PUT /api/v8/downloads/config/ HTTP/1.1
Host: mafreebox.freebox.fr

{
    "throttling": {
        "normal": {
            "rx_rate": 512,
            "tx_rate": 40
        },
        "slow": {
            "rx_rate": 128,
            "tx_rate": 10
        },
        "mode": "normal",
        "schedule": [
            "slow",
            "normal",
            "normal",

            [ ... ]

            "normal",
            "normal",
            "normal",
            "normal",
            "slow"
        ]
    },
    "max_downloading_tasks": 5,
    "download_dir": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv", /* /Disque dur/Téléchargements/ */
    "use_watch_dir": true,
    "watch_dir": "L0Rpc3F1ZSBkdXIvLnF1ZXVl", /* /Disque dur/.queue */
    "news": {
        "server": "news.free.fr",
        "port": "119",
        "ssl": false,
        "nthreads": 1,
        "user": "",
        "lazy_par2": true,
        "auto_repair": true,
        "auto_extract": true,
        "erase_tmp": true
    },
    "bt": {
        "max_peers": 50,
        "stop_ratio": 150,
        "crypto_support": "allowed"
    },
    "feed": {
        "fetch_interval": 60
    }
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
        "feed": {
            "max_items": 0,
            "fetch_interval": 60
        },
        "use_watch_dir": true,
        "watch_dir": "L0Rpc3F1ZSBkdXIvLnF1ZXVl", /* /Disque dur/.queue */
        "news": {
            "user": "",
            "erase_tmp": true,
            "port": 119,
            "nthreads": 1,
            "auto_repair": true,
            "ssl": false,
            "auto_extract": true,
            "lazy_par2": true,
            "server": "news.free.fr"
        },
        "bt": {
            "max_peers": 50,
            "stop_ratio": 150,
            "crypto_support": "allowed"
        },
        "max_downloading_tasks": 5,
        "download_dir": "L0Rpc3F1ZSBkdXIvVMOpbMOpY2hhcmdlbWVudHMv", /* /Disque dur/Téléchargements/ */
        "throttling": {
            "normal": {
                "rx_rate": 512,
                "tx_rate": 40
            },
            "slow": {
                "rx_rate": 128,
                "tx_rate": 10
            },
            "schedule": [
                "slow",
                "normal",
                "normal",
                "normal",

                [ ... ]

                "normal",
                "normal",
                "normal",
                "slow"
            ],
            "mode": "normal"
        }
    }

}
```

<a id="updating-the-current-throttling-mode"></a>

### Updating the current Throttling mode

<a id="put--api-v8-downloads-throttling"></a>

**`PUT /api/v8/downloads/throttling`**

You can force the throttling mode using this method. You can use
any of the throttling modes defined in
[`DlThrottlingConfig`](download_config.md#DlThrottlingConfig "DlThrottlingConfig"). Setting to schedule will
automatically set correct throttling mode. Other values will force
the throttling mode until you set it back to schedule.

**Example request**:

```http
PUT /api/v8/downloads/throttling HTTP/1.1
Host: mafreebox.freebox.fr

{
    throttling: "slow"
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
        "is_scheduled": false,
        "throttling": "slow"
    }
}
```
