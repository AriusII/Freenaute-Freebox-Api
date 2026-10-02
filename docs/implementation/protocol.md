# Découverte, authentification, caméra et notifications

Le manifeste [protocol.json](protocol.json) relie douze opérations Freebox aux signatures de l’instantané API 16.0. La découverte `/api_version` configure la version et les capacités du serveur ; les numéros historiques des exemples actifs ne limitent pas les appels à ces anciennes versions.

L’authentification conserve l’autorisation explicite, le suivi, le challenge HMAC-SHA1, la session partagée et la fermeture. Aucun appel métier n’est rejoué après un refus d’authentification. `Cameras.ListAsync()` et `Cameras.Camera(id).GetAsync()` exposent les informations disponibles sans inventer un protocole vidéo.

Les notifications permettent de gérer les cibles. Leur lecture individuelle conserve la forme JSON objet ou tableau ; leurs écritures n’envoient que les champs attestés par les exemples. Les routes `/register`, `/register/{box_id}/{device_id}` et `/send` décrivent le serveur push du consommateur. Elles disposent de contrats de callback, mais ne sont jamais envoyées à la Freebox et ne comptent pas comme opérations clientes.

Les événements et uploads WebSocket disposent de leur [manifeste séparé](websockets.json). Les tests utilisent des réponses simulées ; aucun appel sur une Freebox réelle n’est annoncé.
