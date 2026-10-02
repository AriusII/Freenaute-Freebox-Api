# Audit indépendant final de couverture

**Décision : conforme.** La référence livrée couvre le contrat Freebox Server exposé par la documentation embarquée de l’équipement cible, annoncée en API 16.0. Elle ne mélange pas ce contrat actuel avec l’archive publique Freebox OS v4. Les contrôles structurels, de fidélité des exclusions, de catalogue et de liens locaux sont passés le 2 octobre 2026.

## Périmètre vérifié

La source de référence est [la documentation embarquée brute](../sources/raw/embedded/doc/index.html), dont l’empreinte SHA-256 est `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`. Elle annonce l’API `16.0` dans [la réponse de découverte expurgée](../sources/api-version.json).

Le manifeste contient 87 requêtes documentaires : 84 ressources présentes et intègres, et 3 réponses 404 de liens de thème de la Freebox (`favicon.ico`, `genindex.html`, `search.html`). Ces trois absences ne portent aucune définition d’API. L’archive publique complète est également présente à titre historique : 33 pages HTML de contenu, index inclus, 33 sources Sphinx RST, les index de recherche/navigation (`objects.inv`, `searchindex.js`, `genindex`, `search`) et les ressources statiques accessibles.

Le périmètre Server comprend la fiche `Player [UNSTABLE]` publiée par la passerelle Freebox Server. Les interfaces QML Player publiques et le protocole de télécommande ne font pas partie de ce contrat et ne sont pas inclus. Les API marquées `UNSTABLE` restent documentées : ce statut n’est pas une dépréciation.

## Conservation mesurée

Le découpage du monolithe Sphinx est contrôlé avant et après filtrage. Les 72 unités brutes correspondent à l’introduction, 44 modules d’API et 27 historiques de changement. Les 45 fiches publiées regroupent l’introduction et les 44 modules d’API actuels.

| Élément | Source brute | Référence publiée | Écart expliqué |
| --- | ---: | ---: | --- |
| Signatures HTTP | 343 | 342 | 1 exemple historique v5 retiré |
| Opérations méthode + chemin uniques | — | 338 | Les variantes de forme restent dans les fiches |
| Objets | 196 | 195 | `SystemConfigV5` retiré |
| Propriétés | 1 271 | 1 254 | 17 propriétés dépréciées retirées |
| Blocs d’exemples | 1 012 | 1 009 | 3 blocs liés au contrat retiré |
| Tableaux | 207 | 205 | 2 tableaux du schéma v5 retirés |
| Lignes de tableaux | 1 508 | 1 494 | 14 lignes ciblées retirées |

La vérification indépendante de `owned_modules()` retrouve exactement les six totaux bruts : 343, 196, 1 271, 1 012, 207 et 1 508. Elle évite l’ancien comptage par expression régulière qui trouvait 340 opérations parce qu’il supposait le préfixe `/api/`. Les trois opérations Notification publiées hors de ce préfixe sont bien conservées : `POST /register`, `DELETE /register/{box_id}/{device_id}` et `POST /send`.

Le catalogue garde 342 occurrences de signature et regroupe l’index en 338 opérations. Les quatre occurrences supplémentaires sont des variantes réellement publiées : deux pour `PUT /api/v9/wifi/config/`, deux pour `POST /api/v8/downloads/add` et trois pour `POST /api/v8/home/pairing/{adapter_id}`. Les ancres locales distinguent ces variantes sans les faire passer pour des opérations différentes.

## Dépréciations et données conservées

Les [14 règles examinées](deprecation-exclusions.json) comprennent 10 suppressions concrètes et 4 règles historiques ou logiques, absentes de la référence actuelle. Chaque suppression cible un identifiant ou une ligne de tableau source ; aucune suppression n’est fondée sur le seul numéro de version d’un chemin. Les six exemples transformés sont consignés dans [adapted-examples.json](adapted-examples.json).

Le contrôle post-génération confirme l’absence de `DownloadFile.path`, `WifiBssStatus.is_main_bss`, `WifiBssConfig.use_default_config`, `SystemConfigV5` et des métriques RRD `temp1`, `temp2`, `temp3`. Le champ actuel `WifiGlobalState.expected_phys` est conservé. Les occurrences restantes de `path` et `device_type` correspondent à des contrats actuels distincts.

## Liens, ancres et catalogues

La validation de livraison vérifie les empreintes des 84 ressources présentes, 56 fichiers Markdown, 1 283 liens locaux, 45 modules, 1 009 blocs de code et 205 tableaux. Elle confirme aussi les exclusions et la présence du champ Wi-Fi actuel. Son résultat est archivé dans [validation.json](validation.json).

Les 342 entrées d’endpoint, les 195 objets et les 1 254 propriétés ont une cible Markdown locale. Neuf propriétés de la source n’avaient pas d’attribut `id` Sphinx ; elles reçoivent une ancre Markdown déterministe et gardent leur contexte source parent. La liste et cette provenance sont conservées dans [unanchored-source-properties.json](unanchored-source-properties.json). C’est une lacune d’ancrage de la source, pas une perte de propriété.

Les liens internes issus de la source n’ont produit aucun défaut non résolu dans [source-link-defects.json](source-link-defects.json). Les quatre événements WebSocket explicitement publiés sont présents dans [le catalogue](../catalogue/events.json) et dans [la fiche d’événements](../docs/EVENEMENTS.md).

## Limites vérifiées

Cet audit valide l’exhaustivité documentaire de l’instantané collecté et la cohérence de sa transformation en Markdown. Les chemins sont reproduits tels que la source les publie ; aucun endpoint n’a été appelé et aucune disponibilité matérielle, autorisation ou compatibilité d’exécution n’est déduite. L’archive publique v4 est conservée pour traçabilité, sans être présentée comme le contrat API 16.0.
