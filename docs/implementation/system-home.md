# System, Home et Profiles

Le lot expose **40 opérations revues** en C# 14/.NET 10 via
`IFreeboxSystemHomeApi` et `FreeboxSystemHomeApi`. Les façades `System`, `Language`, `Lcd`,
`Ledstrip`, `Standby`, `Updates`, `Home` et `Profiles` utilisent un seul `IFreeboxTransport`
injecté et partagent son origine, sa découverte et sa session. Le
[manifeste](system-home.json) donne la méthode, le chemin documentaire, le chemin relatif,
la preuve et le membre public de chaque opération.

La source est l’instantané embarqué **16.0** fourni par l’utilisateur, SHA-256
`cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`. Les références aux
signatures v8/v11/v16 restent des preuves ; le transport utilise la version majeure
découverte. Cette livraison ne revendique pas une acquisition distante indépendante ni
une couverture complète de tout le SDK Server.

## Commandes et ressources

```csharp
using Freenaute.Freebox.Client.Domains.SystemHome;
using Freenaute.Freebox.Mapper.Contracts.SystemHome;

var api = new FreeboxSystemHomeApi(client.Transport);
var system = await api.System.GetAsync(cancellationToken);
var languages = await api.Language.GetAsync(cancellationToken);
await api.Language.Set("fra").SendAsync(cancellationToken);

var lcd = await api.Lcd.Configure()
    .UseFields(new LcdConfigPatch()
        .WithBrightness(50)
        .WithOrientationForced(false))
    .SendAsync(cancellationToken);

var value = await api.Home.Node(14).Endpoint(1).GetAsync(cancellationToken);
await api.Home.Node(14).Endpoint(1).SetBoolean(true).SendAsync(cancellationToken);

var profile = api.Profiles.For(3);
var control = await profile.NetworkControl.Configure()
    .UseFields(new NetworkControlUpdate()
        .WithOverride(false)
        .WithOverrideMode(NetworkControlMode.Allowed))
    .SendAsync(cancellationToken);
```

La construction d’une commande n’effectue aucune I/O. `UseFields` crée une nouvelle
commande et conserve l’originale. Les DTO de requête sont des records immuables et les
collections de commande utilisent `ImmutableArray<T>`. Les patches distinguent les champs
non définis, `false` et `0` grâce à `Optional<T>`. Le `null` explicite est refusé pour les
patches où aucun effacement n’est documenté. Les propriétés de lecture seule, notamment
la résolution des plannings, ne sont pas envoyées.

## Contrats filaires et AOT

`SystemHomeJsonSerializerContext` fournit les métadonnées compilées. Les converters sont
fermés et typés ; aucun contrat non résolu n’est remplacé par `object` ou `dynamic`.
Les enums ont des types dédiés qui conservent leur token exact et leurs valeurs inconnues.
Une permission ou une capacité absente ne devient pas une affirmation d’autorisation.

`HomeIoValue` préserve `Null`, `Boolean`, `Integer`, `Float` ou `String`. Un flottant
intégral reste flottant à la réécriture ; les lexèmes flottants reçus sont conservés.
Une propriété `value` absente reste différente d’un `HomeIoValue.Null` explicite. Les
boutons documentés utilisent `Endpoint(...).Trigger()` pour envoyer `{"value":null}`.
L’endpoint visé doit être un slot approprié à la commande choisie.

Les variantes pairing Start/Next/Stop possèdent trois corps distincts sur le même chemin.
Leurs `op` sont constants. Les identifiants du corps Next utilisent `StringOrInteger`
pour conserver les représentations numériques et chaînes montrées par les sources. Les
valeurs positionnelles des widgets n’acceptent que les types attestés : null, booléen,
entier et chaîne. Les entrées sont copiées lors de la construction.

Les hôtes NetworkControl conservent soit un tableau de noms, soit un tableau de `LanHost`
issus du module Network. Les clés `icon`/`url` de Profile et `hide_status_led`/`hide_led`
de LCD restent des membres de lecture distincts. La création Profile envoie le champ
`url` de son exemple explicite ; le patch LCD expose le champ déclaré `hide_status_led`.
Les errata précis `SystemConfig.sensors` et `HomeTileData.history` sont inscrits au
manifeste avec leurs preuves. Seul le véritable champ ouvert `HomeAdapter.props` utilise
une map immuable de `JsonElement`.

## Limites localisées

Quatre opérations documentées restent non exposées : mise à jour Profile au chemin
contradictoire, lecture de tous les NetworkControl au résultat absent, création de règle
au chemin `network_controlr` ambigu et modification de règle au corps/résultat non défini.
Le manifeste conserve leurs sources et motifs. Slowness ne documente aucun endpoint ;
aucun stub ou slot d’opération n’a été ajouté.

Les champs `HomeNode.signal_links` et `slot_links` ne sont pas exposés, car `HomeNodeLink`
reste non défini. La propriété technique anonyme `label name` de HomeNodeType est
également absente du DTO ; son label d’affichage est présent une seule fois. Le type
d’adaptateur projette uniquement le champ `name` attesté par les exemples. Ces projections
partielles sont explicites dans `unsupported_fields` ; les lectures et autres commandes
documentées restent utilisables.

La suppression d’une règle vérifie l’enveloppe de succès et ne prétend pas un résultat
dont la source ne fournit pas le schéma. Aucune mutation n’est rejouée et aucune route
alternative n’est testée automatiquement.

Les tests du lot couvrent les unions, les noms exacts, omission/false/zéro/null,
l’immutabilité des commandes, les trois variantes pairing et les chemins HTTP. Les
fixtures restent des données de test, pas des preuves de comportement serveur. Les
builds, tests et parcours NativeAOT sont exécutés séquentiellement par l’intégrateur.
