# Revue System / Home / Profile — snapshot API 16.0

Source complète vérifiée : `/workspace/.cloud-setup/freebox-upload-20261002/Freebox-Server-API-16.0/sources/raw/embedded/doc/index.html`. SHA-256 `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

Les sept sections système, Home API et Special Tiles, et Profile ont été lues entièrement. La version 16.0 est celle du `api_version` fourni avec cet instantané. Les signatures v8/v11/v16 conservées dans le HTML sont des preuves documentaires, pas une demande d’implémenter des versions historiques.

**44 opérations sémantiques** (42 couples méthode/chemin littéral), **27 modèles**, 161 déclarations de champs, 20 domaines enum et 10 champs présents seulement dans les exemples. Les trois POST pairing partagent un chemin mais ont les variantes `start`, `next`, `stop`. Les GET ledstrip planning et standby config existent dans les exemples HTTP malgré leur absence dans les signatures Sphinx. Aucun endpoint Slowness n’est défini.

**Verdict : `blocked_evidence` pour la génération exhaustive ; revue du périmètre terminée.** Le JSON compagnon détaille chaque contrat et conserve les inconnues. Aucun code applicatif ni appel Freebox opérationnel n’a été effectué.

## Décisions de consolidation

- Exclure uniquement `SystemConfigV5` et l’ancienne variante System <v5. Conserver la réponse moderne `SystemConfig` malgré le même chemin/identifiant HTML dans les deux branches.
- Dissocier `discovery.box_model = fbxgw9-r1` de `SystemModelInfo.name = fbxgw9-r1/full`. Un enum fermé partagé ferait échouer la découverte sur les données réellement fournies ; le socle corrigé conserve maintenant la chaîne brute.
- Corriger les doublons avec preuves et décision explicite : `SystemConfig.expansions` des capteurs → `sensors` dans l’exemple courant ; second `HomeTileData.value` décrivant l’historique → `history` dans l’exemple. Ne jamais générer deux propriétés JSON de même nom.
- Définir `HomeIoValue` comme union typée null/bool/int/float/string, avec convertisseur statique AOT. Les propriétés déclarées String portent effectivement bool/int/null dans les exemples. Les commandes `void`/bouton envoient explicitement `value:null` ; absence et null restent distincts.
- Séparer réponses et commandes : renommage HomeNode, statut adaptateur, pairing, patch LCD/plannings, override profil. Les champs Read-only n’entrent pas automatiquement dans les DTO de mutation, même quand un exemple les copie.
- Conserver les tokens enum inconnus avec une représentation typée. La source contredit ses propres tables pour `suspend`, `auto_up_to_date`, `void`, les états de l’alarme et les animations disponibles dynamiquement.

## Points bloquants ou incomplets

| Point | Preuve / conséquence |
| --- | --- |
| SlownessContracts | Slowness section has only an empty Get the last result of a given host heading and an error table; no method/path/request/response. No diagnostics endpoint may be invented. |
| SystemSensorsName | Thermal sensors are named expansions in declaration while current example separates sensors and expansion modules. Proposed erratum sensors from official example; cannot emit duplicate expansions JSON properties. |
| LcdLedAlias | Declared hide_status_led versus examples hide_led; alias/version relationship absent. Keep examples/declarations separate; do not send undocumented alias. |
| LedAnimationValues | Enum values are not provided; availability array gives dynamic supported animations. Use a named token + known supported list, not fabricated enum members. |
| PlanningRouteSlash | Ledstrip and standby PUT signature/example disagree about final slash. Retain both exact forms; conservative no-redirect client requires a reviewed route choice. |
| PlanningReadonlyResolution | Planning PUT examples send resolution though it is declared Read-only (same standby). Request DTO must separate read/write fields; accept an erratum before sending resolution. |
| PlanningTimestampUnit | timestamp type with millisecond-sized example; no explicit unit in domain text. Shared protocol owner must resolve units; do not assume seconds because NetworkControl timestamps explicitly are seconds. |
| StandbyModeSuspend | Enum table wifi_off/standby; config GET/PUT examples use suspend. Retain typed unknown mode and avoid silently translating suspend. |
| UpdateStateVariants | State table and GET example auto_up_to_date disagree; upgrade_failed appears only in relevance prose. Closed enum would fail official example; preserve additional wire tokens until confirmed. |
| UpdateResponseType | GET says Upgrade status object but example/top-level state matches UpdateStatus; nested upgrade_state conditional relevance. Model top-level status and optional nested UpgradeState separately, record erratum. |
| AdapterType | AdapterType has no definition; examples contain object name. Typed HomeAdapterType with documented-example name possible; other fields remain open/undefined. |
| HomeNodeLink | HomeNodeLink is referenced by signal_links/slot_links but never defined in the corpus. Blocks exact fully typed HomeNode links; no object/JsonElement stand-in or guessed fields. |
| HomeNodeTypeName | Anonymous property literally label name duplicates display label. Technical wire name/type needs confirmation; no duplicate property generated. |
| HomeHistoryName | Anonymous value property actually describes history; example has history. Accept documented-example erratum history and retain proof. Timestamp unit still unspecified. |
| PairingSessionIdTypes | Next example quotes session/pageid while model/stop example use integers, and JSON example contains invalid punctuation. Named IDs plus confirmed numeric/string serialization or deliberate documented tolerance; do not copy invalid JSON. |
| PairingProgressText | String property versus progress widget int percentage table. Choose a typed string/int union only after explicit evidence policy; no generic object. |
| HomeEndpointScalarTypes | String declaration contradicts native JSON scalar examples and UI value contract. Use HomeIoValue typed null/bool/int/float/string union; distinguish explicit null and absence. |
| HomeTileValueTypes | Value type enum table omits void but official example uses it; alarm-control special tile also uses enum. Shared value-type token must represent documented variants; discriminate values from JSON token/metadata. |
| HomeUiFields | Access exists in examples only; display icon text refers icon_ranges while actual property is range. Retain separate example evidence; do not fabricate missing icon_ranges field. |
| HomeBulbState | Bulb state slot typed void in special tile table, prose true=on. Conflict prevents exact write type for specialized light state command; generic union can preserve actual scalar. |
| HomeAlarmEnum | Alarm state value type enum absent from generic endpoint/tile value_type tables. Typed AlarmState values exist; confirm on-wire enum representation before specialized command converter. |
| ProfileIconUrl | Definition icon versus all examples url. Choose accepted documentation erratum for request field, do not send both automatically. |
| ProfileUpdateRoute | Literal PUT /profile/3 and example PUT /profile conflict; no documented generic id route. Blocks general UpdateProfile route implementation absent accepted erratum/new evidence. |
| NetworkControlsListShape | List-all has empty description/no response example. No array or keyed-map result shape may be assumed solely from heading. |
| NetworkHosts | Declared LanHost[] versus string-name arrays in GET/PUT examples. Coordinate LAN owner; typed variant or authoritative correction required. |
| NetworkControlPost | Field says writable with POST adding network control but no such endpoint exists in section. Do not invent POST /network_control. |
| RuleCreatePath | Source says network_controlr whereas all other rules/control paths say network_control. Blocks route; no silent typo correction. |
| RuleTimeTypes | start_time/end_time have no declared type though prose specifies seconds/slot arithmetic. Choose reviewed integer seconds type/width and record absence of declaration; range/day wrap not documented. |
| RuleMutationShapes | Rule create/update/delete has no response envelope/result and no concrete request encoding/requiredness examples. Do not assume generic CRUD response returns model or ack; common protocol plus evidence needed. |
| DomainPermissions | Shared profile/settings permission descriptions do not enumerate per-endpoint requirements; Home permission absent. No assumption of anonymous read or permission name home. |
| PresenceNullability | Most properties do not specify requiredness/nullability; examples omit fields on models/capabilities. Generated contracts must retain not_documented and avoid required=true or nullable=false inferred from examples. |

## Opérations retenues ou encore bloquées

| Opération | Méthode / chemin source | Résultat |
| --- | --- | --- |
| GetLanguage | `GET /api/v8/lang/` | LanguageSupport |
| SetLanguage | `POST /api/v8/lang/` | ack sans result |
| GetLcd | `GET /api/v8/lcd/config/` | LcdConfig |
| UpdateLcd | `PUT /api/v8/lcd/config/` | LcdConfig |
| GetLedstripStatus | `GET /api/v16/ledstrip/status` | LedstripStatus |
| GetLedstripPlanning | `GET /api/v16/ledstrip/planning/` | LedstripPlanning |
| UpdateLedstripPlanning | `PUT /api/v16/ledstrip/planning` | LedstripPlanning |
| GetStandbyStatus | `GET /api/v11/standby/status` | StandbyStatus |
| GetStandbyConfig | `GET /api/v11/standby/config/` | StandbyConfig |
| UpdateStandbyConfig | `PUT /api/v11/standby/config` | StandbyConfig |
| GetSystem | `GET /api/v8/system/` | SystemConfig |
| RebootSystem | `POST /api/v8/system/reboot/` | ack sans result |
| ShutdownSystem | `POST /api/v11/system/shutdown/` | ack sans result |
| GetFirmwareUpdate | `GET /api/v11/update/` | UpdateStatus |
| ListHomeAdapters | `GET /api/v8/home/adapters` | HomeAdapter[] |
| GetHomeAdapter | `GET /api/v8/home/adapters/{id}` | HomeAdapter |
| SetHomeAdapterStatus | `PUT /api/v8/home/adapters/{id}` | ack sans result |
| StartHomePairing | `POST /api/v8/home/pairing/{adapter_id}` | ack sans result |
| GetHomePairingStep | `GET /api/v8/home/pairing/{adapter_id}` | HomePairingStep |
| AdvanceHomePairing | `POST /api/v8/home/pairing/{adapter_id}` | HomePairingStep |
| StopHomePairing | `POST /api/v8/home/pairing/{adapter_id}` | ack sans result |
| ListHomeNodes | `GET /api/v8/home/nodes` | HomeNode[] |
| GetHomeNode | `GET /api/v8/home/nodes/{id}` | HomeNode |
| RenameHomeNode | `PUT /api/v8/home/nodes/{id}` | ack sans result |
| RemoveHomeNode | `DELETE /api/v8/home/nodes/{id}` | ack sans result |
| GetHomeEndpointValue | `GET /api/v8/home/endpoints/{node_id}/{endpoint_id}` | HomeNodeEndpointValue |
| SetHomeEndpointValue | `PUT /api/v8/home/endpoints/{node_id}/{endpoint_id}` | ack sans result |
| ListHomeTiles | `GET /api/v8/home/tileset/all` | HomeTile[] |
| GetHomeSubTileset | `GET /api/v8/home/tileset/{node_id}` | HomeTile[] |
| ListProfiles | `GET /api/v8/profile` | Profile[] |
| GetProfile | `GET /api/v8/profile/{id}` | Profile |
| CreateProfile | `POST /api/v8/profile/` | ProfileCreated (id) |
| DeleteProfile | `DELETE /api/v8/profile/{id}` | ack sans result |
| UpdateProfile | `PUT /api/v8/profile/3` | Profile |
| ListNetworkControls | `GET /api/v8/network_control` | unknown (heading all profiles, no result shape) |
| GetNetworkControl | `GET /api/v8/network_control/{profile_id}` | NetworkControl |
| UpdateNetworkControl | `PUT /api/v8/network_control/{profile_id}` | NetworkControl |
| GetDefaultModeMigration | `GET /api/v8/network_control/migrate` | DefaultModeMigrationStatus |
| MigrateDefaultMode | `POST /api/v8/network_control/migrate` | DefaultModeMigrationStatus |
| ListNetworkControlRules | `GET /api/v8/network_control/{profile_id}/rules` | NetworkControlRule[] |
| GetNetworkControlRule | `GET /api/v8/network_control/{profile_id}/rules/{rule_id}` | NetworkControlRule |
| CreateNetworkControlRule | `POST /api/v8/network_controlr/{profile_id}/rules/` | unknown |
| UpdateNetworkControlRule | `PUT /api/v8/network_control/{id}/rules/{rule_id}` | unknown |
| DeleteNetworkControlRule | `DELETE /api/v8/network_control/{id}/rules/{rule_id}` | unknown |

## Architecture forte, DI et Native AOT

La normalisation doit précéder la génération. Un schéma revu possède les variantes de requête, les champs de lecture/écriture, présence/nullabilité/unités, types liés, enums et preuves de version. La génération déterministe de DTO/facades à partir de ce schéma réduit la répétition ; elle ne décide pas des erreurs du HTML. Le générateur System.Text.Json intégré produit les métadonnées AOT. Un générateur Roslyn personnalisé n’est pas nécessaire pour l’union HomeIoValue ni pour la DI.

Les sélecteurs fluents restent immuables et les commandes déclenchent l’I/O seulement via une méthode Async terminale. Le transport commun reste propriétaire des enveloppes, erreurs, délais, tokens et chemins. La session est liée à une configuration de Freebox et partagée par les domaines ; des registrations nommées/keyed doivent isoler cet état si le SDK gère plusieurs appareils.

La commande pairing Next conserve session, pageid et ordre des champs. Le serveur fournit son délai de rafraîchissement en millisecondes ; aucune mutation n’est rejouée automatiquement. Les heures NetworkControl sont explicitement en secondes UNIX ; les timestamps planning et historiques Home requièrent encore la convention commune et ne doivent pas être confondus avec ces secondes.

Le tableau WebSocket documente quatre événements VM/LAN, attribués au propriétaire protocole. Aucun événement Home n’a été ajouté. `LanHostNetworkControl.current_mode` utilise les tokens `allowed`, `denied`, `webonly` ; webonly est legacy/déconseillé, sans marque explicite deprecated dans cette section.

Validation effectuée : empreinte de la source originale, présence des ancres, relecture des déclarations/exemples contradictoires et validité JSON du livrable. Les validations C# HTTP/AOT proposées dans le JSON restent à effectuer après consolidation.

## Rapprochement du catalogue

`catalogue_traceability` dans le JSON rapproche **42/42 occurrences de signatures** du
catalogue avec les identifiants et sources canoniques des opérations revues. Chaque
rapprochement a été vérifié par méthode/chemin dans la portée HTML canonique. Les trois
POST pairing emploient les sections Start/Next/Stop pour désambiguïser leur identifiant
HTML dupliqué. Le GET System moderne reste dans la section courante, distincte de
SystemConfigV5 exclu. Les alias d’ancres ne sont pas des opérations supplémentaires.

Les **cinq propriétés sans ancre** sont rapprochées de leur modèle, signature brute
exacte et indice de champ revu : expansions/SystemConfig, label name/HomeNodeType,
value historique/HomeTileData, start_time et end_time/NetworkControlRule. Le rapprochement
préserve leurs inconnues de nom/type. Les GET ledstrip planning et standby config restent
deux opérations supplémentaires issues uniquement des exemples HTTP. Ces contrôles
établissent la couverture des déclarations du périmètre, sans approuver les contrats
contradictoires ni prétendre à une couverture d’implémentation.
