# Sources et provenance

`raw/embedded/doc/index.html` est la référence actuelle, issue de `http://mafreebox.freebox.fr/doc/index.html` sur la Freebox cible. C’est une page Sphinx monolithique qui annonce l’API 16.0.

`raw/public/` conserve le portail public Freebox SDK et Freebox OS en version 4.0, ses sources Sphinx et ses ressources. Ce corpus est historique et n’alimente pas les schémas actuels dans `docs/reference/`.

| Fichier | Contenu |
| --- | --- |
| [manifest.json](manifest.json) | URL demandée et finale, statut HTTP, date UTC, chemin local, taille, SHA-256, ETag et Last-Modified |
| [links.json](links.json) | Graphe des liens, ancres et ressources ; décision de périmètre pour chaque cible |
| [api-version.json](api-version.json) | Découverte de version et modèle, expurgée des valeurs identifiantes propres à la cible |
| `seed/` | Premières captures conservées comme preuves des audits initiaux |

La page locale affiche des exemples de Free, qui peuvent contenir des adresses, UID et jetons de démonstration. Aucune session n’a été ouverte sur la cible ; aucun jeton réel d’application ou de session n’a été obtenu.

Les sources brutes sont volontairement conservées sans filtrage afin que les exclusions soient vérifiables. La documentation filtrée à utiliser se trouve dans [l’index principal](../docs/INDEX.md).
