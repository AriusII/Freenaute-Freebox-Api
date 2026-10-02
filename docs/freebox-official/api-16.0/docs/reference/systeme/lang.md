<a id="language-support"></a>

<a id="lang-api"></a>

# Language support

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#language-support)

## Navigation

- [Language support Object](#language-support-object)
- [Get language status](#get-language-status)
- [Set language](#set-language)


With this API you can fetch the list of supported languages on the Freebox, and change the current language.

<a id="language-support-object"></a>

## Language support Object

<a id="LanguageSupport"></a>

### Objet LanguageSupport

<a id="LanguageSupport.lang"></a>

**`lang enum`**

Currently configured language.

<a id="LanguageSupport.avalaible"></a>

**`avalaible [] array of string Read-only`**

List of supported languages, in iso 639-3 (alpha-3) format, used for changing the language.

<a id="get-language-status"></a>

## Get language status

<a id="get--api-v8-lang-"></a>

**`GET /api/v8/lang/`**

Get the current language in iso 639-3 (alpha-3) format, as well as the list of supported languages.

**Example request**:

```http
GET /api/v8/lang HTTP/1.1
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
      "lang": "fra",
      "avalaible": [
         "fra",
         "eng"
      ]
    }
}
```

<a id="set-language"></a>

## Set language

<a id="post--api-v8-lang-"></a>

**`POST /api/v8/lang/`**

Set the current language.

**Example request**:

```http
POST /api/v8/lang HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
   "lang": "eng"
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
}
```
