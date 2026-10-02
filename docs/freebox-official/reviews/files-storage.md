# Revue SDK16 — fichiers, téléchargements, stockage et VM

`task_id: files-storage` · Reviewer `/root/mapper_aot` · Phase revue seule · Verdict `blocked_evidence` · Acceptation indépendante : pending.

La source existe et a été lue. Le verdict conserve les contradictions de schéma et les réponses insuffisamment précisées qui empêchent de prétendre à un contrat fortement typé uniforme. L’orchestrateur peut accepter des représentations conservatrices explicites ; aucune API réelle n’a été invoquée.

Source : `http://mafreebox.freebox.fr/doc/index.html` ; SHA-256 brut vérifié `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`. Fichier analysé : `docs/freebox-official/api-16.0/sources/raw/embedded/doc/index.html`. Archive utilisateur hors ligne : identité des octets vérifiée, authenticité officielle non vérifiée indépendamment. Le numéro API16.0 provient de la découverte assainie fournie dans l’archive ; les signatures v8/v15 sont des preuves historiques conservées, la version major effective doit venir de la découverte.

## Couverture

| Module | Variantes d’opérations | Modèles | Champs retenus |
| --- | ---: | ---: | ---: |
| download | 21 | 8 | 89 |
| download_config | 3 | 7 | 38 |
| download_feeds | 11 | 2 | 25 |
| fs | 19 | 2 | 34 |
| share | 4 | 1 | 5 |
| upload | 5 | 5 | 21 |
| raid | 9 | 3 | 36 |
| rrd | 2 | 1 | 5 |
| storage | 11 | 4 | 37 |
| vm | 18 | 6 | 34 |

Contrôle brut/catalogue : 103 variantes HTTP, 39 objets, 325 champs bruts dont 324 retenus et le seul champ `DownloadFile.path` exclu. Deux signatures `POST downloads/add` possèdent une même ancre brute ; les variantes form et multipart sont distinguées par ordinal. La partie download inclut les sections sœurs stats/files/trackers/peers/pieces/blacklist.

Toutes les sections générales, opérations, modèles, enums et exemples ont une disposition dans le companion JSON. Les sections UNSTABLE sont retenues : ce mot ne signifie pas deprecated. Aucun code d’application modifié.

## I/O, chemins et état

- Les chemins FS sont du Base64 des octets UTF8 originaux. Réutiliser le chemin opaque retourné par ls ; ne jamais normaliser Unicode. Les formes « Spécial » NFC et NFD sont deux chemins distincts. Encoder une seule fois le segment URI, en préservant +, / et =. Les noms dirname/dst de mkdir/rename sont du texte clair selon leur contrat local.
- Les tâches FS de déplacement/copie/suppression/concaténation/archive/extraction/réparation/hash sont asynchrones ; une seule tâche active selon l’état queued. DELETE stoppe sans rollback. mkdir renvoie seulement success ; rename renvoie une chaîne Base64. `fs/ls` récent renvoie `{entries,cursor}` avec limit/cursor optionnels. Le batch info ignore les chemins invalides.
- downloads/add est form-urlencoded pour URL ou liste newline, multipart pour fichiers torrent/nzb. Le result.id est un entier pour une tâche, un tableau d’entiers pour une liste. Les options filename/hash sont réservées à une URL non récursive. Cookies et mots de passe sont des données secrètes à ne pas journaliser.
- Le téléchargement FS renvoie des octets bruts et des en-têtes de contenu ; prévoir une réponse propriétaire IDisposable gardant HttpResponseMessage ouvert jusqu’à fermeture du stream. Cette ownership est une décision du client, aucune garantie de Range/reprise n’est documentée.
- L’upload fichier moderne utilise WebSocket, jamais le transfert HTTP déprécié. Les actions utilisent `request_id`, distinct du `req_id` des événements généraux. Après upload_start/succès, les frames binaires sont envoyées en pipeline pendant réception des progrès upload_data ; upload_finalize puis ACK de fin. Réutiliser une connexion pour plusieurs fichiers séquentiels. Fermeture conserve le partiel pour resume ; upload_cancel l’efface. Taille maximale de chunk/fenêtre non documentée.
- VM exige SystemConfig.has_vm ; console QEMU et écran VNC sont des WebSocket authentifiés. Les événements vm_state_changed et vm_disk_task_done remplacent le polling recommandé contre la source. Une VM détient l’USB à la fois. Les changements de configuration/start/delete exigent stopped ; stop/restart/powerbutton exigent running. shrink_allow permet une réduction potentiellement destructive.
- RAID : uuid seul garanti stable entre reboots. PUT état ne prend que id et stopped/running ; autres champs ignorés. forcestart exige error ; réparation des membres/faulty/spares exige non-running, addspares exige spare. Les mutations ne sont jamais rejouées automatiquement (politique client).
- RRD : conserver les timestamps ajustables de résolution et les nombres bruts ; precision multiplie les valeurs (100 pour deux décimales), les taux sont byte/s et SNR 1/10 dB. GET dit être permis sans settings, mais sa position des paramètres n’est pas documentée. Les clés connues n’impliquent pas un schéma dictionnaire arbitraire.

## Exclusions vérifiées

| Élément | Portée | Remplacement / preuve |
| --- | --- | --- |
| DownloadFile.path | DownloadFile response only; not unrelated path parameters | Raw field description [ DEPRECATED ] ; DownloadFile.filepath (current neighbouring field and historical change); explicit use-replacement phrase absent ; [DownloadFile.path](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.path) |
| temp1 | RRD db=temp only | [DEPRECATED, use cpum] ; cpum ; [rrd-temp-db](http://mafreebox.freebox.fr/doc/index.html#rrd-temp-db) |
| temp2 | RRD db=temp only | [DEPRECATED, use cpub] ; cpub ; [rrd-temp-db](http://mafreebox.freebox.fr/doc/index.html#rrd-temp-db) |
| temp3 | RRD db=temp only | [DEPRECATED, use sw] ; sw ; [rrd-temp-db](http://mafreebox.freebox.fr/doc/index.html#rrd-temp-db) |
| previous HTTP upload method | Former HTTP file-transfer mechanism; retained tracking GET/DELETE upload endpoints and new WS are not excluded | previous http upload method is now deprecated since api v4; use new WebSocket API, else FTP ; /api/v{major}/ws/upload WebSocket; FTP if WS unsupported ; [file-upload](http://mafreebox.freebox.fr/doc/index.html#file-upload) |

Les champs temp1/temp2/temp3 sont retirés seulement de db=temp. Les opérations RRD restent actives. Le paramètre documentaire `path` de priorité d’un fichier est un alias de placeholder file_id, et ne doit pas être exclu comme s’il était DownloadFile.path. Le suivi upload GET/DELETE est conservé : il n’est pas le transfert HTTP exclu.

## Opérations vérifiées

| Méthode et chemin exact | Request | Result | I/O / contraintes | Source |
| --- | --- | --- | --- | --- |
| `GET /api/v8/downloads/` | non spécifié / paramètres route | conflict: collection<Download> narrative vs single Download object example | json ; not_documented | [get--api-v8-downloads-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-) |
| `GET /api/v8/downloads/{id}` | non spécifié / paramètres route | Download | json ; not_documented | [get--api-v8-downloads-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-id) |
| `DELETE /api/v8/downloads/{id}` | non spécifié / paramètres route | no result in success example | json ; Delete stops incomplete task; preserves downloaded files unless /erase. not_documented | [delete--api-v8-downloads-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-id) |
| `DELETE /api/v8/downloads/{id}/erase` | non spécifié / paramètres route | same as delete downloads/{id}; files additionally erased | json ; Delete stops incomplete task; preserves downloaded files unless /erase. not_documented | [delete--api-v8-downloads-id-erase](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-id-erase) |
| `PUT /api/v8/downloads/{id}` | {"model": "Download writable documented fields; example io_priority,status", "partial": "example changes subset; null semantics not documented"} | Download | json ; not_documented | [put--api-v8-downloads-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-id) |
| `GET /api/v8/downloads/{id}/log` | non spécifié / paramètres route | string | json ; not_documented | [get--api-v8-downloads-id-log](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-id-log) |
| `POST /api/v8/downloads/add` | {"fields_from_parameter_table": ["download_url", "download_url_list", "download_dir", "filename", "hash", "recursive", "username", "password", "archive_password", "cookies"]} | object {id: int} for one task; object {id: int[]} for multiple URL tasks | form_request ; URL vs newline URL-list alternatives; supported schemes stated http://, ftp://, magnet:. Recursive follows same domain and same root path. filename/hash valid only one non-recursive download_url. download_dir defaults to config when omitted. .torrent/.nzb uploaded files supported. Credentials/cookies are not logged. not_documented | [post--api-v8-downloads-add](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-add) |
| `POST /api/v8/downloads/add` | {"fields_from_parameter_table": ["download_file", "download_dir", "archive_password"]} | object {id: int} for one task; object {id: int[]} for multiple URL tasks | multipart_request ; URL vs newline URL-list alternatives; supported schemes stated http://, ftp://, magnet:. Recursive follows same domain and same root path. filename/hash valid only one non-recursive download_url. download_dir defaults to config when omitted. .torrent/.nzb uploaded files supported. Credentials/cookies are not logged. not_documented | [post--api-v8-downloads-add](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-add) |
| `GET /api/v8/downloads/stats` | non spécifié / paramètres route | DownloadStats | json ; not_documented | [get--api-v8-downloads-stats](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-stats) |
| `GET /api/v8/downloads/{task_id}/files` | non spécifié / paramètres route | DownloadFile[] | json ; not_documented | [get--api-v8-downloads-task_id-files](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-files) |
| `PUT /api/v8/downloads/{task_id}/files/{file_id}` | {"fields": [["priority", "string"]], "note": "path parameter documentation is named path but placeholder is file_id; it is NOT deprecated DownloadFile.path"} | no result in success example | json ; not_documented | [put--api-v8-downloads-task_id-files-file_id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-task_id-files-file_id) |
| `GET /api/v8/downloads/{task_id}/trackers` | non spécifié / paramètres route | DownloadTracker[] | json ; Bittorrent tasks only where explicitly stated. Announce URL must be escaped as a single path segment; no route guessing from singular example. not_documented | [get--api-v8-downloads-task_id-trackers](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-trackers) |
| `POST /api/v8/downloads/{task_id}/trackers` | {"fields": [["announce", "string"]]} | no result in success example | json ; Bittorrent tasks only where explicitly stated. Announce URL must be escaped as a single path segment; no route guessing from singular example. not_documented | [post--api-v8-downloads-task_id-trackers](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-task_id-trackers) |
| `DELETE /api/v8/downloads/{task_id}/trackers/{announce}` | non spécifié / paramètres route | no result in success example | json ; Bittorrent tasks only where explicitly stated. Announce URL must be escaped as a single path segment; no route guessing from singular example. not_documented | [delete--api-v8-downloads-task_id-trackers-announce](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-task_id-trackers-announce) |
| `PUT /api/v8/downloads/{task_id}/trackers/{announce}` | {"fields": [["announce", "string"], ["is_enabled", "bool"]]} | no result in success example | json ; Bittorrent tasks only where explicitly stated. Announce URL must be escaped as a single path segment; no route guessing from singular example. not_documented | [put--api-v8-downloads-task_id-trackers-announce](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-task_id-trackers-announce) |
| `GET /api/v8/downloads/{task_id}/peers` | non spécifié / paramètres route | DownloadPeer[]; requests field conflicting array<int> vs object example | json ; Bittorrent tasks only where explicitly stated. Announce URL must be escaped as a single path segment; no route guessing from singular example. not_documented | [get--api-v8-downloads-task_id-peers](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-peers) |
| `GET /api/v8/downloads/{task_id}/pieces` | non spécifié / paramètres route | string of per-piece status characters | json ; not_documented | [get--api-v8-downloads-task_id-pieces](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-pieces) |
| `GET /api/v8/downloads/{task_id}/blacklist` | non spécifié / paramètres route | DownloadBlacklistEntry[] | json ; Bittorrent tasks only where explicitly stated. Announce URL must be escaped as a single path segment; no route guessing from singular example. not_documented | [get--api-v8-downloads-task_id-blacklist](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-blacklist) |
| `DELETE /api/v8/downloads/{task_id}/blacklist/empty` | non spécifié / paramètres route | no result in success example | json ; Clears global entries plus entries related to given torrent. not_documented | [delete--api-v8-downloads-task_id-blacklist-empty](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-task_id-blacklist-empty) |
| `DELETE /api/v8/downloads/blacklist/{host}` | non spécifié / paramètres route | no result in success example | json ; not_documented | [delete--api-v8-downloads-blacklist-host](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-blacklist-host) |
| `POST /api/v8/downloads/blacklist` | {"fields": [["host", "string"], ["expire", "int"]]} | DownloadBlacklistEntry | json ; not_documented | [post--api-v8-downloads-blacklist](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-blacklist) |
| `GET /api/v8/downloads/feeds/` | non spécifié / paramètres route | DownloadFeed[] | json ; not_documented | [get--api-v8-downloads-feeds-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-feeds-) |
| `GET /api/v8/downloads/feeds/{id}` | non spécifié / paramètres route | DownloadFeed | json ; not_documented | [get--api-v8-downloads-feeds-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-feeds-id) |
| `POST /api/v8/downloads/feeds/` | {"fields": [["url", "string"]]} | DownloadFeed-shaped object with feed_id instead of documented id | json ; not_documented | [post--api-v8-downloads-feeds-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-) |
| `DELETE /api/v8/downloads/feeds/{id}` | non spécifié / paramètres route | no result in success example | json ; Deletes feed and associated items; does not alter Download tasks. not_documented | [delete--api-v8-downloads-feeds-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-downloads-feeds-id) |
| `PUT /api/v8/downloads/feeds/{id}` | {"fields": [["auto_download", "bool"]]} | DownloadFeed-shaped object with feed_id instead of documented id | json ; not_documented | [put--api-v8-downloads-feeds-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-feeds-id) |
| `POST /api/v8/downloads/feeds/{id}/fetch` | non spécifié / paramètres route | no result in success example | json ; Remote RSS TTL must expire; otherwise feed_is_recent. not_documented | [post--api-v8-downloads-feeds-id-fetch](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-id-fetch) |
| `POST /api/v8/downloads/feeds/fetch` | non spécifié / paramètres route | no result in success example | json ; not_documented | [post--api-v8-downloads-feeds-fetch](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-fetch) |
| `GET /api/v8/downloads/feeds/{feed_id}/items/` | non spécifié / paramètres route | DownloadFeedItem[] | json ; not_documented | [get--api-v8-downloads-feeds-feed_id-items-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-feeds-feed_id-items-) |
| `PUT /api/v8/downloads/feeds/{feed_id}/items/{item_id}` | {"fields": [["is_read", "bool"]]} | no result in success example | json ; not_documented | [put--api-v8-downloads-feeds-feed_id-items-item_id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-feeds-feed_id-items-item_id) |
| `POST /api/v8/downloads/feeds/{feed_id}/items/{item_id}/download` | non spécifié / paramètres route | no result in success example | json ; not_documented | [post--api-v8-downloads-feeds-feed_id-items-item_id-download](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-feed_id-items-item_id-download) |
| `POST /api/v8/downloads/feeds/{feed_id}/items/mark_all_as_read` | non spécifié / paramètres route | no result in success example | json ; not_documented | [post--api-v8-downloads-feeds-feed_id-items-mark_all_as_read](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-feed_id-items-mark_all_as_read) |
| `GET /api/v8/downloads/config/` | non spécifié / paramètres route | DownloadConfiguration | json ; not_documented | [get--api-v8-downloads-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-config-) |
| `PUT /api/v8/downloads/config/` | {"model": "DownloadConfiguration", "partial": "example contains writable subobjects; omitted fields unchanged in example; null semantics not documented"} | DownloadConfiguration | json ; not_documented | [put--api-v8-downloads-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-config-) |
| `PUT /api/v8/downloads/throttling` | {"fields": [["throttling", "enum DlThrottlingConfig.mode"]]} | object {is_scheduled: bool, throttling: enum} | json ; not_documented | [put--api-v8-downloads-throttling](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-throttling) |
| `GET /api/v15/fs/tasks/` | non spécifié / paramètres route | FsTask[] | json ; not_documented | [get--api-v15-fs-tasks-](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-tasks-) |
| `GET /api/v15/fs/tasks/{id}` | non spécifié / paramètres route | FsTask | json ; not_documented | [get--api-v15-fs-tasks-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-tasks-id) |
| `DELETE /api/v15/fs/tasks/{id}` | non spécifié / paramètres route | no result in success example | json ; Stops running task, no rollback; processed files remain as-is. | [delete--api-v15-fs-tasks-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v15-fs-tasks-id) |
| `PUT /api/v15/fs/tasks/{id}` | {"fields": [["state", "enum FsTask.state"]]} | FsTask | json ; not_documented | [put--api-v15-fs-tasks-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v15-fs-tasks-id) |
| `GET /api/v15/fs/ls/{path}` | non spécifié / paramètres route | object {entries: FileInfo[], cursor: string}; description incorrectly says list | json ; not_documented | [get--api-v15-fs-ls-path](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-ls-path) |
| `GET /api/v15/fs/info/{path}` | non spécifié / paramètres route | FileInfo | json ; not_documented | [get--api-v15-fs-info-path](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-info-path) |
| `POST /api/v15/fs/info` | {"type": "string[]", "encoding": "Base64 paths; invalid paths ignored"} | FileInfo[] | json ; Invalid paths ignored; response array may be shorter than request. not_documented | [post--api-v15-fs-info](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-info) |
| `POST /api/v15/fs/mv/` | {"fields_from_parameter_table": ["files", "dst", "mode"]} | FsTask; operation asynchronous | json ; Asynchronous FsTask, may enqueue to avoid excessive disk I/O; queued means only one active task. | [post--api-v15-fs-mv-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-mv-) |
| `POST /api/v15/fs/cp/` | {"fields_from_parameter_table": ["files", "dst", "mode"]} | FsTask; operation asynchronous | json ; Asynchronous FsTask, may enqueue to avoid excessive disk I/O; queued means only one active task. | [post--api-v15-fs-cp-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-cp-) |
| `POST /api/v15/fs/rm/` | {"fields_from_parameter_table": ["files"]} | FsTask; operation asynchronous | json ; Asynchronous FsTask, may enqueue to avoid excessive disk I/O; queued means only one active task. | [post--api-v15-fs-rm-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-rm-) |
| `POST /api/v15/fs/cat/` | {"fields_from_parameter_table": ["files", "dst", "multi_volumes", "delete_files", "overwrite", "append"]} | FsTask; operation asynchronous | json ; Asynchronous FsTask, may enqueue to avoid excessive disk I/O; queued means only one active task. | [post--api-v15-fs-cat-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-cat-) |
| `POST /api/v15/fs/archive/` | {"fields_from_parameter_table": ["files", "dst"]} | FsTask; operation asynchronous | json ; Asynchronous FsTask, may enqueue to avoid excessive disk I/O; queued means only one active task. | [post--api-v15-fs-archive-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-archive-) |
| `POST /api/v15/fs/extract/` | {"fields_from_parameter_table": ["src", "dst", "password", "delete_archive", "overwrite"]} | FsTask; operation asynchronous | json ; Asynchronous FsTask, may enqueue to avoid excessive disk I/O; queued means only one active task. | [post--api-v15-fs-extract-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-extract-) |
| `POST /api/v15/fs/repair/` | {"fields_from_parameter_table": ["src", "delete_archive"]} | FsTask; operation asynchronous | json ; Asynchronous FsTask, may enqueue to avoid excessive disk I/O; queued means only one active task. | [post--api-v15-fs-repair-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-repair-) |
| `POST /api/v15/fs/hash/` | {"fields_from_parameter_table": ["src", "hash_type"]} | FsTask; operation asynchronous | json ; Asynchronous FsTask, may enqueue to avoid excessive disk I/O; queued means only one active task. | [post--api-v15-fs-hash-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-hash-) |
| `GET /api/v15/fs/tasks/{id}/hash` | non spécifié / paramètres route | object {hash: string} | json ; Hash task must have succeeded and be done before reading hash. | [get--api-v15-fs-tasks-id-hash](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-tasks-id-hash) |
| `POST /api/v15/fs/mkdir/` | {"fields_from_parameter_table": ["parent", "dirname"]} | no result; synchronous success status | json ; not_documented | [post--api-v15-fs-mkdir-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-mkdir-) |
| `POST /api/v15/fs/rename/` | {"fields_from_parameter_table": ["src", "dst"]} | string: new Base64 path; synchronous | json ; not_documented | [post--api-v15-fs-rename-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-rename-) |
| `GET /api/v15/dl/{path}` | non spécifié / paramètres route | raw file bytes, Content-Type/Content-Length/Content-Disposition example | binary_download ; not_documented | [get--api-v15-dl-path](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-dl-path) |
| `GET /api/v8/share_link/` | non spécifié / paramètres route | ShareLink[] | json ; Feature requires HTTP remote access enabled; fullurl empty when disabled. expire=0 means no expiration. fullurl may be used by callers; do not transmit session credentials to it. not_documented | [get--api-v8-share_link-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-share_link-) |
| `GET /api/v8/share_link/{token}` | non spécifié / paramètres route | ShareLink | json ; Feature requires HTTP remote access enabled; fullurl empty when disabled. expire=0 means no expiration. fullurl may be used by callers; do not transmit session credentials to it. not_documented | [get--api-v8-share_link-token](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-share_link-token) |
| `DELETE /api/v8/share_link/{token}` | non spécifié / paramètres route | no result in success example | json ; Feature requires HTTP remote access enabled; fullurl empty when disabled. expire=0 means no expiration. fullurl may be used by callers; do not transmit session credentials to it. not_documented | [delete--api-v8-share_link-token](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-share_link-token) |
| `POST /api/v8/share_link/` | {"fields": [["path", "string"], ["expire", "timestamp"]], "example_extra_fields": ["fullurl"], "note": "ShareLink properties described read-only but create request example sets path/expire/fullurl; request subset inferred only from example."} | ShareLink | json ; Feature requires HTTP remote access enabled; fullurl empty when disabled. expire=0 means no expiration. fullurl may be used by callers; do not transmit session credentials to it. not_documented | [post--api-v8-share_link-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-share_link-) |
| `GET /api/v8/ws/upload` | non spécifié / paramètres route | HTTP 101 WebSocket, followed by upload-specific JSON control replies and binary file frames | websocket ; Single upload state machine per WebSocket; reuse same connection for sequential files. Send upload_start and await success; pipeline binary chunks and consume per-chunk progress; upload_finalize then await success. Closing socket leaves partial file; upload_cancel deletes partial file. Distinct request_id field from event-WebSocket req_id. | [get--api-v8-ws-upload](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-ws-upload) |
| `GET /api/v8/upload/` | non spécifié / paramètres route | FileUpload[] | json ; not_documented | [get--api-v8-upload-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upload-) |
| `GET /api/v8/upload/{id}` | non spécifié / paramètres route | FileUpload | json ; not_documented | [get--api-v8-upload-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upload-id) |
| `DELETE /api/v8/upload/{id}/cancel` | non spécifié / paramètres route | no result in success example | json ; Requires FileUpload.status=in_progress; closes connection. not_documented | [delete--api-v8-upload-id-cancel](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-upload-id-cancel) |
| `DELETE /api/v8/upload/{id}` | non spécifié / paramètres route | no result in success example | json ; not_documented | [delete--api-v8-upload-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-upload-id) |
| `POST /api/v8/rrd/` | {"model": "RRDFetch", "example_deprecated_fields_excluded": true} | object {date_start: int, date_end: int, data: array of per-sample field values plus time} | json ; not_documented | [post--api-v8-rrd-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-rrd-) |
| `GET /api/v8/rrd/` | {"model": "RRDFetch", "status": "unknown", "note": "Same as POST but placement JSON body vs query unspecified; do not choose query encoding without evidence."} | same as POST /rrd/ according to source | json ; not_documented | [get--api-v8-rrd-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-rrd-) |
| `GET /api/v8/storage/disk/` | non spécifié / paramètres route | StorageDisk[] | json ; not_documented | [get--api-v8-storage-disk-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-disk-) |
| `GET /api/v8/storage/disk/{id}` | non spécifié / paramètres route | StorageDisk | json ; not_documented | [get--api-v8-storage-disk-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-disk-id) |
| `PUT /api/v8/storage/disk/{id}` | {"fields": [["state", "enum StorageDisk.state"]], "note": "Enable/Disable operation; example disabled."} | StorageDisk | json ; not_documented | [put--api-v8-storage-disk-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-disk-id) |
| `GET /api/v8/storage/disk/{disk_id}/fsadvice?partition_id={partition_id}&dedicated_disk={bool}` | non spécifié / paramètres route | object {fstype: string, table_type: string, reason: enum, partitions_to_delete: DiskPartition[]} | json ; not_documented | [get--api-v8-storage-disk-disk_id-fsadvice?partition_id=partition_id&dedicated_disk=bool](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-disk-disk_id-fsadvice?partition_id=partition_id&dedicated_disk=bool) |
| `PUT /api/v8/storage/disk/{id}/format/` | {"fields_from_parameter_table": ["table_type", "fs_type", "label"]} | no result in success example; operation progress obtained from StorageDisk.operation_pct | json ; Destroys all previous data; creates one partition using all available space; parameters ignored for Freebox internal disk; progress from StorageDisk.operation_pct. not_documented | [put--api-v8-storage-disk-id-format-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-disk-id-format-) |
| `GET /api/v8/storage/partition/` | non spécifié / paramètres route | DiskPartition[] | json ; not_documented | [get--api-v8-storage-partition-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-partition-) |
| `GET /api/v8/storage/partition/{id}` | non spécifié / paramètres route | DiskPartition | json ; not_documented | [get--api-v8-storage-partition-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-partition-id) |
| `PUT /api/v8/storage/partition/{id}` | {"fields": [["state", "enum DiskPartition.state"]], "note": "Enable/Disable operation; example umounted."} | DiskPartition | json ; not_documented | [put--api-v8-storage-partition-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-partition-id) |
| `PUT /api/v8/storage/partition/{id}/check/` | {"fields_from_parameter_table": ["checkmode"]} | no result in success example; progress from DiskPartition.operation_pct | json ; checkmode ro is read-only; rw attempts repairs; monitor DiskPartition.operation_pct. not_documented | [put--api-v8-storage-partition-id-check-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-partition-id-check-) |
| `GET /api/v8/storage/config/` | non spécifié / paramètres route | StorageConfig | json ; not_documented | [get--api-v8-storage-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-config-) |
| `PUT /api/v8/storage/config/` | {"model": "StorageConfig", "partial": "Example external_pm_enabled=false preserves existing idle timeout; omission distinct from false/0, null undocumented."} | StorageConfig | json ; not_documented | [put--api-v8-storage-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-config-) |
| `GET /api/v8/storage/raid/` | non spécifié / paramètres route | RaidArray[] | json ; not_documented | [get--api-v8-storage-raid-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-raid-) |
| `GET /api/v8/storage/raid/{id}` | non spécifié / paramètres route | RaidArray | json ; not_documented | [get--api-v8-storage-raid-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-raid-id) |
| `POST /api/v8/storage/raid/` | {"fields": [["level", "enum RaidArray.level"], ["name", "string"], ["members", "array of {id:int}"]], "required_from_text": ["level", "name", "members", "members[].id"]} | not_documented | json ; not_documented | [post--api-v8-storage-raid-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-storage-raid-) |
| `DELETE /api/v8/storage/raid/{id}` | non spécifié / paramètres route | not_documented | json ; not_documented | [delete--api-v8-storage-raid-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-storage-raid-id) |
| `PUT /api/v8/storage/raid/{id}` | {"fields": [["id", "int"], ["state", "enum stopped\|running"]], "required_from_text": ["id", "state"], "note": "Only start/stop supported, all other fields ignored."} | not_documented | json ; not_documented | [put--api-v8-storage-raid-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-raid-id) |
| `POST /api/v8/storage/raid/{id}/forcestart` | non spécifié / paramètres route | not_documented | json ; Only array state error; incomplete array with sufficient data may start degraded. | [post--api-v8-storage-raid-id-forcestart](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-storage-raid-id-forcestart) |
| `DELETE /api/v8/storage/raid/{id}/members/faulty` | non spécifié / paramètres route | not_documented | json ; Array must not be running; addspares also requires a spare member. | [delete--api-v8-storage-raid-id-members-faulty](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-storage-raid-id-members-faulty) |
| `PUT /api/v8/storage/raid/{id}/members` | {"fields": [["members", "array of {id:int}"]], "required_from_text": ["members[].id"]} | not_documented | json ; Array must not be running; addspares also requires a spare member. | [put--api-v8-storage-raid-id-members](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-raid-id-members) |
| `POST /api/v8/storage/raid/{id}/members/addspares` | non spécifié / paramètres route | not_documented | json ; Array must not be running; addspares also requires a spare member. | [post--api-v8-storage-raid-id-members-addspares](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-storage-raid-id-members-addspares) |
| `GET /api/v8/vm/info/` | non spécifié / paramètres route | VmSystemInfo | json ; not_documented | [get--api-v8-vm-info-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-info-) |
| `GET /api/v8/vm/distros/` | non spécifié / paramètres route | VmDistribution[] | json ; not_documented | [get--api-v8-vm-distros-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-distros-) |
| `GET /api/v8/vm/` | non spécifié / paramètres route | VM[] | json ; not_documented | [get--api-v8-vm-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-) |
| `GET /api/v8/vm/{id}` | non spécifié / paramètres route | VM | json ; not_documented | [get--api-v8-vm-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-id) |
| `POST /api/v8/vm/` | {"model": "VM writable fields", "note": "POST explicitly requires VM object; individual field requiredness mostly undocumented."} | not_documented | json ; not_documented | [post--api-v8-vm-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-) |
| `DELETE /api/v8/vm/{id}` | non spécifié / paramètres route | not_documented | json ; VM must be stopped. | [delete--api-v8-vm-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-vm-id) |
| `PUT /api/v8/vm/{id}` | {"model": "VM writable fields", "status": "not_documented", "note": "Section update VM gives only stopped-state precondition; request body not explicitly specified."} | not_documented | json ; VM must be stopped. | [put--api-v8-vm-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-vm-id) |
| `POST /api/v8/vm/{id}/start` | non spécifié / paramètres route | not_documented | json ; VM must be stopped. | [post--api-v8-vm-id-start](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-start) |
| `POST /api/v8/vm/{id}/powerbutton` | non spécifié / paramètres route | not_documented | json ; VM must be running; powerbutton sends ACPI shutdown request; stop/restart immediate without safety. | [post--api-v8-vm-id-powerbutton](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-powerbutton) |
| `POST /api/v8/vm/{id}/stop` | non spécifié / paramètres route | not_documented | json ; VM must be running; powerbutton sends ACPI shutdown request; stop/restart immediate without safety. | [post--api-v8-vm-id-stop](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-stop) |
| `POST /api/v8/vm/{id}/restart` | non spécifié / paramètres route | not_documented | json ; VM must be running; powerbutton sends ACPI shutdown request; stop/restart immediate without safety. | [post--api-v8-vm-id-restart](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-restart) |
| `GET /api/v8/vm/{id}/console` | non spécifié / paramètres route | WebSocket QEMU chardev console | websocket ; not_documented | [get--api-v8-vm-id-console](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-id-console) |
| `GET /api/v8/vm/{id}/vnc` | non spécifié / paramètres route | WebSocket QEMU VNC; noVNC compatible | websocket ; Requires enable_screen=true; authenticated QEMU VNC websocket, noVNC unmodified. not_documented | [get--api-v8-vm-id-vnc](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-id-vnc) |
| `POST /api/v8/vm/disk/info` | {"fields_from_parameter_table": ["disk_path"]} | VmDiskInfo | json ; not_documented | [post--api-v8-vm-disk-info](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-disk-info) |
| `POST /api/v8/vm/disk/create` | {"fields_from_parameter_table": ["disk_path", "size", "disk_type"]} | task id; primitive vs object envelope shape not documented | json ; Use vm_disk_task_done RegisterAction event; source says task should not be polled. | [post--api-v8-vm-disk-create](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-disk-create) |
| `POST /api/v8/vm/disk/resize` | {"fields_from_parameter_table": ["disk_path", "size", "shrink_allow"]} | task id; primitive vs object envelope shape not documented | json ; Use vm_disk_task_done RegisterAction event; source says task should not be polled. | [post--api-v8-vm-disk-resize](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-disk-resize) |
| `GET /api/v8/vm/disk/task/{id}` | non spécifié / paramètres route | VmDiskTask | json ; not_documented | [get--api-v8-vm-disk-task-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-vm-disk-task-id) |
| `DELETE /api/v8/vm/disk/task/{id}` | non spécifié / paramètres route | not_documented | json ; Delete own completed tasks. | [delete--api-v8-vm-disk-task-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-vm-disk-task-id) |

Pour chaque opération le JSON conserve major littéral, path_template, ordinal source, paramètres path/query/body/form/multipart, required et nullable séparés, erreurs locales, permissions documentées/not_documented et absence de garantie de retry. Les permissions d’usage supposées ne sont pas fabriquées à partir des noms de modules.

## Modèles, accès et contraintes

Les champs sont recensés ci-dessous ; `not_documented` indique l’absence de règle de présence/nullabilité, jamais une valeur false/0/null. RO/WO sont conservés. Les DTOs création/modification doivent utiliser les sous-ensembles décrits par l’opération et préserver omission distincte de false/0 ; aucune sémantique null de patch n’est établie. Les entiers de tailles/transferts/timestamps sont proposés en long pour éviter troncature 32 bits, sans prétendre à une plage serveur documentée.

### Download

Source [Download](http://mafreebox.freebox.fr/doc/index.html#Download) ; module download.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#Download.id) | int | read_only | not_documented ; null not_documented | id |
| [type](http://mafreebox.freebox.fr/doc/index.html#Download.type) | enum | read_only | not_documented ; null not_documented | The valid download types are: Type Description bt bittorrent download nzb newsgroup download http HTTP download ftp FTP download |
| [name](http://mafreebox.freebox.fr/doc/index.html#Download.name) | string | read_only | not_documented ; null not_documented |  |
| [status](http://mafreebox.freebox.fr/doc/index.html#Download.status) | enum | not_documented | not_documented ; null not_documented | The valid download status are: Status Description stopped task is stopped, can be resumed by setting the status to downloading queued task will start when a new download slot is available the queue position is stored in queue_pos attribute starting task is preparing to start download downloading stopping task is gracefully stopping error there was a problem with the download, you can get an error code in the error field done the download is over. For bt you can resume seeding setting the status to seeding if the ratio is not reached yet checking (only valid for nzb) download is over, the downloaded files are being checked using par2 repairing (only valid for nzb) download is over, the downloaded files are being repaired using par2 extracting (only valid for nzb) download is over, the downloaded files are being extracted seeding (only valid for bt) download is over, the content is Change to being shared to other users. The task will automatically stop once the seed ratio has been reached retry You can set a task status to ‘retry’ to restart the download task. |
| [size](http://mafreebox.freebox.fr/doc/index.html#Download.size) | int | read_only | not_documented ; null not_documented | download size (in Bytes) |
| [queue_pos](http://mafreebox.freebox.fr/doc/index.html#Download.queue_pos) | int | not_documented | not_documented ; null not_documented | position in download queue (0 if not queued) |
| [io_priority](http://mafreebox.freebox.fr/doc/index.html#Download.io_priority) | enum | not_documented | not_documented ; null not_documented | The valid download priorities are: Priority Description low low normal normal high high |
| [tx_bytes](http://mafreebox.freebox.fr/doc/index.html#Download.tx_bytes) | int | read_only | not_documented ; null not_documented | transmitted bytes (including protocol overhead) |
| [rx_bytes](http://mafreebox.freebox.fr/doc/index.html#Download.rx_bytes) | int | read_only | not_documented ; null not_documented | received bytes (including protocol overhead) |
| [tx_rate](http://mafreebox.freebox.fr/doc/index.html#Download.tx_rate) | int | read_only | not_documented ; null not_documented | current transmit rate (in byte/s) |
| [rx_rate](http://mafreebox.freebox.fr/doc/index.html#Download.rx_rate) | int | read_only | not_documented ; null not_documented | current receive rate (in byte/s) |
| [tx_pct](http://mafreebox.freebox.fr/doc/index.html#Download.tx_pct) | int | read_only | not_documented ; null not_documented | transmit percentage (without protocol overhead) To improve precision the value as been scaled by 100 so that a tx_pct of 123 means 1.23% |
| [rx_pct](http://mafreebox.freebox.fr/doc/index.html#Download.rx_pct) | int | read_only | not_documented ; null not_documented | received percentage (without protocol overhead) To improve precision the value as been scaled by 100 so that a tx_pct of 123 means 1.23% |
| [error](http://mafreebox.freebox.fr/doc/index.html#Download.error) | enum | read_only | not_documented ; null not_documented | An error code |
| [created_ts](http://mafreebox.freebox.fr/doc/index.html#Download.created_ts) | timestamp | read_only | not_documented ; null not_documented | timestamp of the download creation time |
| [eta](http://mafreebox.freebox.fr/doc/index.html#Download.eta) | int | read_only | not_documented ; null not_documented | estimated remaining download time (in seconds) |
| [download_dir](http://mafreebox.freebox.fr/doc/index.html#Download.download_dir) | string | read_only | not_documented ; null not_documented | directory where the file(s) will be saved (base64 encoded) |
| [stop_ratio](http://mafreebox.freebox.fr/doc/index.html#Download.stop_ratio) | int | read_only | not_documented ; null not_documented | Only relevant for bittorrent tasks. Once the transmit ration has been reached the task will stop seeding. The ratio is scaled by 100 to improve resolution. A stop_ratio of 150 means that the task will stop seeding once tx_bytes = 1.5 * rx_bytes. |
| [archive_password](http://mafreebox.freebox.fr/doc/index.html#Download.archive_password) | string | not_documented | not_documented ; null not_documented | ( only relevant for nzb ) password for extracting downloaded archives |
| [info_hash](http://mafreebox.freebox.fr/doc/index.html#Download.info_hash) | string | not_documented | not_documented ; null not_documented | ( only relevant for bt ) torrent info_hash encoded in hexa |
| [piece_length](http://mafreebox.freebox.fr/doc/index.html#Download.piece_length) | int | not_documented | not_documented ; null not_documented | ( only relevant for bt ) torrent piece length in bytes |

### NzbConfigStatus

Source [NzbConfigStatus](http://mafreebox.freebox.fr/doc/index.html#NzbConfigStatus) ; module download.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [status](http://mafreebox.freebox.fr/doc/index.html#NzbConfigStatus.status) | enum | read_only | not_documented ; null not_documented | The valid config status are: Type Description not_checked config has not been checked yet checking test in progress error config is invalid, see error ok config is ok |
| [error](http://mafreebox.freebox.fr/doc/index.html#NzbConfigStatus.error) | enum | read_only | not_documented ; null not_documented | The valid config status are: Type Description none test is ok nzb_authentication_required authentication is required bad_authentication incorrect credentials connection_refused unable to connect to NNTP server |

### DhtStats

Source [DhtStats](http://mafreebox.freebox.fr/doc/index.html#DhtStats) ; module download.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [enabled](http://mafreebox.freebox.fr/doc/index.html#DhtStats.enabled) | bool | read_only | not_documented ; null not_documented | is the dht enabled |
| [node_count](http://mafreebox.freebox.fr/doc/index.html#DhtStats.node_count) | int | read_only | not_documented ; null not_documented | number of active nodes |
| [enabled_ipv6](http://mafreebox.freebox.fr/doc/index.html#DhtStats.enabled_ipv6) | bool | read_only | not_documented ; null not_documented | is the dht enabled on IPv6 |
| [node_count_ipv6](http://mafreebox.freebox.fr/doc/index.html#DhtStats.node_count_ipv6) | int | read_only | not_documented ; null not_documented | number of active nodes on IPv6 |

### DownloadStats

Source [DownloadStats](http://mafreebox.freebox.fr/doc/index.html#DownloadStats) ; module download.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [nb_tasks](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks) | int | read_only | not_documented ; null not_documented | total number of tasks |
| [nb_tasks_stopped](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_stopped) | int | read_only | not_documented ; null not_documented | number of stopped tasks |
| [nb_tasks_checking](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_checking) | int | read_only | not_documented ; null not_documented | number of checking tasks |
| [nb_tasks_queued](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_queued) | int | read_only | not_documented ; null not_documented | number of queued tasks |
| [nb_tasks_extracting](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_extracting) | int | read_only | not_documented ; null not_documented | number of extracting tasks |
| [nb_tasks_done](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_done) | int | read_only | not_documented ; null not_documented | number of done tasks |
| [nb_tasks_repairing](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_repairing) | int | read_only | not_documented ; null not_documented | number of repairing tasks |
| [nb_tasks_seeding](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_seeding) | int | read_only | not_documented ; null not_documented | number of seeding tasks |
| [nb_tasks_downloading](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_downloading) | int | read_only | not_documented ; null not_documented | number of downloading tasks |
| [nb_tasks_error](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_error) | int | read_only | not_documented ; null not_documented | number of error tasks |
| [nb_tasks_stopping](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_stopping) | int | read_only | not_documented ; null not_documented | number of stopping tasks |
| [nb_tasks_active](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_tasks_active) | int | read_only | not_documented ; null not_documented | number of active tasks (checking + queued + extracting + repairing + seeding + downloading) |
| [nb_rss](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_rss) | int | read_only | not_documented ; null not_documented | number of RSS feed subscriptions |
| [nb_rss_items_unread](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_rss_items_unread) | int | read_only | not_documented ; null not_documented | number of unread RSS items |
| [rx_rate](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.rx_rate) | int | read_only | not_documented ; null not_documented | current receive rate in bytes / second |
| [tx_rate](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.tx_rate) | int | read_only | not_documented ; null not_documented | current transmit rate in bytes / second |
| [throttling_mode](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.throttling_mode) | enum | read_only | not_documented ; null not_documented | active throttling_mode (see DlThrottlingConfig ) |
| [throttling_is_scheduled](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.throttling_is_scheduled) | bool | read_only | not_documented ; null not_documented | if true, the current throttling mode has been computed using the throttling schedule if false, the current throttling mode has been manually forced |
| [throttling_rate](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.throttling_rate) | :json:object:`DlRate` | read_only | not_documented ; null not_documented | current rate for throttling |
| [nzb_config_status](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nzb_config_status) | :json:object:`NzbConfigStatus` | read_only | not_documented ; null not_documented | current nzb configuration status |
| [conn_ready](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.conn_ready) | bool | read_only | not_documented ; null not_documented | is the connection ready |
| [nb_peer](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.nb_peer) | int | read_only | not_documented ; null not_documented | number of bittorrent peers |
| [blocklist_entries](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.blocklist_entries) | int | read_only | not_documented ; null not_documented | number of rules in blocklist |
| [blocklist_hits](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.blocklist_hits) | int | read_only | not_documented ; null not_documented | number of hits in blocklist |
| [dht_stats](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.dht_stats) | :json:object:`DhtStats` | read_only | not_documented ; null not_documented | dht stats |

### DownloadFile

Source [DownloadFile](http://mafreebox.freebox.fr/doc/index.html#DownloadFile) ; module download.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.id) | string | read_only | not_documented ; null not_documented | opaque id |
| [task_id](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.task_id) | int | read_only | not_documented ; null not_documented | id of the download task |
| [filepath](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.filepath) | string | read_only | not_documented ; null not_documented | full filepath on the disk (encoded as in file system api) |
| [name](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.name) | string | read_only | not_documented ; null not_documented | file name |
| [mimetype](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.mimetype) | string | read_only | not_documented ; null not_documented | file mimetype |
| [size](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.size) | int | read_only | not_documented ; null not_documented | file size in bytes |
| [rx](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.rx) | int | read_only | not_documented ; null not_documented | received bytes |
| [status](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.status) | enum | read_only | not_documented ; null not_documented | file download status Status Description queued file is queued for download error there was a problem with this file, see error to get the error code done file download is completed |
| [error](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.error) | enum | read_only | not_documented ; null not_documented | file error code in case status is error |
| [priority](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.priority) | string | not_documented | not_documented ; null not_documented | file download priority inside the download task Priority Description no_dl this file will not be downloaded low low priority normal default priority high high priority |
| [preview_url](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.preview_url) | string | read_only | not_documented ; null not_documented | url to preview downloaded file (only available for bittorrent) as a share link, this url can be use without requiring any form of authentication so that it can be passed as-is to any software. |

### DownloadTracker

Source [DownloadTracker](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker) ; module download.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [announce](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.announce) | string | read_only | not_documented ; null not_documented | tracker announce URL |
| [is_backup](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.is_backup) | bool | read_only | not_documented ; null not_documented | true if the tracker is a backup tracker (the downloader won’t connect to this tracker unless the primary tracker fails) |
| [status](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.status) | enum | read_only | not_documented ; null not_documented | tracker status Status Description unannounced not announced announcing announcing announce_failed an error occurred while trying to announce announced announced |
| [interval](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.interval) | int | read_only | not_documented ; null not_documented | desired interval between two announces (in seconds) |
| [min_interval](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.min_interval) | int | read_only | not_documented ; null not_documented | minimum interval between two announces (in seconds) |
| [reannounce_in](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.reannounce_in) | int | read_only | not_documented ; null not_documented | time left before reannounce (in seconds) |
| [nseeders](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.nseeders) | int | read_only | not_documented ; null not_documented | number of seeders announced on tracker |
| [nleechers](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.nleechers) | int | read_only | not_documented ; null not_documented | number of leechers announced on tracker |
| [is_enabled](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.is_enabled) | bool | not_documented | not_documented ; null not_documented | is the tracker enabled |

### DownloadPeer

Source [DownloadPeer](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer) ; module download.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [host](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.host) | string | read_only | not_documented ; null not_documented | peer IP |
| [port](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.port) | int | read_only | not_documented ; null not_documented | peer port |
| [state](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.state) | enum | read_only | not_documented ; null not_documented | peer state State Description disconnected not connected connecting trying to connect to the peer handshaking connected to the peer, negotiating capabilities ready ready to exchange data |
| [origin](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.origin) | enum | read_only | not_documented ; null not_documented | peer origin Origin Description tracker got the peer from the tracker incoming incoming peer dht got the peer from DHT pex got the peer from Peer exchange protocol user manually added peer |
| [protocol](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.protocol) | enum | read_only | not_documented ; null not_documented | Protocol Description tcp TCP tcp_obfuscated Obfuscated TCP udp UDP |
| [client](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.client) | string | read_only | not_documented ; null not_documented | Bittorrent client name |
| [country_code](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.country_code) | string | read_only | not_documented ; null not_documented | Peer country code (iso 3166) If country code is not available it will have the value “??” |
| [tx](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.tx) | int | read_only | not_documented ; null not_documented | transmitted bytes |
| [rx](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.rx) | int | read_only | not_documented ; null not_documented | received bytes |
| [tx_rate](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.tx_rate) | int | read_only | not_documented ; null not_documented | current transmit rate in byte/s |
| [rx_rate](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.rx_rate) | int | read_only | not_documented ; null not_documented | current receive rate in byte/s |
| [progress](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.progress) | int | read_only | not_documented ; null not_documented | peer current download progress |
| [requests](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.requests) | [] array of int | read_only | not_documented ; null not_documented | current requested pieces |

### DownloadBlacklistEntry

Source [DownloadBlacklistEntry](http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry) ; module download.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [host](http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.host) | string | read_only | not_documented ; null not_documented | entry ip |
| [reason](http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.reason) | enum | read_only | not_documented ; null not_documented | blacklist reason State Description not_blacklisted crypto_not_supported peer does not support encrypted connection connect_fail failed to connect hs_timeout handshake timeout hs_failed handshake failed hs_crypt_failed handshake failed during crypto hs_crypto_disabled handshake failed because encryption is disabled torrent_not_found torrent not found read_failed failed to read from peer write_failed failed to send data to peer crap_received received invalid data from peer conn_closed connection closed by remote peer timeout timeout blocklist peer is in a blocked ip range user manually blacklisted |
| [expire](http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.expire) | int | read_only | not_documented ; null not_documented | time left before blacklist removal |
| [global](http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.global) | bool | read_only | not_documented ; null not_documented | does this entry applies to all torrents |

### DownloadFeed

Source [DownloadFeed](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed) ; module download_feeds.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.id) | int | read_only | not_documented ; null not_documented | id |
| [status](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.status) | enum | read_only | not_documented ; null not_documented | The feed can have the following status Status Description ready feed is up to date fetching feed is updating error there was an error trying to refresh this feed, see error |
| [url](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.url) | string | read_only | not_documented ; null not_documented | Feed URL |
| [title](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.title) | string | read_only | not_documented ; null not_documented | Feed title (extracted from the RSS) |
| [desc](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.desc) | string | read_only | not_documented ; null not_documented | Feed description (extracted from the RSS) |
| [image_url](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.image_url) | string | read_only | not_documented ; null not_documented | Feed image URL (extracted from the RSS) |
| [nb_read](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.nb_read) | int | read_only | not_documented ; null not_documented | Number of read items in the feed |
| [nb_unread](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.nb_unread) | int | read_only | not_documented ; null not_documented | Number of unread items in the feed |
| [auto_download](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.auto_download) | bool | not_documented | not_documented ; null not_documented | If set to true, the downloader will automatically download new items |
| [fetch_ts](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.fetch_ts) | timestamp | read_only | not_documented ; null not_documented | Last time the feed was fetched |
| [pub_ts](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.pub_ts) | timestamp | read_only | not_documented ; null not_documented | Last time the feed was published on remote server |
| [error](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.error) | enum | read_only | not_documented ; null not_documented | Error code (same as used in Download or DownloadFile ). |

### DownloadFeedItem

Source [DownloadFeedItem](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem) ; module download_feeds.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.id) | int | read_only | not_documented ; null not_documented | id |
| [feed_id](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.feed_id) | int | read_only | not_documented ; null not_documented | id of the DownloadFeed |
| [title](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.title) | string | read_only | not_documented ; null not_documented | item title |
| [desc](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.desc) | string | read_only | not_documented ; null not_documented | item description |
| [author](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.author) | string | read_only | not_documented ; null not_documented | item author |
| [link](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.link) | string | read_only | not_documented ; null not_documented | URL of the RSS feed attachment |
| [is_read](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.is_read) | bool | not_documented | not_documented ; null not_documented | you can mark the item as read manually, or it is marked as read automatically when the item is downloaded |
| [is_downloaded](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.is_downloaded) | bool | read_only | not_documented ; null not_documented | mark downloaded items, automatically set to true when RSS item is downloaded |
| [fetch_ts](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.fetch_ts) | timestamp | read_only | not_documented ; null not_documented | timestamp of the item creation |
| [pub_ts](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.pub_ts) | timestamp | read_only | not_documented ; null not_documented | item publish timestamp |
| [enclosure_url](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.enclosure_url) | string | read_only | not_documented ; null not_documented | enclosure URL (if specified in RSS feed) |
| [enclosure_type](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.enclosure_type) | string | read_only | not_documented ; null not_documented | enclosure mime type (if specified in RSS feed) |
| [enclosure_length](http://mafreebox.freebox.fr/doc/index.html#DownloadFeedItem.enclosure_length) | int | read_only | not_documented ; null not_documented | enclosure size in bytes (if specified in RSS feed) |

### DownloadConfiguration

Source [DownloadConfiguration](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration) ; module download_config.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [max_downloading_tasks](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.max_downloading_tasks) | int | not_documented | not_documented ; null not_documented | max concurrent download tasks |
| [download_dir](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.download_dir) | string | not_documented | not_documented ; null not_documented | the default path where downloads will be stored (base64 encoded) |
| [watch_dir](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.watch_dir) | string | not_documented | not_documented ; null not_documented | special folder that will be monitored. When a new supported file (.nzb, .torrent) is copied in that folder, the task is automatically added to the download queue. (base64 encoded) |
| [use_watch_dir](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.use_watch_dir) | bool | not_documented | not_documented ; null not_documented | if set to false, the watch_dir will not be monitored |
| [throttling](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.throttling) | DlThrottlingConfig | not_documented | not_documented ; null not_documented | throttling configuration |
| [news](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.news) | DlNewsConfig | not_documented | not_documented ; null not_documented | newsgroups configuration |
| [bt](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.bt) | DlBtConfig | not_documented | not_documented ; null not_documented | bittorrent configuration |
| [feed](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.feed) | DlFeedConfig | not_documented | not_documented ; null not_documented | RSS feed configuration |
| [blocklist](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.blocklist) | DlBlockListConfig | not_documented | not_documented ; null not_documented | block list configuration |
| [dns1](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.dns1) | string | not_documented | not_documented ; null not_documented | dns server ip to use for downloader (leave blank for default dns server) |
| [dns2](http://mafreebox.freebox.fr/doc/index.html#DownloadConfiguration.dns2) | string | not_documented | not_documented ; null not_documented | dns server ip to use for downloader |

### DlThrottlingConfig

Source [DlThrottlingConfig](http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig) ; module download_config.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [normal](http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.normal) | DlRate | not_documented | not_documented ; null not_documented | download rate for normal time slot (in B/s) |
| [slow](http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.slow) | DlRate | not_documented | not_documented ; null not_documented | download rate for normal slow slot (in B/s) |
| [schedule](http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.schedule) | enum[168] | not_documented | not_documented ; null not_documented | The schedule array represent the list of week hours timeslot, starting on monday a midnight. Therefore the complete week is represented in a array of 168 elements (24 * 7) Each slot can have the following value: Type Description normal downloads will use normal DlRate config for this timeslot slow downloads will use slow DlRate config for this timeslot hibernate downloads will be paused for this timeslot |
| [mode](http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.mode) | enum | not_documented | not_documented ; null not_documented | Throttling mode can have to following values Type Description normal force use of normal rate limits (not using the scheduler) slow force use of slow rate limits (not using the scheduler) hibernate force hibernate (not using the scheduler) schedule use scheduded rate limit |

### DlRate

Source [DlRate](http://mafreebox.freebox.fr/doc/index.html#DlRate) ; module download_config.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [tx_rate](http://mafreebox.freebox.fr/doc/index.html#DlRate.tx_rate) | int | not_documented | not_documented ; null not_documented | maximum transmit rate (in byte/s) 0 means no limit |
| [rx_rate](http://mafreebox.freebox.fr/doc/index.html#DlRate.rx_rate) | int | not_documented | not_documented ; null not_documented | maximum receive rate (in byte/s) 0 means no limit |

### DlNewsConfig

Source [DlNewsConfig](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig) ; module download_config.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [server](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.server) | string | not_documented | not_documented ; null not_documented | NNTP server hostname |
| [port](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.port) | int | not_documented | not_documented ; null not_documented | NNTP server port |
| [ssl](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.ssl) | bool | not_documented | not_documented ; null not_documented | Use SSL to connect to server if set to true |
| [user](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.user) | string | not_documented | not_documented ; null not_documented | NNTP auth username (can be empty if no auth is required) |
| [password](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.password) | string | write_only | not_documented ; null not_documented | NNTP auth password (can be empty if no auth is required) |
| [nthreads](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.nthreads) | int | not_documented | not_documented ; null not_documented | maximum concurrent connections to the NNTP server |
| [auto_repair](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.auto_repair) | bool | not_documented | not_documented ; null not_documented | automatically check and repair downloaded files using the provided par2 files |
| [lazy_par2](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.lazy_par2) | bool | not_documented | not_documented ; null not_documented | if set to true the downloader will download the par2 files only if the download is corrupted |
| [auto_extract](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.auto_extract) | bool | not_documented | not_documented ; null not_documented | automatically attempt to extract downloaded files |
| [erase_tmp](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.erase_tmp) | bool | not_documented | not_documented ; null not_documented | if auto_extract is enabled, delete archive files once successfully extracted |

### DlBtConfig

Source [DlBtConfig](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig) ; module download_config.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [max_peers](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.max_peers) | int | not_documented | not_documented ; null not_documented | maximum number of peers at a given time |
| [stop_ratio](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.stop_ratio) | int | not_documented | not_documented ; null not_documented | default stop_ratio for bt Download tasks This value is scaled by a factor 100 , for instance a stop_ratio of 200 means that the task will stop once tx_bytes = 2 * size A value of 0 means that the task will continue seeding until it is manually stopped |
| [crypto_support](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.crypto_support) | enum | not_documented | not_documented ; null not_documented | The crypto_support can have the following values Type Description unsupported will never use bittorrent crypto allowed will select plain during handshake preferred will select crypto during handshake required will allow plain bittorrent |
| [enable_dht](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.enable_dht) | bool | not_documented | not_documented ; null not_documented | enable the dht protocol |
| [enable_pex](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.enable_pex) | bool | not_documented | not_documented ; null not_documented | enable the peer exchange protocol |
| [announce_timeout](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.announce_timeout) | int | not_documented | not_documented ; null not_documented | timeout in seconds for announcing to tracker |
| [main_port](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.main_port) | int | not_documented | not_documented ; null not_documented | main bittorrent port |
| [dht_port](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.dht_port) | int | not_documented | not_documented ; null not_documented | bittorrent dht port |

### DlFeedConfig

Source [DlFeedConfig](http://mafreebox.freebox.fr/doc/index.html#DlFeedConfig) ; module download_config.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [fetch_interval](http://mafreebox.freebox.fr/doc/index.html#DlFeedConfig.fetch_interval) | int | not_documented | not_documented ; null not_documented | interval between automatic RSS refresh (in minutes) |
| [max_items](http://mafreebox.freebox.fr/doc/index.html#DlFeedConfig.max_items) | int | not_documented | not_documented ; null not_documented | maximum feed item to keep |

### DlBlockListConfig

Source [DlBlockListConfig](http://mafreebox.freebox.fr/doc/index.html#DlBlockListConfig) ; module download_config.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [sources[]](http://mafreebox.freebox.fr/doc/index.html#DlBlockListConfig.sources[]) | string | not_documented | not_documented ; null not_documented | list of block list URL source The block list should be in cidr format e.g.: http://list.iblocklist.com/?list=bt_level1&fileformat=cidr&archiveformat= |

### FsTask

Source [FsTask](http://mafreebox.freebox.fr/doc/index.html#FsTask) ; module fs.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#FsTask.id) | int | read_only | not_documented ; null not_documented | id |
| [type](http://mafreebox.freebox.fr/doc/index.html#FsTask.type) | enum | read_only | not_documented ; null not_documented | The valid task types are: Type Description cat Concatenate multiple files cp Copy files mv Move files rm Remove files archive Creates an archive extract Extract an archive repair Check and repair files |
| [state](http://mafreebox.freebox.fr/doc/index.html#FsTask.state) | enum | not_documented | not_documented ; null not_documented | State Description queued Queued (only one task is active at a given time) running Running paused Paused (user suspended) done Done failed Failed (see error) |
| [error](http://mafreebox.freebox.fr/doc/index.html#FsTask.error) | enum | read_only | not_documented ; null not_documented | Error Description none No error archive_read_failed Error reading archive archive_open_failed Error opening archive archive_write_failed Error writing archive chdir_failed Error changing directory dest_is_not_dir The destination is not a directory file_exists File already exists file_not_found File not found mkdir_failed Unable to create directory open_input_failed Error opening input file open_output_failed Error opening output file opendir_failed Error opening directory overwrite_failed Error overwriting file path_too_big Path is too long repair_failed Failed to repair corrupted files rmdir_failed Error removing directory same_file Source and Destination are the same file unlink_failed Error removing file unsupported_file_type This file type is not supported write_failed Error writing file disk_full Disk is full internal Internal error invalid_format Invalid file format (corrupted ?) incorrect_password Invalid or missing password for extraction permission_denied Permission denied readlink_failed Failed to read the target of a symbolic link symlink_failed Failed to create a symbolic link copy_into_itself Attempted to copy a directory to a subdirectory of itself truncate_failed Failed to truncate file |
| [created_ts](http://mafreebox.freebox.fr/doc/index.html#FsTask.created_ts) | timestamp | read_only | not_documented ; null not_documented | task creation timestamp |
| [started_ts](http://mafreebox.freebox.fr/doc/index.html#FsTask.started_ts) | timestamp | read_only | not_documented ; null not_documented | task start timestamp |
| [done_ts](http://mafreebox.freebox.fr/doc/index.html#FsTask.done_ts) | timestamp | read_only | not_documented ; null not_documented | task end timestamp |
| [duration](http://mafreebox.freebox.fr/doc/index.html#FsTask.duration) | int | read_only | not_documented ; null not_documented | task duration in seconds |
| [progress](http://mafreebox.freebox.fr/doc/index.html#FsTask.progress) | int | read_only | not_documented ; null not_documented | task progress in percent (scaled by 100) |
| [eta](http://mafreebox.freebox.fr/doc/index.html#FsTask.eta) | int | read_only | not_documented ; null not_documented | estimated time remaining before the task completion (in seconds) |
| [from](http://mafreebox.freebox.fr/doc/index.html#FsTask.from) | string | read_only | not_documented ; null not_documented | current source file (if available) |
| [to](http://mafreebox.freebox.fr/doc/index.html#FsTask.to) | string | read_only | not_documented ; null not_documented | current destination file (if available) |
| [nfiles](http://mafreebox.freebox.fr/doc/index.html#FsTask.nfiles) | int | read_only | not_documented ; null not_documented | number of files to process |
| [nfiles_done](http://mafreebox.freebox.fr/doc/index.html#FsTask.nfiles_done) | int | read_only | not_documented ; null not_documented | number of files processed |
| [total_bytes](http://mafreebox.freebox.fr/doc/index.html#FsTask.total_bytes) | int | read_only | not_documented ; null not_documented | total bytes to process |
| [total_bytes_done](http://mafreebox.freebox.fr/doc/index.html#FsTask.total_bytes_done) | int | read_only | not_documented ; null not_documented | number of bytes processed |
| [curr_bytes](http://mafreebox.freebox.fr/doc/index.html#FsTask.curr_bytes) | int | read_only | not_documented ; null not_documented | size of the file currently processed |
| [curr_bytes_done](http://mafreebox.freebox.fr/doc/index.html#FsTask.curr_bytes_done) | int | read_only | not_documented ; null not_documented | number of bytes processed for the current file |
| [rate](http://mafreebox.freebox.fr/doc/index.html#FsTask.rate) | int | read_only | not_documented ; null not_documented | processing rate in byte/s |
| [src](http://mafreebox.freebox.fr/doc/index.html#FsTask.src) | [] array of string | read_only | not_documented ; null not_documented | task source files |
| [dst](http://mafreebox.freebox.fr/doc/index.html#FsTask.dst) | string | read_only | not_documented ; null not_documented | task destination path |

### FileInfo

Source [FileInfo](http://mafreebox.freebox.fr/doc/index.html#FileInfo) ; module fs.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [path](http://mafreebox.freebox.fr/doc/index.html#FileInfo.path) | string | read_only | not_documented ; null not_documented | file path (encoded in base64 as explained in Path Encoding ) |
| [name](http://mafreebox.freebox.fr/doc/index.html#FileInfo.name) | string | read_only | not_documented ; null not_documented | file name (in clear text) |
| [mimetype](http://mafreebox.freebox.fr/doc/index.html#FileInfo.mimetype) | string | read_only | not_documented ; null not_documented | file mimetype |
| [type](http://mafreebox.freebox.fr/doc/index.html#FileInfo.type) | enum | not_documented | not_documented ; null not_documented | Type Description dir Directory file Regular file |
| [size](http://mafreebox.freebox.fr/doc/index.html#FileInfo.size) | int | read_only | not_documented ; null not_documented | file size in bytes |
| [modification](http://mafreebox.freebox.fr/doc/index.html#FileInfo.modification) | int | read_only | not_documented ; null not_documented | file modification timestamp |
| [index](http://mafreebox.freebox.fr/doc/index.html#FileInfo.index) | int | read_only | not_documented ; null not_documented | display order for natural sort |
| [link](http://mafreebox.freebox.fr/doc/index.html#FileInfo.link) | boolean | read_only | not_documented ; null not_documented | is this file a link |
| [target](http://mafreebox.freebox.fr/doc/index.html#FileInfo.target) | string | read_only | not_documented ; null not_documented | symlink target path (encoded in base64 as explained in Path Encoding ) (only present when link is set to true) |
| [hidden](http://mafreebox.freebox.fr/doc/index.html#FileInfo.hidden) | boolean | read_only | not_documented ; null not_documented | should the file be hidden to user |
| [foldercount](http://mafreebox.freebox.fr/doc/index.html#FileInfo.foldercount) | int | read_only | not_documented ; null not_documented | number of subfolders only relevant for dir, only provided if “countSubFolder” parameter is set |
| [filecount](http://mafreebox.freebox.fr/doc/index.html#FileInfo.filecount) | int | read_only | not_documented ; null not_documented | number of files inside directory only relevant for dir, only provided if “countSubFolder” parameter is set |
| [exif](http://mafreebox.freebox.fr/doc/index.html#FileInfo.exif) | object | read_only | not_documented ; null not_documented | EXIF metadada if available. only relevant for supported image files (JPEG, HEIC), when the “exifMode” parameter is set |

### ShareLink

Source [ShareLink](http://mafreebox.freebox.fr/doc/index.html#ShareLink) ; module share.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [token](http://mafreebox.freebox.fr/doc/index.html#ShareLink.token) | string | read_only | not_documented ; null not_documented | The link unique sharing token |
| [path](http://mafreebox.freebox.fr/doc/index.html#ShareLink.path) | string | read_only | not_documented ; null not_documented | The root path of the share, if the path is a regular file, only this file will be shared |
| [name](http://mafreebox.freebox.fr/doc/index.html#ShareLink.name) | string | read_only | not_documented ; null not_documented | The readable name of the shared file/folder |
| [expire](http://mafreebox.freebox.fr/doc/index.html#ShareLink.expire) | timestamp | read_only | not_documented ; null not_documented | Link expiration timestamp, 0 means no expiration. |
| [fullurl](http://mafreebox.freebox.fr/doc/index.html#ShareLink.fullurl) | string | read_only | not_documented ; null not_documented | Full URL to use for remote access. If remote access is disabled, the field will be empty. |

### FileUpload

Source [FileUpload](http://mafreebox.freebox.fr/doc/index.html#FileUpload) ; module upload.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#FileUpload.id) | int | read_only | not_documented ; null not_documented | upload id |
| [size](http://mafreebox.freebox.fr/doc/index.html#FileUpload.size) | int | read_only | not_documented ; null not_documented | Upload file size in bytes |
| [uploaded](http://mafreebox.freebox.fr/doc/index.html#FileUpload.uploaded) | int | read_only | not_documented ; null not_documented | Uploaded bytes |
| [status](http://mafreebox.freebox.fr/doc/index.html#FileUpload.status) | enum | read_only | not_documented ; null not_documented | upload status can have the following values status Description authorized Upload authorization is valid, upload has not started yet in_progress Upload in progress done Upload done failed Upload failed conflict Destination file conflict timeout Upload authorization is no longer valid cancelled Upload cancelled by user |
| [start_date](http://mafreebox.freebox.fr/doc/index.html#FileUpload.start_date) | timestamp | read_only | not_documented ; null not_documented | upload start date |
| [last_update](http://mafreebox.freebox.fr/doc/index.html#FileUpload.last_update) | timestamp | read_only | not_documented ; null not_documented | last update of file upload object |
| [upload_name](http://mafreebox.freebox.fr/doc/index.html#FileUpload.upload_name) | string | read_only | not_documented ; null not_documented | name of the file uploaded |
| [dirname](http://mafreebox.freebox.fr/doc/index.html#FileUpload.dirname) | string | read_only | not_documented ; null not_documented | upload destination directory |

### FileUploadStartAction

Source [FileUploadStartAction](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction) ; module upload.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [request_id](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.request_id) | int | not_documented | optional ; null not_documented | optional request_id |
| [action](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.action) | string | not_documented | not_documented ; null not_documented | must be ‘upload_start’ |
| [size](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.size) | int | not_documented | optional ; null not_documented | optional file size |
| [dirname](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.dirname) | string | not_documented | not_documented ; null not_documented | the destination directory (encoded value) |
| [filename](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.filename) | string | not_documented | not_documented ; null not_documented | the destination filename |
| [force](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.force) | enum | not_documented | not_documented ; null not_documented | select the way conflicts are handled Force mode Description missing The response to the FileUploadStartAction will be an error with ‘destination_conflict’ if the destination file already exists. The response will also contain a file_size attribute containing the existing file length (useful for resuming upload) overwrite If the target file already exists it will be overridden resume The upload will resume, all sent chunks will then be appended to the existing file. |

### FileUploadFinalizeAction

Source [FileUploadFinalizeAction](http://mafreebox.freebox.fr/doc/index.html#FileUploadFinalizeAction) ; module upload.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [request_id](http://mafreebox.freebox.fr/doc/index.html#FileUploadFinalizeAction.request_id) | int | not_documented | optional ; null not_documented | optional request_id |
| [action](http://mafreebox.freebox.fr/doc/index.html#FileUploadFinalizeAction.action) | string | not_documented | not_documented ; null not_documented | must be ‘upload_finalize’ |

### FileUploadCancelAction

Source [FileUploadCancelAction](http://mafreebox.freebox.fr/doc/index.html#FileUploadCancelAction) ; module upload.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [request_id](http://mafreebox.freebox.fr/doc/index.html#FileUploadCancelAction.request_id) | int | not_documented | optional ; null not_documented | optional request_id |
| [action](http://mafreebox.freebox.fr/doc/index.html#FileUploadCancelAction.action) | string | not_documented | not_documented ; null not_documented | must be ‘upload_cancel’ |

### FileUploadChunkResponse

Source [FileUploadChunkResponse](http://mafreebox.freebox.fr/doc/index.html#FileUploadChunkResponse) ; module upload.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [total_len](http://mafreebox.freebox.fr/doc/index.html#FileUploadChunkResponse.total_len) | int | not_documented | not_documented ; null not_documented | target file current length |
| [complete](http://mafreebox.freebox.fr/doc/index.html#FileUploadChunkResponse.complete) | bool | not_documented | not_documented ; null not_documented | will be true in a reply to FileUploadFinalizeAction or FileUploadCancelAction |
| [cancelled](http://mafreebox.freebox.fr/doc/index.html#FileUploadChunkResponse.cancelled) | bool | not_documented | not_documented ; null not_documented | will be true in a reply FileUploadCancelAction |

### RRDFetch

Source [RRDFetch](http://mafreebox.freebox.fr/doc/index.html#RRDFetch) ; module rrd.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [db](http://mafreebox.freebox.fr/doc/index.html#RRDFetch.db) | enum | not_documented | not_documented ; null not_documented | Name of the rrd database to read. It can take one of the following values Db Description net network stats temp temperature stats dsl xDSL stats switch switch stats |
| [date_start](http://mafreebox.freebox.fr/doc/index.html#RRDFetch.date_start) | int | not_documented | optional ; null not_documented | The requested start timestamp of the stats to get NOTE: this can be adjusted to fit the best available resolution |
| [date_end](http://mafreebox.freebox.fr/doc/index.html#RRDFetch.date_end) | int | not_documented | optional ; null not_documented | The requested end timestamp of the stats to get NOTE: this can be adjusted to fit the best available resolution |
| [precision](http://mafreebox.freebox.fr/doc/index.html#RRDFetch.precision) | int | not_documented | optional ; null not_documented | By default all values are cast to int, if you need floating point precision you can provide a precision factor that will be applied to all values before being returned. For instance if you want 2 digit precision you should use a precision of 100, and divide the obtained results by 100. |
| [fields](http://mafreebox.freebox.fr/doc/index.html#RRDFetch.fields) | [] array of string | not_documented | optional ; null not_documented | If you are only interested in getting some fields you can provide the list of fields you want to get. |

### OperationProgress

Source [OperationProgress](http://mafreebox.freebox.fr/doc/index.html#OperationProgress) ; module storage.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [done_steps](http://mafreebox.freebox.fr/doc/index.html#OperationProgress.done_steps) | int | read_only | not_documented ; null not_documented | number of steps done |
| [max_steps](http://mafreebox.freebox.fr/doc/index.html#OperationProgress.max_steps) | int | read_only | not_documented ; null not_documented | total number of steps |
| [percent](http://mafreebox.freebox.fr/doc/index.html#OperationProgress.percent) | int | read_only | not_documented ; null not_documented | current step progress |

### DiskPartition

Source [DiskPartition](http://mafreebox.freebox.fr/doc/index.html#DiskPartition) ; module storage.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.id) | int | read_only | not_documented ; null not_documented | unique partition id |
| [disk_id](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.disk_id) | int | read_only | not_documented ; null not_documented | related disk id |
| [state](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.state) | enum | not_documented | not_documented ; null not_documented | state Description error Partition has error checking Partition check in progress formatting Partition format in progress mounting Partition mount in progress maintenance Partition is in maintenance mode mounted Partition is ready umounting Partition umount in progress umounted Partition is umounted ejecting Partition ejection in progress |
| [fstype](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.fstype) | enum | read_only | not_documented ; null not_documented | fstype empty unknown xfs ext4 vfat ntfs hf hfsplus swap exfat |
| [label](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.label) | string | not_documented | not_documented ; null not_documented | partition name |
| [path](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.path) | string | read_only | not_documented ; null not_documented | partition mount point (encoded in base64 as explained in fs API) |
| [total_bytes](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.total_bytes) | int | read_only | not_documented ; null not_documented | partition size (in bytes) |
| [used_bytes](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.used_bytes) | int | read_only | not_documented ; null not_documented | partition used space (in bytes) |
| [free_bytes](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.free_bytes) | int | read_only | not_documented ; null not_documented | partition free space (in bytes) |
| [fsck_result](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.fsck_result) | enum | read_only | not_documented ; null not_documented | fsck result state Description no_run_yet Partition has not been checked yet running Check is in progress fs_clean File system is ok fs_corrected File system was corrected fs_needs_correction File system need correction failed File system has unrecoverable error |
| [operation_pct](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.operation_pct) | OperationProgress | read_only | not_documented ; null not_documented | partition operation progress |

### StorageDisk

Source [StorageDisk](http://mafreebox.freebox.fr/doc/index.html#StorageDisk) ; module storage.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.id) | int | read_only | not_documented ; null not_documented | the disk id |
| [type](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.type) | enum | read_only | not_documented ; null not_documented | type Description internal Freebox internal disk usb usb disk sata sata disk nvme nvme disk |
| [state](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.state) | enum | not_documented | not_documented ; null not_documented | state Description error Disk has error disabled Disk is disabled enabled Disk is enabled formatting Disk is formatting |
| [connector](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.connector) | int | read_only | not_documented ; null not_documented | Disk physical connector id |
| [total_bytes](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.total_bytes) | int | read_only | not_documented ; null not_documented | Disk size (in bytes) |
| [table_type](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.table_type) | int | read_only | not_documented ; null not_documented | table_type msdos gpt superfloppy empty |
| [model](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.model) | string | read_only | not_documented ; null not_documented | Disk model |
| [serial](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.serial) | string | read_only | not_documented ; null not_documented | Disk serial number |
| [firmware](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.firmware) | string | read_only | not_documented ; null not_documented | Disk firmware version |
| [temp](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.temp) | int | read_only | not_documented ; null not_documented | Disk temperature (when supported) in °C |
| [operation_pct](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.operation_pct) | OperationProgress | read_only | not_documented ; null not_documented | partition operation progress |
| [partitions](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.partitions) | [] array of DiskPartition | read_only | not_documented ; null not_documented | list of disk partitions |
| [idle](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.idle) | bool | read_only | not_documented ; null not_documented | is disk idle (when available) |
| [idle_duration](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.idle_duration) | int | read_only | not_documented ; null not_documented | disk idle duration (in seconds) (when available) |
| [spinning](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.spinning) | bool | read_only | not_documented ; null not_documented | is disk spinning (when available) |
| [active_duration](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.active_duration) | int | read_only | not_documented ; null not_documented | disk activity duration (in seconds) (when available) |
| [time_before_spindown](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.time_before_spindown) | int | read_only | not_documented ; null not_documented | seconds left before disk spin down (in seconds) (when available) |
| [read_requests](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.read_requests) | int | read_only | not_documented ; null not_documented | Number of read requests sent since to disk since boot (when available) |
| [read_error_requests](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.read_error_requests) | int | read_only | not_documented ; null not_documented | Number of read requests in error since boot. Might indicate disk failure (when available) |
| [write_requests](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.write_requests) | int | read_only | not_documented ; null not_documented | Number of write requests sent since to disk since boot (when available) |
| [write_error_requests](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.write_error_requests) | int | read_only | not_documented ; null not_documented | Number of write requests in error since boot. Might indicate disk failure (when available) |

### StorageConfig

Source [StorageConfig](http://mafreebox.freebox.fr/doc/index.html#StorageConfig) ; module storage.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [external_pm_enabled](http://mafreebox.freebox.fr/doc/index.html#StorageConfig.external_pm_enabled) | bool | not_documented | not_documented ; null not_documented | enable/disable external disk power management |
| [external_pm_idle_before_spindown](http://mafreebox.freebox.fr/doc/index.html#StorageConfig.external_pm_idle_before_spindown) | int | not_documented | not_documented ; null not_documented | idle time in minutes to wait before spinning down an external disk |

### RaidArray

Source [RaidArray](http://mafreebox.freebox.fr/doc/index.html#RaidArray) ; module raid.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#RaidArray.id) | int | read_only | not_documented ; null not_documented | unique id of this array. Used as a reference for API calls. |
| [state](http://mafreebox.freebox.fr/doc/index.html#RaidArray.state) | enum | not_documented | not_documented ; null not_documented | state Description stopped Array is stopped running Array is running error Array is in error |
| [name](http://mafreebox.freebox.fr/doc/index.html#RaidArray.name) | string | not_documented | not_documented ; null not_documented | The array name |
| [level](http://mafreebox.freebox.fr/doc/index.html#RaidArray.level) | enum | not_documented | not_documented ; null not_documented | level Description basic Basic RAID level, like a single drive raid1 array raid0 RAID 0 raid1 RAID 1 raid5 RAID 5 raid10 RAID 10 |
| [disk_id](http://mafreebox.freebox.fr/doc/index.html#RaidArray.disk_id) | int | read_only | not_documented ; null not_documented | The disk id of the array, for use with the disk format API. |
| [uuid](http://mafreebox.freebox.fr/doc/index.html#RaidArray.uuid) | string | read_only | not_documented ; null not_documented | The array unique id. Only this id is guaranteed to stay stable across reboots. |
| [sync_action](http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_action) | enum | read_only | not_documented ; null not_documented | sync_action Description idle Array is idle resync Sync operation in progress recover Recover operation in progress check Array is being checked repair Repair operation in progress reshape Array growth in progress frozen Array is frozen |
| [sysfs_state](http://mafreebox.freebox.fr/doc/index.html#RaidArray.sysfs_state) | enum | read_only | not_documented ; null not_documented | Low-level Linux-specific md state value read in sysfs array_state property . sysfs_state clear inactive suspended readonly read_auto clean active write_pending active_idle |
| [array_size](http://mafreebox.freebox.fr/doc/index.html#RaidArray.array_size) | int | read_only | not_documented ; null not_documented | Size of array in bytes. |
| [raid_disks](http://mafreebox.freebox.fr/doc/index.html#RaidArray.raid_disks) | int | read_only | not_documented ; null not_documented | Number of members that should be in this array. |
| [sync_speed](http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_speed) | int | read_only | not_documented ; null not_documented | Sync speed in bytes per second |
| [sync_completed_pos](http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_completed_pos) | int | read_only | not_documented ; null not_documented | Current position of sync process. |
| [sync_completed_end](http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_completed_end) | int | read_only | not_documented ; null not_documented | End position of sync process: total of bytes to sync. |
| [sync_completed_percent](http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_completed_percent) | int | read_only | not_documented ; null not_documented | Percentage of sync completion. |
| [check_interval](http://mafreebox.freebox.fr/doc/index.html#RaidArray.check_interval) | int | read_only | not_documented ; null not_documented | Check interval in seconds. |
| [last_check](http://mafreebox.freebox.fr/doc/index.html#RaidArray.last_check) | int | read_only | not_documented ; null not_documented | Unix timestamp of last check in seconds. |
| [next_check](http://mafreebox.freebox.fr/doc/index.html#RaidArray.next_check) | int | read_only | not_documented ; null not_documented | Unix timestamp of next check in seconds. Might be 0 if check_interval is 0. |
| [degraded](http://mafreebox.freebox.fr/doc/index.html#RaidArray.degraded) | bool | read_only | not_documented ; null not_documented | Whether the array is degraded or not. |
| [members](http://mafreebox.freebox.fr/doc/index.html#RaidArray.members) | [] array of RaidMember | not_documented | not_documented ; null not_documented | List of members of this array |

### RaidMember

Source [RaidMember](http://mafreebox.freebox.fr/doc/index.html#RaidMember) ; module raid.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#RaidMember.id) | int | read_only | not_documented ; null not_documented | unique id of this member. This corresponds to the disk id, usable with the Storage Disk API. |
| [array_id](http://mafreebox.freebox.fr/doc/index.html#RaidMember.array_id) | int | read_only | not_documented ; null not_documented | id of the array this member is in |
| [role](http://mafreebox.freebox.fr/doc/index.html#RaidMember.role) | enum | read_only | not_documented ; null not_documented | role Description active Active member of the array faulty Faulty member spare Member kept as spare missing Missing (removed or dead) member of the array |
| [set_name](http://mafreebox.freebox.fr/doc/index.html#RaidMember.set_name) | string | read_only | not_documented ; null not_documented | name of the array this member is into |
| [set_uuid](http://mafreebox.freebox.fr/doc/index.html#RaidMember.set_uuid) | string | read_only | not_documented ; null not_documented | uuid of the array this member is into |
| [dev_uuid](http://mafreebox.freebox.fr/doc/index.html#RaidMember.dev_uuid) | string | read_only | not_documented ; null not_documented | uuid of this member |
| [device_location](http://mafreebox.freebox.fr/doc/index.html#RaidMember.device_location) | enum | read_only | not_documented ; null not_documented | internal location of this member. Possible slot values: sata-internal-p0, sata-internal-p1, sata-internal-p2, sata-internal-p4 |
| [total_bytes](http://mafreebox.freebox.fr/doc/index.html#RaidMember.total_bytes) | int | read_only | not_documented ; null not_documented | size of this member in bytes |
| [active_device](http://mafreebox.freebox.fr/doc/index.html#RaidMember.active_device) | int | read_only | not_documented ; null not_documented | device number inside the array |
| [corrected_read_errors](http://mafreebox.freebox.fr/doc/index.html#RaidMember.corrected_read_errors) | int | read_only | not_documented ; null not_documented | Device read errors count |
| [sct_erc_supported](http://mafreebox.freebox.fr/doc/index.html#RaidMember.sct_erc_supported) | bool | read_only | not_documented ; null not_documented | Whether SCT_ERC is supported by the device according to its S.M.A.R.T. data. |
| [sct_erc_enabled](http://mafreebox.freebox.fr/doc/index.html#RaidMember.sct_erc_enabled) | bool | read_only | not_documented ; null not_documented | Whether SCT_ERC is enabled on the device according to its S.M.A.R.T. data. |
| [disk](http://mafreebox.freebox.fr/doc/index.html#RaidMember.disk) | RaidDisk | read_only | not_documented ; null not_documented | A few properties of the disk. |

### RaidDisk

Source [RaidDisk](http://mafreebox.freebox.fr/doc/index.html#RaidDisk) ; module raid.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [model](http://mafreebox.freebox.fr/doc/index.html#RaidDisk.model) | string | read_only | not_documented ; null not_documented | Disk model. |
| [serial](http://mafreebox.freebox.fr/doc/index.html#RaidDisk.serial) | string | read_only | not_documented ; null not_documented | Disk serial number. |
| [firmware](http://mafreebox.freebox.fr/doc/index.html#RaidDisk.firmware) | string | read_only | not_documented ; null not_documented | Disk firmware revision |
| [temp](http://mafreebox.freebox.fr/doc/index.html#RaidDisk.temp) | int | read_only | not_documented ; null not_documented | Disk temperature in °C. |

### VM

Source [VM](http://mafreebox.freebox.fr/doc/index.html#VM) ; module vm.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#VM.id) | int | read_only | not_documented ; null not_documented | unique id of this VM |
| [name](http://mafreebox.freebox.fr/doc/index.html#VM.name) | string | not_documented | not_documented ; null not_documented | Name of this VM. Max 31 characters. |
| [disk_path](http://mafreebox.freebox.fr/doc/index.html#VM.disk_path) | string | not_documented | not_documented ; null not_documented | Base64-encoded path to the hard disk image of this VM. |
| [disk_type](http://mafreebox.freebox.fr/doc/index.html#VM.disk_type) | enum | not_documented | not_documented ; null not_documented | Type of disk image. disk_type Description raw Raw disk data qcow2 Qcow2 image type. Usually qcow version 3. Note: not all features are supported. In particular, reference to other images is disabled. |
| [cd_path](http://mafreebox.freebox.fr/doc/index.html#VM.cd_path) | string | not_documented | optional ; null not_documented | Base64-encoded path to CDROM device ISO image. Optional. |
| [memory](http://mafreebox.freebox.fr/doc/index.html#VM.memory) | int | not_documented | not_documented ; null not_documented | Memory allocated to this VM in megabytes. |
| [vcpus](http://mafreebox.freebox.fr/doc/index.html#VM.vcpus) | int | not_documented | not_documented ; null not_documented | Number of virtual CPUs to allocate to this VM. |
| [status](http://mafreebox.freebox.fr/doc/index.html#VM.status) | enum | read_only | not_documented ; null not_documented | VM status status Description stopped VM is stopped running VM is running starting VM is starting up. Transitional state stopping VM is being stopped. Transitional state |
| [enable_screen](http://mafreebox.freebox.fr/doc/index.html#VM.enable_screen) | bool | not_documented | not_documented ; null not_documented | Whether or not this VM should have a virtual screen, to use with the VNC websocket protocol. |
| [bind_usb_ports](http://mafreebox.freebox.fr/doc/index.html#VM.bind_usb_ports) | [] array of enum | not_documented | not_documented ; null not_documented | List of ports that should be bound to this VM. Only one VM can use USB at given time, whether is uses only one or all USB ports. The list of system USB ports is available in VmSystemInfo . For example: “usb-external-type-a”, “usb-external-type-c”. |
| [enable_cloudinit](http://mafreebox.freebox.fr/doc/index.html#VM.enable_cloudinit) | bool | not_documented | not_documented ; null not_documented | Whether or not to enable passing data through cloudinit. This uses the NoCloud iso image method; it will add a virtual cdrom drive (distinct from the one passed by cd_path) with the data in cloudinit_userdata and cloudinit_hostname when enabled. |
| [cloudinit_hostname](http://mafreebox.freebox.fr/doc/index.html#VM.cloudinit_hostname) | string | not_documented | not_documented ; null not_documented | When cloudinit is enabled, hostname desired for this VM. Max 59 characters. |
| [cloudinit_userdata](http://mafreebox.freebox.fr/doc/index.html#VM.cloudinit_userdata) | string | not_documented | not_documented ; null not_documented | When cloudinit is enabled, raw yaml to be passed in the user-data file. Maximum 32767 characters. |
| [mac](http://mafreebox.freebox.fr/doc/index.html#VM.mac) | string | read_only | not_documented ; null not_documented | VM ethernet interface MAC address. |
| [os](http://mafreebox.freebox.fr/doc/index.html#VM.os) | string | not_documented | not_documented ; null not_documented | Type of OS used for this VM. Only used to set an icon for now. Example values: unknown fedora debian ubuntu freebsd opensuse centos jeedom homebridge |

### VmSystemInfo

Source [VmSystemInfo](http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo) ; module vm.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [total_memory](http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.total_memory) | int | read_only | not_documented ; null not_documented | Total memory available to VMs. |
| [used_memory](http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.used_memory) | int | read_only | not_documented ; null not_documented | Currently used memory by all VMs. |
| [total_cpus](http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.total_cpus) | int | read_only | not_documented ; null not_documented | Total number of vCPUs available to VMs. |
| [used_cpus](http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.used_cpus) | int | read_only | not_documented ; null not_documented | Currently used vCPUs by all VMs. |
| [usb_ports](http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.usb_ports) | [] array of string | read_only | not_documented ; null not_documented | List of USB ports available on this system |
| [usb_used](http://mafreebox.freebox.fr/doc/index.html#VmSystemInfo.usb_used) | bool | read_only | not_documented ; null not_documented | Whether a VM is currently using USB. (only one can use USB at a given time) |

### VmDistribution

Source [VmDistribution](http://mafreebox.freebox.fr/doc/index.html#VmDistribution) ; module vm.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [name](http://mafreebox.freebox.fr/doc/index.html#VmDistribution.name) | string | read_only | not_documented ; null not_documented | Name of downloadable distribution image. |
| [url](http://mafreebox.freebox.fr/doc/index.html#VmDistribution.url) | string | read_only | not_documented ; null not_documented | URL of distribution. Usually an arm64 qcow2 cloud image, supporting EFI boot and cloud-init. |
| [hash](http://mafreebox.freebox.fr/doc/index.html#VmDistribution.hash) | string | read_only | not_documented ; null not_documented | Hash in the format sha256:<hash> or sha512:<hash>; or a URL to a SHA256SUMS or SHA512SUMS file (used by Ubuntu, Debian), or to a -CHECKSUM file (used by Fedora). It is designed to be passed as-is to the download add API . |
| [os](http://mafreebox.freebox.fr/doc/index.html#VmDistribution.os) | string | read_only | not_documented ; null not_documented | OS of this distribution image; to be passed as a os type in the VM . |

### VmDiskInfo

Source [VmDiskInfo](http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo) ; module vm.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [type](http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo.type) | enum | read_only | not_documented ; null not_documented | Type of disk, just like in VM.disk_type |
| [actual_size](http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo.actual_size) | int | read_only | not_documented ; null not_documented | Space used by virtual image on disk. This is how much filesystem space is consumed on the box. |
| [virtual_size](http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo.virtual_size) | int | read_only | not_documented ; null not_documented | Size of virtual disk. This is the size the disk will appear inside the VM. |

### VmDiskTask

Source [VmDiskTask](http://mafreebox.freebox.fr/doc/index.html#VmDiskTask) ; module vm.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.id) | int | read_only | not_documented ; null not_documented | Task id. |
| [type](http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.type) | enum | read_only | not_documented ; null not_documented | Type of disk operation: create resize |
| [done](http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.done) | bool | read_only | not_documented ; null not_documented | Is task done |
| [error](http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.error) | bool | read_only | not_documented ; null not_documented | Is task in error |

### VmStateChange

Source [VmStateChange](http://mafreebox.freebox.fr/doc/index.html#VmStateChange) ; module vm.

| Champ | Type exact | Accès | Présence | Contraintes / description |
| --- | --- | --- | --- | --- |
| [id](http://mafreebox.freebox.fr/doc/index.html#VmStateChange.id) | int | read_only | not_documented ; null not_documented | VM id. |
| [status](http://mafreebox.freebox.fr/doc/index.html#VmStateChange.status) | enum | read_only | not_documented ; null not_documented | New VM.status . |

## Énumérations

| Propriété | Valeurs filaires établies | Politique / preuve |
| --- | --- | --- |
| Download.type | bt, nzb, http, ftp | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [Download.type](http://mafreebox.freebox.fr/doc/index.html#Download.type) |
| Download.status | stopped, queued, starting, downloading, stopping, error, done, checking, repairing, extracting, seeding, retry | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [Download.status](http://mafreebox.freebox.fr/doc/index.html#Download.status) |
| Download.io_priority | low, normal, high | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [Download.io_priority](http://mafreebox.freebox.fr/doc/index.html#Download.io_priority) |
| Download.error | none, internal, disk_full, unknown, parse_error, http_301, http_400, http_401, http_402, http_403, http_404, http_405, http_406, http_407, http_408, http_409, http_410, http_411, http_412, http_413, http_414, http_415, http_416, http_417, http_422, http_423, http_424, http_425, http_426, http_427, http_428, http_429, http_430, http_431, http_4xx, http_500, http_501, http_502, http_503, http_504, http_505, http_506, http_507, http_508, http_509, http_510, http_511, http_5xx, http_redirections_exceeded, nzb_no_group, nzb_not_found, nzb_invalid_crc, nzb_invalid_size, nzb_invalid_filename, nzb_open_failed, nzb_write_failed, nzb_missing_size, nzb_decode_error, nzb_missing_segments, nzb_error, unknown_host, timeout, bad_authentication, connection_refused, nzb_authentication_required, bt_tracker_error, bt_missing_files, bt_file_error, missing_ctx_file | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. Référence http://mafreebox.freebox.fr/doc/index.html#download-task-taskfile-errors. ; [Download.error](http://mafreebox.freebox.fr/doc/index.html#Download.error) |
| NzbConfigStatus.status | not_checked, checking, error, ok | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [NzbConfigStatus.status](http://mafreebox.freebox.fr/doc/index.html#NzbConfigStatus.status) |
| NzbConfigStatus.error | none, nzb_authentication_required, bad_authentication, connection_refused | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [NzbConfigStatus.error](http://mafreebox.freebox.fr/doc/index.html#NzbConfigStatus.error) |
| DownloadStats.throttling_mode | normal, slow, hibernate, schedule | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. Référence http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.mode. ; [DownloadStats.throttling_mode](http://mafreebox.freebox.fr/doc/index.html#DownloadStats.throttling_mode) |
| DownloadFile.status | queued, error, done | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DownloadFile.status](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.status) |
| DownloadFile.error | none, internal, disk_full, unknown, parse_error, http_301, http_400, http_401, http_402, http_403, http_404, http_405, http_406, http_407, http_408, http_409, http_410, http_411, http_412, http_413, http_414, http_415, http_416, http_417, http_422, http_423, http_424, http_425, http_426, http_427, http_428, http_429, http_430, http_431, http_4xx, http_500, http_501, http_502, http_503, http_504, http_505, http_506, http_507, http_508, http_509, http_510, http_511, http_5xx, http_redirections_exceeded, nzb_no_group, nzb_not_found, nzb_invalid_crc, nzb_invalid_size, nzb_invalid_filename, nzb_open_failed, nzb_write_failed, nzb_missing_size, nzb_decode_error, nzb_missing_segments, nzb_error, unknown_host, timeout, bad_authentication, connection_refused, nzb_authentication_required, bt_tracker_error, bt_missing_files, bt_file_error, missing_ctx_file | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. Référence http://mafreebox.freebox.fr/doc/index.html#download-task-taskfile-errors. ; [DownloadFile.error](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.error) |
| DownloadFile.priority | no_dl, low, normal, high | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DownloadFile.priority](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.priority) |
| DownloadTracker.status | unannounced, announcing, announce_failed, announced | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DownloadTracker.status](http://mafreebox.freebox.fr/doc/index.html#DownloadTracker.status) |
| DownloadPeer.state | disconnected, connecting, handshaking, ready | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DownloadPeer.state](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.state) |
| DownloadPeer.origin | tracker, incoming, dht, pex, user | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DownloadPeer.origin](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.origin) |
| DownloadPeer.protocol | tcp, tcp_obfuscated, udp | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DownloadPeer.protocol](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.protocol) |
| DownloadBlacklistEntry.reason | not_blacklisted, crypto_not_supported, connect_fail, hs_timeout, hs_failed, hs_crypt_failed, hs_crypto_disabled, torrent_not_found, read_failed, write_failed, crap_received, conn_closed, timeout, blocklist, user | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DownloadBlacklistEntry.reason](http://mafreebox.freebox.fr/doc/index.html#DownloadBlacklistEntry.reason) |
| DownloadFeed.status | ready, fetching, error | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DownloadFeed.status](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.status) |
| DownloadFeed.error | none, internal, disk_full, unknown, parse_error, http_301, http_400, http_401, http_402, http_403, http_404, http_405, http_406, http_407, http_408, http_409, http_410, http_411, http_412, http_413, http_414, http_415, http_416, http_417, http_422, http_423, http_424, http_425, http_426, http_427, http_428, http_429, http_430, http_431, http_4xx, http_500, http_501, http_502, http_503, http_504, http_505, http_506, http_507, http_508, http_509, http_510, http_511, http_5xx, http_redirections_exceeded, nzb_no_group, nzb_not_found, nzb_invalid_crc, nzb_invalid_size, nzb_invalid_filename, nzb_open_failed, nzb_write_failed, nzb_missing_size, nzb_decode_error, nzb_missing_segments, nzb_error, unknown_host, timeout, bad_authentication, connection_refused, nzb_authentication_required, bt_tracker_error, bt_missing_files, bt_file_error, missing_ctx_file | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. Référence http://mafreebox.freebox.fr/doc/index.html#download-task-taskfile-errors. ; [DownloadFeed.error](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.error) |
| DlThrottlingConfig.schedule | normal, slow, hibernate | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DlThrottlingConfig.schedule](http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.schedule) |
| DlThrottlingConfig.mode | normal, slow, hibernate, schedule | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DlThrottlingConfig.mode](http://mafreebox.freebox.fr/doc/index.html#DlThrottlingConfig.mode) |
| DlBtConfig.crypto_support | unsupported, allowed, preferred, required | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DlBtConfig.crypto_support](http://mafreebox.freebox.fr/doc/index.html#DlBtConfig.crypto_support) |
| FsTask.type | cat, cp, mv, rm, archive, extract, repair | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. Hash est établi par exemple, absent du tableau FsTask.type. ; [FsTask.type](http://mafreebox.freebox.fr/doc/index.html#FsTask.type) |
| FsTask.state | queued, running, paused, done, failed | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [FsTask.state](http://mafreebox.freebox.fr/doc/index.html#FsTask.state) |
| FsTask.error | none, archive_read_failed, archive_open_failed, archive_write_failed, chdir_failed, dest_is_not_dir, file_exists, file_not_found, mkdir_failed, open_input_failed, open_output_failed, opendir_failed, overwrite_failed, path_too_big, repair_failed, rmdir_failed, same_file, unlink_failed, unsupported_file_type, write_failed, disk_full, internal, invalid_format, incorrect_password, permission_denied, readlink_failed, symlink_failed, copy_into_itself, truncate_failed | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [FsTask.error](http://mafreebox.freebox.fr/doc/index.html#FsTask.error) |
| FileInfo.type | dir, file | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [FileInfo.type](http://mafreebox.freebox.fr/doc/index.html#FileInfo.type) |
| FileUpload.status | authorized, in_progress, done, failed, conflict, timeout, cancelled | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [FileUpload.status](http://mafreebox.freebox.fr/doc/index.html#FileUpload.status) |
| FileUploadStartAction.force | missing, overwrite, resume | Table value missing describes absence of the parameter; do not emit literal missing without clarification. overwrite/resume explicitly described. ; [FileUploadStartAction.force](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.force) |
| RRDFetch.db | net, temp, dsl, switch | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [RRDFetch.db](http://mafreebox.freebox.fr/doc/index.html#RRDFetch.db) |
| DiskPartition.state | error, checking, formatting, mounting, maintenance, mounted, umounting, umounted, ejecting | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DiskPartition.state](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.state) |
| DiskPartition.fstype | empty, unknown, xfs, ext4, vfat, ntfs, hf, hfsplus, swap, exfat | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DiskPartition.fstype](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.fstype) |
| DiskPartition.fsck_result | no_run_yet, running, fs_clean, fs_corrected, fs_needs_correction, failed | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [DiskPartition.fsck_result](http://mafreebox.freebox.fr/doc/index.html#DiskPartition.fsck_result) |
| StorageDisk.type | internal, usb, sata, nvme | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [StorageDisk.type](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.type) |
| StorageDisk.state | error, disabled, enabled, formatting | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [StorageDisk.state](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.state) |
| StorageDisk.table_type | msdos, gpt, superfloppy, empty | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [StorageDisk.table_type](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.table_type) |
| RaidArray.state | stopped, running, error | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [RaidArray.state](http://mafreebox.freebox.fr/doc/index.html#RaidArray.state) |
| RaidArray.level | basic, raid0, raid1, raid5, raid10 | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [RaidArray.level](http://mafreebox.freebox.fr/doc/index.html#RaidArray.level) |
| RaidArray.sync_action | idle, resync, recover, check, repair, reshape, frozen | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [RaidArray.sync_action](http://mafreebox.freebox.fr/doc/index.html#RaidArray.sync_action) |
| RaidArray.sysfs_state | clear, inactive, suspended, readonly, read_auto, clean, active, write_pending, active_idle | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [RaidArray.sysfs_state](http://mafreebox.freebox.fr/doc/index.html#RaidArray.sysfs_state) |
| RaidMember.role | active, faulty, spare, missing | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [RaidMember.role](http://mafreebox.freebox.fr/doc/index.html#RaidMember.role) |
| RaidMember.device_location | référence / non documenté | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [RaidMember.device_location](http://mafreebox.freebox.fr/doc/index.html#RaidMember.device_location) |
| VM.disk_type | raw, qcow2 | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [VM.disk_type](http://mafreebox.freebox.fr/doc/index.html#VM.disk_type) |
| VM.status | stopped, running, starting, stopping | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [VM.status](http://mafreebox.freebox.fr/doc/index.html#VM.status) |
| VM.bind_usb_ports | référence / non documenté | Use system-provided VmSystemInfo.usb_ports strings; no closed enum. ; [VM.bind_usb_ports](http://mafreebox.freebox.fr/doc/index.html#VM.bind_usb_ports) |
| VmDiskInfo.type | raw, qcow2 | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. Référence http://mafreebox.freebox.fr/doc/index.html#VM.disk_type. ; [VmDiskInfo.type](http://mafreebox.freebox.fr/doc/index.html#VmDiskInfo.type) |
| VmDiskTask.type | create, resize | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. ; [VmDiskTask.type](http://mafreebox.freebox.fr/doc/index.html#VmDiskTask.type) |
| VmStateChange.status | stopped, running, starting, stopping | Préserver une valeur réponse inconnue ; écritures limitées aux valeurs établies. Référence http://mafreebox.freebox.fr/doc/index.html#VM.status. ; [VmStateChange.status](http://mafreebox.freebox.fr/doc/index.html#VmStateChange.status) |

## Dépendances et validation

Protocole (`protocol`) : APIResponse, découverte/session, headers WS/HTTP, RegisterAction. Système (`system-home`) : has_vm. Réseau (`network`) : ConnectionStatus.ipv4_port_range. Fichiers/Stockage owns les modèles définis ici et les transformations Base64 locales ; l’orchestrateur owns le transport partagé, JSON context, solution et intégration événements.

Vérifications réalisées : SHA brut exact, aucune exécution des outils uploadés, parsing HTML propre au dépôt, toutes signatures actives comparées au catalogue, comptes de déclarations alignés. Cas futurs : NFC/NFD/+//= ; listing cursor ; form/newline ; multipart streamed ; owned download ; WS pipeline/finalize/resume/cancel ; états VM/RAID ; grandes tailles ; omission false/0 ; AOT avec metadata générées. Les fixtures réparées sont explicitement dérivées des exemples et ne deviennent pas une preuve officielle.

## Inconnues et décisions à accepter

| Question | État | Impact / décision conservatrice | Source |
| --- | --- | --- | --- |
| GET downloads/ narrative says collection, raw example result is a single Download object. Which current canonical shape is returned? | unknown | Blocks a single fixed typed collection return unless orchestrator accepts an explicitly documented object-or-array compatibility union. | [get--api-v8-downloads-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-) |
| DownloadFeed create/update return feed_id while model and GET return id. Is this an operation-specific model or documentation defect? | unknown | Preserve both proven field names separately; do not merge IDs silently. Conventional single Id accessor remains unresolved. | [DownloadFeed.id](http://mafreebox.freebox.fr/doc/index.html#DownloadFeed.id) ; [post--api-v8-downloads-feeds-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-feeds-) ; [put--api-v8-downloads-feeds-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-feeds-id) |
| StorageDisk.table_type says int, table and official GET samples use strings. | unknown | Numeric declaration contradicts observed string examples; cannot claim an int-only or string-only wire contract without an explicit choice. | [StorageDisk.table_type](http://mafreebox.freebox.fr/doc/index.html#StorageDisk.table_type) ; [get--api-v8-storage-disk-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-disk-id) |
| DownloadPeer.requests says array<int>, samples use {}. Is empty object a special empty array representation? | unknown | No arbitrary dictionary inferred; raw JsonElement/explicit empty-object-or-array union can preserve conflicting evidence pending decision. | [DownloadPeer.requests](http://mafreebox.freebox.fr/doc/index.html#DownloadPeer.requests) ; [get--api-v8-downloads-task_id-peers](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-peers) |
| GET RRD says same as POST, but no GET example defines body vs query placement. | unknown | Typed GET encoder blocked; GET request with guessed query is not documented. | [get--api-v8-rrd-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-rrd-) ; [post--api-v8-rrd-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-rrd-) |
| Tracker declarations use /trackers but POST/PUT example requests use /tracker. | unknown | Retain declared plural path; preserve singular example as a documented conflict, never register both routes based on a guess. | [post--api-v8-downloads-task_id-trackers](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-downloads-task_id-trackers) ; [put--api-v8-downloads-task_id-trackers-announce](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-task_id-trackers-announce) |
| FileUploadStartAction.force table missing: literal enum value or omission? | unknown | Expose overwrite/resume and omission; literal missing not emitted until resolved. | [FileUploadStartAction.force](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.force) |
| Upload conflict prose says destination_conflict; example error_code is conflict. | unknown | Keep error codes as raw strings and recognize both documented spellings; no invented alias guarantee. | [FileUploadStartAction.force](http://mafreebox.freebox.fr/doc/index.html#FileUploadStartAction.force) ; [get--api-v8-ws-upload](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-ws-upload) |
| VM disk create/resize return task id without showing primitive vs {id} JSON result. | not_documented | Endpoint known, strongly typed result wire shape unknown; keep raw envelope result or explicit unresolved marker until evidence. | [post--api-v8-vm-disk-create](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-disk-create) ; [post--api-v8-vm-disk-resize](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-disk-resize) |
| RAID mutation and VM creation/state mutation responses are not shown nor typed in prose. | not_documented | Do not fabricate RaidArray/VM/bool result; generic success-only operation may discard unspecified result under explicit architecture policy. | [post--api-v8-storage-raid-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-storage-raid-) ; [delete--api-v8-storage-raid-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-storage-raid-id) ; [put--api-v8-storage-raid-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-raid-id) ; [post--api-v8-vm-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-) ; [post--api-v8-vm-id-start](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-vm-id-start) |
| FsTask.type table omits hash, official hash response uses hash. | unknown | Retain proven hash as additional documented value and preserve future unknown response values; input is not task-type mutation. | [FsTask.type](http://mafreebox.freebox.fr/doc/index.html#FsTask.type) ; [post--api-v15-fs-hash-](http://mafreebox.freebox.fr/doc/index.html#post--api-v15-fs-hash-) |
| FsTask.progress says percent scaled by 100; done example shows progress=100. | unknown | Preserve raw integer, do not present normalized progress percentage without explicit documented conversion decision. | [FsTask.progress](http://mafreebox.freebox.fr/doc/index.html#FsTask.progress) ; [get--api-v15-fs-tasks-](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-tasks-) |
| DownloadFile.task_id int definition vs quoted numeric string sample; DlNewsConfig.port int vs quoted request sample. | unknown | Numbers-from-strings compatibility may be chosen only on these evidenced fields; do not globally relax all JSON numbers. | [DownloadFile.task_id](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.task_id) ; [get--api-v8-downloads-task_id-files](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-files) ; [DlNewsConfig.port](http://mafreebox.freebox.fr/doc/index.html#DlNewsConfig.port) ; [put--api-v8-downloads-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-config-) |
| Piece status table loses two cells as empty UL markup; example contains + and -, meanings not determinable from table text. | unknown | Use opaque status string; X . / U meanings known, do not assert + or - meanings. | [get-the-pieces-status-a-given-download](http://mafreebox.freebox.fr/doc/index.html#get-the-pieces-status-a-given-download) ; [get--api-v8-downloads-task_id-pieces](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-task_id-pieces) |
| DlBlockListConfig.sources[] signature names array notation as property and declares string; no JSON blocklist example shows exact wire name. | unknown | Request serializer naming sources vs sources[] requires a source-aware decision; no arbitrary dictionary fallback. | [DlBlockListConfig.sources[]](http://mafreebox.freebox.fr/doc/index.html#DlBlockListConfig.sources[]) |
| Create ShareLink model is entirely marked read-only, but request example writes path/expire/fullurl. Is fullurl ignored? | unknown | Define separate creation DTO from proven path/expire sample fields; do not assume fullurl writable. | [ShareLink.path](http://mafreebox.freebox.fr/doc/index.html#ShareLink.path) ; [ShareLink.expire](http://mafreebox.freebox.fr/doc/index.html#ShareLink.expire) ; [ShareLink.fullurl](http://mafreebox.freebox.fr/doc/index.html#ShareLink.fullurl) ; [post--api-v8-share_link-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-share_link-) |
| Default/max FS listing limit, pagination terminal cursor, request nullability and patch-null semantics. | not_documented | Omit unspecified fields; represent absence separately from false/0, never send null by default or claim response nullability. | [get--api-v15-fs-ls-path](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-ls-path) ; [put--api-v8-storage-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-storage-config-) ; [put--api-v8-downloads-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-downloads-config-) |
| Per-operation permissions except RRD, cancellation deadlines, upload max chunk size/window/timeout, and binary download range/restart guarantees. | not_documented | Reuse reviewed common auth; cancellation is client policy, stream/disposal ownership documented by client, no automatic mutation replay or speculative ranges. | [file-upload](http://mafreebox.freebox.fr/doc/index.html#file-upload) ; [file-system-558](http://mafreebox.freebox.fr/doc/index.html#file-system-558) ; [download](http://mafreebox.freebox.fr/doc/index.html#download) ; [raid-api-unstable](http://mafreebox.freebox.fr/doc/index.html#raid-api-unstable) ; [vm-api-unstable](http://mafreebox.freebox.fr/doc/index.html#vm-api-unstable) |
| VM update body writable subset and partial update semantics are not explicitly specified. | not_documented | Facade must preserve model read-only separation and omission; avoid claiming full replacement or null clearing. | [put--api-v8-vm-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-vm-id) ; [VM](http://mafreebox.freebox.fr/doc/index.html#VM) |
| VM page references SystemConfig.has_vm but actual field is SystemModelInfo.has_vm. | unknown | Capability gating should use model_info.has_vm as defined by system page; preserve mismatched cross-reference and consolidate with system-home. | [vm-api-unstable](http://mafreebox.freebox.fr/doc/index.html#vm-api-unstable) ; [SystemModelInfo.has_vm](http://mafreebox.freebox.fr/doc/index.html#SystemModelInfo.has_vm) |
| FS listing request example appends &limit=100 without a question mark. | unknown | Parameter table establishes limit/cursor; URI construction should follow reviewed common query grammar and preserve malformed example as a defect, not reproduce it. | [get--api-v15-fs-ls-path](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-ls-path) |
| Share DELETE prose talks about stopping a task and no rollback, although resource is a link, not FsTask. | unknown | Treat deletion of the sharing token as the documented operation; do not infer file deletion, task stop or filesystem rollback behavior from copied text. | [delete--api-v8-share_link-token](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-share_link-token) |
| Download.list examples contain malformed JSON and FS/RAID/storage samples omit commas or contain ellipses. | not_documented | Do not execute raw samples as fixtures without labeled normalization; their syntax repairs never establish missing semantic rules. | [get--api-v8-downloads-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-downloads-) ; [get--api-v15-fs-tasks-](http://mafreebox.freebox.fr/doc/index.html#get--api-v15-fs-tasks-) ; [get--api-v8-storage-disk-disk_id-fsadvice?partition_id=partition_id&dedicated_disk=bool](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-storage-disk-disk_id-fsadvice?partition_id=partition_id&dedicated_disk=bool) |

Verdict `blocked_evidence` : lecture complète, contradictions signalées et champs absents non inventés. La prochaine étape est l’acceptation technique globale des unions explicites et des résultats non documentés ; aucune autorisation utilisateur supplémentaire n’est requise pour cette revue. Le companion JSON contient toutes les données détaillées pour l’orchestrateur et reste indépendant du catalogue automatique.
