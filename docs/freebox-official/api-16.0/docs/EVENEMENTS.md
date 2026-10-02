# Événements et actions WebSocket

Le contrat de la source ne nomme que les événements ci-dessous dans `RegisterAction.events`. Aucun événement supplémentaire n’a été déduit de l’interface ou d’un SDK tiers.

[Convention WebSocket et enregistrement des événements](reference/fondamentaux/websocket.md#websocket-event-api)

| Événement | Type de résultat | Description officielle |
| --- | --- | --- |
| `vm_state_changed` | [VmStateChange](reference/stockage-vm/vm.md#VmStateChange) | VM status has changed |
| `vm_disk_task_done` | [VmDiskTask](reference/stockage-vm/vm.md#VmDiskTask) | VM disk task done |
| `lan_host_l3addr_reachable` | [LanHost](reference/reseau/lan.md#LanHost) | LAN machine had an L3 address (IPv4 or IPv6) become reachable. Usually when a machine appears on the network, or changes IP. |
| `lan_host_l3addr_unreachable` | [LanHost](reference/reseau/lan.md#LanHost) | LAN machine had an L3 address (IPv4 or IPv6) become unreachable. Usually when a machine disappears from the network (after a timeout), or changes IP. |

## Actions documentées

[RegisterAction](reference/fondamentaux/websocket.md#RegisterAction) permet l’abonnement aux événements. Les actions d’envoi de fichiers et leurs réponses sont décrites dans [File Upload](reference/fichiers-telechargements/upload.md#ws-upload-api). Les requêtes, réponses et événements utilisent les objets [WebSocketRequest](reference/fondamentaux/websocket.md#WebSocketRequest), [WebSocketResponse](reference/fondamentaux/websocket.md#WebSocketResponse) et [WebSocketNotification](reference/fondamentaux/websocket.md#WebSocketNotification).
