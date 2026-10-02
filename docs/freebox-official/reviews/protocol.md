# Revue du protocole Freebox Server 16.0

Périmètre relu : `00_index`, `login`, `websocket`, `notif`, `camera`, leurs références
communes et les notes de version de l'introduction. Le [companion JSON](protocol.json)
contient 13 opérations du client Freebox, 3 contrats de callback externes classifiés à part,
14 objets déclarés, 57 propriétés et leurs ancres vérifiées dans le HTML brut.

La source est le corpus fourni par l'utilisateur, pas une nouvelle acquisition réseau dans
cet environnement. Les octets de
`/workspace/.cloud-setup/freebox-upload-20261002/Freebox-Server-API-16.0/sources/raw/embedded/doc/index.html`
ont été vérifiés : SHA-256
`cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.
Les citations ci-dessous désignent les ancres de
<http://mafreebox.freebox.fr/doc/index.html> dans cet instantané. Le manifeste fourni rapporte
une capture GET du 2 octobre 2026 ; aucun endpoint métier ni WebSocket n'a été exécuté ici.

## Version et résolution des chemins

`#api-version` annonce exactement « Current API version is “16.0” » et major 16.
`#building-the-api-request-url` impose la forme `api_base_url/v[major_api_version]/[api_url]`
et donne un exemple `https://example.fbxos.fr:3615/api/v16/login/`. Cela justifie une racine
versionnée à partir de la découverte pour les ressources ordinaires ; les signatures v8,
v9 et v11 présentes dans cette même source doivent rester enregistrées comme preuves.

Le numéro d'une signature ne suffit pas à déclarer l'opération dépréciée, ni à prétendre
qu'une requête réelle sous v16 a été validée. Les modèles actuels doivent également tenir
compte des changements versionnés : l'introduction indique notamment une nouvelle forme
de réponse paginée des fichiers en v15 et de nouvelles valeurs Wi-Fi en v14.

Une vérification transversale a révélé une exception concrète :
`#put--api-latest-tftp-config-` déclare `PUT /api/latest/tftp/config/`, mais son propre exemple
appelle `/api/v16/tftp/config/`. Les deux formes sont attestées. Aucun `/api/vlatest/` n'a
été trouvé. Préserver l'alias et la signature ; choisir la forme explicitement documentée
dans une politique de route, sans remplacement de texte global. Les éventuelles versions
internes d'une ressource Player constituent une autre dimension et doivent être conservées.

## Découverte et HTTPS

`#discovery-using-mdns` privilégie `_fbx-api._tcp`. `#discovery-using-https` décrit le repli
`GET /api_version` et exige la validation du certificat. `#https-access` précise :
« all applications MUST now use HTTPS to access the api ». Deux racines françaises sont
publiées dans cette section : Freebox ECC Root CA et Freebox Root CA ; des racines Iliad
sont également publiées pour la variante italienne.

Le SDK doit proposer une validation de chaîne et de nom d'hôte correcte, scoped au transport
Freebox. Une configuration de confiance custom des racines source vérifiées peut éviter une
installation dans le trust store global. Un callback acceptant tous les certificats, une
validation de chaîne sans contrôle hostname, ou un fallback HTTP silencieux sont incompatibles
avec ce contrat. Les fixtures locales peuvent disposer d'un mode HTTP explicitement choisi.

`#discovery-using-http` prévient que les réponses distantes ne contiennent que les champs
nécessaires à construire l'URL. UID, nom et modèle ne peuvent pas être exigés dans tous les
parcours. La capture de découverte fournie rapporte aussi `box_model: "fbxgw9-r1"`, tandis
que le tableau de référence contient `fbxgw9-r1/full`. Une chaîne brute avec classification
connue facultative évite de casser la découverte d'un modèle absent de l'enum.

`#remote-connection-port-change-discovery` prescrit `_https._tcp.<api_domain>` comme repli
en cas de changement du port distant ; seul le champ port du SRV est pertinent. Ce mécanisme
ne permet pas de déplacer silencieusement une session vers un autre hostname. Aucun secret
n'est envoyé sur l'origine de documentation ou sur un `api_url` externe des notifications.

## Enveloppes et authentification

`#APIResponse` définit `success`, `result`, `error_code` et `msg`. `result` peut être absent
pour une commande sans résultat. `error_code` est spécifique à la ressource ; `msg` est un
message humain français. Vérifier statut HTTP et `success`, conserver les diagnostics structurés,
et traiter séparément les transports non JSON annoncés par leurs opérations.

Les cinq signatures du module login sont :

| Méthode | Signature source | Contrat |
| --- | --- | --- |
| POST | `/api/v8/login/authorize/` | TokenRequest, puis app_token et track_id |
| GET | `/api/v8/login/authorize/{track_id}` | status et challenge |
| GET | `/api/v8/login/` | logged_in et challenge |
| POST | `/api/v8/login/session/` | SessionStart, puis session_token, challenge et permissions |
| POST | `/api/v8/login/logout/` | Ferme la session ; résultat absent |

L'association nécessite le réseau local, HTTPS et l'approbation sur la façade physique.
`#track-authorization-progress` exige le suivi jusqu'à un statut différent de `pending`,
même si l'utilisateur a déjà accordé l'autorisation. Les statuts sont `unknown`, `pending`,
`timeout`, `granted`, `denied` ; seul `granted` permet de continuer.

`#SessionStart.password` prescrit `hmac-sha1(app_token, challenge)`. L'exemple présente un
digest hexadécimal minuscule. `#SessionStart.app_version` déclare aussi `app_version`, absent
du DTO actuel et de l'exemple d'ouverture de session : supporter ce champ, mais conserver la
requiredness comme non documentée. Le challenge expire fréquemment ; aucune durée numérique
de validité de session n'est fournie. Renouveler une session après invalidation pour la prochaine
opération explicite, sans rejouer une mutation déjà tentée.

`#opening-a-session` indique qu'une permission absente vaut false. Le tableau actuel contient
`settings`, `contacts`, `calls`, `explorer`, `downloader`, `pvr`, `profile`. `parental` est
explicitement obsolete et exclu. La liste n'est pas exhaustive : `#changed-api-v8-5` documente
aussi `camera`, encore nécessaire aux enregistrements et streams live, mais pas à la liste.

`settings` autorise les modifications ; une lecture de réglages n'exige pas nécessairement
cette permission. Certaines données sensibles Wi-Fi/VPN restent occultées ou refusées.
Ne pas déduire une permission endpoint par endpoint uniquement de son nom ou du verbe GET.
Le contrat transversal est authentifié sauf indication contraire (`#authentication`).

`#authentication-errors` donne HTTP 403 et les codes `auth_required`, `invalid_token`,
`pending_token`, `insufficient_rights`, `denied_from_external_ip`, `invalid_request`,
`ratelimited`, `new_apps_denied`, `apps_denied`, `internal_error`. Ils restent des chaînes
filaires dans les erreurs, même si des constantes typées offrent un usage pratique.

## WebSocket

`#ws-api` exige `X-Fbx-App-Auth` lors du handshake. Les messages sont habituellement UTF-8
JSON ; la taille maximale de frame annoncée est « 1 MB ». La source ne distingue pas explicitement
MB décimal et MiB : choisir une limite conservatrice documentée et reconstruire les messages
fragmentés sans dépasser les limites de mémoire du client.

Le champ de corrélation est **`request_id`**, integer optionnel (`#WebSocketRequest.request_id`),
pas `req_id` actuellement présent dans les DTO. La réponse le renvoie conditionnellement.
`result` peut être absent ; les erreurs portent `error_code`/`msg`. Les notifications ont
`action: "notification"`, `success: true`, `source`, `event` et un éventuel `result`.

`#get--api-v8-ws-event` déclare le canal `/api/v8/ws/event`, JSON texte un objet par ligne.
`#RegisterAction` exige `action: "register"` et `events` :

| Événement | Type de résultat |
| --- | --- |
| `vm_state_changed` | VmStateChange |
| `vm_disk_task_done` | VmDiskTask |
| `lan_host_l3addr_reachable` | LanHost |
| `lan_host_l3addr_unreachable` | LanHost |

Les noms sont divisés en préfixe `source` et suffixe `event`, par exemple `vm` /
`disk_task_done`. Une boucle de réception propriétaire de la connexion peut router les
réponses corrélées et les notifications, avec envois sérialisés, annulation et disposal.
Les métadonnées de payloads VM/LAN appartiennent à leurs reviewers ; le résultat réellement
ouvert peut rester un `JsonElement` conservé correctement. Aucun event supplémentaire,
intervalle de reconnexion ou garantie d'ordre n'est déduit du corpus.

## Notifications et caméras

Cinq opérations Freebox gèrent les targets sous `/api/v11/notif/targets` : liste, détail,
suppression, mise à jour et création. Les deux mutations POST/PUT montrent notamment un champ
`token` dans leur body alors qu'il est absent du modèle de lecture NotificationTarget.
Séparer lecture et requête d'écriture, sans journaliser ce secret.

Deux incohérences sont enregistrées plutôt que corrigées silencieusement : le détail annonce
un objet dans la prose mais montre une liste dans `result` ; les subscriptions utilisent `download`
dans la table et `downloader` dans des exemples. Une lecture explicite AOT peut accepter objet
ou liste singleton pour le détail et conserver les valeurs de subscription brutes. Les données
restent liées à leurs deux preuves, et les tests montrent les deux formes.

`#notification-server-specification` est explicite : « Your server must implement this API
contract ». `POST /register`, `DELETE /register/{box_id}/{device_id}` et `POST /send` sont
appelés **par la Freebox vers le serveur de notification configuré**. Ces trois opérations
ne sont pas des routes outbound de la façade Freebox et ne reçoivent pas son session_token.
Les types de payloads reverse sont toutefois décrits et peuvent être exposés comme contrats
de réception si le SDK couvre ce besoin. Ici `device_type` est le fournisseur push ; la
dépréciation homonyme de la découverte ne s'y applique pas.

Le module Camera contient exactement deux GET, `/api/v8/camera/` et `/api/v8/camera/{id}`,
et le modèle Camera : `id`, `node_id`, `name`, `stream_url`, `lan_gid`. Les exemples de
`stream_url` sont `/camera/stream/<id>/stream.m3u8`, hors racine API versionnée. Ils ne
définissent pas un endpoint JSON supplémentaire. La suppression passe par Home Node et
`node_id` ; aucun DELETE Camera n'est inventé. L'accès stream/records est distinct de la
lecture de liste et reste soumis à la permission camera documentée.

## Dépendances et validations

Les références VmStateChange, VmDiskTask, LanHost, CallEntry et Home Node doivent être
résolues vers les lots propriétaires par l'orchestrateur. Les 57 propriétés de ces cinq modules
ont été confrontées à leurs ancres brutes ; presence et `null` ne sont pas supposés équivalents.
Les détails non précisés restent `not_documented`.

Les corrections protocolaires prioritaires sont `request_id`, `app_version`, HTTPS avec
confiance correcte, découverte partielle et permissions false par défaut. Les tests doivent
ensuite couvrir URI/origine, chaîne/hostname, HMAC, association suivie, ouverture concurrente
et invalidation de session, frames WS, corrélation, annulation, I/O/disposal et contradictions
de réponse Notification. Une publication et une exécution Native AOT vérifient les parcours
ajoutés ; les tests HTTP locaux ne sont pas présentés comme tests d'une Freebox physique.
