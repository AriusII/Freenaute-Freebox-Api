# Évolutions officielles jusqu’à l’API 16.0

Le document embarqué contient 27 historiques de changement. Les ajouts et modifications ci-dessous proviennent de ces historiques. Les retraits et dépréciations sont réunis dans [le journal des exclusions](MIGRATIONS-EXCLUSIONS.md). Les anciennes versions restent des indications historiques ; la référence fonctionnelle utilise uniquement les schémas actuels.

## Api changes from version 1.1 to 2.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-1-1-to-2-0)

- invalid_address
- port_conflict
- Added a new ‘cookies’ parameter when adding a download by url. This allow browser plugins to pass cookies along with url. This can be useful for session based authentication.
- conn_ready
- nb_peer
- blocklist_entries
- blocklist_hits
- dht_stats
- filepath
- name
- mimetype
- Added a blacklist API to control bittorrent peers blacklist entries
- main_port
- dht_port
- Added a host property to UPnPRedir
- Added a ipv6ll property to ConnectionIpv6Configuration
- Added a snr_10, attn_10 property to XdslStats
- vpn_rate_down
- vpn_rate_up
- cpum
- cpub
- sw
- hdd
- fan_speed
- temp1
- temp2
- temp3
- Added uptime_val attribute to SystemConfig
- Completely rework Wifi API to be able to handle multiple Access Points.
- Added an Incoming port configuration Api
- Added a VPN Client Api
- Added a VPN Server Api

## Api changes from version 2.0 to 3.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-2-0-to-3-0)

- Added a ginp, rtx_tx, rtx_c, rtx_uc property to XdslStats
- Some tv, epg and pvr api have been added. Those api are undocumented and should be considered UNSTABLE (may be modified without further notice).

## Api changes from version 3.0 to 4.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-3-0-to-4-0)

- The File System api now return more details error codes, and can now return ‘access_denied’ and ‘disk_full’ in case of IO errors
- SystemConfig has new ‘disk_status’, ‘box_flavor’ attributes
- ConnectionStatus now expose ‘ipv4_port_range’ for customers that don’t have a ‘full’ IPv4
- Added new ‘port_outside_range’ error_code when attempting to use a port outside of assigned ‘ipv4_port_range’
- Added ‘remote_access_min_port’ and ‘remote_access_max_port’ to ConnectionConfiguration
- Added ‘min_port’, ‘max_port’ for IncomingPortConfig , VPNServerConfig
- Added ‘readonly’ for IncomingPortConfig
- Added ‘allow_remote_access’ for FtpConfig
- Added ‘mark_all_as_read’ and ‘delete_all’ for Call api
- Added ‘enabled_ipv6’ and ‘node_count_ipv6’ for DhtStats
- Added ‘preview_url’ to DownloadFile for bittorrent downloads
- Added ‘info_hash’, ‘piece_length’ to Download for bittorrent downloads
- Added StorageConfig api
- Added Download Pieces information

## Api changes from version 4.0 to 5.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-4-0-to-5-0)

- Added ‘wps_enabled’, ‘wps_uuid’ to WifiBssConfig wps configuration
- Changed WifiBss logic to expose both ‘bss_params’ and ‘shared_bss_params’ and telling which one is currently used with the new field ‘use_shared_params’. This replaces the ‘use_default_config’ from WifiBssConfig and ‘is_main_bss’ from WifiBssStatus
- Added wifi WifiCustomKey api
- Added wifi WifiWpsSession api
- Added wifi DHCPv6Config api

## Api changes from version 5.0 to 6.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-5-0-to-6-0)

- Added optional ‘filename’ parameter, to download “Add by url” api.
- Added Home API
- Added Player API
- Added Notification API

## Api changes from version 6.0 to 7.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-6-0-to-7-0)

- The api_version contains less information when called unauthenticated and remotely.
- Added VM API for Freebox Delta.

## Api changes from version 7.0 to 8.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-7-0-to-8-0)

- Profile API is simpler to use and replaces parental control API.

## Api changes from version 8.0 to 8.1

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-8-0-to-8-1)

- Wifi has a new diagnostic API
- New language API

## Api changes from version 8.1 to 8.2

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-8-1-to-8-2)

- New LAN browser device type (car): LanHost.host_type .
- File system task now have source and destination info: FsTask.from
- Fix file system rm issue preventing status to be correctly updated
- RAID API is now documented. It is still considered unstable.
- VM API is now documented. It is still considered unstable.
- WebSocket event API has now additional documentation.
- Added Language API to allow changing box language.

## Api changes from version 8.2 to 8.3

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-8-2-to-8-3)

- Added File System Advice API to help user configure the storage attached to the Freebox.

## Api changes from version 8.3 to 8.4

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-8-3-to-8-4)

- New Wifi api error code ‘inval_wps_hidden_ssid’ when trying to enable WPS with hidden SSID.

## Api changes from version 8.4 to 8.5

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-8-4-to-8-5)

- Camera API does not require “camera” permission any more to list cameras. The permission is still needed to access camera records and live stream.
- Add camera lan id in camera API result to find the corresponding lan host in lan browser API.
- Add API to retrieve channel survey history

## Api changes from version 8.5 to 9.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-8-5-to-9-0)

- WiFi API was extended to support 6Ghz band and 802.11ax (HE)

## Api changes from version 9.0 to 9.1

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-9-0-to-9-1)

- New diagnostics API for network throughput slowness detection.

## Api changes from version 9.1 to 10.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-9-1-to-10-0)

- The Connection API has been changed to not mix aggregation and LTE connection status.
- The Connection API exposes Internet Backup connection status.

## Api changes from version 10.0 to 10.1

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-10-0-to-10-1)

- Expose phone number associated with the subscription
- Expose voicemails left on the line

## Api changes from version 10.1 to 10.2

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-10-1-to-10-2)

- Add wifi global state API

## Api changes from version 10.2 to 11.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-10-2-to-11-0)

- API to get the box update status
- API to configure box standby (either WiFi or box standby)
- API to shutdown box
- API to configure LAN SFP port on supported platforms
- Update notification API to be able to customize notification server
- Add custom_key_ssid to BSS status
- Standby API supersedes WiFi planning API (which may be removed in the future)

## Api changes from version 11.0 to 11.1

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-11-0-to-11-1)

- The SystemModelInfo object can contain additional fields to indicate Eco-WiFi and WOP support
- Add ipv6_prefix_firewall field to IPv6 configuration object, in order to enable the IPv6 firewall on secondary prefixes

## Api changes from version 11.1 to 11.2

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-11-1-to-11-2)

- Add api to get a FileInfo list from a list of file paths

## Api changes from version 11.2 to 12.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-11-2-to-12-0)

- The WifiApStatus field of WifiAp object has a new value stopping when a stop operation is pending due to param or disabled state
- The WifiAllowedComb object now have a psc field to indicate that this channel combination is using a Primary Scanning Channel (PSC)
- Add new lan_host notification type to be notified when a new host is connected to the box for the first time
- Add new password_change notification type to be notified when the admin password has been changed

## Api changes from version 12.0 to 12.1

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-12-0-to-12-1)

- Add exifMode optional parameter to file list API to get exif data from supported images (jpeg, heic)
- WifiCustomKeyParams can now have a max_use_count of 0. This means the key has no restriction of how many users can use it to associate to the ap.

## Api changes from version 12.1 to 12.2

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-12-1-to-12-2)

- Add settings to control Freebo Ultra Limited Edition LED strip configuration
- Add capability flag to know if the Freebox Model supports LED strip configuration

## Api changes from version 12.2 to 13.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-12-2-to-13-0)

- The WifiApStatus field of WifiAp object has a new value ‘disabled_temp’ when AP is disabled temporarily.
- A new field named ‘temp_disable_remaining_time’ has been added to WifiAp object.
- Add API /wifi/temp_disable

## Api changes from version 13.0 to 14.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-13-0-to-14-0)

- Add new BSS encryption value wpa23_psk_ccmp_mrsno. When targeting an api version older than 14, this new encryption value is replaced by wpa2_psk_ccmp.
- Add new gcmp256 field in BSS config.
- Add new BSS info to inform if access point supports wep encryption or not
- The guest wifi is now using a dedicated network, and the name of the network can be changed. Use the WifiCustomKeyConfig api to enable/configure it.
- Added support for MLO (Multi Link Operation) configurations. See the MLOConfig API
- Add new lan host types.
- Add categories to lan/browser/types API.
- Add hide_led parameter to control the power LED on supported Freebox models

## Api changes from version 14.0 to 15.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-14-0-to-15-0)

- The file listing API returns an object rather than simply an array of entries
- The file listing API supports pagination

## Api changes from version 15.0 to 16.0

[Historique officiel](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-15-0-to-16-0)

- Added API to configure the limited edition ledstrip activation planning.
- Added API to configure the TFTP server
- The ‘options’ field has been added to the DHCP API. This field can be used to configure the DHCP options included in the replies from the DHCP server.
- Added API to configure the limited edition ledstrip activation planning.
- Added API to configure the screensaver animation on compatible boxes.
- Added API to configure static IPv4 routes.
- Added ‘domain_name’ field in LanHost object to configure a local domain name.
- Added the Wi-Fi steering config API.
