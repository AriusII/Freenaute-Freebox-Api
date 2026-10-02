# Audit des dépréciations — changelogs Freebox Server SDK

_Relevé effectué le 2 octobre 2026. Périmètre : documentation SDK publique
FreeboxOS sous `dev.freebox.fr/sdk/os/`, ses trois pages officielles « API
changes », et le billet officiel d’annonce du SDK Server. Les termes recherchés
étaient `deprecated` / `deprecate`, `obsolete`, `removed`, `replaced` et
`retired`._

## Règle de classement

| Classement | Sens retenu dans cet audit |
| --- | --- |
| **explicitement deprecated** | La documentation qualifie explicitement l’élément de déprécié. Il doit être exclu de la documentation de l’API courante. |
| **removed** | La documentation affirme que le comportement ou le prérequis n’est plus nécessaire / a été retiré. |
| **replacement only** | Une alternative est proposée sans déclaration de dépréciation. |
| **incertain** | Une suppression est annoncée pour le futur, sans preuve dans la source qu’elle a effectivement eu lieu. L’élément ne doit pas être exclu sur cette seule base. |

`[UNSTABLE]` n’est pas une dépréciation : aucune API n’a été placée dans cet
audit uniquement pour ce motif.

## Éléments à exclure de l’API courante

| Élément concerné | Preuve officielle et version | Remplacement / action | Classement |
| --- | --- | --- | --- |
| `DownloadFile.path` (lecture seule) | Le changelog [API 1.1 → 2.0, « Download API Changes »](https://dev.freebox.fr/sdk/os/api_changes_1_1_to_2_0/#download-api-changes) dit explicitement de déprécier l’attribut `path` de `DownloadFile`. La [page courante des fichiers de téléchargement](https://dev.freebox.fr/sdk/os/download/#download-files) l’affiche encore `[DEPRECATED]`. Version : **1.1 → 2.0**. | Ne pas exposer `path` dans la référence actuelle. Le même changelog introduit `filepath`, `name` et `mimetype`; `filepath` est l’équivalent fonctionnel apparent, mais la source ne le désigne pas formellement comme remplacement obligatoire. | **explicitement deprecated** |
| `temp1` (base RRD `temp`) | Le changelog [API 1.1 → 2.0, « RRD API Changes »](https://dev.freebox.fr/sdk/os/api_changes_1_1_to_2_0/#rrd-api-changes) déprécie `temp1`; la [référence RRD](https://dev.freebox.fr/sdk/os/rrd/#rrd-unstable) précise `[DEPRECATED, use cpum]`. Version : **1.1 → 2.0**. | Utiliser `cpum`. | **explicitement deprecated** |
| `temp2` (base RRD `temp`) | Même [changelog](https://dev.freebox.fr/sdk/os/api_changes_1_1_to_2_0/#rrd-api-changes); la [référence RRD](https://dev.freebox.fr/sdk/os/rrd/#rrd-unstable) précise `[DEPRECATED, use cpub]`. Version : **1.1 → 2.0**. | Utiliser `cpub`. | **explicitement deprecated** |
| `temp3` (base RRD `temp`) | Même [changelog](https://dev.freebox.fr/sdk/os/api_changes_1_1_to_2_0/#rrd-api-changes); la [référence RRD](https://dev.freebox.fr/sdk/os/rrd/#rrd-unstable) précise `[DEPRECATED, use sw]`. Version : **1.1 → 2.0**. | Utiliser `sw`. | **explicitement deprecated** |
| Ancienne méthode HTTP d’upload (API d’upload v3) | Le changelog [API 3.0 → 4.0, « Deprecated API »](https://dev.freebox.fr/sdk/os/api_changes_3_0_to_4_0/#deprecated-api) indique que l’ancienne API d’upload est dépréciée au profit de l’upload WebSocket. La [référence File Upload](https://dev.freebox.fr/sdk/os/upload/#file-upload) confirme que la méthode HTTP précédente est dépréciée **depuis l’API v4**. | Pour un nouvel intégrateur, utiliser l’API WebSocket d’upload (`/api/v4/ws/upload`); si WebSocket est impossible, la source indique FTP. | **explicitement deprecated** |

## Suppression confirmée dans le flux WebSocket

| Élément concerné | Preuve officielle et version | Conséquence documentaire | Classement |
| --- | --- | --- | --- |
| Création préalable d’une « file upload authorization » dans le flux d’upload WebSocket | La [section WebSocket File Upload API](https://dev.freebox.fr/sdk/os/upload/#websocket-file-upload-api) indique que ce besoin « has now been removed ». Elle introduit le chemin `/api/v4/ws/upload`; la version implicite est donc **v4**. | Ne pas décrire une étape d’autorisation préalable pour un upload WebSocket. Cette preuve porte sur le flux WebSocket, pas sur la suppression vérifiée de chaque ancien endpoint HTTP. | **removed** |

## Annonces de suppression non confirmées — ne pas en déduire une exclusion automatique

| Élément concerné | Preuve officielle et version / date | Pourquoi le statut reste indéterminé | Classement |
| --- | --- | --- | --- |
| Upload API v3 | Le changelog [API 3.0 → 4.0](https://dev.freebox.fr/sdk/os/api_changes_3_0_to_4_0/#deprecated-api) annonce que l’API v3 d’upload « will be removed in next firmware release ». | La même source est une annonce de retrait; elle ne donne ni version de firmware ni confirmation ultérieure de la suppression. La dépréciation, elle, est certaine et figure déjà dans le tableau d’exclusion. | **incertain** |
| Accès non sécurisé à l’API (HTTP) | Le changelog [API 3.0 → 4.0, « Secure Access »](https://dev.freebox.fr/sdk/os/api_changes_3_0_to_4_0/#secure-access) impose HTTPS aux applications et annonce que l’accès non sécurisé « will be removed at some point ». La [page racine](https://dev.freebox.fr/sdk/os/#https-access) répète cette formulation. Version : **3.0 → 4.0**. | La source impose déjà HTTPS pour les applications. Elle n’établit pas que HTTP a effectivement disparu; aucune date ni version de suppression n’est fournie. Documenter HTTPS comme obligation, sans affirmer que tous les endpoints HTTP sont absents. | **incertain** |
| Ancienne API Server non documentée antérieure à Freebox Server 2.0.0 | Le billet officiel [Documentation de l’API du Freebox Server](https://dev.freebox.fr/blog/?p=1321), publié le **27 juin 2013** lors de 2.0.0, demande aux applications de migrer vers la nouvelle API et annonce que l’ancienne API, non documentée, sera supprimée dans une prochaine mise à jour. | Aucun nom d’opération, chemin ou version de retrait n’est publié dans ce billet. Cette source historique ne permet donc pas de filtrer une route précise du SDK actuel. | **incertain** |

## Contrôles de couverture et limites

- Les **32 pages liées depuis l’index public FreeboxOS** ont été parcourues, ainsi que les trois changelogs versionnés (`1.1 → 2.0`, `2.0 → 3.0`, `3.0 → 4.0`). Les seules occurrences SDK pertinentes sont celles consignées ci-dessus.
- Le lien demandé `https://dev.freebox.fr/sdk/changelog.html` répond **404** au relevé; les trois pages « API changes » sous `/sdk/os/` sont le changelog public disponible et ont servi de source canonique.
- La recherche n’a trouvé aucune mention SDK exploitable de `obsolete`, `replaced` ou `retired` hors des éléments ci-dessus. Les occurrences de « removed » relatives à l’autorisation d’upload et aux annonces futures sont distinguées afin de ne pas les transformer abusivement en suppressions confirmées.
- Ce relevé décrit ce que la documentation publique officielle affirme. Il ne prétend pas valider la disponibilité effective d’une route sur tous les firmwares actuels; la documentation publique affiche elle-même une API v4 et doit être confrontée aux sources plus récentes collectées dans le reste du projet.
