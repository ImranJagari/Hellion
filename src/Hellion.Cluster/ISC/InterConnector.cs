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

                default: Log.Warning("Unknow packet header: 0x{0}", packetHeaderNumber.ToString("X2")); break;
            }
        }
    }
}
