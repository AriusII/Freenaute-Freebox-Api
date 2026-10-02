<a id="lcd-315"></a>

# LCD

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#lcd-315)

## Navigation

- [LCD Errors](#lcd-errors)
- [LCD Config](#lcd-config)
- [LCD config API](#lcd-config-api)


The lcd API allow you to control the Freebox lcd screen settings

<a id="lcd-errors"></a>

## LCD Errors

When attempting to access the lcd API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| inval | Invalid parameters |
| no_panel | No screen detected |
| setup | Unable to setup screen |
| notsup | Operation is not supported |

<a id="lcd-config"></a>

## LCD Config

LcdConfig has the following attributes:

<a id="LcdConfig"></a>

### Objet LcdConfig

<a id="LcdConfig.brightness"></a>

**`brightness int`**

the screen brightness (range from 0 to 100)

<a id="LcdConfig.orientation_forced"></a>

**`orientation_forced bool`**

is the screen orientation forced

<a id="LcdConfig.orientation"></a>

**`orientation int`**

the screen orientation angle

<a id="LcdConfig.hide_wifi_key"></a>

**`hide_wifi_key bool`**

hide wifi key information (including qrcode) - optional

<a id="LcdConfig.led_strip_enabled"></a>

**`led_strip_enabled bool`**

enable/disable led strip brightness - optional

<a id="LcdConfig.led_strip_brightness"></a>

**`led_strip_brightness int`**

led strip brightness (range from 0 to 100) - optional

<a id="LcdConfig.led_strip_animation"></a>

**`led_strip_animation enum`**

led strip animation - optional

<a id="LcdConfig.available_led_strip_animations"></a>

**`available_led_strip_animations [] array of enum Read-only`**

array containing what LED strip animations can be configured

<a id="LcdConfig.hide_status_led"></a>

**`hide_status_led bool`**

hide status LED (on supported Freebox models) - optional

<a id="LcdConfig.screensaver"></a>

**`screensaver enum`**

Configure the screensaver - optional

Only present on boxes that have has_lcd_screensaver set to true in their [`SystemConfig`](system.md#SystemConfig "SystemConfig") information.

Possible values are listed in the following table:

| Value | Description |
| --- | --- |
| disabled | Display always on |
| on | Screensaver enabled |
| night | Screensaver enabled during the night |

<a id="lcd-config-api"></a>

## LCD config API

<a id="get-the-current-lcd-configuration"></a>

### Get the current LCD configuration

<a id="get--api-v8-lcd-config-"></a>

**`GET /api/v8/lcd/config/`**

Get the [`LcdConfig`](lcd.md#LcdConfig "LcdConfig")

**Example request**:

```http
GET /api/v8/lcd/config/ HTTP/1.1
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
        "brightness": 100,
        "orientation": 0,
        "orientation_forced": false,
        "hide_wifi_key": false,
        "hide_led": false
    }
}
```

<a id="update-the-lcd-configuration"></a>

### Update the lcd configuration

<a id="put--api-v8-lcd-config-"></a>

**`PUT /api/v8/lcd/config/`**

Update the [`LcdConfig`](lcd.md#LcdConfig "LcdConfig")

**Example request**:

```http
PUT /api/v8/lcd/config/ HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "brightness": 50
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
        "brightness": 50,
        "orientation": 0,
        "orientation_forced": false
        "hide_wifi_key": false,
        "hide_led": false
    }
}
```
