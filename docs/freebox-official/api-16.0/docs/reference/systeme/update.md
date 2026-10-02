<a id="update-status"></a>

<a id="update-api"></a>

# Update Status

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#update-status)

## Navigation

- [Update status object](#update-status-object)
- [Upgrade status object](#upgrade-status-object)
- [Update API](#id1-800)


The Update API allows you to access box firmware update status

<a id="update-status-object"></a>

## Update status object

Update status object have the following properties

<a id="UpdateStatus"></a>

### Objet UpdateStatus

<a id="UpdateStatus.state"></a>

**`state enum`**

update current state

| State | Description |
| --- | --- |
| initializing | update process is initializing |
| upgrading | firmware is upgrading |
| up_to_date | firmware is up to date |
| error | an error occurred during update |

<a id="UpdateStatus.upgrade_state"></a>

**`upgrade_state UpgradeState`**

<a id="upgrade-status-object"></a>

## Upgrade status object

Details of current box upgrade. Only relevant for “upgrading” and “upgrade_failed” states.

<a id="UpgradeState"></a>

### Objet UpgradeState

<a id="UpgradeState.state"></a>

**`state enum`**

upgrade state

| State | Description |
| --- | --- |
| downloading | downloading update |
| download_failed | update downloading has failed |
| checking | checking the downloaded data |
| check_failed | downloaded data check has failed |
| prepare_write | preparing to write data |
| prepare_write_failed | preparing to write data ha failed |
| writing | writing the data |
| write_failed | data writing has failed |
| reread | checking written data |
| reread_failed | written data checking has failed |
| commit | applying the update |
| commit_failed | update applying has failed |

<a id="UpgradeState.old_version"></a>

**`old_version string`**

current firmware version

<a id="UpgradeState.new_version"></a>

**`new_version string`**

new firmware version being downloaded

<a id="UpgradeState.percent"></a>

**`percent int`**

download progress if state is downloading

<a id="UpgradeState.error_string"></a>

**`error_string string`**

update error if state is download_failed

<a id="id1-800"></a>

## Update API

<a id="get-the-update-status"></a>

### Get the update status

<a id="get--api-v11-update-"></a>

**`GET /api/v11/update/`**

Returns the `Upgrade status object`

**Example request**:

```http
GET /api/v11/update/ HTTP/1.1
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
    "state": "auto_up_to_date"
  }
}
```
