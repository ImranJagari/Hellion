using Ether.Network.Packets;
using Hellion.Core.Database;
using Hellion.Core.IO;
using Hellion.World.Structures;

namespace Hellion.World
{
    public partial class WorldClient
    {
        private void OnJoin(NetPacketBase packet)
        {
            var worldId = packet.Read<int>();
            var playerId = packet.Read<int>();
            var authKey = packet.Read<int>();
            var partyId = packet.Read<int>();
            var guildId = packet.Read<int>();
            var guildWarId = packet.Read<int>();
            var idOfMulti = packet.Read<int>(); // what is this?
            var slot = packet.Read<byte>();
            var playerName = packet.Read<string>();
            var username = packet.Read<string>();
            var password = packet.Read<string>();
            var messengerState = packet.Read<int>();
            var messengerCount = packet.Read<int>();
            // Not using messenger yet

            DbUser account = DatabaseService.Users.Get(x =>
                x.Username.ToLower() == username.ToLower() &&
                x.Password.ToLower() == password.ToLower() &&
                x.Authority > 0);

            if (account == null)
            {
                Log.Warning("Unknow account: '{0}'.", username);
                this.Server.RemoveClient(this);
                return;
            }

            DbCharacter character = DatabaseService.Characters.Get(x =>
                x.AccountId == account.Id &&
                x.Name.ToLower() == playerName.ToLower() &&
                x.Id == playerId, includes => includes.Items);

            if (character == null)
            {
                Log.Warning("Cannot find character '{0}' with id {1} for account '{2}'.", playerName, playerId, account.Id);
                this.Server.RemoveClient(this);
                return;
            }

            this.Player = new Player(character);
            // Send world transfer
        }
    }
}
