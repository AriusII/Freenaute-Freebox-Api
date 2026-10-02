# Sources documentaires et vérification

Le corpus actif provient du ZIP utilisateur
`Freebox-Server-API-16.0-2026-10-02.zip`, reçu le **2 octobre 2026**. L’import conserve
**175 originaux** byte pour byte ; cinq fichiers d’outillage fournis ont été exclus
et n’ont pas été exécutés. Les sources brutes, projections et métadonnées sont dans
[api-16.0](freebox-official/api-16.0/IMPORT.md).

| Preuve | SHA-256 |
| --- | --- |
| ZIP fourni | `8d53243e3855ae2ec077c2e3ac9b890120d9ba9eaedc3ba955cdfe0b034b2094` |
| Monolithe embarqué brut | `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03` |

Le document embarqué et la découverte assainie annoncent **API 16.0** pour cet
instantané. Les pages publiques API **4.0** restent historiques. Une date de capture
et une empreinte ne prouvent ni l’authenticité distante indépendante ni la dernière
version sur toutes les Freebox. Les métadonnées de provenance conservent cette limite.

## Vérifier le corpus sélectionné

Depuis la racine du dépôt, avec le SDK défini dans `global.json` :

```sh
dotnet run --project Tools/Freenaute.Freebox.Documentation -- verify
dotnet run --project Tools/Freenaute.Freebox.Documentation -- context
dotnet run --project Tools/Freenaute.Freebox.Documentation -- coverage
```

L’outil est écrit en C# 14/.NET 10 et fonctionne hors ligne. `verify` charge
[current.json](freebox-official/current.json), revérifie les fichiers copiés et
l’inventaire de livraison, puis contrôle l’accord entre version, HTML, découverte
et rapports. Les chemins doivent rester contenus et portables ; les liens
symboliques et points de réanalyse sont refusés. Les défauts et ressources
indisponibles connus restent enregistrés.

`context` produit [context-net10](freebox-official/context-net10/README.md) : tâches
par module, sources et références complètes, empreintes et dépendances de lecture.
`coverage` produit [coverage-net10](freebox-official/coverage-net10/README.md) depuis
les revues et [manifestes d’implémentation](implementation/). Il rapproche les
méthodes, chemins et citations sans approuver automatiquement du code. Les signatures
formelles et les opérations attestées uniquement dans les exemples sont distinctes.

Ces commandes ne récupèrent aucune URL, n’extraient aucun nouveau ZIP et n’exécutent
aucun script ni exemple inclus dans la documentation. Leurs options, limites et
protections d’écrasement sont décrites dans [la documentation de l’outil](../Tools/Freenaute.Freebox.Documentation/README.md).

## Préparer un nouvel instantané

Les points d’entrée officiels restent :

- <https://dev.freebox.fr/sdk/>
- <https://dev.freebox.fr/sdk/server.html>
- <https://dev.freebox.fr/sdk/os/>

Une nouvelle collecte suit les liens, ressources et fragments du périmètre Server
réellement trouvé. Elle conserve les octets bruts, URL finale, type de contenu,
statut, date et SHA-256 de chaque ressource. Les échecs, liens externes et références
non résolues sont enregistrés ; une copie historique ou un exemple synthétique ne
remplace pas une page manquante.

Conserver le ZIP original et sa livraison. Préparer une nouvelle sélection dans un
dossier distinct, contrôler les tailles et chemins d’archive, puis vérifier les
rapports d’intégrité avant de changer `current.json`. Le propriétaire du corpus
adapte les métadonnées attendues par l’outil ; aucune commande d’import ou de crawler
supprimée ne fait partie du workflow actuel. Les originaux déjà sélectionnés restent
inchangés et les exécutables fournis sont traités comme des données.

Une collecte de documentation ne nécessite aucune mutation métier de la Freebox.
Les tokens, identifiants de matériel et données personnelles n’entrent pas dans les
preuves versionnées. La capture de découverte conservée ici est assainie.

## Lire et consolider

L’[orchestration](orchestration.md) attribue les fichiers et les dépendances.
Le [modèle de revue](sdk-review-template.md) impose méthode, chemin, champs,
présence/nullabilité, enums, unités, permissions, versions, erreurs et I/O avec leurs
preuves. Une propriété obsolète n’exclut pas son modèle actif. La couverture d’un
inventaire ne remplace pas la compréhension des contrats ; les inconnues bloquantes
restent dans les revues et manifestes.
