# Services et médias — implémentation API 16

`FreeboxServicesApi` expose Call, Contacts, Ftp, Tftp, NetworkShare, UPnpAv, Vpn, VpnClient, Player et Pvr. AirMedia conserve sa façade existante. Le manifeste [services.json](services.json) relie chaque opération à sa route relative, son membre C# et l’ancre exacte du document officiel fourni, vérifié par SHA-256 `cab9f9512f5df0c8c7857efde19790b99dce92fb5c189dd7b87aec1011e3ae03`.

Les 75 signatures documentées de ces 11 modules possèdent un membre client : 71 signatures ajoutées et 4 AirMedia existantes. L’expansion des alternatives de Contacts produit 90 routes concrètes, dont 86 ajoutées. Cette couverture des signatures conserve deux limites de variante : `fmt=json` du téléchargement VPN n’a pas de schéma documenté, et la création VPN Client OpenVPN ne dispose pas de contrat de configuration/import. Le téléchargement `plain`, les configurations PPTP/WireGuard et les opérations sur les configurations existantes sont disponibles. Les champs non bornés `routes`, `package_id` et `dhcp_options` sont également listés dans le manifeste comme omissions locales.

Les routes utilisent la version majeure découverte par le transport. Les segments `player/{id}/api/v6/` conservent la version interne du Player. Le PUT TFTP emploie la route majeure courante attestée par l’exemple v16, tout en conservant `/api/latest/tftp/config/` comme signature documentaire. Les actions groupées Call suivent leurs méthodes POST formelles. Les exemples contradictoires ne déclenchent aucun essai de méthode ou de route alternative.

```csharp
var services = new FreeboxServicesApi(client.Transport);

await services.Player.Device(1).Volume()
    .Level(0)
    .Muted(false)
    .SendAsync(cancellationToken);

await services.Ftp.Configure()
    .With(config => config with { Enabled = false })
    .SendAsync(cancellationToken);

await services.Contacts.Contact(7).Numbers.Create()
    .With(number => number with
    {
        Number = "0999999999",
        Type = ContactNumberType.Fixed,
        IsDefault = false
    })
    .SendAsync(cancellationToken);

await using var audio = await services.Call
    .Voicemail("20221215_154135_r0334371508.au")
    .DownloadAudioAsync(cancellationToken);
await audio.Content.CopyToAsync(destination, cancellationToken);
```

Les sélecteurs et commandes sont immuables : une branche fluent ne modifie pas une autre branche. La création d’un élément Contact renseigne l’ID du contact sélectionné ; ses lectures, mises à jour et suppressions suivent les routes top-level `number`, `address`, `url`, `email` documentées. Les identifiants textuels de messages vocaux, connexions VPN et configurations VPN Client sont encodés séparément comme segments de route.

Les DTO de lecture et d’écriture sont distincts. Les mises à jour utilisent `Optional<T>` et des métadonnées JSON générées : champ absent omis, `false` et `0` explicitement transmis, `null` rejeté lorsqu’aucune sémantique n’est documentée. Les champs marqués en lecture seule et les ajouts attestés uniquement par un exemple de réponse ne figurent pas dans les écritures. Le volume vérifie 0–100 ; les mots de passe utilisateur VPN vérifient 8–32 caractères. Les enregistrements manuels/générés restent soumis aux restrictions du serveur ; aucune mutation implicite n’est ajoutée.

Les valeurs textuelles documentées possèdent des types propres et des valeurs nommées ; les valeurs inconnues reçues restent lisibles. Les différences déclaration/exemple sont conservées par des unions typées pour `country_code`, `local_ip`, les IDs PVR, `record_time` et les modes IPSec. Les modes IPSec gardent les clés du dictionnaire, notamment `psk`. Les compteurs utilisent `long`. Les timestamps restent des entiers du protocole ; aucune règle universelle de conversion en date n’est supposée.

`ServicesJsonSerializerContext` utilise exclusivement le générateur intégré de `System.Text.Json`, y compris pour les représentations fermées des unions et les valeurs des champs optionnels. Il n’introduit ni résolution par réflexion ni projet de générateur supplémentaire. Le contexte du premier plan du Player est le seul objet JSON ouvert, car la source le décrit explicitement comme défini par l’auteur de l’application. Les représentations `ToString()` des objets omettent leur contenu, notamment les secrets VPN et les coordonnées.

Les réponses binaires conservent leur contenu et leur type MIME ; le téléchargement possède et libère la réponse HTTP. La génération d’un profil OpenVPN peut invalider le précédent fichier : l’opération envoie une seule requête. Les tests du lot vérifient ces comportements, les branches immuables, les routes, les corps partiels, les unions et les limites locales. La compilation et l’exécution finale sont effectuées par l’orchestrateur avec les autres domaines.
