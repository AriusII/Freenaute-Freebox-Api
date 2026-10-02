<a id="slowness"></a>

# Slowness

> Référence officielle embarquée — API annoncée **16.0** · instantané du **2 octobre 2026**.
> Les signatures et exemples conservent leurs versions documentées. Les éléments dépréciés sont exclus.
> Les descriptions techniques restent dans la langue de la source pour préserver leur sens.

[Index de référence](../../INDEX.md) · [Guide SDK](../../GUIDE-SDK.md) · [Source officielle](http://mafreebox.freebox.fr/doc/index.html#slowness)

## Navigation

- [Slowness Errors](#slowness-errors)
- [Slowness API](#slowness-api)


The slowness API allow you to execute diagnostics on a selected host to detect
a potential causes of degradation of the throughput.

<a id="slowness-errors"></a>

## Slowness Errors

When attempting to access this API, you may encounter the following
errors:

| error_code | Description |
| --- | --- |
| inval | invalid parameters |
| nodev | invalid device id |
| nohost | invalid host GID or not found |
| noconn | WAN connection is down |
| netdown | link with host is down |
| erunning | API is already running |
| internal | system internal error |

<a id="slowness-api"></a>

## Slowness API

<a id="get-the-last-result-of-a-given-host"></a>

### Get the last result of a given host
