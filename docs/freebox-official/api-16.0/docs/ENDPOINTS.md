# Catalogue des endpoints documentés

Les chemins ci-dessous sont reproduits tels que Free les documente. Une signature contenant `v8`, `v6` ou `v5` n’est pas automatiquement dépréciée. La version découverte est `16.0`; aucune compatibilité opérationnelle des appels n’a été testée.

**338 opérations distinctes ; 342 signatures incluant les variantes de corps ou de paramètres.**

| Méthode | Chemin officiel | Stabilité publiée | Module et détail |
| --- | --- | --- | --- |
| `POST` | `/api/v8/login/authorize/` | not_marked_unstable | [login — Request authorization](reference/fondamentaux/login.md#post--api-v8-login-authorize-) |
| `GET` | `/api/v8/login/authorize/{track_id}` | not_marked_unstable | [login — Track authorization progress](reference/fondamentaux/login.md#get--api-v8-login-authorize-track_id) |
| `GET` | `/api/v8/login/` | not_marked_unstable | [login — Getting the challenge value](reference/fondamentaux/login.md#get--api-v8-login-) |
| `POST` | `/api/v8/login/session/` | not_marked_unstable | [login — Opening a session](reference/fondamentaux/login.md#post--api-v8-login-session-) |
| `POST` | `/api/v8/login/logout/` | not_marked_unstable | [login — Closing the current session](reference/fondamentaux/login.md#post--api-v8-login-logout-) |
| `GET` | `/api/v8/ws/event` | not_marked_unstable | [websocket — WebSocket event API](reference/fondamentaux/websocket.md#get--api-v8-ws-event) |
| `GET` | `/api/v8/airmedia/config/` | not_marked_unstable | [airmedia — Get the current AirMedia configuration](reference/services/airmedia.md#get--api-v8-airmedia-config-) |
| `PUT` | `/api/v8/airmedia/config/` | not_marked_unstable | [airmedia — Update the current AirMedia configuration](reference/services/airmedia.md#put--api-v8-airmedia-config-) |
| `GET` | `/api/v8/airmedia/receivers/` | not_marked_unstable | [airmedia — Get the list of available AirMedia receivers](reference/services/airmedia.md#get--api-v8-airmedia-receivers-) |
| `POST` | `/api/v8/airmedia/receviers/{receiver_name}/` | not_marked_unstable | [airmedia — Sending a new request to an AirMedia receiver](reference/services/airmedia.md#post--api-v8-airmedia-receviers-receiver_name-) |
| `GET` | `/api/v10/call/log/` | not_marked_unstable | [call — List every calls](reference/services/call.md#get--api-v10-call-log-) |
| `POST` | `/api/v10/call/log/delete_all/` | not_marked_unstable | [call — Delete all calls](reference/services/call.md#post--api-v10-call-log-delete_all-) |
| `POST` | `/api/v10/call/log/mark_all_as_read/` | not_marked_unstable | [call — Mark all calls as read](reference/services/call.md#post--api-v10-call-log-mark_all_as_read-) |
| `GET` | `/api/v10/call/log/{id}` | not_marked_unstable | [call — Access a given call entry](reference/services/call.md#get--api-v10-call-log-id) |
| `DELETE` | `/api/v10/call/log/{id}` | not_marked_unstable | [call — Delete a call](reference/services/call.md#delete--api-v10-call-log-id) |
| `PUT` | `/api/v10/call/log/{id}` | not_marked_unstable | [call — Update a call entry](reference/services/call.md#put--api-v10-call-log-id) |
| `GET` | `/api/v10/call/account` | not_marked_unstable | [call — Account](reference/services/call.md#get--api-v10-call-account) |
| `GET` | `/api/v10/call/voicemail/` | not_marked_unstable | [call — List voicemails](reference/services/call.md#get--api-v10-call-voicemail-) |
| `GET` | `/api/v10/call/voicemail/{id}` | not_marked_unstable | [call — Access a specific voicemail entry](reference/services/call.md#get--api-v10-call-voicemail-id) |
| `DELETE` | `/api/v10/call/voicemail/{id}` | not_marked_unstable | [call — Delete a voicemail](reference/services/call.md#delete--api-v10-call-voicemail-id) |
| `PUT` | `/api/v10/call/voicemail/{id}` | not_marked_unstable | [call — Update a voicemail entry](reference/services/call.md#put--api-v10-call-voicemail-id) |
| `GET` | `/api/v10/call/voicemail/{id}/audio_file` | not_marked_unstable | [call — Retrieve a voicemail](reference/services/call.md#get--api-v10-call-voicemail-id-audio_file) |
| `GET` | `/api/v8/contact/` | not_marked_unstable | [contacts — Get a list of contacts](reference/services/contacts.md#get--api-v8-contact-) |
| `GET` | `/api/v8/contact/{id}` | not_marked_unstable | [contacts — Access a given contact entry](reference/services/contacts.md#get--api-v8-contact-id) |
| `POST` | `/api/v8/contact/` | not_marked_unstable | [contacts — Create a contact](reference/services/contacts.md#post--api-v8-contact-) |
| `DELETE` | `/api/v8/contact/{id}` | not_marked_unstable | [contacts — Delete a contact](reference/services/contacts.md#delete--api-v8-contact-id) |
| `PUT` | `/api/v8/contact/{id}` | not_marked_unstable | [contacts — Update a contact entry](reference/services/contacts.md#put--api-v8-contact-id) |
| `GET` | `/api/v8/contact/{contact_id}/[numbers|addresses|urls|emails]/` | not_marked_unstable | [contacts — Get the list of numbers for a given contact](reference/services/contacts.md#get--api-v8-contact-contact_id-[numbers|addresses|urls|emails]-) |
| `GET` | `/api/v8/[number,address,url,email]/{id}` | not_marked_unstable | [contacts — Access a given contact number](reference/services/contacts.md#get--api-v8-[number,address,url,email]-id) |
| `POST` | `/api/v8/[number,address,url,email]/` | not_marked_unstable | [contacts — Create a contact number](reference/services/contacts.md#post--api-v8-[number,address,url,email]-) |
| `DELETE` | `/api/v8/[number,address,url,email]/{id}` | not_marked_unstable | [contacts — Delete a contact number](reference/services/contacts.md#delete--api-v8-[number,address,url,email]-id) |
| `PUT` | `/api/v8/[number,address,url,email]/{id}` | not_marked_unstable | [contacts — Update a contact number](reference/services/contacts.md#put--api-v8-[number,address,url,email]-id) |
| `GET` | `/api/v11/connection/` | not_marked_unstable | [connection — Get the current Connection status](reference/reseau/connection.md#get--api-v11-connection-) |
| `GET` | `/api/v11/connection/config/` | not_marked_unstable | [connection — Get the current Connection configuration](reference/reseau/connection.md#get--api-v11-connection-config-) |
| `PUT` | `/api/v11/connection/config/` | not_marked_unstable | [connection — Update the Connection configuration](reference/reseau/connection.md#put--api-v11-connection-config-) |
| `GET` | `/api/v11/connection/ipv6/config/` | not_marked_unstable | [connection — Get the current IPv6 Connection configuration](reference/reseau/connection.md#get--api-v11-connection-ipv6-config-) |
| `PUT` | `/api/v11/connection/ipv6/config/` | not_marked_unstable | [connection — Update the IPv6 Connection configuration](reference/reseau/connection.md#put--api-v11-connection-ipv6-config-) |
| `GET` | `/api/v11/connection/xdsl/` | UNSTABLE | [connection — Get the current xDSL infos](reference/reseau/connection.md#get--api-v11-connection-xdsl-) |
| `GET` | `/api/v11/connection/lte/{id}` | UNSTABLE | [connection — Get the current LTE infos](reference/reseau/connection.md#get--api-v11-connection-lte-id) |
| `GET` | `/api/v11/connection/aggregation` | UNSTABLE | [connection — Get the current xDSL/LTE aggregation infos](reference/reseau/connection.md#get--api-v11-connection-aggregation) |
| `PUT` | `/api/v11/connection/aggregation` | UNSTABLE | [connection — Update the xDSL/LTE aggregation configuration](reference/reseau/connection.md#put--api-v11-connection-aggregation) |
| `GET` | `/api/v11/connection/ftth/` | UNSTABLE | [connection — Get the current FTTH status](reference/reseau/connection.md#get--api-v11-connection-ftth-) |
| `GET` | `/api/v11/connection/ddns/{provider}/status/` | not_marked_unstable | [connection — Get the status of a DynDNS service](reference/reseau/connection.md#get--api-v11-connection-ddns-provider-status-) |
| `GET` | `/api/v11/connection/ddns/{provider}/` | not_marked_unstable | [connection — Get the config of a DynDNS service](reference/reseau/connection.md#get--api-v11-connection-ddns-provider-) |
| `PUT` | `/api/v11/connection/ddns/{provider}/` | not_marked_unstable | [connection — Set the config of a DynDNS service](reference/reseau/connection.md#put--api-v11-connection-ddns-provider-) |
| `GET` | `/api/v8/lan/config/` | not_marked_unstable | [lan — Get the current Lan configuration](reference/reseau/lan.md#get--api-v8-lan-config-) |
| `PUT` | `/api/v8/lan/config/` | not_marked_unstable | [lan — Update the current Lan configuration](reference/reseau/lan.md#put--api-v8-lan-config-) |
| `GET` | `/api/v16/lan/routes` | not_marked_unstable | [lan — Get the current routing configuration](reference/reseau/lan.md#get--api-v16-lan-routes) |
| `PUT` | `/api/v16/lan/routes/` | not_marked_unstable | [lan — Update the current routing configuration](reference/reseau/lan.md#put--api-v16-lan-routes-) |
| `GET` | `/api/v8/lan/browser/interfaces/` | not_marked_unstable | [lan — Getting the list of browsable LAN interfaces](reference/reseau/lan.md#get--api-v8-lan-browser-interfaces-) |
| `GET` | `/api/v16/lan/browser/{interface}/` | not_marked_unstable | [lan — Getting the list of hosts on a given interface](reference/reseau/lan.md#get--api-v16-lan-browser-interface-) |
| `GET` | `/api/v16/lan/browser/{interface}/{hostid}/` | not_marked_unstable | [lan — Getting an host information](reference/reseau/lan.md#get--api-v16-lan-browser-interface-hostid-) |
| `PUT` | `/api/v16/lan/browser/{interface}/{hostid}/` | not_marked_unstable | [lan — Updating an host information](reference/reseau/lan.md#put--api-v16-lan-browser-interface-hostid-) |
| `GET` | `/api/v8/lan/browser/types/` | not_marked_unstable | [lan — Getting available lan host types](reference/reseau/lan.md#get--api-v8-lan-browser-types-) |
| `POST` | `/api/v8/lan/wol/{interface}/` | not_marked_unstable | [lan — Send Wake ok Lan packet to an host](reference/reseau/lan.md#post--api-v8-lan-wol-interface-) |
| `GET` | `/api/v8/freeplug/` | not_marked_unstable | [freeplug — Get the current Freeplugs networks](reference/reseau/freeplug.md#get--api-v8-freeplug-) |
| `GET` | `/api/v8/freeplug/{id}/` | not_marked_unstable | [freeplug — Get a particular Freeplug information](reference/reseau/freeplug.md#get--api-v8-freeplug-id-) |
| `POST` | `/api/v8/freeplug/{id}/reset/` | not_marked_unstable | [freeplug — Reset a Freeplug](reference/reseau/freeplug.md#post--api-v8-freeplug-id-reset-) |
| `GET` | `/api/v16/dhcp/config/` | not_marked_unstable | [dhcp — Get the current DHCP configuration](reference/reseau/dhcp.md#get--api-v16-dhcp-config-) |
| `PUT` | `/api/v16/dhcp/config/` | not_marked_unstable | [dhcp — Update the current DHCP configuration](reference/reseau/dhcp.md#put--api-v16-dhcp-config-) |
| `GET` | `/api/v16/dhcp/static_lease/` | not_marked_unstable | [dhcp — Get the list of DHCP static leases](reference/reseau/dhcp.md#get--api-v16-dhcp-static_lease-) |
| `GET` | `/api/v16/dhcp/static_lease/{id}` | not_marked_unstable | [dhcp — Get a given DHCP static lease](reference/reseau/dhcp.md#get--api-v16-dhcp-static_lease-id) |
| `PUT` | `/api/v16/dhcp/static_lease/{id}` | not_marked_unstable | [dhcp — Update DHCP static lease](reference/reseau/dhcp.md#put--api-v16-dhcp-static_lease-id) |
| `DELETE` | `/api/v8/dhcp/static_lease/{id}` | not_marked_unstable | [dhcp — Delete a DHCP static lease](reference/reseau/dhcp.md#delete--api-v8-dhcp-static_lease-id) |
| `POST` | `/api/v16/dhcp/static_lease/` | not_marked_unstable | [dhcp — Add a DHCP static lease](reference/reseau/dhcp.md#post--api-v16-dhcp-static_lease-) |
| `GET` | `/api/v16/dhcp/dynamic_lease/` | not_marked_unstable | [dhcp — Get the list of DHCP dynamic leases](reference/reseau/dhcp.md#get--api-v16-dhcp-dynamic_lease-) |
| `GET` | `/api/v8/dhcpv6/config/` | not_marked_unstable | [dhcpv6 — Get the current DHCPv6 configuration](reference/reseau/dhcpv6.md#get--api-v8-dhcpv6-config-) |
| `PUT` | `/api/v8/dhcpv6/config/` | not_marked_unstable | [dhcpv6 — Update the current DHCPv6 configuration](reference/reseau/dhcpv6.md#put--api-v8-dhcpv6-config-) |
| `GET` | `/api/v8/ftp/config/` | not_marked_unstable | [ftp — Get the current Ftp configuration](reference/services/ftp.md#get--api-v8-ftp-config-) |
| `PUT` | `/api/v8/ftp/config/` | not_marked_unstable | [ftp — Update the FTP configuration](reference/services/ftp.md#put--api-v8-ftp-config-) |
| `GET` | `/api/v16/tftp/config/` | not_marked_unstable | [tftp — Get the current TFTP configuration](reference/services/tftp.md#get--api-v16-tftp-config-) |
| `PUT` | `/api/latest/tftp/config/` | not_marked_unstable | [tftp — Update the TFTP configuration](reference/services/tftp.md#put--api-latest-tftp-config-) |
| `GET` | `/api/v8/fw/dmz/` | not_marked_unstable | [nat — Get the current Dmz configuration](reference/reseau/nat.md#get--api-v8-fw-dmz-) |
| `PUT` | `/api/v8/fw/dmz/` | not_marked_unstable | [nat — Update the current Dmz configuration](reference/reseau/nat.md#put--api-v8-fw-dmz-) |
| `GET` | `/api/v8/fw/redir/` | not_marked_unstable | [nat — Getting the list of port forwarding](reference/reseau/nat.md#get--api-v8-fw-redir-) |
| `GET` | `/api/v8/fw/redir/{redir_id}` | not_marked_unstable | [nat — Getting a specific port forwarding](reference/reseau/nat.md#get--api-v8-fw-redir-redir_id) |
| `PUT` | `/api/v8/fw/redir/{redir_id}` | not_marked_unstable | [nat — Updating a port forwarding](reference/reseau/nat.md#put--api-v8-fw-redir-redir_id) |
| `POST` | `/api/v8/fw/redir/` | not_marked_unstable | [nat — Add a port forwarding](reference/reseau/nat.md#post--api-v8-fw-redir-) |
| `DELETE` | `/api/v8/fw/redir/{redir_id}` | not_marked_unstable | [nat — Delete a port forwarding](reference/reseau/nat.md#delete--api-v8-fw-redir-redir_id) |
| `GET` | `/api/v8/fw/incoming/` | not_marked_unstable | [nat — Getting the list of incoming ports](reference/reseau/nat.md#get--api-v8-fw-incoming-) |
| `GET` | `/api/v8/fw/incoming/{port_id}` | not_marked_unstable | [nat — Getting a specific incoming port](reference/reseau/nat.md#get--api-v8-fw-incoming-port_id) |
| `PUT` | `/api/v8/fw/incoming/{port_id}` | not_marked_unstable | [nat — Updating an incoming port](reference/reseau/nat.md#put--api-v8-fw-incoming-port_id) |
| `GET` | `/api/v8/upnpigd/config/` | not_marked_unstable | [igd — Get the current UPnP IGD configuration](reference/reseau/igd.md#get--api-v8-upnpigd-config-) |
| `PUT` | `/api/v8/upnpigd/config/` | not_marked_unstable | [igd — Update the UPnP IGD configuration](reference/reseau/igd.md#put--api-v8-upnpigd-config-) |
| `GET` | `/api/v8/upnpigd/redir/` | not_marked_unstable | [igd — Get the list of current redirection](reference/reseau/igd.md#get--api-v8-upnpigd-redir-) |
| `DELETE` | `/api/v8/upnpigd/redir/{id}` | not_marked_unstable | [igd — Delete a redirection](reference/reseau/igd.md#delete--api-v8-upnpigd-redir-id) |
| `GET` | `/api/v8/lcd/config/` | not_marked_unstable | [lcd — Get the current LCD configuration](reference/systeme/lcd.md#get--api-v8-lcd-config-) |
| `PUT` | `/api/v8/lcd/config/` | not_marked_unstable | [lcd — Update the lcd configuration](reference/systeme/lcd.md#put--api-v8-lcd-config-) |
| `GET` | `/api/v16/ledstrip/status` | not_marked_unstable | [ledstrip — Get ledstrip status](reference/systeme/ledstrip.md#get--api-v16-ledstrip-status) |
| `PUT` | `/api/v16/ledstrip/planning` | not_marked_unstable | [ledstrip — Update ledstrip planning](reference/systeme/ledstrip.md#put--api-v16-ledstrip-planning) |
| `GET` | `/api/v8/netshare/samba/` | not_marked_unstable | [network_share — Get the current Samba configuration](reference/services/network_share.md#get--api-v8-netshare-samba-) |
| `PUT` | `/api/v8/netshare/samba/` | not_marked_unstable | [network_share — Update the Samba configuration](reference/services/network_share.md#put--api-v8-netshare-samba-) |
| `GET` | `/api/v8/netshare/afp/` | not_marked_unstable | [network_share — Get the current Afp configuration](reference/services/network_share.md#get--api-v8-netshare-afp-) |
| `PUT` | `/api/v8/netshare/afp/` | not_marked_unstable | [network_share — Update the Afp configuration](reference/services/network_share.md#put--api-v8-netshare-afp-) |
| `GET` | `/api/v8/upnpav/config/` | not_marked_unstable | [upnpav — Get the current UPnP AV configuration](reference/services/upnpav.md#get--api-v8-upnpav-config-) |
| `PUT` | `/api/v8/upnpav/config/` | not_marked_unstable | [upnpav — Update the UPnP AV configuration](reference/services/upnpav.md#put--api-v8-upnpav-config-) |
| `GET` | `/api/v8/switch/status/` | not_marked_unstable | [switch — Get the current switch status](reference/reseau/switch.md#get--api-v8-switch-status-) |
| `GET` | `/api/v8/switch/port/{id}` | not_marked_unstable | [switch — Get a port configuration](reference/reseau/switch.md#get--api-v8-switch-port-id) |
| `PUT` | `/api/v8/switch/port/{id}` | not_marked_unstable | [switch — Update a port configuration](reference/reseau/switch.md#put--api-v8-switch-port-id) |
| `GET` | `/api/v8/switch/port/{id}/stats` | not_marked_unstable | [switch — Get a port stats](reference/reseau/switch.md#get--api-v8-switch-port-id-stats) |
| `GET` | `/api/v9/wifi/config/` | not_marked_unstable | [wifi — Get the current Wi-Fi global configuration](reference/reseau/wifi.md#get--api-v9-wifi-config-) |
| `PUT` | `/api/v9/wifi/config/` | not_marked_unstable | [wifi — Update the Wi-Fi global configuration](reference/reseau/wifi.md#put--api-v9-wifi-config-) · [wifi — Changing the MLO config](reference/reseau/wifi.md#put--api-v9-wifi-config---variante-2) |
| `GET` | `/api/v16/wifi/steering/config/` | not_marked_unstable | [wifi — Get the current Wi-Fi steering configuration](reference/reseau/wifi.md#get--api-v16-wifi-steering-config-) |
| `PUT` | `/api/v16/wifi/steering/config/` | not_marked_unstable | [wifi — Update the Wi-Fi steering configuration](reference/reseau/wifi.md#put--api-v16-wifi-steering-config-) |
| `GET` | `/api/v10/wifi/state/` | not_marked_unstable | [wifi — Get the global wifi state](reference/reseau/wifi.md#get--api-v10-wifi-state-) |
| `GET` | `/api/v9/wifi/ap/` | not_marked_unstable | [wifi — Get the ap list](reference/reseau/wifi.md#get--api-v9-wifi-ap-) |
| `GET` | `/api/v9/wifi/ap/{id}` | not_marked_unstable | [wifi — Get a particular AP](reference/reseau/wifi.md#get--api-v9-wifi-ap-id) |
| `PUT` | `/api/v9/wifi/ap/{id}` | not_marked_unstable | [wifi — Update an AP](reference/reseau/wifi.md#put--api-v9-wifi-ap-id) |
| `GET` | `/api/v9/wifi/ap/{id}/allowed_channel_comb` | not_marked_unstable | [wifi — Wi-Fi AP allowed channels](reference/reseau/wifi.md#get--api-v9-wifi-ap-id-allowed_channel_comb) |
| `GET` | `/api/v9/wifi/ap/{id}/stations/` | not_marked_unstable | [wifi — Get Wi-Fi Stations List](reference/reseau/wifi.md#get--api-v9-wifi-ap-id-stations-) |
| `GET` | `/api/v9/wifi/ap/{id}/stations/{mac}` | not_marked_unstable | [wifi — Get Wi-Fi Station](reference/reseau/wifi.md#get--api-v9-wifi-ap-id-stations-mac) |
| `GET` | `/api/v9/wifi/ap/{id}/channel_survey_history/{timestamp}` | not_marked_unstable | [wifi — Get survey data history](reference/reseau/wifi.md#get--api-v9-wifi-ap-id-channel_survey_history-timestamp) |
| `POST` | `/api/v9/wifi/ap/{id}/restart` | not_marked_unstable | [wifi — Restart an AP](reference/reseau/wifi.md#post--api-v9-wifi-ap-id-restart) |
| `GET` | `/api/v9/wifi/bss/` | not_marked_unstable | [wifi — Get the bss list](reference/reseau/wifi.md#get--api-v9-wifi-bss-) |
| `GET` | `/api/v9/wifi/bss/{id}` | not_marked_unstable | [wifi — Get a particular BSS](reference/reseau/wifi.md#get--api-v9-wifi-bss-id) |
| `PUT` | `/api/v9/wifi/bss/{id}` | not_marked_unstable | [wifi — Update an BSS](reference/reseau/wifi.md#put--api-v9-wifi-bss-id) |
| `GET` | `/api/v9/wifi/ap/{id}/neighbors/` | not_marked_unstable | [wifi — List AP neighbors](reference/reseau/wifi.md#get--api-v9-wifi-ap-id-neighbors-) |
| `GET` | `/api/v9/wifi/ap/{id}/channel_usage/` | not_marked_unstable | [wifi — List Wi-Fi channels usage](reference/reseau/wifi.md#get--api-v9-wifi-ap-id-channel_usage-) |
| `POST` | `/api/v9/wifi/ap/{id}/neighbors/scan` | not_marked_unstable | [wifi — Refresh radar informations](reference/reseau/wifi.md#post--api-v9-wifi-ap-id-neighbors-scan) |
| `GET` | `/api/v9/wifi/planning/` | not_marked_unstable | [wifi — Get Wi-Fi Planning](reference/reseau/wifi.md#get--api-v9-wifi-planning-) |
| `PUT` | `/api/v9/wifi/planning/` | not_marked_unstable | [wifi — Update Wi-Fi Planning](reference/reseau/wifi.md#put--api-v9-wifi-planning-) |
| `GET` | `/api/v9/wifi/mac_filter/` | not_marked_unstable | [wifi — Get the MAC filter list](reference/reseau/wifi.md#get--api-v9-wifi-mac_filter-) |
| `GET` | `/api/v9/wifi/mac_filter/{filter_id}` | not_marked_unstable | [wifi — Getting a particular MAC filter](reference/reseau/wifi.md#get--api-v9-wifi-mac_filter-filter_id) |
| `PUT` | `/api/v9/wifi/mac_filter/{filter_id}` | not_marked_unstable | [wifi — Updating a MAC filter](reference/reseau/wifi.md#put--api-v9-wifi-mac_filter-filter_id) |
| `DELETE` | `/api/v9/wifi/mac_filter/{filter_id}` | not_marked_unstable | [wifi — Delete a MAC filter](reference/reseau/wifi.md#delete--api-v9-wifi-mac_filter-filter_id) |
| `POST` | `/api/v9/wifi/mac_filter/` | not_marked_unstable | [wifi — Create a new MAC filter](reference/reseau/wifi.md#post--api-v9-wifi-mac_filter-) |
| `POST` | `/api/v9/wifi/config/reset/` | not_marked_unstable | [wifi — Global reset](reference/reseau/wifi.md#post--api-v9-wifi-config-reset-) |
| `GET` | `/api/v9/wifi/ap/{id}/default` | not_marked_unstable | [wifi — Config reset value of an AP](reference/reseau/wifi.md#get--api-v9-wifi-ap-id-default) |
| `GET` | `/api/v9/wifi/bss/{id}/default` | not_marked_unstable | [wifi — Config reset value of a BSS](reference/reseau/wifi.md#get--api-v9-wifi-bss-id-default) |
| `GET` | `/api/v9/wifi/default` | not_marked_unstable | [wifi — Config reset value (bulk)](reference/reseau/wifi.md#get--api-v9-wifi-default) |
| `GET` | `/api/v9/wifi/diag` | not_marked_unstable | [wifi — Global diagnostic](reference/reseau/wifi.md#get--api-v9-wifi-diag) |
| `POST` | `/api/v9/wifi/diag` | not_marked_unstable | [wifi — Global diagnostic](reference/reseau/wifi.md#post--api-v9-wifi-diag) |
| `GET` | `/api/v9/wifi/ap/{id}/diag & /api/v9/wifi/bss/{id}/diag` | not_marked_unstable | [wifi — Per AP/BSS diagnostic](reference/reseau/wifi.md#get--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag) |
| `POST` | `/api/v9/wifi/ap/{id}/diag & /api/v9/wifi/bss/{id}/diag` | not_marked_unstable | [wifi — Per AP/BSS diagnostic](reference/reseau/wifi.md#post--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag) |
| `GET` | `/api/v9/wifi/wps/config/` | not_marked_unstable | [wifi — Enable/disable WPS on all Wi-Fi cards](reference/reseau/wifi.md#get--api-v9-wifi-wps-config-) |
| `PUT` | `/api/v9/wifi/wps/config/` | not_marked_unstable | [wifi — Enable/disable WPS on all Wi-Fi cards](reference/reseau/wifi.md#put--api-v9-wifi-wps-config-) |
| `POST` | `/api/v9/wifi/wps/start/` | not_marked_unstable | [wifi — Start a Wps session on a bss](reference/reseau/wifi.md#post--api-v9-wifi-wps-start-) |
| `GET` | `/api/v9/wifi/wps/sessions/` | not_marked_unstable | [wifi — List the Wps session](reference/reseau/wifi.md#get--api-v9-wifi-wps-sessions-) |
| `DELETE` | `/api/v9/wifi/wps/sessions/` | not_marked_unstable | [wifi — Clear all Wps Sessions](reference/reseau/wifi.md#delete--api-v9-wifi-wps-sessions-) |
| `GET` | `/api/v14/wifi/custom_keys/config/` | not_marked_unstable | [wifi — Get or change the dedicated ap config](reference/reseau/wifi.md#get--api-v14-wifi-custom_keys-config-) |
| `PUT` | `/api/v14/wifi/custom_keys/config/` | not_marked_unstable | [wifi — Get or change the dedicated ap config](reference/reseau/wifi.md#put--api-v14-wifi-custom_keys-config-) |
| `GET` | `/api/v9/wifi/custom_key/` | not_marked_unstable | [wifi — Get the list of wifi custom key](reference/reseau/wifi.md#get--api-v9-wifi-custom_key-) |
| `GET` | `/api/v9/wifi/custom_key/{key_id}` | not_marked_unstable | [wifi — Getting a particular wifi custom key](reference/reseau/wifi.md#get--api-v9-wifi-custom_key-key_id) |
| `DELETE` | `/api/v9/wifi/custom_key/{key_id}` | not_marked_unstable | [wifi — Delete a wifi custom key](reference/reseau/wifi.md#delete--api-v9-wifi-custom_key-key_id) |
| `POST` | `/api/v9/wifi/custom_key/` | not_marked_unstable | [wifi — Create a new wifi custom key](reference/reseau/wifi.md#post--api-v9-wifi-custom_key-) |
| `GET` | `/api/v13/wifi/temp_disable` | not_marked_unstable | [wifi — Get temporary disable state](reference/reseau/wifi.md#get--api-v13-wifi-temp_disable) |
| `POST` | `/api/v13/wifi/temp_disable` | not_marked_unstable | [wifi — Get temporary disable state](reference/reseau/wifi.md#post--api-v13-wifi-temp_disable) |
| `GET` | `/api/v14/wifi/bss/{id}/mlo/allowed_comb` | not_marked_unstable | [wifi — Available partner](reference/reseau/wifi.md#get--api-v14-wifi-bss-id-mlo-allowed_comb) |
| `GET` | `/api/v14/wifi/bss/{id}/mlo/config` | not_marked_unstable | [wifi — Getting the MLO config](reference/reseau/wifi.md#get--api-v14-wifi-bss-id-mlo-config) |
| `GET` | `/api/v8/system/` | UNSTABLE | [system — Current version (api >= v6)](reference/systeme/system.md#get--api-v8-system-) |
| `POST` | `/api/v8/system/reboot/` | not_marked_unstable | [system — Reboot the system](reference/systeme/system.md#post--api-v8-system-reboot-) |
| `POST` | `/api/v11/system/shutdown/` | not_marked_unstable | [system — Shutdown the system](reference/systeme/system.md#post--api-v11-system-shutdown-) |
| `GET` | `/api/v8/vpn/` | UNSTABLE | [vpn — VPN Server List API](reference/services/vpn.md#get--api-v8-vpn-) |
| `GET` | `/api/v8/vpn/{vpn_id}/config/` | UNSTABLE | [vpn — Get a VPN config](reference/services/vpn.md#get--api-v8-vpn-vpn_id-config-) |
| `PUT` | `/api/v8/vpn/openvpn_routed/config/` | UNSTABLE | [vpn — Update the VPN configuration](reference/services/vpn.md#put--api-v8-vpn-openvpn_routed-config-) |
| `GET` | `/api/v8/vpn/user/` | UNSTABLE | [vpn — VPN Server User List](reference/services/vpn.md#get--api-v8-vpn-user-) |
| `GET` | `/api/v8/vpn/user/{login}` | UNSTABLE | [vpn — Get a VPN user](reference/services/vpn.md#get--api-v8-vpn-user-login) |
| `POST` | `/api/v8/vpn/user/` | UNSTABLE | [vpn — Add a VPN User](reference/services/vpn.md#post--api-v8-vpn-user-) |
| `DELETE` | `/api/v8/vpn/user/{login}` | UNSTABLE | [vpn — Delete a VPN User](reference/services/vpn.md#delete--api-v8-vpn-user-login) |
| `PUT` | `/api/v8/vpn/user/{login}` | UNSTABLE | [vpn — Update a VPN User](reference/services/vpn.md#put--api-v8-vpn-user-login) |
| `GET` | `/api/v8/vpn/ip_pool/` | UNSTABLE | [vpn — Get the VPN server IP pool reservations](reference/services/vpn.md#get--api-v8-vpn-ip_pool-) |
| `GET` | `/api/v8/vpn/connection/` | UNSTABLE | [vpn — Get the list of connections](reference/services/vpn.md#get--api-v8-vpn-connection-) |
| `DELETE` | `/api/v8/vpn/connection/{id}` | UNSTABLE | [vpn — Close a given connection](reference/services/vpn.md#delete--api-v8-vpn-connection-id) |
| `GET` | `/api/v8/vpn/download_config/{server_name}/{login}/{fmt}` | UNSTABLE | [vpn — Donwload a user configuration file](reference/services/vpn.md#get--api-v8-vpn-download_config-server_name-login-fmt) |
| `GET` | `/api/v8/vpn_client/config/` | UNSTABLE | [vpn_client — Get VPN Client configuration list](reference/services/vpn_client.md#get--api-v8-vpn_client-config-) |
| `GET` | `/api/v8/vpn_client/config/{id}` | UNSTABLE | [vpn_client — Get a VPN client config](reference/services/vpn_client.md#get--api-v8-vpn_client-config-id) |
| `POST` | `/api/v8/vpn_client/config/` | UNSTABLE | [vpn_client — Add a VPN client configuration](reference/services/vpn_client.md#post--api-v8-vpn_client-config-) |
| `DELETE` | `/api/v8/vpn_client/config/{id}` | UNSTABLE | [vpn_client — Delete a VPN client Configuration](reference/services/vpn_client.md#delete--api-v8-vpn_client-config-id) |
| `PUT` | `/api/v8/vpn_client/config/{id}` | UNSTABLE | [vpn_client — Update the VPN client configuration](reference/services/vpn_client.md#put--api-v8-vpn_client-config-id) |
| `GET` | `/api/v8/vpn_client/status` | UNSTABLE | [vpn_client — Get the VPN client status](reference/services/vpn_client.md#get--api-v8-vpn_client-status) |
| `GET` | `/api/v8/vpn_client/log` | UNSTABLE | [vpn_client — Get the VPN client logs](reference/services/vpn_client.md#get--api-v8-vpn_client-log) |
| `GET` | `/api/v8/downloads/` | not_marked_unstable | [download — Retrieve a Download task](reference/fichiers-telechargements/download.md#get--api-v8-downloads-) |
| `GET` | `/api/v8/downloads/{id}` | not_marked_unstable | [download — Retrieve a Download task](reference/fichiers-telechargements/download.md#get--api-v8-downloads-id) |
| `DELETE` | `/api/v8/downloads/{id}` | not_marked_unstable | [download — Delete a Download task](reference/fichiers-telechargements/download.md#delete--api-v8-downloads-id) |
| `DELETE` | `/api/v8/downloads/{id}/erase` | not_marked_unstable | [download — Delete a Download task](reference/fichiers-telechargements/download.md#delete--api-v8-downloads-id-erase) |
| `PUT` | `/api/v8/downloads/{id}` | not_marked_unstable | [download — Update a Download task](reference/fichiers-telechargements/download.md#put--api-v8-downloads-id) |
| `GET` | `/api/v8/downloads/{id}/log` | not_marked_unstable | [download — Get download log](reference/fichiers-telechargements/download.md#get--api-v8-downloads-id-log) |
| `POST` | `/api/v8/downloads/add` | not_marked_unstable | [download — Adding by URL](reference/fichiers-telechargements/download.md#post--api-v8-downloads-add) · [download — Adding by file upload](reference/fichiers-telechargements/download.md#post--api-v8-downloads-add--variante-2) |
| `GET` | `/api/v8/downloads/stats` | not_marked_unstable | [download — Get the Download Stats](reference/fichiers-telechargements/download.md#get--api-v8-downloads-stats) |
| `GET` | `/api/v8/downloads/{task_id}/files` | not_marked_unstable | [download — Get the list of files for a given Download](reference/fichiers-telechargements/download.md#get--api-v8-downloads-task_id-files) |
| `PUT` | `/api/v8/downloads/{task_id}/files/{file_id}` | not_marked_unstable | [download — Change the priority of a Download File](reference/fichiers-telechargements/download.md#put--api-v8-downloads-task_id-files-file_id) |
| `GET` | `/api/v8/downloads/{task_id}/trackers` | UNSTABLE | [download — Get the list of trackers for a given Download](reference/fichiers-telechargements/download.md#get--api-v8-downloads-task_id-trackers) |
| `POST` | `/api/v8/downloads/{task_id}/trackers` | UNSTABLE | [download — Add a new tracker](reference/fichiers-telechargements/download.md#post--api-v8-downloads-task_id-trackers) |
| `DELETE` | `/api/v8/downloads/{task_id}/trackers/{announce}` | UNSTABLE | [download — Remove a tracker](reference/fichiers-telechargements/download.md#delete--api-v8-downloads-task_id-trackers-announce) |
| `PUT` | `/api/v8/downloads/{task_id}/trackers/{announce}` | UNSTABLE | [download — Update a tracker](reference/fichiers-telechargements/download.md#put--api-v8-downloads-task_id-trackers-announce) |
| `GET` | `/api/v8/downloads/{task_id}/peers` | UNSTABLE | [download — Get the list of peers for a given Download](reference/fichiers-telechargements/download.md#get--api-v8-downloads-task_id-peers) |
| `GET` | `/api/v8/downloads/{task_id}/pieces` | not_marked_unstable | [download — Get the pieces status a given download](reference/fichiers-telechargements/download.md#get--api-v8-downloads-task_id-pieces) |
| `GET` | `/api/v8/downloads/{task_id}/blacklist` | UNSTABLE | [download — Get the list of blacklist entries for a given download](reference/fichiers-telechargements/download.md#get--api-v8-downloads-task_id-blacklist) |
| `DELETE` | `/api/v8/downloads/{task_id}/blacklist/empty` | UNSTABLE | [download — Empty the blacklist for a given download](reference/fichiers-telechargements/download.md#delete--api-v8-downloads-task_id-blacklist-empty) |
| `DELETE` | `/api/v8/downloads/blacklist/{host}` | UNSTABLE | [download — Delete a particular blacklist entry](reference/fichiers-telechargements/download.md#delete--api-v8-downloads-blacklist-host) |
| `POST` | `/api/v8/downloads/blacklist` | UNSTABLE | [download — Add a blacklist entry](reference/fichiers-telechargements/download.md#post--api-v8-downloads-blacklist) |
| `GET` | `/api/v8/downloads/feeds/` | not_marked_unstable | [download_feeds — Get the list of all download Feeds](reference/fichiers-telechargements/download_feeds.md#get--api-v8-downloads-feeds-) |
| `GET` | `/api/v8/downloads/feeds/{id}` | not_marked_unstable | [download_feeds — Get a download Feed](reference/fichiers-telechargements/download_feeds.md#get--api-v8-downloads-feeds-id) |
| `POST` | `/api/v8/downloads/feeds/` | not_marked_unstable | [download_feeds — Add a Download Feed](reference/fichiers-telechargements/download_feeds.md#post--api-v8-downloads-feeds-) |
| `DELETE` | `/api/v8/downloads/feeds/{id}` | not_marked_unstable | [download_feeds — Delete Download Feed](reference/fichiers-telechargements/download_feeds.md#delete--api-v8-downloads-feeds-id) |
| `PUT` | `/api/v8/downloads/feeds/{id}` | not_marked_unstable | [download_feeds — Update a Download Feed](reference/fichiers-telechargements/download_feeds.md#put--api-v8-downloads-feeds-id) |
| `POST` | `/api/v8/downloads/feeds/{id}/fetch` | not_marked_unstable | [download_feeds — Refresh a Download Feed](reference/fichiers-telechargements/download_feeds.md#post--api-v8-downloads-feeds-id-fetch) |
| `POST` | `/api/v8/downloads/feeds/fetch` | not_marked_unstable | [download_feeds — Refresh all Download Feeds](reference/fichiers-telechargements/download_feeds.md#post--api-v8-downloads-feeds-fetch) |
| `GET` | `/api/v8/downloads/feeds/{feed_id}/items/` | not_marked_unstable | [download_feeds — Get the items of a given RSS feed](reference/fichiers-telechargements/download_feeds.md#get--api-v8-downloads-feeds-feed_id-items-) |
| `PUT` | `/api/v8/downloads/feeds/{feed_id}/items/{item_id}` | not_marked_unstable | [download_feeds — Update a feed item](reference/fichiers-telechargements/download_feeds.md#put--api-v8-downloads-feeds-feed_id-items-item_id) |
| `POST` | `/api/v8/downloads/feeds/{feed_id}/items/{item_id}/download` | not_marked_unstable | [download_feeds — Download a feed item](reference/fichiers-telechargements/download_feeds.md#post--api-v8-downloads-feeds-feed_id-items-item_id-download) |
| `POST` | `/api/v8/downloads/feeds/{feed_id}/items/mark_all_as_read` | not_marked_unstable | [download_feeds — Mark all items as read](reference/fichiers-telechargements/download_feeds.md#post--api-v8-downloads-feeds-feed_id-items-mark_all_as_read) |
| `GET` | `/api/v8/downloads/config/` | not_marked_unstable | [download_config — Get the current Download configuration](reference/fichiers-telechargements/download_config.md#get--api-v8-downloads-config-) |
| `PUT` | `/api/v8/downloads/config/` | not_marked_unstable | [download_config — Update the Download configuration](reference/fichiers-telechargements/download_config.md#put--api-v8-downloads-config-) |
| `PUT` | `/api/v8/downloads/throttling` | not_marked_unstable | [download_config — Updating the current Throttling mode](reference/fichiers-telechargements/download_config.md#put--api-v8-downloads-throttling) |
| `GET` | `/api/v15/fs/tasks/` | not_marked_unstable | [fs — List every tasks](reference/fichiers-telechargements/fs.md#get--api-v15-fs-tasks-) |
| `GET` | `/api/v15/fs/tasks/{id}` | not_marked_unstable | [fs — List a task](reference/fichiers-telechargements/fs.md#get--api-v15-fs-tasks-id) |
| `DELETE` | `/api/v15/fs/tasks/{id}` | not_marked_unstable | [fs — Delete a task](reference/fichiers-telechargements/fs.md#delete--api-v15-fs-tasks-id) |
| `PUT` | `/api/v15/fs/tasks/{id}` | not_marked_unstable | [fs — Update a task](reference/fichiers-telechargements/fs.md#put--api-v15-fs-tasks-id) |
| `GET` | `/api/v15/fs/ls/{path}` | not_marked_unstable | [fs — List files](reference/fichiers-telechargements/fs.md#get--api-v15-fs-ls-path) |
| `GET` | `/api/v15/fs/info/{path}` | not_marked_unstable | [fs — Get file information](reference/fichiers-telechargements/fs.md#get--api-v15-fs-info-path) |
| `POST` | `/api/v15/fs/info` | not_marked_unstable | [fs — Batch file information](reference/fichiers-telechargements/fs.md#post--api-v15-fs-info) |
| `POST` | `/api/v15/fs/mv/` | not_marked_unstable | [fs — Move files](reference/fichiers-telechargements/fs.md#post--api-v15-fs-mv-) |
| `POST` | `/api/v15/fs/cp/` | not_marked_unstable | [fs — Copy files](reference/fichiers-telechargements/fs.md#post--api-v15-fs-cp-) |
| `POST` | `/api/v15/fs/rm/` | not_marked_unstable | [fs — Remove files](reference/fichiers-telechargements/fs.md#post--api-v15-fs-rm-) |
| `POST` | `/api/v15/fs/cat/` | not_marked_unstable | [fs — Cat files](reference/fichiers-telechargements/fs.md#post--api-v15-fs-cat-) |
| `POST` | `/api/v15/fs/archive/` | not_marked_unstable | [fs — Create an archive](reference/fichiers-telechargements/fs.md#post--api-v15-fs-archive-) |
| `POST` | `/api/v15/fs/extract/` | not_marked_unstable | [fs — Extract a file](reference/fichiers-telechargements/fs.md#post--api-v15-fs-extract-) |
| `POST` | `/api/v15/fs/repair/` | not_marked_unstable | [fs — Repair a file](reference/fichiers-telechargements/fs.md#post--api-v15-fs-repair-) |
| `POST` | `/api/v15/fs/hash/` | not_marked_unstable | [fs — Hash a file](reference/fichiers-telechargements/fs.md#post--api-v15-fs-hash-) |
| `GET` | `/api/v15/fs/tasks/{id}/hash` | not_marked_unstable | [fs — Get the hash value](reference/fichiers-telechargements/fs.md#get--api-v15-fs-tasks-id-hash) |
| `POST` | `/api/v15/fs/mkdir/` | not_marked_unstable | [fs — Create a directory](reference/fichiers-telechargements/fs.md#post--api-v15-fs-mkdir-) |
| `POST` | `/api/v15/fs/rename/` | not_marked_unstable | [fs — Rename a file/folder](reference/fichiers-telechargements/fs.md#post--api-v15-fs-rename-) |
| `GET` | `/api/v15/dl/{path}` | not_marked_unstable | [fs — Download a file](reference/fichiers-telechargements/fs.md#get--api-v15-dl-path) |
| `GET` | `/api/v8/share_link/` | not_marked_unstable | [share — Retrieve a File Sharing link](reference/fichiers-telechargements/share.md#get--api-v8-share_link-) |
| `GET` | `/api/v8/share_link/{token}` | not_marked_unstable | [share — Retrieve a File Sharing link](reference/fichiers-telechargements/share.md#get--api-v8-share_link-token) |
| `DELETE` | `/api/v8/share_link/{token}` | not_marked_unstable | [share — Delete a File Sharing link](reference/fichiers-telechargements/share.md#delete--api-v8-share_link-token) |
| `POST` | `/api/v8/share_link/` | not_marked_unstable | [share — Create a File Sharing link](reference/fichiers-telechargements/share.md#post--api-v8-share_link-) |
| `GET` | `/api/v8/ws/upload` | not_marked_unstable | [upload — File Upload example](reference/fichiers-telechargements/upload.md#get--api-v8-ws-upload) |
| `GET` | `/api/v8/upload/` | not_marked_unstable | [upload — Get the list of uploads](reference/fichiers-telechargements/upload.md#get--api-v8-upload-) |
| `GET` | `/api/v8/upload/{id}` | not_marked_unstable | [upload — Track an upload status](reference/fichiers-telechargements/upload.md#get--api-v8-upload-id) |
| `DELETE` | `/api/v8/upload/{id}/cancel` | not_marked_unstable | [upload — Cancel an upload](reference/fichiers-telechargements/upload.md#delete--api-v8-upload-id-cancel) |
| `DELETE` | `/api/v8/upload/{id}` | not_marked_unstable | [upload — Delete an upload](reference/fichiers-telechargements/upload.md#delete--api-v8-upload-id) |
| `GET` | `/api/v8/home/adapters` | not_marked_unstable | [home — Get Home Adapters List](reference/maison-profils/home.md#get--api-v8-home-adapters) |
| `GET` | `/api/v8/home/adapters/{id}` | not_marked_unstable | [home — Get a Home Adapter](reference/maison-profils/home.md#get--api-v8-home-adapters-id) |
| `PUT` | `/api/v8/home/adapters/{id}` | not_marked_unstable | [home — Change a Home Adapter status](reference/maison-profils/home.md#put--api-v8-home-adapters-id) |
| `POST` | `/api/v8/home/pairing/{adapter_id}` | not_marked_unstable | [home — Start Pairing](reference/maison-profils/home.md#post--api-v8-home-pairing-adapter_id) · [home — Next Step](reference/maison-profils/home.md#post--api-v8-home-pairing-adapter_id--variante-2) · [home — Stop Pairing](reference/maison-profils/home.md#post--api-v8-home-pairing-adapter_id--variante-3) |
| `GET` | `/api/v8/home/pairing/{adapter_id}` | not_marked_unstable | [home — Current Pairing Step](reference/maison-profils/home.md#get--api-v8-home-pairing-adapter_id) |
| `GET` | `/api/v8/home/nodes` | not_marked_unstable | [home — Get Home Nodes](reference/maison-profils/home.md#get--api-v8-home-nodes) |
| `GET` | `/api/v8/home/nodes/{id}` | not_marked_unstable | [home — Get a Home Node](reference/maison-profils/home.md#get--api-v8-home-nodes-id) |
| `PUT` | `/api/v8/home/nodes/{id}` | not_marked_unstable | [home — Rename a Home Node](reference/maison-profils/home.md#put--api-v8-home-nodes-id) |
| `DELETE` | `/api/v8/home/nodes/{id}` | not_marked_unstable | [home — Delete a Home Node](reference/maison-profils/home.md#delete--api-v8-home-nodes-id) |
| `GET` | `/api/v8/home/endpoints/{node_id}/{endpoint_id}` | not_marked_unstable | [home — Fetch Endpoint Value](reference/maison-profils/home.md#get--api-v8-home-endpoints-node_id-endpoint_id) |
| `PUT` | `/api/v8/home/endpoints/{node_id}/{endpoint_id}` | not_marked_unstable | [home — Change Endpoint Value](reference/maison-profils/home.md#put--api-v8-home-endpoints-node_id-endpoint_id) |
| `GET` | `/api/v8/home/tileset/all` | not_marked_unstable | [home — List all Tiles](reference/maison-profils/home.md#get--api-v8-home-tileset-all) |
| `GET` | `/api/v8/home/tileset/{node_id}` | not_marked_unstable | [home — List a Node sub-tileset](reference/maison-profils/home.md#get--api-v8-home-tileset-node_id) |
| `GET` | `/api/v8/camera/` | not_marked_unstable | [camera — Get list of cameras](reference/maison-profils/camera.md#get--api-v8-camera-) |
| `GET` | `/api/v8/camera/{id}` | not_marked_unstable | [camera — Access a given camera](reference/maison-profils/camera.md#get--api-v8-camera-id) |
| `GET` | `/api/v8/lang/` | not_marked_unstable | [lang — Get language status](reference/systeme/lang.md#get--api-v8-lang-) |
| `POST` | `/api/v8/lang/` | not_marked_unstable | [lang — Set language](reference/systeme/lang.md#post--api-v8-lang-) |
| `GET` | `/api/v11/notif/targets` | not_marked_unstable | [notif — Get list of notification target](reference/maison-profils/notif.md#get--api-v11-notif-targets) |
| `GET` | `/api/v11/notif/targets/{id}` | not_marked_unstable | [notif — Get a given notification target by this id](reference/maison-profils/notif.md#get--api-v11-notif-targets-id) |
| `DELETE` | `/api/v11/notif/targets/{id}` | not_marked_unstable | [notif — Delete a notification target](reference/maison-profils/notif.md#delete--api-v11-notif-targets-id) |
| `PUT` | `/api/v11/notif/targets/{id}` | not_marked_unstable | [notif — Update a notification target](reference/maison-profils/notif.md#put--api-v11-notif-targets-id) |
| `POST` | `/api/v11/notif/targets/` | not_marked_unstable | [notif — Add a notification target](reference/maison-profils/notif.md#post--api-v11-notif-targets-) |
| `POST` | `/register` | not_marked_unstable | [notif — Notification server specification](reference/maison-profils/notif.md#post--register) |
| `DELETE` | `/register/{box_id}/{device_id}` | not_marked_unstable | [notif — Notification server specification](reference/maison-profils/notif.md#delete--register-box_id-device_id) |
| `POST` | `/send` | not_marked_unstable | [notif — Notification server specification](reference/maison-profils/notif.md#post--send) |
| `GET` | `/api/v8/profile` | not_marked_unstable | [profile — Get the list of profiles](reference/maison-profils/profile.md#get--api-v8-profile) |
| `GET` | `/api/v8/profile/{id}` | not_marked_unstable | [profile — Get a profile](reference/maison-profils/profile.md#get--api-v8-profile-id) |
| `POST` | `/api/v8/profile/` | not_marked_unstable | [profile — Add a profile](reference/maison-profils/profile.md#post--api-v8-profile-) |
| `DELETE` | `/api/v8/profile/{id}` | not_marked_unstable | [profile — Delete a profile](reference/maison-profils/profile.md#delete--api-v8-profile-id) |
| `PUT` | `/api/v8/profile/3` | not_marked_unstable | [profile — Update a profile](reference/maison-profils/profile.md#put--api-v8-profile-3) |
| `GET` | `/api/v8/network_control` | not_marked_unstable | [profile — Get Network Control for all profiles](reference/maison-profils/profile.md#get--api-v8-network_control) |
| `GET` | `/api/v8/network_control/{profile_id}` | not_marked_unstable | [profile — Get Network Control for a profile](reference/maison-profils/profile.md#get--api-v8-network_control-profile_id) |
| `PUT` | `/api/v8/network_control/{profile_id}` | not_marked_unstable | [profile — Update Network Control for a profile](reference/maison-profils/profile.md#put--api-v8-network_control-profile_id) |
| `GET` | `/api/v8/network_control/migrate` | not_marked_unstable | [profile — Get migration to new default mode status](reference/maison-profils/profile.md#get--api-v8-network_control-migrate) |
| `POST` | `/api/v8/network_control/migrate` | not_marked_unstable | [profile — Migrate to new default mode](reference/maison-profils/profile.md#post--api-v8-network_control-migrate) |
| `GET` | `/api/v8/network_control/{profile_id}/rules` | not_marked_unstable | [profile — Get Network Control Rules for a profile](reference/maison-profils/profile.md#get--api-v8-network_control-profile_id-rules) |
| `GET` | `/api/v8/network_control/{profile_id}/rules/{rule_id}` | not_marked_unstable | [profile — Get a Network Control Rule](reference/maison-profils/profile.md#get--api-v8-network_control-profile_id-rules-rule_id) |
| `POST` | `/api/v8/network_controlr/{profile_id}/rules/` | not_marked_unstable | [profile — Create a Network Control Rule](reference/maison-profils/profile.md#post--api-v8-network_controlr-profile_id-rules-) |
| `PUT` | `/api/v8/network_control/{id}/rules/{rule_id}` | not_marked_unstable | [profile — Update a Network Control Rule](reference/maison-profils/profile.md#put--api-v8-network_control-id-rules-rule_id) |
| `DELETE` | `/api/v8/network_control/{id}/rules/{rule_id}` | not_marked_unstable | [profile — Delete a Network Control Rule](reference/maison-profils/profile.md#delete--api-v8-network_control-id-rules-rule_id) |
| `GET` | `/api/v8/player` | UNSTABLE | [player — List every player devices](reference/multimedia/player.md#get--api-v8-player) |
| `GET` | `/api/v8/player/{id_player}/api/v6/status/` | UNSTABLE | [player — Get player device status](reference/multimedia/player.md#get--api-v8-player-id_player-api-v6-status-) |
| `POST` | `/api/v8/player/{id_player}/api/v6/control/mediactrl/` | UNSTABLE | [player — Control the active media player of a device](reference/multimedia/player.md#post--api-v8-player-id_player-api-v6-control-mediactrl-) |
| `GET` | `/api/v8/player/{id_player}/api/v6/control/volume/` | UNSTABLE | [player — Control the playback volume of the device](reference/multimedia/player.md#get--api-v8-player-id_player-api-v6-control-volume-) |
| `PUT` | `/api/v8/player/{id_player}/api/v6/control/volume/` | UNSTABLE | [player — Control the playback volume of the device](reference/multimedia/player.md#put--api-v8-player-id_player-api-v6-control-volume-) |
| `POST` | `/api/v8/player/{id_player}/api/v6/control/open` | UNSTABLE | [player — Open a url on a player device](reference/multimedia/player.md#post--api-v8-player-id_player-api-v6-control-open) |
| `GET` | `/api/v8/pvr/config/` | UNSTABLE | [pvr — Get the current PVR configuration](reference/multimedia/pvr.md#get--api-v8-pvr-config-) |
| `PUT` | `/api/v8/pvr/config/` | UNSTABLE | [pvr — Update the current PVR configuration](reference/multimedia/pvr.md#put--api-v8-pvr-config-) |
| `GET` | `/api/v8/pvr/quota/` | UNSTABLE | [pvr — Getting the current quota info](reference/multimedia/pvr.md#get--api-v8-pvr-quota-) |
| `PUT` | `/api/v8/pvr/quota/` | UNSTABLE | [pvr — Request next quota threshold](reference/multimedia/pvr.md#put--api-v8-pvr-quota-) |
| `GET` | `/api/v8/pvr/programmed/` | UNSTABLE | [pvr — Getting the list of precords](reference/multimedia/pvr.md#get--api-v8-pvr-programmed-) |
| `GET` | `/api/v8/pvr/programmed/{id}` | UNSTABLE | [pvr — Getting a specific precord](reference/multimedia/pvr.md#get--api-v8-pvr-programmed-id) |
| `PUT` | `/api/v8/pvr/programmed/{id}` | UNSTABLE | [pvr — Updating a precord](reference/multimedia/pvr.md#put--api-v8-pvr-programmed-id) |
| `DELETE` | `/api/v8/pvr/programmed/{id}` | UNSTABLE | [pvr — Delete a precord](reference/multimedia/pvr.md#delete--api-v8-pvr-programmed-id) |
| `POST` | `/api/v8/pvr/programmed/` | UNSTABLE | [pvr — Create a precord](reference/multimedia/pvr.md#post--api-v8-pvr-programmed-) |
| `GET` | `/api/v8/pvr/finished/` | UNSTABLE | [pvr — Getting the list of frecords](reference/multimedia/pvr.md#get--api-v8-pvr-finished-) |
| `GET` | `/api/v8/pvr/finished/{id}` | UNSTABLE | [pvr — Getting a specific frecord](reference/multimedia/pvr.md#get--api-v8-pvr-finished-id) |
| `PUT` | `/api/v8/pvr/finished/{id}` | UNSTABLE | [pvr — Updating an frecord](reference/multimedia/pvr.md#put--api-v8-pvr-finished-id) |
| `DELETE` | `/api/v8/pvr/finished/{id}` | UNSTABLE | [pvr — Delete an frecord](reference/multimedia/pvr.md#delete--api-v8-pvr-finished-id) |
| `GET` | `/api/v8/pvr/media/` | UNSTABLE | [pvr — Getting the list of media](reference/multimedia/pvr.md#get--api-v8-pvr-media-) |
| `POST` | `/api/v8/rrd/` | UNSTABLE | [rrd — Get RRD stats [UNSTABLE]](reference/stockage-vm/rrd.md#post--api-v8-rrd-) |
| `GET` | `/api/v8/rrd/` | UNSTABLE | [rrd — Get RRD stats [UNSTABLE]](reference/stockage-vm/rrd.md#get--api-v8-rrd-) |
| `GET` | `/api/v11/standby/status` | not_marked_unstable | [standby — Get standby status](reference/systeme/standby.md#get--api-v11-standby-status) |
| `PUT` | `/api/v11/standby/config` | not_marked_unstable | [standby — Update standby config](reference/systeme/standby.md#put--api-v11-standby-config) |
| `GET` | `/api/v8/storage/disk/` | UNSTABLE | [storage — Get the list of disks](reference/stockage-vm/storage.md#get--api-v8-storage-disk-) |
| `GET` | `/api/v8/storage/disk/{id}` | UNSTABLE | [storage — Get a given disk info](reference/stockage-vm/storage.md#get--api-v8-storage-disk-id) |
| `PUT` | `/api/v8/storage/disk/{id}` | UNSTABLE | [storage — Update a disk state](reference/stockage-vm/storage.md#put--api-v8-storage-disk-id) |
| `GET` | `/api/v8/storage/disk/{disk_id}/fsadvice?partition_id={partition_id}&dedicated_disk={bool}` | UNSTABLE | [storage — Get FS advices](reference/stockage-vm/storage.md#get--api-v8-storage-disk-disk_id-fsadvice?partition_id=partition_id&dedicated_disk=bool) |
| `PUT` | `/api/v8/storage/disk/{id}/format/` | UNSTABLE | [storage — Format a disk](reference/stockage-vm/storage.md#put--api-v8-storage-disk-id-format-) |
| `GET` | `/api/v8/storage/partition/` | UNSTABLE | [storage — Get the list of partitions](reference/stockage-vm/storage.md#get--api-v8-storage-partition-) |
| `GET` | `/api/v8/storage/partition/{id}` | UNSTABLE | [storage — Get a given partition info](reference/stockage-vm/storage.md#get--api-v8-storage-partition-id) |
| `PUT` | `/api/v8/storage/partition/{id}` | UNSTABLE | [storage — Update a partition state](reference/stockage-vm/storage.md#put--api-v8-storage-partition-id) |
| `PUT` | `/api/v8/storage/partition/{id}/check/` | UNSTABLE | [storage — Check a partition](reference/stockage-vm/storage.md#put--api-v8-storage-partition-id-check-) |
| `GET` | `/api/v8/storage/config/` | UNSTABLE | [storage — Get the current storage configuration](reference/stockage-vm/storage.md#get--api-v8-storage-config-) |
| `PUT` | `/api/v8/storage/config/` | UNSTABLE | [storage — Update the External Storage configuration](reference/stockage-vm/storage.md#put--api-v8-storage-config-) |
| `GET` | `/api/v8/storage/raid/` | UNSTABLE | [raid — Get the list of RAID arrays](reference/stockage-vm/raid.md#get--api-v8-storage-raid-) |
| `GET` | `/api/v8/storage/raid/{id}` | UNSTABLE | [raid — Get a given RAID array info](reference/stockage-vm/raid.md#get--api-v8-storage-raid-id) |
| `POST` | `/api/v8/storage/raid/` | UNSTABLE | [raid — Create a RAID array](reference/stockage-vm/raid.md#post--api-v8-storage-raid-) |
| `DELETE` | `/api/v8/storage/raid/{id}` | UNSTABLE | [raid — Delete a RAID array](reference/stockage-vm/raid.md#delete--api-v8-storage-raid-id) |
| `PUT` | `/api/v8/storage/raid/{id}` | UNSTABLE | [raid — Start or stop a RAID array](reference/stockage-vm/raid.md#put--api-v8-storage-raid-id) |
| `POST` | `/api/v8/storage/raid/{id}/forcestart` | UNSTABLE | [raid — Force start a RAID array](reference/stockage-vm/raid.md#post--api-v8-storage-raid-id-forcestart) |
| `DELETE` | `/api/v8/storage/raid/{id}/members/faulty` | UNSTABLE | [raid — Remove faulty members from RAID array](reference/stockage-vm/raid.md#delete--api-v8-storage-raid-id-members-faulty) |
| `PUT` | `/api/v8/storage/raid/{id}/members` | UNSTABLE | [raid — Add members to an existing array that has missing members](reference/stockage-vm/raid.md#put--api-v8-storage-raid-id-members) |
| `POST` | `/api/v8/storage/raid/{id}/members/addspares` | UNSTABLE | [raid — Re-add out-of-sync members that appear as spares](reference/stockage-vm/raid.md#post--api-v8-storage-raid-id-members-addspares) |
| `GET` | `/api/v11/sfp/status` | not_marked_unstable | [sfp — Get SFP status](reference/reseau/sfp.md#get--api-v11-sfp-status) |
| `PUT` | `/api/v11/sfp/config` | not_marked_unstable | [sfp — Update SFP config](reference/reseau/sfp.md#put--api-v11-sfp-config) |
| `GET` | `/api/v11/update/` | not_marked_unstable | [update — Get the update status](reference/systeme/update.md#get--api-v11-update-) |
| `GET` | `/api/v8/vm/info/` | UNSTABLE | [vm — Get VM System Info](reference/stockage-vm/vm.md#get--api-v8-vm-info-) |
| `GET` | `/api/v8/vm/distros/` | UNSTABLE | [vm — Get Installable VM distributions](reference/stockage-vm/vm.md#get--api-v8-vm-distros-) |
| `GET` | `/api/v8/vm/` | UNSTABLE | [vm — Get the list of all VMs](reference/stockage-vm/vm.md#get--api-v8-vm-) |
| `GET` | `/api/v8/vm/{id}` | UNSTABLE | [vm — Get a VM](reference/stockage-vm/vm.md#get--api-v8-vm-id) |
| `POST` | `/api/v8/vm/` | UNSTABLE | [vm — Add a VM](reference/stockage-vm/vm.md#post--api-v8-vm-) |
| `DELETE` | `/api/v8/vm/{id}` | UNSTABLE | [vm — Delete a VM](reference/stockage-vm/vm.md#delete--api-v8-vm-id) |
| `PUT` | `/api/v8/vm/{id}` | UNSTABLE | [vm — Update a VM](reference/stockage-vm/vm.md#put--api-v8-vm-id) |
| `POST` | `/api/v8/vm/{id}/start` | UNSTABLE | [vm — Start a VM](reference/stockage-vm/vm.md#post--api-v8-vm-id-start) |
| `POST` | `/api/v8/vm/{id}/powerbutton` | UNSTABLE | [vm — Send a powerbutton signal to a VM](reference/stockage-vm/vm.md#post--api-v8-vm-id-powerbutton) |
| `POST` | `/api/v8/vm/{id}/stop` | UNSTABLE | [vm — Stop a VM](reference/stockage-vm/vm.md#post--api-v8-vm-id-stop) |
| `POST` | `/api/v8/vm/{id}/restart` | UNSTABLE | [vm — Reset a VM](reference/stockage-vm/vm.md#post--api-v8-vm-id-restart) |
| `GET` | `/api/v8/vm/{id}/console` | UNSTABLE | [vm — VM virtual console](reference/stockage-vm/vm.md#get--api-v8-vm-id-console) |
| `GET` | `/api/v8/vm/{id}/vnc` | UNSTABLE | [vm — VM virtual screen](reference/stockage-vm/vm.md#get--api-v8-vm-id-vnc) |
| `POST` | `/api/v8/vm/disk/info` | UNSTABLE | [vm — Get information on a virtual disk](reference/stockage-vm/vm.md#post--api-v8-vm-disk-info) |
| `POST` | `/api/v8/vm/disk/create` | UNSTABLE | [vm — Create a virtual disk](reference/stockage-vm/vm.md#post--api-v8-vm-disk-create) |
| `POST` | `/api/v8/vm/disk/resize` | UNSTABLE | [vm — Resize a virtual disk](reference/stockage-vm/vm.md#post--api-v8-vm-disk-resize) |
| `GET` | `/api/v8/vm/disk/task/{id}` | UNSTABLE | [vm — Get a virtual disk task](reference/stockage-vm/vm.md#get--api-v8-vm-disk-task-id) |
| `DELETE` | `/api/v8/vm/disk/task/{id}` | UNSTABLE | [vm — Delete a virtual disk task](reference/stockage-vm/vm.md#delete--api-v8-vm-disk-task-id) |
