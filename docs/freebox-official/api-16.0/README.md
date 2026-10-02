# Freebox Server — Référence SDK / API 16.0

Documentation de l’API Gateway Freebox Server, collectée et vérifiée le **2 octobre 2026**. Le contrat de référence provient de la documentation officielle **embarquée sur la Freebox cible**, qui annonce l’API **16.0**, en accord avec la découverte `/api_version`. Le modèle observé est une Freebox v9 (r1).

**Commencer par [le guide d’intégration](docs/GUIDE-SDK.md), puis consulter [l’index des modules](docs/INDEX.md).**

Le portail public [Freebox SDK](https://dev.freebox.fr/sdk/) et sa [documentation Freebox OS](https://dev.freebox.fr/sdk/os/) annoncent encore l’API 4.0. Ils ont été archivés pour la traçabilité, mais leurs anciens contrats ne complètent pas la référence actuelle. Les descriptions techniques restent dans la langue originale de Free ; les guides, index, décisions de filtrage et explications de portée sont en français.

## Accès à la documentation

| Document | Usage |
| --- | --- |
| [Guide SDK](docs/GUIDE-SDK.md) | Découverte, HTTPS, version, association, authentification, REST, WebSocket, chemins Base64 |
| [Index thématique](docs/INDEX.md) | Les 44 unités d’API et l’introduction transversale |
| [Catalogue des endpoints](docs/ENDPOINTS.md) | Méthodes, chemins officiels, stabilité et variantes de requête |
| [Catalogue des objets](docs/OBJETS.md) | Schémas, propriétés, types, droits et énumérations |
| [Événements WebSocket](docs/EVENEMENTS.md) | Événements et types de résultat explicitement documentés |
| [Évolutions jusqu’à 16.0](docs/EVOLUTIONS.md) | Les 27 historiques officiels, sans réintroduire les anciens contrats |
| [Exclusions et remplacements](docs/MIGRATIONS-EXCLUSIONS.md) | Éléments dépréciés, obsolètes ou retirés, avec preuve officielle |
| [Qualité et portée des sources](docs/QUALITE-SOURCES.md) | Exhaustivité contrôlée, limites documentaires et validation |

## Contenu vérifié

| Élément conservé dans la référence | Nombre |
| --- | ---: |
| Modules Markdown : 44 API + introduction | 45 |
| Opérations HTTP distinctes, méthode + chemin | 338 |
| Signatures HTTP, incluant les variantes documentées | 342 |
| Objets | 195 |
| Propriétés | 1 254 |
| Blocs d’exemples | 1 009 |
| Tableaux | 205 |
| Événements nommés dans `RegisterAction.events` | 4 |

Les **14 décisions d’exclusion** comprennent 10 suppressions ciblées dans le document actuel et 4 anciens contrats déjà absents de celui-ci. Six blocs d’exemples ont été adaptés pour retirer ou remplacer des champs dépréciés ; ces adaptations sont signalées dans les fiches et consignées dans [le journal des exemples](audits/adapted-examples.json). Les API `UNSTABLE` restent incluses avec leur statut.

## Organisation du dossier

```text
README.md                 Point d’entrée
docs/                     Documentation Markdown à utiliser
  reference/              Fiches par thème et module
catalogue/                Données JSON : opérations, variantes, objets, propriétés, événements
sources/                  Documents officiels bruts, ressources et manifeste SHA-256
  raw/embedded/           Source actuelle de la Freebox cible
  raw/public/             Archive historique du portail public v4
audits/                   Couverture, fraîcheur, dépréciations, transformations et validation
tools/                    Scripts reproductibles de collecte, génération et contrôle
```

## Interpréter les versions et la couverture

La découverte et la page embarquée annoncent **16.0**. Les signatures publiées par Free utilisent néanmoins plusieurs versions, notamment `/api/v8/`, et une route TFTP emploie `/api/latest/`. Ces chemins sont préservés littéralement. La référence ne prétend pas que remplacer leur version par `v16` est valide pour chaque appel.

Le module `player` est inclus parce qu’il décrit des routes du Gateway Server. Le SDK QML Player et le protocole de télécommande réseau sont hors du périmètre demandé. Les liens vers des applications, standards ou exemples externes sont inventoriés dans le graphe de liens ; ils ne constituent pas des contrats Freebox Server.

L’exhaustivité porte sur **toute la documentation officielle accessible dans ce périmètre**, après filtrage. Elle ne prouve pas que Free documente toutes les fonctions du firmware. Aucun endpoint métier, aucune session, aucun WebSocket et aucune mutation de configuration n’ont été exécutés. Les caractéristiques propres à d’autres modèles ou firmwares doivent être vérifiées sur leurs cibles.

## Traçabilité et reproduction

[Le manifeste des sources](sources/manifest.json) contient les URL, statuts HTTP, dates de collecte, métadonnées HTTP et empreintes SHA-256. [Le rapport de validation](audits/validation.json) contrôle l’intégrité, les liens locaux, la conservation des exemples et tableaux, ainsi que les exclusions. [Le catalogue JSON](catalogue/modules.json) permet de rattacher chaque fiche à son module et à sa source.

Les sources brutes conservent les anciens éléments pour permettre l’audit ; les contrats à utiliser se trouvent dans `docs/reference/`. Les valeurs identifiantes propres à la Freebox ne sont pas archivées dans la découverte. Les adresses, noms et jetons présents dans les exemples officiels sont ceux du document source.

Pour une nouvelle collecte, voir [les instructions des outils](tools/README.md). Conserver cet instantané et produire un nouveau dossier daté avant de renouveler les sources et de revoir les exclusions.
