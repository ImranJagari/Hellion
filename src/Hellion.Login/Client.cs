using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core;
using Hellion.Core.Data.Headers;
using Hellion.Core.Network;
using System.Net.Sockets;

namespace Hellion.Login
{
    /// <summary>
    /// Represents a Login client.
    /// </summary>
    public class Client : NetConnection
    {
        private uint sessionId;

        /// <summary>
        /// Creates a new Login Client instance.
        /// </summary>
        public Client()
            : base()
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
        }

        /// <summary>
        /// Creates a new Login Client instance.
        /// </summary>
        /// <param name="socket">Socket</param>
        public Client(Socket socket)
            : base(socket)
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
        }

        /// <summary>
        /// Send greetings to the client.
        /// </summary>
        public override void Greetings()
        {
            var packet = new FFPacket();

            packet.WriteHeader(LoginHeaders.Outgoing.Greetings);
            packet.Write(this.sessionId);

            this.Send(packet);
        }

        /// <summary>
        /// Handle incoming packets.
        /// </summary>
        /// <param name="packet">Incoming packet</param>
        public override void HandleMessage(NetPacketBase packet)
        {
            packet.Position += 13;
            var packetHeaderNumber = packet.Read<int>();
            var packetHeader = (LoginHeaders.Incoming)packetHeaderNumber;

            switch (packetHeader)
            {
                default: FFPacket.UnknowPacket<LoginHeaders.Incoming>(packetHeaderNumber, 2); break;
            }
        }
    }
}
