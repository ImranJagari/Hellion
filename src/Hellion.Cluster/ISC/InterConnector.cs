using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;

namespace Hellion.Cluster.ISC
{
    /// <summary>
    /// Reprensents the Cluster InterConnector.
    /// </summary>
    public class InterConnector : NetClient
    {
        public ClusterServer clusterServer;

        /// <summary>
        /// Creates a new cluster InterConnector instance.
        /// </summary>
        /// <param name="server">Parent cluster server</param>
        public InterConnector(ClusterServer server)
        {
            this.clusterServer = server;
        }

        /// <summary>
        /// Handle incoming packet.
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

        /// <summary>
        /// Authentificate the cluster server.
        /// </summary>
        private void Authentificate()
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.Authentification);
            packet.Write((int)InterServerType.Cluster);
            packet.Write(this.clusterServer.ClusterConfiguration.ISC.Password);
            packet.Write(this.clusterServer.ClusterConfiguration.ClusterId);
            packet.Write(this.clusterServer.ClusterConfiguration.Name);
            packet.Write(this.clusterServer.ClusterConfiguration.Ip);

            this.Send(packet);
        }
    }
}
