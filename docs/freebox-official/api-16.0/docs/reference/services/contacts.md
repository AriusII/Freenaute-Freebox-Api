<a id="contacts"></a>

# Contacts

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#contacts)

## Navigation

- [Contacts Errors](#contacts-errors)
- [Contact Objects](#contact-objects)
- [Contact API](#contact-api)
- [Contact Related objects API](#contact-related-objects-api)


The contact API allow to interact with the contact list stored on the
Freebox

<a id="contacts-errors"></a>

## Contacts Errors

When attempting to access the contact API, you may encounter the
following errors:

| error_code | Description |
| --- | --- |
| noent | no entry with this id |
| exists | an entry already exists |
| no_match | no entry matched your request |

<a id="contact-objects"></a>

## Contact Objects

<a id="contact-entry"></a>

### Contact Entry

Contact entries have the following properties

<a id="ContactEntry"></a>

#### Objet ContactEntry

<a id="ContactEntry.id"></a>

**`id int`**

contact id

<a id="ContactEntry.display_name"></a>

**`display_name string`**

contact display name

<a id="ContactEntry.first_name"></a>

**`first_name string`**

contact first name

<a id="ContactEntry.last_name"></a>

**`last_name string`**

contact last name

<a id="ContactEntry.company"></a>

**`company string`**

contact company name

<a id="ContactEntry.photo_url"></a>

**`photo_url string`**

contact photo URL

*NOTE* the photo URL can be embedded (for instance
“<data:image/jpeg;base64,/9j/4AA> [ … ]”)

<a id="ContactEntry.last_update"></a>

**`last_update timestamp`**

contact last modification timestamp

<a id="ContactEntry.notes"></a>

**`notes string`**

contact last modification timestamp

<a id="ContactEntry.addresses"></a>

**`addresses [] array of ContactAddress`**

list of contact postal addresses

<a id="ContactEntry.emails"></a>

**`emails [] array of ContactEmail`**

list of contact email addresses

<a id="ContactEntry.numbers"></a>

**`numbers [] array of ContactNumber`**

list of contact phone numbers

<a id="ContactEntry.urls"></a>

**`urls [] array of ContactUrl`**

list of contact URL

<a id="contact-number"></a>

### Contact Number

Contact number have the following properties

<a id="ContactNumber"></a>

#### Objet ContactNumber

<a id="ContactNumber.id"></a>

**`id int`**

address id

<a id="ContactNumber.contact_id"></a>

**`contact_id int`**

id of the related contact

<a id="ContactNumber.type"></a>

**`type enum`**

Type of number

| Type | Description |
| --- | --- |
| fixed | fixed phone |
| mobile | mobile phone |
| work | work |
| fax | fax |
| other | other |

<a id="ContactNumber.number"></a>

**`number string`**

<a id="ContactNumber.is_default"></a>

**`is_default bool`**

is this number the preferred contact phone number

<a id="ContactNumber.is_own"></a>

**`is_own bool`**

is this number the Freebox owner number

<a id="contact-address"></a>

### Contact Address

Contact address have the following properties

<a id="ContactAddress"></a>

#### Objet ContactAddress

<a id="ContactAddress.id"></a>

**`id int`**

address id

<a id="ContactAddress.contact_id"></a>

**`contact_id int`**

id of the related contact

<a id="ContactAddress.type"></a>

**`type enum`**

Type of email

| Type | Description |
| --- | --- |
| home | home address |
| work | work address |
| other | other |

<a id="ContactAddress.number"></a>

**`number string`**

<a id="ContactAddress.street"></a>

**`street string`**

<a id="ContactAddress.street2"></a>

**`street2 string`**

<a id="ContactAddress.city"></a>

**`city string`**

<a id="ContactAddress.zipcode"></a>

**`zipcode string`**

<a id="ContactAddress.country"></a>

**`country string`**

<a id="contact-url"></a>

### Contact Url

Contact URL have the following properties

<a id="ContactUrl"></a>

#### Objet ContactUrl

<a id="ContactUrl.id"></a>

**`id int`**

address id

<a id="ContactUrl.contact_id"></a>

**`contact_id int`**

id of the related contact

<a id="ContactUrl.type"></a>

**`type enum`**

Type of URL

| Type | Description |
| --- | --- |
| profile | profile address |
| blog | blog address |
| site | website address |
| other | other |

<a id="ContactUrl.url"></a>

**`url string`**

URL address

<a id="contact-email"></a>

### Contact Email

Contact email have the following properties

<a id="ContactEmail"></a>

#### Objet ContactEmail

<a id="ContactEmail.id"></a>

**`id int`**

address id

<a id="ContactEmail.contact_id"></a>

**`contact_id int`**

id of the related contact

<a id="ContactEmail.type"></a>

**`type enum`**

Type of address

| Type | Description |
| --- | --- |
| home | home address |
| work | work address |
| other | other |

<a id="ContactEmail.email"></a>

**`email string`**

email address

<a id="contact-api"></a>

## Contact API

<a id="get-a-list-of-contacts"></a>

### Get a list of contacts

<a id="get--api-v8-contact-"></a>

**`GET /api/v8/contact/`**

Returns the collection of all [`ContactEntry`](contacts.md#ContactEntry "ContactEntry")

Parameters

- **start** (*int*) – Offset
- **limit** (*int*) – Limit of contact to return (-1 means no limit)
- **group_id** (*int*) – Return only the contacts that belong to this group

**Example request**:

```http
GET /api/v8/contact/ HTTP/1.1
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
    "result": [
        {
            "last_name": "Niel",
            "company": "Free",
            "photo_url": "data:image/jpeg;base64,/9j/4AA [ ... ]",
            "id": 2,
            "birthday": "",
            "last_update": 1363964483,
            "display_name": "",
            "emails": [
                {
                    "id": 2,
                    "contact_id": 2,
                    "type": "home",
                    "email": "rocket@launchpad.free"
                }
            ],
            "urls": [
                {
                    "id": 1,
                    "contact_id": 2,
                    "url": "http://www.free.fr/",
                    "type": "site"
                }
            ],
            "notes": "",
            "first_name": "Xavier"
        },

        [ ... ],

        {
            "last_name": "Mamie",
            "first_name": "Kipic",
            "company": "",
            "photo_url": "data:image/jpeg;base64,/9j/4A [ ... ] ",
            "id": 1,
            "birthday": "",
            "numbers": [
                {
                    "number": "0612345678",
                    "type": "fixed",
                    "id": 1,
                    "contact_id": 1,
                    "is_default": false,
                    "is_own": false
                }
            ],
            "last_update": 1363973599,
            "display_name": "Mamie",
            "emails": [
                {
                    "id": 1,
                    "contact_id": 1,
                    "type": "home",
                    "email": "mamie@example.org"
                }
            ],
            "urls": [
                {
                    "id": 3,
                    "contact_id": 1,
                    "url": "ftp://free.fr",
                    "type": "site"
                }
            ],
            "addresses": [
                {
                    "street2": "",
                    "type": "home",
                    "country": "France",
                    "id": 1,
                    "street": "8 rue du pont",
                    "contact_id": 1,
                    "city": "Paris",
                    "zipcode": "75008",
                    "number": "11"
                }
            ],
            "notes": ""
        }
    ]
}
```

<a id="access-a-given-contact-entry"></a>

### Access a given contact entry

<a id="get--api-v8-contact-id"></a>

**`GET /api/v8/contact/{id}`**

Returns the [`ContactEntry`](contacts.md#ContactEntry "ContactEntry") with the given id

**Example request**:

```http
GET /api/v8/contact/1 HTTP/1.1
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
      "last_name": "Mamie",
      "first_name": "Kipic",
      "company": "",
      "photo_url": "data:image/jpeg;base64,/9j/4A [ ... ] ",
      "id": 1,
      "birthday": "",
      "numbers": [
          {
              "number": "0612345678",
              "type": "fixed",
              "id": 1,
              "contact_id": 1,
              "is_default": false,
              "is_own": false
          }
      ],
      "last_update": 1363973599,
      "display_name": "Mamie",
      "emails": [
          {
              "id": 1,
              "contact_id": 1,
              "type": "home",
              "email": "mamie@example.org"
          }
      ],
      "urls": [
          {
              "id": 3,
              "contact_id": 1,
              "url": "ftp://free.fr",
              "type": "site"
          }
      ],
      "addresses": [
          {
              "street2": "",
              "type": "home",
              "country": "France",
              "id": 1,
              "street": "8 rue du pont",
              "contact_id": 1,
              "city": "Paris",
              "zipcode": "75008",
              "number": "11"
          }
      ],
      "notes": ""
    }
}
```

<a id="create-a-contact"></a>

### Create a contact

<a id="post--api-v8-contact-"></a>

**`POST /api/v8/contact/`**

Creates a new [`ContactEntry`](contacts.md#ContactEntry "ContactEntry")

**Example request**:

```http
POST /api/v8/contact/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "display_name": "Sandy Kilo",
   "first_name": "Sandy",
   "last_name":"Kilo"
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
        "last_name": "Kilo",
        "company": "",
        "photo_url": "",
        "id": 10,
        "birthday": "",
        "last_update": 1372433423,
        "display_name": "Sandy Kilo",
        "notes": "",
        "first_name": "Sandy"
    }
}
```

<a id="delete-a-contact"></a>

### Delete a contact

<a id="delete--api-v8-contact-id"></a>

**`DELETE /api/v8/contact/{id}`**

Deletes the [`ContactEntry`](contacts.md#ContactEntry "ContactEntry") with the given id.

**Example request**:

```http
DELETE /api/v8/contact/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true
}
```

<a id="update-a-contact-entry"></a>

### Update a contact entry

<a id="put--api-v8-contact-id"></a>

**`PUT /api/v8/contact/{id}`**

Updates the [`ContactEntry`](contacts.md#ContactEntry "ContactEntry") with the given id

**Example request**:

```http
PUT /api/v8/contact/4 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "company": "Freebox"
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
        "last_name": "Anderson",
        "company": "Freebox",
        "photo_url": "data:image/jpeg;base64,/9j/4AAQ [ ... ]",
        "id": 4,
        "birthday": "",
        "last_update": 1363977825,
        "display_name": "Thomas A. Anderson",
        "emails": [
            {
                "id": 3,
                "contact_id": 4,
                "type": "home",
                "email": "neo@matrix.com"
            }
        ],
        "notes": "",
        "first_name": "Thomas"
    }
}
```

<a id="contact-related-objects-api"></a>

## Contact Related objects API

Contact related entries such as phone numbers, addresses, URLs and
emails are all handled the same way.

Below we’ll document the numbers API, you can use the same calls with
addresses, URL and emails.

<a id="get-the-list-of-numbers-for-a-given-contact"></a>

### Get the list of numbers for a given contact

<a id="get--api-v8-contact-contact_id-[numbers|addresses|urls|emails]-"></a>

**`GET /api/v8/contact/{contact_id}/[numbers|addresses|urls|emails]/`**

Returns the collection of all [`ContactNumber`](contacts.md#ContactNumber "ContactNumber") for a
given contact

**Example request**:

```http
GET /api/v8/contact/4/numbers/ HTTP/1.1
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
    "result": [
        {
            "number": "+13374242",
            "type": "fixed",
            "id": 6,
            "contact_id": 4,
            "is_default": false,
            "is_own": false
        },
        {
            "number": "0611223344",
            "type": "mobile",
            "id": 5,
            "contact_id": 4,
            "is_default": false,
            "is_own": false
        }
    ]
}
```

<a id="access-a-given-contact-number"></a>

### Access a given contact number

<a id="get--api-v8-[number,address,url,email]-id"></a>

**`GET /api/v8/[number,address,url,email]/{id}`**

Returns the [`ContactNumber`](contacts.md#ContactNumber "ContactNumber") with the given id

**Example request**:

```http
GET /api/v8/number/6 HTTP/1.1
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
        "number": "+13374242",
        "type": "fixed",
        "id": 6,
        "contact_id": 4,
        "is_default": false,
        "is_own": false
    }
}
```

<a id="create-a-contact-number"></a>

### Create a contact number

<a id="post--api-v8-[number,address,url,email]-"></a>

**`POST /api/v8/[number,address,url,email]/`**

Creates the [`ContactNumber`](contacts.md#ContactNumber "ContactNumber")

**Example request**:

```http
POST /api/v8/number/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "contact_id":9,
   "number":"0144456789",
   "type":"fixed"
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
        "number": "0144456789",
        "type": "fixed",
        "id": 18,
        "contact_id": 9,
        "is_default": false,
        "is_own": false
    }
}
```

<a id="delete-a-contact-number"></a>

### Delete a contact number

<a id="delete--api-v8-[number,address,url,email]-id"></a>

**`DELETE /api/v8/[number,address,url,email]/{id}`**

Deletes the [`ContactNumber`](contacts.md#ContactNumber "ContactNumber") with the given id.

**Example request**:

```http
DELETE /api/v8/number/6 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
    "success": true
}
```

<a id="update-a-contact-number"></a>

### Update a contact number

<a id="put--api-v8-[number,address,url,email]-id"></a>

**`PUT /api/v8/[number,address,url,email]/{id}`**

Updates the [`ContactNumber`](contacts.md#ContactNumber "ContactNumber") with the given id

**Example request**:

```http
PUT /api/v8/number/5 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "number": "0655667788",
  "type": "mobile"
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
        "number": "0655667788",
        "type": "mobile",
        "id": 5,
        "contact_id": 4,
        "is_default": false,
        "is_own": false
    }
}
```
