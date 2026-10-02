# Revue SDK Server API 16 — services et multimédia

La lecture du lot est complète : **75 signatures documentées**, **90 routes concrètes** après les alternatives explicites des contacts, **42 objets nommés**, **260 déclarations de champs**, **6 formes inline supplémentaires**, **7 champs présents seulement dans les exemples** et **32 tables de valeurs**. Aucune dépréciation explicite n’a été trouvée dans ce périmètre.

Le verdict technique reste **`blocked_evidence`** : plusieurs déclarations et exemples se contredisent. Les preuves et les propositions de résolution ci-dessous permettent à l’orchestrateur d’accepter un contrat borné. Le verdict ne demande aucune nouvelle autorisation utilisateur. Aucun code, test ou catalogue partagé n’a été modifié pendant cette revue.

| Identité | Valeur |
| --- | --- |
| task_id | `services-media` — groupe attribué par root |
| Tâche de page canonique | `review-4be656bd518b2599` |
| Reviewer / parent | `/root/client_tests` / `root` |
| Date / révision du dépôt | `2026-10-02` / `595cedd2104629c7aa8cd4b344e1af9354f0ab7f` ; changements de travail partagés possibles |
| Cible SDK Freebox | API **16.0**, déclaration brute `#api-version` ; aucun rapport avec le numéro du SDK .NET |
| Acceptation indépendante | Pending orchestrator |

## Sources et provenance

Source primaire : <http://mafreebox.freebox.fr/doc/index.html>, fichier `/workspace/.cloud-setup/freebox-upload-20261002/Freebox-Server-API-16.0/sources/raw/embedded/doc/index.html`.
Empreinte SHA-256 vérifiée : `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03` ; acquisition déclarée par le manifeste : `2026-10-02T17:19:23.212510+00:00`.

**Qualification d’origine** : archive téléchargée fournie par l’utilisateur, rapportant une acquisition HTTP locale de la documentation embarquée. L’empreinte prouve la stabilité des octets examinés ; elle ne constitue pas une nouvelle acquisition HTTPS ou une vérification indépendante de l’émetteur. Le lot API16 repose sur le HTML brut, et non sur les seules descriptions Markdown retraitées.

Tous les documents `docs/reference/services/{airmedia,call,contacts,ftp,network_share,tftp,upnpav,vpn,vpn_client}.md` et `docs/reference/multimedia/{player,pvr}.md` de l’archive ont été lus. Les **75 dt/dd HTTP**, **260 déclarations**, toutes leurs tables et leurs blocs d’exemples ont été confrontés au HTML brut. Les identifiants, champs sans ancre et références erronées ne sont pas corrigés silencieusement.

Le JSON compagnon contient toutes les ancres examinées, les contrats, les champs, les enums, les dépendances et les inconnues : [services-media.json](services-media.json). Toute preuve locale ci-dessous est liée à l’empreinte primaire ci-dessus. Une ancre seule avec une empreinte différente ne conserve pas cette revue.

Les neuf pages publiques archivées correspondantes ont leur empreinte vérifiée et sont enregistrées comme **annexe historique**, sans import de contrat v4 dans la cible16. Aucune page publique TFTP ou Player correspondante n’est présente dans ce sous-ensemble. Leur lecture complète est du ressort du registre global ; les empreintes ci-dessous ne sont pas une affirmation de nouvelle collecte réseau.

| Annexe historique | SHA-256 |
| --- | --- |
| https://dev.freebox.fr/sdk/os/airmedia/ | `d0afb118b43112dcc82908be6e12885b0d4478dd793fb44867524ebbe3bb37a1` |
| https://dev.freebox.fr/sdk/os/call/ | `d46ad4cfd39e06a12a6cec0bf910ba18cf5cab0cf758ad31f1043c53ff0d79a9` |
| https://dev.freebox.fr/sdk/os/contacts/ | `48256ba5ffd71ed939fbee7066a048144a4ccfb42c9587325344ee0e017a07b3` |
| https://dev.freebox.fr/sdk/os/ftp/ | `0f79e2f4f439fcd6c71dcd5d3739199b6750e3d3f7c43adde61ff164af57eeb3` |
| https://dev.freebox.fr/sdk/os/network_share/ | `c237a5731472837b1bfea980c5a2cfd32c4a8d8455bd39e4cae6c932ced96c22` |
| https://dev.freebox.fr/sdk/os/pvr/ | `c166cb9b9991667fad66b540885bdda7220b3305141e8cd0e47e8cfcfcb9b338` |
| https://dev.freebox.fr/sdk/os/upnpav/ | `e899e547aaa54a7ed80380fece7a3dd28d34436472157b15fd50a7e2c4c873bb` |
| https://dev.freebox.fr/sdk/os/vpn/ | `e7421ecc2de00c6a0f2dc704a32f644306d6be3d86ead6074c1c961481f20fa8` |
| https://dev.freebox.fr/sdk/os/vpn_client/ | `ac1da6c8a31dbf14358792df8227da5569b193e8c2064eba50dc85fc3211a11a` |

## Périmètre et versions

| Module | Signatures | Objets | Champs | Classification |
| --- | ---: | ---: | ---: | --- |
| airmedia | 4 | 3 | 10 | retained |
| call | 12 | 2 | 14 | retained |
| contacts | 10 | 5 | 35 | retained |
| ftp | 2 | 1 | 10 | retained |
| tftp | 2 | 1 | 2 | retained |
| network_share | 4 | 2 | 12 | retained |
| upnpav | 2 | 1 | 1 | retained |
| vpn | 12 | 10 | 50 | retained_unstable |
| vpn_client | 7 | 7 | 43 | retained_unstable |
| player | 6 | 5 | 32 | retained_unstable_internal_server_gateway |
| pvr | 14 | 5 | 51 | retained_unstable_internal |

Les signatures historiques v8/v10 sont conservées en preuve. La convention commune `#building-the-api-request-url` compose le root à partir du major découvert, donc `v16` pour cet instantané ; elle ne réécrit pas le **second `/api/v6/` du Player**. L’alias formel TFTP `/api/latest/` reste une dimension explicite à résoudre. Ne jamais construire `/api/vlatest/`.

**Player** reste un gateway HTTP du Freebox Server vers un Player de son réseau local. Il ne s’agit pas du SDK QML autonome. Les labels `UNSTABLE` et `INTERNAL USE ONLY` décrivent la stabilité/disponibilité, sans marque de dépréciation. La source `#api-version` dit : “you can use it but it may change or disappear at any time”.

Le périmètre comprend Account et Voicemail dans le module Call, ainsi que PVR programmed/finished/storage media. Les API FTP/TFTP/SMB/AFP/UPnP AV documentées ici configurent les services de la Freebox ; ce ne sont pas des clients de ces protocoles.

## Opérations HTTP et I/O

Chaque ligne ci-dessous représente une signature entière du catalogue confrontée au HTML brut. Une forme avec alternatives contacts correspond à quatre routes concrètes explicitement autorisées dans le texte. Les formes requête/réponse sont détaillées dans le JSON. `not_documented` et `unknown` ne deviennent pas une valeur par défaut, une permission libre ou une garantie de nullabilité.

| Méthode / chemin documentaire exact | Requête | Résultat / I/O | Preuve |
| --- | --- | --- | --- |
| `GET /api/v8/airmedia/config/` | Pas de corps décrit localement | AirMediaConfig ; json_envelope | [#get--api-v8-airmedia-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-airmedia-config-) |
| `PUT /api/v8/airmedia/config/` | AirMediaConfig | AirMediaConfig ; json_envelope | [#put--api-v8-airmedia-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-airmedia-config-) |
| `GET /api/v8/airmedia/receivers/` | Pas de corps décrit localement | array of AirMediaReceiver ; json_envelope | [#get--api-v8-airmedia-receivers-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-airmedia-receivers-) |
| `POST /api/v8/airmedia/receviers/{receiver_name}/` | AirMediaReceiverRequest | None ; json_envelope | [#post--api-v8-airmedia-receviers-receiver_name-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-airmedia-receviers-receiver_name-) |
| `GET /api/v10/call/log/` | Pas de corps décrit localement | array of CallEntry ; json_envelope | [#get--api-v10-call-log-](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-log-) |
| `POST /api/v10/call/log/delete_all/` | Pas de corps décrit localement | None ; HTTP success shown; body/envelope not documented locally | [#post--api-v10-call-log-delete_all-](http://mafreebox.freebox.fr/doc/index.html#post--api-v10-call-log-delete_all-) |
| `POST /api/v10/call/log/mark_all_as_read/` | Pas de corps décrit localement | None ; HTTP success shown; body/envelope not documented locally | [#post--api-v10-call-log-mark_all_as_read-](http://mafreebox.freebox.fr/doc/index.html#post--api-v10-call-log-mark_all_as_read-) |
| `GET /api/v10/call/log/{id}` | Pas de corps décrit localement | CallEntry ; json_envelope | [#get--api-v10-call-log-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-log-id) |
| `DELETE /api/v10/call/log/{id}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v10-call-log-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v10-call-log-id) |
| `PUT /api/v10/call/log/{id}` | CallEntry | CallEntry ; json_envelope | [#put--api-v10-call-log-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v10-call-log-id) |
| `GET /api/v10/call/account` | Pas de corps décrit localement | CallAccount ; json_envelope | [#get--api-v10-call-account](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-account) |
| `GET /api/v10/call/voicemail/` | Pas de corps décrit localement | array of VoicemailEntry ; json_envelope | [#get--api-v10-call-voicemail-](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-voicemail-) |
| `GET /api/v10/call/voicemail/{id}` | Pas de corps décrit localement | VoicemailEntry ; json_envelope | [#get--api-v10-call-voicemail-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-voicemail-id) |
| `DELETE /api/v10/call/voicemail/{id}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v10-call-voicemail-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v10-call-voicemail-id) |
| `PUT /api/v10/call/voicemail/{id}` | VoicemailEntry | VoicemailEntry ; json_envelope | [#put--api-v10-call-voicemail-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v10-call-voicemail-id) |
| `GET /api/v10/call/voicemail/{id}/audio_file` | Pas de corps décrit localement | WAV bytes ; raw_audio_wav | [#get--api-v10-call-voicemail-id-audio_file](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-voicemail-id-audio_file) |
| `GET /api/v8/contact/` | Pas de corps décrit localement | array of ContactEntry ; json_envelope | [#get--api-v8-contact-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-contact-) |
| `GET /api/v8/contact/{id}` | Pas de corps décrit localement | ContactEntry ; json_envelope | [#get--api-v8-contact-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-contact-id) |
| `POST /api/v8/contact/` | ContactEntry | ContactEntry ; json_envelope | [#post--api-v8-contact-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-contact-) |
| `DELETE /api/v8/contact/{id}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v8-contact-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-contact-id) |
| `PUT /api/v8/contact/{id}` | ContactEntry | ContactEntry ; json_envelope | [#put--api-v8-contact-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-contact-id) |
| `GET /api/v8/contact/{contact_id}/[numbers\|addresses\|urls\|emails]/` | Pas de corps décrit localement | array of ContactNumber\|ContactAddress\|ContactUrl\|ContactEmail ; json_envelope | [#get--api-v8-contact-contact_id-[numbers\|addresses\|urls\|emails]-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-contact-contact_id-[numbers|addresses|urls|emails]-) |
| `GET /api/v8/[number,address,url,email]/{id}` | Pas de corps décrit localement | ContactNumber\|ContactAddress\|ContactUrl\|ContactEmail ; json_envelope | [#get--api-v8-[number,address,url,email]-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-[number,address,url,email]-id) |
| `POST /api/v8/[number,address,url,email]/` | ContactNumber\|ContactAddress\|ContactUrl\|ContactEmail | ContactNumber\|ContactAddress\|ContactUrl\|ContactEmail ; json_envelope | [#post--api-v8-[number,address,url,email]-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-[number,address,url,email]-) |
| `DELETE /api/v8/[number,address,url,email]/{id}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v8-[number,address,url,email]-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-[number,address,url,email]-id) |
| `PUT /api/v8/[number,address,url,email]/{id}` | ContactNumber\|ContactAddress\|ContactUrl\|ContactEmail | ContactNumber\|ContactAddress\|ContactUrl\|ContactEmail ; json_envelope | [#put--api-v8-[number,address,url,email]-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-[number,address,url,email]-id) |
| `GET /api/v8/ftp/config/` | Pas de corps décrit localement | FtpConfig ; json_envelope | [#get--api-v8-ftp-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-ftp-config-) |
| `PUT /api/v8/ftp/config/` | FtpConfig | FtpConfig ; json_envelope | [#put--api-v8-ftp-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-ftp-config-) |
| `GET /api/v16/tftp/config/` | Pas de corps décrit localement | TftpConfig ; json_envelope | [#get--api-v16-tftp-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-tftp-config-) |
| `PUT /api/latest/tftp/config/` | TftpConfig | TftpConfig ; json_envelope | [#put--api-latest-tftp-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-latest-tftp-config-) |
| `GET /api/v8/netshare/samba/` | Pas de corps décrit localement | SambaConfig ; json_envelope | [#get--api-v8-netshare-samba-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-netshare-samba-) |
| `PUT /api/v8/netshare/samba/` | SambaConfig | SambaConfig ; json_envelope | [#put--api-v8-netshare-samba-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-netshare-samba-) |
| `GET /api/v8/netshare/afp/` | Pas de corps décrit localement | AfpConfig ; json_envelope | [#get--api-v8-netshare-afp-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-netshare-afp-) |
| `PUT /api/v8/netshare/afp/` | AfpConfig | AfpConfig ; json_envelope | [#put--api-v8-netshare-afp-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-netshare-afp-) |
| `GET /api/v8/upnpav/config/` | Pas de corps décrit localement | UPnPAVConfig ; json_envelope | [#get--api-v8-upnpav-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upnpav-config-) |
| `PUT /api/v8/upnpav/config/` | UPnPAVConfig | UPnPAVConfig ; json_envelope | [#put--api-v8-upnpav-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-upnpav-config-) |
| `GET /api/v8/vpn/` | Pas de corps décrit localement | array of VPNServer ; json_envelope | [#get--api-v8-vpn-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-) |
| `GET /api/v8/vpn/{vpn_id}/config/` | Pas de corps décrit localement | VPNServerConfig ; json_envelope | [#get--api-v8-vpn-vpn_id-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-vpn_id-config-) |
| `PUT /api/v8/vpn/openvpn_routed/config/` | VPNServerConfig | VPNServerConfig ; json_envelope | [#put--api-v8-vpn-openvpn_routed-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-vpn-openvpn_routed-config-) |
| `GET /api/v8/vpn/user/` | Pas de corps décrit localement | array of VPNUser ; json_envelope | [#get--api-v8-vpn-user-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-user-) |
| `GET /api/v8/vpn/user/{login}` | Pas de corps décrit localement | VPNUser ; json_envelope | [#get--api-v8-vpn-user-login](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-user-login) |
| `POST /api/v8/vpn/user/` | VPNUser | VPNUser ; json_envelope | [#post--api-v8-vpn-user-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vpn-user-) |
| `DELETE /api/v8/vpn/user/{login}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v8-vpn-user-login](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-vpn-user-login) |
| `PUT /api/v8/vpn/user/{login}` | VPNUser | VPNUser ; json_envelope | [#put--api-v8-vpn-user-login](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-vpn-user-login) |
| `GET /api/v8/vpn/ip_pool/` | Pas de corps décrit localement | VPNIpPool ; json_envelope | [#get--api-v8-vpn-ip_pool-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-) |
| `GET /api/v8/vpn/connection/` | Pas de corps décrit localement | array of VPNConnection ; json_envelope | [#get--api-v8-vpn-connection-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-connection-) |
| `DELETE /api/v8/vpn/connection/{id}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v8-vpn-connection-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-vpn-connection-id) |
| `GET /api/v8/vpn/download_config/{server_name}/{login}/{fmt}` | Pas de corps décrit localement | raw profile (plain) / JSON shape not documented (json) ; raw_file_or_unspecified_json | [#get--api-v8-vpn-download_config-server_name-login-fmt](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-download_config-server_name-login-fmt) |
| `GET /api/v8/vpn_client/config/` | Pas de corps décrit localement | array of VPNClientConfig ; json_envelope | [#get--api-v8-vpn_client-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-config-) |
| `GET /api/v8/vpn_client/config/{id}` | Pas de corps décrit localement | VPNClientConfig ; json_envelope | [#get--api-v8-vpn_client-config-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-config-id) |
| `POST /api/v8/vpn_client/config/` | VPNClientConfig | VPNClientConfig ; json_envelope | [#post--api-v8-vpn_client-config-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vpn_client-config-) |
| `DELETE /api/v8/vpn_client/config/{id}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v8-vpn_client-config-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-vpn_client-config-id) |
| `PUT /api/v8/vpn_client/config/{id}` | VPNClientConfig | VPNClientConfig ; json_envelope | [#put--api-v8-vpn_client-config-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-vpn_client-config-id) |
| `GET /api/v8/vpn_client/status` | Pas de corps décrit localement | VPNClientStatus ; json_envelope | [#get--api-v8-vpn_client-status](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-status) |
| `GET /api/v8/vpn_client/log` | Pas de corps décrit localement | string ; json_envelope | [#get--api-v8-vpn_client-log](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-log) |
| `GET /api/v8/player` | Pas de corps décrit localement | array of Player ; json_envelope | [#get--api-v8-player](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-player) |
| `GET /api/v8/player/{id_player}/api/v6/status/` | Pas de corps décrit localement | PlayerStatus ; json_envelope | [#get--api-v8-player-id_player-api-v6-status-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-player-id_player-api-v6-status-) |
| `POST /api/v8/player/{id_player}/api/v6/control/mediactrl/` | PlayerMediaCommand | None ; json_envelope | [#post--api-v8-player-id_player-api-v6-control-mediactrl-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-mediactrl-) |
| `GET /api/v8/player/{id_player}/api/v6/control/volume/` | Pas de corps décrit localement | PlayerVolume ; json_envelope | [#get--api-v8-player-id_player-api-v6-control-volume-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-player-id_player-api-v6-control-volume-) |
| `PUT /api/v8/player/{id_player}/api/v6/control/volume/` | PlayerVolume | PlayerVolume ; json_envelope | [#put--api-v8-player-id_player-api-v6-control-volume-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-player-id_player-api-v6-control-volume-) |
| `POST /api/v8/player/{id_player}/api/v6/control/open` | PlayerOpenRequest | None ; json_envelope | [#post--api-v8-player-id_player-api-v6-control-open](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-open) |
| `GET /api/v8/pvr/config/` | Pas de corps décrit localement | PvrConfig ; json_envelope | [#get--api-v8-pvr-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-config-) |
| `PUT /api/v8/pvr/config/` | PvrConfig | not_documented | [#put--api-v8-pvr-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-pvr-config-) |
| `GET /api/v8/pvr/quota/` | Pas de corps décrit localement | PvrQuota ; json_envelope | [#get--api-v8-pvr-quota-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-quota-) |
| `PUT /api/v8/pvr/quota/` | no arguments required; empty JSON object example | PvrQuota ; json_envelope | [#put--api-v8-pvr-quota-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-pvr-quota-) |
| `GET /api/v8/pvr/programmed/` | Pas de corps décrit localement | array of Precord ; json_envelope | [#get--api-v8-pvr-programmed-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-programmed-) |
| `GET /api/v8/pvr/programmed/{id}` | Pas de corps décrit localement | Precord ; json_envelope | [#get--api-v8-pvr-programmed-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-programmed-id) |
| `PUT /api/v8/pvr/programmed/{id}` | Precord | Precord ; json_envelope | [#put--api-v8-pvr-programmed-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-pvr-programmed-id) |
| `DELETE /api/v8/pvr/programmed/{id}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v8-pvr-programmed-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-pvr-programmed-id) |
| `POST /api/v8/pvr/programmed/` | Precord | Precord ; json_envelope | [#post--api-v8-pvr-programmed-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-pvr-programmed-) |
| `GET /api/v8/pvr/finished/` | Pas de corps décrit localement | array of Frecord ; json_envelope | [#get--api-v8-pvr-finished-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-finished-) |
| `GET /api/v8/pvr/finished/{id}` | Pas de corps décrit localement | Frecord ; json_envelope | [#get--api-v8-pvr-finished-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-finished-id) |
| `PUT /api/v8/pvr/finished/{id}` | Frecord | Frecord ; json_envelope | [#put--api-v8-pvr-finished-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-pvr-finished-id) |
| `DELETE /api/v8/pvr/finished/{id}` | Pas de corps décrit localement | None ; json_envelope | [#delete--api-v8-pvr-finished-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-pvr-finished-id) |
| `GET /api/v8/pvr/media/` | Pas de corps décrit localement | array of Media ; json_envelope | [#get--api-v8-pvr-media-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-media-) |

**Pagination contacts** : GET `/contact/` a les paramètres query `start` (offset int), `limit` (int, `-1` = aucune limite), `group_id` (int, filtre groupe). Défauts, bornes et obligation de présence ne sont pas publiés. Les opérations de gestion de groupe ne sont pas décrites ici.

**Corps modifiables** : CallEntry.new et VoicemailEntry.read sont les seuls champs non Read-only de leurs objets. Les exemples de modification partielle sont conservés, mais n’établissent pas une règle universelle de clearing par `null`. Séparer créations, modifications et réponses ; ne pas envoyer les propriétés Read-only depuis un DTO de réponse complet.

**VPN** : seul PUT `/vpn/openvpn_routed/config/` est nommé dans la référence ; aucun PUT générique `/vpn/{vpn_id}/config/` n’est inventé. Le client VPN n’a ni start, ni stop, ni PUT status dans le HTML brut : `active` est modifiable via PUT `/vpn_client/config/{id}`. Un seul profil client est actif à la fois. OpenVPN apparaît comme type client, mais son schéma d’import/configuration dédié n’est pas publié ici.

**Player** : `cmd` autorise `play_pause`, `stop`, `prev`, `next`, `select_stream`, `select_audio_track`, `select_srt_track`. Les capacités runtime ne suffisent pas à inventer d’autres commandes seek/shuffle/repeat. Volume maître entier de **0 à100**, mute booléen. Open `type` est explicitement optionnel, valeur par défaut chaîne vide ; les URL peuvent être `tv:?channel=2`, HTTP ou HTTPS, sans restriction générale à HTTP.

**PVR** : seules les programmations manuelles sont modifiables directement ; `enabled` concerne les programmations générées. Supprimer un Frecord supprime également ses fichiers. PUT quota demande le seuil suivant sans argument, avec un objet JSON vide dans l’exemple. PUT config n’a aucun exemple de réponse : ne pas promettre un PvrConfig résultat. Les unités des marges ne sont pas indiquées.

## Authentification, permissions et états

La règle commune `#authentication` authentifie toutes les API sauf exception indiquée ; aucune exception publique n’apparaît dans ce lot. Le tableau `#opening-a-session` établit `contacts` pour la liste de contacts, `calls` pour les journaux d’appels, `pvr` pour PVR, et `settings` pour la modification des réglages. Lecture des réglages généralement autorisée, avec champs sensibles masqués selon le protocole. Une permission absente est fausse ; la table ne permet pas d’inventer un champ de permission supplémentaire.

Le téléchargement de configuration VPN exige explicitement **`settings`**. Call account/voicemail et Player n’énoncent pas de clé de permission locale supplémentaire : ne pas assimiler leur absence à des requêtes publiques. Les références erronées vers VPNUser/VPNServerConfig ne remplacent pas l’examen des formes filaires réelles.

Les codes d’erreur de chaque module sont enregistrés dans chacune de ses opérations. Les exemples montrent généralement HTTP200 ; aucune correspondance exhaustive codeAPI/statutHTTP n’est donnée. Les nouveaux contrats conservent les erreurs inconnues et les propriétés diagnostics sans mettre des valeurs sensibles dans un message d’exception.

## Modèles et champs — inventaire complet

Chaque modèle ci-dessous conserve le type déclaré, la présence et la nullabilité distinguées, l’accès, les contraintes et la preuve. `not_documented` en accès signifie que la déclaration n’a ni marque Read-only ni Write-only ; ce n’est pas une autorisation d’écriture générale. Les sept champs d’exemple sont identifiés séparément. Le champ imbriqué `VPNUser.conf_wireguard` existe comme objet dans le brut, mais n’est pas une entrée property autonome du catalogue.

### AirMediaConfig

Preuve : [AirMediaConfig](http://mafreebox.freebox.fr/doc/index.html#AirMediaConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `enabled` | bool | not_documented | not_documented | not_documented | Enable/Disable the airmedia server [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaConfig.enabled) |
| `password` | string | not_documented | not_documented | write_only | If not empty, the client will have to enter a password to be able to use this airmedia server Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaConfig.password) |

### AirMediaReceiver

Preuve : [AirMediaReceiver](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiver) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `name` | string | not_documented | not_documented | read_only | AirMedia name [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiver.name) |
| `password_protected` | bool | not_documented | not_documented | read_only | Is set to true the receiver is protected by a password [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiver.password_protected) |
| `capabilities` | map | not_documented | not_documented | read_only | List of receiver capabilities from the following list Capability Description photo can display photos audio can play audio files video can play video files screen can display remote screen [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiver.capabilities) |

### AirMediaReceiverRequest

Preuve : [AirMediaReceiverRequest](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `action` | enum | not_documented | not_documented | not_documented | Action Description start start playing a media stop stop playing a media [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.action) |
| `media_type` | string | not_documented | not_documented | not_documented | Media Type Description photo display a photo video display a video [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.media_type) |
| `password` | string | no | not_documented | not_documented | Optional receiver password. Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.password) |
| `position` | int | not_documented | not_documented | not_documented | Start position for a video. The start position is expressed in percent * 1000, for instance 50000 means 50% of the video 50000 means 50%; this is not elapsed seconds. Valid bounds are not explicitly stated. Unité: percent * 1000. [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.position) |
| `media` | string | conditional | not_documented | not_documented | The media to play. For video media, you have to specify the media URL, for instance http://anon.nasa-global.edgesuite.net/HD_downloads/GRAIL_launch_480.mov For photo media, you have to specify the file path on the Freebox Server (base64 encoded as returned in fs/ls call), for instance L0Rpc3F1ZSBkdXIvUGhvdG9zL1JvY2tldHMvRFNDXzM0OTEuanBn photo uses the Base64 filesystem path returned by fs/ls; video uses a media URL. Do not treat both values as URI. [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.media) |

### CallEntry

Preuve : [CallEntry](http://mafreebox.freebox.fr/doc/index.html#CallEntry) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | int | not_documented | not_documented | read_only | id [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.id) |
| `type` | enum | not_documented | not_documented | read_only | The valid call types are: Type Description missed Missed incoming call accepted Incoming call outgoing Outgoing call [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.type) |
| `datetime` | timestamp | not_documented | not_documented | read_only | Call creation timestamp. Unité: timestamp; Unix seconds under common timestamp convention, dependent on protocol review. [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.datetime) |
| `number` | string | not_documented | not_documented | read_only | Callee number for outgoing calls. Caller number for incoming calls. [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.number) |
| `name` | string | not_documented | not_documented | read_only | Callee name for outgoing calls. Caller name for incoming calls. For incoming call if the network does not provide a contact name, we try to use the contact database to find a suitable name [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.name) |
| `duration` | int | not_documented | not_documented | read_only | Call duration in seconds. Unité: seconds. [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.duration) |
| `new` | bool | not_documented | not_documented | not_documented | Call entry has not been acknowledged yet. [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.new) |
| `contact_id` | int | not_documented | not_documented | read_only | If the number matches an entry in the contact database, the id of the matching contact. [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.contact_id) |
| `line_id` | integer | not_documented | not_documented | not_documented | Present in a response example but absent from the source property declarations; no write permission or completeness inference. [example_only_field_not_in_catalogue] [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-log-) |

### VoicemailEntry

Preuve : [VoicemailEntry](http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | string | not_documented | not_documented | read_only | id [preuve](http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry.id) |
| `country_code` | string | not_documented | not_documented | read_only | Country code part of the caller number. May be empty. [preuve](http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry.country_code) |
| `phone_number` | string | not_documented | not_documented | read_only | Caller number. May be empty. [preuve](http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry.phone_number) |
| `date` | timestamp | not_documented | not_documented | read_only | Voicemail creation timestamp. Unité: timestamp; Unix seconds under common timestamp convention, dependent on protocol review. [preuve](http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry.date) |
| `read` | bool | not_documented | not_documented | not_documented | Voicemail read status [preuve](http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry.read) |
| `duration` | int | not_documented | not_documented | read_only | Voicemail duration in seconds Unité: seconds. [preuve](http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry.duration) |

### ContactEntry

Preuve : [ContactEntry](http://mafreebox.freebox.fr/doc/index.html#ContactEntry) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | int | not_documented | not_documented | not_documented | contact id [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.id) |
| `display_name` | string | not_documented | not_documented | not_documented | contact display name [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.display_name) |
| `first_name` | string | not_documented | not_documented | not_documented | contact first name [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.first_name) |
| `last_name` | string | not_documented | not_documented | not_documented | contact last name [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.last_name) |
| `company` | string | not_documented | not_documented | not_documented | contact company name [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.company) |
| `photo_url` | string | not_documented | not_documented | not_documented | contact photo URL NOTE the photo URL can be embedded (for instance “ data:image/jpeg;base64,/9j/4AA [ … ]”) [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.photo_url) |
| `last_update` | timestamp | not_documented | not_documented | not_documented | contact last modification timestamp Unité: timestamp; Unix seconds under common timestamp convention, dependent on protocol review. [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.last_update) |
| `notes` | string | not_documented | not_documented | not_documented | contact last modification timestamp [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.notes) |
| `addresses` | [] array of ContactAddress | not_documented | not_documented | not_documented | list of contact postal addresses [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.addresses) |
| `emails` | [] array of ContactEmail | not_documented | not_documented | not_documented | list of contact email addresses [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.emails) |
| `numbers` | [] array of ContactNumber | not_documented | not_documented | not_documented | list of contact phone numbers [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.numbers) |
| `urls` | [] array of ContactUrl | not_documented | not_documented | not_documented | list of contact URL [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEntry.urls) |
| `birthday` | string | not_documented | not_documented | not_documented | Present in a response example but absent from the source property declarations; no write permission or completeness inference. [example_only_field_not_in_catalogue] [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-contact-) |

### ContactNumber

Preuve : [ContactNumber](http://mafreebox.freebox.fr/doc/index.html#ContactNumber) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | int | not_documented | not_documented | not_documented | address id [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactNumber.id) |
| `contact_id` | int | not_documented | not_documented | not_documented | id of the related contact [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactNumber.contact_id) |
| `type` | enum | not_documented | not_documented | not_documented | Type of number Type Description fixed fixed phone mobile mobile phone work work fax fax other other [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactNumber.type) |
| `number` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactNumber.number) |
| `is_default` | bool | not_documented | not_documented | not_documented | is this number the preferred contact phone number [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactNumber.is_default) |
| `is_own` | bool | not_documented | not_documented | not_documented | is this number the Freebox owner number [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactNumber.is_own) |

### ContactAddress

Preuve : [ContactAddress](http://mafreebox.freebox.fr/doc/index.html#ContactAddress) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | int | not_documented | not_documented | not_documented | address id [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.id) |
| `contact_id` | int | not_documented | not_documented | not_documented | id of the related contact [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.contact_id) |
| `type` | enum | not_documented | not_documented | not_documented | Type of email Type Description home home address work work address other other [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.type) |
| `number` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.number) |
| `street` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.street) |
| `street2` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.street2) |
| `city` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.city) |
| `zipcode` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.zipcode) |
| `country` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.country) |

### ContactUrl

Preuve : [ContactUrl](http://mafreebox.freebox.fr/doc/index.html#ContactUrl) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | int | not_documented | not_documented | not_documented | address id [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactUrl.id) |
| `contact_id` | int | not_documented | not_documented | not_documented | id of the related contact [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactUrl.contact_id) |
| `type` | enum | not_documented | not_documented | not_documented | Type of URL Type Description profile profile address blog blog address site website address other other [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactUrl.type) |
| `url` | string | not_documented | not_documented | not_documented | URL address [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactUrl.url) |

### ContactEmail

Preuve : [ContactEmail](http://mafreebox.freebox.fr/doc/index.html#ContactEmail) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | int | not_documented | not_documented | not_documented | address id [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEmail.id) |
| `contact_id` | int | not_documented | not_documented | not_documented | id of the related contact [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEmail.contact_id) |
| `type` | enum | not_documented | not_documented | not_documented | Type of address Type Description home home address work work address other other [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEmail.type) |
| `email` | string | not_documented | not_documented | not_documented | email address [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEmail.email) |

### FtpConfig

Preuve : [FtpConfig](http://mafreebox.freebox.fr/doc/index.html#FtpConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `enabled` | bool | not_documented | not_documented | not_documented | is the FTP server enabled [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.enabled) |
| `allow_anonymous` | bool | not_documented | not_documented | not_documented | can anonymous user log in [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.allow_anonymous) |
| `allow_anonymous_write` | bool | not_documented | not_documented | not_documented | can anonymous user write data [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.allow_anonymous_write) |
| `username` | string | not_documented | not_documented | read_only | default user name to use. Cannot be changed [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.username) |
| `password` | string | not_documented | not_documented | write_only | user password Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.password) |
| `allow_remote_access` | bool | not_documented | not_documented | not_documented | enable ftp server remote access NOTE: to be able to enable the remote access the password must be strong enough [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.allow_remote_access) |
| `weak_password` | bool | not_documented | not_documented | read_only | is the ftp password weak (in this case remote access is disabled) [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.weak_password) |
| `port_ctrl` | int | not_documented | not_documented | not_documented | ftp control port to use for remote access [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.port_ctrl) |
| `port_data` | int | not_documented | not_documented | not_documented | ftp data port to use for remote access [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.port_data) |
| `remote_domain` | string | not_documented | not_documented | not_documented | domain name to use for remote access [preuve](http://mafreebox.freebox.fr/doc/index.html#FtpConfig.remote_domain) |

### TftpConfig

Preuve : [TftpConfig](http://mafreebox.freebox.fr/doc/index.html#TftpConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `enabled` | bool | not_documented | not_documented | not_documented | is the TFTP server enabled [preuve](http://mafreebox.freebox.fr/doc/index.html#TftpConfig.enabled) |
| `root` | string | not_documented | not_documented | not_documented | is the base64 encoded absolute path to the root directory exposed by the server. This path points to a folder inside the storage device (My Freebox). [preuve](http://mafreebox.freebox.fr/doc/index.html#TftpConfig.root) |

### SambaConfig

Preuve : [SambaConfig](http://mafreebox.freebox.fr/doc/index.html#SambaConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `file_share_enabled` | bool | not_documented | not_documented | not_documented | is file sharing enabled [preuve](http://mafreebox.freebox.fr/doc/index.html#SambaConfig.file_share_enabled) |
| `print_share_enabled` | bool | not_documented | not_documented | not_documented | is printer sharing enabled [preuve](http://mafreebox.freebox.fr/doc/index.html#SambaConfig.print_share_enabled) |
| `logon_enabled` | bool | not_documented | not_documented | not_documented | is login/password required to access shares [preuve](http://mafreebox.freebox.fr/doc/index.html#SambaConfig.logon_enabled) |
| `logon_user` | string | not_documented | not_documented | not_documented | samba user name [preuve](http://mafreebox.freebox.fr/doc/index.html#SambaConfig.logon_user) |
| `logon_password` | string | not_documented | not_documented | write_only | samba user password Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#SambaConfig.logon_password) |
| `workgroup` | string | not_documented | not_documented | not_documented | name of the workgroup [preuve](http://mafreebox.freebox.fr/doc/index.html#SambaConfig.workgroup) |
| `smbv2_enabled` | bool | not_documented | not_documented | not_documented | Set to true to enable SMBv2/v3 [preuve](http://mafreebox.freebox.fr/doc/index.html#SambaConfig.smbv2_enabled) |

### AfpConfig

Preuve : [AfpConfig](http://mafreebox.freebox.fr/doc/index.html#AfpConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `enabled` | bool | not_documented | not_documented | not_documented | is afp service enabled [preuve](http://mafreebox.freebox.fr/doc/index.html#AfpConfig.enabled) |
| `guest_allow` | bool | not_documented | not_documented | not_documented | allow guest to access shared files [preuve](http://mafreebox.freebox.fr/doc/index.html#AfpConfig.guest_allow) |
| `server_type` | enum | not_documented | not_documented | not_documented | Afp server type (to display proper icon) in MacOS valid server types are: server_type powerbook powermac macmini imac macbook macbookpro macbookair macpro appletv airport xserve [preuve](http://mafreebox.freebox.fr/doc/index.html#AfpConfig.server_type) |
| `login_name` | string | not_documented | not_documented | not_documented | Afp user name [preuve](http://mafreebox.freebox.fr/doc/index.html#AfpConfig.login_name) |
| `login_password` | string | not_documented | not_documented | write_only | Afp user password Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#AfpConfig.login_password) |

### UPnPAVConfig

Preuve : [UPnPAVConfig](http://mafreebox.freebox.fr/doc/index.html#UPnPAVConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `enabled` | bool | not_documented | not_documented | not_documented | is the UPnP AV service enabled [preuve](http://mafreebox.freebox.fr/doc/index.html#UPnPAVConfig.enabled) |

### VPNServer

Preuve : [VPNServer](http://mafreebox.freebox.fr/doc/index.html#VPNServer) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `name` | string | not_documented | not_documented | read_only | VPN server name (id) [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServer.name) |
| `type` | enum | not_documented | not_documented | read_only | VPN server type type Description ipsec IPsec IKEv2 server pptp PPTP VPN server openvpn OpenVPN server wireguard WireGuard server [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServer.type) |
| `state` | enum | not_documented | not_documented | read_only | server state state stopped starting started stopping error [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServer.state) |
| `connection_count` | int | not_documented | not_documented | read_only | number of active connections [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServer.connection_count) |
| `auth_connection_count` | int | not_documented | not_documented | read_only | number of active connections that have passed authentication [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServer.auth_connection_count) |

### VPNPPTPConfig

Preuve : [VPNPPTPConfig](http://mafreebox.freebox.fr/doc/index.html#VPNPPTPConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `mppe` | enum | not_documented | not_documented | not_documented | mppe Description disable disable mppe require require mppe require_128 require 128 bits mppe [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNPPTPConfig.mppe) |
| `allowed_auth` | dict | not_documented | not_documented | not_documented | allowed authentication methods dictionnary with following entries: pap chap mschapv2 values are booleans. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNPPTPConfig.allowed_auth) |

### VPNOpenVpnConfig

Preuve : [VPNOpenVpnConfig](http://mafreebox.freebox.fr/doc/index.html#VPNOpenVpnConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `cipher` | enum | not_documented | not_documented | not_documented | cipher blowfish aes128 aes256 chacha20poly1305 [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNOpenVpnConfig.cipher) |
| `disable_fragment` | bool | not_documented | not_documented | not_documented | disable fragment configuration option [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNOpenVpnConfig.disable_fragment) |
| `use_tcp` | bool | not_documented | not_documented | not_documented | use TCP instead of UDP [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNOpenVpnConfig.use_tcp) |

### VPNWireGuardConfig

Preuve : [VPNWireGuardConfig](http://mafreebox.freebox.fr/doc/index.html#VPNWireGuardConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `mtu` | int | not_documented | not_documented | not_documented | wireguard device MTU. Value must be between 512 and 1420. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNWireGuardConfig.mtu) |

### VPNIPSecAuthMode

Preuve : [VPNIPSecAuthMode](http://mafreebox.freebox.fr/doc/index.html#VPNIPSecAuthMode) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id_source` | enum | not_documented | not_documented | not_documented | source of the connection id id_source custom [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNIPSecAuthMode.id_source) |
| `id_custom` | string | not_documented | not_documented | not_documented | value of the source id when id_source is custom [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNIPSecAuthMode.id_custom) |

### VPNIPSecConfig

Preuve : [VPNIPSecConfig](http://mafreebox.freebox.fr/doc/index.html#VPNIPSecConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `ike_version` | int | not_documented | not_documented | read_only | IKE protocol version [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNIPSecConfig.ike_version) |
| `auth_modes` | [] array of VPNIPSecAuthMode | not_documented | not_documented | read_only | map of supported auth modes, currently only psk is supported [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNIPSecConfig.auth_modes) |

### VPNServerConfig

Preuve : [VPNServerConfig](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | string | not_documented | not_documented | read_only | VPN server id [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.id) |
| `type` | enum | not_documented | not_documented | read_only | VPN server type type Description pptp PPTP VPN server openvpn OpenVPN server ipsec IPsec IKEv2 server wireguard WireGuard server [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.type) |
| `enabled` | bool | not_documented | not_documented | not_documented | is the VPN server enabled [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.enabled) |
| `enable_ipv4` | bool | not_documented | not_documented | not_documented | enable IPv4 on this server NOTE: Not relevant for openvpn_bridge, pptp and wireguard [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.enable_ipv4) |
| `enable_ipv6` | bool | not_documented | not_documented | not_documented | enable IPv6 on this server NOTE: Not relevant for openvpn_bridge, pptp and wireguard [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.enable_ipv6) |
| `port` | int | not_documented | not_documented | not_documented | the server port NOTE: you can only edit the server port when type is openvpn or wireguard [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.port) |
| `min_port` | int | not_documented | not_documented | read_only | This field indicate the minimum possible value for port (see ConnectionStatus ipv4_port_range) [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.min_port) |
| `max_port` | int | not_documented | not_documented | read_only | This field indicate the maximum possible value for port (see ConnectionStatus ipv4_port_range) [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.max_port) |
| `port_ike` | int | not_documented | not_documented | not_documented | IPSec ike server port NOTE: only present for ipsec server [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.port_ike) |
| `port_nat` | int | not_documented | not_documented | not_documented | IPSec nat server port NOTE: only present for ipsec server [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.port_nat) |
| `conf_pptp` | VPNPPTPConfig | not_documented | not_documented | not_documented | only available when type is PPTP [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.conf_pptp) |
| `conf_openvpn` | VPNOpenVpnConfig | not_documented | not_documented | not_documented | only available when type is OpenVPN [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.conf_openvpn) |
| `conf_ipsec` | VPNIPSecConfig | not_documented | not_documented | not_documented | only available when type is IPsec [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.conf_ipsec) |
| `conf_wireguard` | VPNWireGuardConfig | not_documented | not_documented | not_documented | only available when type is WireGuard [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.conf_wireguard) |
| `ip_start` | string | not_documented | not_documented | read_only | start of the IP range that will be used to give clients an IP [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.ip_start) |
| `ip_end` | string | not_documented | not_documented | read_only | end of the IP range that will be used to give clients an IP [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.ip_end) |
| `ip6_start` | string | not_documented | not_documented | read_only | start of the IPv6 range that will be used to give clients an IPv6 [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.ip6_start) |
| `ip6_end` | string | not_documented | not_documented | read_only | end of the IPv6 range that will be used to give clients an IPv6 [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.ip6_end) |

### VPNUser

Preuve : [VPNUser](http://mafreebox.freebox.fr/doc/index.html#VPNUser) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `login` | string | not_documented | not_documented | not_documented | VPN user login [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.login) |
| `type` | enum | not_documented | not_documented | not_documented | VPN user type type standard wireguard [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.type) |
| `password` | string | not_documented | not_documented | write_only | VPN user password (length must be between 8 and 32) Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.password) |
| `password_set` | bool | not_documented | not_documented | read_only | True if a password was provided for this user [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.password_set) |
| `ip_reservation` | ipv4 | conditional | not_documented | not_documented | You can specify the IP you want to assign to this user. If you don’t want to use a specific IP pass an empty string or omit this property. This field is required if the type property is set to ‘wireguard’. The IP must be in the VPN range (see ip_start, ip_end). [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.ip_reservation) |
| `conf_wireguard` | object conf_wireguard | conditional | not_documented | not_documented | This field is present only if the type property is set to ‘wireguard’. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.conf_wireguard) |

### conf_wireguard

Preuve : [VPNUser.conf_wireguard](http://mafreebox.freebox.fr/doc/index.html#VPNUser.conf_wireguard) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `keepalive` | int | not_documented | not_documented | not_documented | Interval in seconds at which keepalive packets are sent. Unité: seconds. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.conf_wireguard.keepalive) |
| `psk` | bool | not_documented | not_documented | not_documented | Enable optional preshared-key. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.conf_wireguard.psk) |

### VPNConnection

Preuve : [VPNConnection](http://mafreebox.freebox.fr/doc/index.html#VPNConnection) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | string | not_documented | not_documented | read_only | connection id [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.id) |
| `vpn` | strong | not_documented | not_documented | read_only | related VPN server id [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.vpn) |
| `user` | string | not_documented | not_documented | read_only | user login [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.user) |
| `authenticated` | bool | not_documented | not_documented | read_only | is the connection authenticated [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.authenticated) |
| `auth_time` | int | not_documented | not_documented | read_only | timestamp of the authentication Unité: timestamp; Unix seconds under common timestamp convention, dependent on protocol review. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.auth_time) |
| `src_ip` | ipv4 | not_documented | not_documented | read_only | connection source IP address [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.src_ip) |
| `src_port` | int | not_documented | not_documented | read_only | connection source port [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.src_port) |
| `local_ip` | int | not_documented | not_documented | read_only | attributed IP address from VPN adress pool [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.local_ip) |
| `rx_bytes` | int | not_documented | not_documented | read_only | rx bytes Unité: bytes. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.rx_bytes) |
| `tx_bytes` | int | not_documented | not_documented | read_only | tx bytes Unité: bytes. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.tx_bytes) |

### VPNClientConfig

Preuve : [VPNClientConfig](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | string | not_documented | not_documented | read_only | VPN config id [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.id) |
| `description` | string | not_documented | not_documented | not_documented | VPN description [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.description) |
| `type` | enum | not_documented | not_documented | not_documented | VPN server type type Description pptp PPTP VPN server openvpn OpenVPN server wireguard WireGuard server [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.type) |
| `active` | bool | not_documented | not_documented | not_documented | is this configuration active. Only one configuration is active at a time. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.active) |
| `conf_pptp` | VPNClientConfigPPTP | not_documented | not_documented | not_documented | only available when type is PPTP [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.conf_pptp) |
| `conf_wireguard` | VPNClientConfigWireGuard | not_documented | not_documented | not_documented | only available when type is WireGuard [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.conf_wireguard) |

### VPNClientConfigPPTP

Preuve : [VPNClientConfigPPTP](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `remote_host` | string | not_documented | not_documented | not_documented | remote host IP or name [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP.remote_host) |
| `username` | string | not_documented | not_documented | not_documented | VPN username [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP.username) |
| `password` | string | not_documented | not_documented | write_only | VPN password Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP.password) |
| `mppe` | enum | not_documented | not_documented | not_documented | mppe Description disable disable mppe require require mppe require_128 require 128 bits mppe [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP.mppe) |
| `allowed_auth` | dict | not_documented | not_documented | not_documented | allowed authentication methods dictionary with following keys: eap pap chap mschap mschapv2 values are booleans. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP.allowed_auth) |

### VPNClientConfigWireGuard

Preuve : [VPNClientConfigWireGuard](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `remote_addr` | string | not_documented | not_documented | not_documented | remote host IP [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard.remote_addr) |
| `remote_port` | int | not_documented | not_documented | not_documented | remote host port [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard.remote_port) |
| `remote_public_key` | string | not_documented | not_documented | not_documented | remote host public key [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard.remote_public_key) |
| `remote_preshared_key` | string | no | not_documented | not_documented | optional preshared key Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard.remote_preshared_key) |
| `local_priv_key` | string | not_documented | not_documented | not_documented | local private key Valeur sensible : ne jamais journaliser. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard.local_priv_key) |
| `local_addr` | [] array of VPNClientConfigWireGuardIP | not_documented | not_documented | not_documented | IPs to assign to the local interface. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard.local_addr) |
| `dns` | [] array of string | not_documented | not_documented | not_documented | list of strings containing IPs of DNS servers to use. Both IPv4 and IPv6 are supported. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuard.dns) |
| `mtu` | integer | not_documented | not_documented | not_documented | Present in a response example but absent from the source property declarations; no write permission or completeness inference. [example_only_field_not_in_catalogue] [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-config-) |

### VPNClientConfigWireGuardIP

Preuve : [VPNClientConfigWireGuardIP](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuardIP) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `ip` | string | not_documented | not_documented | not_documented | string representation of an IPv4 or IPv6 address [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuardIP.ip) |
| `len` | int | not_documented | not_documented | not_documented | prefix length associated with the IP address [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigWireGuardIP.len) |

### VPNClientStatus

Preuve : [VPNClientStatus](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `enabled` | bool | not_documented | not_documented | read_only | is VPN client enabled [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.enabled) |
| `active_vpn` | string | not_documented | not_documented | read_only | active VPN id [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.active_vpn) |
| `active_vpn_description` | string | not_documented | not_documented | read_only | active VPN description [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.active_vpn_description) |
| `type` | enum | not_documented | not_documented | read_only | active VPN type type Description pptp PPTP VPN server openvpn OpenVPN server wireguard WireGuard server [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.type) |
| `state` | enum | not_documented | not_documented | read_only | state Description waiting_wan waiting for wan connection going_up connecting up connected going_down disconnecting down disconnected [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.state) |
| `last_up` | int | not_documented | not_documented | read_only | timestamp of last successful connection Unité: timestamp; Unix seconds under common timestamp convention, dependent on protocol review. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.last_up) |
| `last_try` | int | not_documented | not_documented | read_only | timestamp of last connection attempt Unité: timestamp; Unix seconds under common timestamp convention, dependent on protocol review. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.last_try) |
| `next_try` | int | not_documented | not_documented | read_only | seconds left until next connection attempt Unité: seconds. [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.next_try) |
| `last_error` | enum | not_documented | not_documented | read_only | last_error Description none no error internal internal error authentication_failed wrong credentials auth_failed wrong credentials resolv_failed invalid host name connect_timeout connection timeout connect_failed connection failed setup_control_failed PPTP session negotiation failure setup_call_failed PPTP session failure protocol protocol error remote_terminated connection closed by remote peer remote_disconnect connection closed by remote peer [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.last_error) |
| `stats` | VpnClientStats | not_documented | not_documented | read_only | connection statistics [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.stats) |
| `IPv4` | VpnClientIpInfo | not_documented | not_documented | read_only | connection IPv4 information [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.IPv4) |

### VpnClientStats

Preuve : [VpnClientStats](http://mafreebox.freebox.fr/doc/index.html#VpnClientStats) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `rate_up` | int | not_documented | not_documented | read_only | current upload rate (in byte/s) Unité: bytes/second. [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientStats.rate_up) |
| `rate_down` | int | not_documented | not_documented | read_only | current download rate (in byte/s) Unité: bytes/second. [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientStats.rate_down) |
| `bytes_up` | int | not_documented | not_documented | read_only | total bytes uploaded Unité: bytes. [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientStats.bytes_up) |
| `bytes_down` | int | not_documented | not_documented | read_only | total bytes downloaded Unité: bytes. [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientStats.bytes_down) |

### VpnClientIpInfo

Preuve : [VpnClientIpInfo](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `config_valid` | bool | not_documented | not_documented | read_only | is the configuration valid [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.config_valid) |
| `ip_mask` | dict | not_documented | not_documented | read_only | assigned IP and netmask [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.ip_mask) |
| `domain` | string | not_documented | not_documented | read_only | provided domain [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.domain) |
| `gateway` | IPv4 | not_documented | not_documented | read_only | provided gateway [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.gateway) |
| `dns` | [] array of ipv4 | not_documented | not_documented | read_only | list of dns servers [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.dns) |
| `provider` | enum | not_documented | not_documented | read_only | ip_mask source provider Description none none static static IP configuration ppp ppp dhcp DHCP server [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.provider) |
| `routes` | list | not_documented | not_documented | read_only | list of provided routes [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.routes) |
| `dhcp` | dict | not_documented | not_documented | read_only | DHCP status information [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.dhcp) |

### Player

Preuve : [Player](http://mafreebox.freebox.fr/doc/index.html#Player) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | int | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#Player.id) |
| `device_name` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#Player.device_name) |
| `uid` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#Player.uid) |
| `reachable` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#Player.reachable) |
| `api_version` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#Player.api_version) |
| `api_available` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#Player.api_available) |
| `stb_type` | string | not_documented | not_documented | not_documented | Present in a response example but absent from the source property declarations; no write permission or completeness inference. [example_only_field_not_in_catalogue] [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-player) |

### PlayerStatusForegroundApp

Preuve : [PlayerStatusForegroundApp](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusForegroundApp) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `package_id` | id | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusForegroundApp.package_id) |
| `cur_url` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusForegroundApp.cur_url) |
| `context` | object | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusForegroundApp.context) |
| `package` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusForegroundApp.package) |

### PlayerStatusCapabilities

Preuve : [PlayerStatusCapabilities](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `play` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.play) |
| `pause` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.pause) |
| `stop` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.stop) |
| `next` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.next) |
| `prev` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.prev) |
| `record` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.record) |
| `record_stop` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.record_stop) |
| `seek_forward` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.seek_forward) |
| `seek_backward` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.seek_backward) |
| `seek_to` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.seek_to) |
| `shuffle` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.shuffle) |
| `repeat_all` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.repeat_all) |
| `repeat_one` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.repeat_one) |
| `select_stream` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.select_stream) |
| `select_audio_track` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.select_audio_track) |
| `select_srt_track` | bool | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusCapabilities.select_srt_track) |

### PlayerStatusInformations

Preuve : [PlayerStatusInformations](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusInformations) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `name` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusInformations.name) |
| `last_activity` | long | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusInformations.last_activity) |
| `capabilities` | PlayerStatusCapabilities | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusInformations.capabilities) |

### PlayerStatus

Preuve : [PlayerStatus](http://mafreebox.freebox.fr/doc/index.html#PlayerStatus) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `power_state` | string | not_documented | not_documented | not_documented |  [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatus.power_state) |
| `player` | PlayerStatusInformations | not_documented | not_documented | not_documented | State of the active media player on the device. [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatus.player) |
| `foreground_app` | PlayerStatusForegroundApp | not_documented | not_documented | not_documented | The context of the currently running application. The fields exposed in this object are left to the discretion of the application author, and thus subject to change at any time. [preuve](http://mafreebox.freebox.fr/doc/index.html#PlayerStatus.foreground_app) |

### PvrConfig

Preuve : [PvrConfig](http://mafreebox.freebox.fr/doc/index.html#PvrConfig) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `margin_before` | int | not_documented | not_documented | not_documented | default margin before recording start time [preuve](http://mafreebox.freebox.fr/doc/index.html#PvrConfig.margin_before) |
| `margin_after` | int | not_documented | not_documented | not_documented | default margin after recording end time [preuve](http://mafreebox.freebox.fr/doc/index.html#PvrConfig.margin_after) |

### PvrQuota

Preuve : [PvrQuota](http://mafreebox.freebox.fr/doc/index.html#PvrQuota) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `quota_exceeded` | bool | not_documented | not_documented | not_documented | is quota exceeded [preuve](http://mafreebox.freebox.fr/doc/index.html#PvrQuota.quota_exceeded) |
| `needed_tresh` | int | not_documented | not_documented | not_documented | needed quota threshold [preuve](http://mafreebox.freebox.fr/doc/index.html#PvrQuota.needed_tresh) |
| `cur_tresh` | int | not_documented | not_documented | not_documented | current quota threshold [preuve](http://mafreebox.freebox.fr/doc/index.html#PvrQuota.cur_tresh) |

### Precord

Preuve : [Precord](http://mafreebox.freebox.fr/doc/index.html#Precord) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | string | not_documented | not_documented | read_only | precord id [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.id) |
| `media` | string | not_documented | not_documented | not_documented | media name on which the record will be written to. See the Media API for more info. This property and can be empty when the file backing the record is not available, for example when secure is set. [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.media) |
| `path` | string | not_documented | not_documented | not_documented | destination directory on the media storage where the record will be written to [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.path) |
| `has_record_gen` | bool | not_documented | not_documented | read_only | if true, this precord has been generated using a Generator [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.has_record_gen) |
| `record_gen_id` | int | not_documented | not_documented | read_only | if has_record_gen, this is the id of the generator [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.record_gen_id) |
| `conflict` | bool | not_documented | not_documented | read_only | if true this record may conflict with another record [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.conflict) |
| `overlap_list` | [] array of int | not_documented | not_documented | read_only | in case of conflict, this will contain the list of records id that may conflict with this record [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.overlap_list) |
| `enabled` | bool | not_documented | not_documented | not_documented | it only applies to generated records. If false the generated precord will be skipped. [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.enabled) |
| `altered` | bool | not_documented | not_documented | read_only | a precord is altered when some part of the recording may be missing. This can be the case if a conflict occurred during the recording (or connection was down) [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.altered) |
| `state` | enum | not_documented | not_documented | read_only | State Description disabled disabled start_error failed to start waiting_start_time scheduled starting starting running running running_error running with error failed failed finished finished [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.state) |
| `error` | enum | not_documented | not_documented | read_only | Error none file_access_error disk_full private_but_no_private_dir network_problem resource_problem no_stream_available no_data_received missed stopped internal_error unknown_error [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.error) |
| `channel_uuid` | string | not_documented | not_documented | not_documented | channel uuid [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.channel_uuid) |
| `channel_name` | string | no | not_documented | not_documented | optional channel name [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.channel_name) |
| `channel_quality` | enum | not_documented | not_documented | not_documented | channel_quality auto hd sd ld 3d [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.channel_quality) |
| `channel_type` | enum | not_documented | not_documented | not_documented | channel_type Description ‘’ (empty string) auto iptv use only iptv streams dvb use only dvb streams [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.channel_type) |
| `name` | string | not_documented | not_documented | not_documented | record name [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.name) |
| `subname` | string | not_documented | not_documented | not_documented | record subname [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.subname) |
| `broadcast_type` | enum | not_documented | not_documented | not_documented | broadcast_type tv radio [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.broadcast_type) |
| `start` | int | not_documented | not_documented | not_documented | record start timestamp Unité: timestamp; Unix seconds under common timestamp convention, dependent on protocol review. [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.start) |
| `end` | int | not_documented | not_documented | not_documented | record end timestamp Unité: timestamp; Unix seconds under common timestamp convention, dependent on protocol review. [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.end) |
| `legacy_uri` | string | not_documented | not_documented | not_documented | only used for legacy apps. Use channel_uuid instead when available NOTE: only visible when called from player Only visible when called from player; prefer channel_uuid when available. No deprecated mark is present. [retained_legacy_conditional_not_deprecated] [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.legacy_uri) |
| `force_channel_name` | string | not_documented | not_documented | not_documented | only used for legacy apps. Use channel_uuid instead when available NOTE: only visible when called from player Only visible when called from player; prefer channel_uuid when available. No deprecated mark is present. [retained_legacy_conditional_not_deprecated] [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.force_channel_name) |
| `margin_before` | integer | not_documented | not_documented | not_documented | Present in a response example but absent from the source property declarations; no write permission or completeness inference. [example_only_field_not_in_catalogue] [preuve](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-pvr-programmed-) |
| `margin_after` | integer | not_documented | not_documented | not_documented | Present in a response example but absent from the source property declarations; no write permission or completeness inference. [example_only_field_not_in_catalogue] [preuve](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-pvr-programmed-) |

### Frecord

Preuve : [Frecord](http://mafreebox.freebox.fr/doc/index.html#Frecord) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `id` | string | not_documented | not_documented | read_only | frecord id [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.id) |
| `media` | string | not_documented | not_documented | read_only | media name on which the record is written. See the Media API for more info. This property and can be empty when the file backing the record is not available, for example when secure is set. [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.media) |
| `path` | string | not_documented | not_documented | read_only | destination directory on the media storage [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.path) |
| `filename` | string | not_documented | not_documented | read_only | filename of the record [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.filename) |
| `byte_size` | int | not_documented | not_documented | read_only | size of the record file in bytes Unité: bytes. [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.byte_size) |
| `has_record_gen` | bool | not_documented | not_documented | read_only | if true, this frecord has been generated using a Generator [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.has_record_gen) |
| `record_gen_id` | int | not_documented | not_documented | read_only | if has_record_gen, this is the id of the generator [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.record_gen_id) |
| `altered` | bool | not_documented | not_documented | read_only | an frecord is altered when some part of the recording may be missing. This can be the case if a conflict occurred during the recording (or connection was down) [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.altered) |
| `state` | enum | not_documented | not_documented | read_only | State Description disabled disabled start_error failed to start waiting_start_time scheduled starting starting running running running_error running with error failed failed finished finished [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.state) |
| `error` | enum | not_documented | not_documented | read_only | Error none file_access_error disk_full private_but_no_private_dir network_problem resource_problem no_stream_available no_data_received missed stopped internal_error unknown_error [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.error) |
| `channel_uuid` | string | not_documented | not_documented | read_only | channel uuid [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.channel_uuid) |
| `channel_name` | string | no | not_documented | read_only | optional channel name [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.channel_name) |
| `channel_quality` | enum | not_documented | not_documented | read_only | channel_quality auto hd sd ld 3d [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.channel_quality) |
| `channel_type` | enum | not_documented | not_documented | read_only | channel_type Description ‘’ (empty string) auto iptv use only iptv streams dvb use only dvb streams [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.channel_type) |
| `name` | string | not_documented | not_documented | not_documented | record name [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.name) |
| `subname` | string | not_documented | not_documented | not_documented | record subname [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.subname) |
| `broadcast_type` | enum | not_documented | not_documented | read_only | broadcast_type tv radio [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.broadcast_type) |
| `start` | int | not_documented | not_documented | read_only | record start timestamp [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.start) |
| `end` | int | not_documented | not_documented | read_only | record end timestamp [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.end) |
| `secure` | bool | not_documented | not_documented | read_only | flag set when the record is protected by DRM [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.secure) |
| `enabled` | boolean | not_documented | not_documented | not_documented | Present in a response example but absent from the source property declarations; no write permission or completeness inference. [example_only_field_not_in_catalogue] [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-finished-) |

### Media

Preuve : [Media](http://mafreebox.freebox.fr/doc/index.html#Media) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `media` | string | not_documented | not_documented | read_only | name of the storage medium [preuve](http://mafreebox.freebox.fr/doc/index.html#Media.media) |
| `free_bytes` | int | not_documented | not_documented | read_only | number of free bytes on the medium Unité: bytes. [preuve](http://mafreebox.freebox.fr/doc/index.html#Media.free_bytes) |
| `total_bytes` | int | not_documented | not_documented | read_only | total number of bytes on the medium Raw unanchored declaration says total bytes int [ro]; the response example spells the JSON key total_bytes. Unité: bytes. [preuve](http://mafreebox.freebox.fr/doc/index.html#Media) |
| `record_time` | int | not_documented | not_documented | read_only | estimated record time in seconds for multiple channel types and qualities Unité: seconds. [preuve](http://mafreebox.freebox.fr/doc/index.html#Media.record_time) |

### CallAccount

Preuve : [get--api-v10-call-account](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-account) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `phone_number` | string | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-account) |

### VPNIpPool

Preuve : [get--api-v8-vpn-ip_pool-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `ip_start` | string | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-) |
| `ip_end` | string | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-) |
| `reservations` | array of VPNIpReservation | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-) |

### VPNIpReservation

Preuve : [get--api-v8-vpn-ip_pool-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `login` | string | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-) |
| `ip` | string | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-ip_pool-) |

### PlayerVolume

Preuve : [put--api-v8-player-id_player-api-v6-control-volume-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-player-id_player-api-v6-control-volume-) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `volume` | integer 0..100 | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-player-id_player-api-v6-control-volume-) |
| `mute` | boolean | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-player-id_player-api-v6-control-volume-) |

### PlayerMediaCommand

Preuve : [post--api-v8-player-id_player-api-v6-control-mediactrl-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-mediactrl-) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `cmd` | string | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-mediactrl-) |

### PlayerOpenRequest

Preuve : [post--api-v8-player-id_player-api-v6-control-open](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-open) @ `sha256:cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

| Champ | Type brut | Présence | null | Accès | Contraintes / preuve |
| --- | --- | --- | --- | --- | --- |
| `url` | string | not_documented | not_documented | not_documented | Parameter/example documented; no complete required/null schema. [preuve](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-open) |
| `type` | string | no | not_documented | not_documented | Parameter/example documented; no complete required/null schema. Explicitly optional; default is empty string. [preuve](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-open) |

## Enums et dictionnaires

Les valeurs ci-dessous viennent des tables réellement lues. Politique proposée : en réponse préserver la chaîne inconnue avec un type valeur ouvert ; en requête offrir les valeurs nommées documentées. Le caractère UNSTABLE et l’absence de promesse de fermeture justifient cette proposition, qui reste à accepter par l’intégrateur. Les noms filaires ne sont pas les noms C#.

| Champ | Valeurs filaires exactes | Preuve |
| --- | --- | --- |
| AirMediaReceiverRequest.action | `start`, `stop` | [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.action) |
| AirMediaReceiverRequest.media_type | `photo`, `video` | [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.media_type) |
| CallEntry.type | `missed`, `accepted`, `outgoing` | [preuve](http://mafreebox.freebox.fr/doc/index.html#CallEntry.type) |
| ContactNumber.type | `fixed`, `mobile`, `work`, `fax`, `other` | [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactNumber.type) |
| ContactAddress.type | `home`, `work`, `other` | [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactAddress.type) |
| ContactUrl.type | `profile`, `blog`, `site`, `other` | [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactUrl.type) |
| ContactEmail.type | `home`, `work`, `other` | [preuve](http://mafreebox.freebox.fr/doc/index.html#ContactEmail.type) |
| AfpConfig.server_type | `powerbook`, `powermac`, `macmini`, `imac`, `macbook`, `macbookpro`, `macbookair`, `macpro`, `appletv`, `airport`, `xserve` | [preuve](http://mafreebox.freebox.fr/doc/index.html#AfpConfig.server_type) |
| VPNServer.type | `ipsec`, `pptp`, `openvpn`, `wireguard` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServer.type) |
| VPNServer.state | `stopped`, `starting`, `started`, `stopping`, `error` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServer.state) |
| VPNPPTPConfig.mppe | `disable`, `require`, `require_128` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNPPTPConfig.mppe) |
| VPNOpenVpnConfig.cipher | `blowfish`, `aes128`, `aes256`, `chacha20poly1305` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNOpenVpnConfig.cipher) |
| VPNIPSecAuthMode.id_source | `custom` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNIPSecAuthMode.id_source) |
| VPNServerConfig.type | `pptp`, `openvpn`, `ipsec`, `wireguard` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNServerConfig.type) |
| VPNUser.type | `standard`, `wireguard` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNUser.type) |
| VPNClientConfig.type | `pptp`, `openvpn`, `wireguard` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.type) |
| VPNClientConfigPPTP.mppe | `disable`, `require`, `require_128` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfigPPTP.mppe) |
| VPNClientStatus.type | `pptp`, `openvpn`, `wireguard` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.type) |
| VPNClientStatus.state | `waiting_wan`, `going_up`, `up`, `going_down`, `down` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.state) |
| VPNClientStatus.last_error | `none`, `internal`, `authentication_failed`, `auth_failed`, `resolv_failed`, `connect_timeout`, `connect_failed`, `setup_control_failed`, `setup_call_failed`, `protocol`, `remote_terminated`, `remote_disconnect` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.last_error) |
| VpnClientIpInfo.provider | `none`, `static`, `ppp`, `dhcp` | [preuve](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.provider) |
| Precord.state | `disabled`, `start_error`, `waiting_start_time`, `starting`, `running`, `running_error`, `failed`, `finished` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.state) |
| Precord.error | `none`, `file_access_error`, `disk_full`, `private_but_no_private_dir`, `network_problem`, `resource_problem`, `no_stream_available`, `no_data_received`, `missed`, `stopped`, `internal_error`, `unknown_error` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.error) |
| Precord.channel_quality | `auto`, `hd`, `sd`, `ld`, `3d` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.channel_quality) |
| Precord.channel_type | `""`, `iptv`, `dvb` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.channel_type) |
| Precord.broadcast_type | `tv`, `radio` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Precord.broadcast_type) |
| Frecord.state | `disabled`, `start_error`, `waiting_start_time`, `starting`, `running`, `running_error`, `failed`, `finished` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.state) |
| Frecord.error | `none`, `file_access_error`, `disk_full`, `private_but_no_private_dir`, `network_problem`, `resource_problem`, `no_stream_available`, `no_data_received`, `missed`, `stopped`, `internal_error`, `unknown_error` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.error) |
| Frecord.channel_quality | `auto`, `hd`, `sd`, `ld`, `3d` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.channel_quality) |
| Frecord.channel_type | `""`, `iptv`, `dvb` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.channel_type) |
| Frecord.broadcast_type | `tv`, `radio` | [preuve](http://mafreebox.freebox.fr/doc/index.html#Frecord.broadcast_type) |
| PlayerMediaCommand.cmd | `play_pause`, `stop`, `prev`, `next`, `select_stream`, `select_audio_track`, `select_srt_track` | [preuve](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-player-id_player-api-v6-control-mediactrl-) |

Dictionnaires explicitement nommés : AirMedia.capabilities `photo/audio/video/screen` → bool ; VPNPPTPConfig.allowed_auth `pap/chap/mschapv2` → bool ; VPNClientConfigPPTP.allowed_auth `eap/pap/chap/mschap/mschapv2` → bool. Une clé absente n’est pas documentée comme false. La présence de stats/routes/DHCP/context ouverts ne permet pas de leur inventer un schéma fermé.

## Durées de vie, flux bruts et concurrence

| Opération | Réponse documentée | Contraintes utiles |
| --- | --- | --- |
| Voicemail audio_file | `audio/wav`, bytes bruts, Content-Disposition inline/filename, taille exemple60218 | Aucun JSON envelope ; streaming annulable, taille exemple jamais limite fixe |
| VPN download_config plain | `application/x-openvpn-profile`, Content-Disposition attachment/filename, Transfer-Encoding chunked dans l’exemple | `fmt` doit être plain ou json ; MIME WireGuard/schémaJSON non décrits |

**GET VPN download_config a un effet de mutation explicite** : “each time you download a new OpenVPN configuration file for a given user, you invalidate previous configuration file emitted for this user”. Il faut une commande utilisateur explicite et **aucun replay automatique**, même pour GET. L’invalidation WireGuard n’est pas déclarée par cette phrase.

Le futur handle de téléchargement possède la réponseHTTP, son contenu et son stream jusqu’à son Dispose/DisposeAsync par le consommateur. Les streams de destination restent possédés par l’appelant. CancellationToken et timeout couvrent l’attente des headers et la lecture du corps. Les noms de fichiers fournis par Content-Disposition restent des métadonnées, sans écriture automatique sur un chemin dérivé non contrôlé.

Aucune méthode multipart n’est documentée ici. Les données privées VPN et tous les mots de passe doivent être absents de logs, messages d’exception et ToString. Ne pas recopier les clés privées des exemples publics dans une fixture durable ; les tests doivent utiliser des valeurs synthétiques explicitement identifiées.

Concurrence documentée : AirMedia `req_in_progress` signale une opération en cours ; VPNClientConfig autorise un profil actif ; PVR expose conflits/chevauchements/états ; Player dépend des capacités actuellement actives. Aucun de ces points ne garantit l’atomicité d’un GET suivi d’un changement. Les commandes fluentes doivent être immuables et ne produire l’I/O qu’à l’envoi explicite.

## Dépréciations, compatibilité du socle et dépendances

Aucune exclusion deprecated/obsolete/removed dans le lot. `Precord.legacy_uri` et `force_channel_name` sont uniquement visibles depuis un Player et recommandent `channel_uuid` lorsqu’il est disponible ; cette préférence n’est pas une déclaration de dépréciation. Les prefixes v8/v10 et les labels UNSTABLE/Internal ne permettent pas d’exclure les opérations actuelles.

| Socle existant | Écart à traiter après consolidation | Preuve |
| --- | --- | --- |
| AirMedia error enum | Current foundation was rechecked and includes all23 documented error values, including unauthorized and unsupported_media; RequestInProgress is correctly req_in_progress. No enum addition is needed. | [preuve](http://mafreebox.freebox.fr/doc/index.html#airmedia-errors) |
| AirMedia stop DTO/fluent API | Current request Media string is required by constructor; source stop example omits media. Need action-specific DTO/command before exposing Stop. | [preuve](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-airmedia-receviers-receiver_name-) |
| AirMedia position | At(int) currently checks only nonnegative input. Clarify encoded percent*1000; source gives no explicit bounds, do not label seconds or infer a guaranteed maximum. | [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.position) |
| AirMedia media value | PlayVideo URL vs ShowPhoto Base64 filesystem path differ; a uniform URL validator is incorrect. | [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.media) |
| AirMedia route | Existing receivers route matches three examples while raw formal source has typo receviers; provenance/resolution must remain visible. | [preuve](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-airmedia-receviers-receiver_name-) |
| AirMedia capabilities | Current booleans cover photo/audio/video/screen; source calls field map. Keep unknown map keys or explicitly document projection; missing keys/null are not declared required. | [preuve](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiver.capabilities) |

| Dépendance | Propriétaire | Effet |
| --- | --- | --- |
| [source](http://mafreebox.freebox.fr/doc/index.html#authentication) | `protocol` | Default authenticated requests, generated envelope, version root, permission matrix, timestamps — all operations |
| [source](http://mafreebox.freebox.fr/doc/index.html#opening-a-session) | `protocol` | contacts/calls/pvr/settings permissions; absent permission false; no invented permission keys — all authorization decisions |
| [source](http://mafreebox.freebox.fr/doc/index.html#ConnectionStatus.ipv4_port_range) | `network` | Inclusive [first,last] port range and VPNServerConfig min_port/max_port definitions — VPN server config port projections |
| [source](http://mafreebox.freebox.fr/doc/index.html#AirMediaReceiverRequest.media) | `files-storage` | Photo media is Base64 FS path returned by fs/ls; preserve exact bytes/normalization — AirMedia photo commands |
| [source](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-voicemail-id-audio_file) | `protocol` | Owned raw HTTP response/stream download and cancellation; do not force JSON envelope — voicemail audio and VPN profile download |

Les futurs fichiers de contrats/façades ne sont pas encore attribués. Les changements `IFreeboxClient`, contexteJSON, solution, sample et transport appartiennent à l’intégrateur et au propriétaire commun. Ce reviewer écrit uniquement son JSON et ce Markdown.

## Inconnues et conflits — décisions requises

| ID / état | Observation exacte | Impact et action proposée | Preuves |
| --- | --- | --- | --- |
| `airmedia-receiver-route` / unknown | Formal route spells receviers, while all three request examples spell receivers. No live route verification was performed. | AirMedia POST routing; catalogue typo must not be silently propagated. Prefer attested examples only after orchestrator explicitly records route resolution; preserve both proofs. | [#post--api-v8-airmedia-receviers-receiver_name-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-airmedia-receviers-receiver_name-) |
| `airmedia-config-put-route` / unknown | PUT formal signature uses /airmedia/config/; example uses /airmedia/. | AirMedia update route conflicts. Prefer formal route with explicit resolution recorded; no alternative replay. | [#put--api-v8-airmedia-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-airmedia-config-) |
| `tftp-latest-route` / unknown | PUT formal signature is /api/latest/tftp/config/ while request example uses /api/v16/tftp/config/. | Transport requires explicit API-root/alias support or accepted v16 example route, not an invented vlatest version. Orchestrator must choose an explicit bounded route policy and retain formal alias metadata. | [#put--api-latest-tftp-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-latest-tftp-config-) |
| `tftp-root-encoding` / unknown | TftpConfig.root is explicitly Base64 absolute path, but both response examples contain plain /ssd2. | Typed encoded-path invariant cannot cover both representations. Keep wire string, never auto-decode or re-encode; prohibit an invented fixed encoding rule until resolved. | [#TftpConfig.root](http://mafreebox.freebox.fr/doc/index.html#TftpConfig.root) ; [#get--api-v16-tftp-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-tftp-config-) ; [#put--api-latest-tftp-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-latest-tftp-config-) |
| `call-batch-methods` / unknown | POST delete_all/ and mark_all_as_read/ signatures have GET request examples without trailing slash. | Deletion/read-acknowledgement method choice matters. Use explicit formal POST signatures after recorded resolution; never probe destructive GET variants. | [#post--api-v10-call-log-delete_all-](http://mafreebox.freebox.fr/doc/index.html#post--api-v10-call-log-delete_all-) ; [#post--api-v10-call-log-mark_all_as_read-](http://mafreebox.freebox.fr/doc/index.html#post--api-v10-call-log-mark_all_as_read-) |
| `call-new-bool` / unknown | CallEntry.new declaration says bool but PUT example sends string "false". | Request scalar representation conflict. Prefer declared boolean schema; retain malformed example as source defect, not serialize string by inference. | [#CallEntry.new](http://mafreebox.freebox.fr/doc/index.html#CallEntry.new) ; [#put--api-v10-call-log-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v10-call-log-id) |
| `voicemail-country-code` / unknown | VoicemailEntry.country_code is declared string (may be empty), but response/update examples use integer 33. | Response requires explicit union/string-number converter policy. Preserve lossless documented string-or-integer representation if orchestrator accepts; never map empty string to zero. | [#VoicemailEntry.country_code](http://mafreebox.freebox.fr/doc/index.html#VoicemailEntry.country_code) ; [#get--api-v10-call-voicemail-](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-voicemail-) ; [#put--api-v10-call-voicemail-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v10-call-voicemail-id) |
| `upnpav-put-example-route` / unknown | PUT formal /upnpav/config/ example says /upnpigd/config/. | Wrong service could be modified by trusting the example. Use formal upnpav signature after recorded resolution; no speculative upnpigd request. | [#put--api-v8-upnpav-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-upnpav-config-) |
| `vpn-generic-update` / not_documented | VPN server update signature names only openvpn_routed; no generic PUT /vpn/{vpn_id}/config/ is declared. | Do not invent other server update routes. Retain the literal documented update route; expose generic read for all discovered server IDs only. | [#put--api-v8-vpn-openvpn_routed-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-vpn-openvpn_routed-config-) ; [#get--api-v8-vpn-vpn_id-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-vpn_id-config-) |
| `vpn-auth-modes-shape` / unknown | VPNIPSecConfig.auth_modes declares array of VPNIPSecAuthMode but prose says map with only psk. | IPsec auth-mode response shape ambiguous. Bounded array/map union proposal pending orchestrator; preserve wire representation, no invented keys. | [#VPNIPSecConfig.auth_modes](http://mafreebox.freebox.fr/doc/index.html#VPNIPSecConfig.auth_modes) |
| `vpn-connection-types` / unknown | VPNConnection.vpn declares type strong (undefined); local_ip declares int but example is IPv4 string. | Connection DTO type selection and source reference differ. Prefer string server ID/example IPv4 with a documented union for local_ip if accepted; preserve conflict. | [#VPNConnection.vpn](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.vpn) ; [#VPNConnection.local_ip](http://mafreebox.freebox.fr/doc/index.html#VPNConnection.local_ip) ; [#get--api-v8-vpn-connection-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-connection-) |
| `vpn-connections-example-reference` / unknown | GET connections prose links VPNUser and request example is GET vpn/user, but declared route and response describe connections. | Catalogue referenced object is incorrect for connection operations. Resolve to VPNConnection from section/type/response; keep original bad reference in metadata. | [#get--api-v8-vpn-connection-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-connection-) ; [#delete--api-v8-vpn-connection-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-vpn-connection-id) |
| `vpn-download-json-shape` / not_documented | fmt=json is allowed, but no JSON response schema, envelope or MIME type is shown; WireGuard file MIME/encoding is also not described. | Cannot generate a typed JSON profile body or assume JSON envelope. Return caller-owned raw download stream plus response metadata for both formats; do not guess JSON DTO. | [#get--api-v8-vpn-download_config-server_name-login-fmt](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn-download_config-server_name-login-fmt) |
| `vpn-client-openvpn-config` / not_documented | OpenVPN is a listed VPNClientConfig type but no OpenVPN-specific configuration object/import field/method is documented. | Typed OpenVPN profile creation cannot be inferred. Do not invent conf_openvpn, multipart/import paths or methods; explicitly expose only known configuration fields. | [#VPNClientConfig.type](http://mafreebox.freebox.fr/doc/index.html#VPNClientConfig.type) ; [#post--api-v8-vpn_client-config-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vpn_client-config-) |
| `vpn-client-update-reference` / unknown | PUT vpn_client/config/{id} prose references VPNServerConfig, while response is VPNClientConfig. | Catalogue reference must be corrected without reusing server DTO. Resolve to VPNClientConfig from route, preceding declaration and response example. | [#put--api-v8-vpn_client-config-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-vpn_client-config-id) |
| `vpn-client-ipv4-key` / unknown | VPNClientStatus property declares uppercase IPv4 while response example has lowercase ipv4. | Case-sensitive JSON generated DTO must preserve both proven wire spellings explicitly. Orchestrator to approve alias reader/writer policy; do not enable global case-insensitive JSON as silent correction. | [#VPNClientStatus.IPv4](http://mafreebox.freebox.fr/doc/index.html#VPNClientStatus.IPv4) ; [#get--api-v8-vpn_client-status](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-status) |
| `vpn-client-routes-shape` / unknown | VpnClientIpInfo.routes declares list but status example contains {}. | Status routes shape ambiguous. Use an explicit supported list/map union or documented open JSON payload if approved. | [#VpnClientIpInfo.routes](http://mafreebox.freebox.fr/doc/index.html#VpnClientIpInfo.routes) ; [#get--api-v8-vpn_client-status](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vpn_client-status) |
| `player-package-id-type` / unknown | PlayerStatusForegroundApp.package_id has undefined wire type id and no scalar example. | Cannot assign integer vs string without evidence. Preserve an open scalar representation with explicit schema-unknown metadata pending acceptance. | [#PlayerStatusForegroundApp.package_id](http://mafreebox.freebox.fr/doc/index.html#PlayerStatusForegroundApp.package_id) |
| `pvr-id-shapes` / unknown | Precord/Frecord ids are declared string; all recording examples use numeric ids; overlap_list is array of int. | Identifier DTO/path arguments require a lossless union policy. Approve exact string/Int64 identity conversion or explicit union, never force integer on opaque strings. | [#Precord.id](http://mafreebox.freebox.fr/doc/index.html#Precord.id) ; [#Frecord.id](http://mafreebox.freebox.fr/doc/index.html#Frecord.id) ; [#get--api-v8-pvr-programmed-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-programmed-) ; [#get--api-v8-pvr-finished-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-finished-) |
| `pvr-media-schema` / unknown | Media.record_time declares int but examples contain nested channel/quality maps. Unanchored total bytes declaration maps to example key total_bytes. | Typed media stats schema conflicts, values total_bytes exceed Int32. Use Int64 counters; approve nested dictionary or bounded int/map union explicitly; cite #Media for unanchored declaration. | [#Media.record_time](http://mafreebox.freebox.fr/doc/index.html#Media.record_time) ; [#Media](http://mafreebox.freebox.fr/doc/index.html#Media) ; [#get--api-v8-pvr-media-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-pvr-media-) |
| `pvr-config-put-response` / not_documented | PUT pvr/config declares update but provides no request/response example; PvrConfig has two integer margin fields with no unit stated. | Do not assume response result or minute/second units. Resultless transport possible after accepted body projection; keep margin integer units not_documented. | [#put--api-v8-pvr-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-pvr-config-) ; [#PvrConfig.margin_before](http://mafreebox.freebox.fr/doc/index.html#PvrConfig.margin_before) ; [#PvrConfig.margin_after](http://mafreebox.freebox.fr/doc/index.html#PvrConfig.margin_after) |
| `null-and-patch-semantics` / not_documented | These module field declarations do not specify explicit null acceptance/clearing, complete required fields, defaults or a universal partial-update rule. | Cannot substitute defaults for omitted/null fields or assume nullable DTO means patch clear. Use explicit presence-aware request fields, send only supplied values; preserve wire null only where future evidence allows. | [#airmedia-api](http://mafreebox.freebox.fr/doc/index.html#airmedia-api) ; [#call](http://mafreebox.freebox.fr/doc/index.html#call) ; [#contacts](http://mafreebox.freebox.fr/doc/index.html#contacts) ; [#ftp](http://mafreebox.freebox.fr/doc/index.html#ftp) ; [#tftp](http://mafreebox.freebox.fr/doc/index.html#tftp) ; [#network-share](http://mafreebox.freebox.fr/doc/index.html#network-share) ; [#upnp-av](http://mafreebox.freebox.fr/doc/index.html#upnp-av) ; [#vpn-server-unstable](http://mafreebox.freebox.fr/doc/index.html#vpn-server-unstable) ; [#vpn-client-unstable](http://mafreebox.freebox.fr/doc/index.html#vpn-client-unstable) ; [#player-unstable](http://mafreebox.freebox.fr/doc/index.html#player-unstable) ; [#pvr-unstable](http://mafreebox.freebox.fr/doc/index.html#pvr-unstable) |
| `undocumented-read-permissions` / not_documented | Call account/voicemail and Player do not declare dedicated permission keys; global authenticated/default permissions apply. | Missing local permission note does not prove public access or permission-free GET. Depend on protocol permission matrix; expose API errors without preemptively claiming authorization. | [#authentication](http://mafreebox.freebox.fr/doc/index.html#authentication) ; [#opening-a-session](http://mafreebox.freebox.fr/doc/index.html#opening-a-session) ; [#get--api-v10-call-account](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-account) ; [#get--api-v10-call-voicemail-](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-call-voicemail-) ; [#player-unstable](http://mafreebox.freebox.fr/doc/index.html#player-unstable) |

Les contradictions peuvent être résolues techniquement en privilégiant explicitement une déclaration, un exemple ou une union bornée. Ce choix doit conserver les deux preuves. Le JSON ne donne jamais à un exemple la valeur d’une validation live : aucune API opérationnelle de la Freebox n’a été appelée durant cette revue.

## Validation significative prévue

| Cas | Assertion utile | Propriétaire |
| --- | --- | --- |
| All75 signature mappings, concrete contacts alternatives and preserved innerPlayerapi6 | Exact method, trailing slash, version root, encoded single segments, body wire keys; no route outside configured origin | services-media implementation tests + integrator |
| AirMedia start photo/video and stop | stop omitsmedia; photoBase64 unchanged; position50000 means50%; action/media_typestrings, passwordomit | services-media |
| RawvoicemailWAV/VPNprofile | Do not parse successenvelope; streaming cancellation/bodytimeout/disposal, metadata, no JSON assumption | transport + native AOT sample |
| GETVPNconfigdownload has invalidating side effect | One request per explicit command; auth/error/network failure neverreplays GET download | transport tests |
| Presence, write-only passwords, partial update | No RO requestfields; WO not required response; absent fields notserialized; nullclearing notinvented | common models |
| PVR64bit data and emptychannel type | byte_size4433869440 total244950000000 survive exact; channel_typeemptystring roundtrips; idstring/numericunion acceptedonlyafterresolution | commoncontract + AOT |
| VPNconditional configs and credentials | wireguardrequiredIP, only matchingconfigpresence, IPempty preservedstandarduser; secretsneverexception/log/ToString | services-media + security review |
| UNSTABLEopen enums and unknown fields | Unknown response string values preservedwithoutreflection; writes exposeonlyknownvalues; no broad case-insensitive correction | common JSON contract + native AOT |
| Syntheticboundarypaths/pagination | Encodedlogin/name/opaqueIDs safe; offset/limit/queryparameters correctlyformatted withoutinvented defaults | services-media |

Vérifications effectuées : empreinte primaire exacte, neuf annexes historiques stables, 75 signatures brut/catalogue identiques, 260 déclarations retrouvées y compris le champ Media sans ancre. Aucun test applicatif n’a été exécuté dans cette phase. Les 43 tests antérieurs du socle ne sont pas une mesure de conformité ou de couverture de ces75 opérations.

**Verdict `blocked_evidence` — revue complète, acceptation technique pending.** Prochaine étape : l’orchestrateur consolide les23 inconnues et approuve les contrats/choix de représentation, puis attribue les fichiers d’implémentation et les adaptations du transport.
