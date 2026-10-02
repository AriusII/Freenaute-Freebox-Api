# Documentation de référence — Freebox Server API 16.0

Le corpus actif provient du ZIP **Freebox-Server-API-16.0-2026-10-02.zip** fourni par
l’utilisateur. [current.json](current.json) sélectionne cet instantané ; les
[originaux](api-16.0/IMPORT.md) sont conservés sans modification.

| Inventaire après exclusion des éléments explicitement deprecated | Nombre |
| --- | ---: |
| Modules | 45 |
| Occurrences de signatures HTTP | 342 |
| Regroupements littéraux méthode/chemin | 338 |
| Objets déclarés | 195 |
| Propriétés actives | 1 254 |
| Événements nommés | 4 |

Les regroupements littéraux comprennent des alternatives de chemins et trois
callbacks destinés au serveur push du consommateur. Des opérations supplémentaires
apparaissent uniquement dans les exemples. Ces inventaires ne représentent donc pas
un nombre de méthodes C# ni une couverture d’implémentation.

## Intégrité et version

Le [rapport d’import](import-report.json) et la [revue d’intégrité](reviews/source-integrity.json)
enregistrent les vérifications suivantes :

- ZIP SHA-256 : `8d53243e3855ae2ec077c2e3ac9b890120d9ba9eaedc3ba955cdfe0b034b2094`.
- HTML embarqué SHA-256 : `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.
- 179 entrées de livraison et 84 ressources disponibles vérifiées ; 175 fichiers
  originaux conservés byte pour byte, cinq outils fournis exclus et jamais exécutés.
- 6 284 références contrôlées, sans fichier ou fragment manquant non recensé.
- 14 règles de dépréciation délimitées et dix retraits effectivement présents vérifiés.

Les trois ressources indisponibles sont le favicon, l’index général et la recherche.
Deux destinations Markdown WiFi contiennent des espaces non échappés ; le décompte
fourni de 2 711 identifiants uniques est corrigé à 2 719 dans l’audit. Les originaux
restent inchangés. Le fichier `.gitattributes` empêche leur conversion de fins de ligne.

Le monolithe embarqué annonce **API 16.0**, corroborée par la découverte assainie
fournie. Cette version décrit la cible de cet instantané. Les empreintes établissent
les octets livrés ; elles ne prouvent pas une acquisition HTTPS indépendante ni
la dernière version disponible pour tous les modèles Freebox. Les pages publiques
API 4.0 incluses restent historiques.

## Revues et contexte par module

| Lot | Modules | Revue |
| --- | ---: | --- |
| Protocole, découverte, authentification, WebSocket, caméra, notifications | 5 | [Protocole](reviews/protocol.md) |
| Réseau et WiFi | 10 | [Réseau](reviews/network.md) |
| Fichiers, téléchargements, stockage, RRD, VM | 10 | [Fichiers et stockage](reviews/files-storage.md) |
| Services, téléphonie, VPN, AirMedia, Player, PVR | 11 | [Services et médias](reviews/services-media.md) |
| Système, domotique et profils | 9 | [Système et maison](reviews/system-home.md) |

Le [contexte généré](context-net10/README.md) contient 45 tâches autonomes avec sources,
empreintes, projections et dépendances de lecture. Les rapports enregistrent les
contrats compris, leurs errata et les inconnues. Un contexte généré n’approuve pas
automatiquement une implémentation.

## Couverture du client

La [couverture calculée](coverage-net10/README.md) et son [JSON](coverage-net10/coverage.json)
rapprochent les citations des cinq revues et les déclarations des
[manifestes d’implémentation](../implementation). Les façades Discovery, Authentication,
Network, Services, Files, SystemHome, Cameras, Notifications, AirMedia et WebSockets
sont intégrées au client.

Le calcul distingue signatures formelles, opérations attestées uniquement par
exemple, routes concrètes alternatives et plusieurs bindings d’une même opération.
Les empreintes des fichiers déclarés relient les manifestes au code contrôlé.
L’outil valide ces déclarations ; il n’analyse pas les corps des méthodes C# et
ne prouve pas le comportement d’une Freebox réelle.

Les routes, encodages ou types insuffisamment documentés sont localisés dans les
manifestes et la [revue d’architecture](reviews/architecture-readiness.md).
Ils ne sont pas remplacés par des contrats inventés. Les trois callbacks
`/register`, `/register/{box_id}/{device_id}` et `/send` restent séparés des opérations
sortantes Freebox. La couverture ne revendique pas un SDK exhaustif.

## Vérification avec .NET

Depuis la racine du dépôt :

```bash
dotnet run --project Tools/Freenaute.Freebox.Documentation -- verify
dotnet run --project Tools/Freenaute.Freebox.Documentation -- context
dotnet run --project Tools/Freenaute.Freebox.Documentation -- coverage
```

L’[outil C#](../../Tools/Freenaute.Freebox.Documentation/README.md) fonctionne hors ligne,
sans exécuter les exemples ni les ressources archivées. Après modification volontaire
d’un fichier déclaré comme preuve, `refresh-evidence` actualise uniquement son
empreinte ; `coverage` reste strict lors d’une vérification normale.

Les [résultats de validation](validation.md) décrivent les tests contrôlés et les
publications Native AOT. Aucun appel métier sur une Freebox réelle n’a été exécuté.
La [procédure de capture](../source-acquisition.md) décrit les informations nécessaires
pour sélectionner un nouvel instantané.
