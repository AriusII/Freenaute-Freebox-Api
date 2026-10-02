<a id="home-api"></a>

<a id="id1"></a>

# Home API

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#home-api)

## Navigation

- [List Home Adapters](#list-home-adapters)
- [Pair a new object](#pair-a-new-object)
- [Home Nodes](#home-nodes)
- [Home Nodes Values](#home-nodes-values)
- [Home Tileset](#home-tileset)
- [Alarm Tiles](#alarm-tiles)
- [Automation tiles](#automation-tiles)


The Home API allows you to access features related to home automation

<a id="list-home-adapters"></a>

## List Home Adapters

<a id="home-adapter-object"></a>

### Home Adapter Object

<a id="HomeAdapter"></a>

#### Objet HomeAdapter

HomeAdapter has the following attributes:

<a id="HomeAdapter.id"></a>

**`id int Read-only`**

this object id

<a id="HomeAdapter.icon_url"></a>

**`icon_url String Read-only`**

Url of the adapter icon

<a id="HomeAdapter.label"></a>

**`label String Read-only`**

The displayable name of this adapter

<a id="HomeAdapter.status"></a>

**`status enum`**

Adapter status

| status | Description |
| --- | --- |
| unplugged | The adapter is not available |
| disabled | The adapter has been disabled |
| active | the adapter is active |

<a id="HomeAdapter.type"></a>

**`type AdapterType Read-only`**

The technical type of this adapter.

<a id="HomeAdapter.props"></a>

**`props Map`**

Technical data related to this adapter, useful fo developers

<a id="get-home-adapters-list"></a>

### Get Home Adapters List

<a id="get--api-v8-home-adapters"></a>

**`GET /api/v8/home/adapters`**

Retrieve the list of registered [`HomeAdapter`](home.md#HomeAdapter "HomeAdapter"). A new adapters appear when the user plugs a new home automation dongle.

**Example request**:

```http
GET /api/v8/home/adapters HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "result": [
    {
      "icon_url": "http://images.com/adapter_dm.png",
      "id": 1,
      "label": "Gestionnaire de caméra",
      "status": "active",
      "type": {
        "name": "adapter::cam"
      }
    },
    {
      "icon_url": "http://images.com/adapter_dm.png",
      "id": 2,
      "label": "Réseau Rts",
      "status": "active",
      "type": {
        "name": "adapter::rts"
      }
    },
    {
      "icon_url": "http://images.com/adapter_dm.png",
      "id": 3,
      "label": "Réseau IOHome",
      "props": {
        "Addr": 160,
        "SomfyId": "00:00:00:00"
      },
      "status": "active",
      "type": {
        "name": "adapter::ios"
      }
    },
    {
      "icon_url": "http://images.com/adapter_dm.png",
      "id": 4,
      "label": "Réseau Domus",
      "props": {
        "Network ID": 50791
      },
      "status": "active",
      "type": {
        "name": "adapter::domus"
      }
    }
  ],
  "success": true
}
```

<a id="get-a-home-adapter"></a>

### Get a Home Adapter

<a id="get--api-v8-home-adapters-id"></a>

**`GET /api/v8/home/adapters/{id}`**

Fetch information about a single [`HomeAdapter`](home.md#HomeAdapter "HomeAdapter").

**Example request**:

```http
GET /api/v8/home/adapters/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "result": {
      "icon_url": "http://images.com/adapter_dm.png",
      "id": 1,
      "label": "Gestionnaire de caméra",
      "status": "active",
      "type": {
        "name": "adapter::cam"
      }
  }
  "success": true
}
```

<a id="change-a-home-adapter-status"></a>

### Change a Home Adapter status

<a id="put--api-v8-home-adapters-id"></a>

**`PUT /api/v8/home/adapters/{id}`**

Change the status of a [`HomeAdapter`](home.md#HomeAdapter "HomeAdapter").

**Example request**:

```http
PUT /api/v8/home/adapters/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "status": "disabled"
}
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

<a id="pair-a-new-object"></a>

## Pair a new object

<a id="pairing-step"></a>

### Pairing Step

<a id="HomePairingStep"></a>

#### Objet HomePairingStep

This represents a pairing process step.

<a id="HomePairingStep.fields"></a>

**`fields [] array of HomePairingStepField Read-only`**

A collection of ui elements to display.

<a id="HomePairingStep.icon_url"></a>

**`icon_url String Read-only`**

The url of an image which represents this step.

<a id="HomePairingStep.pageid"></a>

**`pageid int Read-only`**

The identifier of this step.

<a id="HomePairingStep.refresh"></a>

**`refresh int`**

The delay in millisecond after which to request a new step update.

<a id="HomePairingStep.session"></a>

**`session int Read-only`**

The id of this session process.

<a id="HomePairingStepField"></a>

#### Objet HomePairingStepField

<a id="HomePairingStepField.widget"></a>

**`widget enum Read-only`**

The type of ui element to display.

| widget | Description |
| --- | --- |
| label | A simple text field |
| select | A selectable list item |
| button | A clickable button |
| display_qrcode | A qrcode |
| input | An input text field |
| checkbox | A checkable button |
| progress | A progress bar |
| bar_button_left | A button displayed at the left of the bottom nav bar |
| bar_button_right | A button displayed at the right of the bottom nav bar |

<a id="HomePairingStepField.text"></a>

**`text string Read-only`**

The data to use with the displayed widget.

| widget | text usage |
| --- | --- |
| label | The label text |
| select | The item caption |
| button | The button caption |
| display_qrcode | The data to encode in the qrcode |
| input | The default text |
| checkbox | The button caption |
| progress | The progress value, in percent, as int |
| bar_button_left | The button caption |
| bar_button_right | The button caption |

<a id="start-pairing"></a>

### Start Pairing

<a id="post--api-v8-home-pairing-adapter_id"></a>

**`POST /api/v8/home/pairing/{adapter_id}`**

Start the pairing process on a specific [`HomeAdapter`](home.md#HomeAdapter "HomeAdapter").

op: start

type: the type of object to pair. This parameter is only relevant for the domus adapter.

| type | Description |
| --- | --- |
| node::domus::freebox::secmod | Pair the security module |
| node::domus::sercomm::pir | Pair a movement detector |
| node::domus::sercomm::keyfob | Pair an alarm remote control |
| node::domus::sercomm::doorswitch | Pair an opening detector |

**Example request**:

```http
POST /api/v8/home/pairing/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "op": "start",
  "type": "node::domus::freebox::secmod"
}
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

<a id="current-pairing-step"></a>

### Current Pairing Step

<a id="get--api-v8-home-pairing-adapter_id"></a>

**`GET /api/v8/home/pairing/{adapter_id}`**

Get the current [`HomePairingStep`](home.md#HomePairingStep "HomePairingStep") on a specific [`HomeAdapter`](home.md#HomeAdapter "HomeAdapter")

**Example request**:

```http
GET /api/v8/home/pairing/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "result" : {
      "fields" : [
          {
            "text" : "Veuillez vérifier que votre wifi est bien activé.",
            "widget" : "label"
          }
        ],
      "icon_url" : "/resources/images/home/pairing/wifi.png",
      "pageid" : 2,
      "refresh" : 1000,
      "session" : 62328
    },
  "success" : true
}
```

<a id="next-step"></a>

### Next Step

<a id="post--api-v8-home-pairing-adapter_id--variante-2"></a>

**`POST /api/v8/home/pairing/{adapter_id}`**

Send current step result and get the next step in the process. Call this when the user clicks on a button, bar_button_left, bar_button_right or a select item.

field is a list of value corresponding to the current page widgets.

| widget | value in fields |
| --- | --- |
| label | null |
| select | The index of the selected item, null if none selected |
| button | true if the button has been clicked, false otherwise |
| display_qrcode | null |
| input | The text entered |
| checkbox | true if checked, false otherwise |
| progress | The progress value, in percent, as int |
| bar_button_left | The button caption |
| bar_button_right | The button caption |

**Example request**:

```http
POST /api/v8/home/pairing/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "op": "next",
  "session": "659887",
  "pageid": "1".
  "fields": [null,null,"mon texte", false, true]
}
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "result" : {
      "fields" : [
          {
            "text" : "Veuillez vérifier que votre wifi est bien activé.",
            "widget" : "label"
          }
        ],
      "icon_url" : "/resources/images/home/pairing/wifi.png",
      "pageid" : 2,
      "refresh" : 1000,
      "session" : 62328
    },
  "success" : true
}
```

<a id="stop-pairing"></a>

### Stop Pairing

<a id="post--api-v8-home-pairing-adapter_id--variante-3"></a>

**`POST /api/v8/home/pairing/{adapter_id}`**

Stop the pairing process on a specific [`HomeAdapter`](home.md#HomeAdapter "HomeAdapter").

op: stop
session: the id of the pairing session to stop

**Example request**:

```http
POST /api/v8/home/pairing/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "op": "stop",
  "session": 15645
}
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

<a id="home-nodes"></a>

## Home Nodes

Acces objects connected to the automation network.

<a id="home-node-object"></a>

### Home Node Object

<a id="HomeNode"></a>

#### Objet HomeNode

<a id="HomeNode.adapter"></a>

**`adapter int Read-only`**

Id of the [`HomeAdapter`](home.md#HomeAdapter "HomeAdapter") this node is connected to.

<a id="HomeNode.category"></a>

**`category String Read-only`**

???

<a id="HomeNode.id"></a>

**`id int Read-only`**

Id of this node.

<a id="HomeNode.label"></a>

**`label String Read-only`**

Displayable name of this node

<a id="HomeNode.name"></a>

**`name String Read-only`**

Technical name of this node

<a id="HomeNode.show_endpoints"></a>

**`show_endpoints [] array of HomeNodeEndpoint Read-only`**

Endpoints exposed by this node

<a id="HomeNode.signal_links"></a>

**`signal_links [] array of HomeNodeLink Read-only`**

Links from other objects to this node signals

<a id="HomeNode.slot_links"></a>

**`slot_links [] array of HomeNodeLink Read-only`**

Links from other objects to this node slots

<a id="HomeNode.status"></a>

**`status enum Read-only`**

Status of this node

| status | Description |
| --- | --- |
| unreachable | The adapter is not reachable |
| disabled | The node has been disabled |
| active | The node is connected |
| unpaired | The node has not been paired to any network |

<a id="HomeNode.type"></a>

**`type HomeNodeType Read-only`**

Node type info

<a id="HomeNodeEndpoint"></a>

#### Objet HomeNodeEndpoint

<a id="HomeNodeEndpoint.category"></a>

**`category String Read-only`**

???

<a id="HomeNodeEndpoint.ep_type"></a>

**`ep_type enum Read-only`**

The endpoint type

| ep_type | Description |
| --- | --- |
| signal | The endpoint outputs an information |
| slot | A endpoint that controls the object |

<a id="HomeNodeEndpoint.id"></a>

**`id int Read-only`**

The endpoint id

<a id="HomeNodeEndpoint.visibility"></a>

**`visibility enum Read-only`**

Visibility level of this endpoint

| visibility | Description |
| --- | --- |
| internal | For internal use only, never exposed |
| normal | The endpoint is available for scenarii but does not display info to the user |
| dashboard | The endpoint expose data that can be displayed on UI |

<a id="HomeNodeEndpoint.access"></a>

**`access enum Read-only`**

Access mode of this endpoint

| access | Description |
| --- | --- |
| r | Read only |
| w | Write only |
| rw | Read and write |

<a id="HomeNodeType"></a>

#### Objet HomeNodeType

<a id="HomeNodeType.icon"></a>

**`icon String Read-only`**

The node icon name or url

<a id="HomeNodeType.label"></a>

**`label String Read-only`**

The node displayable type

<a id="propriete-sans-ancre-home-31"></a>

**`label name Read-only`**

The node type technical name

<a id="HomeNodeType.physical"></a>

**`physical boolean Read-only`**

True when the node is an actual connected object, false when it’s a virtual node

<a id="HomeNodeEndpointUi"></a>

#### Objet HomeNodeEndpointUi

<a id="HomeNodeEndpointUi.display"></a>

**`display enum Read-only`**

Display mode of this data

| display | Description |
| --- | --- |
| text | This displays the endpoint value as text. Read access is always allowed when “text” is used. When write access is allowed, the text may be editable on user request. When the “unit” entry is present and not null, it specifies the physical unit associated to the endpoint value. |
| icon | This displays the icon fetched from “icon_url” with % being replaced by the string representation of the endpoint value. For *string* value type, the % is replaced by the endpoint value. For *int* and *float* value types, this requires an “icon_ranges” array of threshold values. The % is replaced by the index in the “range” array which is just below the endpoint value. For *boolean* value type, the % is replaced by “on” or “off”. When the “value” is null, the % is replaced by the empty string. Read access is always allowed when “icon” is used. Write access is not used. |
| button | This displays a push button. Write access is always allowed when “button” is used. A null value must be send to the endpoint when pushed. |
| slider | This displays a slider with the cursor located according to the endpoint value in the range specified by “range”. Read access is always allowed when “slider” is used. When write access is allowed, the cursor may be moved by the user. When write access is not allowed it may be displayed as a progress bar. |
| toggle | This displays an on/off switch. Read access is always allowed when “switch” is used. When write access is allowed, switch may be toggled by the user. A *boolean* value must be send to the endpoint when toggled. |
| color | This displays a color value. The value type is an *int* representing the RGB color. Read access is always allowed when “color” is used. |
| warning | This display the icon fetched from “icon_url” when the value condition is true. For *boolean* value type, the value is the condition. For *int* and *float* value types, this requires a “range” of size 2. If the value is within the range, the condition is true. |

<a id="HomeNodeEndpointUi.icon_url"></a>

**`icon_url String Read-only`**

Url or name of the icon to display. The icon may be displayed for any value of “display”.

<a id="HomeNodeEndpointUi.unit"></a>

**`unit String Read-only`**

The unit of the value to display.

<a id="HomeNodeEndpointUi.icon_color"></a>

**`icon_color String Read-only`**

The hexadecimal presentation of the tint to apply to the icon fetched from “icon_url”.

<a id="HomeNodeEndpointUi.text_color"></a>

**`text_color String Read-only`**

The hexadecimal presentation of the color of this endpoint label.

<a id="HomeNodeEndpointUi.value_color"></a>

**`value_color String Read-only`**

The hexadecimal presentation of the color of this endpoint value.

<a id="HomeNodeEndpointUi.range"></a>

**`range [] array of double Read-only`**

Range of array of threshold values for this endpoint value.

<a id="HomeNodeEndpointUi.icon_color_range"></a>

**`icon_color_range [] array of String Read-only`**

A range of colors to choose from instead of “icon_color”. The index in the range is the index in the “range” array which is just below the endpoint value.

<a id="HomeNodeEndpointUi.text_color_range"></a>

**`text_color_range [] array of String Read-only`**

A range of colors to choose from instead of “text_color”. The index in the range is the index in the “range” array which is just below the endpoint value.

<a id="HomeNodeEndpointUi.value_color_range"></a>

**`value_color_range [] array of String Read-only`**

A range of colors to choose from instead of “value_color”. The index in the range is the index in the “range” array which is just below the endpoint value.

<a id="HomeNodeEndpointUi.status_text_range"></a>

**`status_text_range [] array of String Read-only`**

Text values to display instead of the value itself. The index in the range is the index in the “range” array which is just below the endpoint value.

<a id="get-home-nodes"></a>

### Get Home Nodes

<a id="get--api-v8-home-nodes"></a>

**`GET /api/v8/home/nodes`**

Get the list of [`HomeNode`](home.md#HomeNode "HomeNode")
A node is either a physical home automation device or a virtual black box used to interact with other nodes. Physical nodes are associated to an adapter.
Nodes may have slot and signal endpoints. They can be used to interact with the node from the user interface. They can also be connected together using links.

**Example request**:

```http
GET /api/v8/home/nodes HTTP/1.1
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
       [...]
  ]
}
```

<a id="get-a-home-node"></a>

### Get a Home Node

<a id="get--api-v8-home-nodes-id"></a>

**`GET /api/v8/home/nodes/{id}`**

Get a specific [`HomeNode`](home.md#HomeNode "HomeNode")

**Example request**:

```http
GET /api/v8/home/nodes HTTP/1.1
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
       [...]
    }
  }
}
```

<a id="rename-a-home-node"></a>

### Rename a Home Node

<a id="put--api-v8-home-nodes-id"></a>

**`PUT /api/v8/home/nodes/{id}`**

Rename a [`HomeNode`](home.md#HomeNode "HomeNode")

**Example request**:

```http
PUT /api/v8/home/nodes HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "label": "Mon objet"
}
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

<a id="delete-a-home-node"></a>

### Delete a Home Node

<a id="delete--api-v8-home-nodes-id"></a>

**`DELETE /api/v8/home/nodes/{id}`**

Remove a [`HomeNode`](home.md#HomeNode "HomeNode") from the automation network. The object will need to be paired again if the node is physical.

**Example request**:

```http
DELETE /api/v8/home/nodes HTTP/1.1
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

<a id="home-nodes-values"></a>

## Home Nodes Values

<a id="endpoint-value-object"></a>

### Endpoint value object

<a id="HomeNodeEndpointValue"></a>

#### Objet HomeNodeEndpointValue

<a id="HomeNodeEndpointValue.value"></a>

**`value String Read-only`**

The current value of the endpoint

<a id="HomeNodeEndpointValue.unit"></a>

**`unit String Read-only`**

The displayable unit of the value

<a id="HomeNodeEndpointValue.refresh"></a>

**`refresh int Read-only`**

The period this value need to be refreshed

<a id="HomeNodeEndpointValue.value_type"></a>

**`value_type enum Read-only`**

The type of value this endpoint expose

| value_type |
| --- |
| bool |
| int |
| float |
| void |

<a id="fetch-endpoint-value"></a>

### Fetch Endpoint Value

<a id="get--api-v8-home-endpoints-node_id-endpoint_id"></a>

**`GET /api/v8/home/endpoints/{node_id}/{endpoint_id}`**

Retrieve the current value of the specified node endpoint.
The last pushed value is returned for slot endpoints. For signal endpoint, the value is retrieved directly from the node specific back-end.

**Example request**:

```http
GET /api/v8/home/endpoints/14/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "result": {
    "value": false,
    "value_type": "bool"
  },
  "success": true
}
```

<a id="change-endpoint-value"></a>

### Change Endpoint Value

<a id="put--api-v8-home-endpoints-node_id-endpoint_id"></a>

**`PUT /api/v8/home/endpoints/{node_id}/{endpoint_id}`**

Push a value to the specified node slot endpoint.
Only slot endpoint accept this operation.

**Example request**:

```http
PUT /api/v8/home/endpoints/14/1 HTTP/1.1
Host: mafreebox.freebox.fr
```

```json
{
  "value": true
}
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

<a id="home-tileset"></a>

## Home Tileset

The tileset is a user-friendly representation of connected objects which expose features instead of the actual objects

<a id="hometileobject"></a>

### HomeTileObject

<a id="HomeTile"></a>

#### Objet HomeTile

<a id="HomeTile.node_id"></a>

**`node_id int Read-only`**

Id of the [`HomeNode`](home.md#HomeNode "HomeNode") providing this tile data

<a id="HomeTile.label"></a>

**`label String Read-only`**

Displayable label of this tile

<a id="HomeTile.action"></a>

**`action enum Read-only`**

Action provided by this tile

| action | Description |
| --- | --- |
| tileset | Open the related node sub-tileset |
| graph | Open a graph detail page |
| store | Display a store simple command |
| store_slider | Display a store slider command |
| color_picker | Display a color selection widget |
| heat_picker | Display a white tone selection widget |
| intensity_picker | Display an intensity selection widget |
| none | No action |

<a id="HomeTile.type"></a>

**`type enum Read-only`**

The type of tile to display

| type | Description |
| --- | --- |
| action | A button tile that present no data |
| info | A generic tile that displays datas according to their UI field |
| light | A light control tile with color, intensity and head pickers |
| alarm_sensor | A tile representing a sensor that belongs to an alarm system |
| alarm_control | A tile representing an alarm system control |
| camera | A tile representing a camera |

<a id="HomeTile.group"></a>

**`group HomeNodeGroup Read-only`**

Displayable label of this tile

<a id="HomeTile.data"></a>

**`data [] array of HomeTileData Read-only`**

Displayable label of this tile

<a id="HomeNodeGroup"></a>

#### Objet HomeNodeGroup

<a id="HomeNodeGroup.label"></a>

**`label String Read-only`**

The displayable name of this group

<a id="HomeNodeGroup.icon_url"></a>

**`icon_url String Read-only`**

The icon url or name

<a id="HomeTileData"></a>

#### Objet HomeTileData

<a id="HomeTileData.refresh"></a>

**`refresh int Read-only`**

The period this data needs to be refreshed

<a id="HomeTileData.label"></a>

**`label String Read-only`**

The displayable name of this data

<a id="HomeTileData.ep_id"></a>

**`ep_id int Read-only`**

Id of the [`HomeNodeEndpoint`](home.md#HomeNodeEndpoint "HomeNodeEndpoint") related to this data

<a id="HomeTileData.value_type"></a>

**`value_type enum Read-only`**

The data value type

| value_type |
| --- |
| bool |
| int |
| float |
| string |

<a id="HomeTileData.value"></a>

**`value String`**

The data value

<a id="propriete-sans-ancre-home-61"></a>

**`value String Read-only`**

The data value history as string in the format: “timestamp:value” separated by semicolons

<a id="HomeTileData.ui"></a>

**`ui HomeNodeEndpointUi Read-only`**

Ui descriptor for this data to know how to display it

<a id="list-all-tiles"></a>

### List all Tiles

<a id="get--api-v8-home-tileset-all"></a>

**`GET /api/v8/home/tileset/all`**

Get the list of all tiles.

**Example request**:

```http
GET /api/v8/home/tileset/all HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
  "result" : [
      {
        "data" : [
            {
              "ep_id" : 0,
              "label" : "Trigger",
              "ui" : {
                  "access" : "rw",
                  "display" : "text"
                },
              "value" : null,
              "value_type" : "void"
            },
            {
              "category" : "alarm",
              "ep_id" : 1,
              "label" : "Alarme",
              "ui" : {
                  "access" : "rw",
                  "display" : "toggle",
                  "icon_url" : "http://lagabardine.ovh/~jeremie/img/Alarm.png"
                },
              "value" : false,
              "value_type" : "bool"
            },
            {
              "ep_id" : 2,
              "label" : "Pin Code",
              "ui" : {
                  "access" : "rw",
                  "display" : "text"
                },
              "value" : 0,
              "value_type" : "int"
            },
            {
              "ep_id" : 3,
              "label" : "Sirène",
              "refresh" : 2000,
              "ui" : {
                  "access" : "r",
                  "display" : "toggle",
                  "icon_url" : "http://lagabardine.ovh/~jeremie/img/Alarm.png"
                },
              "value" : false,
              "value_type" : "bool"
            }
          ],
        "ep_type" : "slot",
        "group" : {
            "icon_url" : "http://lagabardine.ovh/~jeremie/img/favori.png",
            "label" : ""
          },
        "node_id" : 17,
        "type" : "alarm_control"
      },
      {
        "data" : [
            {
              "ep_id" : 0,
              "history" : "1539868875260:1;1539876788228:0;1539876788530:1;1539876788796:0;1539876788850:1;1539876798829:0;1539876799143:1;1540282834199:1;1540305925367:0;1540305930508:1;",
              "label" : "Fenêtre",
              "ui" : {
                  "access" : "r",
                  "display" : "icon",
                  "icon_color_range" : [
                      "#ff0000",
                      "#00ff00"
                    ],
                  "icon_url" : "home_picto_dws",
                  "status_text_range" : [
                      "Ouvert",
                      "Fermé"
                    ],
                  "value_color" : "#00ff00"
                },
              "value" : null,
              "value_type" : "bool"
            },
            {
              "ep_id" : 1,
              "history" : "",
              "label" : "Couvercle",
              "ui" : {
                  "access" : "r",
                  "display" : "warning",
                  "icon_color" : "#00ff00",
                  "icon_url" : "home_picto_cover_alert"
                },
              "value" : null,
              "value_type" : "bool"
            },
            {
              "ep_id" : 2,
              "label" : "Niveau de Batterie",
              "ui" : {
                  "access" : "r",
                  "display" : "warning",
                  "icon_color" : "#00ff00",
                  "icon_url" : "home_picto_battery_alert",
                  "range" : [
                      0,
                      10
                    ],
                  "unit" : "%"
                },
              "value" : null,
              "value_type" : "int"
            }
          ],
        "ep_type" : "signal",
        "group" : {
            "icon_url" : "http://lagabardine.ovh/~jeremie/img/favori.png",
            "label" : "alarm"
          },
        "label" : "Détecteur d'ouvertures",
        "node_id" : 24,
        "type" : "alarm_sensor"
      },
      {
        "data" : [
            {
              "ep_id" : 0,
              "history" : "1539597596899:1;1539867684806:1;1539868117300:0;1539868164089:1;1540282931546:1;1540296461125:0;1540296468385:1;",
              "label" : "Détection",
              "ui" : {
                  "access" : "r",
                  "display" : "icon",
                  "icon_color_range" : [
                      "#ff0000",
                      "#00ff00"
                    ],
                  "icon_url" : "home_picto_pir",
                  "status_text_range" : [
                      "Mouvement détecté",
                      "Aucun movement"
                    ],
                  "unit" : ""
                },
              "value" : null,
              "value_type" : "bool"
            },
            {
              "ep_id" : 1,
              "history" : "",
              "label" : "Couvercle",
              "ui" : {
                  "access" : "r",
                  "display" : "warning",
                  "icon_url" : "home_picto_cover_alert",
                  "unit" : ""
                },
              "value" : null,
              "value_type" : "bool"
            },
            {
              "ep_id" : 2,
              "label" : "Niveau de Batterie",
              "ui" : {
                  "access" : "r",
                  "display" : "warning",
                  "icon_url" : "home_picto_battery_alert",
                  "range" : [
                      0,
                      10
                    ],
                  "unit" : "%"
                },
              "value" : null,
              "value_type" : "int"
            }
          ],
        "ep_type" : "signal",
        "group" : {
            "icon_url" : "http://lagabardine.ovh/~jeremie/img/favori.png",
            "label" : "alarm"
          },
        "label" : "move",
        "node_id" : 26,
        "type" : "alarm_sensor"
      }
    ],
  "success" : true
}
```

<a id="list-a-node-sub-tileset"></a>

### List a Node sub-tileset

<a id="get--api-v8-home-tileset-node_id"></a>

**`GET /api/v8/home/tileset/{node_id}`**

Get the list of all tiles corresponding to a node with “action”=”tileset”.

**Example request**:

```http
GET /api/v8/home/tileset/42 HTTP/1.1
Host: mafreebox.freebox.fr
```

**Example response**:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
```

```json
{
   "success" : true,
   "result" : [...]
}
```

<a id="special-tiles-specification"></a>

# Special Tiles specification

<a id="alarm-tiles"></a>

## Alarm Tiles

<a id="alarm-control"></a>

### Alarm control

This tile gives the current state of the alarm and allow to turn it on an off

type

alarm_control

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | enum | r | The current alarm state |
| 1 | void | w | Activate the main alarm |
| 2 | void | w | Activate the night alarm |
| 3 | void | w | Deactivate the alarm |
| 4 | void | w | Skip the alarm activation timer |
| 5 | int | r/w | Alarm PIN code that should be asked before changing the alarm state |
| 6 | string | r | Alarm error code |

state values

| State | Description |
| --- | --- |
| idle | The alarm is off |
| alarm1_arming | The main alarm is being activated, it’s a countdown when only the sensors not in the timed zone can trigger the alert |
| alarm2_arming | The night alarm is being activated, it’s a countdown when only the sensors not in the timed zone can trigger the alert |
| alarm1_armed | The main alarm is on |
| alarm2_armed | The night alarm is on |
| alarm1_alert_timer | The main alarm has been triggered by a sensor in the timed zone and the siren will ring after a countdown |
| alarm2_alert_timer | The night alarm has been triggered by a sensor in the timed zone and the siren will ring after a countdown |
| alert | The siren is ringing |

<a id="alarm-sensor"></a>

### Alarm sensor

This tile represents a connected sensor used to trigger the alarm

type

alarm_sensor

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | boolean | r | The state of this sensor: false=opening detected |
| 1..n | *any* | r | Any data with *warning* display type |

<a id="id2"></a>

### Alarm sensor

This tile represents a connected sensor used to trigger the alarm

type

alarm_sensor

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | boolean | r | The state of this sensor: false=opening detected |
| 1..n | *any* | r | Any data with *warning* display type |

<a id="camera"></a>

### Camera

This tile represents a camera

type

camera

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | string | r | The url of this camera on the local network |

<a id="automation-tiles"></a>

## Automation tiles

<a id="simple-store"></a>

### Simple store

This tile represents a store with simple commands

type

info

action

store

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | boolean | r | The state of the store: true=open, false=closed, null=undetermined |
| 1 | void | w | Command to open the store |
| 2 | void | w | Command to stop the store at its current position |
| 3 | void | w | Command to close the store |

<a id="commanded-store"></a>

### Commanded store

This tile represents a store with precise position command

type

info

action

store_slider

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | int | rw | The position of store in percent: 0=fully opened, 100=fully closed |
| 1 | void | w | Command to stop the store at its current position |

<a id="color-light-bulb"></a>

### Color light bulb

This tile represents a connected light bulb with full color and intensity control

type

light

action

color_picker

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | void | rw | The state of the light: true=on |
| 1 | int | rw | The H and S components of the color HSV value (H: 16 bits, S: 8 bit) |
| 2 | int | rw | The V value of the color HSV value (V: 8 bits) |

<a id="white-light-bulb"></a>

### White light bulb

This tile represents a connected light bulb with intensity and white tone control only

type

light

action

heat_picker

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | void | rw | The state of the light: true=on |
| 1 | int | rw | The H and S components of the color HSV value (H: 16 bits, S: 8 bit) |
| 2 | int | rw | The V value of the color HSV value (V: 8 bits) |

<a id="luminosity-light-bulb"></a>

### Luminosity light bulb

This tile represents a connected light bulb with intensity control only

type

light

action

intensity_picker

data

| Index | Value type | Access | Description |
| --- | --- | --- | --- |
| 0 | void | rw | The state of the light: true=on |
| 1 | int | rw | The luminosity value in percent |
