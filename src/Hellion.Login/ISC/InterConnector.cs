using Ether.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;

namespace Hellion.Login.ISC
{
    public class InterConnector : NetClient
    {


        public InterConnector()
            : base()
        {

        }

        public override void Greetings()
        {
        }

        public override void HandleMessage(NetPacketBase packet)
        {
            var packetHeaderNumber = packet.Read<int>();
            var packetHeader = (InterHeaders)packetHeaderNumber;

            switch (packetHeader)
            {
                case InterHeaders.CanAuthtificate:
                    this.Authentificate();
                    break;
            }
        }

        private void Authentificate()
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.Authentification);
            // todo

            //this.Send(packet);
        }
    }
}
