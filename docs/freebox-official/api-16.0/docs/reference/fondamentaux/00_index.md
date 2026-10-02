<a id="developer-api-documentation"></a>

# Developer API Documentation

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#developer-api-documentation)

## Navigation

- [General Information](#general-information)
- [HTTPS Access](#https-access)
- [Authentication](#authentication)
- [WebSocket API](#websocket-api)
- [API List](#api-list)


FreeboxOS Gateway APi allow access to Freebox Server settings and
apps.

This API can be used to develop companion apps for Smartphone, or
provide an alternative to FreeboxOS web app.

<a id="general-information"></a>

## General Information

<a id="api-version"></a>

### API Version

Api version will always use the following format : “major.minor” where major and minor are integers

Current API version is “16.0”
Current major API version is: 16

When an API is marked as *unstable*, you can use it but it may change
or disappear at any time!

When an API is not documented you should not use it!

Other API will be maintained for at least 1 Freebox release.

<a id="api-changes"></a>

### Api Changes

<a id="freebox-discovery"></a>

### Freebox discovery

To discover a Freebox supporting this API you can either use mDNS, or
make a HTTP request to mafreebox.freebox.fr to get API information.

<a id="discovery-using-mdns"></a>

#### Discovery using mDNS

This is the preferred method since it does not require to know the
Freebox IP address.

The Freebox broadcasts the “_fbx-api._tcp” service

On iOS devices, you can use a [NSNetServiceBrowser](https://developer.apple.com/library/ios/#documentation/Cocoa/Reference/Foundation/Classes/NSNetServiceBrowser_Class/Reference/Reference.html)

On Android devices, you can use [Network Service Discovery](http://developer.android.com/training/connect-devices-wirelessly/nsd.html)
or [JmDNS](http://sourceforge.net/projects/jmdns/)

On the TXT record you can obtain the following information:

| Key | Description |
| --- | --- |
| api_version | The current API version on the Freebox |
| api_base_url | The API root path on the HTTP server |
| uid | The device unique id |
| api_domain | The domain to use in place of hardcoded Freebox ip |
| https_available | Tells if https has been configured on the Freebox |
| https_port | Port to use for remote https access to the Freebox Api |
| box_model_name | Box model display name |
| box_model | Box model |

Currently the existing box models are

| box_model | Description |
| --- | --- |
| fbxgw-r1/full | Freebox Server (v6) revision 1 |
| fbxgw-r2/full | Freebox Server (v6) revision 2 |
| fbxgw-r1/mini | Freebox Mini revision 1 |
| fbxgw-r2/mini | Freebox Mini revision 2 |
| fbxgw-r1/one | Freebox One revision 1 |
| fbxgw-r2/one | Freebox One revision 2 |
| fbxgw7-r1/full | Freebox v7 revision 1 |
| fbxgw8-r1/full | Freebox v8 revision 1 |
| fbxgw9-r1/full | Freebox v9 revision 1 |

<a id="discovery-using-http"></a>

#### Discovery using HTTP

If you can, avoid this method because it requires to use a hardcoded
address to retrieve API information.

If you make a HTTP get request on
<http://mafreebox.freebox.fr/api_version> you can get the same API
information as provided in mDNS.

**Example request**:

```http
GET /api_version HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```json
{
   "uid": "23b86ec8091013d668829fe12791fdab",
   "device_name": "Freebox Server",
   "box_model": "fbxgw7-r1/full",
   "box_model_name": "Freebox v7 (r1)",
   "api_version": "16.0",
   "api_base_url": "/api/",
   "api_domain": "example.fbxos.fr",
   "https_available": true,
   "https_port": 3615
}
```

Only the fields available to build the API request URL (see below) are
available if you connect remotely.

<a id="discovery-using-https"></a>

#### Discovery using HTTPS

Discovery using HTTPS works the same as discovery on HTTP. You can do an HTTP
GET request on <https://mafreebox.freebox.fr/api_version> . You need to validate
the certificate as explained below in [HTTPS access](00_index.md#https-access).

Discovery using HTTPS is preferred to HTTP discovery if you can’t use mDNS. You
MUST implement the certificate validation in your app in order to use the API.

<a id="building-the-api-request-url"></a>

### Building the API request URL

Once you’ve discovered a Freebox on the local network you can access
the API at the following URL:

```default
https://[api_domain]:[freebox_port]/[api_base_url]/v[major_api_version]/[api_url]
```

or for local access

<https://mafreebox.freebox.fr/[api_base_url]/v[major_api_version]/[api_url>]

**Example**:

```default
https://example.fbxos.fr:3615/api/v16/login/
```

<a id="remote-connection-port-change-discovery"></a>

<a id="remote-port-dns"></a>

### Remote connection port change discovery

When the https connection fails to a previously recorded <https://[api_domain]:[https_port>], you should attempt to discover if https_port has changed. This can happen either automatically (port is no longer valid), or manually if the user decided to change the port.

The https port is announced in a DNS “_https._tcp” **SRV** record. For example, for domain [example.fbxos.fr], the SRV record will be:

```default
# _service._proto.name.      TTL class SRV priority weight port  target
_https._tcp.example.fbxos.fr 300 IN    SRV 13       37     12345 example.fbxos.fr
```

Here, only the “port” field of the SRV record is relevant, i.e **12345**. The SRV field is only populated for the https port, and only for the [api_domain] field of the API information.

Port change discovery is important to maintain remote connectability.

<a id="api-conventions"></a>

### API conventions

Most API uses the [REST architecture](http://en.wikipedia.org/wiki/Representational_State_Transfer), pay
attention to the http methods used for each request.

For requests with a body, you must use “application/json”
content-type unless otherwise stated.

The API response is always a JSON object using utf8 encoding.

<a id="APIResponse"></a>

#### Objet APIResponse

<a id="APIResponse.success"></a>

**`success boolean Read-only`**

indicates if the request was successful

<a id="APIResponse.result"></a>

**`result object Read-only`**

the result of the request.

(It may be omitted if the request does not expect any result)

<a id="APIResponse.error_code"></a>

**`error_code string Read-only`**

In case of request error, this error_code provides information
about the error.

The possible error_code values are documented for each API.

<a id="APIResponse.msg"></a>

**`msg string Read-only`**

In cas of error, provides a French error message relative to the
error

**Successful response example**

```json
{
   success: true,
   result: {
      logged_in: false,
      challenge: "WpsbHdkBpRpHLMGQHZ1ri1uUqa4ce6Dw"
   }
}
```

**Error response example**

```json
{
   msg: "Requête invalide",
   success: false,
   error_code: "invalid_request"
}
```

The HTTP response code can also be used to error reason, for instance
if you attempt to access to an API with invalid credential you will
get a 403 error, or if you attempt to call an API with an invalid path
you will get a 404 error.

<a id="https-access"></a>

<a id="id1"></a>

## HTTPS Access

Each Freebox is now automatically assigned a random domain name (api_domain),
and an associated TLS certificate to enable secure access to API.

This is enabled by default and all applications MUST now use HTTPS to access
the api. Unsecure access will be removed at some point.

Certificates used for HTTPS access are emitted by either ‘Freebox ECC Root CA’
in case of ECDSA access, or ‘Freebox Root CA’ in case of RSA.

You must validate the certificate chain, by using the following
Root CA certificates:

**Freebox ECC Root CA**

```lua
-----BEGIN CERTIFICATE-----
MIICWTCCAd+gAwIBAgIJAMaRcLnIgyukMAoGCCqGSM49BAMCMGExCzAJBgNVBAYT
AkZSMQ8wDQYDVQQIDAZGcmFuY2UxDjAMBgNVBAcMBVBhcmlzMRMwEQYDVQQKDApG
cmVlYm94IFNBMRwwGgYDVQQDDBNGcmVlYm94IEVDQyBSb290IENBMB4XDTE1MDkw
MTE4MDIwN1oXDTM1MDgyNzE4MDIwN1owYTELMAkGA1UEBhMCRlIxDzANBgNVBAgM
BkZyYW5jZTEOMAwGA1UEBwwFUGFyaXMxEzARBgNVBAoMCkZyZWVib3ggU0ExHDAa
BgNVBAMME0ZyZWVib3ggRUNDIFJvb3QgQ0EwdjAQBgcqhkjOPQIBBgUrgQQAIgNi
AASCjD6ZKn5ko6cU5Vxh8GA1KqRi6p2GQzndxHtuUmwY8RvBbhZ0GIL7bQ4f08ae
JOv0ycWjEW0fyOnAw6AYdsN6y1eNvH2DVfoXQyGoCSvXQNAUxla+sJuLGICRYiZz
mnijYzBhMB0GA1UdDgQWBBTIB3c2GlbV6EIh2ErEMJvFxMz/QTAfBgNVHSMEGDAW
gBTIB3c2GlbV6EIh2ErEMJvFxMz/QTAPBgNVHRMBAf8EBTADAQH/MA4GA1UdDwEB
/wQEAwIBhjAKBggqhkjOPQQDAgNoADBlAjA8tzEMRVX8vrFuOGDhvZr7OSJjbBr8
gl2I70LeVNGEXZsAThUkqj5Rg9bV8xw3aSMCMQCDjB5CgsLH8EdZmiksdBRRKM2r
vxo6c0dSSNrr7dDN+m2/dRvgoIpGL2GauOGqDFY=
-----END CERTIFICATE-----
```

**Freebox Root CA**

```lua
-----BEGIN CERTIFICATE-----
MIIFmjCCA4KgAwIBAgIJAKLyz15lYOrYMA0GCSqGSIb3DQEBCwUAMFoxCzAJBgNV
BAYTAkZSMQ8wDQYDVQQIDAZGcmFuY2UxDjAMBgNVBAcMBVBhcmlzMRAwDgYDVQQK
DAdGcmVlYm94MRgwFgYDVQQDDA9GcmVlYm94IFJvb3QgQ0EwHhcNMTUwNzMwMTUw
OTIwWhcNMzUwNzI1MTUwOTIwWjBaMQswCQYDVQQGEwJGUjEPMA0GA1UECAwGRnJh
bmNlMQ4wDAYDVQQHDAVQYXJpczEQMA4GA1UECgwHRnJlZWJveDEYMBYGA1UEAwwP
RnJlZWJveCBSb290IENBMIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEA
xqYIvq8538SH6BJ99jDlOPoyDBrlwKEp879oYplicTC2/p0X66R/ft0en1uSQadC
sL/JTyfgyJAgI1Dq2Y5EYVT/7G6GBtVH6Bxa713mM+I/v0JlTGFalgMqamMuIRDQ
tdyvqEIs8DcfGB/1l2A8UhKOFbHQsMcigxOe9ZodMhtVNn0mUyG+9Zgu1e/YMhsS
iG4Kqap6TGtk80yruS1mMWVSgLOq9F5BGD4rlNlWLo0C3R10mFCpqvsFU+g4kYoA
dTxaIpi1pgng3CGLE0FXgwstJz8RBaZObYEslEYKDzmer5zrU1pVHiwkjsgwbnuy
WtM1Xry3Jxc7N/i1rxFmN/4l/Tcb1F7x4yVZmrzbQVptKSmyTEvPvpzqzdxVWuYi
qIFSe/njl8dX9v5hjbMo4CeLuXIRE4nSq2A7GBm4j9Zb6/l2WIBpnCKtwUVlroKw
NBgB6zHg5WI9nWGuy3ozpP4zyxqXhaTgrQcDDIG/SQS1GOXKGdkCcSa+VkJ0jTf5
od7PxBn9/TuN0yYdgQK3YDjD9F9+CLp8QZK1bnPdVGywPfL1iztngF9J6JohTyL/
VMvpWfS/X6R4Y3p8/eSio4BNuPvm9r0xp6IMpW92V8SYL0N6TQQxzZYgkLV7TbQI
Hw6v64yMbbF0YS9VjS0sFpZcFERVQiodRu7nYNC1jy8CAwEAAaNjMGEwHQYDVR0O
BBYEFD2erMkECujilR0BuER09FdsYIebMB8GA1UdIwQYMBaAFD2erMkECujilR0B
uER09FdsYIebMA8GA1UdEwEB/wQFMAMBAf8wDgYDVR0PAQH/BAQDAgGGMA0GCSqG
SIb3DQEBCwUAA4ICAQAZ2Nx8mWIWckNY8X2t/ymmCbcKxGw8Hn3BfTDcUWQ7GLRf
MGzTqxGSLBQ5tENaclbtTpNrqPv2k6LY0VjfrKoTSS8JfXkm6+FUtyXpsGK8MrLL
hZ/YdADTfbbWOjjD0VaPUoglvo2N4n7rOuRxVYIij11fL/wl3OUZ7GHLgL3qXSz0
+RGW+1oZo8HQ7pb6RwLfv42Gf+2gyNBckM7VVh9R19UkLCsHFqhFBbUmqwJgNA2/
3twgV6Y26qlyHXXODUfV3arLCwFoNB+IIrde1E/JoOry9oKvF8DZTo/Qm6o2KsdZ
dxs/YcIUsCvKX8WCKtH6la/kFCUcXIb8f1u+Y4pjj3PBmKI/1+Rs9GqB0kt1otyx
Q6bqxqBSgsrkuhCfRxwjbfBgmXjIZ/a4muY5uMI0gbl9zbMFEJHDojhH6TUB5qd0
JJlI61gldaT5Ci1aLbvVcJtdeGhElf7pOE9JrXINpP3NOJJaUSueAvxyj/WWoo0v
4KO7njox8F6jCHALNDLdTsX0FTGmUZ/s/QfJry3VNwyjCyWDy1ra4KWoqt6U7SzM
d5jENIZChM8TnDXJzqc+mu00cI3icn9bV9flYCXLTIsprB21wVSMh0XeBGylKxeB
S27oDfFq04XSox7JM9HdTt2hLK96x1T7FpFrBTnALzb7vHv9MhXqAT90fPR/8A==
-----END CERTIFICATE-----
```

If you want your app to work in Italy, in addition to changing the default
domain, you should trust this ECC Root CA:

```lua
-----BEGIN CERTIFICATE-----
MIICOjCCAcCgAwIBAgIUI0Tu7zsrBJACQIZgLMJobtbdNn4wCgYIKoZIzj0EAwIw
TDELMAkGA1UEBhMCSVQxDjAMBgNVBAgMBUl0YWx5MQ4wDAYDVQQKDAVJbGlhZDEd
MBsGA1UEAwwUSWxpYWRib3ggRUNDIFJvb3QgQ0EwHhcNMjAxMTI3MDkzODEzWhcN
NDAxMTIyMDkzODEzWjBMMQswCQYDVQQGEwJJVDEOMAwGA1UECAwFSXRhbHkxDjAM
BgNVBAoMBUlsaWFkMR0wGwYDVQQDDBRJbGlhZGJveCBFQ0MgUm9vdCBDQTB2MBAG
ByqGSM49AgEGBSuBBAAiA2IABMryJyb2loHNAioY8IztN5MI3UgbVHVP/vZwcnre
ZvJOyDvE4HJgIti5qmfswlnMzpNbwf/MkT+7HAU8jJoTorRm1wtAnQ9cWD3Ebv79
RPwtjjy3Bza3SgdVxmd6fWPUKaNjMGEwHQYDVR0OBBYEFDUij/4lpoJ+kOXRyrcM
jf2RPzOqMB8GA1UdIwQYMBaAFDUij/4lpoJ+kOXRyrcMjf2RPzOqMA8GA1UdEwEB
/wQFMAMBAf8wDgYDVR0PAQH/BAQDAgGGMAoGCCqGSM49BAMCA2gAMGUCMQC6eUV1
pFh4UpJOTc1JToztN4ttnQR6rIzxMZ6mNCe+nhjkohWp24pr7BpUYSbEizYCMAQ6
LCiBKV2j7QQGy7N1aBmdur17ZepYzR1YV0eI+Kd978aZggsmhjXENQYVTmm/XA==
-----END CERTIFICATE-----
```

and this RSA Root CA:

```lua
-----BEGIN CERTIFICATE-----
MIIFiTCCA3GgAwIBAgIUTXoJE/kJnSKpxk5FjcmqmGah9zcwDQYJKoZIhvcNAQEL
BQAwTDELMAkGA1UEBhMCSVQxDjAMBgNVBAgMBUl0YWx5MQ4wDAYDVQQKDAVJbGlh
ZDEdMBsGA1UEAwwUSWxpYWRib3ggUlNBIFJvb3QgQ0EwHhcNMjAxMTI3MDkzODEy
WhcNNDAxMTIyMDkzODEyWjBMMQswCQYDVQQGEwJJVDEOMAwGA1UECAwFSXRhbHkx
DjAMBgNVBAoMBUlsaWFkMR0wGwYDVQQDDBRJbGlhZGJveCBSU0EgUm9vdCBDQTCC
AiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBANXKZSyCmix6jt7jUmaCP4XF
caF4azeYZuA8A4sWQmQXRWTDj8oNClE5w7zo5qUYzHIBOubKY7hhIU7RXYR5Bdny
arNRoo5ZBplgEkv3G00IgXY2/lCywPQ8WorAn0k/uaRce239r6EkGC3fxCA3Asnc
q9lNkUoWaf0GktJai0DuW7bNY8cq+vzZpy/36ey0LQ4OoehfiA6vlUTVWakpjecJ
ller1RfVlgEH26wnerGge3LYBZv27XiahCft54AQLxRY3H/z8XpKsPnJJrrhEvSo
2p64Bd+g7ZbzCdeakrypjVC/eWn14UzbcBVgh0p4F4990LuGxLVqyh6XcZOSSi01
4fpca5xPDCiohEX7ehMLpdURbhKzPj17IpwTmonfVmxkvV8rca1PqhDPEOouwPtc
M55eCgtwgSBeDznFKD7s+az/SZYC16GTgyXTCd2lId/J1unZ4pdzNVMAglTpnGgz
eQkHvfcVYdJj49tOtW0OpSPBiNIC6LCVY9wtH5dRMm0k+A8QDP+9HQaOs3LIUMwu
WGePw6r+eXUYw/2yO0z3zI/63hOpzZVixW+T7h3SY5B+sTrxR9fRD1oyk/rPV4I3
X5mZnyzSowjcN3+hSkGIZBleMO3CHaYleIf1/9HHhCJCVeeJ4kwEWY18Z0A+ohFh
D/dipgwmLCDH1/irDT4pAgMBAAGjYzBhMB0GA1UdDgQWBBTcW1RrTVIizaqkrkTI
CSw86qDJkTAfBgNVHSMEGDAWgBTcW1RrTVIizaqkrkTICSw86qDJkTAPBgNVHRMB
Af8EBTADAQH/MA4GA1UdDwEB/wQEAwIBhjANBgkqhkiG9w0BAQsFAAOCAgEAOfi6
fCuVLJD+vttO34cdB3i5hofmNrzgLh/spnwdm4y9EvvVqDvLdVLEIbvKf0QEcW0Y
dwP1BgmKwwHVv9YydHov8Jr4ANoGGXJnPLPcYDhRnixYEQmlTwSL/CLUcQ2hQWXx
Oc0k1jJB7uk6TPdX2YJyW4NpIcwI2sa5Dg/L8PqM0/pMYnMyG1hBwUc2M2qg3qTJ
zeiYT9zBHxS/JXA40yH4g9NzcFisVuYrfmINb11GmeqClm2OWehSdgdv9tEph3NW
ntJTENRrDvuj/pGZsnbofzgHNN6/nanymmrEPxG+xUGLIAW7zFndTKityhJ9FRqF
ultoZR2D19hh+n1277TSCPRJzUpq9rrfiqukjua3UjBzEvevnmSbLs1bXcNAxFYN
oZZ2euHoBv+E3BHjGik4RUkEJYtf5Xh+iffk4zTMfKBERn40fB7yF1xzxyoziltL
VxfueF9V6N7qjo5Ia7kiShXXsB+QdQdweuxWm1pPYmMbfTxNEqFUs3GhwEjzLaJc
cJOedwCT4ntbyCcTQaRlDL8QFjdE4gNm2ZaoG+gqGTLPS55H+ZvLsgUCiR5YY44N
G2Gkv4w/V/eB3eAvd5lgm6oOe8ehdr5JdpD6wnW2GOHs4SBdBo6yR+4RgEimNmgF
Yu11tlZsB2Iw/TT1EyPVb5z6tK4wUgWLNFAvjXU=
-----END CERTIFICATE-----
```

<a id="authentication"></a>

## Authentication

Unless otherwise stated API access must be authenticated using the
procedure described in the following document

<a id="websocket-api"></a>

## WebSocket API

<a id="api-list"></a>

## API List

<a id="air-media"></a>

### Air Media

<a id="calls-contacts"></a>

### Calls / Contacts

<a id="configuration"></a>

### Configuration

<a id="diagnostics"></a>

### Diagnostics

<a id="downloads"></a>

### Downloads

<a id="file-system-api"></a>

### File System Api

<a id="home"></a>

### Home

<a id="language"></a>

### Language

<a id="notification-658"></a>

### Notification

<a id="parental-filter"></a>

### Parental filter

<a id="player-devices"></a>

### Player devices

<a id="pvr"></a>

### PVR

<a id="rrd"></a>

### RRD

<a id="standby-742"></a>

### Standby

<a id="storage"></a>

### Storage

<a id="sfp-787"></a>

### SFP

<a id="update-796"></a>

### Update

<a id="virtual-machines"></a>

### Virtual machines
