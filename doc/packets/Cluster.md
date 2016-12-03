# Cluster Server Packets

## Incoming Packets

### Character List Request

_Description:_

Recieve the character list request.

_Structure:_

```c#
[int] header: ClusterHeaders.Incoming.CharacterListRequest (0xF6)
[string] buildDate
[int] authKey
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
[string] username
[string] password
[int] slot
[string] characterName
[byte] faceId
[byte] costumeId
[byte] skinSetId
[byte] hairMeshId
[uint] hairColor
[byte] gender
[byte] job
[byte] headMeshId
[int] bankPassword
[int] authKey
```

### Delete Characater

_Description:_

Recieve a request to delete a character.

_Structure:_

```c#
[int] header: ClusterHeaders.Incoming.DeleteCharacter (0xF5)
[string] username
[string] password
[string] passwordVerification
[int] characterId
[int] authKey
```

### On Pre Join

_Description:_

Recieve a request to join the selected world server.

_Structure:_

```c#
[int] header: ClusterHeaders.Incoming.PreJoin (0xFF05)
[string] username
[int] characterId
[string] characterName
[int] bankCode
[int] authKey
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
[int] authKey
[int] characterCount

for (int i = 0; i < characterCount; ++i)
{
    [int] slot
    [int] ?? // Blocked character?
    [int] mapId
    [int] modelId (0x0B + gender)
    [string] name
    [float] posX
    [float] posY
    [float] posZ
    [int] characterId
    [int] partyId
    [int] guildId
    [int] warId
    [int] skinSetId
    [int] hairId
    [int] hairColor
    [int] faceId
    [byte] gender
    [int] classId
    [int] level
    [int] ?? // Job level ? (Master or hero ?)
    [int] strength
    [int] stamina
    [int] dexterity
    [int] intelligence
    [int] ?? // Mode ?
    [int] itemCount // (where slotId >= 42)

    for (int j = 0; j < itemCount; ++j)
        [int] itemId
}

[int] messengerCount // Mail ?

for (int j = 0; j < messengerCount; ++j)
{
    // TODO
}
```

### Join World

_Description:_

_Structure:_

```c#
[int] header: ClusterHeaders.Outgoing.JoinWorld (0xFF05)
```