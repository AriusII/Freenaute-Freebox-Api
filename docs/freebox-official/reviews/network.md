# Revue réseau du corpus Server annoncé API 16.0

`task_id: network` · `verdict: blocked_evidence` · revue du 2 octobre 2026 · agent `http_client`.

Source brute : [http://mafreebox.freebox.fr/doc/index.html](http://mafreebox.freebox.fr/doc/index.html), SHA-256 `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`. L’empreinte des 1 816 775 octets a été recalculée. L’archive a été fournie hors ligne ; son manifeste indique une collecte embarquée en HTTP le `2026-10-02T17:19:23.212510+00:00`. Cette revue confirme les octets et les références, sans présenter cet upload comme une nouvelle acquisition HTTPS officielle.

Le [companion JSON](network.json) porte les 110 enregistrements d’opérations, les 73 objets formels et chacun des 454 champs bruts examinés. Deux champs dépréciés sont exclus ; trois occurrences de champs dupliqués sans ancre restent identifiées pour éviter de générer deux propriétés JSON identiques. Les 19 formes supplémentaires viennent de prose ou d’exemples et restent distinctes des objets formels. Les deux signatures regroupant AP/BSS ont été séparées en routes individuelles. Le doublon de signature MLO est un conflit conservé, pas un second PUT WiFi global validé.

## Périmètre et preuves

| Module | Objets | Occurrences de champs bruts | Signatures formelles | Opérations trouvées uniquement dans les exemples |
| --- | ---: | ---: | ---: | ---: |
| connection | 17 | 124 | 13 | 0 |
| dhcp | 4 | 29 | 8 | 0 |
| dhcpv6 | 1 | 3 | 2 | 0 |
| freeplug | 2 | 14 | 3 | 0 |
| igd | 2 | 12 | 4 | 0 |
| lan | 7 | 41 | 10 | 0 |
| nat | 3 | 23 | 10 | 0 |
| sfp | 2 | 13 | 2 | 1 |
| switch | 3 | 39 | 4 | 0 |
| wifi | 32 | 156 | 50 | 1 |

Les dix fiches dérivées `docs/reference/reseau/*.md` ont servi à la navigation. Les déclarations, tableaux, paragraphes, exemples et dépréciations ont été confrontés aux sections originales sous `document-api/<module>` dans `sources/raw/embedded/doc/index.html`. Les exemples Markdown déjà nettoyés ne servent pas de preuve de l’absence d’un ancien champ. Dix-neuf sections du changelog brut ont aussi été relues pour établir les ajouts et la portée précise des retraits. Toutes les ancres citées dans le JSON existent dans le même HTML brut.

## Versions, dépréciations et remplacements

Les préfixes `/api/v8`, `/v9`, `/v10`, `/v11`, `/v13`, `/v14` et `/v16` restent des signatures documentées. À l’exécution, seul le préfixe API est remplacé par le major découvert ; aucune branche de compatibilité avec les anciennes versions n’est proposée. Le numéro 16.0 est annoncé par le corpus fourni et ne prouve pas à lui seul la dernière publication accessible sur Internet.

| Élément | Décision | Preuve exacte |
| --- | --- | --- |
| WifiBssStatus.is_main_bss | Exclure seulement : this field only, including occurrences in examples | [WifiBssStatus.is_main_bss](http://mafreebox.freebox.fr/doc/index.html#WifiBssStatus.is_main_bss) |
| WifiBssConfig.use_default_config | Exclure seulement : this field only, including occurrences in examples | [WifiBssConfig.use_default_config](http://mafreebox.freebox.fr/doc/index.html#WifiBssConfig.use_default_config) |
| Former combined xDSL/4G aggregation and LTE connection API | Exclure seulement : former variant only; no exact removed path/id supplied, and no such current operation in this review | [deprecated-api-v10-0](http://mafreebox.freebox.fr/doc/index.html#deprecated-api-v10-0) |
| expected_phys in former WiFi global configuration | Exclure seulement : former configuration property only; absent from current WifiGlobalConfig schema | [wifi-state](http://mafreebox.freebox.fr/doc/index.html#wifi-state) |

`WifiBss.use_shared_params` remplace les deux anciens indicateurs BSS. Pour éditer une BSS, choisir comme source `bss_params` ou `shared_bss_params`, transmettre les modifications sous `config` et régler `use_shared_params` en conséquence. Les deux vues stockées sont `Read-only`.

La valeur AP `stopping` est explicitement documentée dans [le changement 12.0](http://mafreebox.freebox.fr/doc/index.html#api-changes-from-version-11-2-to-12-0). Elle est retenue malgré une ligne erronée répétant `starting` dans le tableau actuel. L’existence de `wps_enabled` et `wps_uuid` est aussi établie par [le changement 5.0](http://mafreebox.freebox.fr/doc/index.html#changed-api-v5-0), même si ces champs manquent dans la déclaration formelle actuelle.

Le `expected_phys` actuel appartient à `WifiGlobalState` et est conservé. `WifiPlanning` est remplacé de préférence par Standby pour de nouvelles interfaces, mais la note « may be removed in the future » ne le marque pas explicitement Deprecated. Les étiquettes UNSTABLE et les booléens réseau nommés `legacy` ne sont pas des dépréciations. Les chiffrements décrits « should not use » restent classifiés comme déconseillés, sans inventer une exclusion API.

## Règles de corps, types et état

- Aucun module réseau ne définit ici un flux binaire, multipart ou WebSocket. Les échanges examinés utilisent les requêtes JSON ou sans corps et les enveloppes JSON communes. Le transport existant suffit pour ces mécanismes ; aucun client HTTP ou état de session par domaine ne doit être créé.
- Les PUT montrent des modifications partielles, parfois imbriquées (`config.ht.ht_enabled`). Ils ne définissent pas une règle universelle de fusion, d’obligation ou de JSON `null`. Une commande doit distinguer absence, `false` et `0` ; ne pas utiliser `WhenWritingDefault` sur les mises à jour. Un `Optional<T>` ou une DTO d’update séparée doit suivre la politique acceptée globalement.
- Les accès `Read-only` et `Write-only` sont conservés exactement. Sans marque, l’accès reste `not_documented`. Les champs créés et les identifiants présents dans les exemples ne deviennent pas automatiquement obligatoires. `DhcpOption.id` est `Read-only` en modèle, mais sert aussi d’identifiant dans la liste d’options envoyée ; ne pas supprimer cet identifiant d’une requête d’options.
- `DhcpOption.val` reste une chaîne, même pour les formats booléens ou entiers. Le tableau brut définit les identifiants et les formats : IPv4, liste IPv4 séparée par virgules, ASCII, hex ASCII, bool `true/false/1/0`, entiers signés/non signés 8/16/32 bits. La forme JSON de la valeur reste string.
- Les compteurs entiers sans plage documentée proposent `long` signé ; conserver les sentinelles négatives, les octets et les taux avec leur unité locale. Les timestamps de survey WiFi sont illustrés sur 13 chiffres, ceux de LAN/WPS sur 10 chiffres : conserver leur entier brut sans convertisseur de date global supposant les secondes.
- `ConnectionStatus.ipv4_port_range` contient deux bornes inclusives. Les redirections doivent rester dans cette plage ; les limites d’accès distant et de ports entrants se réfèrent à cette même allocation. `IncomingPortConfig.readonly` interdit de modifier `in_port` lorsque vrai. Les ports entrants sont gérés par les services : aucune création ou suppression ; ils ont priorité sur une redirection NAT conflictuelle.
- `Route.prefix` utilise CIDR IPv4. Les sous-préfixes de `127.0.0.0/8`, `169.254.0.0/16`, `224.0.0.0/4` et `192.168.27.0/24` sont interdits ; un seul itinéraire activé par préfixe. Le PUT envoie un tableau. La sémantique exacte de remplacement/fusion n’est pas explicitement donnée.
- `LanHost.domain_name` accepte vide pour ne pas enregistrer de domaine. Sinon : suffixe `.home`, au plus 63 caractères, règles de lettres/chiffres/tirets/points indiquées dans la propriété. Appliquer la validation d’écriture sans rejeter les anciennes réponses illustrées qui ne respectent pas le suffixe. `LanHost.info` est un dictionnaire ouvert : `Dictionary<string, JsonElement>` est justifié pour ce champ précis.
- Le planning WiFi contient `7 * resolution` créneaux `on/off`, du lundi 00:00 au dernier créneau dimanche, avec durée `60*24/resolution` minutes. `resolution` reste en lecture seule.
- Un scan rend l’AP indisponible et la source demande confirmation dans le produit appelant ; attendre que l’état quitte `scanning` avant de relire le radar. Un restart peut couper la réception de sa propre réponse si cette carte porte la connexion. Ces mutations ne doivent pas être rejouées automatiquement.
- Une seule session WPS peut être active ; le BSS doit avoir WPS activé avec `wpa2_psk_ccmp` ou `wpa2_psk_auto`. Le démarrage retourne un entier de session ; l’arrêt est documenté par un exemple explicite `{session_id}`. Le modèle de candidat existe, mais aucune route de liste de candidats n’est fournie : ne pas l’inventer.
- Guest create envoie directement `WifiCustomKeyParams`, la réponse les range sous `params`. `max_use_count` vaut au maximum 127 ; zéro signifie illimité. `duration`/`remaining` s’expriment en secondes lorsque leur prose le dit ; `remaining=0` sur une clé signifie absence d’expiration. Une suppression coupe les stations connectées avec cette clé.
- Les configurations MLO utilisent `partners` : tableau vide désactive, seul AP local indique SLO, seules les combinaisons annoncées par `allowed_comb` peuvent être écrites. Ce dernier retourne un tableau de tableaux d’entiers. L’état opérationnel peut différer si les partenaires sont désactivés, sans EHT, avec paramètres non partagés ou sécurité incompatible.
- SFP nécessite `SystemModelInfo.has_lan_sfp` présent et vrai. Le texte d’introduction lie erronément `SystemConfig`. Les types forcés doivent venir des choix `available_sfp_types`, sans renommer les valeurs contradictoires.

La présence et la nullabilité sont indépendantes. Les propriétés conditionnelles (IPv4/IPv6 quand up, retransmissions xDSL selon phyr/ginp, mode de contrôle si profil, diagnostic AP/BSS) restent explicites dans les contraintes JSON. Aucune permission non mentionnée n’est transformée en accès public. Les permissions et la session sont une dépendance de la revue `protocol`.

## Opérations examinées

Les cellules indiquent les formes documentées ; elles ne constituent pas encore une autorisation de générer les lignes marquées par une inconnue. Les sources de chaque ligne, corps et exemple sont dans `network.json`. Les réponses absentes d’un exemple ne prouvent pas une règle de nullabilité.

### connection

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v11/connection/` | non fourni / requête sans corps | ConnectionStatus | [get--api-v11-connection-](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-) ; aucune contradiction spécifique relevée |
| `GET /api/v11/connection/config/` | non fourni / requête sans corps | ConnectionConfiguration | [get--api-v11-connection-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-config-) ; aucune contradiction spécifique relevée |
| `PUT /api/v11/connection/config/` | partial ConnectionConfiguration | ConnectionConfiguration | [put--api-v11-connection-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-config-) ; aucune contradiction spécifique relevée |
| `GET /api/v11/connection/ipv6/config/` | non fourni / requête sans corps | ConnectionIpv6Configuration | [get--api-v11-connection-ipv6-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-ipv6-config-) ; network-route-example-conflicts |
| `PUT /api/v11/connection/ipv6/config/` | partial ConnectionIpv6Configuration | ConnectionIpv6Configuration | [put--api-v11-connection-ipv6-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-ipv6-config-) ; network-route-example-conflicts |
| `GET /api/v11/connection/xdsl/` | non fourni / requête sans corps | XdslInfos | [get--api-v11-connection-xdsl-](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-xdsl-) ; aucune contradiction spécifique relevée |
| `GET /api/v11/connection/lte/{id}` | non fourni / requête sans corps | LteConfiguration | [get--api-v11-connection-lte-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-lte-id) ; lte-radio-bands-missing-wire-type |
| `GET /api/v11/connection/aggregation` | non fourni / requête sans corps | LteAggregationResult | [get--api-v11-connection-aggregation](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-aggregation) ; lte-aggregation-result-wrapper |
| `PUT /api/v11/connection/aggregation` | LteAggregationUpdate | non documenté / succès sans result illustré | [put--api-v11-connection-aggregation](http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-aggregation) ; lte-aggregation-result-wrapper |
| `GET /api/v11/connection/ftth/` | non fourni / requête sans corps | FtthStatus | [get--api-v11-connection-ftth-](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-ftth-) ; aucune contradiction spécifique relevée |
| `GET /api/v11/connection/ddns/{provider}/status/` | non fourni / requête sans corps | DDNSStatus | [get--api-v11-connection-ddns-provider-status-](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-ddns-provider-status-) ; aucune contradiction spécifique relevée |
| `GET /api/v11/connection/ddns/{provider}/` | non fourni / requête sans corps | DDNSConfig | [get--api-v11-connection-ddns-provider-](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-connection-ddns-provider-) ; aucune contradiction spécifique relevée |
| `PUT /api/v11/connection/ddns/{provider}/` | DDNSConfig write fields (password write_only) | DDNSConfig | [put--api-v11-connection-ddns-provider-](http://mafreebox.freebox.fr/doc/index.html#put--api-v11-connection-ddns-provider-) ; aucune contradiction spécifique relevée |
### dhcp

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v16/dhcp/config/` | non fourni / requête sans corps | DhcpConfig | [get--api-v16-dhcp-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-dhcp-config-) ; aucune contradiction spécifique relevée |
| `PUT /api/v16/dhcp/config/` | partial DhcpConfig | DhcpConfig | [put--api-v16-dhcp-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v16-dhcp-config-) ; aucune contradiction spécifique relevée |
| `GET /api/v16/dhcp/static_lease/` | non fourni / requête sans corps | DhcpStaticLease[] | [get--api-v16-dhcp-static_lease-](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-dhcp-static_lease-) ; lan-l2ident-object-versus-array |
| `GET /api/v16/dhcp/static_lease/{id}` | non fourni / requête sans corps | DhcpStaticLease | [get--api-v16-dhcp-static_lease-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-dhcp-static_lease-id) ; lan-l2ident-object-versus-array |
| `PUT /api/v16/dhcp/static_lease/{id}` | partial DhcpStaticLease | DhcpStaticLease | [put--api-v16-dhcp-static_lease-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v16-dhcp-static_lease-id) ; lan-l2ident-object-versus-array |
| `DELETE /api/v8/dhcp/static_lease/{id}` | non fourni / requête sans corps | non documenté / succès sans result illustré | [delete--api-v8-dhcp-static_lease-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-dhcp-static_lease-id) ; lan-l2ident-object-versus-array |
| `POST /api/v16/dhcp/static_lease/` | DhcpStaticLease create fields (example mac/ip; optional comment; options) | DhcpStaticLease | [post--api-v16-dhcp-static_lease-](http://mafreebox.freebox.fr/doc/index.html#post--api-v16-dhcp-static_lease-) ; lan-l2ident-object-versus-array |
| `GET /api/v16/dhcp/dynamic_lease/` | non fourni / requête sans corps | DhcpDynamicLease[] | [get--api-v16-dhcp-dynamic_lease-](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-dhcp-dynamic_lease-) ; lan-l2ident-object-versus-array |
### dhcpv6

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v8/dhcpv6/config/` | non fourni / requête sans corps | DHCPv6Config | [get--api-v8-dhcpv6-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-dhcpv6-config-) ; aucune contradiction spécifique relevée |
| `PUT /api/v8/dhcpv6/config/` | partial DHCPv6Config (dns read_only) | DHCPv6Config | [put--api-v8-dhcpv6-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-dhcpv6-config-) ; aucune contradiction spécifique relevée |
### freeplug

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v8/freeplug/` | non fourni / requête sans corps | FreeplugNetwork[] | [get--api-v8-freeplug-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-freeplug-) ; aucune contradiction spécifique relevée |
| `GET /api/v8/freeplug/{id}/` | non fourni / requête sans corps | Freeplug | [get--api-v8-freeplug-id-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-freeplug-id-) ; aucune contradiction spécifique relevée |
| `POST /api/v8/freeplug/{id}/reset/` | non fourni / requête sans corps | non documenté / succès sans result illustré | [post--api-v8-freeplug-id-reset-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-freeplug-id-reset-) ; aucune contradiction spécifique relevée |
### igd

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v8/upnpigd/config/` | non fourni / requête sans corps | UPnPIGDConfig | [get--api-v8-upnpigd-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upnpigd-config-) ; aucune contradiction spécifique relevée |
| `PUT /api/v8/upnpigd/config/` | partial UPnPIGDConfig | UPnPIGDConfig | [put--api-v8-upnpigd-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-upnpigd-config-) ; aucune contradiction spécifique relevée |
| `GET /api/v8/upnpigd/redir/` | non fourni / requête sans corps | UPnPRedir[] | [get--api-v8-upnpigd-redir-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-upnpigd-redir-) ; aucune contradiction spécifique relevée |
| `DELETE /api/v8/upnpigd/redir/{id}` | non fourni / requête sans corps | non documenté / succès sans result illustré | [delete--api-v8-upnpigd-redir-id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-upnpigd-redir-id) ; aucune contradiction spécifique relevée |
### lan

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v8/lan/config/` | non fourni / requête sans corps | LanConfig | [get--api-v8-lan-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-lan-config-) ; lan-config-type-versus-mode |
| `PUT /api/v8/lan/config/` | partial LanConfig (type/mode unresolved) | LanConfig | [put--api-v8-lan-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-lan-config-) ; lan-config-type-versus-mode |
| `GET /api/v16/lan/routes` | non fourni / requête sans corps | Route[] | [get--api-v16-lan-routes](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-lan-routes) ; network-route-example-conflicts |
| `PUT /api/v16/lan/routes/` | Route[] | Route[] | [put--api-v16-lan-routes-](http://mafreebox.freebox.fr/doc/index.html#put--api-v16-lan-routes-) ; lan-routes-array-update-semantics |
| `GET /api/v8/lan/browser/interfaces/` | non fourni / requête sans corps | LanBrowserInterface[] | [get--api-v8-lan-browser-interfaces-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-lan-browser-interfaces-) ; aucune contradiction spécifique relevée |
| `GET /api/v16/lan/browser/{interface}/` | non fourni / requête sans corps | LanHost[] | [get--api-v16-lan-browser-interface-](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-lan-browser-interface-) ; lan-l2ident-object-versus-array |
| `GET /api/v16/lan/browser/{interface}/{hostid}/` | non fourni / requête sans corps | LanHost | [get--api-v16-lan-browser-interface-hostid-](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-lan-browser-interface-hostid-) ; lan-l2ident-object-versus-array |
| `PUT /api/v16/lan/browser/{interface}/{hostid}/` | partial LanHost authored fields | LanHost | [put--api-v16-lan-browser-interface-hostid-](http://mafreebox.freebox.fr/doc/index.html#put--api-v16-lan-browser-interface-hostid-) ; lan-l2ident-object-versus-array |
| `GET /api/v8/lan/browser/types/` | non fourni / requête sans corps | LanHostTypeDescriptor[] | [get--api-v8-lan-browser-types-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-lan-browser-types-) ; aucune contradiction spécifique relevée |
| `POST /api/v8/lan/wol/{interface}/` | WakeOnLanRequest | non documenté / succès sans result illustré | [post--api-v8-lan-wol-interface-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-lan-wol-interface-) ; aucune contradiction spécifique relevée |
### nat

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v8/fw/dmz/` | non fourni / requête sans corps | DmzConfig | [get--api-v8-fw-dmz-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-fw-dmz-) ; network-route-example-conflicts |
| `PUT /api/v8/fw/dmz/` | partial DmzConfig | DmzConfig | [put--api-v8-fw-dmz-](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-fw-dmz-) ; network-route-example-conflicts |
| `GET /api/v8/fw/redir/` | non fourni / requête sans corps | PortForwardingConfig[] | [get--api-v8-fw-redir-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-fw-redir-) ; nat-wan-port-start-string-versus-number, lan-l2ident-object-versus-array |
| `GET /api/v8/fw/redir/{redir_id}` | non fourni / requête sans corps | PortForwardingConfig | [get--api-v8-fw-redir-redir_id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-fw-redir-redir_id) ; nat-wan-port-start-string-versus-number, lan-l2ident-object-versus-array |
| `PUT /api/v8/fw/redir/{redir_id}` | partial PortForwardingConfig | PortForwardingConfig | [put--api-v8-fw-redir-redir_id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-fw-redir-redir_id) ; nat-wan-port-start-string-versus-number, lan-l2ident-object-versus-array |
| `POST /api/v8/fw/redir/` | PortForwardingConfig create fields | PortForwardingConfig | [post--api-v8-fw-redir-](http://mafreebox.freebox.fr/doc/index.html#post--api-v8-fw-redir-) ; nat-wan-port-start-string-versus-number, lan-l2ident-object-versus-array |
| `DELETE /api/v8/fw/redir/{redir_id}` | non fourni / requête sans corps | non documenté / succès sans result illustré | [delete--api-v8-fw-redir-redir_id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v8-fw-redir-redir_id) ; nat-wan-port-start-string-versus-number, lan-l2ident-object-versus-array |
| `GET /api/v8/fw/incoming/` | non fourni / requête sans corps | IncomingPortConfig[] | [get--api-v8-fw-incoming-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-fw-incoming-) ; aucune contradiction spécifique relevée |
| `GET /api/v8/fw/incoming/{port_id}` | non fourni / requête sans corps | IncomingPortConfig | [get--api-v8-fw-incoming-port_id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-fw-incoming-port_id) ; network-route-example-conflicts |
| `PUT /api/v8/fw/incoming/{port_id}` | partial IncomingPortConfig enabled/in_port | IncomingPortConfig | [put--api-v8-fw-incoming-port_id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-fw-incoming-port_id) ; network-route-example-conflicts |
### sfp

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v11/sfp/status` | non fourni / requête sans corps | SfpStatus | [get--api-v11-sfp-status](http://mafreebox.freebox.fr/doc/index.html#get--api-v11-sfp-status) ; sfp-type-table-example-conflict |
| `PUT /api/v11/sfp/config` | partial SfpConfig | SfpConfig | [put--api-v11-sfp-config](http://mafreebox.freebox.fr/doc/index.html#put--api-v11-sfp-config) ; network-route-example-conflicts, sfp-type-table-example-conflict |
| `GET /api/v11/sfp/config/` | non fourni / requête sans corps | SfpConfig | [get-sfp-config](http://mafreebox.freebox.fr/doc/index.html#get-sfp-config) ; sfp-type-table-example-conflict |
### switch

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v8/switch/status/` | non fourni / requête sans corps | SwitchPortStatus[] | [get--api-v8-switch-status-](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-switch-status-) ; switch-mac-entry-name-versus-hostname |
| `GET /api/v8/switch/port/{id}` | non fourni / requête sans corps | SwitchPortConfig | [get--api-v8-switch-port-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-switch-port-id) ; switch-update-example-id-mismatch |
| `PUT /api/v8/switch/port/{id}` | partial SwitchPortConfig speed/duplex | SwitchPortConfig | [put--api-v8-switch-port-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v8-switch-port-id) ; switch-update-example-id-mismatch |
| `GET /api/v8/switch/port/{id}/stats` | non fourni / requête sans corps | SwitchPortStats | [get--api-v8-switch-port-id-stats](http://mafreebox.freebox.fr/doc/index.html#get--api-v8-switch-port-id-stats) ; aucune contradiction spécifique relevée |
### wifi

| Méthode et chemin source | Corps | Résultat | Preuve / limites |
| --- | --- | --- | --- |
| `GET /api/v9/wifi/config/` | non fourni / requête sans corps | WifiGlobalConfig | [get--api-v9-wifi-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-config-) ; aucune contradiction spécifique relevée |
| `PUT /api/v9/wifi/config/` | partial WifiGlobalConfig | WifiGlobalConfig | [put--api-v9-wifi-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-config-) ; aucune contradiction spécifique relevée |
| `GET /api/v16/wifi/steering/config/` | non fourni / requête sans corps | WifiSteeringConfig | [get--api-v16-wifi-steering-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v16-wifi-steering-config-) ; aucune contradiction spécifique relevée |
| `PUT /api/v16/wifi/steering/config/` | partial WifiSteeringConfig | WifiSteeringConfig | [put--api-v16-wifi-steering-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v16-wifi-steering-config-) ; aucune contradiction spécifique relevée |
| `GET /api/v10/wifi/state/` | non fourni / requête sans corps | WifiGlobalState[] (example) versus WifiGlobalState (prose) | [get--api-v10-wifi-state-](http://mafreebox.freebox.fr/doc/index.html#get--api-v10-wifi-state-) ; wifi-global-state-array-versus-object |
| `GET /api/v9/wifi/ap/` | non fourni / requête sans corps | WifiAp[] | [get--api-v9-wifi-ap-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-) ; wifi-capability-integer-versus-object-map, wifi-channel-width-number-versus-string |
| `GET /api/v9/wifi/ap/{id}` | non fourni / requête sans corps | WifiAp | [get--api-v9-wifi-ap-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id) ; wifi-capability-integer-versus-object-map, wifi-channel-width-number-versus-string |
| `PUT /api/v9/wifi/ap/{id}` | {config: partial WifiApConfig, including partial nested ht/he} | WifiAp | [put--api-v9-wifi-ap-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-ap-id) ; wifi-capability-integer-versus-object-map, wifi-channel-width-number-versus-string |
| `GET /api/v9/wifi/ap/{id}/allowed_channel_comb` | non fourni / requête sans corps | WifiAllowedComb[] | [get--api-v9-wifi-ap-id-allowed_channel_comb](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-allowed_channel_comb) ; aucune contradiction spécifique relevée |
| `GET /api/v9/wifi/ap/{id}/stations/` | non fourni / requête sans corps | WifiStation[] | [get--api-v9-wifi-ap-id-stations-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-stations-) ; lan-l2ident-object-versus-array |
| `GET /api/v9/wifi/ap/{id}/stations/{mac}` | non fourni / requête sans corps | WifiStation | [get--api-v9-wifi-ap-id-stations-mac](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-stations-mac) ; lan-l2ident-object-versus-array |
| `GET /api/v9/wifi/ap/{id}/channel_survey_history/{timestamp}` | non fourni / requête sans corps | WifiApChannelSurveyData[] | [get--api-v9-wifi-ap-id-channel_survey_history-timestamp](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-channel_survey_history-timestamp) ; aucune contradiction spécifique relevée |
| `POST /api/v9/wifi/ap/{id}/restart` | non fourni / requête sans corps | non documenté / succès sans result illustré | [post--api-v9-wifi-ap-id-restart](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-ap-id-restart) ; aucune contradiction spécifique relevée |
| `GET /api/v9/wifi/bss/` | non fourni / requête sans corps | WifiBss[] | [get--api-v9-wifi-bss-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-bss-) ; wifi-bss-identifier-types, wifi-hide-ssid-and-gcmp-types, wifi-wps-fields-absent-from-schema |
| `GET /api/v9/wifi/bss/{id}` | non fourni / requête sans corps | WifiBss | [get--api-v9-wifi-bss-id](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-bss-id) ; wifi-bss-identifier-types, wifi-hide-ssid-and-gcmp-types, wifi-wps-fields-absent-from-schema, network-route-example-conflicts |
| `PUT /api/v9/wifi/bss/{id}` | {config: partial WifiBssConfig,use_shared_params:bool when selected} | WifiBss | [put--api-v9-wifi-bss-id](http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-bss-id) ; wifi-bss-identifier-types, wifi-hide-ssid-and-gcmp-types, wifi-wps-fields-absent-from-schema, network-route-example-conflicts |
| `GET /api/v9/wifi/ap/{id}/neighbors/` | non fourni / requête sans corps | WifiNeighbor[] | [get--api-v9-wifi-ap-id-neighbors-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-neighbors-) ; wifi-channel-width-number-versus-string |
| `GET /api/v9/wifi/ap/{id}/channel_usage/` | non fourni / requête sans corps | WifiChannelUsage[] | [get--api-v9-wifi-ap-id-channel_usage-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-channel_usage-) ; aucune contradiction spécifique relevée |
| `POST /api/v9/wifi/ap/{id}/neighbors/scan` | non fourni / requête sans corps | non documenté / succès sans result illustré | [post--api-v9-wifi-ap-id-neighbors-scan](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-ap-id-neighbors-scan) ; aucune contradiction spécifique relevée |
| `GET /api/v9/wifi/planning/` | non fourni / requête sans corps | WifiPlanning | [get--api-v9-wifi-planning-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-planning-) ; aucune contradiction spécifique relevée |
| `PUT /api/v9/wifi/planning/` | partial WifiPlanning | WifiPlanning | [put--api-v9-wifi-planning-](http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-planning-) ; aucune contradiction spécifique relevée |
| `GET /api/v9/wifi/mac_filter/` | non fourni / requête sans corps | WifiMacFilter[] | [get--api-v9-wifi-mac_filter-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-mac_filter-) ; network-route-example-conflicts, lan-l2ident-object-versus-array |
| `GET /api/v9/wifi/mac_filter/{filter_id}` | non fourni / requête sans corps | WifiMacFilter | [get--api-v9-wifi-mac_filter-filter_id](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-mac_filter-filter_id) ; network-route-example-conflicts, lan-l2ident-object-versus-array |
| `PUT /api/v9/wifi/mac_filter/{filter_id}` | partial WifiMacFilter comment/type | WifiMacFilter | [put--api-v9-wifi-mac_filter-filter_id](http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-mac_filter-filter_id) ; network-route-example-conflicts, lan-l2ident-object-versus-array |
| `DELETE /api/v9/wifi/mac_filter/{filter_id}` | non fourni / requête sans corps | non documenté / succès sans result illustré | [delete--api-v9-wifi-mac_filter-filter_id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v9-wifi-mac_filter-filter_id) ; network-route-example-conflicts, lan-l2ident-object-versus-array |
| `POST /api/v9/wifi/mac_filter/` | WifiMacFilter create mac/type/comment | WifiMacFilter | [post--api-v9-wifi-mac_filter-](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-mac_filter-) ; network-route-example-conflicts, lan-l2ident-object-versus-array |
| `POST /api/v9/wifi/config/reset/` | non fourni / requête sans corps | non documenté / succès sans result illustré | [post--api-v9-wifi-config-reset-](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-config-reset-) ; aucune contradiction spécifique relevée |
| `GET /api/v9/wifi/ap/{id}/default` | non fourni / requête sans corps | WifiApConfig | [get--api-v9-wifi-ap-id-default](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-default) ; wifi-channel-width-number-versus-string |
| `GET /api/v9/wifi/bss/{id}/default` | non fourni / requête sans corps | WifiBssConfig | [get--api-v9-wifi-bss-id-default](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-bss-id-default) ; wifi-hide-ssid-and-gcmp-types, wifi-wps-fields-absent-from-schema |
| `GET /api/v9/wifi/default` | non fourni / requête sans corps | WifiDefaultsResult | [get--api-v9-wifi-default](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-default) ; wifi-channel-width-number-versus-string, wifi-hide-ssid-and-gcmp-types, wifi-wps-fields-absent-from-schema |
| `GET /api/v9/wifi/diag` | non fourni / requête sans corps | WifiDiagnosticsResult | [get--api-v9-wifi-diag](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-diag) ; aucune contradiction spécifique relevée |
| `POST /api/v9/wifi/diag` | WifiDiagnosticFix | non documenté / succès sans result illustré | [post--api-v9-wifi-diag](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-diag) ; aucune contradiction spécifique relevée |
| `GET /api/v9/wifi/ap/{id}/diag` | non fourni / requête sans corps | WifiDiagItem[] | [get--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag) ; wifi-ap-diagnostic-example-wrong-resource |
| `GET /api/v9/wifi/bss/{id}/diag` | non fourni / requête sans corps | WifiDiagItem[] | [get--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag) ; wifi-ap-diagnostic-example-wrong-resource |
| `POST /api/v9/wifi/ap/{id}/diag` | WifiDiagItem.code[] | non documenté / succès sans result illustré | [post--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag) ; wifi-ap-diagnostic-example-wrong-resource |
| `POST /api/v9/wifi/bss/{id}/diag` | WifiDiagItem.code[] | non documenté / succès sans result illustré | [post--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-ap-id-diag & -api-v9-wifi-bss-id-diag) ; wifi-ap-diagnostic-example-wrong-resource |
| `GET /api/v9/wifi/wps/config/` | non fourni / requête sans corps | WifiWpsConfiguration | [get--api-v9-wifi-wps-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-wps-config-) ; aucune contradiction spécifique relevée |
| `PUT /api/v9/wifi/wps/config/` | {enabled:bool} | WifiWpsConfiguration | [put--api-v9-wifi-wps-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v9-wifi-wps-config-) ; aucune contradiction spécifique relevée |
| `POST /api/v9/wifi/wps/start/` | WifiWpsStartRequest | int (created session id) | [post--api-v9-wifi-wps-start-](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-wps-start-) ; aucune contradiction spécifique relevée |
| `GET /api/v9/wifi/wps/sessions/` | non fourni / requête sans corps | WifiWpsSession[] | [get--api-v9-wifi-wps-sessions-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-wps-sessions-) ; wifi-wps-end-date-enum-versus-timestamp |
| `DELETE /api/v9/wifi/wps/sessions/` | non fourni / requête sans corps | non documenté / succès sans result illustré | [delete--api-v9-wifi-wps-sessions-](http://mafreebox.freebox.fr/doc/index.html#delete--api-v9-wifi-wps-sessions-) ; wifi-wps-end-date-enum-versus-timestamp |
| `GET /api/v14/wifi/custom_keys/config/` | non fourni / requête sans corps | WifiCustomKeyConfig | [get--api-v14-wifi-custom_keys-config-](http://mafreebox.freebox.fr/doc/index.html#get--api-v14-wifi-custom_keys-config-) ; wifi-custom-key-enabled-switch-absent, wifi-custom-key-encryption-no-table |
| `PUT /api/v14/wifi/custom_keys/config/` | {ssid:string}; enabled switch unresolved | WifiCustomKeyConfig | [put--api-v14-wifi-custom_keys-config-](http://mafreebox.freebox.fr/doc/index.html#put--api-v14-wifi-custom_keys-config-) ; wifi-custom-key-enabled-switch-absent, wifi-custom-key-encryption-no-table |
| `GET /api/v9/wifi/custom_key/` | non fourni / requête sans corps | WifiCustomKey[] | [get--api-v9-wifi-custom_key-](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-custom_key-) ; wifi-custom-key-max-use-count-type |
| `GET /api/v9/wifi/custom_key/{key_id}` | non fourni / requête sans corps | WifiCustomKey | [get--api-v9-wifi-custom_key-key_id](http://mafreebox.freebox.fr/doc/index.html#get--api-v9-wifi-custom_key-key_id) ; lan-l2ident-object-versus-array |
| `DELETE /api/v9/wifi/custom_key/{key_id}` | non fourni / requête sans corps | non documenté / succès sans result illustré | [delete--api-v9-wifi-custom_key-key_id](http://mafreebox.freebox.fr/doc/index.html#delete--api-v9-wifi-custom_key-key_id) ; lan-l2ident-object-versus-array |
| `POST /api/v9/wifi/custom_key/` | WifiCustomKeyParams (direct object, not wrapped params) | WifiCustomKey | [post--api-v9-wifi-custom_key-](http://mafreebox.freebox.fr/doc/index.html#post--api-v9-wifi-custom_key-) ; wifi-custom-key-max-use-count-type |
| `GET /api/v13/wifi/temp_disable` | non fourni / requête sans corps | TemporaryWifiDisable | [get--api-v13-wifi-temp_disable](http://mafreebox.freebox.fr/doc/index.html#get--api-v13-wifi-temp_disable) ; wifi-temporary-disable-stop-field |
| `POST /api/v13/wifi/temp_disable` | {duration:int,keep:enum TemporaryWifiDisable.keep}; stop body unresolved | non documenté / succès sans result illustré | [post--api-v13-wifi-temp_disable](http://mafreebox.freebox.fr/doc/index.html#post--api-v13-wifi-temp_disable) ; wifi-temporary-disable-stop-field |
| `GET /api/v14/wifi/bss/{id}/mlo/allowed_comb` | non fourni / requête sans corps | int[][] | [get--api-v14-wifi-bss-id-mlo-allowed_comb](http://mafreebox.freebox.fr/doc/index.html#get--api-v14-wifi-bss-id-mlo-allowed_comb) ; aucune contradiction spécifique relevée |
| `GET /api/v14/wifi/bss/{id}/mlo/config` | non fourni / requête sans corps | WifiMLOConfiguration | [get--api-v14-wifi-bss-id-mlo-config](http://mafreebox.freebox.fr/doc/index.html#get--api-v14-wifi-bss-id-mlo-config) ; aucune contradiction spécifique relevée |
| `PUT /api/v9/wifi/config/` | WifiMLOConfiguration (section/example), conflicts with formal WifiGlobalConfig | WifiMLOConfiguration (section/example), conflicts with formal WifiGlobalConfig | [changing-the-mlo-config](http://mafreebox.freebox.fr/doc/index.html#changing-the-mlo-config) ; **bloqué MLO** ; wifi-mlo-update-wrong-signature |
| `POST /api/v9/wifi/wps/stop/` | WifiWpsStopRequest | non documenté / succès sans result illustré | [stop-a-wps-session](http://mafreebox.freebox.fr/doc/index.html#stop-a-wps-session) ; aucune contradiction spécifique relevée |

## Inventaire des modèles et champs

Chaque ligne conserve les noms filaires exacts. Le JSON associé donne pour chacun le type, la présence, `null`, l’accès, la description et ses contraintes/tableaux complets, l’ancre, les versions et les valeurs enum. Aucun champ déprécié n’est remis dans ces contrats. Les doublons bruts sont marqués, avec l’objet parent comme preuve quand une ancre de propriété manque.

| Objet formel | Champs retenus |
| --- | --- |
| [ConnectionStatus](http://mafreebox.freebox.fr/doc/index.html#ConnectionStatus) | `state`, `type`, `media`, `ipv4`, `ipv6`, `rate_up`, `rate_down`, `bandwidth_up`, `bandwidth_down`, `bytes_up`, `bytes_down`, `ipv4_port_range` |
| [ConnectionConfiguration](http://mafreebox.freebox.fr/doc/index.html#ConnectionConfiguration) | `ping`, `is_secure_pass`, `remote_access`, `remote_access_port`, `remote_access_min_port`, `remote_access_max_port`, `remote_access_ip`, `api_remote_access`, `wol`, `adblock`, `adblock_not_set`, `allow_token_request`, `sip_alg` |
| [ConnectionIpv6Delegation](http://mafreebox.freebox.fr/doc/index.html#ConnectionIpv6Delegation) | `prefix`, `next_hop` |
| [ConnectionIpv6Configuration](http://mafreebox.freebox.fr/doc/index.html#ConnectionIpv6Configuration) | `ipv6_enabled`, `ipv6_firewall`, `ipv6_prefix_firewall`, `ipv6ll`, `ipv6_prefix_firewall` [doublon brut], `delegations` |
| [XdslStatus](http://mafreebox.freebox.fr/doc/index.html#XdslStatus) | `status`, `protocol`, `modulation`, `uptime` |
| [XdslStats](http://mafreebox.freebox.fr/doc/index.html#XdslStats) | `maxrate`, `rate`, `snr`, `attn`, `snr_10`, `attn_10`, `fec`, `crc`, `hec`, `es`, `ses`, `phyr`, `ginp`, `nitro`, `rxmt`, `rxmt_corr`, `rxmt_uncorr`, `rtx_tx`, `rtx_c`, `rtx_uc` |
| [XdslInfos](http://mafreebox.freebox.fr/doc/index.html#XdslInfos) | `status`, `down`, `up` |
| [LteRadioBand](http://mafreebox.freebox.fr/doc/index.html#LteRadioBand) | `enabled`, `bandwidth`, `rsrq`, `rsrp`, `rssi`, `band`, `pci` |
| [LteRadio](http://mafreebox.freebox.fr/doc/index.html#LteRadio) | `associated`, `plmn`, `signal_level`, `gcid`, `bands`, `ue_active` |
| [LteNetwork](http://mafreebox.freebox.fr/doc/index.html#LteNetwork) | `pdn_up`, `has_ipv6`, `ipv6_dns`, `ipv6`, `ipv6_netmask`, `has_ipv4`, `ipv4_dns`, `ipv4`, `ipv4_netmask` |
| [LteSim](http://mafreebox.freebox.fr/doc/index.html#LteSim) | `present`, `pin_locked`, `puk_remaining`, `iccid`, `puk_locked`, `pin_remaining` |
| [LteTunnelDetails](http://mafreebox.freebox.fr/doc/index.html#LteTunnelDetails) | `connected`, `last_error`, `tx_flows_rate`, `tx_max_rate`, `tx_used_rate`, `rx_flows_rate`, `rx_max_rate`, `rx_used_rate` |
| [LteTunnel](http://mafreebox.freebox.fr/doc/index.html#LteTunnel) | `lte`, `xdsl` |
| [LteConfiguration](http://mafreebox.freebox.fr/doc/index.html#LteConfiguration) | `enabled`, `radio`, `state`, `network`, `fsm_state`, `sim` |
| [FtthStatus](http://mafreebox.freebox.fr/doc/index.html#FtthStatus) | `sfp_present`, `sfp_alim_ok`, `sfp_has_power_report`, `sfp_has_signal`, `link`, `sfp_serial`, `sfp_model`, `sfp_vendor`, `sfp_vendor` [doublon brut], `sfp_pwr_tx`, `sfp_pwr_rx` |
| [DDNSStatus](http://mafreebox.freebox.fr/doc/index.html#DDNSStatus) | `status`, `next_refresh`, `last_refresh`, `next_retry`, `last_error` |
| [DDNSConfig](http://mafreebox.freebox.fr/doc/index.html#DDNSConfig) | `enabled`, `hostname`, `password`, `user` |
| [DhcpConfig](http://mafreebox.freebox.fr/doc/index.html#DhcpConfig) | `enabled`, `sticky_assign`, `gateway`, `netmask`, `ip_range_start`, `ip_range_end`, `always_broadcast`, `ignore_out_of_range_hint`, `boot_server`, `boot_file`, `dns`, `options` |
| [DhcpOption](http://mafreebox.freebox.fr/doc/index.html#DhcpOption) | `id`, `val` |
| [DhcpStaticLease](http://mafreebox.freebox.fr/doc/index.html#DhcpStaticLease) | `id`, `mac`, `comment`, `hostname`, `ip`, `host`, `options` |
| [DhcpDynamicLease](http://mafreebox.freebox.fr/doc/index.html#DhcpDynamicLease) | `mac`, `hostname`, `ip`, `lease_remaining`, `assign_time`, `refresh_time`, `is_static`, `host` |
| [DHCPv6Config](http://mafreebox.freebox.fr/doc/index.html#DHCPv6Config) | `enabled`, `use_custom_dns`, `dns` |
| [FreeplugNetwork](http://mafreebox.freebox.fr/doc/index.html#FreeplugNetwork) | `id`, `members` |
| [Freeplug](http://mafreebox.freebox.fr/doc/index.html#Freeplug) | `id`, `local`, `net_role`, `model`, `eth_port_status`, `eth_full_duplex`, `has_network`, `eth_speed`, `inactive`, `net_id`, `rx_rate`, `tx_rate` |
| [UPnPIGDConfig](http://mafreebox.freebox.fr/doc/index.html#UPnPIGDConfig) | `enabled`, `version` |
| [UPnPRedir](http://mafreebox.freebox.fr/doc/index.html#UPnPRedir) | `id`, `enabled`, `ext_src_ip`, `ext_port`, `int_ip`, `int_port`, `proto`, `desc`, `remaining`, `host` |
| [LanConfig](http://mafreebox.freebox.fr/doc/index.html#LanConfig) | `ip`, `name`, `name_dns`, `name_mdns`, `name_netbios`, `type` |
| [Route](http://mafreebox.freebox.fr/doc/index.html#Route) | `prefix`, `gateway`, `enabled`, `description` |
| [LanHost](http://mafreebox.freebox.fr/doc/index.html#LanHost) | `id`, `primary_name`, `domain_name`, `host_type`, `primary_name_manual`, `l2ident`, `vendor_name`, `persistent`, `reachable`, `last_time_reachable`, `active`, `last_activity`, `first_activity`, `names`, `l3connectivities`, `network_control`, `info` |
| [LanHostName](http://mafreebox.freebox.fr/doc/index.html#LanHostName) | `name`, `source` |
| [LanHostL2Ident](http://mafreebox.freebox.fr/doc/index.html#LanHostL2Ident) | `id`, `type` |
| [LanHostL3Connectivity](http://mafreebox.freebox.fr/doc/index.html#LanHostL3Connectivity) | `addr`, `af`, `active`, `reachable`, `last_activity`, `last_time_reachable`, `model` |
| [LanHostNetworkControl](http://mafreebox.freebox.fr/doc/index.html#LanHostNetworkControl) | `profile_id`, `name`, `current_mode` |
| [DmzConfig](http://mafreebox.freebox.fr/doc/index.html#DmzConfig) | `ip`, `enabled` |
| [PortForwardingConfig](http://mafreebox.freebox.fr/doc/index.html#PortForwardingConfig) | `id`, `enabled`, `ip_proto`, `wan_port_start`, `wan_port_end`, `lan_ip`, `lan_port`, `hostname`, `host`, `src_ip`, `comment` |
| [IncomingPortConfig](http://mafreebox.freebox.fr/doc/index.html#IncomingPortConfig) | `id`, `enabled`, `active`, `type`, `in_port`, `netns`, `in_port` [doublon brut], `min_port`, `max_port`, `readonly` |
| [SfpConfig](http://mafreebox.freebox.fr/doc/index.html#SfpConfig) | `sfp_type_forced`, `sfp_type_forced_value`, `available_sfp_types` |
| [SfpStatus](http://mafreebox.freebox.fr/doc/index.html#SfpStatus) | `present`, `eeprom_valid`, `supported`, `type`, `power_good`, `link`, `vendor_name`, `part_number`, `hardware_rev`, `serial_number` |
| [SwitchPortStatus](http://mafreebox.freebox.fr/doc/index.html#SwitchPortStatus) | `id`, `link`, `duplex`, `speed`, `mode`, `mac_list` |
| [SwitchPortConfig](http://mafreebox.freebox.fr/doc/index.html#SwitchPortConfig) | `id`, `duplex`, `speed` |
| [SwitchPortStats](http://mafreebox.freebox.fr/doc/index.html#SwitchPortStats) | `rx_bad_bytes`, `rx_broadcast_packets`, `rx_bytes_rate`, `rx_err_packets`, `rx_fcs_packets`, `rx_fragments_packets`, `rx_good_bytes`, `rx_good_packets`, `rx_jabber_packets`, `rx_multicast_packets`, `rx_oversize_packets`, `rx_packets_rate`, `rx_pause`, `rx_undersize_packets`, `rx_unicast_packets`, `tx_broadcast_packets`, `tx_bytes`, `tx_bytes_rate`, `tx_collisions`, `tx_deferred`, `tx_excessive`, `tx_fcs`, `tx_late`, `tx_multicast_packets`, `tx_multiple`, `tx_packets`, `tx_packets_rate`, `tx_pause`, `tx_single`, `tx_unicast_packets` |
| [WifiGlobalConfig](http://mafreebox.freebox.fr/doc/index.html#WifiGlobalConfig) | `enabled`, `mac_filter_state` |
| [WifiSteeringConfig](http://mafreebox.freebox.fr/doc/index.html#WifiSteeringConfig) | `steering_level` |
| [WifiGlobalState](http://mafreebox.freebox.fr/doc/index.html#WifiGlobalState) | `state`, `expected_phys` |
| [ExpectedPhy](http://mafreebox.freebox.fr/doc/index.html#ExpectedPhy) | `band`, `phy_id`, `detected` |
| [WifiAp](http://mafreebox.freebox.fr/doc/index.html#WifiAp) | `id`, `name`, `status`, `capabilities`, `config` |
| [WifiApStatus](http://mafreebox.freebox.fr/doc/index.html#WifiApStatus) | `state`, `channel_width`, `primary_channel`, `secondary_channel`, `dfs_cac_remaining_time`, `dfs_disabled`, `temp_disable_remaining_time` |
| [WifiApCapabilities](http://mafreebox.freebox.fr/doc/index.html#WifiApCapabilities) | `2d4g`, `5g`, `6g`, `60g` |
| [WifiApHtConfig](http://mafreebox.freebox.fr/doc/index.html#WifiApHtConfig) | `ac_enabled`, `ht_enabled` |
| [WifiApHeConfig](http://mafreebox.freebox.fr/doc/index.html#WifiApHeConfig) | `enabled` |
| [WifiApConfig](http://mafreebox.freebox.fr/doc/index.html#WifiApConfig) | `band`, `channel_width`, `primary_channel`, `secondary_channel`, `dfs_enabled`, `ht`, `he` |
| [WifiApChannelSurveyData](http://mafreebox.freebox.fr/doc/index.html#WifiApChannelSurveyData) | `timestamp`, `busy_percent`, `tx_percent`, `rx_percent`, `rx_bss_percent` |
| [WifiAllowedComb](http://mafreebox.freebox.fr/doc/index.html#WifiAllowedComb) | `band`, `channel_width`, `need_dfs`, `dfs_cac_time`, `psc`, `primary`, `secondary` |
| [WifiStation](http://mafreebox.freebox.fr/doc/index.html#WifiStation) | `id`, `mac`, `bssid`, `hostname`, `host`, `state`, `inactive`, `conn_duration`, `rx_bytes`, `tx_bytes`, `tx_rate`, `rx_rate`, `signal`, `flags`, `last_rx`, `last_tx` |
| [WifiStationFlags](http://mafreebox.freebox.fr/doc/index.html#WifiStationFlags) | `legacy`, `ht`, `vht`, `he`, `authorized` |
| [WifiStationStats](http://mafreebox.freebox.fr/doc/index.html#WifiStationStats) | `bitrate`, `mcs`, `vht_mcs`, `width`, `shortgi` |
| [WifiBss](http://mafreebox.freebox.fr/doc/index.html#WifiBss) | `id`, `phy_id`, `status`, `use_shared_params`, `config`, `bss_params`, `shared_bss_params`, `disable_wep` |
| [WifiBssStatus](http://mafreebox.freebox.fr/doc/index.html#WifiBssStatus) | `state`, `sta_count`, `authorized_sta_count`, `custom_key_ssid`, `partners` |
| [WifiBssConfig](http://mafreebox.freebox.fr/doc/index.html#WifiBssConfig) | `enabled`, `ssid`, `hide_ssid`, `gcmp256`, `encryption`, `key`, `eapol_version` |
| [WifiNeighbor](http://mafreebox.freebox.fr/doc/index.html#WifiNeighbor) | `bssid`, `ssid`, `band`, `channel_width`, `channel`, `secondary_channel`, `signal`, `capabilities` |
| [WifiNeighborCap](http://mafreebox.freebox.fr/doc/index.html#WifiNeighborCap) | `legacy`, `ht`, `vht` |
| [WifiChannelUsage](http://mafreebox.freebox.fr/doc/index.html#WifiChannelUsage) | `channel`, `band`, `noise_level`, `rx_busy_percent` |
| [WifiPlanning](http://mafreebox.freebox.fr/doc/index.html#WifiPlanning) | `use_planning`, `resolution`, `mapping` |
| [WifiMacFilter](http://mafreebox.freebox.fr/doc/index.html#WifiMacFilter) | `id`, `mac`, `comment`, `type`, `hostname`, `host` |
| [WifiDiagItem](http://mafreebox.freebox.fr/doc/index.html#WifiDiagItem) | `ap_id`, `bssid`, `code`, `severity` |
| [WifiWpsCandidate](http://mafreebox.freebox.fr/doc/index.html#WifiWpsCandidate) | `bssid`, `ssid`, `bss_uuid`, `band`, `encryption`, `wps_enabled`, `state` |
| [WifiWpsSession](http://mafreebox.freebox.fr/doc/index.html#WifiWpsSession) | `id`, `bss_uuid`, `ssid`, `active`, `result`, `start_date`, `end_date`, `mac` |
| [WifiCustomKeyConfig](http://mafreebox.freebox.fr/doc/index.html#WifiCustomKeyConfig) | `ssid`, `ssid_read_only`, `hide_ssid`, `encryption` |
| [WifiCustomKeyHost](http://mafreebox.freebox.fr/doc/index.html#WifiCustomKeyHost) | `hostname`, `host` |
| [WifiCustomKeyParams](http://mafreebox.freebox.fr/doc/index.html#WifiCustomKeyParams) | `description`, `key`, `max_use_count`, `duration`, `access_type` |
| [WifiCustomKey](http://mafreebox.freebox.fr/doc/index.html#WifiCustomKey) | `id`, `remaining`, `params`, `users` |
| [TemporaryWifiDisable](http://mafreebox.freebox.fr/doc/index.html#TemporaryWifiDisable) | `duration`, `keep`, `remaining` |
| [WifiMLOConfiguration](http://mafreebox.freebox.fr/doc/index.html#WifiMLOConfiguration) | `partners` |

Les formes supplémentaires examinées sont : `LteAggregationResult`, `LteAggregationUpdate`, `LanBrowserInterface`, `LanHostTypeDescriptor`, `WakeOnLanRequest`, `SwitchMacEntry`, `WifiDefaultAp`, `WifiDefaultBss`, `WifiDefaultsResult`, `WifiDiagnosticsResult`, `WifiDiagnosticFix`, `WifiApDiagnosticFix`, `WifiBssDiagnosticFix`, `WifiWpsConfiguration`, `WifiWpsStartRequest`, `WifiWpsStopRequest`, `DhcpDynamicLeaseAdditionalFields`, `LanHostAdditionalFields`, `WifiBssConfigAdditionalFields`. Elles sont fondées sur les exemples/prose et ne prétendent pas être des objets officiellement nommés.

## Inconnues et conflits à accepter globalement

| Identifiant | Question / impact | Décision suivante |
| --- | --- | --- |
| `network-null-and-required` | Except explicitly conditional or optional fields, request/response presence and JSON null acceptance are not specified. A partial example is not a rule that every field is optional. What accepted write DTO presence policy will be used? (`write_policy_requires_consolidation`) | Use per-field omission-aware Optional<T> for explicit updates; send false and 0 when specified, omit unset values; do not send null without documented support. |
| `network-per-operation-permissions` | These network sections provide no per-operation permission names. Authentication/permission requirements must be supplied by the reviewed common protocol; absence of a permission paragraph does not establish public access. (`common_dependency`) | Use protocol review and conservative authenticated transport; mark per-operation permission status not_documented. |
| `network-timestamp-unit` | Timestamp units are not specified globally; WifiApChannelSurveyData and its path example use 13-digit values while LanHost and WPS examples use 10-digit values. (`no_global_date_converter`) | Retain raw signed 64-bit integers and local source unit metadata; do not invent a uniform seconds/milliseconds converter. |
| `lan-config-type-versus-mode` | LanConfig declares type enum router/bridge, but GET/PUT examples use the key mode. Which wire name is canonical at current runtime? (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `lan-l2ident-object-versus-array` | LanHost.l2ident declares [] array of LanHostL2Ident but all examined LAN/DHCP host examples use one object. Required current JSON shape is unresolved. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `lan-source-enum-misplaced` | LanHostName.source is enum without local values; the table dhcp/netbios/mdns/mdns_srv/upnp/wsd is physically under LanHostL2Ident.type (declared string), whose examples use mac_address. The table association is inconsistent. (`enum_contract_incomplete`) | Preserve unknown wire strings; do not reassign the table silently. Source-placement conflict must remain visible. |
| `lan-domain-name-example-invalid` | LanHost.domain_name requires .home suffix and max63 but the list example uses iphone-r0ro without .home. Response validation must not reject documented example by enforcing write validation everywhere. (`response_validation_policy`) | Apply documented constraints to authored updates only; preserve raw response strings; do not normalize existing names. |
| `network-route-example-conflicts` | Several primary signatures differ from request examples: IPv6 PUT uses /connection/config; DMZ PUT uses /lan/config; incoming PUT adds /lan; UPnP DELETE example uses GET; BSS PUT has //; MAC filter POST/PUT examples target collection/item inconsistently; SFP and routes slash variants. (`requires_global_source_priority_policy`) | Prefer explicit method signature and matching effect/title when independent body/model evidence agrees; keep contradictory examples as defects. Preserve source literal paths separately from latest discovered runtime prefix. |
| `lan-routes-array-update-semantics` | PUT lan/routes accepts an array in the example and returns fewer routes, but replacement/merge semantics, defaults and mandatory Route fields are not stated explicitly. (`update_semantics_not_documented`) | Expose explicit list update with caller-supplied Route[]; do not advertise per-route CRUD or implicit deletion semantics. |
| `nat-wan-port-start-string-versus-number` | PortForwardingConfig.wan_port_start is declared string, but all forwarding request/response examples are JSON integers. Current request representation requires an accepted policy. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `lte-radio-bands-missing-wire-type` | LteRadio.bands is declared [ro] with no array element type. Example is only an empty array, so LteRadioBand[] is a plausible relationship but is not proved. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `lte-aggregation-result-wrapper` | GET connection/aggregation says result LteTunnel, whose fields are lte and xdsl, but actual response example is {enabled,tunnel:{lte,xdsl}}. PUT text says LteConfiguration, request has enabled only, and no response is supplied. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `sfp-type-table-example-conflict` | SfpConfig.available_sfp_types table defines copper_sgmii_10g, example advertises copper_usxgmii_10g; forced_value is empty string when not forced; SfpStatus.type has no value table. (`open_value_policy`) | Use open wire type values and dynamically returned available_sfp_types to validate a forced selection; retain both documented spellings as different values, not aliases invented by client. |
| `sfp-capability-reference-mismatch` | SFP introductory text refers to SystemConfig.has_lan_sfp, while system review locates capability in SystemModelInfo.has_lan_sfp. Missing capability is not support. (`cross_reference_defect`) | Resolve through system-home reviewed model; require present-and-true has_lan_sfp before capability convenience path. |
| `switch-mac-entry-name-versus-hostname` | SwitchPortStatus.mac_list is array of object documented as {mac,name}, but example uses {mac,hostname}. No formal entry object is defined. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `switch-update-example-id-mismatch` | PUT switch/port/1 response example has id=4. Do not treat example as a redirection guarantee or mutate path identity from response. (`example_defect`) | Keep request path identity caller-controlled; preserve returned id without invented consistency guarantees. |
| `wifi-ap-state-duplicate-starting` | WifiApStatus.state table repeats starting for both starting and stopping. There is no documented stopping wire value. (`resolved_source_table_defect`) | Accept stopping from explicit12.0 changelog; do not interpret duplicate table rows as two different starting values. Value is supported by source evidence. |
| `wifi-channel-width-number-versus-string` | WifiApConfig.channel_width, WifiApStatus.channel_width and WifiNeighbor.channel_width are int in property signatures but JSON strings in examples. WifiAllowedComb.channel_width is string and consistent; do not classify it as a conflict. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `wifi-capability-integer-versus-object-map` | WifiApCapabilities band properties are int, described as maps; examples are objects of Boolean named capabilities and are explicitly UNSTABLE. Exact complete maps are not documented. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `wifi-bss-identifier-types` | WifiBss.id is int and phy_id is string in schema; examples use BSSID string for id and numeric phy_id. The path placeholder id follows the BSSID examples. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `wifi-hide-ssid-and-gcmp-types` | WifiBssConfig.hide_ssid and gcmp256 are declared str, while hide_ssid is Boolean in every BSS/default example. gcmp256 has only a Boolean description and no examined concrete JSON example. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `wifi-wps-fields-absent-from-schema` | WifiBssConfig formal schema omits wps_enabled and wps_uuid but WPS prose refers to wps_enabled, and BSS/default examples contain Boolean wps_enabled and string wps_uuid. Their write/access/presence contract is incomplete. (`schema_gap`) | Current field existence established by5.0 change and prose/examples; choose request/access/presence policy conservatively at consolidation. |
| `wifi-global-state-array-versus-object` | GET wifi/state describes a WifiGlobalState but its response example wraps this object in a result array. Which result shape is current? (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `wifi-wps-end-date-enum-versus-timestamp` | WifiWpsSession.end_date has type enum, description timestamp, and integer value in example. No enum values exist for it. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `wifi-wps-candidate-route-absent` | WifiWpsCandidate model and call recommendation exist, but there is no candidate discovery HTTP route in this WiFi section. (`candidate_operation_cannot_be_added`) | Do not invent wifi/wps/candidates; use typed BSS data explicitly or request a newer authoritative source. |
| `wifi-custom-key-enabled-switch-absent` | Guest config PUT prose permits SSID or global enabled switch, but WifiCustomKeyConfig has no enabled property and its only request example sets ssid. (`enabled_write_contract_missing`) | Implement only documented ssid update until explicit enabled field contract is supplied. |
| `wifi-custom-key-max-use-count-type` | WifiCustomKeyParams.max_use_count is int with max127 and zero unlimited, but create request example supplies string "100". Response values are numeric. (`blocks_typed_contract`) | Resolve from both exact source statements at consolidation; do not silently substitute a guessed contract. |
| `wifi-custom-key-encryption-no-table` | WifiCustomKeyConfig.encryption has type enum without local value table; its example uses wpa2_psk, which is not in WifiBssConfig.encryption table. Reuse of BSS encryption closed enum is unsafe. (`open_value_policy`) | Keep separate open wire value; do not assume BSS encryption vocabulary. |
| `wifi-temporary-disable-stop-field` | TemporaryWifiDisable.remaining is Read-only but its description says "Set to 0 to stop". POST example shows duration/keep only and no stopping example exists. (`stop_convenience_body_unresolved`) | Do not invent duration=0 or write a Read-only property automatically. Consolidate exact stop body separately. |
| `wifi-mlo-update-wrong-signature` | MLO changing section says PUT new WifiMLOConfiguration at mlo/config and example uses /api/v14/wifi/bss/<BSSID>/mlo/config/, but formal signature repeats PUT /api/v9/wifi/config/ with duplicate ID and refers to WifiGlobalConfig. (`blocks_mlo_mutation_route`) | Retain separate blocked MLO operation candidate with primary signature and explicit example route; do not merge it with global WiFi PUT or silently generate corrected route. |
| `wifi-ap-diagnostic-example-wrong-resource` | Per AP/BSS diagnostic GET formal signatures target /diag, while AP example requests /wifi/ap/0/bss. Both explicit /diag signatures and explanatory diagnostics title agree. (`example_route_defect`) | Prefer paired formal /diag paths at consolidation; expand them into separate AP and BSS operations sharing one source. |
| `network-example-only-properties` | Examples contain additional fields not in formal objects, including DhcpDynamicLease.options and nested LanHost.interface; breadth/completeness/access/nullability of such fields is not established. (`schema_extension_policy`) | Record additional fields explicitly with example evidence; keep unknowns visible and do not infer exhaustive model closure. |

La contradiction MLO est isolée : son titre, sa prose et son exemple désignent `wifi/bss/{id}/mlo/config/`, mais sa signature duplique le PUT global. Cette ligne ne doit être fusionnée avec le PUT global ni générée automatiquement sous une route corrigée. Les conflits fréquents de copier-coller (DMZ, IPv6, incoming, diagnostic et MAC filter) nécessitent la politique de priorité des preuves de l’intégrateur.

## Dépendances, validation et acceptation

- `protocol` : conventions JSON UTF-8, enveloppe, session, major découvert, permissions communes et HTTPS obligatoire.
- `system-home` : modes `allowed`, `denied`, `webonly` de `LanHostNetworkControl` et capacité SFP `SystemModelInfo.has_lan_sfp`. `webonly` est legacy/déconseillé sans marque Deprecated.
- Le type LAN est réutilisé par DHCP, NAT, IGD, WiFi et les domaines extérieurs. `ConnectionStatus.ipv4_port_range` est partagé avec VPN/ports de services ; un seul propriétaire doit générer ces types.

Contrôles réalisés : empreinte brute conforme, références d’ancres toutes résolues, structure obligatoire des propriétés vérifiée, deux signatures manquantes retrouvées dans les exemples, scopes de dépréciation confrontés au brut et aux changelogs. Aucun appel opérationnel à une Freebox, aucune modification du client, du Mapper, des tests ou du contexte partagé. Les seuls fichiers écrits dans le dépôt sont `network.json` et `network.md`.

La validation d’implémentation devra couvrir l’omission et false/0, les enums et nouveaux états, les bornes de ports/CIDR, les schémas request/result distincts WPS/guest/diagnostic/MLO, les unités et sentinelles, les chemins exacts, l’annulation et l’absence de replay. Un parcours de sérialisation représentatif devra être ajouté au sample Native AOT après acceptation du schéma.

**Verdict : `blocked_evidence` pour la génération intégrale de ce lot.** La revue est exhaustive pour les sections assignées, mais certaines formes/routes/types sont contradictoires ou insuffisamment spécifiés. L’orchestrateur doit accepter les résolutions ou scoper les sous-contrats fiables avant application. L’authenticité/fraîcheur du corpus et l’acceptation indépendante restent des décisions séparées de cette revue des octets.
