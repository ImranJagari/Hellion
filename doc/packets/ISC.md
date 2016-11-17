# Inter-Server Communication Packets

## Incoming Packets

### Authentification

_Description:_

The InterClient sends to the InterServer the authentifcation request.

_Structure:_

```c#
[int] header: InterHeaders.Authentification (0x01)
```

## Outgoing Packets

### Welcome

_Description:_

The InterServer sends to the InterClient a message that indicates that he can authentificate.

_Structure:_

```c#
[int] header: InterHeaders.CanAuthentificate (0x00)
```