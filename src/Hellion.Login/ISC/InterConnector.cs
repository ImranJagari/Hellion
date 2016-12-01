using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using Hellion.Core.ISC.Structures;
using System;

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
                case InterHeaders.CanAuthticate: this.Authenticate(); break;
                case InterHeaders.AuthenticationResult: this.OnAuthenticationResult(packet); break;
                case InterHeaders.UpdateServerList: this.OnUpdateServerList(packet); break;

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
        /// Authenticates this LoginServer.
        /// </summary>
        private void Authenticate()
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.Authentication);
            packet.Write((int)InterServerType.Login);
            packet.Write(this.loginServer.LoginConfiguration.ISC.Password);

            this.Send(packet);
        }

        /// <summary>
        /// Recieve the authentication result.
        /// </summary>
        /// <param name="packet"></param>
        private void OnAuthenticationResult(NetPacketBase packet)
        {
            var result = packet.Read<bool>();

            if (result == false)
                Environment.Exit(0);
        }

        /// <summary>
        /// Updates the server list.
        /// </summary>
        /// <param name="packet"></param>
        private void OnUpdateServerList(NetPacketBase packet)
        {
            LoginServer.Clusters.Clear();
            var clusterCount = packet.Read<int>();

            for (int i = 0; i < clusterCount; ++i)
            {
                var clusterId = packet.Read<int>();
                var clusterIp = packet.Read<string>();
                var clusterName = packet.Read<string>();

                var cluster = new ClusterServerInfo(clusterId, clusterIp, clusterName);

                var worldsCount = packet.Read<int>();

                for (int j = 0; j < worldsCount; ++j)
                {
                    var worldId = packet.Read<int>();
                    var worldIp = packet.Read<string>();
                    var worldName = packet.Read<string>();
                    var worldCapacity = packet.Read<int>();
                    var worldConnectedPlayers = packet.Read<int>();

                    cluster.Worlds.Add(new WorldServerInfo(worldId, cluster.Id, worldCapacity, worldIp, worldName, worldConnectedPlayers));
                }

                LoginServer.Clusters.Add(cluster);
            }
        }
    }
}
