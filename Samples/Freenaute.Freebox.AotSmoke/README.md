Ce programme valide le client sans Freebox physique ni secret réel. Il utilise un `HttpMessageHandler`
local, le conteneur DI de Microsoft et la sérialisation JSON générée à la compilation.

Depuis la racine du dépôt, exécuter :

```sh
dotnet run --project Samples/Freenaute.Freebox.AotSmoke --configuration Release
dotnet publish Samples/Freenaute.Freebox.AotSmoke --configuration Release --runtime linux-x64
./artifacts/publish/Freenaute.Freebox.AotSmoke/release_linux-x64/Freenaute.Freebox.AotSmoke
```

Le deuxième lancement utilise un exécutable Native AOT. La publication nécessite les outils natifs
du système, notamment un compilateur C et les bibliothèques de développement de la plateforme.

Une exécution réussie affiche `PASS: 17 protected HTTP fixtures; ...` et termine avec le code zéro.
Les 17 requêtes protégées passent par les façades publiques et le transport HTTP réel du client,
avec des réponses locales contrôlées. Elles couvrent :

- découverte API 16, modèles connus et inconnus, réponse distante partielle, calcul HMAC avec
  `app_version`, partage de session entre clients DI et permissions Camera ;
- AirMedia, pourcentage exact, arrêt sans `media` et contrats JSON générés par le consommateur ;
- Network et Services, avec compteurs Int64, valeurs de réponse ouvertes et patchs conservant
  `false` et zéro tout en omettant les champs `Optional<T>` non renseignés ;
- Files, avec les deux formes objet/tableau des téléchargements, chemin Base64 opaque contenant
  `+` et `/`, URL échappée et soumission de plusieurs URLs en formulaire ;
- SystemHome, avec patch LCD et conservation d'un mode d'écran inconnu ;
- Protocol, avec lecture et création de cibles de notification.

Des vérifications JSON supplémentaires exécutent les unions scalaires Home, chaîne/entier et
objet vide/tableau d'entiers, ainsi que les métadonnées WebSocket de corrélation, inscription aux
événements et contrôle d'upload. Elles ne créent aucune connexion WebSocket et n'envoient aucun
bloc binaire. Toutes les sérialisations et désérialisations utilisent des métadonnées générées à
la compilation, avec la réflexion JSON désactivée.

Le programme charge également les deux racines TLS du SDK dans un handler privé. Les valeurs
`fixture-*` et les adresses `*.example` sont des données locales de test. Le succès démontre
l'exécution de ces parcours en Native AOT après publication ; la compatibilité avec une Freebox
physique et le protocole WebSocket complet nécessitent leurs propres validations.

En production, `AddFreeboxClient` utilise par défaut `https://mafreebox.freebox.fr/` et les racines
TLS publiées par le SDK, en conservant la vérification du nom, de la validité et de l'usage serveur
du certificat. L'adresse HTTPS peut être remplacée par le nom découvert par mDNS. Le handler de
test de ce programme remplace explicitement le transport réseau.
