using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using Hellion.Core.ISC.Structures;
using System;

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
                case InterHeaders.CanAuthticate: this.Authenticate(); break;
                case InterHeaders.AuthenticationResult: this.OnAuthenticationResult(packet); break;
                case InterHeaders.UpdateWorldServerList: this.OnUpdateWorldServerList(packet); break;
                default: Log.Warning("Unknow packet header: 0x{0}", packetHeaderNumber.ToString("X2")); break;
            }
        }

        /// <summary>
        /// On client disconnected.
        /// </summary>
        protected override void OnClientDisconnected()
        {
            Log.Info("Disconnected from ISC.");
        }

        /// <summary>
        /// Authenticate the cluster server.
        /// </summary>
        private void Authenticate()
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.Authentication);
            packet.Write((int)InterServerType.Cluster);
            packet.Write(this.clusterServer.ClusterConfiguration.ISC.Password);
            packet.Write(this.clusterServer.ClusterConfiguration.ClusterId);
            packet.Write(this.clusterServer.ClusterConfiguration.Name);
            packet.Write(this.clusterServer.ClusterConfiguration.Ip);

            this.Send(packet);
        }

        /// <summary>
        /// Process the result of the authentication. 
        /// </summary>
        /// <param name="packet"></param>
        private void OnAuthenticationResult(NetPacketBase packet)
        {
            var result = packet.Read<bool>();

            if (result == false)
                Environment.Exit(0);
        }

        private void OnUpdateWorldServerList(NetPacketBase packet)
        {
            var worldServerCount = packet.Read<int>();

            for (int i = 0; i < worldServerCount; ++i)
            {
                var worldId = packet.Read<int>();
                var worldIp = packet.Read<string>();
                var worldName = packet.Read<string>();
                var worldCapacity = packet.Read<int>();
                var worldConnectedPlayers = packet.Read<int>();
                var worldServer = new WorldServerInfo(
                    worldId, 
                    this.clusterServer.ClusterConfiguration.ClusterId, 
                    worldCapacity,
                    worldIp, 
                    worldName, 
                    worldConnectedPlayers);

                this.clusterServer.ConnectedWorldServers.Add(worldServer);
            }
        }
    }
}
