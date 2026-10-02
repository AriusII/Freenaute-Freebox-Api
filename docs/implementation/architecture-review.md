# Revue de l’architecture intégrée : primitives, réseau et transports

Revue du 2 octobre 2026, limitée aux primitives partagées, aux contrats et façades réseau,
ainsi qu’aux transports HTTP binaire et WebSocket. La cible documentaire est l’instantané
**API 16.0** fourni par l’utilisateur. La source brute est
[`sources/raw/embedded/doc/index.html`](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html),
SHA-256 `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.
La provenance distante et le comportement d’une Freebox réelle n’ont pas été vérifiés par
cette revue. Les décisions transversales restent dans [sdk-contracts.md](../sdk-contracts.md)
et les lacunes du corpus dans la
[revue de préparation](../freebox-official/reviews/architecture-readiness.md).

## Défauts identifiés et corrections relues

Trois défauts concrets ont été transmis aux propriétaires et leurs corrections ont été
relues dans les fichiers intégrés. Aucun fichier réseau ou de transport n’a été modifié
par le reviewer.

| Défaut | Preuve et effet avant correction | Correction relue |
| --- | --- | --- |
| JSON WebSocket indenté rejeté | `FreeboxWebSocketConnection.ReceiveJsonAsync` divisait tout message reçu à chaque retour à la ligne. Un objet valide tel que `{\n"success":true\n}` échouait à la première ligne. [ws-api](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#ws-api) documente des objets JSON UTF-8 ; [ws-event-api](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#ws-event-api) précise seulement pour le canal d’événements « json, one per line ». | Le récepteur assemble les fragments, essaie d’abord l’objet JSON complet, puis accepte plusieurs objets séparés par des lignes. Le fallback valide toutes les lignes avant de les ajouter à la queue. Une erreur JSON ou UTF-8 vide la queue et ferme le socket. |
| Session invalide conservée après handshake HTTP 403 | Le catch WebSocket ne traitait que HTTP 401. [authentication-errors](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#authentication-errors) impose aussi HTTP 403 pour `auth_required`. Les connexions suivantes réutilisaient donc le token rejeté. | `CollectHttpResponseDetails` est activé. HTTP 401 ou 403 invalide atomiquement la session utilisée, via le statut du socket ou celui de l’exception interne. La connexion est libérée ; le handshake échoué n’est pas rejoué. |
| Kind booléen inventé pour `gcmp256` | [WifiBssConfig.gcmp256](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#WifiBssConfig.gcmp256) déclare `str`. Le brut ne contient aucun exemple JSON de ce champ. La description « Whether or not » ne suffit pas à justifier une sérialisation booléenne. | `WifiBssConfig.Gcmp256` est `string?`, le patch est `Optional<string>` avec son converter fermé. Le manifeste réseau et sa documentation décrivent cette limite. |

Le handshake HTTP 403 ne fournit pas le corps de l’enveloppe API à travers
`ClientWebSocket`. Le client ne peut donc pas distinguer `auth_required` et
`insufficient_rights` à cet endroit. L’invalidation sur 403 est une décision conservatrice
de gestion de session, sans mutation ou reconnexion implicite. L’erreur reste visible
au consommateur.

## Primitives : présence, formes filaires et AOT

[`Optional<T>`](../../Libs/Freenaute.Freebox.Mapper/Contracts/Primitives/Optional.cs)
possède trois états distincts : `Unset`, `Null` et `Value`. Le `default` est `Unset` ;
`FromValue(false)` et `FromValue(0)` sont des valeurs présentes. Les propriétés de patch
utilisent `JsonIgnoreCondition.WhenWritingDefault`, ce qui compare le wrapper complet :
elles omettent `Unset`, conservent `false` et `0`, et permettent de représenter un `null`
explicite. La possibilité technique de représenter `null` n’autorise pas son envoi à une
opération dont le contrat ne le documente pas. Les validations réseau le rejettent avant
les appels HTTP, y compris dans les patches imbriqués et tableaux d’options.

Le converter rejette un `Unset` sérialisé seul : l’omission relève de la propriété qui le
contient. Il traite `null` explicitement et emploie un `JsonTypeInfo<T>` fourni ou obtenu
auprès du resolver généré du contexte. Il ne construit aucun type générique à l’exécution,
ne possède pas de factory réflexive et n’utilise pas `object` ou `dynamic`. Les types
internes des converters doivent être enregistrés explicitement ; un converter de propriété
ne garantit pas leur génération automatique. La vérification des contrats réseau a trouvé
**14 types internes distincts** d’`Optional<T>`, tous déclarés dans
[`NetworkJsonSerializerContext`](../../Libs/Freenaute.Freebox.Mapper/Serialization/NetworkJsonSerializerContext.cs).
Tous les membres `Optional<T>` examinés possèdent la condition d’omission et le converter
fermé correspondant.

Une vérification statique supplémentaire des autres contextes intégrés a retrouvé les
mêmes garanties d’enregistrement explicite et d’omission pour **23 types internes** dans
Services, **17** dans Files et **10** dans SystemHome. Les collections immuables de
SystemHome sont notamment enregistrées sous leurs types fermés. Cette vérification porte
sur les métadonnées nécessaires à `Optional<T>`, sans constituer une revue sémantique
complète de ces trois domaines ni une preuve de leur publication AOT intégrée.

Les unions partagées conservent la forme reçue :

- `StringOrInteger` distingue une chaîne, dont les zéros initiaux et la chaîne vide sont
  conservés, d’un entier signé 64 bits. Les nombres fractionnaires, booléens, objets et
  états non initialisés sont rejetés.
- `ObjectOrArray<T>` distingue l’objet du tableau d’objets. Sa vue `Items` est en lecture
  seule et copie la collection fournie ; `GetSingle()` exige exactement un élément sans
  modifier le kind. Le converter rejette les éléments nuls et les formes scalaires. En
  écriture, il contrôle aussi la forme produite par un converter personnalisé de `T`.
- `EmptyObjectOrInt32Array` distingue `{}` de `[]`. Il rejette tout objet non vide et tout
  élément qui n’est pas un entier signé 32 bits ; `{}` ne devient pas une map inventée.
- `EncodedFreeboxPath` conserve le Base64 reçu exactement, puis expose séparément son
  décodage et son encodage de segment URI. `FromUtf8Path` utilise UTF-8 strict, sans
  normalisation Unicode. Un chemin reçu contenant des octets non UTF-8 peut rester opaque ;
  seul son décodage textuel strict échoue. Une valeur non initialisée est distincte d’une
  chaîne Base64 vide initialisée.

Les tests durables sont
[`PrimitiveContractTests.cs`](../../Tests/Freenaute.Freebox.Client.Tests/PrimitiveContractTests.cs)
et son contexte généré. **39 cas ont passé** dans un projet isolé .NET 10/C# 14,
réflexion JSON désactivée. Un exécutable isolé publié en Native AOT `linux-x64` a également
passé les parcours omission/`false`/`0`/`null`, modèle et tableau imbriqués, objet/tableau,
unions scalaires, objet vide/tableau d’entiers et chemins Unicode. Cette publication
utilisait les mêmes fichiers de primitives et le contexte généré ; elle a terminé sans
avertissement d’analyse AOT. Cette preuve ne remplace pas la publication AOT finale du
SDK intégré et de ses domaines.

## Contrats réseau confrontés au brut

| Contrat sensible | Source et décision constatée |
| --- | --- |
| `WifiBss.id` / `phy_id` | [id](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#WifiBss.id) déclare un entier, les exemples présentent une chaîne MAC. [phy_id](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#WifiBss.phy_id) déclare une chaîne, les exemples présentent un entier. Les deux propriétés utilisent `StringOrInteger?` et conservent leur kind. |
| `WifiBssConfig.hide_ssid` | [La déclaration](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#WifiBssConfig.hide_ssid) est `str`, les exemples BSS/defaults sont booléens. L’union réseau `StringOrBoolean` est donc bornée à deux formes attestées ; elle ne convertit pas une chaîne en booléen. `gcmp256` est traité séparément, comme décrit ci-dessus. |
| `LanHost.l2ident` | [La propriété](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#LanHost.l2ident) déclare un tableau de `LanHostL2Ident` ; les exemples de liste, détail et DHCP présentent un objet. `ObjectOrArray<LanHostL2Ident>?` préserve ces deux formes. Le modèle d’élément est explicitement présent dans le contexte généré. |
| Patches DHCP | [DhcpConfig](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#DhcpConfig) distingue les propriétés modifiables de `gateway` et `netmask`, en lecture seule. Le patch ne contient pas ces deux propriétés. `enabled=false`, les options imbriquées et leurs valeurs chaîne `"0"` ne sont pas omis. Les tableaux DNS/options sont typés. |
| LTE | [LteRadio.bands](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#LteRadio.bands) contient seulement `[ro]`, sans type d’élément. Le GET LTE complet reste déclaré non implémenté dans le manifeste réseau ; `LteRadioBand` voisin ne constitue pas une preuve du type de cette collection. Les propriétés documentées de `LteNetwork` et `LteRadioBand` gardent leurs types et unités brutes. |
| Dépréciations | [is_main_bss](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#WifiBssStatus.is_main_bss) et [use_default_config](../freebox-official/api-16.0/sources/raw/embedded/doc/index.html#WifiBssConfig.use_default_config) sont explicitement Deprecated et absents des DTO. `use_shared_params` demeure. Le changelog exclut l’ancien endpoint combinant agrégation et état LTE ; les endpoints séparés actuels ne sont pas exclus. Wi-Fi Planning n’est pas déclaré déprécié par la note « may be removed in the future ». |

Les réponses réseau gardent les tokens d’enum inconnus comme chaînes ; leurs constantes
connues constituent une aide de saisie. Les valeurs de lecture nullables représentent
des champs absents, sans élargir la nullabilité des requêtes. Les identifiants de ressources
sont encodés par segment avant leur insertion dans les routes. Le transport applique le
major découvert à la racine ; les signatures historiques dans les commentaires sont de
la provenance, pas un major figé dans les appels.

Le GET LTE et le PUT MLO restent des lacunes explicites de couverture. La revue ne valide
pas la totalité des 108 opérations réseau : elle vérifie les contrats sensibles ci-dessus,
les conventions communes de présence et les types internes requis par leurs converters.

## HTTP binaire et WebSocket : propriétaires et annulation

`DownloadAsync` emploie `ResponseHeadersRead` et transfère la réponse HTTP, son stream
et le CTS de durée de vie à `FreeboxDownload`. Le client ne libère pas la réponse avant
la consommation du fichier. Le propriétaire expose les métadonnées de contenu et libère
stream, réponse, enregistrement d’annulation et CTS lors de son disposal. Le timeout et
le token initial couvrent aussi les lectures après les headers : le wrapper lie le token
des lectures asynchrones au token de durée de vie et l’annulation ferme la réponse.
Les erreurs HTTP libèrent la réponse ; une enveloppe d’erreur lisible conserve son code
et son message. Un fichier JSON réussi reste un contenu binaire brut : il peut lui-même
contenir une clé `success:false` sans devenir une erreur du SDK.

Les tests existants `BinaryTransportTests` couvrent ces chemins : octets et métadonnées,
disposal explicite, erreurs HTTP avec et sans JSON, timeout durant le corps, annulation
après headers et transfert de propriété du contenu de formulaire. Ils ont été lus pendant
cette revue ; leur exécution intégrée relève de la validation finale de l’intégrateur.

Le handshake WebSocket utilise `ClientWebSocket.ConnectAsync(uri, httpClient, token)`.
Le `HttpClient` fourni conserve donc le handler DI, ses délégations et la configuration
TLS Freebox. `UriBuilder` change seulement le schéma `https` vers `wss` ou `http` vers
`ws` ; l’hôte, le chemin et le port restent issus de l’adresse API validée. Le test
`WebSocketUpgradeUsesConfiguredInvokerOriginAndSession` vérifie avec un handler de
fixture le port **8443**, l’origine, `/api/v16/ws/event` et `X-Fbx-App-Auth` ; il ne
constitue pas une preuve de handshake TLS réel.

`FreeboxWebSocketConnection` possède le socket. Une porte sérialise les envois et une
autre les réceptions ; les opérations lient leur annulation à la fermeture du propriétaire.
La réception assemble les fragments et copie les `JsonElement` avant de libérer les
documents. Le disposal annule les opérations, abort/dispose le socket et laisse les
portes accessibles aux blocs `finally` déjà en cours. Le timeout du handshake finit à
l’ouverture de la connexion ; chaque I/O ultérieure reçoit son propre token. La limite
client de **1 000 000 octets par message assemblé** est conservatrice par rapport à la
limite documentaire par frame et borne la mémoire de réception. Elle ne prétend pas
offrir une réception de messages arbitrairement grands sous fragmentation.

## Limites de cette preuve

Les trois corrections sont relues au niveau du code. Aucun nouveau build global n’a été
lancé par le reviewer pendant les écritures concurrentes ; compilation, tests intégrés,
manifestes de couverture et publication du sample Native AOT sont consolidés par le
propriétaire de l’intégration. Les inconnues documentaires restent enregistrées au lieu
d’être remplacées par des modèles inventés. Aucun appel d’API métier, aucune validation
TLS contre une Freebox et aucune garantie de dernière version matérielle ne sont déduits
des fixtures ou de l’instantané fourni.
