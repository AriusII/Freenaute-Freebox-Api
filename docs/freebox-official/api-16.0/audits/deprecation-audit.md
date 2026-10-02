# Audit de dépréciation — Freebox Server API

Ce rapport fournit les règles de filtrage à appliquer au corpus Freebox Server. La
référence primaire est la documentation embarquée extraite le 2 octobre 2026 :
[`sources/seed/local-doc.html`](../sources/seed/local-doc.html). Elle annonce
l'API **16.0** et sa réponse HTTP indique un contenu modifié le 17 juillet 2026.

Le fichier machine-readable à consommer est
[`deprecation-exclusions.json`](deprecation-exclusions.json). Il met les
règles au niveau précis d'un champ, d'une ligne de tableau, d'un schéma ou
d'une variante historique. Il ne supprime jamais un module entier sur une
simple correspondance textuelle.

## Règles directement applicables au document API 16

| Élément à masquer dans une référence « API actuelle » | Cible exacte | Remplacement documenté | Source courante |
| --- | --- | --- | --- |
| Clé de découverte | `device_type` dans le TXT mDNS / la réponse de découverte | `box_model` | [`#discovery-using-mdns`](http://mafreebox.freebox.fr/doc/index.html#discovery-using-mdns) |
| Fichier de téléchargement | `DownloadFile.path` | `DownloadFile.filepath` (successeur fonctionnel très probable) | [`#DownloadFile.path`](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.path) |
| Séries RRD `temp` | `temp1`, `temp2`, `temp3` | `cpum`, `cpub`, `sw` | [`#rrd-temp-db`](http://mafreebox.freebox.fr/doc/index.html#rrd-temp-db) |
| Wi-Fi BSS | `WifiBssStatus.is_main_bss` | `WifiBss.use_shared_params` | [`#WifiBssStatus.is_main_bss`](http://mafreebox.freebox.fr/doc/index.html#WifiBssStatus.is_main_bss) |
| Wi-Fi BSS | `WifiBssConfig.use_default_config` | `WifiBss.use_shared_params` | [`#WifiBssConfig.use_default_config`](http://mafreebox.freebox.fr/doc/index.html#WifiBssConfig.use_default_config) |
| Schéma système ancien | section `SystemConfigV5` et l'exemple `old-version-api-v5` | `SystemConfig` et `current-version-api-v6` | [`#system-config-v5-deprecated`](http://mafreebox.freebox.fr/doc/index.html#system-config-v5-deprecated) |
| Permission de session | ligne `parental` | Profile API / permission `profile` | [`#opening-a-session`](http://mafreebox.freebox.fr/doc/index.html#opening-a-session) |

## Transitions historiques sans cible à filtrer dans le document API 16

| Élément | État et remplacement | Règle du générateur |
| --- | --- | --- |
| Ancienne méthode HTTP d'upload / API v3 | Dépréciée au profit de WebSocket `/api/v8/ws/upload`, avec FTP en repli | `apply: false` : la procédure v3 n'a plus d'opération distincte dans le document API 16. Ne pas supprimer les opérations WebSocket ni le suivi `/api/v8/upload/`. |
| Ancienne famille Parental control | Non utilisable depuis v8, remplacée par Profile | `apply: false` : aucune section Parental legacy n'est présente dans le document API 16. La section `parental-filter` actuelle contient Profile et doit rester. |
| Ancienne variante Connection agrégation/LTE combinée | Non utilisable depuis v10, remplacée par les endpoints LTE et agrégation séparés | `apply: false` : la source ne fournit ni route ni id de la variante retirée. Ne pas supprimer Connection ni les endpoints actuels. |
| `ParentalFilter.ip` de l'archive publique v4 | Marqué déprécié dans la source Sphinx publique | `apply: false` : absent du document API 16; activer seulement si l'archive v4 est ingérée. |

## Limites et règles importantes

Les étiquettes **UNSTABLE** n'indiquent pas une dépréciation. Une opération en
`/api/v8`, `/api/v9`, `/api/v10` ou `/api/v11` n'est pas obsolète du seul fait
que l'API courante est 16.0 : ces versions sont celles des contrats de route
encore documentés. Le filtre conserve donc les opérations parentes RRD,
Wi-Fi, System, Connection et Upload lorsque seule une propriété ou procédure
historique est concernée.

Le changelog v10.2 déprécie `expected_phys` dans une ancienne API de
**configuration** Wi-Fi globale. Il ne vise pas
`WifiGlobalState.expected_phys` : cette propriété, sous
[`#WifiGlobalState.expected_phys`](http://mafreebox.freebox.fr/doc/index.html#WifiGlobalState.expected_phys),
appartient à l'API d'**état** ajoutée au même changement, est encore décrite
comme `Read-only` et n'a pas de label `Deprecated`. Elle doit rester dans la
référence. Une règle reposant seulement sur le nom `expected_phys` créerait
une suppression erronée.

La documentation marque l'API Wi-Fi Planning comme supplantée par Standby et
« possiblement » supprimée à l'avenir. Elle ne la marque ni dépréciée ni
supprimée. Elle reste donc dans le corpus exhaustif, avec une recommandation
en faveur de Standby. Les champs PVR `legacy_uri` et `force_channel_name` ont
le même traitement : ils visent la compatibilité et conseillent
`channel_uuid`, sans déclaration formelle de dépréciation.

Les exemples JSON doivent être filtrés avec les schémas. Le document source
conserve encore `DownloadFile.path`, `temp1`, `use_default_config` et
`is_main_bss` dans des exemples. Les ancres et les
transformations précises sont consignées dans la clé `examples_to_sanitize`
du JSON.

## Références secondaires contrôlées

L'archive publique officielle `https://dev.freebox.fr/sdk/os/` est utile pour
ses sources Sphinx et les anciens changelogs, mais elle décrit une API v4 et
est historiquement figée. Les constats provenant uniquement de cette archive
sont clairement marqués `archive_only` dans le JSON. Les relevés détaillés
sont conservés dans [changelog-deprecation-findings.md](changelog-deprecation-findings.md)
et [reference-deprecation-findings.md](reference-deprecation-findings.md).
