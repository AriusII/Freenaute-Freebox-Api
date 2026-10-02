# Éléments exclus et remplacements

Cette liste explique les exclusions de la référence actuelle. Les anciens contrats restent consultables dans les sources brutes pour audit. Les marqueurs `UNSTABLE` sont conservés ; ils indiquent une stabilité limitée, pas une dépréciation.

<a id="exclusion-1"></a>

## device_type

**Module :** `00_index` · **Portée :** `table_row`

Le tableau des clés du TXT mDNS indique explicitement « (DEPRECATED: use box_model) ».

**Remplacement :** box_model

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#discovery-using-mdns)

<a id="exclusion-2"></a>

## DownloadFile.path

**Module :** `download` · **Portée :** `property`

La propriété est marquée « [ DEPRECATED ] » dans la référence courante; le changelog 1.1 vers 2.0 dit aussi de déprécier path pour DownloadFile.

**Remplacement :** DownloadFile.filepath

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#DownloadFile.path)

<a id="exclusion-3"></a>

## temp1

**Module :** `rrd` · **Portée :** `table_row`

La ligne de la table temp indique « [DEPRECATED, use cpum] ».

**Remplacement :** cpum

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#rrd-temp-db)

<a id="exclusion-4"></a>

## temp2

**Module :** `rrd` · **Portée :** `table_row`

La ligne de la table temp indique « [DEPRECATED, use cpub] ».

**Remplacement :** cpub

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#rrd-temp-db)

<a id="exclusion-5"></a>

## temp3

**Module :** `rrd` · **Portée :** `table_row`

La ligne de la table temp indique « [DEPRECATED, use sw] ».

**Remplacement :** sw

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#rrd-temp-db)

<a id="exclusion-6"></a>

## WifiBssStatus.is_main_bss

**Module :** `wifi` · **Portée :** `property`

La propriété porte directement le label Deprecated et sa description indique qu'elle est remplacée par use_shared_params de WifiBss.

**Remplacement :** WifiBss.use_shared_params

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#WifiBssStatus.is_main_bss)

<a id="exclusion-7"></a>

## WifiBssConfig.use_default_config

**Module :** `wifi` · **Portée :** `property`

La propriété porte directement le label Deprecated et sa description indique qu'elle est remplacée par use_shared_params de WifiBss.

**Remplacement :** WifiBss.use_shared_params

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#WifiBssConfig.use_default_config)

<a id="exclusion-8"></a>

## system-config-v5-deprecated

**Module :** `system` · **Portée :** `section`

La section porte le titre exact « System Config V5 (DEPRECATED) ». Elle représente un schéma de réponse historique, non l'opération système actuelle.

**Remplacement :** SystemConfig

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#system-config-v5-deprecated)

<a id="exclusion-9"></a>

## old-version-api-v5

**Module :** `system` · **Portée :** `section`

La section est explicitement intitulée « Old version (api < v5) » et ne documente que le schéma SystemConfigV5 déjà marqué DEPRECATED.

**Remplacement :** Section #current-version-api-v6 et schéma SystemConfig

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#old-version-api-v5)

<a id="exclusion-10"></a>

## file-upload

**Module :** `upload` · **Portée :** `procedure`

La section File Upload dit explicitement que la méthode HTTP précédente est dépréciée depuis l'API v4 et prescrit l'API WebSocket. Le changelog v4 confirme la dépréciation de l'ancienne API d'upload.

**Remplacement :** WebSocket upload via /api/v8/ws/upload; FTP si WebSocket ne peut pas être pris en charge.

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#file-upload)

<a id="exclusion-11"></a>

## deprecated-api-v8-0

**Module :** `parental_legacy` · **Portée :** `historical_api_family`

Le changelog v8 affirme que l'API de contrôle parental n'est plus utilisable et qu'elle est remplacée par l'API Profile. La table de permissions marque également parental comme obsolete.

**Remplacement :** Profile API (/api/v8/profile)

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#deprecated-api-v8-0)

<a id="exclusion-12"></a>

## parental

**Module :** `login` · **Portée :** `table_row`

La ligne de la table des permissions d'application dit « Access to parental control (obsolete) ».

**Remplacement :** profile permission / Profile API, selon l'accès souhaité

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#opening-a-session)

<a id="exclusion-13"></a>

## deprecated-api-v10-0

**Module :** `connection` · **Portée :** `historical_api_variant`

Le changelog v10 dit que cette variante de l'API Connection n'est plus utilisable et qu'elle est remplacée par des endpoints distincts pour LTE et l'agrégation.

**Remplacement :** GET /api/v11/connection/lte/{id}; GET et PUT /api/v11/connection/aggregation

[Preuve officielle](http://mafreebox.freebox.fr/doc/index.html#deprecated-api-v10-0)

<a id="exclusion-14"></a>

## ParentalFilter.ip

**Module :** `parental_legacy_archive` · **Portée :** `property`

La source Sphinx officielle publique parental.txt porte [DEPRECATED] et précise que le champ n'est rempli que pour les anciennes règles, sans possibilité de créer une règle par IP. Cette API est ensuite déclarée non utilisable par le changelog v8 de la référence embarquée.

**Remplacement :** Aucun remplacement explicitement documenté.

[Preuve officielle](https://dev.freebox.fr/sdk/os/parental/#ParentalFilter.ip)
