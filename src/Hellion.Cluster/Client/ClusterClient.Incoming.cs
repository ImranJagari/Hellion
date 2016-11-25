using Ether.Network.Packets;
using Hellion.Cluster.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Cluster.Client
{
    public partial class ClusterClient
    {
        private void OnPing(NetPacketBase packet)
        {
            var time = packet.Read<int>();

            this.SendPong(time);
        }

        private void OnCharacterListRequest(NetPacketBase packet)
        {
            var buildDate = packet.Read<string>();
            var time = packet.Read<int>();
            var username = packet.Read<string>();
            var password = packet.Read<string>();
            var serverId = packet.Read<int>();

            var account = (from x in ClusterServer.DbContext.Users
                           where x.Username.ToLower() == username.ToLower()
                           where x.Password.ToLower() == password.ToLower()
                           where x.Authority != 0
                           select x).FirstOrDefault();

            if (account == null)
            {
                this.Server.RemoveClient(this);
                return;
            }

            var characters = from x in ClusterServer.DbContext.Characters
                             where x.Id == account.Id
                             select x;

            this.SendCharacterList(time, characters?.ToList());
        }

        private void OnCreateCharacter(NetPacketBase packet)
        {
        }

        private void OnDeleteCharacter(NetPacketBase packet)
        {
        }
    }
}
