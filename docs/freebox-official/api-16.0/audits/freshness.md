# Audit de fraîcheur — API Freebox Server

**État vérifié : 2 octobre 2026, 17:12 UTC**  
**Périmètre :** sources publiques Freebox et instantané documentaire de la Freebox cible. Aucune requête authentifiée, aucune mutation de la Freebox.

## Décision de référence

La documentation publique `dev.freebox.fr/sdk/os/` ne doit **pas** servir de référence fonctionnelle pour ce dossier. Elle déclare encore l'API **4.0**, alors que la documentation embarquée collectée depuis la Freebox cible déclare l'API **16.0**. Elle est donc conservée uniquement comme source historique (par exemple, pour expliquer une compatibilité ancienne), et jamais comme description de l'API actuelle.

La référence fonctionnelle à utiliser dans le corpus est la documentation **embarquée dans la Freebox effectivement interrogée**, après vérification de sa version par le mécanisme de découverte. Pour l'instantané collecté ici, cette version est **16.0**.

Ce choix est nécessaire pour satisfaire l'objectif « API à jour » : le portail public ne contient pas les changements 4.0 → 16.0, alors que la documentation embarquée les liste jusqu'à 16.0.

## Éléments observés

| Source officielle | Contrôle effectué | Résultat | Statut dans le corpus |
|---|---|---|---|
| [SDK public](https://dev.freebox.fr/sdk/) | HTTPS `HEAD` et `GET` | `200`; `Last-Modified: Mon, 20 Jul 2020 17:22:10 GMT` | Index historique uniquement |
| [Page Freebox Server publique](https://dev.freebox.fr/sdk/server.html) | HTTPS `HEAD` et `GET` | `200`; même `Last-Modified` de 2020; elle mène à `os/` | Orientation historique uniquement |
| [Documentation publique Freebox OS](https://dev.freebox.fr/sdk/os/) | HTTPS `HEAD` et `GET` | `200`; `Last-Modified: Tue, 28 Mar 2017 14:13:34 GMT`; déclaration explicite « Current API version is 4.0 » | **Exclue de la référence actuelle** |
| [Source Sphinx public](https://dev.freebox.fr/sdk/os/_sources/index.txt) | HTTPS `HEAD` et `GET` | `200`; même `Last-Modified` du 28 mars 2017 | Confirme une copie Sphinx statique, historique uniquement |
| Documentation Freebox OS embarquée capturée dans `sources/seed/local-doc.html` | Lecture du fichier collecté depuis la cible | titre : `FreeboxOS Gateway api b'c1fd8795' documentation`; déclaration explicite « Current API version is 16.0 » | **Référence principale de cet instantané** |
| Découverte locale de la cible (`/api_version`), relevée par l'orchestrateur parent | Lecture non authentifiée | `api_version: 16.0` | Contrôle de cohérence de la référence principale |

Les dates `Last-Modified` sont des métadonnées HTTP de cache : elles ne constituent pas, seules, une date de publication. Ici, elles corroborent toutefois une incompatibilité déjà démontrée par le contenu : l'écart de version majeure 4 → 16.

L'empreinte SHA-256 du document embarqué utilisé pour cet audit est :

```text
cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03
```

Elle permet d'identifier précisément l'instantané sans enregistrer d'identifiant, de domaine, d'adresse ou de jeton propres à la Freebox.

## Vérification du mouvement firmware, sans extrapolation sur la cible

Le blog officiel Freebox montre que les firmwares Server continuent d'évoluer. La publication [Freebox Server 4.13.0](https://dev.freebox.fr/blog/?p=22529), datée du **16 septembre 2026**, annonce des changements Wi-Fi/DFS. La publication [4.9.0](https://dev.freebox.fr/blog/?p=21453), datée du **18 mars 2025**, annonçait entre autres WPA3, réseau invité, MLO, contrôle LED et un correctif d'URL d'API caméra. Ces évolutions postérieures à 2017 sont absentes de la copie publique v4.0.

Le billet historique officiel [Documentation de l'API du Freebox Server](https://dev.freebox.fr/blog/?p=1321), du **27 juin 2013**, indique que la documentation est d'abord disponible sur le Server et répliquée sur `dev.freebox.fr`, afin de suivre les évolutions de firmware. L'écart mesuré entre les deux copies montre que seule la documentation embarquée est actuellement appropriée comme contrat de l'instance.

Ces publications établissent l'activité du produit, **pas la version de firmware de la Freebox cible** : cette version n'a pas été relevée pour le présent audit et ne doit pas être déduite de la version d'API 16.0 ou de ces billets.

## Ce que la documentation embarquée affirme elle-même

Dans la section « API Version », le document v16.0 établit la politique à reprendre dans les Markdown finaux :

- une API marquée **unstable** peut changer ou disparaître à tout moment ;
- une API non documentée ne doit pas être utilisée ;
- les autres API sont maintenues pendant au moins une publication Freebox.

Le document embarqué possède en outre les entrées de changements jusqu'à **15.0 → 16.0**. Cette dernière entrée ajoute notamment les fonctions de planification du bandeau LED, TFTP, options DHCP, économiseur d'écran, routes IPv4 statiques, nom de domaine LAN et Wi-Fi steering. Ces éléments n'existent pas dans la page publique v4.0.

## Règles de production pour le dossier final

1. Les fiches d'endpoint, objets et exemples doivent venir de l'instantané embarqué v16.0 et de ses sous-pages collectées, jamais de la page publique v4.0 lorsqu'elles divergent.
2. La découverte fournit une **URL candidate** construite avec le majeur courant (`v16` pour cet instantané). Elle ne valide pas rétroactivement les signatures littérales d'exemples ou d'endpoints historiques. Conserver fidèlement les chemins et les versions explicitement écrits dans la documentation, notamment `/api/v5`, `/api/v6` et `/api/v8`, avec leur contexte et leur statut ; ne pas les réécrire mécaniquement en `/api/v16`.
3. Exclure de la référence principale chaque endpoint, type ou champ explicitement marqué **Deprecated**, ainsi que les API indiquées comme « no longer usable ». Conserver, dans un journal de migration séparé, le remplacement documenté.
4. Conserver les API **unstable** seulement si elles sont clairement étiquetées comme telles ; elles ne peuvent pas être présentées comme garanties ou universelles.
5. N'ajouter aucun endpoint seulement observé dans l'interface web ou dans une bibliothèque tierce. La documentation embarquée dit explicitement de ne pas utiliser les API non documentées.
6. Associer chaque page finale à la version de l'instantané et à son empreinte. Une prochaine mise à jour de firmware impose de relire `/api_version`, de re-télécharger la documentation embarquée et de refaire cette comparaison avant de déclarer le corpus « à jour ».

## Limites exactes de l'affirmation « à jour »

Le dossier peut être qualifié d'**à jour pour la Freebox cible, au moment de l'instantané, en API 16.0**, si les sous-pages embarquées ont toutes été collectées et que le filtre de dépréciation a été appliqué.

Il ne peut pas être qualifié d'« API actuelle de toutes les Freebox » sans instantanés supplémentaires. La documentation de découverte distingue plusieurs modèles (v6 à v9) et l'implémentation, les permissions et les fonctions matérielles peuvent dépendre du modèle, du firmware et de la configuration. La version majeure indique le contrat disponible sur une cible ; elle ne prouve pas que chaque fonction est applicable à tous les modèles.

Le commit affiché dans le titre de la documentation embarquée (`c1fd8795`) identifie un build documentaire, mais ne fournit pas de date de publication publique. Il sert donc d'identifiant d'instantané, non de preuve qu'aucune mise à jour postérieure n'existe.

## Liens officiels complémentaires à conserver

- [Découverte de l'API sur la Freebox](https://mafreebox.freebox.fr/api_version) : source d'exécution locale qui fournit notamment la version et la base d'API. Utiliser seulement sur le réseau de la Freebox et ne jamais publier les valeurs propres à une installation.
- [Documentation Freebox OS publique, v4.0](https://dev.freebox.fr/sdk/os/) : annexe historique explicitement étiquetée comme telle.
- [Page SDK Server publique](https://dev.freebox.fr/sdk/server.html) : point d'entrée historique vers la page précédente, pas une source de couverture v16.
- [Suivi officiel FS#33956](https://dev.freebox.fr/bugs/task/33956) : signalement historique sur l'écart entre la documentation publique et celle de Freebox OS. C'est un ticket communautaire hébergé par Free, utile comme contexte mais non comme spécification.

## Contrôle à effectuer lors d'un renouvellement

1. Relever `api_version` via la découverte, sans authentification.
2. Télécharger la page de documentation embarquée et ses dépendances documentaires dans un répertoire daté.
3. Comparer la version déclarée par le document avec celle de la découverte ; signaler l'écart avant toute génération.
4. Enregistrer l'empreinte du document racine et la version de chaque sous-page récupérée.
5. Repasser le filtre de dépréciation et signaler les entrées ajoutées, supprimées ou devenues instables.
6. Mettre à jour la mention de portée : modèle(s), firmware(s) et date de l'instantané réellement vérifiés.

## Prudence sur les exemples et les liens internes de la documentation v16

Le monolithe embarqué v16 contient tout l'historique des changements depuis les premières versions. Il conserve donc des signatures, URLs d'exemple et noms d'objets anciens, y compris avec des majeurs API précédents. Leur présence dans une page v16 ne signifie ni que l'exemple doit être modernisé par substitution de version, ni que le comportement a été testé sur la cible avec `v16`.

La génération Sphinx contient aussi des renvois croisés, dont certains peuvent être anciens, incomplets ou cassés après une extraction partielle. Un lien interne qui ne se résout pas n'autorise pas à inventer une route, un type ou une compatibilité. Le corpus doit préserver le libellé source, signaler le renvoi défaillant, puis s'appuyer seulement sur une section documentaire explicite ou une validation ciblée autorisée.
