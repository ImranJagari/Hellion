using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;

namespace Hellion.Login.ISC
{
    public class InterConnector : NetClient
    {
        private LoginServer loginServer;

        /// <summary>
        /// Creates a new InterConnector instance.
        /// </summary>
        /// <param name="loginServer">Parent server</param>
        public InterConnector(LoginServer server)
            : base()
        {
            this.loginServer = server;
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
                case InterHeaders.CanAuthtificate:
                    this.Authentificate();
                    break;
            }
        }

        /// <summary>
        /// Authentificates this LoginServer.
        /// </summary>
        private void Authentificate()
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.Authentification);
            packet.Write((int)InterServerType.Login);
            packet.Write(this.loginServer.LoginConfiguration.ISC.Password);

            this.Send(packet);
        }
    }
}
