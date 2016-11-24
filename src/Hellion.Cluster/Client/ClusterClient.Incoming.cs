using Ether.Network.Packets;
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
        }

        private void OnCreateCharacter(NetPacketBase packet)
        {
        }

        private void OnDeleteCharacter(NetPacketBase packet)
        {
        }
    }
}
