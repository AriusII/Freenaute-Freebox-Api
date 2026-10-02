# Contrats transversaux du SDK Server

La référence est l’instantané embarqué **API 16.0** fourni le 2 octobre 2026. Son HTML
brut porte le SHA-256 `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.
La [revue protocole](freebox-official/reviews/protocol.json) et les
[manifestes de domaines](implementation/) conservent les citations détaillées.
Les règles ci-dessous distinguent protocole documenté et choix du client.

## Version, chemins et dépréciations

Le Server annonce une version `major.minor` et une racine `api_base_url`. Le transport
joint cette racine à `v<major>/` puis au chemin relatif de ressource. Les signatures
v8/v9/v11/v16 restent dans les preuves ; elles n’ajoutent aucune branche de
compatibilité historique. Une origine, un segment de chemin et une query ont leurs
encodages respectifs, sans concaténation d’URI arbitraire.

Les versions internes d’une ressource, notamment Player, sont conservées. TFTP
présente une déclaration `/api/latest/...` et un exemple v16 ; le manifeste du lot
conserve la décision retenue. Aucun `/api/vlatest/` ni remplacement global des nombres
n’est inventé. Les routes hors racine API, telles que les flux caméra, gardent leur
origine et leur chemin documentés.

Seuls les éléments explicitement dépréciés, obsolètes, supprimés ou inutilisables
sont exclus, dans leur portée exacte. Un champ obsolète ne retire pas son modèle
actuel. Une ancienne signature, `UNSTABLE`, ou une suppression future possible ne
suffit pas à exclure une API. La version 16.0 décrit ce corpus, sans preuve de
fraîcheur universelle.

## Origine et TLS

HTTPS est prescrit par la documentation. Le client utilise par défaut
`https://mafreebox.freebox.fr/`, également documenté pour la découverte et l’enrôlement.
L’application peut choisir explicitement une autre origine. La découverte conserve
le modèle brut de la box et ne déplace pas les credentials vers `api_domain`.
Le SDK ne fournit pas d’intégration mDNS ou de résolution de port distante implicite.

Le handler DI emploie les racines françaises Freebox en `CustomRootTrust`, avec
`NoFlag`, EKU serveur TLS et validation standard du nom par `SslStream`. Les racines
sont possédées par le handler ; aucun magasin de confiance global n’est modifié.
La révocation utilise le défaut .NET `NoCheck`, avec choix explicite `Online` ou
`Offline`. Aucun callback permissif ne remplace ces contrôles.

| Racine | SHA-256 du DER |
| --- | --- |
| Freebox ECC Root CA | `23cfb72636be665dbbf2fe3e0527904771a8dff1bace461d8bd69f34812c2444` |
| Freebox Root CA | `2bd8b5be1a990e42ad1bd79c306eb519b637ee2475c0d931f257535610e9c3e7` |

Ces certificats correspondent aux PEM de `#https-access`. Les racines Iliad italiennes
publiées dans cette section ne sont pas incluses par défaut. Un handler fourni par
l’appelant conserve sa propre politique. HTTP reste un choix explicite, notamment
pour une fixture ; aucun repli HTTP ou suivi de redirection n’est ajouté au handler DI.

## Authentification, erreurs et concurrence

L’autorisation initiale est locale et approuvée sur la Freebox. Le challenge sert à
calculer HMAC-SHA1 avec `app_token`, puis un digest hexadécimal minuscule. L’ouverture
transmet `app_id`, `password` et `app_version` si configuré. La documentation ne rend
pas explicitement ce dernier champ obligatoire.

Le token durable appartient au stockage de secrets de l’application. Le token de
session est ajouté à chaque requête dans `X-Fbx-App-Auth`, jamais dans l’URI ni un
`DefaultRequestHeaders` partagé. Les ouvertures concurrentes sont coordonnées par
origine et identité d’application. Une permission absente vaut faux ; `parental`
est obsolète. `camera` reste pertinente pour enregistrements et live stream.
L’absence d’une règle locale ne permet pas d’inventer une permission ou un accès public.

Les réponses JSON utilisent `success`, un éventuel `result`, `error_code` et `msg`.
Le transport contrôle HTTP et l’enveloppe, puis expose `FreeboxApiException` avec les
informations présentes. Les messages humains ne commandent pas le comportement.
Le résultat peut être scalaire, collection, objet ou absent selon l’opération.
Une commande de succès sans résultat ne fabrique pas un modèle de réponse.

La source ne fixe aucun délai numérique universel de session. Une erreur
`auth_required`, `invalid_session` ou HTTP 401 invalide seulement la session concernée
pour l’appel explicite suivant. Aucun appel n’est rejoué automatiquement, y compris
un GET documentaire avec effet de bord. Le délai et l’annulation du client couvrent
la lecture du corps ; ils ne sont pas une garantie de durée du serveur.

## JSON typé et mises à jour

Chaque opération possède ses DTO de lecture et de commande ainsi que ses
`JsonTypeInfo<T>` générés. Les noms JSON exacts sont indépendants des noms C#.
`Optional<T>` conserve absent, valeur et null : les propriétés non définies sont
omises, tandis que false et zéro explicites restent envoyés. Un null de requête
n’est accepté que sur un champ attesté. Une DTO nullable ne promet ni effacement,
ni merge, ni remplacement de ressource.

Les formes contradictoires attestées peuvent être représentées par des unions
bornées conservant le kind JSON. Les identifiants textuels gardent leurs zéros
initiaux ; les clés distinctes ne sont pas fusionnées silencieusement. Un objet
vide ne prouve pas une map arbitraire. Les champs réellement ouverts peuvent utiliser
`JsonElement` ; un type inconnu reste explicitement non exposé. Les unités de
compteurs et timestamps restent propres au champ, sans convertisseur global deviné.

Les contextes et converters fermés sont compatibles avec la réflexion JSON désactivée.
La génération compilée `System.Text.Json` suffit. Le catalogue d’objets ne constitue
pas à lui seul un schéma approuvé pour générer tous les contrats.

## Binaire, formulaires et WebSocket

`EncodedFreeboxPath` préserve Base64 et octets UTF-8. Les formulaires et multipart
utilisent les types HTTP standard ; leur propriété est transférée au transport.
Les téléchargements binaires renvoient un `FreeboxDownload` à disposer, avec flux
`Content`, type et longueur disponibles. Les octets restent bruts ; aucun schéma
JSON absent n’est inventé pour un export.

Les WebSockets partagent l’origine, le handler et la session HTTP. `request_id`
corrèle les contrôles. Une connexion coordonne lecture et écriture, reconstitue les
fragments JSON et borne les messages à 1 000 000 octets. Cette borne du client est
conservatrice par rapport à l’annonce documentaire d’une limite de frame de 1 MB.

Le canal event expose les quatre événements catalogués : `vm_state_changed`,
`vm_disk_task_done`, `lan_host_l3addr_reachable` et `lan_host_l3addr_unreachable`.
Leurs payloads typés viennent des contrats VM/LAN. Les abonnements sont explicites ;
aucune reconnexion ne rejoue silencieusement une action ou un abonnement.

L’upload moderne utilise acquittement initial, envois binaires, progressions reçues
concurremment et confirmation de finalisation. Sa fermeture ou annulation conserve
le fichier partiel ; `upload_cancel` demande explicitement sa suppression. L’upload
HTTP obsolète est exclu. Les protocoles spécialisés QEMU/VNC ne sont pas présentés
comme des messages JSON génériques et restent signalés dans les manifestes.

## Notifications et limites de couverture

La gestion `notif/targets` appartient au client Freebox. Le résultat objet/tableau
contradictoire est conservé par une union explicite. Les tokens `download` et
`downloader` restent distincts, sans renommage silencieux.

La spécification du serveur de notifications décrit l’autre sens d’appel : la
Freebox appelle le serveur consommateur via POST `/register`, DELETE
`/register/{box_id}/{device_id}` et POST `/send`. Ces callbacks sont conservés comme
contrats de réception et hors couverture outbound. `device_type` obsolète en
découverte ne supprime pas son homonyme ios/android/firebase du callback.

Les [manifestes](implementation/) localisent les routes, encodages et types non
établis. La [couverture .NET](freebox-official/coverage-net10/README.md) rapproche les
citations ; les tests et NativeAOT vérifient le code. Aucun de ces contrôles ne
revendique un SDK à 100 % ni une validation métier sur une Freebox réelle.
