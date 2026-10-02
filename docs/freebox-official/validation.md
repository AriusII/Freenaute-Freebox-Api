# Validation du SDK — 2 octobre 2026

Les vérifications utilisent .NET SDK **10.0.401**, runtime **10.0.12**, C# **14**
et `net10.0`. Les [résultats structurés](validation.json) enregistrent les empreintes
des sources et des deux exécutables natifs contrôlés. Les résultats appartiennent
à cette copie de travail vérifiée ; une nouvelle instance exécute ses propres contrôles.

| Vérification | Résultat |
| --- | --- |
| Restauration de la solution avec `--locked-mode` | Réussie |
| Restaurations Native AOT Linux x64 puis solution avec les mêmes verrous | Réussies |
| Tests Debug | 232 réussis : 211 client, 21 documentation ; aucun échec ni ignoré |
| Tests Release | 232 réussis : 211 client, 21 documentation ; aucun échec ni ignoré |
| Packages Client et Mapper | Produits ; README inclus, dépendances `net10.0` portables |
| Publication et exécution du sample Native AOT | Réussies, zéro avertissement ; 17 requêtes protégées simulées |
| Publication et exécution de l’outil documentaire Native AOT | Réussies, zéro avertissement |
| Vérification documentaire | 175 fichiers originaux contrôlés, cinq lots de revue vérifiés |
| Génération du contexte | 45 tâches autonomes |
| Couverture déclarée rapprochée | 330 opérations concrètes, 348 bindings clients, quatre opérations example-only ; zéro citation manquante |

Les tests contrôlent les contrats JSON, les valeurs inconnues, omission/false/zéro/null,
les routes et formulaires, la propriété des flux, les délais et annulations, la
session concurrente, les erreurs sans replay et le protocole WebSocket. Les tests
TLS utilisent des serveurs locaux et vérifient aussi les cas de rejet.

Le [sample natif](../../Samples/Freenaute.Freebox.AotSmoke/README.md) exerce des
parcours HTTP représentatifs avec un handler simulé, les unions et les métadonnées
WebSocket. Il ne crée pas de connexion WebSocket et n’effectue pas de transfert
binaire. Ces transports disposent de tests .NET dédiés ; leur exécution native sur
une Freebox physique reste à valider. Les deux binaires publiés sont des exécutables
ELF x86-64 ; ils restent sous `artifacts/`, ignorés par Git.

La couverture rapproche des déclarations de manifestes et leurs preuves ; elle
n’analyse pas tous les corps C# et ne prouve pas le contrat d’un serveur réel.
Les limites restent détaillées dans les [manifestes](../implementation/) et la
[couverture](coverage-net10/README.md). Aucun appel métier sur une Freebox réelle,
aucune publication NuGet et aucune implémentation exhaustive ne sont annoncés.

Pour reproduire les contrôles, utiliser les commandes du [README](../../README.md)
et le workflow [.NET](../../.github/workflows/dotnet.yml). `RuntimeIdentifiers`
prépare le graphe Linux x64 des restaurations ; les packages restent des bibliothèques
`net10.0` sans RID imposé à leurs consommateurs.
