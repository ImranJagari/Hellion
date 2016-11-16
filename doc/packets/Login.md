# Login Server Packets

## Incoming Packets

### Login Request

```c#
[string] buildVersion <- 32 bits
[string] username
[string] password
```

## Outgoing Packets

### Welcome

```c#
[int] header: 0
[int] sessionKey
[int] ?
[int] ?
```
