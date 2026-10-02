# Audit des sources non visibles — documentation locale Freebox OS v16

**Périmètre.** Audit en lecture seule, exclusivement par requêtes HTTP `GET`, réalisé le 2 octobre 2026 (Europe/Paris). Les résultats concernent la documentation que l’instance locale expose elle-même ; ils ne décrivent ni ne modifient la configuration de la Freebox.

## Source à retenir

| Élément | Valeur constatée |
|---|---|
| URL canonique exploitable | `http://mafreebox.freebox.fr/doc/index.html` |
| Réponse | `200 OK` (nginx) |
| Version de l’API annoncée dans le document | `16.0` (majeure `16`) |
| Version de build Sphinx | `b'c1fd8795'` |
| Dernière modification HTTP | `Fri, 17 Jul 2026 13:51:53 GMT` |
| ETag HTTP | `W/"6a5a3379-282b1"` |
| Instantané local déjà récupéré | `sources/seed/local-doc.html` — 1 816 775 octets, SHA-256 `CAB9F9512F5DF0C8C7857EFDE19790B99DCE92FB5C189DD7B87AEC1011E3AE03` |

L’URL de répertoire `http://mafreebox.freebox.fr/doc/` répond `403 Forbidden`. La collecte doit donc partir explicitement de `index.html`.

La page SDK publique `https://dev.freebox.fr/sdk/os/` annonce encore l’API `4.0` et ses fichiers HTTP indiquent une dernière modification en 2017. Pour la documentation d’une instance actuellement accessible, elle est une source historique et non la référence fonctionnelle à privilégier.

## Topologie réellement publiée

`index.html` est une documentation Sphinx monolithique : ses 830 éléments `<div class="section">` sont regroupés dans un seul fichier HTML. Les liens d’interface vers `search.html` et `genindex.html` ne sont pas servis par le document local ; les URL directes suivantes renvoient `404 Not Found` :

- `/doc/searchindex.js`
- `/doc/objects.inv`
- `/doc/search.html`
- `/doc/genindex.html`
- `/doc/_static/searchtools.js`
- `/doc/_sources/index.txt`
- `/doc/_sources/document-api/00_index.txt`
- `/doc/_sources/document-api/00_index.rst.txt`
- `/doc/robots.txt`
- `/doc/sitemap.xml`

`_static/documentation_options.js` annonce pourtant `HAS_SOURCE: true` et `SOURCELINK_SUFFIX: '.txt'`. Aucun lien source n’est présent dans le HTML généré et les emplacements Sphinx usuels testés ci-dessus sont absents : ce paramètre de build ne constitue donc pas une source récupérable sur cette Freebox.

Les ressources utiles servies et à conserver comme métadonnées sont :

| Ressource | État | Usage observé |
|---|---|---|
| `/doc/_static/documentation_options.js` | `200`, JavaScript, même date de modification que le document | porte le build `c1fd8795`, la version de routage et les indicateurs Sphinx |
| `/doc/_static/fbx.js` | `200`, JavaScript, même date de modification que le document | interprète les fragments `#ancre`, clone la section associée et masque le reste du document ; ce n’est pas un index d’API |
| `/doc/_static/main.css`, `/doc/_static/pygments.css`, `/doc/_static/favicon.ico` | référencés | présentation uniquement |

## Inventaire caché dans les ancres

L’HTML comporte **2 729** attributs `id`, dont **340 occurrences** d’ancres d’opérations HTTP de la forme `get--api-v…`, `post--api-v…`, `put--api-v…` ou `delete--api-v…`. Quatre identifiants sont répétés dans le HTML ; l’index d’opérations doit donc dédupliquer sur l’ancre exacte et contient **335 opérations uniques**.

| Méthode | Occurrences | Ancres uniques |
|---|---:|---:|
| GET | 169 | 168 |
| POST | 69 | 66 |
| PUT | 69 | 68 |
| DELETE | 33 | 33 |
| **Total** | **340** | **335** |

Les opérations présentes utilisent les versions de chemin `v8`, `v9`, `v10`, `v11`, `v13`, `v14`, `v15` et `v16`. Les ancres constituent ainsi une table d’index complète à partir de laquelle extraire les sections et leurs contrats, alors que les index Sphinx séparés ne sont pas disponibles.

Le document contient également **27** sections d’historique allant de `api-changes-from-version-1-1-to-2-0` à `api-changes-from-version-15-0-to-16-0`. Elles sont utiles pour expliquer une transition, mais ne doivent pas former le corpus d’API courante.

## Signaux de filtrage des API obsolètes

Les informations « deprecated » sont des sections d’historique intégrées et non des pages séparées. Cinq ancres explicites ont été relevées :

| Ancre | Consigne fournie par la documentation |
|---|---|
| `deprecated-api-v4` | ancienne API d’upload v3 : dépréciée au profit de l’upload WebSocket ; les nouvelles applications doivent employer le WebSocket |
| `deprecated-api-v5-0` | rappelle la même dépréciation de l’upload v3 |
| `deprecated-api-v8-0` | contrôle parental : remplacé par l’API Profile |
| `deprecated-api-v10-0` | API Connection d’agrégation xDSL/4G : remplacée par des endpoints distincts de statut LTE et d’agrégation |
| `system-config-v5-deprecated` | modèle/contrat `SystemConfigV5` explicitement marqué `DEPRECATED` |

Les **21** ancres contenant `unstable` forment une catégorie séparée de `deprecated` (par exemple VPN, PVR, RRD, Storage, RAID, VM, certaines données de connexion et de téléchargement). Elles ne doivent pas être supprimées au titre de la dépréciation : la documentation les décrit comme utilisables mais susceptibles de changer.

## Conséquence pour la collecte finale

1. Prendre `http://mafreebox.freebox.fr/doc/index.html` comme corpus primaire de l’instance et le préserver avec son ETag, sa date et son hachage.
2. Construire les fichiers Markdown à partir des sections/ancres du document monolithique, pas depuis le SDK public v4 ni depuis un index Sphinx inexistant.
3. Écarter du corpus courant les contrats expressément dépréciés ci-dessus ; conserver l’historique versionné dans une annexe de migration si nécessaire.
4. Garder les éléments `[UNSTABLE]` clairement étiquetés dans la documentation finale au lieu de les assimiler aux API dépréciées.

## URLs de preuve

- `http://mafreebox.freebox.fr/doc/index.html`
- `http://mafreebox.freebox.fr/doc/_static/documentation_options.js`
- `http://mafreebox.freebox.fr/doc/_static/fbx.js`
- `https://dev.freebox.fr/sdk/os/` (référence historique v4, utilisée seulement pour la comparaison)
