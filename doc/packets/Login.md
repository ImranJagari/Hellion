# Login Server Packets

## Incoming Packets

### Login Request

_Description:_

The client sends to the server a login request.

_Structure:_

```c#
[string] buildVersion <- 32 bits
[string] username
[string] password
```

## Outgoing Packets

### Welcome

_Description:_

The server sends to the client a welcome message with a generated sessionKey.
According to the official sources, it seems to be usefull for the GameGuard protection.

_Structure:_

```c#
[int] header: LoginHeaders.Outgoing.Welcome (0x00)
[int] sessionKey
[int] ? // Not used
[int] ? // Not used
```

### Login Message

_Description:_

The server sends to the client a login message.
Essentially used to deliver login error messages.

_Structure:_

```c#
[int] header: LoginHeaders.Outgoing.LoginMessage (0xFE)
[int] errorCode
```

### Server List

_Description:_

The server sends to the client the list of the servers actually online.

_Structure:_

```c#
[int] header: LoginHeaders.Outgoing.ServerList (0xFD)
[int] 
[byte]
[string] username
[int] serverCount (= clusterCount + worldCount)

for (int i = 0; i < serverCount; ++i)
{
    [int] clusterId (-1 when it's a ClusterServer)
    [int] serverId
    [string] serverName
    [string] host ip
    [int] 0 // need to figure out what this is
    [int] 0 // number of people connected ?
    [int] 1 // ??
    [int] worldCapacity // Just for world servers ??
}

```