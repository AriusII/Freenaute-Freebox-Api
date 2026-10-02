<a id="wi-fi"></a>

# Wi-Fi

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#wi-fi)

## Navigation

- [Wi-Fi Errors](#wi-fi-errors)
- [Wi-Fi Global Config](#wi-fi-global-config)
- [Wi-Fi global config API](#wi-fi-global-config-api)
- [Wi-Fi Steering Config](#wi-fi-steering-config)
- [Wi-Fi steering config API](#wi-fi-steering-config-api)
- [Wi-Fi global state](#wi-fi-global-state)
- [Wi-Fi Access Point](#wi-fi-access-point)
- [Wi-Fi BSS](#wi-fi-bss)
- [Wi-Fi Radar](#wi-fi-radar)
- [Wi-Fi Planning](#wi-fi-planning)
- [Wi-Fi MAC Filter API](#wi-fi-mac-filter-api)
- [Wifi Config reset](#wifi-config-reset)
- [Diagnostic API](#diagnostic-api)
- [Wifi WPS API](#wifi-wps-api)
- [Wifi guest](#wifi-guest)
- [Temporary disabling Wifi](#temporary-disabling-wifi)
- [Multi Link Operation (MLO)](#multi-link-operation-mlo)


The Wi-Fi API allow you to control the settings of the Freebox Wi-Fi.

<a id="wi-fi-errors"></a>

## Wi-Fi Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| inval | invalid parameters |
| exist | entry already exists |
| nospc | maximum entry count reached |
| nodev | invalid device id |
| noent | invalid id |
| busy | device busy |
| inval_band | invalid wifi band |
| inval_ssid | invalid ssid |
| inval_freq | invalid wifi frequency |
| inval_cipher | invalid cipher mod |
| inval_key_len | invalid key length |
| inval_key | invalid key |
| inval_ht_needs_wmm | wmm must be enabled for 802.11n |
| inval_ac_needs_ht | invalid configuration 802.11ac need ht support |
| inval_ac_not_2d4g | invalid configuration 802.11ac is not supported on 2.4G band |
| inval_wps_needs_ccmp | wps need WPA2/AES to be enabled |
| inval_wps_macfilter | wps cannot work when mac filter is enabled |
| inval_wps_hidden_ssid | wps cannot work with hidden ssid |
| inval_eht_needs_he | 802.11ax must be enabled for 802.11be |
| inval_ht_needs_ht | 802.11n must be enabled for 802.11ax on 2.4G band |
| inval_ht_needs_vht | 802.11ac must be enabled for 802.11ax on 6G band |
| inval_6g_needs_he | 6G band requires 802.11ax |

<a id="wi-fi-global-config"></a>

## Wi-Fi Global Config

Global config gives quick access to major configuration settings (eg: toggle Wi-Fi)

WifiGlobalConfig has the following attributes:

<a id="WifiGlobalConfig"></a>

### Objet WifiGlobalConfig

<a id="WifiGlobalConfig.enabled"></a>

**`enabled bool`**

is wifi enabled

<a id="WifiGlobalConfig.mac_filter_state"></a>

**`mac_filter_state enum`**

| mac_filter_state | Description |
| --- | --- |
| disabled | mac filter is disabled |
| whitelist | mac filter is enabled, using a whitelist |
| blacklist | mac filter is enabled, using a blacklist |

<a id="wi-fi-global-config-api"></a>

## Wi-Fi global config API

<a id="get-the-current-wi-fi-global-configuration"></a>

### Get the current Wi-Fi global configuration

<a id="get--api-v9-wifi-config-"></a>

**`GET /api/v9/wifi/config/`**

Get the [`WifiGlobalConfig`](wifi.md#WifiGlobalConfig "WifiGlobalConfig")

**Example request**:

```http
GET /api/v9/wifi/config/ HTTP/1.1
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
        "enabled": true,
        "mac_filter_state": "blacklist"
    }
}
```

<a id="update-the-wi-fi-global-configuration"></a>

### Update the Wi-Fi global configuration

<a id="put--api-v9-wifi-config-"></a>

**`PUT /api/v9/wifi/config/`**

Update the [`WifiGlobalConfig`](wifi.md#WifiGlobalConfig "WifiGlobalConfig")

**Example request**:

```http
PUT /api/v9/wifi/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "enabled": false
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
        "enabled": false,
        "mac_filter_state": "blacklist"
    }
}
```

<a id="wi-fi-steering-config"></a>

## Wi-Fi Steering Config

WifiSteeringConfig has the following attributes:

<a id="WifiSteeringConfig"></a>

### Objet WifiSteeringConfig

<a id="WifiSteeringConfig.steering_level"></a>

**`steering_level int`**

Wi-Fi steering level.

| Value | Description |
| --- | --- |
| 0 | Wi-Fi steering is disabled |
| 1 | Devices are steered when they accept the change |
| 2 | Devices are steered more aggressively |

<a id="wi-fi-steering-config-api"></a>

## Wi-Fi steering config API

<a id="get-the-current-wi-fi-steering-configuration"></a>

### Get the current Wi-Fi steering configuration

<a id="get--api-v16-wifi-steering-config-"></a>

**`GET /api/v16/wifi/steering/config/`**

Get the [`WifiSteeringConfig`](wifi.md#WifiSteeringConfig "WifiSteeringConfig")

**Example request**:

```http
GET /api/v16/wifi/steering/config/ HTTP/1.1
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
        "steering_level": 2
    }
}
```

<a id="update-the-wi-fi-steering-configuration"></a>

### Update the Wi-Fi steering configuration

<a id="put--api-v16-wifi-steering-config-"></a>

**`PUT /api/v16/wifi/steering/config/`**

Update the [`WifiSteeringConfig`](wifi.md#WifiSteeringConfig "WifiSteeringConfig")

**Example request**:

```http
PUT /api/v16/wifi/steering/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "steering_level": 2
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
        "steering_level": 2
    }
}
```

<a id="wi-fi-global-state"></a>

## Wi-Fi global state

<a id="wi-fi-global-state-object"></a>

### Wi-Fi global state object

<a id="WifiGlobalState"></a>

#### Objet WifiGlobalState

<a id="WifiGlobalState.state"></a>

**`state enum Read-only`**

wifi global state

| state | Description |
| --- | --- |
| enabled | Wifi is enabled |
| disabled | Wi-Fi is disabled |
| disabled_planning | Wi-Fi is disabled by planning |

<a id="WifiGlobalState.expected_phys"></a>

**`expected_phys [] array of ExpectedPhy Read-only`**

expected wifi cards

<a id="ExpectedPhy"></a>

#### Objet ExpectedPhy

<a id="ExpectedPhy.band"></a>

**`band enum Read-only`**

| state | Description |
| --- | --- |
| 2d4g | 2.4GHz band |
| 5g | 5GHz band |
| 6g | 6 GHz band |
| 60g | 60GHz band |

<a id="ExpectedPhy.phy_id"></a>

**`phy_id int Read-only`**

id of the phy

<a id="ExpectedPhy.detected"></a>

**`detected bool Read-only`**

true if the wifi card is detected

<a id="wi-fi-global-state-api"></a>

### Wi-Fi global state API

<a id="get-the-global-wifi-state"></a>

#### Get the global wifi state

<a id="get--api-v10-wifi-state-"></a>

**`GET /api/v10/wifi/state/`**

Get the global wifi state [`WifiGlobalState`](wifi.md#WifiGlobalState "WifiGlobalState")

**Example request**:

```http
GET /api/v10/wifi/state/ HTTP/1.1
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
            "state": "enabled",
            "expected_phys": [
                {
                    "band": "2d4g",
                    "phy_id": 0,
                    "detected": true
                },
                {
                    "band": "5g",
                    "phy_id": 1,
                    "detected": true
                }
            ],
        }
    ]
}
```

<a id="wi-fi-access-point"></a>

## Wi-Fi Access Point

<a id="wi-fi-ap-objects"></a>

### Wi-Fi AP objects

The Freebox may have one or more access points, you can configure each access point with this api.

<a id="WifiAp"></a>

#### Objet WifiAp

<a id="WifiAp.id"></a>

**`id int Read-only`**

wifi ap id

<a id="WifiAp.name"></a>

**`name string Read-only`**

wifi ap name

<a id="WifiAp.status"></a>

**`status WifiApStatus Read-only`**

ap status

<a id="WifiAp.capabilities"></a>

**`capabilities WifiApCapabilities Read-only`**

ap capabilities

<a id="WifiAp.config"></a>

**`config WifiApConfig`**

ap configuration

<a id="WifiApStatus"></a>

#### Objet WifiApStatus

<a id="WifiApStatus.state"></a>

**`state enum Read-only`**

| state | Description |
| --- | --- |
| scanning | Ap is probing wifi channels |
| no_param | Ap is not configured |
| bad_param | Ap has an invalid configuration |
| disabled | Ap is permanently disabled |
| disabled_planning | Ap is currently disabled according to planning |
| disabled_power_saving | Ap is currently disabled according to power save |
| disabled_temp | Ap is currently disabled temporarily |
| no_active_bss | Ap has no active BSS |
| starting | Ap is starting |
| starting | Ap is stopping |
| acs | Ap is selecting the best available channel |
| ht_scan | Ap is scanning for other access point |
| dfs | Ap is performing dynamic frequency selection |
| active | Ap is active |
| failed | Ap has failed to start |

<a id="WifiApStatus.channel_width"></a>

**`channel_width int Read-only`**

effective channel width (in MHz)

<a id="WifiApStatus.primary_channel"></a>

**`primary_channel int Read-only`**

effective primary channel

<a id="WifiApStatus.secondary_channel"></a>

**`secondary_channel int Read-only`**

effective secondary channel

<a id="WifiApStatus.dfs_cac_remaining_time"></a>

**`dfs_cac_remaining_time int Read-only`**

time left in dfs state

<a id="WifiApStatus.dfs_disabled"></a>

**`dfs_disabled bool Read-only`**

Indicates if DFS channels are unavailable regardless of how the WifiApConfig is configured for this phy.
This is enabled when your freebox is in compatibility mode for other Freebox wifi products.

<a id="WifiApStatus.temp_disable_remaining_time"></a>

**`temp_disable_remaining_time int Read-only`**

Optional remaining time this access point is temporarily disabled.

<a id="WifiApCapabilities"></a>

#### Objet WifiApCapabilities

[UNSTABLE]

<a id="WifiApCapabilities.2d4g"></a>

**`2d4g int Read-only`**

map of capabilities in 2.4 GHz band

<a id="WifiApCapabilities.5g"></a>

**`5g int Read-only`**

map of capabilities in 5 GHz band

<a id="WifiApCapabilities.6g"></a>

**`6g int Read-only`**

map of capabilities in 6 GHz band

<a id="WifiApCapabilities.60g"></a>

**`60g int Read-only`**

map of capabilities in 60 GHz band

NOTE: before enabling some feature in ap config, you should ensure that AP supports the
feature using its provided capabilities.

<a id="WifiApHtConfig"></a>

#### Objet WifiApHtConfig

<a id="WifiApHtConfig.ac_enabled"></a>

**`ac_enabled bool`**

enable 802.11ac

<a id="WifiApHtConfig.ht_enabled"></a>

**`ht_enabled bool`**

enable 802.11n

[UNSTABLE]

<a id="WifiApHeConfig"></a>

#### Objet WifiApHeConfig

<a id="WifiApHeConfig.enabled"></a>

**`enabled bool`**

enable 802.11ax (HE)

[UNSTABLE]

<a id="WifiApConfig"></a>

#### Objet WifiApConfig

<a id="WifiApConfig.band"></a>

**`band enum`**

| band | Description |
| --- | --- |
| 2d4g | 2.4 GHz |
| 5g | 5 GHz |
| 6g | 6 GHz |
| 60g | 60 GHz |

<a id="WifiApConfig.channel_width"></a>

**`channel_width int`**

wanted channel width (in MHz) :

- 20 MHz
- 40 MHz
- 80 MHz
- 160 MHz

<a id="WifiApConfig.primary_channel"></a>

**`primary_channel int`**

wanted primary channel, value of 0 means automatic selection

<a id="WifiApConfig.secondary_channel"></a>

**`secondary_channel int`**

wanted secondary channel, value of 0 means automatic selection

<a id="WifiApConfig.dfs_enabled"></a>

**`dfs_enabled bool`**

enable channels that require DFS

<a id="WifiApConfig.ht"></a>

**`ht WifiApHtConfig`**

wifi ht config

<a id="WifiApConfig.he"></a>

**`he WifiApHeConfig`**

wifi HE config

<a id="WifiApChannelSurveyData"></a>

#### Objet WifiApChannelSurveyData

<a id="WifiApChannelSurveyData.timestamp"></a>

**`timestamp int`**

timestamp at which the survey data was retrieved

<a id="WifiApChannelSurveyData.busy_percent"></a>

**`busy_percent int`**

percentage of time the channel was sensed busy

<a id="WifiApChannelSurveyData.tx_percent"></a>

**`tx_percent int`**

percentage of time spent sending on the channel

<a id="WifiApChannelSurveyData.rx_percent"></a>

**`rx_percent int`**

percentage of time spent receiving Wi-Fi traffic on the channel

<a id="WifiApChannelSurveyData.rx_bss_percent"></a>

**`rx_bss_percent int`**

percentage of time spent receiving Wi-Fi traffic for a local BSS

<a id="wi-fi-ap-api"></a>

### Wi-Fi AP API

<a id="get-the-ap-list"></a>

#### Get the ap list

<a id="get--api-v9-wifi-ap-"></a>

**`GET /api/v9/wifi/ap/`**

Get the list of Freebox Access Points [`WifiAp`](wifi.md#WifiAp "WifiAp")

**Example request**:

```http
GET /api/v9/wifi/ap/ HTTP/1.1
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
            "capabilities": {
                "2d4g": {
                    "shortgi20": true,
                    "vht_rx_ldpc": false,

                     [ ... ]

                    "shortgi40": true,
                },
                "60g": {
                     [ ... ]
                },
                "5g": {
                     [ ... ]
                }
            },
            "name": "2.4G",
            "id": 0,
            "config": {
                "channel_width": "40",
                "ht": {
                    "ht_enabled": true,
                    "ac_enabled": false,

                    [ ... ]
                },
                "dfs_enabled": false,
                "band": "2d4g",
                "secondary_channel": 13,
                "primary_channel": 9
            },
            "status": {
                "channel_width": "20",
                "primary_channel": 9,
                "dfs_cac_remaining_time": 0,
                "secondary_channel": 0,
                "state": "active"
            }
        }
    ]
}
```

<a id="get-a-particular-ap"></a>

#### Get a particular AP

<a id="get--api-v9-wifi-ap-id"></a>

**`GET /api/v9/wifi/ap/{id}`**

Get the [`WifiAp`](wifi.md#WifiAp "WifiAp") with the requested id

**Example request**:

```http
GET /api/v9/wifi/ap/0 HTTP/1.1
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
         "capabilities": {
             "2d4g": {
                 "shortgi20": true,
                 "vht_rx_ldpc": false,

                  [ ... ]

                 "shortgi40": true,
             },
             "60g": {
                  [ ... ]
             },
             "5g": {
                  [ ... ]
             }
         },
         "name": "2.4G",
         "id": 0,
         "config": {
             "channel_width": "40",
             "ht": {
                 "ht_enabled": true,
                 "ac_enabled": false,

                 [ ... ]
             },
             "dfs_enabled": false,
             "band": "2d4g",
             "secondary_channel": 13,
             "primary_channel": 9
         },
         "status": {
             "channel_width": "20",
             "primary_channel": 9,
             "dfs_cac_remaining_time": 0,
             "secondary_channel": 0,
             "state": "active"
         }
     }
}
```

<a id="update-an-ap"></a>

#### Update an AP

<a id="put--api-v9-wifi-ap-id"></a>

**`PUT /api/v9/wifi/ap/{id}`**

Update the [`WifiAp`](wifi.md#WifiAp "WifiAp")

**Example request**:

```http
PUT /api/v9/wifi/ap/0 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "config": {
    "channel_width": "20",
    "ht": {
        "ht_enabled": false
    },
    "primary_channel": 0,
    "secondary_channel": 0
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
        "capabilities": [ ... ],
        "name": "2.4G",
        "id": 0,
        "config": {
            "channel_width": "20",
            "ht": {
                "ht_enabled": false,
                "ac_enabled": false

                [ ... ]
            },
            "dfs_enabled": false,
            "band": "2d4g",
            "secondary_channel": 0,
            "primary_channel": 0
        },
        "status": {
            "channel_width": "20",
            "primary_channel": 0,
            "dfs_cac_remaining_time": 0,
            "secondary_channel": 0,
            "state": "scanning"
        }
    }
}
```

<a id="wi-fi-ap-allowed-channels"></a>

### Wi-Fi AP allowed channels

To be able to allow user to pick a valid channel combination for a given AP you should use
the following api to retrieve the list of allowed channel combination.

<a id="WifiAllowedComb"></a>

#### Objet WifiAllowedComb

<a id="WifiAllowedComb.band"></a>

**`band enum Read-only`**

the band for which the combination can be used

| band | Description |
| --- | --- |
| 2d4g | 2.4 GHz |
| 5g | 5 GHz |
| 60g | 60 GHz |

<a id="WifiAllowedComb.channel_width"></a>

**`channel_width string Read-only`**

the channel_width for which the combination can be used

<a id="WifiAllowedComb.need_dfs"></a>

**`need_dfs bool Read-only`**

does this combination requires DFS.

You should only allow this combination if ap has allowed dfs.

<a id="WifiAllowedComb.dfs_cac_time"></a>

**`dfs_cac_time int Read-only`**

time required in dfs state before being able to start the AP.

<a id="WifiAllowedComb.psc"></a>

**`psc bool Read-only`**

is this using a PSC channel as primary.

Some phones/PCs can only see 6GHz APs when their primary channel is a
Preferred Scanning Channel (PSC).

<a id="WifiAllowedComb.primary"></a>

**`primary int Read-only`**

primary channel

<a id="WifiAllowedComb.secondary"></a>

**`secondary int Read-only`**

secondary channel (zero means that secondary channel will not be used)

<a id="get--api-v9-wifi-ap-id-allowed_channel_comb"></a>

**`GET /api/v9/wifi/ap/{id}/allowed_channel_comb`**

Get the [`WifiAllowedComb`](wifi.md#WifiAllowedComb "WifiAllowedComb") for the given ap id

**Example request**:

```http
GET /api/v9/wifi/ap/0/allowed_channel_comb HTTP/1.1
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
            "channel_width": "20",
            "dfs_cac_time": 0,
            "need_dfs": false,
            "primary": 1,
            "band": "2d4g",
            "secondary": 0
        },

        [ ... ]

        {
            "channel_width": "20",
            "dfs_cac_time": 0,
            "need_dfs": false,
            "primary": 13,
            "band": "2d4g",
            "secondary": 0
        },
        {
            "channel_width": "40",
            "dfs_cac_time": 0,
            "need_dfs": false,
            "primary": 1,
            "band": "2d4g",
            "secondary": 5
        },

        [ ... ]

        {
            "channel_width": "40",
            "dfs_cac_time": 0,
            "need_dfs": false,
            "primary": 13,
            "band": "2d4g",
            "secondary": 9
        }
    ]
}
```

<a id="wi-fi-ap-stations"></a>

### Wi-Fi AP stations

<a id="wi-fi-ap-stations-objects"></a>

#### Wi-Fi AP Stations objects

WifiStation has the following attributes:

<a id="WifiStation"></a>

##### Objet WifiStation

<a id="WifiStation.id"></a>

**`id string Read-only`**

station id

<a id="WifiStation.mac"></a>

**`mac string Read-only`**

client MAC address

<a id="WifiStation.bssid"></a>

**`bssid string Read-only`**

bssid on which the client is associated

<a id="WifiStation.hostname"></a>

**`hostname string Read-only`**

client host name

<a id="WifiStation.host"></a>

**`host LanHost Read-only`**

client host information

<a id="WifiStation.state"></a>

**`state enum Read-only`**

| state | Description |
| --- | --- |
| associated | station is associated |
| authenticated | station is authenticated |

<a id="WifiStation.inactive"></a>

**`inactive int Read-only`**

inactive duration (in seconds)

<a id="WifiStation.conn_duration"></a>

**`conn_duration int Read-only`**

connection duration (in seconds)

<a id="WifiStation.rx_bytes"></a>

**`rx_bytes int Read-only`**

received bytes (from station to Freebox)

<a id="WifiStation.tx_bytes"></a>

**`tx_bytes int Read-only`**

transmitted bytes (from Freebox to station)

<a id="WifiStation.tx_rate"></a>

**`tx_rate int Read-only`**

reception data rate (in bytes/s)

<a id="WifiStation.rx_rate"></a>

**`rx_rate int Read-only`**

transmission data rate (in bytes/s)

<a id="WifiStation.signal"></a>

**`signal int Read-only`**

signal attenuation (in dB)

<a id="WifiStation.flags"></a>

**`flags WifiStationFlags Read-only`**

station flags

<a id="WifiStation.last_rx"></a>

**`last_rx WifiStationStats Read-only`**

last rx stats

<a id="WifiStation.last_tx"></a>

**`last_tx WifiStationStats Read-only`**

last tx stats

<a id="WifiStationFlags"></a>

##### Objet WifiStationFlags

[UNSTABLE]

<a id="WifiStationFlags.legacy"></a>

**`legacy bool Read-only`**

does station uses legacy wifi (802.11a, 802.11b)

<a id="WifiStationFlags.ht"></a>

**`ht bool Read-only`**

does station support ht (802.11n)

<a id="WifiStationFlags.vht"></a>

**`vht bool Read-only`**

does station support vht (802.11ac)

<a id="WifiStationFlags.he"></a>

**`he bool Read-only`**

does station support he (802.11ax)

<a id="WifiStationFlags.authorized"></a>

**`authorized bool Read-only`**

is the station authenticated

<a id="WifiStationStats"></a>

##### Objet WifiStationStats

[UNSTABLE]

<a id="WifiStationStats.bitrate"></a>

**`bitrate int Read-only`**

physical link rate (in 1/10th of MBit/s), -1 if unknown

<a id="WifiStationStats.mcs"></a>

**`mcs int Read-only`**

current link mcs, -1 if not used

<a id="WifiStationStats.vht_mcs"></a>

**`vht_mcs int Read-only`**

current link vht mcs, -1 if not used

<a id="WifiStationStats.width"></a>

**`width string Read-only`**

current channel width

<a id="WifiStationStats.shortgi"></a>

**`shortgi bool Read-only`**

is shortgi enabled

<a id="get-wi-fi-stations-list"></a>

#### Get Wi-Fi Stations List

<a id="get--api-v9-wifi-ap-id-stations-"></a>

**`GET /api/v9/wifi/ap/{id}/stations/`**

Get the list of [`WifiStation`](wifi.md#WifiStation "WifiStation") associated to the AP

**Example request**:

```http
GET /api/v9/wifi/ap/0/stations/ HTTP/1.1
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
            "mac": "18:AF:36:15:69:42",
            "last_rx": {
                "bitrate": 110,
                "mcs": -1,
                "shortgi": false,
                "vht_mcs": -1,
                "width": "20"
            },
            "tx_bytes": 2651,
            "last_tx": {
                "bitrate": 360,
                "mcs": -1,
                "shortgi": false,
                "vht_mcs": -1,
                "width": "20"
            },
            "id": "00:24:D4:AC:DC:88-18:AF:36:15:69:42",
            "bssid": "00:24:D4:AC:DC:88",
            "flags": {
                "vht": false,
                "legacy": false,
                "authorized": true,
                "ht": false
            },
            "tx_rate": 0,
            "host": {
                [ ... ]
            },
            "inactive": 168,
            "conn_duration": 263,
            "hostname": "iPhone-de-r0ro",
            "state": "authenticated",
            "rx_bytes": 781,
            "rx_rate": 0,
            "signal": -38
        }
    ]
}
```

<a id="get-wi-fi-station"></a>

#### Get Wi-Fi Station

<a id="get--api-v9-wifi-ap-id-stations-mac"></a>

**`GET /api/v9/wifi/ap/{id}/stations/{mac}`**

Get a [`WifiStation`](wifi.md#WifiStation "WifiStation") associated to the AP

**Example request**:

```http
GET /api/v9/wifi/ap/0/stations/18:AF:36:15:69:42 HTTP/1.1
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
        "mac": "18:AF:36:15:69:42",
        "last_rx": {
            "bitrate": 110,
            "mcs": -1,
            "shortgi": false,
            "vht_mcs": -1,
            "width": "20"
        },
        "tx_bytes": 2651,
        "last_tx": {
            "bitrate": 360,
            "mcs": -1,
            "shortgi": false,
            "vht_mcs": -1,
            "width": "20"
        },
        "id": "00:24:D4:AC:DC:88-18:AF:36:15:69:42",
        "bssid": "00:24:D4:AC:DC:88",
        "flags": {
            "vht": false,
            "legacy": false,
            "authorized": true,
            "ht": false
        },
        "tx_rate": 0,
        "host": {
            [ ... ]
        },
        "inactive": 168,
        "conn_duration": 263,
        "hostname": "iPhone-de-r0ro",
        "state": "authenticated",
        "rx_bytes": 781,
        "rx_rate": 0,
        "signal": -38
    }
}
```

<a id="wi-fi-ap-channel-survey-history"></a>

### Wi-Fi AP channel survey history

Retrieve survey data for the channel the AP is operating on, starting from
a given timestamp.

<a id="get-survey-data-history"></a>

#### Get survey data history

<a id="get--api-v9-wifi-ap-id-channel_survey_history-timestamp"></a>

**`GET /api/v9/wifi/ap/{id}/channel_survey_history/{timestamp}`**

Get an array of [`WifiApChannelSurveyData`](wifi.md#WifiApChannelSurveyData "WifiApChannelSurveyData")

**Example request**:

```http
GET /api/v9/wifi/ap/0/channel_survey_history/1651135474000 HTTP/1.1
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
                  "busy_percent": 65,
                  "tx_percent": 2,
                  "timestamp": 1651135474996,
                  "rx_bss_percent": 0,
                  "rx_percent": 56
            },
            {
                  "busy_percent": 70,
                  "tx_percent": 3,
                  "timestamp": 1651135475796,
                  "rx_bss_percent": 0,
                  "rx_percent": 58
            },
            {
                  "busy_percent": 71,
                  "tx_percent": 3,
                  "timestamp": 1651135475896,
                  "rx_bss_percent": 0,
                  "rx_percent": 58
            },
            {
                  "busy_percent": 73,
                  "tx_percent": 4,
                  "timestamp": 1651135475998,
                  "rx_bss_percent": 0,
                  "rx_percent": 59
            }
      ]
}
```

<a id="restart-an-ap"></a>

### Restart an AP

**WARNING** during the restart the AP will be unavailable.
You may not receive the response if you restart the Wifi card you are using to call the api

This will restart an AP, this is useful when an AP is in failed state.
This is the same as disabling/re-enabling the BSS on an AP.

<a id="post--api-v9-wifi-ap-id-restart"></a>

**`POST /api/v9/wifi/ap/{id}/restart`**

Restarts the AP

**Example request**:

```http
POST /api/v9/wifi/ap/0/restart HTTP/1.1
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

<a id="wi-fi-bss"></a>

## Wi-Fi BSS

Each AP can manage a set of BSS, with this api you can manage BSS settings

<a id="wi-fi-bss-objects"></a>

### Wi-Fi BSS objects

<a id="WifiBss"></a>

#### Objet WifiBss

<a id="WifiBss.id"></a>

**`id int Read-only`**

bss id

<a id="WifiBss.phy_id"></a>

**`phy_id string Read-only`**

associated AP id

<a id="WifiBss.status"></a>

**`status WifiBssStatus Read-only`**

bss status

<a id="WifiBss.use_shared_params"></a>

**`use_shared_params bool`**

if set to True the bss will use the shared parameters
stored under shared_bss_params

if not the bss will use a configuration specific to this bss
stored under bss_params

when you want to edit the bss config you should change the config
values using values from bss_params or shared_bss_params as a source
and update use_shared_params accordingly.

<a id="WifiBss.config"></a>

**`config WifiBssConfig`**

bss configuration (use this field for editing)

<a id="WifiBss.bss_params"></a>

**`bss_params WifiBssConfig Read-only`**

current configuration specific to this bss

<a id="WifiBss.shared_bss_params"></a>

**`shared_bss_params WifiBssConfig Read-only`**

current configuration for shared bss config

<a id="WifiBss.disable_wep"></a>

**`disable_wep bool Read-only`**

Whether or not this BSS can work with wep encryption or not

<a id="WifiBssStatus"></a>

#### Objet WifiBssStatus

<a id="WifiBssStatus.state"></a>

**`state enum Read-only`**

| state | Description |
| --- | --- |
| phy_stopped | associated AP is stopped |
| no_param | bss is missing config |
| bad_param | bss has an invalid config |
| disabled | bss is disabled |
| temp_disabled | bss has been temporary disabled |
| starting | bss is starting |
| active | bss is active |
| failed | bss has failed to start |

<a id="WifiBssStatus.sta_count"></a>

**`sta_count int Read-only`**

number of stations for this bss

<a id="WifiBssStatus.authorized_sta_count"></a>

**`authorized_sta_count int Read-only`**

number of authenticated stations for this bss

<a id="WifiBssStatus.custom_key_ssid"></a>

**`custom_key_ssid string Read-only`**

SSID to use with custom keys

<a id="WifiBssStatus.partners"></a>

**`partners [int] Read-only`**

The currently active MLO partners’s AP for this BSS. Can be empty if MLO
is disabled. See the MLO chapter for more info

<a id="WifiBssConfig"></a>

#### Objet WifiBssConfig

<a id="WifiBssConfig.enabled"></a>

**`enabled bool`**

enable this BSS. Note that if you want the AP to completely stop emitting wifi
you should use [`WifiGlobalConfig`](wifi.md#WifiGlobalConfig "WifiGlobalConfig") enabled attribute.

<a id="WifiBssConfig.ssid"></a>

**`ssid str`**

bss displayed name

<a id="WifiBssConfig.hide_ssid"></a>

**`hide_ssid str`**

don’t show bss in bss list

<a id="WifiBssConfig.gcmp256"></a>

**`gcmp256 str`**

Whether or not to use GCMP-256 (only in WPA3 & for box that supports 802.11-be)

<a id="WifiBssConfig.encryption"></a>

**`encryption enum`**

| encryption | Description |
| --- | --- |
| wep | wep (should not use) |
| wpa_psk_auto | wpa1 CCMP+TKIP (should not use) |
| wpa_psk_tkip | wpa1 TKIP (should not use) |
| wpa_psk_ccmp | wpa1 CCMP (should not use) |
| wpa12_psk_auto | wpa1+wpa2 CCMP+TKIP (should not use) |
| wpa2_psk_auto | wpa2 CCMP+TKIP (should not use) |
| wpa2_psk_tkip | wpa2 TKIP (should not use) |
| wpa2_psk_ccmp | wpa2 CCMP |
| wpa23_psk_ccmp | wpa2+wpa3 CCMP WPA3-personal transition mode |
| wpa23_psk_ccmp_mrsno | wpa2+wpa3 CCMP WPA3-personal compatibility mode |
| wpa3_psk_ccmp | wpa3 CCMP WPA3-personal only mode |

<a id="WifiBssConfig.key"></a>

**`key string`**

wifi key
“**\*\*\*\***” will be returned when insufficient permission

<a id="WifiBssConfig.eapol_version"></a>

**`eapol_version int Read-only`**

eapol version

<a id="wi-fi-bss-api"></a>

### Wi-Fi BSS API

<a id="get-the-bss-list"></a>

#### Get the bss list

<a id="get--api-v9-wifi-bss-"></a>

**`GET /api/v9/wifi/bss/`**

Get the list of Freebox Access Points [`WifiBss`](wifi.md#WifiBss "WifiBss")

**Example request**:

```http
GET /api/v9/wifi/bss/ HTTP/1.1
Host: mafreebox.freebox.fr
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
    "result": [
        {
            "id": "00:24:D4:AA:BB:CC",
            "phy_id": 0,
            "use_shared_params": false,
            "config": {
                  "enabled": true,
                  "ssid": "r0ro 2.4",
                  "encryption": "wpa2_psk_ccmp",
                  "hide_ssid": false,
                  "eapol_version": 2,
                  "wps_enabled": true,
                  "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
                  "key": "jesaispasdevine!"
            },
            "bss_params": {
                  "enabled": true,
                  "ssid": "r0ro 2.4",
                  "encryption": "wpa2_psk_ccmp",
                  "hide_ssid": false,
                  "eapol_version": 2,
                  "wps_enabled": true,
                  "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
                  "key": "jesaispasdevine!"
            },
            "shared_bss_params": {
                  "enabled": true,
                  "ssid": "r0ro",
                  "encryption": "wpa2_psk_ccmp",
                  "hide_ssid": false,
                  "eapol_version": 2,
                  "wps_enabled": true,
                  "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
                  "key": "lav7lav7!"
            },
            "status": {
                "state": "active",
                "sta_count": 1,
                "authorized_sta_count": 1
            }
        },

        [ ... ]
    ]
}
```

<a id="get-a-particular-bss"></a>

#### Get a particular BSS

<a id="get--api-v9-wifi-bss-id"></a>

**`GET /api/v9/wifi/bss/{id}`**

Get the [`WifiBss`](wifi.md#WifiBss "WifiBss") with the requested id

**Example request**:

```http
GET /api/v9/wifi/bss/00:24:D4:AA:BB:CC HTTP/1.1
Host: mafreebox.freebox.fr
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
      "id": "00:24:D4:AA:BB:CC",
      "phy_id": 0,
      "use_shared_params": false,
      "config": {
            "enabled": true,
            "ssid": "r0ro 2.4",
            "encryption": "wpa2_psk_ccmp",
            "hide_ssid": false,
            "eapol_version": 2,
            "wps_enabled": true,
            "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
            "key": "jesaispasdevine!"
      },
      "bss_params": {
            "enabled": true,
            "ssid": "r0ro 2.4",
            "encryption": "wpa2_psk_ccmp",
            "hide_ssid": false,
            "eapol_version": 2,
            "wps_enabled": true,
            "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
            "key": "jesaispasdevine!"
      },
      "shared_bss_params": {
            "enabled": true,
            "ssid": "r0ro",
            "encryption": "wpa2_psk_ccmp",
            "hide_ssid": false,
            "eapol_version": 2,
            "wps_enabled": true,
            "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
            "key": "lav7lav7!"
      },
      "status": {
          "state": "active",
          "sta_count": 1,
          "authorized_sta_count": 1
      }
    }
}
```

<a id="update-an-bss"></a>

#### Update an BSS

<a id="put--api-v9-wifi-bss-id"></a>

**`PUT /api/v9/wifi/bss/{id}`**

Update the [`WifiAp`](wifi.md#WifiAp "WifiAp")

**Example request**:

```http
PUT /api/v9/wifi//bss/00:24:D4:AA:BB:CC HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "config": {
    "key": "c'était trop facile"
  }
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
      "id": "00:24:D4:AA:BB:CC",
      "phy_id": 0,
      "use_shared_params": false,
      "config": {
            "enabled": true,
            "ssid": "r0ro 2.4",
            "encryption": "wpa2_psk_ccmp",
            "hide_ssid": false,
            "eapol_version": 2,
            "wps_enabled": true,
            "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
            "key": "jesaispasdevine!"
      },
      "bss_params": {
            "enabled": true,
            "ssid": "r0ro 2.4",
            "encryption": "wpa2_psk_ccmp",
            "hide_ssid": false,
            "eapol_version": 2,
            "wps_enabled": true,
            "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
            "key": "c'était trop facile"
      },
      "shared_bss_params": {
            "enabled": true,
            "ssid": "r0ro",
            "encryption": "wpa2_psk_ccmp",
            "hide_ssid": false,
            "eapol_version": 2,
            "wps_enabled": true,
            "wps_uuid": "37f5c24a-4d8f-4dfc-9321-c40c42e588c0",
            "key": "lav7lav7!"
      },
      "status": {
          "state": "active",
          "sta_count": 1,
          "authorized_sta_count": 1
      }
    }
}
```

<a id="wi-fi-radar"></a>

## Wi-Fi Radar

With this api you can list the surrounding Wi-Fi access points, and Wi-fi channel usage.

This a new feature introduced in firmware 2.1.0 (api v2).

A scan is automatically done at AP startup, if you need to refresh the information you can use the scan api

<a id="wi-fi-neighbor-object"></a>

### Wi-Fi Neighbor Object

WifiNeighbor has the following attributes:

<a id="WifiNeighbor"></a>

#### Objet WifiNeighbor

<a id="WifiNeighbor.bssid"></a>

**`bssid string Read-only`**

neighbor bssid

<a id="WifiNeighbor.ssid"></a>

**`ssid string Read-only`**

neighbor ssid

<a id="WifiNeighbor.band"></a>

**`band enum Read-only`**

the band for which the combination can be used

| band | Description |
| --- | --- |
| 2d4g | 2.4 GHz |
| 5g | 5 GHz |
| 60g | 60 GHz |

<a id="WifiNeighbor.channel_width"></a>

**`channel_width int Read-only`**

neighbor channel_width

<a id="WifiNeighbor.channel"></a>

**`channel int Read-only`**

neighbor primary channel

<a id="WifiNeighbor.secondary_channel"></a>

**`secondary_channel int Read-only`**

neighbor secondary channel (0 for unused)

<a id="WifiNeighbor.signal"></a>

**`signal int Read-only`**

signal attenuation in dB

<a id="WifiNeighbor.capabilities"></a>

**`capabilities WifiNeighborCap Read-only`**

neighbor capabilities

<a id="WifiNeighborCap"></a>

#### Objet WifiNeighborCap

<a id="WifiNeighborCap.legacy"></a>

**`legacy bool Read-only`**

neighbor uses legacy wifi (802.11a, 802.11b)

<a id="WifiNeighborCap.ht"></a>

**`ht bool Read-only`**

neighbor supports ht (802.11n)

<a id="WifiNeighborCap.vht"></a>

**`vht bool Read-only`**

neighbor supports vht (802.11ac)

<a id="list-ap-neighbors"></a>

### List AP neighbors

<a id="get--api-v9-wifi-ap-id-neighbors-"></a>

**`GET /api/v9/wifi/ap/{id}/neighbors/`**

Get the list of [`WifiNeighbor`](wifi.md#WifiNeighbor "WifiNeighbor") seen by the AP

**Example request**:

```http
GET /api/v9/wifi/ap/0/neighbors/ HTTP/1.1
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
            "channel_width": "20",
            "capabilities": {
                "legacy": false,
                "vht": false,
                "ht": true
            },
            "ssid": "Freebox-future",
            "channel": 1,
            "band": "2d4g",
            "bssid": "00:24:D4:BA:BB:EE",
            "secondary_channel": 0,
            "signal": -27
        },

        [ ... ]

        {
            "channel_width": "20",
            "capabilities": {
                "legacy": false,
                "vht": false,
                "ht": true
            },
            "ssid": "Encore une freebox",
            "channel": 1,
            "band": "2d4g",
            "bssid": "F4:CA:E5:5E:AC:4F",
            "secondary_channel": 0,
            "signal": -33
        },
        {
            "channel_width": "20",
            "capabilities": {
                "legacy": false,
                "vht": false,
                "ht": true
            },
            "ssid": "lav6-140c76670212",
            "channel": 1,
            "band": "2d4g",
            "bssid": "00:07:CB:00:00:FD",
            "secondary_channel": 0,
            "signal": -33
        }
    ]
}
```

<a id="wi-fi-channel-usage-object"></a>

### Wi-Fi Channel usage Object

<a id="WifiChannelUsage"></a>

#### Objet WifiChannelUsage

<a id="WifiChannelUsage.channel"></a>

**`channel int Read-only`**

channel number

<a id="WifiChannelUsage.band"></a>

**`band enum Read-only`**

| band | Description |
| --- | --- |
| 2d4g | 2.4 GHz |
| 5g | 5 GHz |
| 60g | 60 GHz |

<a id="WifiChannelUsage.noise_level"></a>

**`noise_level int Read-only`**

noise level on channel in dB

<a id="WifiChannelUsage.rx_busy_percent"></a>

**`rx_busy_percent int Read-only`**

rx channel busy time percentage

<a id="list-wi-fi-channels-usage"></a>

### List Wi-Fi channels usage

<a id="get--api-v9-wifi-ap-id-channel_usage-"></a>

**`GET /api/v9/wifi/ap/{id}/channel_usage/`**

Get the list of [`WifiChannelUsage`](wifi.md#WifiChannelUsage "WifiChannelUsage") for the given AP

**Example request**:

```http
GET /api/v9/wifi/ap/0/channel_usage/ HTTP/1.1
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
    "result": "result": [
       {
           "band": "2d4g",
           "noise_level": -66,
           "rx_busy_percent": 35,
           "channel": 1
       },

       [ ... ]

       {
           "band": "2d4g",
           "noise_level": -58,
           "rx_busy_percent": 46,
           "channel": 13
       }
   ]
}
```

<a id="refresh-radar-informations"></a>

### Refresh radar informations

**WARNING** during the scan the AP will be unavailable. Therefore, you should ask for
user confirmation prior to launching a scan.

Once launched you should wait until the ap state comes back from scanning to get updated info.

<a id="post--api-v9-wifi-ap-id-neighbors-scan"></a>

**`POST /api/v9/wifi/ap/{id}/neighbors/scan`**

Launch a wifi scan on given ap

**Example request**:

```http
POST /api/v9/wifi/ap/0/neighbors/scan HTTP/1.1
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

<a id="wi-fi-planning"></a>

## Wi-Fi Planning

With api v2 you can now specify time range when you want to enable your wifi.

<a id="wi-fi-planning-object"></a>

### Wi-Fi Planning Object

<a id="WifiPlanning"></a>

#### Objet WifiPlanning

<a id="WifiPlanning.use_planning"></a>

**`use_planning bool`**

is the planning enabled

<a id="WifiPlanning.resolution"></a>

**`resolution int Read-only`**

planning resolution (number of slots per day)

<a id="WifiPlanning.mapping"></a>

**`mapping [] array of str`**

mapping for planning : “on” or “off”
mapping[0] is monday at 0:0
mapping[7 \* resolution - 1] is sunday last slot

(each slot has a duration of 60 \* 24 / resolution minutes)

<a id="get-wi-fi-planning"></a>

### Get Wi-Fi Planning

<a id="get--api-v9-wifi-planning-"></a>

**`GET /api/v9/wifi/planning/`**

Get the current [`WifiPlanning`](wifi.md#WifiPlanning "WifiPlanning")

**Example request**:

```http
GET /api/v9/wifi/planning/ HTTP/1.1
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
        "use_planning": false,
        "resolution": 48,
        "mapping": [
            "on",
            "on",
            "on",
            "on",

            [ ... ]

            "on",
            "on",
            "on",
            "on"
        ]
    }
}
```

<a id="update-wi-fi-planning"></a>

### Update Wi-Fi Planning

<a id="put--api-v9-wifi-planning-"></a>

**`PUT /api/v9/wifi/planning/`**

Update the [`WifiPlanning`](wifi.md#WifiPlanning "WifiPlanning")

**Example request**:

```http
PUT /api/v9/wifi/planning/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "use_planning": true
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
        "use_planning": true,
        "resolution": 48,
        "mapping": [
            "on",
            "on",
            "on",
            "on",

            [ ... ]

            "on",
            "on",
            "on",
            "on"
        ]
    }
}
```

<a id="wi-fi-mac-filter-api"></a>

## Wi-Fi MAC Filter API

<a id="wi-fi-mac-filter-object"></a>

### Wi-Fi MAC Filter object

WifiMacFilter has the following attributes:

<a id="WifiMacFilter"></a>

#### Objet WifiMacFilter

<a id="WifiMacFilter.id"></a>

**`id string Read-only`**

filter id

<a id="WifiMacFilter.mac"></a>

**`mac string Read-only`**

MAC address to filter

<a id="WifiMacFilter.comment"></a>

**`comment string`**

comment

<a id="WifiMacFilter.type"></a>

**`type enum`**

| type | Description |
| --- | --- |
| whitelist | if mac_filter is set to whitelist this station will be allowed |
| blacklist | if mac_filter is set to blacklist this station will be rejected |

<a id="WifiMacFilter.hostname"></a>

**`hostname string Read-only`**

host name when available

<a id="WifiMacFilter.host"></a>

**`host LanHost Read-only`**

host information when available

<a id="get-the-mac-filter-list"></a>

### Get the MAC filter list

<a id="get--api-v9-wifi-mac_filter-"></a>

**`GET /api/v9/wifi/mac_filter/`**

Get the list of [`WifiMacFilter`](wifi.md#WifiMacFilter "WifiMacFilter")

**Example request**:

```http
GET /api/v9/wifi/mac_filter/ HTTP/1.1
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
            "mac": "00:07:CB:01:02:03",
            "type": "whitelist",
            "comment": "test",
            "hostname": "00:07:CB:01:02:03",
            "id": "00:07:CB:01:02:03"
        },
        {
            "mac": "00:24:D4:00:00:69",
            "type": "blacklist",
            "comment": "plop",
            "hostname": "r0ro's iPad",
            "id": "00:24:D4:00:00:69",
            "host": {
               [ ... ]
            }
        }
    ]
}
```

<a id="getting-a-particular-mac-filter"></a>

### Getting a particular MAC filter

<a id="get--api-v9-wifi-mac_filter-filter_id"></a>

**`GET /api/v9/wifi/mac_filter/{filter_id}`**

Returns the requested [`WifiMacFilter`](wifi.md#WifiMacFilter "WifiMacFilter") properties

**Example request**:

```http
GET /api/v9/wifi/mac_filter/00:07:CB:01:02:03 HTTP/1.1
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
        "mac": "00:07:CB:01:02:03",
        "type": "whitelist",
        "comment": "test",
        "hostname": "00:07:CB:01:02:03",
        "id": "00:07:CB:01:02:03"
    }
}
```

<a id="updating-a-mac-filter"></a>

### Updating a MAC filter

<a id="put--api-v9-wifi-mac_filter-filter_id"></a>

**`PUT /api/v9/wifi/mac_filter/{filter_id}`**

Update a [`WifiMacFilter`](wifi.md#WifiMacFilter "WifiMacFilter") properties

**Example request**:

```http
PUT /api/v9/wifi/mac_filter/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "comment": "filtre de test",
   "type": "blacklist"
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
        "mac": "00:07:CB:01:02:03",
        "type": "blacklist",
        "comment": "filtre de test",
        "hostname": "00:07:CB:01:02:03",
        "id": "00:07:CB:01:02:03"
    }
}
```

<a id="delete-a-mac-filter"></a>

### Delete a MAC filter

<a id="delete--api-v9-wifi-mac_filter-filter_id"></a>

**`DELETE /api/v9/wifi/mac_filter/{filter_id}`**

Delete the [`WifiMacFilter`](wifi.md#WifiMacFilter "WifiMacFilter") with the given id

**Example request**:

```http
DELETE /api/v9/wifi/mac_filter/00:07:CB:01:02:03 HTTP/1.1
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

<a id="create-a-new-mac-filter"></a>

### Create a new MAC filter

<a id="post--api-v9-wifi-mac_filter-"></a>

**`POST /api/v9/wifi/mac_filter/`**

Crate a new the [`WifiMacFilter`](wifi.md#WifiMacFilter "WifiMacFilter")

**Example request**:

```http
POST /api/v9/wifi/mac_filter/00:07:CB:01:02:03 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "comment": "filtre de test",
   "type": "blacklist",
   "mac": "00:07:CB:CB:07:00"
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
        "mac": "00:07:CB:CB:07:00",
        "type": "blacklist",
        "comment": "filtre de test",
        "hostname": "00:07:CB:CB:07:00",
        "id": "00:07:CB:CB:07:00"
    }
}
```

<a id="wifi-config-reset"></a>

## Wifi Config reset

<a id="global-reset"></a>

### Global reset

You can reset Wifi to default configuration with this api

<a id="post--api-v9-wifi-config-reset-"></a>

**`POST /api/v9/wifi/config/reset/`**

Create a new the [`WifiMacFilter`](wifi.md#WifiMacFilter "WifiMacFilter")

**Example request**:

```http
POST /api/v9/wifi/config/reset/ HTTP/1.1
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

<a id="config-reset-value-of-an-ap"></a>

### Config reset value of an AP

You can get the default config value of a given AP.

<a id="get--api-v9-wifi-ap-id-default"></a>

**`GET /api/v9/wifi/ap/{id}/default`**

Get the [`WifiApConfig`](wifi.md#WifiApConfig "WifiApConfig") with the requested id

**Example request**:

```http
GET /api/v9/wifi/ap/0/default HTTP/1.1
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
    "channel_width": "20",
    "ht": {
      [ ... ]
    },
    "dfs_enabled": false,
    "band": "2d4g",
    "secondary_channel": 0,
    "primary_channel": 0
  }
}
```

<a id="config-reset-value-of-a-bss"></a>

### Config reset value of a BSS

You can get the default config value for a given BSS.

<a id="get--api-v9-wifi-bss-id-default"></a>

**`GET /api/v9/wifi/bss/{id}/default`**

Get the [`WifiBssConfig`](wifi.md#WifiBssConfig "WifiBssConfig") with the requested bssid

**Example request**:

```http
GET /api/v9/wifi/bss/02:00:00:00:00:00/default HTTP/1.1
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
    "enabled": true,
    "wps_uuid": "7ace9cb4-3aec-486e-b487-28df4998ff46",
    "ssid": "super_ssid",
    "encryption": "wpa2_psk_ccmp",
    "wps_enabled": true,
    "hide_ssid": false,
    "eapol_version": 2,
    "key": "motdepasse"
  }
}
```

<a id="config-reset-value-bulk"></a>

### Config reset value (bulk)

This api gets the same data as the per AP/BSS ones but in one call only

<a id="get--api-v9-wifi-default"></a>

**`GET /api/v9/wifi/default`**

Get the [`WifiBssConfig`](wifi.md#WifiBssConfig "WifiBssConfig") or [`WifiApConfig`](wifi.md#WifiApConfig "WifiApConfig") of all cards

**Example request**:

```http
GET /api/v9/wifi/default HTTP/1.1
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
    "aps": [
      {
        "params": {
          "channel_width": "20",
          "ht": { ... },
          "dfs_enabled": false,
          "band": "2d4g",
          "secondary_channel": 0,
          "primary_channel": 0
        },
        "ap_id": 0
      },
      {
        "params": {
          "channel_width": "80",
          "ht": { ... },
          "dfs_enabled": true,
          "band": "5g",
          "secondary_channel": 0,
          "primary_channel": 0
        },
        "ap_id": 1
      }
    ],
    "bsss": [
      {
        "params": {
          "enabled": true,
          "wps_uuid": "cbf5826c-25b2-4795-a7c7-cbd8f9454431",
          "ssid": "super_ssid",
          "encryption": "wpa2_psk_ccmp",
          "wps_enabled": true,
          "hide_ssid": false,
          "eapol_version": 2,
          "key": "lolzme"
        },
        "bssid": "00:00:00:00:00:08"
      },
      {
        "params": {
          "enabled": true,
          "wps_uuid": "1d77f4c0-9544-4478-a8f0-cccb77031b94",
          "ssid": "super_ssid",
          "encryption": "wpa2_psk_ccmp",
          "wps_enabled": true,
          "hide_ssid": false,
          "eapol_version": 2,
          "key": "lolzme"
        },
        "bssid": "00:00:00:00:00:0C"
      }
    ]
  }
}
```

<a id="diagnostic-api"></a>

## Diagnostic API

This API is intended to simplify detecting problems or suboptimal configs on
bsss or aps. This API is articulated around the WifiDiagItem

<a id="WifiDiagItem"></a>

### Objet WifiDiagItem

<a id="WifiDiagItem.ap_id"></a>

**`ap_id int`**

When this item relates to an AP, this indicates the AP’s index
When this item relates to a BSS, this field is unset

<a id="WifiDiagItem.bssid"></a>

**`bssid str`**

When this item relates to a BSS, this field indicates the bss’s id
When this item relates to an AP, this field is unset

<a id="WifiDiagItem.code"></a>

**`code enum`**

The code identifying which param is faulty/suboptimal

| Code | Description |
| --- | --- |
| all | This is a the same as doing a full reset of this AP/BSS |
| network_disabled | This changes the ‘enabled’ field in [`WifiBssConfig`](wifi.md#WifiBssConfig "WifiBssConfig") |
| network_security | This changes the ‘encryption’ field in [`WifiBssConfig`](wifi.md#WifiBssConfig "WifiBssConfig") |
| network_visibility | This changes the ‘hide_ssid’ field in [`WifiBssConfig`](wifi.md#WifiBssConfig "WifiBssConfig") |
| channel_width | This changes the ‘channel_width’ field in [`WifiApConfig`](wifi.md#WifiApConfig "WifiApConfig") |
| channel_value | This changes the ‘channel’ & ‘secondary_channel’ fields in [`WifiApConfig`](wifi.md#WifiApConfig "WifiApConfig") |

<a id="WifiDiagItem.severity"></a>

**`severity enum`**

| Severity | Description |
| --- | --- |
| minor | minor problems don’t have performance/compatibility implications |
| major | major problems do |

<a id="global-diagnostic"></a>

### Global diagnostic

The global diagnostics evaluates/works on all AP/BSS at once.
This is good for bulk access

<a id="get--api-v9-wifi-diag"></a>

**`GET /api/v9/wifi/diag`**

Get the [`WifiDiagItem`](wifi.md#WifiDiagItem "WifiDiagItem") for the box

**Example request**:

```http
GET /api/v9/wifi/diag HTTP/1.1
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
    "aps": [
      {
        "severity": "minor",
        "ap_id": 0,
        "code": "channel_width"
      },
      {
        "severity": "major",
        "ap_id": 1,
        "code": "channel_value"
      }
    ],
    "bsss": [
      {
        "severity": "major",
        "bssid": "02:00:00:00:00:08",
        "code": "network_security"
      },
      {
        "severity": "major",
        "bssid": "02:00:00:00:00:0C",
        "code": "network_visibility"
      }
    ]
  }
}
```

<a id="post--api-v9-wifi-diag"></a>

**`POST /api/v9/wifi/diag`**

Fix a few of the [`WifiDiagItem`](wifi.md#WifiDiagItem "WifiDiagItem") at once.
‘aps’ & ‘bsss’ are arrays in which you can put any items.
You can also omit ‘aps’ and/or ‘bsss’

**Example request**:

```http
POST /api/v9/wifi/diag HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "aps": [
    {
      "ap_id": 0,
      "code": "channel_width"
    },
    [ ... ]
  ],
  "bsss": [
    {
      "bssid": "02:00:00:00:00:08",
      "code": "all"
    },
    [ ... ]
  ]
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

<a id="per-ap-bss-diagnostic"></a>

### Per AP/BSS diagnostic

Same as the global API there also is a per AP/BSS api to get/fix the problems.

<a id="get--api-v9-wifi-ap-id-diag &amp; -api-v9-wifi-bss-id-diag"></a>

**`GET /api/v9/wifi/ap/{id}/diag & /api/v9/wifi/bss/{id}/diag`**

Get the [`WifiDiagItem`](wifi.md#WifiDiagItem "WifiDiagItem") for the AP/BSS

**Example request**:

```http
GET /api/v9/wifi/ap/0/bss HTTP/1.1
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
      "severity": "minor",
      "ap_id": 0,
      "code": "channel_width"
    },
    {
      "severity": "major",
      "ap_id": 0,
      "code": "channel_value"
    },
  ]
}
```

<a id="post--api-v9-wifi-ap-id-diag &amp; -api-v9-wifi-bss-id-diag"></a>

**`POST /api/v9/wifi/ap/{id}/diag & /api/v9/wifi/bss/{id}/diag`**

Fix a few of the [`WifiDiagItem`](wifi.md#WifiDiagItem "WifiDiagItem") at once for a given AP/BSS

**Example request**:

```http
POST /api/v9/wifi/bss/02:00:00:00:00:08/diag HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
[ "network_visibility", "network_visibility", ... ]
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

<a id="wifi-wps-api"></a>

## Wifi WPS API

This api lets you open wps sessions on wifi a bss to allow a device to connect
to Wifi using WPS

To be able to open wps session, you first need to make sure that the bss is
properly configured (with [`WifiBssConfig`](wifi.md#WifiBssConfig "WifiBssConfig") field ‘wps_enabled’ set to true)

Note that wps_enabled requires the encryption to either be wpa2_psk_ccmp
or wpa2_psk_auto

You should call the [`WifiWpsCandidate`](wifi.md#WifiWpsCandidate "WifiWpsCandidate") api help to check which bss
can be used for wps

Also, only one WPS session can be active at a given time

<a id="wifi-wps-candidate-object"></a>

### Wifi Wps Candidate object

WifiWpsCandidate has the following attributes:

<a id="WifiWpsCandidate"></a>

#### Objet WifiWpsCandidate

<a id="WifiWpsCandidate.bssid"></a>

**`bssid string Read-only`**

bss id

<a id="WifiWpsCandidate.ssid"></a>

**`ssid string Read-only`**

wifi network name

<a id="WifiWpsCandidate.bss_uuid"></a>

**`bss_uuid string Read-only`**

bss uuid for wps

<a id="WifiWpsCandidate.band"></a>

**`band string Read-only`**

| band | Description |
| --- | --- |
| 2d4g | 2.4 GHz |
| 5g | 5 GHz |
| 60g | 60 GHz |

<a id="WifiWpsCandidate.encryption"></a>

**`encryption enum Read-only`**

currently configured encryption mode
see [`WifiBssConfig`](wifi.md#WifiBssConfig "WifiBssConfig") encryption field

<a id="WifiWpsCandidate.wps_enabled"></a>

**`wps_enabled bool Read-only`**

is wps enabled for this bss

<a id="WifiWpsCandidate.state"></a>

**`state enum Read-only`**

the current state of the associated ap
see [`WifiBssStatus`](wifi.md#WifiBssStatus "WifiBssStatus") state

<a id="enable-disable-wps-on-all-wi-fi-cards"></a>

### Enable/disable WPS on all Wi-Fi cards

<a id="get--api-v9-wifi-wps-config-"></a>

**`GET /api/v9/wifi/wps/config/`**

Get the global WPS state. WPS is globally enabled if at least one BSS has WPS enabled.

**Example request**:

```http
GET /api/v9/wifi/wps/config/ HTTP/1.1
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
            "enabled": true
      }
}
```

<a id="put--api-v9-wifi-wps-config-"></a>

**`PUT /api/v9/wifi/wps/config/`**

Set the global WPS state. It will update each BSS config with the provided state.

**Example request**:

```http
PUT /api/v9/wifi/wps/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
      "enabled": false
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
            "enabled": false
      }
}
```

<a id="wifi-wps-session-object"></a>

### Wifi WPS Session object

WifiWpsSession has the following attributes:

<a id="WifiWpsSession"></a>

#### Objet WifiWpsSession

<a id="WifiWpsSession.id"></a>

**`id int Read-only`**

wps session id

<a id="WifiWpsSession.bss_uuid"></a>

**`bss_uuid string Read-only`**

bss wps uuid

<a id="WifiWpsSession.ssid"></a>

**`ssid string Read-only`**

ssid

<a id="WifiWpsSession.active"></a>

**`active bool Read-only`**

is the session active

<a id="WifiWpsSession.result"></a>

**`result enum Read-only`**

result of the wps session

| result | Description |
| --- | --- |
| success | success |
| user_canceled | canceled by user |
| self_canceled | canceled by restart of bss |
| failed_timeout | timeout while waiting for station |
| failed_overlap | another wps session was active |
| failed_unknown | unknown failure |

<a id="WifiWpsSession.start_date"></a>

**`start_date int Read-only`**

session start date (timestamp)

<a id="WifiWpsSession.end_date"></a>

**`end_date enum Read-only`**

session end date (timestamp)

<a id="WifiWpsSession.mac"></a>

**`mac string Read-only`**

mac of the associated client (in case of success)

<a id="start-a-wps-session-on-a-bss"></a>

### Start a Wps session on a bss

<a id="post--api-v9-wifi-wps-start-"></a>

**`POST /api/v9/wifi/wps/start/`**

Once you identified a [`WifiWpsCandidate`](wifi.md#WifiWpsCandidate "WifiWpsCandidate") eligible for wps
you can start a [`WifiWpsSession`](wifi.md#WifiWpsSession "WifiWpsSession") on the associated bss.
In return you’ll get the id of the created session.

**Example request**:

```http
POST /api/v9/wifi/wps/start/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
      "bssid":"14:0C:76:87:04:38"
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
      "result": 1
}
```

<a id="stop-a-wps-session"></a>

### Stop a Wps session

This lets you close an open session

**Example request**:

```http
POST /api/v9/wifi/wps/stop/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
      "session_id": 1
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

<a id="list-the-wps-session"></a>

### List the Wps session

<a id="get--api-v9-wifi-wps-sessions-"></a>

**`GET /api/v9/wifi/wps/sessions/`**

Get the list of [`WifiWpsSession`](wifi.md#WifiWpsSession "WifiWpsSession")

**Example request**:

```http
GET /api/v9/wifi/wps/sessions/ HTTP/1.1
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
                  "mac": "00:00:00:00:00:00",
                  "end_date": 1516012651,
                  "ssid": "r0ro 5G",
                  "active": false,
                  "id": 1,
                  "start_date": 1516012531,
                  "result": "failed_timeout",
                  "bss_uuid": "6a55ea3d-29fa-4bd9-b1e3-22a49a3ca134"
            }
      ]
}
```

<a id="clear-all-wps-sessions"></a>

### Clear all Wps Sessions

<a id="delete--api-v9-wifi-wps-sessions-"></a>

**`DELETE /api/v9/wifi/wps/sessions/`**

Clear all the existing wps sessions

**Example request**:

```http
DELETE /api/v9/wifi/wps/sessions/ HTTP/1.1
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

<a id="wifi-guest"></a>

## Wifi guest

This api lets you create “custom key” (guest Wi-Fi access) that can be used
on your existing bss to allow someone to connect to your Wi-Fi network without
knowing your actual Wi-Fi password.

When creating a “custom key” you can select if the associated access should
be restricted to WAN only access, or if the guest can also access your local
network. You can also define how long the access should be available.

A dedicated Wi-Fi network is created for guest usage, and the SSID can
be configured. Note that network will only be running when you have
wifi running and a custom key created.

<a id="wifi-custom-key-config"></a>

### Wifi Custom Key config

<a id="WifiCustomKeyConfig"></a>

#### Objet WifiCustomKeyConfig

<a id="WifiCustomKeyConfig.ssid"></a>

**`ssid string`**

The name of the dedicated wifi network

<a id="WifiCustomKeyConfig.ssid_read_only"></a>

**`ssid_read_only bool Read-only`**

When true, the SSID name cannot be changed.

<a id="WifiCustomKeyConfig.hide_ssid"></a>

**`hide_ssid bool Read-only`**

When true, the SSID used for guest network is hidden.

<a id="WifiCustomKeyConfig.encryption"></a>

**`encryption enum Read-only`**

Encryption used for guest Wi-Fi network.

<a id="get-or-change-the-dedicated-ap-config"></a>

### Get or change the dedicated ap config

<a id="get--api-v14-wifi-custom_keys-config-"></a>

**`GET /api/v14/wifi/custom_keys/config/`**

Get the dedicated guest config as a [`WifiCustomKeyConfig`](wifi.md#WifiCustomKeyConfig "WifiCustomKeyConfig")

**Example request**:

```http
GET /api/v14/wifi/custom_keys/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
      "success":true,
      "result": {
            "ssid":"Freebox-C0001B-guest",
            "ssid_read_only":false,
            "hide_ssid":false,
            "encryption":"wpa2_psk"
      }
}
```

<a id="put--api-v14-wifi-custom_keys-config-"></a>

**`PUT /api/v14/wifi/custom_keys/config/`**

Set the dedicated guest AP config. Only SSID or global enabled switch.

**Example request**:

```http
PUT /api/v9/wifi/custom_keys/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
      "ssid": "my-guest-network-ssid"
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
      "success":true,
      "result": {
            "ssid":"my-guest-network-ssid",
            "ssid_read_only":true,
            "hide_ssid":false,
            "encryption":"wpa2_psk"
      }
}
```

<a id="wifi-custom-key-object"></a>

### Wifi Custom Key object

WifiCustomKey has the following attributes:

<a id="WifiCustomKeyHost"></a>

#### Objet WifiCustomKeyHost

<a id="WifiCustomKeyHost.hostname"></a>

**`hostname string Read-only`**

host name

<a id="WifiCustomKeyHost.host"></a>

**`host LanHost Read-only`**

optional host information from Lan Browser (if available)

<a id="WifiCustomKeyParams"></a>

#### Objet WifiCustomKeyParams

<a id="WifiCustomKeyParams.description"></a>

**`description string`**

description of the custom key

<a id="WifiCustomKeyParams.key"></a>

**`key string`**

Wi-Fi password for this custom access
“**\*\*\*\***” will be returned when insufficient permission

<a id="WifiCustomKeyParams.max_use_count"></a>

**`max_use_count int`**

Number of different hosts that can connect to this network
(maximum 127)
0 has special meaning, it means unlimited number of users.

<a id="WifiCustomKeyParams.duration"></a>

**`duration int`**

Number of seconds before the custom access is revoked

<a id="WifiCustomKeyParams.access_type"></a>

**`access_type enum`**

| access_type | Description |
| --- | --- |
| full | stations will get full access to local network + internet |
| net_only | stations connected using this custom key will be isolated and won’t have access to local network devices |

<a id="WifiCustomKey"></a>

#### Objet WifiCustomKey

<a id="WifiCustomKey.id"></a>

**`id int Read-only`**

custom key id

<a id="WifiCustomKey.remaining"></a>

**`remaining int Read-only`**

time remaining before the access (seconds)
if 0 then it does not expire

<a id="WifiCustomKey.params"></a>

**`params WifiCustomKeyParams`**

custom key parameters

<a id="WifiCustomKey.users"></a>

**`users [] array of WifiCustomKeyHost Read-only`**

list of hosts that used the custom key

<a id="get-the-list-of-wifi-custom-key"></a>

### Get the list of wifi custom key

<a id="get--api-v9-wifi-custom_key-"></a>

**`GET /api/v9/wifi/custom_key/`**

Get the list of [`WifiCustomKey`](wifi.md#WifiCustomKey "WifiCustomKey")

**Example request**:

```http
GET /api/v9/wifi/custom_key/ HTTP/1.1
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
                        "id": 8,
                        "remaining": 86376,
                        "params": {
                                "max_use_count": 100,
                                "description": "soirée mario kart",
                                "duration": 86400,
                                "access_type": "full",
                                "key": "YY5Sg74W3VNxrmfwAz7aCY7OVqRVG2JN"
                        }
                }
        ]

}
```

<a id="getting-a-particular-wifi-custom-key"></a>

### Getting a particular wifi custom key

<a id="get--api-v9-wifi-custom_key-key_id"></a>

**`GET /api/v9/wifi/custom_key/{key_id}`**

Returns the requested [`WifiCustomKey`](wifi.md#WifiCustomKey "WifiCustomKey") properties

**Example request**:

```http
GET /api/v9/wifi/custom_key/8 HTTP/1.1
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
                "id": 8,
                "remaining": 86376,
                "params": {
                        "max_use_count": 100,
                        "description": "soirée mario kart",
                        "duration": 86400,
                        "access_type": "full",
                        "key": "YY5Sg74W3VNxrmfwAz7aCY7OVqRVG2JN"
                }
        }
}
```

<a id="delete-a-wifi-custom-key"></a>

### Delete a wifi custom key

<a id="delete--api-v9-wifi-custom_key-key_id"></a>

**`DELETE /api/v9/wifi/custom_key/{key_id}`**

Delete the [`WifiCustomKey`](wifi.md#WifiCustomKey "WifiCustomKey") with the given id
It will automatically disconnect any connected stations using this custom key

**Example request**:

```http
DELETE /api/v9/wifi/custom_key/8 HTTP/1.1
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

<a id="create-a-new-wifi-custom-key"></a>

### Create a new wifi custom key

<a id="post--api-v9-wifi-custom_key-"></a>

**`POST /api/v9/wifi/custom_key/`**

Create a new the [`WifiCustomKey`](wifi.md#WifiCustomKey "WifiCustomKey")
Post the parameters of the custom key

**Example request**:

```http
POST /api/v9/wifi/custom_key HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
      "description": "zuper",
      "key": "rzR18eLeh6D8B7n1DtMbeDxwo2d4O9fB",
      "max_use_count": "100",
      "duration":86400,
      "access_type":"net_only"
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
      "success":true,
      "result": {
            "id": 11,
            "remaining": 86399,
            "params": {
                  "max_use_count": 100,
                  "description": "zuper",
                  "duration": 86400,
                  "access_type": "full",
                  "key":"rzR18eLeh6D8B7n1DtMbeDxwo2d4O9fB"
            }
      }
}
```

<a id="temporary-disabling-wifi"></a>

## Temporary disabling Wifi

This API lets you disable some wifi bands for a given amount of time. This is useful to pair IOT devices that only supports some bands.

<a id="temporary-disable-object"></a>

### Temporary disable object

TemporaryWifiDisable has the following attributes:

<a id="TemporaryWifiDisable"></a>

#### Objet TemporaryWifiDisable

<a id="TemporaryWifiDisable.duration"></a>

**`duration int Write-only`**

temporary disable duration

<a id="TemporaryWifiDisable.keep"></a>

**`keep enum Write-only`**

specify a wifi band to keep active

| keep | Description |
| --- | --- |
| 2d4g | keep only 2,4Ghz band active |
| 5g | keep only 5GHz bands active |
| 6g | keep only 6GHz band active |

<a id="TemporaryWifiDisable.remaining"></a>

**`remaining int Read-only`**

remaining seconds the wifi is temporarily disabled. Set to 0 to stop the temporary wifi disabling period.

<a id="get-temporary-disable-state"></a>

### Get temporary disable state

<a id="get--api-v13-wifi-temp_disable"></a>

**`GET /api/v13/wifi/temp_disable`**

Get the state of temporary wifi disable.

**Example request**:

```http
GET /api/v13/wifi/temp_disable HTTP/1.1
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
        "remaining": 267
    }
}
```

<a id="post--api-v13-wifi-temp_disable"></a>

**`POST /api/v13/wifi/temp_disable`**

Start or stop a temporary wifi disabling period

**Example request**:

```http
POST /api/v13/wifi/temp_disable HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
    "duration": 1200,
    "keep": "2d4g"
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

<a id="multi-link-operation-mlo"></a>

## Multi Link Operation (MLO)

For a given BSS you can configure with which bands it will try to participate in
an MLD. Whatever the configuration is, the operational state may be
different if the BSS on the partner AP is unavailable (disabled or no EHT) or
does not have the right parameters (not using shared params or wrong security)

<a id="available-partner"></a>

### Available partner

To get the available AP partner of a BSS use the mlo/allowed_comb api to return
a list of possible combinations:

<a id="get--api-v14-wifi-bss-id-mlo-allowed_comb"></a>

**`GET /api/v14/wifi/bss/{id}/mlo/allowed_comb`**

Get the allowed phy combination for a BSS

**Example request**:

```http
GET /api/v14/wifi/bss/02:00:00:00:00:00/mlo/allowed_comb HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json

```

{

“success”: true,
“result”: [

[ 0, 1 ],
[ 0 ]

]

}

<a id="mlo-configuration-object"></a>

### MLO configuration object

<a id="WifiMLOConfiguration"></a>

#### Objet WifiMLOConfiguration

<a id="WifiMLOConfiguration.partners"></a>

**`partners [int]`**

List of phys participating in the MLD for the BSS
An empty array means MLO is disabled
An array with only the BSS’s AP index in it means SLO (single link mode)
The allowed combinations are retrieved by the mlo/allowed_comb api.

<a id="getting-the-mlo-config"></a>

### Getting the MLO config

To get the currently configured partners of a BSS mlo/config. It will return the
current [`WifiMLOConfiguration`](wifi.md#WifiMLOConfiguration "WifiMLOConfiguration") for this BSS

<a id="get--api-v14-wifi-bss-id-mlo-config"></a>

**`GET /api/v14/wifi/bss/{id}/mlo/config`**

Get the current [`WifiMLOConfiguration`](wifi.md#WifiMLOConfiguration "WifiMLOConfiguration") for the BSS

**Example request**:

```http
GET /api/v14/wifi/bss/02:00:00:00:00:00/mlo/config HTTP/1.1
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
      partners: [ 0, 1 ]
   }
}
```

<a id="changing-the-mlo-config"></a>

### Changing the MLO config

To update the MLO confuguration put a new [`WifiMLOConfiguration`](wifi.md#WifiMLOConfiguration "WifiMLOConfiguration")
at mlo/config. Please note that only combinations from mlo/allowed_comb can
be used for the ‘partners’ field

<a id="put--api-v9-wifi-config---variante-2"></a>

**`PUT /api/v9/wifi/config/`**

Update the [`WifiGlobalConfig`](wifi.md#WifiGlobalConfig "WifiGlobalConfig")

**Example request**:

```http
PUT /api/v14/wifi/bss/02:00:00:00:00:00/mlo/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "partners": [ 0, 1 ]
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
       partners: [ 0, 1 ]
   }
}
```
