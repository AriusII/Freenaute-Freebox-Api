<a id="login"></a>

# Login

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#login)

## Navigation

- [Obtaining an app_token](#obtaining-an-app-token)
- [Obtaining a session_token](#obtaining-a-session-token)
- [Closing the current session](#closing-the-current-session)
- [Make an authenticated call to the API](#make-an-authenticated-call-to-the-api)


Each application identified with an *app_name* must gain access to
Freebox API before being able to use the api. This procedure can only
be initiated from the local network, and the user must have access to
the Freebox front panel to grant access to the app. Note that since you must be
on the local network, you must do your requests on <https://mafreebox.freebox.fr>
for the initial app authorization, since adding a new app is forbidden from the
outside network, and then use the generated [api_domain]:[freebox_port] for
subsequent accesses.

Once the user authorize the app, the app will be provided with a
unique *app_token* associated with a set of default permissions.

This *app_token* must be store securely by the app, and will not be
exchanged in clear text for the following requests.

Note that the user can revoke the *app_token*, or edit its permissions
afterwards. For instance if the user resets the admin password, app
permissions will be reset.

Then the app will need to open a *session* to get an *auth_token*. The
app will then be authenticated by adding this session_token in HTTP
headers of the following requests. The validity of the *auth_token* is
limited in time and the app will have to renew this *auth_token* once
in a while.

<a id="obtaining-an-app-token"></a>

## Obtaining an *app_token*

<a id="tokenrequest-object"></a>

### TokenRequest object

TokenRequest objects have the following attributes

<a id="TokenRequest"></a>

#### Objet TokenRequest

<a id="TokenRequest.app_id"></a>

**`app_id string`**

A unique app_id string

<a id="TokenRequest.app_name"></a>

**`app_name string`**

A descriptive application name (will be displayed on lcd)

<a id="TokenRequest.app_version"></a>

**`app_version string`**

app version

<a id="TokenRequest.device_name"></a>

**`device_name string`**

The name of the device on which the app will be used

<a id="request-authorization"></a>

### Request authorization

This is the first step, the app will ask for an *app_token* using the
following call. A message will be displayed on the Freebox LCD asking
the user to grant/deny access to the requesting app.

Once the app has obtained a valid app_token, it will not have to do
this procedure again unless the user revokes the app_token.

<a id="post--api-v8-login-authorize-"></a>

**`POST /api/v8/login/authorize/`**

**Example request**:

```http
POST /api/v8/login/authorize/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "app_id": "fr.freebox.testapp",
   "app_name": "Test App",
   "app_version": "0.0.7",
   "device_name": "Pc de Xavier"
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success": true,
   "result": {
      "app_token": "dyNYgfK0Ya6FWGqq83sBHa7TwzWo+pg4fDFUJHShcjVYzTfaRrZzm93p7OTAfH/0",
      "track_id": 42
   }
}
```

<a id="track-authorization-progress"></a>

### Track authorization progress

Once the authorization request has been made, the app should monitor
the token status by using the following API and using the *track_id*
returned by the previous call.

The status can have one of the following values:

| Status | Description |
| --- | --- |
| unknown | the app_token is invalid or has been revoked |
| pending | the user has not confirmed the authorization request yet |
| timeout | the user did not confirmed the authorization within the given time |
| granted | the app_token is valid and can be used to open a session |
| denied | the user denied the authorization request |

The app should monitor the status until it is different from pending. You MUST
implement this monitoring, otherwise your authorization will be invalid, even
if the user grants you access.

<a id="get--api-v8-login-authorize-track_id"></a>

**`GET /api/v8/login/authorize/{track_id}`**

**Example request**:

```http
GET /api/v8/login/authorize/42 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true,
    "result": {
        "status": "pending",
        "challenge": "Bj6xMqoe+DCHD44KqBljJ579seOXNWr2"
    }
}
```

**Example response once the user has validated the request**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true,
    "result": {
        "status": "granted",
        "challenge": "Bj6xMqoe+DCHD44KqBljJ579seOXNWr2"
    }
}
```

<a id="obtaining-a-session-token"></a>

## Obtaining a *session_token*

To protect the *app_token* secret, it will never be used directly to
authenticate the application, instead the API will provide a challenge
the app will combine to its *app_token* to open a session and get a
*session_token*

The app will then have to include the *session_token* in the HTTP
headers of the following requests

<a id="sessionstart-object"></a>

### SessionStart object

SessionStart objects have the following attributes:

<a id="SessionStart"></a>

#### Objet SessionStart

<a id="SessionStart.app_id"></a>

**`app_id string`**

Same app_id used in [`TokenRequest`](login.md#TokenRequest "TokenRequest") to get the *app_token*

<a id="SessionStart.app_version"></a>

**`app_version string`**

app version

<a id="SessionStart.password"></a>

**`password string`**

The password computed using the *challenge* and the *app_token*

To compute the password you have to compute the hmac-sha1 of the
challenge and the app_token

password = hmac-sha1(app_token, challenge)

<a id="getting-the-challenge-value"></a>

### Getting the challenge value

The challenge returned by the API will change frequently and have a
limited time validity.

There are several ways of getting the current challenge value, it will
always be included in response requesting the app authentication. It
is also included in the authorization tracking API response.

You can also explicitly request a challenge with the following API

<a id="get--api-v8-login-"></a>

**`GET /api/v8/login/`**

**Example request**:

```http
GET /api/v8/login/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true,
    "result": {
        "logged_in": false,
        "challenge": "VzhbtpR4r8CLaJle2QgJBEkyd8JPb0zL"
    }
}
```

<a id="opening-a-session"></a>

### Opening a session

Once you have the challenge you just need use the following API to get
a *session_token*

NOTE: in case of session opening failure, ensure that the box you’re
connected is the one you expect by checking the uid returned in the
answer.

In the response you get your app permissions. App permissions are:

| App permission | Description |
| --- | --- |
| settings | Allow modifying the Freebox settings (reading settings is always allowed) |
| contacts | Access to contact list |
| calls | Access to call logs |
| explorer | Access to filesystem |
| downloader | Access to downloader |
| pvr | Access personal video recorder |
| profile | Access to user profile management |

NOTE: A permission not listed in app permissions is equivalent to having this permission set
to false.

NOTE: There is no “privileged read” for the “settings” permission. This means
that, by default, any allowed application can read sensitive information, such
as MAC addresses, even without the “settings” permission. Certain items, such as
Wi-Fi or VPN credentials, are therefore denied or obfuscated if you do not have
full access.

<a id="post--api-v8-login-session-"></a>

**`POST /api/v8/login/session/`**

**Example request**:

```http
POST /api/v8/login/session/ HTTP/1.1
Host: mafreebox.freebox.fr

{
   "app_id": "fr.freebox.testapp",
   "password": "d4da8517c2c25b1b145f2e5ba91bd0589fc0053d"
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success": true,
   "result" : {
         "session_token" : "35JYdQSvkcBYK84IFMU7H86clfhS75OzwlQrKlQN1gBch\/Dd62RGzDpgC7YB9jB2",
         "challenge":"jdGL6CtuJ3Dm7p9nkcIQ8pjB+eLwr4Ya",
         "permissions": {
               "downloader": true,
         }
    }
}
```

**Example response with invalid password**:

```http
HTTP/1.1 403 Forbidden
Content-Type: application/json; charset=utf-8
```

```json
{
    "msg": "Erreur d'authentification de l'application",
    "success": false,
    "uid": "23b86ec8091013d668829fe12791fdab",
    "error_code": "invalid_token",
    "result": {
         "challenge": "DLjXFEf1kaDwAEn6xRUnEVPU++gnjiSn"
    }
}
```

<a id="closing-the-current-session"></a>

## Closing the current session

to close the current session you can use the following call

<a id="post--api-v8-login-logout-"></a>

**`POST /api/v8/login/logout/`**

**Example request**:

```http
POST /api/v8/login/logout/ HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success": true,
}
```

<a id="make-an-authenticated-call-to-the-api"></a>

## Make an authenticated call to the API

Once you have a valid *session_token* you should use it by add the the
HTTP header **X-Fbx-App-Auth**

**Example request**:

```http
GET /api/v8/login/session/ HTTP/1.1
Host: mafreebox.freebox.fr
X-Fbx-App-Auth: 35JYdQSvkcBYK84IFMU7H86clfhS75OzwlQrKlQN1gBch\/Dd62RGzDpgC7YB9jB2
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success": true,
   "result" : {
         [
            ...
         ]
    }
}
```

<a id="authentication-errors"></a>

### Authentication errors

When attempting to access the API, you may encounter the following
authentication errors:

NOTE that in this case the HTTP 403 return code will be used as well

| Error | Description |
| --- | --- |
| auth_required | Invalid session token, or not session token sent |
| invalid_token | The app token you are trying to use is invalid or has been revoked |
| pending_token | The app token you are trying to use has not been validated by user yet |
| insufficient_rights | Your app permissions does not allow accessing this API |
| denied_from_external_ip | You are trying to get an app_token from a remote IP |
| invalid_request | Your request is invalid |
| ratelimited | Too many auth error have been made from your IP |
| new_apps_denied | New application token request has been disabled |
| apps_denied | API access from apps has been disabled |
| internal_error | Internal error |
