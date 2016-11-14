using Ether.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ether.Network.Packets;

namespace Hellion.ISC
{
    public class Client : NetConnection
    {
        public Client()
            : base()
        {
        }

        public Client(Socket socket)
            : base(socket)
        {
        }


        public override void Greetings()
        {
            base.Greetings();
        }


        public override void HandleMessage(NetPacketBase packet)
        {
            base.HandleMessage(packet);
        }
    }
}
