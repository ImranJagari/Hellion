using Ether.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;

namespace Hellion.ISC
{
    public class InterClient : NetConnection
    {
        /// <summary>
        /// Creates a new InterClient instance.
        /// </summary>
        public InterClient()
            : base()
        {
        }

        /// <summary>
        /// Creates a new InterClient instance.
        /// </summary>
        /// <param name="socket">Socket</param>
        public InterClient(Socket socket)
            : base(socket)
        {
        }
        
        /// <summary>
        /// Sends a welcome message to the inter client.
        /// </summary>
        public override void Greetings()
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.CanAuthtificate);

            this.Send(packet);
        }

        /// <summary>
        /// Handles the incoming message.
        /// </summary>
        /// <param name="packet">Packet</param>
        public override void HandleMessage(NetPacketBase packet)
        {
            var packetHeaderNumber = packet.Read<int>();
            var packetHeader = (InterHeaders)packetHeaderNumber;

            switch (packetHeader)
            {
                default:
                    Log.Warning("Unknow packet: 0x{0}", packetHeaderNumber.ToString("X2"));
                    break;
            }
        }
    }
}
