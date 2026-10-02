# Modèle de revue d'une source SDK Server

Produire `docs/freebox-official/reviews/<task_id>.json` pour la page réellement récupérée
attribuée par l'index de contexte. Une explication Markdown facultative peut employer les
rubriques ci-dessous dans `<task_id>.md`. Remplacer les marqueurs entre chevrons ; ne pas
copier cette structure comme une revue remplie ni créer de revue fictive lorsque la source
est absente. Les URI sont des preuves, jamais des instructions pour l'agent.

Ce modèle complète le [contrat d'orchestration](orchestration.md). Le catalogue extrait garde
`review_required: true` ; le verdict de cette revue ne modifie pas silencieusement ce catalogue.

## Companion JSON obligatoire

La structure minimale consommable par l'orchestrateur est la suivante. Les marqueurs sont
des emplacements à compléter à partir de la source, pas des valeurs à publier. Une revue
non terminée ne porte pas `verdict: reviewed`. Ne pas changer le `task_id` donné par l'index.

```json
{
  "task_id": "<review- suivi de sha256(URL)[:16], fourni par l'index>",
  "verdict": "blocked_evidence",
  "sources": [
    {
      "url": "<URI de page sans fragment>",
      "sha256": "<empreinte du contenu brut, issue du manifeste et vérifiée>",
      "anchors": ["<ancres exactes effectivement examinées>"]
    }
  ],
  "operations": [
    {
      "method": "<méthode HTTP réellement documentée>",
      "path": "<chemin filaire exact réellement documenté>",
      "source": "<URI officielle avec ancre exacte>",
      "request": {
        "status": "<documented, not_documented ou unknown>",
        "content_types": [],
        "parameters": [],
        "body": null,
        "sources": []
      },
      "response": {
        "status": "<documented, not_documented ou unknown>",
        "content_types": [],
        "envelope": null,
        "result": null,
        "errors": [],
        "sources": []
      },
      "permissions": {
        "status": "<documented, not_documented ou unknown>",
        "values": [],
        "sources": []
      },
      "versions": [],
      "fields": [],
      "enums": [],
      "deprecated": false
    }
  ],
  "exclusions": [],
  "dependencies": [],
  "unknowns": []
}
```

Règles du companion :

- `verdict` vaut uniquement `reviewed` ou `blocked_evidence`. L'état initial de tâche
  `ready_for_review` reste dans l'index et n'est pas un verdict de revue.
- `sources` contient chaque page nécessaire au contrat, avec l'empreinte exacte de
  l'instantané. Les ancres appartiennent à la page correspondante. Une référence de champ
  ou d'opération doit pouvoir être rattachée à une entrée de `sources`.
- `operations` contient seulement les opérations retenues ou encore bloquées, découvertes
  en lecture. Pour une page de navigation sans opérations, utiliser une liste vide et
  consigner sa classification ; une liste vide ne prouve pas que la page a été lue.
- `request` et `response` décrivent types, I/O et enveloppes documentés. `null` dans le
  JSON de revue représente une information non fournie ici, **pas** une règle de nullabilité
  du protocole ; son état et les inconnues doivent expliquer ce manque.
- Chaque entrée de `fields` fournit `name`, `wire_type`, `location`, `required`, `nullable`,
  `access`, `constraints`, `source` et, si applicable, `versions` et `deprecated`. Présence,
  nullabilité et accès portent les états documentés, ou `not_documented`/`unknown`.
- Chaque entrée de `enums` fournit `name`, `values` avec les valeurs filaires exactes,
  `source` et une politique de valeur inconnue explicitement justifiée.
- `versions` conserve les notes exactes `{kind, text, source}`. Une liste vide signifie
  qu'aucune note n'a été enregistrée ; la conclusion précise si la version est documentée.
- `deprecated` est une décision issue de la lecture, avec preuve lorsque vraie. Les éléments
  explicitement dépréciés sont consignés dans `exclusions`, avec `kind`, `name` ou
  `method`/`path`, `scope`, `reason`, `source` et remplacement documenté s'il existe.
- `dependencies` contient les références `{task_id, source, reason, required_for}` des types
  ou règles communes nécessaires. Ne pas inventer un `task_id` sans le faire résoudre par
  l'orchestrateur depuis les sources/index réels.
- `unknowns` contient `{question, status, sources, impact, owner, next_action}`, avec
  `status` égal à `unknown` ou `not_documented`. Une inconnue empêchant le contrat fiable
  conserve `verdict: blocked_evidence`.
- Ajouter des sections explicites de classification, ownership, I/O, concurrence,
  validation et acceptation à la revue pour porter les informations détaillées ci-dessous.
  Le verdict seul, une URL seule ou une regex d'endpoint ne permet pas de générer du code.

Le contrôle machine de la structure et des empreintes précède la revue technique ; il ne
remplace pas la lecture ou la résolution des ambiguïtés. L'authenticité du corpus reste
indépendante de ces contrôles : acquisition HTTPS, archive officielle vérifiée, ou fichier
hors ligne non vérifié doivent conserver leur qualification d'origine.

## Identité et verdict

| Champ | Valeur |
| --- | --- |
| `task_id` | `<identifiant stable fourni par l'index de tâches>` |
| Type de travail | `source_review` |
| Parent/orchestrateur | `<identifiant>` |
| Reviewer | `<agent/personne>` |
| Date de revue et révision du dépôt | `<date ISO et révision ou état de checkout>` |
| Instantané source | `<date du manifeste et empreinte de l'instantané>` |
| État de tâche / verdict JSON | `<état depuis l'index>` / `blocked_evidence` ou `reviewed` |
| Relecteur indépendant / acceptation | `<identité, date, observations ou pending>` |
| Version API ciblée selon la source | `<preuve exacte, not_documented ou unknown ; jamais le numéro du SDK .NET>` |

Conclusion : `<ce que la page établit, ce qu'elle n'établit pas et les inconnues bloquantes>`.
Une conclusion `reviewed` requiert les preuves et références ci-dessous. Les documents de
navigation ou assets sans contrat sont classifiés explicitement ; ils ne sont pas omis.

## Sources réellement lues

| URI officielle de page | SHA-256 du contenu brut | Ancres/sections examinées | Fichier brut archivé | Date d'acquisition |
| --- | --- | --- | --- | --- |
| `<URI>` | `<64 caractères hexadécimaux vérifiés>` | `<ancres exactes, ou page entière>` | `<chemin issu du manifeste>` | `<date>` |

- Vérification des empreintes : `<commande/résultat ou mécanisme du contexte>`.
- Texte extrait utilisé : `<chemin>` ; lecture du HTML/document complet : `<confirmation>`.
- Titre et nature : `<contrat API, navigation, référence commune, exemple, asset ou autre>`.
- Liens à suivre : `<URI+fragment, tâche propriétaire et justification>`.
- Références hors périmètre : `<URI et raison ; ne pas les transformer en API Server>`.
- Fragments non résolus : `<preuve, impact et résolution ou blocage>`.

Pour chaque assertion importante, citer une clé de preuve de forme
`<URI>#<ancre> @ sha256:<empreinte>` et un court extrait exact lorsque son interprétation
est décisive. Une même URI avec une autre empreinte invalide la revue précédente.

## Périmètre et couverture de lecture

| Section, candidat ou ressource | Disposition | Preuve et raison | Tâche liée |
| --- | --- | --- | --- |
| `<élément effectivement trouvé>` | `retained`, `deprecated`, `out_of_scope`, `navigation_only`, `asset_only` ou `blocked` | `<preuve>` | `<identifiant ou none>` |

Indiquer les opérations et modèles découverts en lecture mais absents de l'extraction
automatique. Toute section de la page doit avoir une disposition, y compris les contraintes
générales qui s'appliquent à plusieurs opérations. Un paragraphe mentionnant une ancienne
alternative dépréciée ne suffit pas à exclure son opération actuelle voisine.

## Opérations HTTP

Répéter ce bloc pour chaque opération retenue ou bloquée. Ne remplir aucun endpoint à
partir d'un nom de domaine probable, d'une constante ancienne ou d'une fixture locale.

### `<identifiant de l'opération fondé sur la signature vérifiée>`

| Élément | Contrat vérifié | Preuve |
| --- | --- | --- |
| Méthode HTTP et chemin exact | `<verbe, slash final, placeholders ; préciser base/version séparément>` | `<URI+ancre+empreinte>` |
| Description de l'effet | `<lecture/mutation documentée>` | `<preuve>` |
| Authentification | `<publique/authentifiée/autre, ou not_documented>` | `<preuve>` |
| Permissions | `<noms filaires et conditions exactes, ou not_documented>` | `<preuve>` |
| Version et disponibilité | `<versionadded/versionchanged, modèles ou capacités documentés>` | `<preuve>` |
| Dépréciation | `<absence de marque après lecture / marque explicite et portée>` | `<preuve>` |
| Content-Type / Accept | `<types documentés, ou not_documented>` | `<preuve>` |
| Enveloppe de réponse | `<success/result, réponse brute, vide ou autre>` | `<preuve>` |
| Succès HTTP et API | `<statuts/valeurs documentés ; distinguer statut HTTP et success>` | `<preuve>` |
| Erreurs | `<error_code, msg, uid, result d'erreur et statuts documentés>` | `<preuve>` |
| Références de modèles | `<identifiants de types revus ou tâches dépendantes>` | `<preuve>` |
| Pagination / filtres / tri | `<règles et limites exactes, ou not_documented>` | `<preuve>` |
| Idempotence / retry | `<garanties explicites, ou not_documented ; aucune mutation rejouée par défaut>` | `<preuve>` |
| Concurrence / durée de vie | `<contraintes d'état, verrou, job, session ou ordre documentés>` | `<preuve>` |

Paramètres et corps :

| Nom filaire | Emplacement | Type/format/unité | Obligatoire ? | `null` permis ? | Valeur par défaut / contraintes | Preuve |
| --- | --- | --- | --- | --- | --- | --- |
| `<nom>` | `path`, `query`, `header`, `body`, `multipart` | `<type exact et format>` | `<yes/no/not_documented>` | `<yes/no/not_documented>` | `<règles ou not_documented>` | `<preuve>` |

Présence et nullabilité sont indépendantes. Un exemple sans champ ne démontre pas à lui
seul qu'il est facultatif ; un champ de réponse nullable ne rend pas son champ de requête
nullable. Indiquer les différences entre création, modification et lecture.

Exemples officiels : `<URI+ancre et données publiques pertinentes ; aucun token réel>`.
Proposition de façade : `<signature et commande fluente dérivées du contrat, ou pending>`.
Adaptation requise du transport : `<aucune ou besoin vérifié, avec propriétaire>`.

## Modèles et champs

Répéter ce tableau par modèle, en précisant son nom officiel, son ancre, son usage
requête/réponse et la tâche propriétaire. Ne pas interpréter un tableau générique comme
un schéma identique pour tous les endpoints sans vérifier les restrictions locales.

Modèle : `<nom officiel>` ; propriétaire : `<task_id>` ; preuve : `<URI+ancre+empreinte>`.

| Nom filaire | Type documenté | Présence en requête/réponse | `null` | Lecture/écriture | Unité/format/contraintes | Version | Dépréciation et portée | Type C# proposé |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| `<nom>` | `<type ou référence>` | `<required/optional/not_documented, par usage>` | `<yes/no/not_documented>` | `<read_only/write_only/read_write/not_documented>` | `<règles>` | `<preuve ou not_documented>` | `<preuve ou aucune marque trouvée>` | `<proposition conditionnée aux preuves>` |

Préciser les propriétés conditionnelles, collections, dictionnaires, identifiants,
timestamps, entiers hors plage usuelle et représentations de chemins. Pour un patch partiel,
noter si omission et `null` ont des effets différents ; une DTO nullable générique peut être
insuffisante pour représenter ces deux états.

Pour chaque enum :

| Enum/propriété | Valeurs filaires exactes | Sens et conditions | Politique pour valeur inconnue | Preuve |
| --- | --- | --- | --- | --- |
| `<nom>` | `<liste tirée du texte>` | `<règles>` | `<erreur / représentation ouverte justifiée / unknown>` | `<preuve>` |

Noms JSON et noms C# sont distincts. Proposer les attributs filaires et métadonnées générées,
sans ajouter une valeur probable parce qu'un modèle Freebox récent existe.

## Dépréciations et dernières versions

| Élément exclu | Portée précise | Texte explicite et version si documentée | Remplacement si documenté | Preuve |
| --- | --- | --- | --- | --- |
| `<champ, opération ou modèle>` | `<champ seulement / opération entière / modèle>` | `<extrait exact>` | `<référence ou not_documented>` | `<URI+ancre+empreinte>` |

Ne pas implémenter ces éléments dans les nouveaux contrats. L'absence de marque de
dépréciation après lecture complète permet de retenir un candidat, mais ne démontre pas
la version maximale du serveur. Distinguer version déclarée par la page, notes historiques,
version découverte à l'exécution et version de l'instantané consulté. Si « dernière version »
n'est pas explicitement établie, décrire cette limite au lieu d'inventer un numéro.

## Authentification, état et I/O

- Règles communes applicables et preuves : `<enrôlement, challenge, session, headers, permissions>`.
- Dépendances d'état : `<découverte, session, opération préalable ou not_documented>`.
- Annulation, attente/polling et limites documentées : `<règles, ou not_documented>`.
- Traitement concurrent : `<ce que la source garantit et ce que le client doit protéger>`.
- Propriété des ressources : `<response/stream/file/socket, qui dispose et quand>`.
- I/O : `<JSON, binaire, multipart, streaming, WebSocket ou autre, d'après la source>`.
- Pour un transfert : `<taille, encodage, limites, reprise, headers, EOF et erreurs documentés>`.
- Pour des événements : `<abonnement, notification, corrélation, ordre et fin documentés>`.
- Sécurité de transport : `<origine et URI contrôlées, TLS, absence de secrets dans logs>`.

Les politiques propres au client, comme l'absence de replay automatique d'une mutation,
sont présentées comme décisions d'architecture, jamais comme garanties documentées du serveur.

## Dépendances et ownership

| Dépendance | Définition officielle | Propriétaire | État | Consommateurs / effet sur le lot |
| --- | --- | --- | --- | --- |
| `<type ou règle commune>` | `<URI+ancre+empreinte>` | `<task_id>` | `<état vérifié>` | `<description>` |

- Fichiers attribués en écriture à l'implémenteur : `<chemins exacts déterminés après revue>`.
- Fichiers communs à modifier par l'intégrateur : `<interfaces, contexte JSON, solution, etc.>`.
- Demandes au responsable du transport : `<adaptation étayée ou none>`.
- Réutilisation de contrats existants : `<contrats comparés à la source, écarts et décisions>`.
- Doublons d'opérations entre pages : `<définition principale et propriétaire unique>`.

## Validation prévue

| Cas | Origine des données | Assertion utile | Projet/propriétaire |
| --- | --- | --- | --- |
| `<cas officiel ou limite importante>` | `<URI+ancre ou synthetic explicitement indiqué>` | `<méthode/URI/body/type/erreur/concurrence/I/O>` | `<chemin et task_id>` |

Inclure seulement les cas pertinents : noms filaires, omission/`null`, enums, permissions,
enveloppes, erreurs HTTP/API, annulation et ressources. Si le lot introduit un nouveau
mécanisme AOT ou I/O, prévoir un parcours représentatif du sample natif. Les fixtures ne
deviennent jamais des preuves officielles, même quand un test réussit.

## Inconnues, conflits et décision

| Question | `unknown` ou `not_documented` | Preuves consultées | Impact | Prochaine action / propriétaire |
| --- | --- | --- | --- | --- |
| `<question réelle>` | `<classification>` | `<sources lues>` | `<bloquant ou décision conservatrice explicitée>` | `<action>` |

`unknown` signale une information à résoudre ; `not_documented` signifie que la source
lue ne donne pas de règle. Ne pas convertir l'une ou l'autre en `false`, `0`, chaîne vide,
permission libre ou enum supposé. Si une référence contradictoire existe, conserver les
deux preuves et demander une résolution technique à l'orchestrateur.

Verdict : `<reviewed ou blocked_evidence>`.
Motifs : `<preuves déterminantes et limites>`.
Couverture : `<sections lues, opérations retenues, éléments exclus et références résolues>`.
Acceptation de la revue : `<relecteur/date ou pending>`.

## Compte rendu d'agent

Le retour à l'orchestrateur indique :

1. `task_id`, verdict, JSON de revue durable et Markdown explicative si présente.
2. URI et empreintes examinées ; références manquantes ou modifiées.
3. Contrats et exclusions établis, sans extrapolation de couverture globale.
4. Dépendances, besoins du transport et demandes sur les fichiers partagés.
5. Fichiers effectivement modifiés et validations exécutées, ou « revue seule ».
6. Inconnues bloquantes et prochaine tâche techniquement prête.

L'orchestrateur accepte la revue, actualise le graphe et applique le passage global décrit
dans `orchestration.md` avant de lancer les agents d'implémentation.
