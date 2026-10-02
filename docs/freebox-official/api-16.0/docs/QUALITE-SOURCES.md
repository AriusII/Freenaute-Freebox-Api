# Qualité des sources et portée de la référence

## Source actuelle retenue

La [documentation embarquée](../sources/raw/embedded/doc/index.html#developer-api-documentation) annonce l’API **16.0**, comme [la découverte expurgée](../sources/api-version.json). Son identifiant de build documentaire est `c1fd8795`. L’empreinte du document de 1 816 775 octets est :

```text
cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03
```

La collecte a été effectuée le **2 octobre 2026**. L’en-tête `Last-Modified` de la documentation embarquée indique le 17 juillet 2026 ; c’est une métadonnée HTTP et non une preuve du firmware effectivement installé. Ce dernier n’a pas été interrogé.

La [documentation publique Freebox OS](https://dev.freebox.fr/sdk/os/) annonce encore l’API 4.0. Les données de cette archive ne sont pas fusionnées avec les schémas de la référence actuelle. [L’audit de fraîcheur](../audits/freshness.md) explique cette décision.

## Exhaustivité contrôlée

Le document embarqué est un monolithe Sphinx : les liens internes mènent généralement à des ancres de la même page. Il contient **72 unités documentaires** : l’introduction, **44 unités d’API** et **27 historiques de changement**.

Avant tout filtrage, le découpage retrouve exactement les **343 signatures HTTP, 196 objets, 1 271 propriétés, 1 012 blocs d’exemples et 207 tableaux** de la source. Ce contrôle est bloquant dans le générateur : une perte ou une duplication empêche la génération.

Après les exclusions ciblées, la référence contient **342 signatures, 338 opérations distinctes, 195 objets, 1 254 propriétés, 1 009 blocs d’exemples et 205 tableaux**. Les variantes qui partagent une méthode et un chemin sont toutes conservées dans les fiches, avec des ancres locales distinctes ; l’index des endpoints les regroupe.

Le crawl a collecté **84 ressources avec succès** et enregistré **3 réponses 404**. L’archive publique comprend toutes les 33 pages de contenu Freebox OS, les 33 sources Sphinx, les index de navigation/recherche et les ressources de rendu accessibles dans le périmètre. [Le manifeste](../sources/manifest.json) et [le graphe de liens](../sources/links.json) décrivent précisément les ressources et la frontière du crawl.

Les trois liens embarqués qui répondent 404 sont `favicon.ico`, `genindex.html` et `search.html`. Ils sont déclarés par le thème mais absents de la Freebox cible. Ils ne contiennent aucune définition d’API manquante à partir de laquelle le contrat aurait dû être complété. La page monolithique porte les définitions et leurs ancres.

## Dépréciations et transformations

Le filtre s’appuie sur les déclarations officielles `Deprecated`, `obsolete`, `no longer usable` et les retraits explicitement documentés. Il supprime un champ, une ligne de tableau ou une variante historique identifiée. Le numéro ancien d’un chemin et le label `UNSTABLE` ne déclenchent pas d’exclusion.

Le champ actuel **`WifiGlobalState.expected_phys` est conservé**. Le changelog v10.2 déprécie un champ du même nom dans l’ancienne **configuration** Wi-Fi et introduit l’API d’**état**. Confondre ces schémas aurait retiré un champ actuel. [L’audit de dépréciation](../audits/deprecation-audit.md) et [les règles JSON](../audits/deprecation-exclusions.json) consignent cette distinction.

Les autres exclusions et leurs remplacements sont détaillés dans [le journal de migration](MIGRATIONS-EXCLUSIONS.md). [Les suppressions effectivement appliquées](../audits/applied-exclusions.json) et [les six adaptations d’exemples](../audits/adapted-examples.json) sont disponibles pour contrôle. Les descriptions, signatures, valeurs de types, énumérations, contraintes et erreurs restantes sont préservées.

Le générateur normalise les titres, transforme les définitions Sphinx en Markdown lisible, garde les tableaux et blocs de code, remplace les liens internes par des liens entre fiches et distingue les ancres répétées des variantes. Une illustration Base64 tronquée dans la source est signalée comme telle ; aucune image complète ne peut être reconstruite à partir de cet exemple.

Neuf propriétés de la source ne possèdent pas d’identifiant HTML. Elles restent conservées et disposent d’une ancre locale ajoutée par le générateur. Leur provenance pointe vers la section officielle qui les contient ; [le relevé des propriétés sans ancre source](../audits/unanchored-source-properties.json) les identifie précisément.

## Limites de la documentation officielle

- L’annonce API 16.0 coexiste avec des signatures et exemples sous des versions plus anciennes. Ces chemins sont reproduits tels quels, sans prétendre à une compatibilité vérifiée sous `v16`.
- La documentation expose des API dépendantes du matériel, des fonctionnalités configurées et des permissions. Leur présence dans un document n’établit pas leur disponibilité sur tous les modèles.
- Les exemples officiels comportent parfois des JSON illustratifs avec commentaires, des valeurs de démonstration, des espaces ou des incohérences rédactionnelles. Ils ne sont pas présentés comme des tests automatisés exécutés.
- Certains endpoints Notification sont publiés hors du préfixe `/api/` : `POST /register`, `DELETE /register/{box_id}/{device_id}` et `POST /send`. Leur contexte et leur hôte de service restent ceux de [la fiche Notification](reference/maison-profils/notif.md).
- Les API non documentées n’ont pas été ajoutées par inspection de l’interface ou par récupération d’un SDK tiers. Les contrats manquants chez Free restent une limite de la source.

## Contrôles de livraison

[Le rapport machine](../audits/validation.json) vérifie les empreintes des sources, l’existence des fichiers et ancres liés, l’unicité des ancres locales, la conservation de chaque bloc d’exemple et tableau, le retrait des champs dépréciés et le maintien du champ d’état Wi-Fi actuel. [L’audit indépendant final](../audits/final-coverage-review.md) complète ces contrôles.

Cette livraison constitue un **instantané documentaire officiel de l’API 16.0 de la Freebox cible**. La validation a porté sur la collecte et la documentation, pas sur l’exécution fonctionnelle des appels.
