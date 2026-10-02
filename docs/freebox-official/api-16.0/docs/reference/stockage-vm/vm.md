<a id="vm-api-unstable"></a>

<a id="vm-api"></a>

# VM API [UNSTABLE]

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#vm-api-unstable)

## Navigation

- [VM API Errors](#vm-api-errors)
- [VM API objects](#vm-api-objects)
- [VM API actions](#vm-api-actions)


This API allows to control VMs on boxes that have has_vm to true in their [`SystemConfig`](../systeme/system.md#SystemConfig "SystemConfig") information.

<a id="vm-api-errors"></a>

## VM API Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| initfail | VM cannot be initialized |
| startfail | The VM cannot be launched |
| inval | Invalid parameter |
| nomem | Not enough memory available |
| already_running | The VM is already running |
| not_running | The VM is not running |
| too_big | Size too big |
| too_small | Size too small |
| exists | File exists |
| too_many_vms | The maximum number of configurable VMs has been reached |
| no_such_vm | VM does not exist |
| disk_in_use | The disk is already in use |
| nocpu | Not enough CPUs available |
| no_such_usb_port | USB port does not exist |
| usb_in_use | Another VM is already using USB |
| usb_init_fail | Unable to initialize USB |
| disk_not_qcow2 | The disk is not in Qcow2 format |
| unsupported_disk_type | Unsupported disk format |
| file_not_found | Disk file not found |
| efi_file_in_use | EFI settings file is already in use |
| efi_file_fail | Cannot open EFI settings file |
| distro_http | Internal http error |
| distro_sig | Internal sig error |
| distro_json | Internal json error |
| create | Unable to create file |
| perm_own | Incorrect permission |
| open_info | Unable to open file for information |
| open_resize | Cannot open file for resizing |
| resize_trunc | Unable to resize raw disk |
| power_button | Unable to send shutdown to VM |
| restart | Cannot send restart to VM |
| open_launch_disk | Error opening disk file |
| open_launch_cd | Error opening cdrom file |
| start_nodisk | Cannot start without disk |
| init_vm_control | Unable to initialize VM control |
| set_nodisk | Cannot set up a VM without disk |
| set_badformat | Unsupported disk format |
| save_data | Cannot save VM settings |
| stop_control | Cannot stop VM control |
| info | Unable to retrieve disk info |
| info_parse | Unable to analyze disk information |
| info_novirtual | Unable to retrieve disk size |
| info_noactual | Unable to retrieve actual disk size |
| info_noformat | Unable to retrieve disk format |
| create_qcow | Unable to create qcow2 disk |
| resize_qcow | Unable to resize qcow2 disk |
| set_too_many_disks | The VM has too many disks |
| set_empty_disk_path | Empty disk path |
| task_notfound | The task does not exist |
| not_stopped | The VM is not stopped |

<a id="vm-api-objects"></a>

## VM API objects

<a id="vm-object"></a>

### VM object

<a id="VM"></a>

#### Objet VM

<a id="VM.id"></a>

**`id int Read-only`**

unique id of this VM

<a id="VM.name"></a>

**`name string`**

Name of this VM. Max 31 characters.

<a id="VM.disk_path"></a>

**`disk_path string`**

Base64-encoded path to the hard disk image of this VM.

<a id="VM.disk_type"></a>

**`disk_type enum`**

Type of disk image.

| disk_type | Description |
| --- | --- |
| raw | Raw disk data |
| qcow2 | Qcow2 image type. Usually qcow version 3. Note: not all features are supported. In particular, reference to other images is disabled. |

<a id="VM.cd_path"></a>

**`cd_path string Optionnal`**

Base64-encoded path to CDROM device ISO image. Optional.

<a id="VM.memory"></a>

**`memory int`**

Memory allocated to this VM in megabytes.

<a id="VM.vcpus"></a>

**`vcpus int`**

Number of virtual CPUs to allocate to this VM.

<a id="VM.status"></a>

**`status enum Read-only`**

VM status

| status | Description |
| --- | --- |
| stopped | VM is stopped |
| running | VM is running |
| starting | VM is starting up. Transitional state |
| stopping | VM is being stopped. Transitional state |

<a id="VM.enable_screen"></a>

**`enable_screen bool`**

Whether or not this VM should have a virtual screen, to use with the VNC websocket protocol.

<a id="VM.bind_usb_ports"></a>

**`bind_usb_ports [] array of enum`**

List of ports that should be bound to this VM. Only one VM can use USB at given time, whether is uses only one or all USB ports. The list of system USB ports is available in [`VmSystemInfo`](vm.md#VmSystemInfo "VmSystemInfo"). For example: “usb-external-type-a”, “usb-external-type-c”.

<a id="VM.enable_cloudinit"></a>

**`enable_cloudinit bool`**

Whether or not to enable passing data through cloudinit. This uses the NoCloud iso image method; it will add a virtual cdrom drive (distinct from the one passed by cd_path) with the data in cloudinit_userdata and cloudinit_hostname when enabled.

<a id="VM.cloudinit_hostname"></a>

**`cloudinit_hostname string`**

When cloudinit is enabled, hostname desired for this VM. Max 59 characters.

<a id="VM.cloudinit_userdata"></a>

**`cloudinit_userdata string`**

When cloudinit is enabled, raw yaml to be passed in the user-data file. Maximum 32767 characters.

<a id="VM.mac"></a>

**`mac string Read-only`**

VM ethernet interface MAC address.

<a id="VM.os"></a>

**`os string`**

Type of OS used for this VM. Only used to set an icon for now. Example values:

- unknown
- fedora
- debian
- ubuntu
- freebsd
- opensuse
- centos
- jeedom
- homebridge

<a id="vm-system-info-object"></a>

### VM System Info object

<a id="VmSystemInfo"></a>

#### Objet VmSystemInfo

<a id="VmSystemInfo.total_memory"></a>

**`total_memory int Read-only`**

Total memory available to VMs.

<a id="VmSystemInfo.used_memory"></a>

**`used_memory int Read-only`**

Currently used memory by all VMs.

<a id="VmSystemInfo.total_cpus"></a>

**`total_cpus int Read-only`**

Total number of vCPUs available to VMs.

<a id="VmSystemInfo.used_cpus"></a>

**`used_cpus int Read-only`**

Currently used vCPUs by all VMs.

<a id="VmSystemInfo.usb_ports"></a>

**`usb_ports [] array of string Read-only`**

List of USB ports available on this system

<a id="VmSystemInfo.usb_used"></a>

**`usb_used bool Read-only`**

Whether a VM is currently using USB. (only one can use USB at a given time)

<a id="vm-distribution-object"></a>

### VM Distribution object

<a id="VmDistribution"></a>

#### Objet VmDistribution

<a id="VmDistribution.name"></a>

**`name string Read-only`**

Name of downloadable distribution image.

<a id="VmDistribution.url"></a>

**`url string Read-only`**

URL of distribution. Usually an arm64 qcow2 cloud image, supporting EFI boot and cloud-init.

<a id="VmDistribution.hash"></a>

**`hash string Read-only`**

Hash in the format sha256:<hash> or sha512:<hash>; or a URL to a SHA256SUMS or SHA512SUMS file (used by Ubuntu, Debian), or to a -CHECKSUM file (used by Fedora). It is designed to be passed as-is to the [download add API](../fichiers-telechargements/download.md#adding-a-new-download-task).

<a id="VmDistribution.os"></a>

**`os string Read-only`**

OS of this distribution image; to be passed as a os type in the [`VM`](vm.md#VM "VM").

<a id="vm-disk-info-object"></a>

### VM Disk info object

<a id="VmDiskInfo"></a>

#### Objet VmDiskInfo

<a id="VmDiskInfo.type"></a>

**`type enum Read-only`**

Type of disk, just like in [`VM.disk_type`](vm.md#VM.disk_type "VM.disk_type")

<a id="VmDiskInfo.actual_size"></a>

**`actual_size int Read-only`**

Space used by virtual image on disk. This is how much filesystem space is consumed on the box.

<a id="VmDiskInfo.virtual_size"></a>

**`virtual_size int Read-only`**

Size of virtual disk. This is the size the disk will appear inside the VM.

<a id="vm-disk-task-object"></a>

### VM Disk task object

<a id="VmDiskTask"></a>

#### Objet VmDiskTask

<a id="VmDiskTask.id"></a>

**`id int Read-only`**

Task id.

<a id="VmDiskTask.type"></a>

**`type enum Read-only`**

Type of disk operation:

- create
- resize

<a id="VmDiskTask.done"></a>

**`done bool Read-only`**

Is task done

<a id="VmDiskTask.error"></a>

**`error bool Read-only`**

Is task in error

<a id="vm-api-actions"></a>

## VM API actions

<a id="get-vm-system-info"></a>

### Get VM System Info

<a id="get--api-v8-vm-info-"></a>

**`GET /api/v8/vm/info/`**

Returns a [`VmSystemInfo`](vm.md#VmSystemInfo "VmSystemInfo")

<a id="get-installable-vm-distributions"></a>

### Get Installable VM distributions

<a id="get--api-v8-vm-distros-"></a>

**`GET /api/v8/vm/distros/`**

Returns a collection of [`VmDistribution`](vm.md#VmDistribution "VmDistribution")

<a id="get-the-list-of-all-vms"></a>

### Get the list of all VMs

<a id="get--api-v8-vm-"></a>

**`GET /api/v8/vm/`**

Returns a collection of [`VM`](vm.md#VM "VM")

<a id="get-a-vm"></a>

### Get a VM

<a id="get--api-v8-vm-id"></a>

**`GET /api/v8/vm/{id}`**

Returns a [`VM`](vm.md#VM "VM") object

<a id="add-a-vm"></a>

### Add a VM

<a id="post--api-v8-vm-"></a>

**`POST /api/v8/vm/`**

Needs to be passed a [`VM`](vm.md#VM "VM") object

<a id="delete-a-vm"></a>

### Delete a VM

<a id="delete--api-v8-vm-id"></a>

**`DELETE /api/v8/vm/{id}`**

Only works if vm is stopped.

<a id="update-a-vm"></a>

### Update a VM

<a id="put--api-v8-vm-id"></a>

**`PUT /api/v8/vm/{id}`**

Only works if vm is stopped.

<a id="start-a-vm"></a>

### Start a VM

<a id="post--api-v8-vm-id-start"></a>

**`POST /api/v8/vm/{id}/start`**

Only works if vm is stopped.

<a id="send-a-powerbutton-signal-to-a-vm"></a>

### Send a powerbutton signal to a VM

<a id="post--api-v8-vm-id-powerbutton"></a>

**`POST /api/v8/vm/{id}/powerbutton`**

This will send an ACPI shutdown button event to the VM, so that it can decide to shutdown itself.

Only works if vm is running.

<a id="stop-a-vm"></a>

### Stop a VM

Immediately stops the VM without any safety.

<a id="post--api-v8-vm-id-stop"></a>

**`POST /api/v8/vm/{id}/stop`**

Only works if vm is running.

<a id="reset-a-vm"></a>

### Reset a VM

Immediately restarts the VM without any safety.

<a id="post--api-v8-vm-id-restart"></a>

**`POST /api/v8/vm/{id}/restart`**

Only works if vm is running.

<a id="watch-for-vm-status-changes"></a>

### Watch for VM status changes

You should use the websocket [`RegisterAction`](../fondamentaux/websocket.md#RegisterAction "RegisterAction") API with the `vm_state_changed` event to watch for changes in VM status, instead of polling.

The event will contain this object:

<a id="VmStateChange"></a>

#### Objet VmStateChange

<a id="VmStateChange.id"></a>

**`id int Read-only`**

VM id.

<a id="VmStateChange.status"></a>

**`status enum Read-only`**

New [`VM.status`](vm.md#VM.status "VM.status").

You can also watch for `lan_host_l3addr_reachable` and compare it with [`VM.mac`](vm.md#VM.mac "VM.mac") to get the VM IP when it starts.

<a id="vm-virtual-console"></a>

### VM virtual console

The serial port of the VM is available via a WebSocket.

<a id="get--api-v8-vm-id-console"></a>

**`GET /api/v8/vm/{id}/console`**

It uses the QEMU websocket chardev device. Call must be authentified like the rest of the API.

<a id="vm-virtual-screen"></a>

### VM virtual screen

When [`VM.enable_screen`](vm.md#VM.enable_screen "VM.enable_screen") is `true`, the VM will have a VNC over websocket device available.

<a id="get--api-v8-vm-id-vnc"></a>

**`GET /api/v8/vm/{id}/vnc`**

It uses the QEMU VNC websocket device. Call must be authentified like the rest of the API. This device should work with noVNC unmodified.

<a id="get-information-on-a-virtual-disk"></a>

### Get information on a virtual disk

<a id="post--api-v8-vm-disk-info"></a>

**`POST /api/v8/vm/disk/info`**

Parameters

- **disk_path** (*string*) – base64-encoded disk path

Returns a [`VmDiskInfo`](vm.md#VmDiskInfo "VmDiskInfo") object.

<a id="create-a-virtual-disk"></a>

### Create a virtual disk

<a id="post--api-v8-vm-disk-create"></a>

**`POST /api/v8/vm/disk/create`**

Parameters

- **disk_path** (*string*) – base64-encoded disk path
- **size** (*int*) – Size in bytes of virtual disk.
- **disk_type** (*enum*) – Type of [`VM.disk_type`](vm.md#VM.disk_type "VM.disk_type")

Returns a task id. Task should not be polled, use the `vm_disk_task_done` websocket event with [`RegisterAction`](../fondamentaux/websocket.md#RegisterAction "RegisterAction").

<a id="resize-a-virtual-disk"></a>

### Resize a virtual disk

<a id="post--api-v8-vm-disk-resize"></a>

**`POST /api/v8/vm/disk/resize`**

Parameters

- **disk_path** (*string*) – base64-encoded disk path
- **size** (*int*) – New size of virtual disk
- **shrink_allow** (*bool*) – Whether shrinking the disk is allowed. Setting to true means this operation can be destructive.

Returns a task id. Task should not be polled, use the `vm_disk_task_done` websocket event with [`RegisterAction`](../fondamentaux/websocket.md#RegisterAction "RegisterAction").

<a id="get-a-virtual-disk-task"></a>

### Get a virtual disk task

<a id="get--api-v8-vm-disk-task-id"></a>

**`GET /api/v8/vm/disk/task/{id}`**

Returns a [`VmDiskTask`](vm.md#VmDiskTask "VmDiskTask")

<a id="delete-a-virtual-disk-task"></a>

### Delete a virtual disk task

<a id="delete--api-v8-vm-disk-task-id"></a>

**`DELETE /api/v8/vm/disk/task/{id}`**

Delete your tasks once they are done.
