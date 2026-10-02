# Guide d’intégration — Freebox Server API

Ce guide décrit le socle d’intégration d’une application cliente avec la Freebox Server API : découverte de la box, construction d’URL, contrat REST, association d’application, authentification, événements WebSocket et transfert de fichiers. Il sert de référence transversale aux fiches d’API du dossier.

## Périmètre et niveau de vérification

La capture de découverte conservée dans [sources/api-version.json](../sources/api-version.json) indique <code>api_version</code> <code>16.0</code> et <code>api_base_url</code> <code>/api/</code> au 2 octobre 2026. La documentation embarquée indique également que la version courante est <code>16.0</code> et que la version se note <code>majeure.mineure</code>. [API Version — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#api-version)

La collecte s’est limitée aux lectures GET de découverte et de documentation officielle embarquée. Aucun endpoint métier, d’association, de session, WebSocket ou de téléversement n’a été exécuté pour produire ce guide. Les chemins affichés dans la documentation officielle sont très souvent signés <code>/api/v8/…</code>, y compris dans la section qui annonce une API courante 16.0. Ils sont donc reproduits ici comme **signatures documentées** et ne constituent pas une preuve de compatibilité avec <code>/api/v16/…</code>. Une intégration doit négocier la version puis valider, dans son environnement de test, chaque ressource réellement utilisée.

Les API marquées <code>UNSTABLE</code> peuvent changer ou disparaître ; les API non documentées ne doivent pas être utilisées. [Statut des API — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#api-version)

## 1. Découvrir la Freebox et négocier la version

### mDNS : méthode à privilégier

La méthode recommandée est la découverte mDNS, qui évite de connaître au préalable l’adresse IP de la box. Le service publié est <code>_fbx-api._tcp</code>. Son enregistrement TXT fournit notamment <code>api_version</code>, <code>api_base_url</code>, <code>api_domain</code>, <code>https_available</code>, <code>https_port</code>, <code>box_model</code> et <code>box_model_name</code>. Ne pas s’appuyer sur <code>device_type</code> : il est explicitement déprécié au profit de <code>box_model</code>. [Découverte mDNS — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#discovery-using-mdns)

Conserver uniquement les métadonnées nécessaires à la connexion. Un UID, un nom de périphérique, un domaine ou un jeton de session ne doivent pas être placés dans les journaux applicatifs ni dans la télémétrie.

### Repli par <code>/api_version</code>

Lorsque mDNS n’est pas disponible, la documentation prévoit la lecture de <code>/api_version</code> sur <code>mafreebox.freebox.fr</code>. Elle précise que la découverte HTTPS est préférable au repli HTTP et que l’application doit valider le certificat pour utiliser l’API. Une découverte distante non authentifiée peut exposer moins de champs. [Découverte HTTP — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#discovery-using-http) · [Découverte HTTPS — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#discovery-using-https) · [Changement de l’API v7 — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#changed-api-v7-0)

Pour un accès distant déjà configuré, la documentation décrit aussi la découverte du port HTTPS dans l’enregistrement DNS SRV <code>_https._tcp</code> associé à <code>api_domain</code>. [Découverte d’un changement de port — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#remote-connection-port-change-discovery)

### Construire l’URL sans figer la version

La convention officielle est :

~~~
https://<api_domain>:<https_port><api_base_url>v<version_majeure>/<ressource>
~~~

En accès local, l’hôte de la convention est <code>mafreebox.freebox.fr</code> ; les valeurs de domaine, port, racine et version proviennent de la découverte. La documentation illustre la forme avec une ressource <code>login</code> sous <code>v16</code>. [Construction des URL — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#building-the-api-request-url)

~~~
# Pseudo-code de négociation ; aucune requête n’est exécutée ici.
metadata = lire_les_métadonnées_de_découverte()
major = entier(metadata.api_version avant le premier ".")
api_root = joindre("https://<hôte_découvert>", metadata.api_base_url)
url_candidate = joindre(api_root, "v" + major, "<ressource>")
~~~

Traiter <code>api_version</code> comme une capacité négociée et non comme une constante de build. La présence de signatures <code>v8</code> dans la référence est une information de documentation historique : elle ne justifie ni un remplacement mécanique par <code>v16</code>, ni l’appel d’un chemin <code>v8</code> sur une box déclarant <code>16.0</code>, sans validation de compatibilité.

## 2. Contrat REST : enveloppe, méthodes et erreurs

La plupart des API suivent REST. Toute requête ayant un corps utilise <code>Content-Type: application/json</code>, sauf indication contraire ; les réponses sont des objets JSON UTF-8. Respecter la méthode HTTP annoncée par chaque fiche. [Conventions API — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#api-conventions)

L’enveloppe de réponse standard est :

~~~json
{
  "success": true,
  "result": { "…": "…" }
}
~~~

Sur échec, l’enveloppe contient <code>success: false</code>, un <code>error_code</code> documenté par l’API concernée, et éventuellement <code>msg</code> (message humain, en français). <code>result</code> peut être absent lorsqu’une action n’a pas de résultat. Le code HTTP complète l’information : la documentation cite notamment <code>403</code> pour des identifiants invalides et <code>404</code> pour un chemin invalide. [Objet <code>APIResponse</code> — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#APIResponse) · [Exemples et codes HTTP — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#api-conventions)

La logique cliente doit donc :

1. vérifier le statut HTTP ;
2. analyser <code>success</code> ;
3. brancher sur <code>error_code</code> stablement documenté pour cette ressource ;
4. réserver <code>msg</code> à l’affichage ou au diagnostic, pas à une décision métier.

Ne pas supposer qu’un même <code>error_code</code> s’applique à toutes les ressources : la documentation indique que les valeurs possibles sont précisées par API.

## 3. Association et authentification d’application

### Cycle de vie des secrets

L’autorisation initiale doit être initiée depuis le réseau local et requiert une validation par l’utilisateur sur la façade de la Freebox. Elle aboutit à un <code>app_token</code> propre à l’application, associé à des permissions. La documentation demande de le conserver de façon sécurisée ; l’utilisateur peut le révoquer ou modifier les permissions. [Introduction au login — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#login)

| Élément | Rôle | Traitement client |
| --- | --- | --- |
| <code>app_id</code> | Identifiant stable de l’application | Le conserver constant entre association et ouverture de session. |
| <code>app_token</code> | Secret durable délivré après approbation | Stockage protégé ; ne jamais le journaliser ni l’envoyer comme en-tête d’API. |
| <code>challenge</code> | Valeur éphémère fournie par la box | L’utiliser immédiatement ; elle change fréquemment. |
| <code>session_token</code> | Jeton temporaire de session | L’envoyer seulement dans <code>X-Fbx-App-Auth</code>, puis le renouveler lorsqu’il expire. |

Les champs d’une demande d’association sont <code>app_id</code>, <code>app_name</code>, <code>app_version</code> et <code>device_name</code>. La signature documentée est <code>POST /api/v8/login/authorize/</code>. Elle retourne un <code>app_token</code> et un <code>track_id</code> ; le chemin est présenté ici comme une signature de documentation <code>v8</code>, avec la limite de version exposée en tête de guide. [Objet de demande — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#TokenRequest) · [Demande d’autorisation — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#request-authorization)

Après cette étape, suivre le statut lié au <code>track_id</code> jusqu’à ce qu’il ne soit plus <code>pending</code> : la documentation indique que ce suivi est obligatoire, même si l’utilisateur a accordé l’autorisation. Les états sont <code>unknown</code>, <code>pending</code>, <code>timeout</code>, <code>granted</code> et <code>denied</code> ; seule la transition vers <code>granted</code> permet l’ouverture d’une session. La signature publiée est <code>GET /api/v8/login/authorize/{track_id}</code>. [Suivi de l’autorisation — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#track-authorization-progress)

### Challenge et HMAC-SHA1

L’<code>app_token</code> n’est pas utilisé directement pour s’authentifier. Il sert à produire le mot de passe de session à partir du <code>challenge</code> courant :

~~~
# Pseudo-code cryptographique, sans appel réseau.
password = HMAC-SHA1(key = app_token, message = challenge)
~~~

La documentation formule cette opération comme <code>hmac-sha1(app_token, challenge)</code>. Le challenge peut être obtenu dans une réponse d’authentification, dans le suivi d’autorisation, ou via la signature documentée <code>GET /api/v8/login/</code> ; il a une validité limitée et change fréquemment. [Obtention de <code>session_token</code> — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#obtaining-a-session-token) · [Obtention du challenge — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#getting-the-challenge-value)

La signature documentée de création de session est <code>POST /api/v8/login/session/</code> avec <code>app_id</code>, <code>app_version</code> et le <code>password</code> calculé. Une réponse réussie fournit <code>session_token</code>, un nouveau <code>challenge</code> et les permissions effectives. [Ouverture de session — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#opening-a-session)

Pour les appels authentifiés, transmettre le jeton dans :

~~~
X-Fbx-App-Auth: <session_token>
~~~

La signature publiée de fermeture est <code>POST /api/v8/login/logout/</code>. Ne jamais mettre <code>app_token</code>, <code>session_token</code>, challenge ou en-tête d’authentification dans un exemple de production, une URL, un log ou un rapport d’erreur. [Appel authentifié — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#make-an-authenticated-call-to-the-api) · [Fermeture de session — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#closing-the-current-session)

### Permissions et erreurs d’authentification

Les permissions présentées par la documentation sont <code>settings</code>, <code>contacts</code>, <code>calls</code>, <code>explorer</code>, <code>downloader</code>, <code>pvr</code> et <code>profile</code>. Une permission absente équivaut à <code>false</code>. <code>settings</code> autorise la modification des réglages, tandis que la lecture de réglages est normalement autorisée ; certains éléments sensibles restent toutefois refusés ou occultés sans accès complet. La permission <code>parental</code> est annoncée comme obsolète et ne doit pas être retenue pour une nouvelle intégration. [Permissions de session — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#opening-a-session)

Avant d’activer une fonction, vérifier explicitement la permission reçue, puis gérer les erreurs d’authentification suivantes : <code>auth_required</code>, <code>invalid_token</code>, <code>pending_token</code>, <code>insufficient_rights</code>, <code>denied_from_external_ip</code>, <code>invalid_request</code>, <code>ratelimited</code>, <code>new_apps_denied</code>, <code>apps_denied</code> et <code>internal_error</code>. Elles sont associées à un statut HTTP 403 dans la documentation. [Erreurs d’authentification — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#authentication-errors)

## 4. WebSocket : authentification, requêtes et événements

Les WebSocket réutilisent le mécanisme d’authentification HTTP : inclure <code>X-Fbx-App-Auth</code> lors de l’ouverture de la connexion. Les messages sont généralement des textes UTF-8 encodés en JSON et la taille maximale de trame acceptée est de 1 Mio. [API WebSocket — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#websocket-api)

Une requête WebSocket porte une <code>action</code>, des paramètres propres à cette action et, de façon optionnelle, <code>request_id</code> pour corréler la réponse. La réponse reprend cet identifiant et peut contenir <code>success</code>, <code>result</code>, <code>error_code</code> et <code>msg</code>. [Conventions WebSocket — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#WebSocketRequest) · [Réponse WebSocket — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#WebSocketResponse)

La signature publiée du canal d’événements est <code>GET /api/v8/ws/event</code>. C’est un WebSocket texte JSON, un objet par ligne. Pour s’abonner, envoyer une action <code>register</code> contenant la liste <code>events</code> ; les notifications ont <code>action</code> égal à <code>notification</code>, puis les champs <code>source</code>, <code>event</code> et éventuellement <code>result</code>. [Canal d’événements — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#websocket-event-api) · [Format des notifications — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#WebSocketNotification)

~~~json
{
  "request_id": "<identifiant-corrélation>",
  "action": "register",
  "events": ["<événement-documenté>"]
}
~~~

Les événements publiés dans cette référence sont <code>vm_state_changed</code>, <code>vm_disk_task_done</code>, <code>lan_host_l3addr_reachable</code> et <code>lan_host_l3addr_unreachable</code>. Le protocole découpe le nom d’événement reçu en <code>source</code> et <code>event</code> (par exemple <code>vm_disk_task_done</code> devient <code>vm</code> et <code>disk_task_done</code>). [Action <code>register</code> et événements — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#RegisterAction)

## 5. Chemins encodés et téléversement WebSocket

### Chemins : préserver l’encodage fourni par l’API

L’API de système de fichiers encode les chemins en Base64 pour préserver leurs octets et éviter les problèmes d’équivalence Unicode. La recommandation officielle est d’utiliser directement la valeur de chemin renvoyée par l’appel de liste (<code>ls</code>) plutôt que de la reconstruire ou de la normaliser. Cela s’applique notamment aux champs <code>path</code>, aux cibles de liens et aux répertoires destination encodés. [Encodage des chemins — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#path-encoding) · [Champ <code>FileInfo.path</code> — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#FileInfo.path)

### Téléversement courant : WebSocket

La méthode HTTP d’upload antérieure est dépréciée depuis l’API v4. La documentation prescrit l’API WebSocket d’upload ; si WebSocket n’est pas possible, elle indique FTP pour le transfert de fichiers. Ne pas intégrer la méthode HTTP historique dans une nouvelle application. [Dépréciation de l’upload HTTP — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#file-upload) · [Changement v4 — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#deprecated-api-v4)

La signature documentée du canal est <code>GET /api/v8/ws/upload</code>, avec le même en-tête <code>X-Fbx-App-Auth</code> que les autres connexions WebSocket. Cette API ne nécessite plus de créer une autorisation de téléversement dédiée. [API d’upload WebSocket — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#websocket-file-upload-api) · [Exemple de handshake — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#get--api-v8-ws-upload)

Pour chaque fichier, la séquence documentée est :

1. envoyer une action JSON <code>upload_start</code> avec, selon le cas, <code>request_id</code>, <code>size</code>, <code>dirname</code> encodé, <code>filename</code> et <code>force</code> ;
2. attendre l’accusé de réussite de cette action ;
3. envoyer le contenu en trames WebSocket binaires brutes ;
4. recevoir les progrès <code>upload_data</code> sans attendre ce progrès avant d’émettre la trame suivante ;
5. terminer par l’action JSON <code>upload_finalize</code> et attendre sa réponse.

<code>force</code> absent produit un conflit si le fichier existe ; <code>overwrite</code> remplace la destination et <code>resume</code> ajoute les nouvelles trames au fichier existant. La fermeture de la connexion laisse le fichier partiel en place pour une reprise ultérieure ; <code>upload_cancel</code> supprime ce fichier partiel. Une même connexion peut servir à plusieurs fichiers. [Protocole d’upload — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#websocket-file-upload-api) · [Action <code>upload_start</code> — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#FileUploadStartAction) · [Trames binaires et progrès — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#file-upload-chunk) · [Finalisation — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#FileUploadFinalizeAction) · [Annulation — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#FileUploadCancelAction)

Le retour de progression associe le <code>request_id</code> de départ, <code>action</code> = <code>upload_data</code> et notamment <code>total_len</code> ; les indicateurs <code>complete</code> et <code>cancelled</code> renseignent la fin ou l’annulation. Gérer les erreurs documentées, en particulier <code>path_not_found</code>, <code>access_denied</code>, <code>destination_conflict</code>, <code>invalid_id</code>, <code>cancelled</code> et <code>noent</code>. [Réponse de progression — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#FileUploadChunkResponse) · [Erreurs d’upload — documentation officielle embarquée](../sources/raw/embedded/doc/index.html#file-upload-errors)

## Références et règles de maintenance

Les liens de ce guide pointent vers la copie locale de la documentation officielle recueillie pendant le crawl, avec ses ancres d’origine. Lors d’une mise à jour du corpus :

- relire la version annoncée par la découverte et l’ancre [API Version](../sources/raw/embedded/doc/index.html#api-version) ;
- conserver les signatures publiées telles quelles, en distinguant toujours l’observation documentée de la compatibilité vérifiée ;
- exclure les éléments explicitement dépréciés, en particulier <code>device_type</code> et l’upload HTTP historique ;
- ne publier aucune valeur réelle de jeton, challenge, UID, adresse, domaine, port ou nom de périphérique issu d’une installation.
