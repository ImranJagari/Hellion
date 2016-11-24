# Cluster Server Packets

## Incoming Packets



## Outgoing Packets

### Welcome

_Description:_

Send the welcome message to the client along with the SessionKey.

_Structure:_

```c#
[int] header: 0
[int] sessionId
[int] 0 // ? not used
[int] 0 // ? not used
```

### Pong

_Description:_

Send the pong message to the client

_Structure:_

```c#
[int] header: ClusterHeaders.Outgoing.Pong (0x14)
[int] time
```