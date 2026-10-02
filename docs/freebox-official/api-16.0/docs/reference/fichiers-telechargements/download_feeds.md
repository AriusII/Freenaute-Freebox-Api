<a id="download-feeds"></a>

# Download Feeds

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#download-feeds)

## Navigation

- [Download Feed object](#download-feed-object)
- [Download Feed Errors](#download-feed-errors)
- [Download Feed API](#download-feed-api)
- [Download Feed Item object](#download-feed-item-object)


The Freebox downloader supports subscribing to RSS feeds, for
automatic content download.

<a id="download-feed-object"></a>

## Download Feed object

Download Feeds have the following attributes:

<a id="DownloadFeed"></a>

### Objet DownloadFeed

<a id="DownloadFeed.id"></a>

**`id int Read-only`**

id

<a id="DownloadFeed.status"></a>

**`status enum Read-only`**

The feed can have the following status

| Status | Description |
| --- | --- |
| ready | feed is up to date |
| fetching | feed is updating |
| error | there was an error trying to refresh this feed, see error |

<a id="DownloadFeed.url"></a>

**`url string Read-only`**

Feed URL

<a id="DownloadFeed.title"></a>

**`title string Read-only`**

Feed title (extracted from the RSS)

<a id="DownloadFeed.desc"></a>

**`desc string Read-only`**

Feed description (extracted from the RSS)

<a id="DownloadFeed.image_url"></a>

**`image_url string Read-only`**

Feed image URL (extracted from the RSS)

<a id="DownloadFeed.nb_read"></a>

**`nb_read int Read-only`**

Number of read items in the feed

<a id="DownloadFeed.nb_unread"></a>

**`nb_unread int Read-only`**

Number of unread items in the feed

<a id="DownloadFeed.auto_download"></a>

**`auto_download bool`**

If set to true, the downloader will automatically download new
items

<a id="DownloadFeed.fetch_ts"></a>

**`fetch_ts timestamp Read-only`**

Last time the feed was fetched

<a id="DownloadFeed.pub_ts"></a>

**`pub_ts timestamp Read-only`**

Last time the feed was published on remote server

<a id="DownloadFeed.error"></a>

**`error enum Read-only`**

Error code (same as used in [`Download`](download.md#Download "Download") or
[`DownloadFile`](download.md#DownloadFile "DownloadFile")).

<a id="download-feed-errors"></a>

## Download Feed Errors

When attempting to access the download feed API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| feed_not_found | No feed was found with the given id |
| item_not_found | No feed item was found with the given id |
| feed_is_recent | You are trying to update a feed that is already up to date |
| internal_error | Internal error |

<a id="download-feed-api"></a>

## Download Feed API

<a id="get-the-list-of-all-download-feeds"></a>

### Get the list of all download Feeds

<a id="get--api-v8-downloads-feeds-"></a>

**`GET /api/v8/downloads/feeds/`**

Returns the collection of all [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed") feeds

**Example request**:

```http
GET /api/v8/downloads/feeds/ HTTP/1.1
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
          "auto_download": false,
          "id": 1,
          "desc": "Custom RSS feed based off search filters.",
          "error": "none",
          "nb_read": 0,
          "title": "ezRSS - Search Results",
          "image_url": "http://ezrss.it/images/ezrssit.png",
          "status": "ready",
          "url": "http://www.ezrss.it/search/index.php?show_name=Ubuntu&mode=rss",
          "nb_unread": 29,
          "fetch_ts": 1349885023,
          "pub_ts": 1350583600
      },
      {
          "auto_download": false,
          "id": 2,
          "desc": "Latest nzb for Debian",
          "error": "none",
          "nb_read": 0,
          "title": "Debian NZB RSS",
          "image_url": "",
          "status": "ready",
          "url": "http://www.nzb-rss.com/rss/Debian.rss",
          "nb_unread": 13,
          "fetch_ts": 1350469391,
          "pub_ts": 1350583600
      }
   ]
}
```

<a id="get-a-download-feed"></a>

### Get a download Feed

<a id="get--api-v8-downloads-feeds-id"></a>

**`GET /api/v8/downloads/feeds/{id}`**

Gets the [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed") with the given id

**Example request**:

```http
GET /api/v8/downloads/feeds/2 HTTP/1.1
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
          "auto_download": false,
          "id": 2,
          "desc": "Latest nzb for Debian",
          "error": "none",
          "nb_read": 0,
          "title": "Debian NZB RSS",
          "image_url": "",
          "status": "ready",
          "url": "http://www.nzb-rss.com/rss/Debian.rss",
          "nb_unread": 13,
          "fetch_ts": 1350469391,
          "pub_ts": 1350583600
      }
}
```

<a id="add-a-download-feed"></a>

### Add a Download Feed

<a id="post--api-v8-downloads-feeds-"></a>

**`POST /api/v8/downloads/feeds/`**

Creates a new [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed").

**Example request**:

```http
POST /api/v8/downloads/feeds/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "url": "http://www.nzb-rss.com/rss/Debian-unstable.rss"
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
        "auto_download": false,
        "error": "none",
        "desc": "",
        "status": "ready",
        "nb_read": 0,
        "title": "",
        "image_url": "",
        "feed_id": 6,
        "url": "http://www.nzb-rss.com/rss/Debian-unstable.rss",
        "nb_unread": 0,
        "fetch_ts": 0,
        "pub_ts": 1350583600
    }

}
```

<a id="delete-download-feed"></a>

### Delete Download Feed

<a id="delete--api-v8-downloads-feeds-id"></a>

**`DELETE /api/v8/downloads/feeds/{id}`**

Deletes the [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed") and all the associated items.

This will not alter the [`Download`](download.md#Download "Download") tasks.

**Example request**:

```http
DELETE /api/v8/downloads/feeds/1 HTTP/1.1
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

<a id="update-a-download-feed"></a>

### Update a Download Feed

<a id="put--api-v8-downloads-feeds-id"></a>

**`PUT /api/v8/downloads/feeds/{id}`**

Updates the [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed") task with the given id

**Example request**:

```http
PUT /api/v8/downloads/feeds/2 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
    "auto_download": true
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
        "auto_download": true,
        "error": "none",
        "desc": "Latest nzb for Debian",
        "title": "Debian NZB RSS",
        "status": "ready",
        "nb_read": 0,
        "image_url": "",
        "feed_id": 2,
        "url": "http://www.nzb-rss.com/rss/Debian.rss",
        "nb_unread": 13,
        "fetch_ts": 1350583674,
        "pub_ts": 1350583600
    }
}
```

<a id="refresh-a-download-feed"></a>

### Refresh a Download Feed

<a id="post--api-v8-downloads-feeds-id-fetch"></a>

**`POST /api/v8/downloads/feeds/{id}/fetch`**

Remotely fetches the RSS feed and updates it.

Note that if the remote feed specifies a TTL, trying to update
before the ttl will result in feed_is_recent error

**Example request**:

```http
POST /api/v8/downloads/feeds/2/fetch HTTP/1.1
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

<a id="refresh-all-download-feeds"></a>

### Refresh all Download Feeds

<a id="post--api-v8-downloads-feeds-fetch"></a>

**`POST /api/v8/downloads/feeds/fetch`**

Remotely fetches all the RSS feeds.

**Example request**:

```http
POST /api/v8/downloads/feeds/fetch HTTP/1.1
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

<a id="download-feed-item-object"></a>

## Download Feed Item object

Each RSS [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed") contains feed items object

<a id="DownloadFeedItem"></a>

### Objet DownloadFeedItem

<a id="DownloadFeedItem.id"></a>

**`id int Read-only`**

id

<a id="DownloadFeedItem.feed_id"></a>

**`feed_id int Read-only`**

id of the [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed")

<a id="DownloadFeedItem.title"></a>

**`title string[ro]`**

item title

<a id="DownloadFeedItem.desc"></a>

**`desc string[ro]`**

item description

<a id="DownloadFeedItem.author"></a>

**`author string Read-only`**

item author

<a id="DownloadFeedItem.link"></a>

**`link string Read-only`**

URL of the RSS feed attachment

<a id="DownloadFeedItem.is_read"></a>

**`is_read bool`**

you can mark the item as read manually, or it is marked as read
automatically when the item is downloaded

<a id="DownloadFeedItem.is_downloaded"></a>

**`is_downloaded bool Read-only`**

mark downloaded items, automatically set to true when RSS item
is downloaded

<a id="DownloadFeedItem.fetch_ts"></a>

**`fetch_ts timestamp Read-only`**

timestamp of the item creation

<a id="DownloadFeedItem.pub_ts"></a>

**`pub_ts timestamp Read-only`**

item publish timestamp

<a id="DownloadFeedItem.enclosure_url"></a>

**`enclosure_url string Read-only`**

enclosure URL (if specified in RSS feed)

<a id="DownloadFeedItem.enclosure_type"></a>

**`enclosure_type string Read-only`**

enclosure mime type (if specified in RSS feed)

<a id="DownloadFeedItem.enclosure_length"></a>

**`enclosure_length int Read-only`**

enclosure size in bytes (if specified in RSS feed)

<a id="get-the-items-of-a-given-rss-feed"></a>

### Get the items of a given RSS feed

<a id="get--api-v8-downloads-feeds-feed_id-items-"></a>

**`GET /api/v8/downloads/feeds/{feed_id}/items/`**

Returns the collection of all `DownloadFeedItems` for
a given [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed")

**Example request**:

```http
GET /api/v8/downloads/feeds/2/items/ HTTP/1.1
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
       "pub_ts": 1350657300,
       "fetch_ts": 1350657317,
       "is_read": true,
       "title": "debian-6.0.4-amd64-CD-1.iso",
       "link": "http://bttracker.debian.org:6969/file/debian-6.0.4-amd64-CD-1.iso.torrent?info_hash=95ce23e889cc26901740f87ac25270da725bfd36",
       "id": 2845,
       "author": "debian",
       "feed_id": 2,
       "desc": ""
     },
     {
       "pub_ts": 1350657300,
       "fetch_ts": 1350657318,
       "is_read": false,
       "title": "debian-6.0.4-amd64-CD-2.iso",
       "link": "http://bttracker.debian.org:6969/file/debian-6.0.4-amd64-CD-2.iso.torrent?info_hash=34583a8e25ef1528a8bfce99d24f401acb24d982",
       "id": 2846,
       "author": "debian",
       "feed_id": 2,
       "desc": ""
     }
   ]
}
```

<a id="update-a-feed-item"></a>

### Update a feed item

<a id="put--api-v8-downloads-feeds-feed_id-items-item_id"></a>

**`PUT /api/v8/downloads/feeds/{feed_id}/items/{item_id}`**

Returns the collection of all `DownloadFeedItems` for
a given [`DownloadFeed`](download_feeds.md#DownloadFeed "DownloadFeed")

**Example request**:

```http
PUT /api/v8/downloads/feeds/2/items/2846 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
    "is_read": true
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

<a id="download-a-feed-item"></a>

### Download a feed item

<a id="post--api-v8-downloads-feeds-feed_id-items-item_id-download"></a>

**`POST /api/v8/downloads/feeds/{feed_id}/items/{item_id}/download`**

This method will enqueue the RSS item to the download list

**Example request**:

```http
POST /api/v8/downloads/feeds/2/items/2846/download HTTP/1.1
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

<a id="mark-all-items-as-read"></a>

### Mark all items as read

<a id="post--api-v8-downloads-feeds-feed_id-items-mark_all_as_read"></a>

**`POST /api/v8/downloads/feeds/{feed_id}/items/mark_all_as_read`**

This method will mark each items as read

**Example request**:

```http
POST /api/v8/downloads/feeds/2/items/mark_all_as_read HTTP/1.1
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
