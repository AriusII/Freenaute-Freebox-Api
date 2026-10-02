# Implémentation réseau du SDK Freebox Server

`FreeboxNetworkApi(IFreeboxTransport)` expose Connection, Dhcp, DhcpV6, Lan, Nat, Igd, Freeplug, Sfp, Switch et Wifi. Les façades et les sélecteurs sont publics et utilisent le transport injecté ; chaque sélection conserve ses identifiants sans modifier une sélection précédente. Toutes les opérations prennent un `CancellationToken`.

```csharp
using Freenaute.Freebox.Client.Domains.Network;
using Freenaute.Freebox.Mapper.Contracts.Network;

var network = new FreeboxNetworkApi(client.Transport);
var status = await network.Connection.GetStatusAsync(cancellationToken);
var host = await network.Lan.Interface("pub").Host("ether-00:11:22:33:44:55")
    .GetAsync(cancellationToken);

await network.Wifi.AccessPoint(0).UpdateAsync(new WifiAccessPointPatch
{
    Config = new WifiApConfigurationPatch
    {
        PrimaryChannel = 0,
        Ht = new WifiApHtConfigurationPatch { AcEnabled = false }
    }
}, cancellationToken);

await network.Wifi.GuestAccess.CreateKeyAsync(new CreateWifiCustomKeyRequest
{
    Description = "Invités",
    Key = secret,
    MaxUseCount = StringOrInteger.FromString("100"),
    Duration = 86400,
    AccessType = NetworkWireValues.WifiCustomKeyParamsAccessType.NetOnly
}, cancellationToken);
```

Le dernier exemple nécessite aussi `using Freenaute.Freebox.Mapper.Contracts.Primitives;`. Les méthodes de configuration font des mises à jour partielles avec des contrats d'écriture dédiés. `Optional<T>` omet les champs non sélectionnés, conserve les valeurs explicites `false` et `0`, et les façades réseau refusent `Optional<T>.Null` faute de preuve locale autorisant un effacement par JSON null. Les tableaux de routes constituent une opération de remplacement documentée ; les délégations IPv6 sont un tableau de huit entrées. Les paramètres du chemin sont encodés comme segments, sans culture courante pour les nombres.

Le manifeste machine [network.json](network.json) fournit les 108 opérations implémentées avec leur méthode, chemin relatif exact, déclaration officielle et membre public. Les versions littérales v8/v9/v11/v14/v16 sont des références documentaires : l'exécution utilise uniquement le major externe courant découvert par le transport. Les slashs terminaux de chaque déclaration sont conservés. Les exemples dont le chemin copié contredit une déclaration cohérente ne remplacent pas celle-ci.

Les 121 contrats disponibles ont leur provenance dans le même manifeste. Les objets de lecture ont des propriétés nullables pour représenter l'absence d'information ; ceci n'affirme aucune règle générale de nullabilité côté serveur. Les compteurs et horodatages restent des entiers signés de 64 bits avec leurs unités documentées localement. Les chaînes d'énumération préservent les valeurs inconnues ; `NetworkWireValues` fournit les tokens explicitement documentés. `LanHost.Info` est la seule carte de données JSON générales, car sa forme ouverte `dict` est indiquée dans le brut.

Les contradictions de types finies sont représentées par des unions sans conversion silencieuse : chaîne/entier pour les identifiants BSS, largeurs de canal, premier port NAT, limite d'usagers invités et date WPS ; chaîne/booléen pour `hide_ssid` ; objet/tableau pour les identifiants L2 et l'état global WiFi ; entier/carte de booléens pour les capacités radio. Les deux noms `name` et `hostname` d'une entrée MAC switch, et `type` et `mode` de la configuration LAN, restent des propriétés distinctes de lecture.

Deux opérations restent bloquées localement : le GET LTE contient `LteRadio.bands` sans type d'élément établi, et la déclaration du PUT MLO contredit son exemple et sa section. Aucun chemin pour lister les candidats WPS n'est inventé. Le choix `type/mode` du LAN, le commutateur global `enabled` d'accès invité, l'écriture de `wps_uuid` et l'arrêt du WiFi temporaire via `remaining=0` ne sont pas exposés en écriture faute de contrat établi. Le démarrage temporaire avec `duration/keep` est disponible. Les champs explicitement dépréciés `is_main_bss` et `use_default_config` sont absents des contrats ; leur remplacement `use_shared_params` est présent. La planification WiFi demeure déclarée sans marque explicite de dépréciation, avec Standby comme remplacement recommandé dans le changelog.

Les scans interrompent temporairement l'accès via le point WiFi concerné, les redémarrages peuvent interrompre la réponse et la suppression d'une clé invitée déconnecte ses stations : le transport ne rejoue aucune mutation. La gestion d'une session WPS reste explicite (démarrage, arrêt, consultation, suppression des sessions) ; aucune boucle cachée ne décide d'une action réseau.

`NetworkJsonSerializerContext` contient les métadonnées générées et les conversions fermées nécessaires au mode AOT. Les tests réseau couvrent des variantes de réponse réellement présentes dans le corpus, les mises à jour imbriquées, l'omission des champs, les identifiants encodés, les corps directs/tableaux, les limites invitées et l'annulation. Le build d'intégration, les tests et la publication AOT sont exécutés par l'orchestrateur ; aucune requête opérationnelle contre une Freebox physique n'a été effectuée.

La source brute est l'archive `index.html` d'API 16.0 fournie, SHA-256 `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`. La revue détaillée, les distinctions déclaration/exemple/changelog et les exclusions sont conservées dans [network.json](../freebox-official/reviews/network.json) et [network.md](../freebox-official/reviews/network.md).

`gcmp256` demeure une chaîne selon sa signature `str` ; la description booléenne seule ne prouve pas un JSON booléen.
