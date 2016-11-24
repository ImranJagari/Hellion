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

            var character = from x in ClusterServer.DbContext.Characters
                            where x.Id == 1
                            select x;

            var character2 = new Character();
            character2.FromDbCharacter(character.FirstOrDefault());
        }

        private void OnCreateCharacter(NetPacketBase packet)
        {
        }

        private void OnDeleteCharacter(NetPacketBase packet)
        {
        }
    }
}
