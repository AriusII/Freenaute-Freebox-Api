# Implémentation fichiers, téléchargements, stockage et VM

Le lot expose `IFreeboxFilesApi` / `FreeboxFilesApi` avec huit façades : Downloads (dont Feeds),
FileSystem, Shares, Uploads, Storage, Raid, VirtualMachines et Statistics. Il partage les transports
JSON/binaire, la session et les métadonnées JSON générées du client ; aucun client HTTP autonome,
aucun générateur Roslyn supplémentaire et aucune réflexion JSON ne sont ajoutés.

96 variantes d'opérations sur les 103 relues sont implémentées, soit 95 signatures méthode/chemin
car `POST downloads/add` possède deux encodages de body. Les signatures et sources précises sont
dans [files.json](files.json). Les préfixes documentaires v8/v15 restent des preuves ; le routage
emploie la version majeure découverte, dans le contexte documentaire 16.0.

Les réponses conservent les tailles et compteurs en Int64, les enums inconnus en chaîne et les
chemins explicitement encodés en `EncodedFreeboxPath`. Les descriptions plaintext FsTask.from/to/src/dst
et FileUpload.dirname restent des chaînes. Les patches ont des `Optional<T>` : omission, false et
zéro restent distincts ; le domaine rejette un null explicite dont l'effet n'est pas documenté.
Les DTO de lecture et d'écriture sont séparés ; les modèles de réponse ne sont pas envoyés en PUT.

Les contradictions documentaires ont des représentations fermées : objet/tableau pour la liste
Downloads ; entier/chaîne pour table_type et les deux champs numériques contradictoires ; tableau
d'entiers/objet exactement vide pour DownloadPeer.requests ; id et feed_id indépendants pour les
mutations de feeds. Le bloc blocklist de lecture et les métadonnées Exif restent `JsonElement`
à leur emplacement précis ; le nom JSON sources/sources[] bloque uniquement la partie blocklist
écrite. Le champ deprecated DownloadFile.path et les anciens sélecteurs RRD temp1/temp2/temp3 sont exclus.

La soumission URLs emploie `application/x-www-form-urlencoded`, avec LF entre les URL d'une liste.
L'ajout de descripteur torrent/NZB emploie `multipart/form-data`, champ download_file. Ce mécanisme
n'est pas l'ancien upload HTTP de fichiers, qui est deprecated. Le transport consomme le HttpContent ;
le stream de descripteur reste possédé par le consommateur par défaut, sauf leaveOpen=false.
`FileSystem.At(path).DownloadAsync()` retourne une réponse binaire possédée, à disposer après lecture.
Les chemins Base64, URL de trackers et curseurs sont échappés une seule fois, sans transformation
Base64URL et sans ajout de fallback vers la route tracker singulière contradictoire.

Les sept variantes non implémentées sont localisées : upload WebSocket, console/VNC VM, GET RRD
sans encodage de paramètres établi, PUT VM sans body établi, et création/redimensionnement de disque
VM sans forme JSON du task-id établie. Les mutations VM/RAID dont le résultat est non spécifié utilisent
le succès commun sans inventer VM/RaidArray/bool. Aucune tâche FS ou VM n'est automatiquement pollée.

Les tests contrôlent les URL exactes et noms de champs, le paging, la conservation opaque des chemins,
les formulaires et multipart, la propriété des streams, les unions, les patches, les invariants des
requêtes, le choix POST RRD et l'absence de polling. Les fixtures locales ne prouvent aucune opération
métier exécutée sur une Freebox physique. La compilation et la publication Native AOT finales sont
assurées par l'intégration racine après stabilisation des autres lots.
