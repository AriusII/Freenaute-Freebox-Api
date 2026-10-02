# Readiness de l’architecture et des contrats

Le socle HTTP/DI peut recevoir les corrections de protocole directement étayées. La
génération de l’intégralité du SDK fortement typé reste bloquée par des contrats réellement
absents ou contradictoires. Le catalogue fourni contient 45 modules, 342 occurrences de
signatures HTTP, 338 signatures littérales distinctes, 195 déclarations d’objets, 1 254
propriétés et quatre événements. Ces nombres décrivent des déclarations conservées ; ils
ne mesurent ni des types validés ni la couverture du code.

La revue durable est [architecture-readiness.json](architecture-readiness.json), tâche
`architecture-readiness`, verdict `blocked_evidence`, acceptation globale en attente de
`/root`. Elle examine l’import, les contextes, les corrections du socle et les choix
d’architecture. Elle complète les cinq revues sémantiques ; elle ne prétend pas les remplacer
par une deuxième lecture exhaustive de tous les modules.

## Preuves et périmètre

Source principale : `http://mafreebox.freebox.fr/doc/index.html`, instantané fourni par
l’utilisateur, SHA-256
`cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.
Le document embarqué et la découverte sanitizée indiquent **16.0** pour cet instantané.
L’archive publique **4.0** est historique. L’origine distante et la dernière version sur
toutes les Freebox n’ont pas été établies indépendamment par le cloud.

Le JSON conserve les empreintes des fichiers relus et des revues consultées, les ancres
brutes effectivement vérifiées et les propositions conditionnelles. Les outils fournis
dans le ZIP n’ont pas été exécutés. Aucun appel d’API réelle ni handshake TLS n’a été réalisé
pendant cette revue.

## Import et contextes

L’import vérifie le ZIP borné, ses chemins, les octets extraits et l’inventaire de livraison
avant de copier les documents. Il indique explicitement la provenance fournie, conserve
les exclusions comme preuves à relire et laisse `review_required` vrai. Les contextes
revérifient chaque fichier copié et l’accord de version. Ils attribuent des tâches aux
modules réels ; `implementation_ready` reste faux. Les callbacks du serveur push
consommateur ne deviennent pas des routes de la Freebox.

Le défaut **P2** du chemin `--review` a été corrigé par le propriétaire de l’importeur :
le défaut et le lien des contextes pointent désormais tous deux vers
`docs/freebox-official/reviews/source-integrity.json`. La correction a été relue. Aucun
autre problème concret de provenance, de validation des octets ou d’exécution de contenu
fourni n’a été trouvé.

## Corrections du socle relues

Les deux racines françaises sont identiques aux PEM bruts de `#https-access`. Leurs DER et
empreintes ont été vérifiés indépendamment avec OpenSSL :

| Racine | SHA-256 DER |
| --- | --- |
| Freebox ECC Root CA | `23cfb72636be665dbbf2fe3e0527904771a8dff1bace461d8bd69f34812c2444` |
| Freebox Root CA | `2bd8b5be1a990e42ad1bd79c306eb519b637ee2475c0d931f257535610e9c3e7` |

La confiance est limitée au handler avec `CustomRootTrust`, `NoFlag` et EKU
`serverAuth`. La vérification standard de `SslStream` conserve la validation du nom et de
la chaîne ; aucun callback permissif ne la contourne. Les certificats sont possédés et
libérés par le handler. Les redirections et cookies sont désactivés. La révocation suit
`NoCheck`, défaut .NET HTTP, avec options explicites `Online` et `Offline`. Les racines
Iliad italiennes publiées dans la même section ne sont pas intégrées à ce socle français.
Ces conclusions portent sur le code et les certificats, sans preuve de handshake réel.

La source documente explicitement la découverte HTTPS sur `mafreebox.freebox.fr`. Le
défaut HTTPS est donc justifié. `BoxModel` conserve la chaîne reçue et sa classification
connue reste facultative ; le constructeur JSON sélectionné préserve ce comportement avec
le contexte généré. `app_version` est envoyé si configuré et omis sinon, sans inventer son
caractère obligatoire. Les DTO WebSocket utilisent `request_id` et l’omettent s’il est nul.
Une permission absente vaut faux selon `#opening-a-session` ; la permission `camera` est
représentée, tandis que `parental`, explicitement obsolète, reste exclue.

## Conflits compatibles avec des unions typées

L’intégrateur a accepté la stratégie des unions bornées et de la présence explicite.
Deux formes attestées peuvent être représentées sans abandonner le typage. La forme filaire doit rester accessible et être conservée
à la réécriture. Une vue de commodité ne remplace pas cette représentation.

| Contrat | Représentation proposée | Limite |
| --- | --- | --- |
| Valeurs Home | `Null`, `Boolean`, `Integer`, `Float`, `String` | Le `null` d’une action `void` ne prouve pas qu’un patch quelconque accepte `null`. |
| `LanHost.l2ident` | Objet `LanHostL2Ident` ou tableau typé | Une vue liste est distincte du kind reçu. |
| Downloads, état Wi-Fi, détail de notification | Objet documenté ou tableau du même objet | Une commodité singulière rejette les tableaux de cardinalité différente de un. |
| Type de table disque, code pays, identifiants PVR | Chaîne ou entier, type nommé par domaine | Conserver chaîne vide, identifiants opaques et zéros initiaux. |
| `DownloadPeer.requests` | Tableau d’entiers ou `EmptyObject` | `{}` ne prouve aucune map non vide ni son schéma. |
| `VPNIPSecConfig.auth_modes` | Tableau typé ou map des modes documentés | Seul `psk` est explicitement annoncé ; aucune sémantique inventée. |
| Capacités Wi-Fi, statistiques Media, agrégation LTE | Variantes propres au domaine | L’existence de deux formes ne fournit pas les champs internes absents. |

Des noms contradictoires tels que `id`/`feed_id`, `IPv4`/`ipv4`, `icon`/`url` et
`type`/`mode` nécessitent des membres distincts ou un discriminant du nom reçu. Un lecteur
qui fusionne silencieusement ces clés puis réécrit une clé différente perd une information
du protocole. Les réponses doivent conserver les tokens d’enum inconnus, avec
classification facultative. Les unités restent propres au champ ; aucun convertisseur
global de timestamps n’est justifié. Les chemins encodés ou bruts doivent rester explicites.

## Inconnues minimales avant un SDK entièrement typé

- **Définitions absentes** : `HomeNodeLink`, élément de `LteRadio.bands`, type scalaire de
  `PlayerStatusForegroundApp.package_id`. Un nom voisin ou un tableau vide ne suffit pas.
- **Liste insuffisamment définie** : `VpnClientIpInfo.routes` ne fournit pas le type de ses
  éléments. Une union tableau/objet vide ne résout pas ce manque.
- **Noms non établis** : la déclaration anonyme `label name` de `HomeNodeType` et
  `sources[]` de `DlBlockListConfig` demandent une décision documentée sur le nom filaire.
- **Mutations ambiguës** : mise à jour Profile avec un identifiant concret dans la
  signature, faute `network_controlr` et signature MLO recopiée de la configuration Wi-Fi.
  Ne pas généraliser un identifiant littéral ni essayer des routes de mutation alternatives.
- **Encodages ou résultats absents** : paramètres du GET RRD, collection de tous les
  NetworkControl et forme de l’identifiant de tâche créé/redimensionné par VM. Une union
  primitive/objet n’est pas justifiée lorsque aucune de ces formes n’est montrée.
- **Sections sans contrat opérable** : Slowness, découverte de candidats WPS et création
  OpenVPN spécifique. Consigner ces lacunes ; elles n’autorisent aucune route fictive.

Les permissions locales non décrites, les unités absentes et les résultats non nécessaires
ne doivent pas créer des centaines de blocages artificiels. L’authentification commune,
des entiers bruts, des commandes de succès sans résultat sous politique explicite, ou un
flux possédé pour un téléchargement de format non défini peuvent être des limites propres
et honnêtes. Ils ne prouvent pas une forme JSON ou un effet serveur absent de la source.

## Architecture proposée

Construire d’abord un schéma normalisé revu : opération, chemin, corps, résultat, accès,
présence, nullabilité, unités, preuves et errata acceptés. Valider les références et la
couverture avant toute émission. Une génération déterministe hors ligne des contrats C#
peut s’appuyer sur le Source Generator JSON natif de .NET et des `JsonTypeInfo<T>` explicites.
Un générateur Roslyn personnalisé n’est nécessaire que si la génération des façades ou
métadonnées apporte un bénéfice concret supplémentaire.

Séparer DTO de lecture, création, modification et action. Un `Optional<T>` garde `IsSet`
indépendant de la valeur : omission, `false` et `0` sont différents. Les propriétés non
définies sont omises par le writer contenant ou par une métadonnée `WhenWritingDefault`
appropriée ; le convertisseur de valeur seul ne peut pas omettre la propriété. Le `null`
explicite reste limité aux champs de requête où il est attesté. Aucun PUT générique ne
doit promettre merge, remplacement ou effacement par `null` sans preuve locale.

Les façades et sélecteurs de ressources immuables partagent un transport et un coordinateur
de découverte/session par origine et application. `Microsoft.Extensions.Http` et
`IHttpClientBuilder` portent la composition DI et la durée de vie des handlers. Les
builders de commandes sont propres à une opération et produisent des snapshots typés.
Les variantes JSON bornées utilisent des converters typés compatibles AOT ; ni reflection,
ni `dynamic`, ni `object` ne remplace un contrat non résolu.

Les erreurs d’authentification invalident la session pour l’opération explicite suivante.
Aucune mutation n’est rejouée automatiquement et aucune variante de route n’est tentée.
Les flux possèdent leur réponse HTTP et leur durée de vie d’annulation. Le protocole
WebSocket d’upload exige ses états et framing documentés. Les quatre événements attestés
ne deviennent pas un inventaire de notifications Home supposées.

Avant une génération complète, valider les roundtrips des unions, les noms exacts,
l’omission/`false`/`0`/`null`, puis les parcours JSON, binaire et WebSocket représentatifs
en NativeAOT. Les builds et tests restent pilotés par l’intégrateur ; cette revue n’en a
pas relancé. Les deux seuls fichiers écrits par ce reviewer sont cette note et son JSON.
