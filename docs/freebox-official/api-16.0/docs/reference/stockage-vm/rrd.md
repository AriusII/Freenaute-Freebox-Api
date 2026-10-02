<a id="rrd-unstable"></a>

# RRD [UNSTABLE]

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#rrd-unstable)

## Navigation

- [RRD Fetch Object](#rrd-fetch-object)
- [Get RRD stats [UNSTABLE]](#get-rrd-stats-unstable)


With the rrd API you can retrieve stats collected on the Freebox.
Right now the stats available are: network stats, switch stats, dsl
stats, and temperature stats.

<a id="rrd-fetch-object"></a>

## RRD Fetch Object

This is the object used to get stats

<a id="RRDFetch"></a>

### Objet RRDFetch

<a id="RRDFetch.db"></a>

**`db enum`**

Name of the rrd database to read. It can take one of the
following values

| Db | Description |
| --- | --- |
| net | network stats |
| temp | temperature stats |
| dsl | xDSL stats |
| switch | switch stats |

<a id="RRDFetch.date_start"></a>

**`date_start int Optionnal`**

The requested start timestamp of the stats to get

NOTE: this can be adjusted to fit the best available resolution

<a id="RRDFetch.date_end"></a>

**`date_end int Optionnal`**

The requested end timestamp of the stats to get

NOTE: this can be adjusted to fit the best available resolution

<a id="RRDFetch.precision"></a>

**`precision int Optionnal`**

By default all values are cast to int, if you need floating
point precision you can provide a precision factor that will be
applied to all values before being returned.

For instance if you want 2 digit precision you should use a
precision of 100, and divide the obtained results by 100.

<a id="RRDFetch.fields"></a>

**`fields [] array of string Optionnal`**

If you are only interested in getting some fields you can
provide the list of fields you want to get.

<a id="rrd-net-db"></a>

For the net database the fields are:

| Field | Description |
| --- | --- |
| bw_up | upload available bandwidth (in byte/s) |
| bw_down | download available bandwidth (in byte/s) |
| rate_up | upload rate (in byte/s) |
| rate_down | download rate (in byte/s) |
| vpn_rate_up | vpn client upload rate (in byte/s) |
| vpn_rate_down | vpn client download rate (in byte/s) |

<a id="rrd-temp-db"></a>

For the temp database the fields are:

| Field | Description |
| --- | --- |
| cpum | temperature cpum (in °C) |
| cpub | temperature cpub (in °C) |
| sw | temperature sw (in °C) |
| hdd | temperature hdd (in °C) |
| fan_speed | fan rpm |

For the dsl database the fields are:

| Field | Description |
| --- | --- |
| rate_up | dsl available upload bandwidth (in byte/s) |
| rate_down | dsl available download bandwidth (in byte/s) |
| snr_up | dsl upload signal/noise ratio (in 1/10 dB) |
| snr_down | dsl download signal/noise ratio (in 1/10 dB) |

For the switch database the fields are:

| Field | Description |
| --- | --- |
| rx_1 | receive rate on port 1 (in byte/s) |
| tx_1 | transmit on port 1 (in byte/s) |
| rx_2 | receive rate on port 2 (in byte/s) |
| tx_2 | transmit on port 2 (in byte/s) |
| rx_3 | receive rate on port 3 (in byte/s) |
| tx_3 | transmit on port 3 (in byte/s) |
| rx_4 | receive rate on port 4 (in byte/s) |
| tx_4 | transmit on port 4 (in byte/s) |

<a id="get-rrd-stats-unstable"></a>

## Get RRD stats [UNSTABLE]

<a id="post--api-v8-rrd-"></a>

**`POST /api/v8/rrd/`**

**Example request**:

Exemple adapté : les champs dépréciés ont été retirés ou remplacés selon la documentation officielle ; les chemins HTTP restent ceux de la source.

```http
POST /api/v8/rrd/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "db": "temp",
   "fields": [ "cpum" ],
   "precision": 10
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

Exemple adapté : les champs dépréciés ont été retirés ou remplacés selon la documentation officielle ; les chemins HTTP restent ceux de la source.

```json
{
    "success": true,
    "result": {
        "date_start": 1353048060,
        "data": [
            {
                "cpum": 540,
                "time": 1353060840
            },
            {
                "cpum": 545,
                "time": 1353060900
            },

            [ ... ],

            {
                "cpum": 540,
                "time": 1353069600
            }
        ],
        "date_end": 1353069660
    }
}
```

<a id="get--api-v8-rrd-"></a>

**`GET /api/v8/rrd/`**

Same as post request, but allowed without ‘settings’ permission
