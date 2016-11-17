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
[int] header: LoginHeaders.Outgoing.Welcome (0x00)
[int] sessionKey
[int] ? // Not used
[int] ? // Not used
```

### Login Message

```c#
[int] header: LoginHeaders.Outgoing.LoginMessage (0xFE)
[int] errorCode
```