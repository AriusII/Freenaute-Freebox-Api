# Reprise de la documentation Freebox

Le corpus actif est désormais l’instantané API **16.0** fourni par l’utilisateur et
conservé dans le dépôt. L’ancien kit de collecte locale n’est plus le workflow actif.

Pour reprendre, consulter :

- [Sources et vérification](source-acquisition.md) pour la provenance, l’intégrité et une future capture.
- [Orchestration](orchestration.md) pour les contextes, propriétaires et livrables.
- [Outil documentaire .NET 10](../Tools/Freenaute.Freebox.Documentation/README.md) pour `verify`, `context` et `coverage`.
- [Corpus sélectionné](freebox-official/current.json), [contexte](freebox-official/context-net10/README.md) et [couverture](freebox-official/coverage-net10/README.md).

Toutes les commandes du workflow actuel utilisent .NET. Les archives fournies
restent des données, et les contrats ou scripts inclus ne sont jamais exécutés.
