# Audit des indicateurs de dépréciation — documentation officielle Freebox Server

Date du scan : 2026-10-02 (Europe/Paris)  
Périmètre : documentation publiée sous `https://dev.freebox.fr/sdk/server.html` et `https://dev.freebox.fr/sdk/os/`, avec les pages intra-site atteignables depuis l’index, leurs sous-pages, les sources Sphinx servies et les ressources JavaScript associées.

## Méthode et couverture

- 34 documents HTML récupérés avec HTTP 200 : `server.html`, l’index FreeboxOS et les 32 pages/API de son arbre de navigation.
- 33 sources Sphinx officielles (`/sdk/os/_sources/*.txt`) récupérées avec HTTP 200 afin de détecter les annotations que le rendu HTML peut perdre.
- JavaScript vérifié : `_static/fbx.js` (aucun marqueur) et `searchindex.js` (index de recherche seulement). Aucune ressource JSON liée ou découverte dans ce périmètre.
- Termes inspectés sans faire de l’étiquette `[UNSTABLE]`, ni du seul numéro de version, un indicateur de dépréciation : `deprecat*`, `obsolete*`, `removed/remove*`, `replacement/replace*`, `supersed*`, `legacy`, `withdrawn`.
- Les mentions de suppression employées pour une action métier (`DELETE`, supprimer un fichier, etc.) ont été écartées : elles ne qualifient pas une API comme dépréciée.

## Indicateurs explicites à exclure du référentiel courant

### `DownloadFile.path`

- **URL / ancre** : <https://dev.freebox.fr/sdk/os/download/#DownloadFile.path>
- **Élément exact** : champ en lecture seule `DownloadFile.path`, retourné par les opérations de lecture des téléchargements, notamment `GET /api/v4/downloads/` et `GET /api/v4/downloads/{id}`.
- **Preuve** : le rendu officiel déclare littéralement `[ DEPRECATED ]` sous ce champ. Le journal 1.1 → 2.0 indique aussi « Deprecate path attribute for DownloadFile », puis ajoute `filepath`, `name` et `mimetype`.
- **Classification** : **déprécié explicitement**.
- **Remplacement** : `DownloadFile.filepath` est le successeur fonctionnel fortement étayé (ajouté dans le même changement et documenté comme « full filepath »), mais la phrase de dépréciation ne formule pas elle-même « use filepath ».
- **Incertitude** : faible sur la dépréciation ; moyenne sur le caractère formellement prescriptif du remplacement.

### Séries `temp1`, `temp2`, `temp3` de la base RRD `temp`

- **URL / ancre** : <https://dev.freebox.fr/sdk/os/rrd/#rrd-temp-db>
- **Élément exact** : valeurs de `fields` de la base `temp`, utilisables avec `POST /api/v4/rrd/` (et `GET /api/v4/rrd/`) : `temp1`, `temp2`, `temp3`.
- **Preuve** : le tableau officiel indique respectivement `[DEPRECATED, use cpum]`, `[DEPRECATED, use cpub]` et `[DEPRECATED, use sw]`. Le journal 1.1 → 2.0 les répertorie également comme entrées dépréciées.
- **Classification** : **dépréciés explicitement**.
- **Remplacements** : `temp1` → `cpum` ; `temp2` → `cpub` ; `temp3` → `sw`.
- **Incertitude** : faible. L’exemple de requête publié emploie encore `temp1`, ce qui montre une incohérence éditoriale et ne doit pas être interprété comme une réhabilitation du champ.

### Ancienne méthode HTTP d’envoi de fichier (API v3)

- **URL / ancre** : <https://dev.freebox.fr/sdk/os/upload/#file-upload> ; changement associé : <https://dev.freebox.fr/sdk/os/api_changes_3_0_to_4_0/#deprecated-api>
- **Élément exact** : « previous http upload method » / ancienne API d’upload v3. Le chemin de cette méthode ancienne n’est pas nommé dans la documentation actuelle.
- **Preuve** : la page Upload dit : « the previous http upload method is now deprecated since api v4 » et impose la nouvelle API WebSocket ; le journal 3.0 → 4.0 précise que « The v3 upload api will be removed in next firmware release ».
- **Classification** : **dépréciée explicitement et annoncée pour suppression**.
- **Remplacement** : WebSocket upload, chemin `/api/v4/ws/upload`, documenté sous <https://dev.freebox.fr/sdk/os/upload/#ws-upload-api>. FTP est le repli documenté si WebSocket n’est pas pris en charge.
- **Incertitude** : le calendrier « next firmware release » est une affirmation historique de la documentation statique, pas une vérification de présence/absence sur un Freebox Server actuel. Le référentiel courant doit néanmoins exclure l’ancienne méthode HTTP v3.

### `ParentalFilter.ip`

- **URL / ancre** : <https://dev.freebox.fr/sdk/os/parental/#ParentalFilter.ip>
- **Élément exact** : champ en lecture seule `ip` de l’objet `ParentalFilter`, exposé par `GET /api/v4/parental/filter/` et `GET /api/v4/parental/filter/{id}` ; l’objet est également manipulé par les opérations POST, PUT et DELETE de `/api/v4/parental/filter/`.
- **Preuve** : la source officielle servie par Sphinx, <https://dev.freebox.fr/sdk/os/_sources/parental.txt>, porte l’annotation `[DEPRECATED]` et explique : « only filled for old rules, you cannot set a rule with an IP ». Le rendu HTML courant conserve l’explication mais perd l’étiquette `[DEPRECATED]`.
- **Classification** : **déprécié explicitement dans la source publiée ; annotation perdue dans le rendu HTML**.
- **Remplacement** : aucun remplacement nommé explicitement. Les champs `macs` et `hosts` figurent dans le même objet, mais ils ne doivent pas être présentés comme un remplacement formel sans preuve additionnelle.
- **Incertitude** : moyenne à cause de la divergence source/rendu. L’interdiction de créer une règle avec une IP et la limitation aux anciennes règles confirment toutefois qu’il s’agit d’un champ de compatibilité à écarter de l’API courante.

## Indicateurs de transition ou faux positifs, non classés comme dépréciations formelles

### Transport HTTP non chiffré

- **URL / ancre** : <https://dev.freebox.fr/sdk/os/#https-access> ; <https://dev.freebox.fr/sdk/os/api_changes_3_0_to_4_0/#secure-access>
- **Élément exact** : accès API en HTTP non chiffré.
- **Preuve** : « all applications MUST now use HTTPS » et « Unsecure access will be removed at some point ».
- **Classification** : **retrait futur annoncé, sans marque officielle `deprecated` ni échéance**.
- **Incertitude** : élevée sur l’état réel de disponibilité de HTTP, car la documentation ne donne pas de date ni de mécanisme de compatibilité. Le référentiel doit présenter HTTPS comme transport requis, sans inventer un endpoint HTTP « déprécié ».

### Champs de compatibilité `Precord.legacy_uri` et `Precord.force_channel_name`

- **URL / ancre** : <https://dev.freebox.fr/sdk/os/pvr/#Precord.legacy_uri> ; <https://dev.freebox.fr/sdk/os/pvr/#Precord.force_channel_name>
- **Éléments exacts** : deux champs réservés aux « legacy apps » ; le texte indique d’utiliser `channel_uuid` lorsqu’il est disponible.
- **Preuve** : « only used for legacy apps. Use channel_uuid instead when available ».
- **Classification** : **orientation de compatibilité, sans étiquette de dépréciation**.
- **Incertitude** : moyenne : `channel_uuid` est recommandé conditionnellement (« when available »). Ces champs ne doivent donc pas être supprimés en prétendant qu’ils sont officiellement dépréciés, mais peuvent être omis d’un guide destiné exclusivement aux nouvelles intégrations.

### Champs Wi-Fi nommés `legacy`

- **URL / ancre** : <https://dev.freebox.fr/sdk/os/wifi/#WifiStationFlags.legacy> ; <https://dev.freebox.fr/sdk/os/wifi/#WifiNeighborCap.legacy>
- **Éléments exacts** : booléens `legacy` décrivant les normes Wi‑Fi 802.11a/802.11b.
- **Preuve** : les descriptions parlent des capacités radio « legacy wifi », sans mot-clé de cycle de vie API.
- **Classification** : **faux positif lexical ; pas une dépréciation**.
- **Incertitude** : faible.

## Aucun indicateur

- Aucun indicateur officiel de dépréciation, d’obsolescence, de remplacement, de supersession ou de retrait n’a été trouvé dans `https://dev.freebox.fr/sdk/server.html`.
- Aucun indicateur sémantique de dépréciation n’a été trouvé dans `_static/fbx.js`.
- Aucun usage sémantique de `obsolete`, `superseded`, `withdrawn` ou `replacement` n’a été trouvé dans les ressources analysées. Les occurrences `legacy` et la racine d’indexation `remov` dans `searchindex.js` sont des termes de recherche/indexation ; elles ne constituent pas une déclaration de cycle de vie API.
