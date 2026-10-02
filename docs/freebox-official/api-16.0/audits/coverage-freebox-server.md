# Audit de couverture — Freebox Server API

**État de l’audit : complet pour le corpus actif v16, avec des compléments identifiés pour l’archive publique v4.** Audit réalisé le 2 octobre 2026, en lecture seule. Les constats portent sur les sources déjà collectées dans `sources/` et sur les URL officielles ou locales explicitement publiées par le SDK.

## Source de vérité et priorité

La page publique `https://dev.freebox.fr/sdk/server.html` est bien le point d’entrée Server, mais elle mène à `https://dev.freebox.fr/sdk/os/`, dont le contenu annonce l’API **4.0** et a été modifié en 2017. Elle ne peut donc pas définir l’API actuelle.

La Freebox interrogée annonce `api_version: "16.0"` et sa documentation locale `http://mafreebox.freebox.fr/doc/index.html` est disponible en HTTP et HTTPS. C’est la source primaire à utiliser pour le référentiel final : document Sphinx monolithique de 1 816 775 octets, build `c1fd8795`, dernière modification HTTP le 17 juillet 2026, ETag `W/"6a5a3379-282b1"`. L’instantané est présent dans [local-doc.html](../sources/seed/local-doc.html).

L’archive publique v4 doit être conservée comme preuve historique et pour les changements anciens. Elle ne doit pas compléter silencieusement les contrats v16 ni définir les endpoints courants.

## Couverture contrôlée

| Élément contrôlé | Résultat | Conséquence |
|---|---:|---|
| Document local v16 | 200, collecté | Corpus fonctionnel principal |
| Identifiants HTML locaux | 2 729 occurrences, 2 711 uniques | Toutes les sections et définitions sont adressables |
| Liens de fragments locaux | 2 634 uniques, 0 cible manquante après décodage HTML | Navigation interne entièrement résolue |
| Unités Sphinx `document-api/*` | 72 | 1 introduction, 27 historiques de migration, 44 unités de référence courante |
| Opérations HTTP locales | 340 occurrences, 335 ancres d’opération uniques | Dédupliquer les quatre identifiants répétés avant de générer l’index |
| Pages HTML publiques v4 attendues | 33 / 33 collectées | Archive HTML publique complète |
| Ressources collectées indiquées par le manifeste | 47 succès, 3 réponses 404 attendues | Les 404 correspondent à des éléments d’interface locale absents |

Les 335 ancres d’opération uniques se répartissent ainsi : 168 `GET`, 66 `POST`, 68 `PUT` et 33 `DELETE`. Elles emploient les chemins publiés sous `v8`, `v9`, `v10`, `v11`, `v13`, `v14`, `v15` et `v16`. Ces versions dans les chemins font partie du contrat et doivent être reproduites telles quelles ; ne pas les réécrire toutes vers `/api/v16/`.

Les identifiants d’opération répétées sont `get--api-v8-system-`, `post--api-v8-downloads-add`, `post--api-v8-home-pairing-adapter_id` et `put--api-v9-wifi-config-`. Le dernier identifiant est présent deux fois et celui d’appairage Home trois fois. La génération doit produire une seule entrée par ancre exacte, en fusionnant les renvois documentaires.

## Frontière Freebox Server / Player

Le périmètre final est le Gateway Freebox Server : découverte, authentification, HTTP REST, WebSocket, configuration, réseau, stockage, téléchargements, domotique et les autres modules présents sous `document-api/*` dans le document local.

Le module local `document-api/player` reste **inclus**, car il décrit une API exposée par le Gateway Server pour piloter le Player. Il est marqué `[UNSTABLE]` et doit porter ce statut dans le Markdown final. En revanche, les liens publics `player.html`, `libfbxqml/` et `telec.html` sont hors périmètre : ils concernent respectivement le SDK QML du boîtier Player et le protocole de télécommande réseau, pas l’API Gateway Server. Les actualités, GitHub, magasins d’applications et les liens d’exemples externes ne sont pas des sources de contrat API.

Les 44 unités de référence active attendues sont :

`login`, `websocket`, `airmedia`, `call`, `contacts`, `connection`, `lan`, `freeplug`, `dhcp`, `dhcpv6`, `ftp`, `tftp`, `nat`, `igd`, `lcd`, `ledstrip`, `network_share`, `upnpav`, `switch`, `wifi`, `system`, `vpn`, `vpn_client`, `slowness`, `download`, `download_feeds`, `download_config`, `fs`, `share`, `upload`, `home`, `camera`, `lang`, `notif`, `profile`, `player`, `pvr`, `rrd`, `standby`, `storage`, `raid`, `sfp`, `update`, `vm`.

## Filtrage exact des éléments dépréciés

Le statut `[UNSTABLE]` n’est pas une dépréciation. Les 21 sections `[UNSTABLE]` doivent rester dans la documentation, avec un avertissement de stabilité. Cela inclut notamment Player, VPN Server et Client, PVR, RRD, Storage, RAID, VM, les diagnostics de connexion et certaines API de téléchargement.

À écarter de la référence courante, tout en les conservant dans une annexe de migration si nécessaire :

- les sections d’historique `deprecated-api-v4` et `deprecated-api-v5-0` : ancien upload HTTP v3, remplacé par l’upload WebSocket ; l’unité active `upload` reste incluse ;
- `deprecated-api-v8-0` : contrôle parental remplacé par `profile` ; aucune unité locale `parental` active n’est publiée ;
- `deprecated-api-v10-0` : ancien contrat Connection pour l’agrégation xDSL/4G ; conserver les endpoints de statut séparés actuellement documentés, ne pas réintroduire le contrat historique ;
- `system-config-v5-deprecated` et l’objet `SystemConfigV5` avec toutes ses propriétés ;
- le champ de découverte `device_type`, explicitement remplacé par `box_model` ;
- `WifiBssStatus.is_main_bss` et `WifiBssConfig.use_default_config`, tous deux remplacés par `use_shared_params` ;
- `DownloadFile.path`, remplacé par `filepath` ;
- les métriques RRD `temp1`, `temp2` et `temp3`, remplacées par `cpum`, `cpub` et `sw`.

`expected_phys` apparaît dans l’historique comme ancien champ de configuration. La propriété courante `WifiGlobalState.expected_phys` est présente dans le contrat actif : elle doit être conservée sous ce nom et ne doit pas être confondue avec le champ historique déprécié.

## Ressources supplémentaires à récupérer pour l’intégralité de l’archive publique

Le manifeste contient déjà les 33 pages HTML attendues de `https://dev.freebox.fr/sdk/os/`. Il ne contient toutefois pas les sources RST publiques ni les index techniques, tous accessibles et utiles pour une archive exhaustive et traçable :

- 33 fichiers `https://dev.freebox.fr/sdk/os/_sources/{nom}.txt`, pour chaque nom exposé par `searchindex.js` : `airmedia`, `api_changes_1_1_to_2_0`, `api_changes_2_0_to_3_0`, `api_changes_3_0_to_4_0`, `call`, `connection`, `contacts`, `dhcp`, `download`, `download_config`, `download_feeds`, `freeplug`, `fs`, `ftp`, `igd`, `index`, `lan`, `lcd`, `login`, `nat`, `network_share`, `parental`, `pvr`, `rrd`, `share`, `storage`, `switch`, `system`, `upload`, `upnpav`, `vpn`, `vpn_client`, `wifi` ;
- `https://dev.freebox.fr/sdk/os/objects.inv` ;
- `https://dev.freebox.fr/sdk/os/searchindex.js` ;
- `https://dev.freebox.fr/sdk/os/genindex/` et `https://dev.freebox.fr/sdk/os/search/`.

Ces éléments sont nécessaires pour rendre l’archive publique v4 intégrale ; ils ne changent pas le corpus courant v16. Les index Sphinx analogues sont volontairement absents de la Freebox locale : `/doc/` répond 403, et `objects.inv`, `searchindex.js`, `search.html`, `genindex.html` et les chemins `_sources` usuels répondent 404. Il n’existe donc aucun sous-lien local supplémentaire à crawler au-delà de `index.html` et de ses ressources statiques.

Les ressources locales de rendu `main.css`, `pygments.css`, `documentation_options.js` et `fbx.js` répondent 200 et figurent déjà dans le manifeste. `favicon.ico`, `genindex.html` et `search.html` répondent 404 sur l’instance ; ces trois résultats doivent rester enregistrés comme liens d’interface absents, et non être traités comme une omission de contrat.

## Critères d’exhaustivité pour la livraison Markdown

Le dossier final est exhaustif lorsque :

1. le snapshot local v16 et ses métadonnées d’intégrité sont conservés ;
2. les 44 unités de référence active ont chacune une page ou une section Markdown, et les 27 unités de migration sont isolées du référentiel courant ;
3. les 335 opérations uniques sont indexées avec méthode, chemin exact, version de chemin, ancre source et statut `stable` ou `unstable` ;
4. chaque définition, paramètre, objet, erreur, exemple et lien interne est rattaché à son ancre source ;
5. les entrées listées dans la section de filtrage ne figurent pas dans les contrats actifs ;
6. les liens internes générés résolvent les 2 634 références de fragments ;
7. l’archive publique ajoute les 33 sources RST et les quatre index identifiés ci-dessus, ou documente explicitement qu’elle est limitée aux pages HTML déjà collectées.

Les constats spécialisés sur les ressources Sphinx invisibles sont détaillés dans [hidden-sources-local-v16.md](hidden-sources-local-v16.md).
