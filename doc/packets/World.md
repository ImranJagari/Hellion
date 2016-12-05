# World Server Packets

## Incoming Packets

### Join World

_Description:_

Recieve a request to join the game world.

_Structure:_

```c#
[int] header: WorldHeaders.Incoming.Join (0x0000FF00)
[int] worldId
[int] playerId
[int] authKey
[int] partyId
[int] guildId
[int] warId
[int] idOfMulti // what is this?
[byte] slot
[string] characterName
[string] username
[string] password
[int] messengerState
[int] messengerSize
for (int i = 0; i < messengerSize; ++i)
{
	[int] friendId
	// friend structure
	[bool] blocked
	[int] state
}
```

## Outgoing Packets
