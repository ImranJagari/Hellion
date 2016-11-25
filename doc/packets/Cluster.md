# Cluster Server Packets

## Incoming Packets

### Character List Request

_Description:_

Recieve the character list request.

_Structure:_

```c#
[int] header: ClusterHeaders.Incoming.CharacterListRequest (0xF6)
[string] buildDate
[int] time
[string] username
[string] password
[int] serverId
```

### Create Character

_Description:_

Recieve a request to create a character.

_Structure:_

```c#
[int] header: ClusterHeaders.Incoming.CreateCharacter (0xF4)
```

### Delete Characater

_Description:_

Recieve a request to delete a character.

_Structure:_

```c#
[int] header: ClusterHeaders.Incoming.DeleteCharacter (0xF5)
```

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

### Character List

_Description:_

Send the character list to the client.

_Structure:_

```c#
[int] header: ClusterHeaders.Outgoing.CharacterList (0xF3)

```