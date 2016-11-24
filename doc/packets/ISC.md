# Inter-Server Communication Packets

## Incoming Packets

### Authentification

_Description:_

The InterClient sends to the InterServer the authentifcation request.

_Structure:_

**Login**
```c#
[int] header: InterHeaders.Authentification (0x01)
[int] serverType
[string] interPassword
```

**Cluster**
```c#
[int] header: InterHeaders.Authentification (0x01)
[int] serverType
[string] interPassword
[int] clusterId
[string] clusterName
[string] clusterIp
```

**World**
```c#
[int] header: InterHeaders.Authentification (0x01)
[int] serverType
[string] interPassword
[int] clusterId
[int] worldId
[string] worldName
[string] worldIp
[int] worldCapacity
[int] worldConnectedPlayersCount
```

## Outgoing Packets

### Welcome

_Description:_

The InterServer sends to the InterClient a message that indicates that he can authentificate.

_Structure:_

```c#
[int] header: InterHeaders.CanAuthentificate (0x00)
```

### Authentification Result

_Description:_

Sends the authentification result.

_Structure:_

```c#
[int] header: InterHeaders.AuthentificationResult (0x02)
[bool] result
```

### Update Server List

_Description:_

Sends the cluster and world server list to the login server

_Structure:_

```c#
[int] header: InterHeaders.UpdateServerList (0x03)
[int] clusterCount
for (int i = 0; i < clusterCount; ++i)
{
    [int] clusterId
    [string] clusterIp
    [string] clusterName
    [int] worldsCount
    for (int j = 0; j < worldsCount; ++j)
    {
        [int] worldId
        [string] worldIp
        [string] worldName
        [int] worldCapacity
        [int] worldConnectedPlayersCount
    }
}
```