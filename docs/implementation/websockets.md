# Événements et upload WebSocket

`FreeboxWebSocketsApi(IFreeboxWebSocketTransport)` fournit les deux handshakes spécialisés attestés, `ws/event` et `ws/upload`, sous le major externe courant. Le transport racine partage l'origine, le handler HTTPS et la session HTTP, et ajoute le token au handshake. Le manifeste [websockets.json](websockets.json) sépare les deux routes HTTP des actions WebSocket et des quatre événements enregistrables.

```csharp
using Freenaute.Freebox.Client.Domains.WebSockets;
using Freenaute.Freebox.Mapper.Contracts.Primitives;

var sockets = new FreeboxWebSocketsApi(webSocketTransport);
await using var events = await sockets.Events.Subscribe(
    FreeboxServerEvent.VmStateChanged,
    FreeboxServerEvent.VmDiskTaskDone,
    FreeboxServerEvent.LanHostL3AddressReachable,
    FreeboxServerEvent.LanHostL3AddressUnreachable).OpenAsync(cancellationToken);

await foreach (var notification in events.ReadAllAsync(cancellationToken))
{
    var vmTask = notification.GetVmDiskTask();
    var lanHost = notification.GetLanHost();
    // Result reste disponible pour le payload extensible documenté.
}
```

Les noms filaires sont exactement `vm_state_changed`, `vm_disk_task_done`, `lan_host_l3addr_reachable` et `lan_host_l3addr_unreachable`. Les payloads VM et LAN utilisent les contrats et contextes générés des domaines correspondants. Les champs `source` et `event` restent bruts ; le rapprochement LAN utilise leur nom qualifié concaténé, sans inventer où couper les underscores. Les événements futurs reçus conservent leur payload `JsonElement` et peuvent être décodés avec des `JsonTypeInfo<T>` fournis par l'application.

L'ouverture envoie `register`. Le lecteur vérifie sa réponse corrélée lorsqu'elle est reçue et accepte aussi les notifications directes, sans exiger un ordre d'ACK supplémentaire. Un seul lecteur énumère la souscription. La fin de l'énumération, l'annulation ou le disposal ferme la connexion ; aucune reconnexion ni boucle de polling n'est implicite. La réception commune reconstitue les messages fragmentés et les enregistrements NDJSON dans la limite conservatrice de 1 000 000 octets.

```csharp
var command = sockets.Uploads
    .To(EncodedFreeboxPath.FromEncoded(directoryReturnedByFileSystem))
    .File("rapport.bin")
    .Overwrite()
    .WithSize(expectedFinalFileLength);

var uploaded = await command.SendAsync(inputStream, cancellationToken, progress);
```

La sélection est immuable. Par défaut aucun `force` n'est envoyé et un conflit est remonté. `Overwrite()` produit `force:"overwrite"`, `Resume()` produit `force:"resume"`. La taille est optionnelle, fournie explicitement par `WithSize`, et zéro est conservé. Le chemin Base64 et le nom du fichier restent exacts, sans normalisation Unicode.

L'upload attend le succès corrélé d'`upload_start` avant les bytes binaires. Ensuite un lecteur reçoit `upload_data` pendant que l'émetteur envoie les chunks, sans attendre un ACK pour chaque chunk. Les messages binaires contiennent les bytes du fichier ; aucun offset ou en-tête non documenté n'est ajouté. Leur taille par défaut est de 512 KiB et `WithChunkSize` reste borné à 1 000 000 octets. L'opération termine après `upload_finalize` et une réponse corrélée confirmant `complete:true` et `total_len`.

`Resume()` ajoute les bytes du stream tel qu'il est fourni ; le client ne consulte ni `Length` ni `Position` et n'effectue aucun `Seek`. L'appelant choisit les bytes restants et peut fournir la taille finale attendue. La progression distingue les bytes envoyés par cette session de `total_len`, longueur totale indiquée par le serveur, qui peut inclure un préfixe existant. Le stream appartient à l'appelant et reste ouvert.

L'annulation par token, les erreurs et le disposal ferment la connexion et conservent le fichier partiel selon la documentation. Ils n'envoient jamais `upload_cancel`. Une suppression explicite est disponible via la session avancée :

```csharp
await using var session = await sockets.Uploads.To(destination).File("partiel.bin")
    .OpenAsync(cancellationToken);
await session.SendChunkAsync(bytes, cancellationToken);
await session.CancelAsync(cancellationToken); // Efface explicitement le partiel.
```

La session avancée expose aussi `FinalizeAsync` et `SendAsync`. Elle sérialise ses écritures et maintient un lecteur de réponse unique. Ses IDs sont 1 pour start/data, 2 pour finalize et 3 pour cancel ; une corrélation incorrecte échoue immédiatement. Les rejets du serveur conservent `error_code`, `msg` et `file_size` éventuel dans `FreeboxWebSocketApiException`, sans copier le texte non fiable dans le message de l'exception. Les deux graphies documentées de conflit (`conflict` et `destination_conflict`) sont conservées telles quelles. Aucune mutation n'est rejouée.

Chaque fichier de l'API de haut niveau possède une connexion. La réutilisation pour plusieurs fichiers, les consoles QEMU/VNC et les politiques de reconnexion restent hors des clients spécialisés ; le manifeste précise ces limites. Le transfert HTTP antérieur déprécié n'est jamais utilisé.

Les tests emploient un serveur WebSocket simulé, via le constructeur commun `FreeboxWebSocketConnection(WebSocket)`, et vérifient le pipeline, la réception de progrès pendant l'émission, les ACK et erreurs corrélés, les fichiers partiels, l'upload depuis un stream non seekable, le NDJSON fragmenté au milieu d'un caractère UTF-8 et les payloads VM/LAN conservés après fermeture. Le build, les tests et la publication AOT sont effectués par l'orchestrateur ; aucune Freebox physique n'est modifiée.
