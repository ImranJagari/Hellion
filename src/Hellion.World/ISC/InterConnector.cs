using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;

namespace Hellion.World.ISC
{
    public class InterConnector : NetClient
    {
        private WorldServer worldServer;

        /// <summary>
        /// Creates a new InterConnector instance.
        /// </summary>
        /// <param name="loginServer">Parent server</param>
        public InterConnector(WorldServer server)
            : base()
        {
            this.worldServer = server;
        }

        /// <summary>
        /// Handles the incoming data.
        /// </summary>
        /// <param name="packet">Incoming packet</param>
        public override void HandleMessage(NetPacketBase packet)
        {
            var packetHeaderNumber = packet.Read<int>();
            var packetHeader = (InterHeaders)packetHeaderNumber;

            Log.Debug("Recieved: {0}", packetHeader);

            switch (packetHeader)
            {
                case InterHeaders.CanAuthtificate: this.Authentificate(); break;
                default: Log.Warning("Unknow packet header: 0x{0}", packetHeaderNumber.ToString("X2")); break;
            }
        }

        private void Authentificate()
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.Authentification);
            packet.Write((int)InterServerType.World);
            packet.Write(this.worldServer.WorldConfiguration.ISC.Password);
            packet.Write(this.worldServer.WorldConfiguration.ClusterId);
            packet.Write(this.worldServer.WorldConfiguration.WorldId);
            packet.Write(this.worldServer.WorldConfiguration.Name);
            packet.Write(this.worldServer.WorldConfiguration.Ip);
            packet.Write(this.worldServer.WorldConfiguration.Capacity);
            packet.Write(this.worldServer.Clients.Count);

            this.Send(packet);
        }
    }
}
