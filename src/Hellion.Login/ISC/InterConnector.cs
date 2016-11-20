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
                case InterHeaders.CanAuthtificate: this.Authentificate(); break;
                case InterHeaders.AuthentificationResult: this.OnAuthentificationResult(packet); break;
                case InterHeaders.UpdateServerList: this.OnUpdateServerList(packet); break;

                default: Log.Warning("Unknow packet header: 0x{0}", packetHeaderNumber.ToString("X2")); break;
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

        private void OnAuthentificationResult(NetPacketBase packet)
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

                    cluster.Worlds.Add(new WorldServerInfo(worldId, cluster.Id, worldCapacity, worldIp, worldName));
                }

                LoginServer.Clusters.Add(cluster);
            }
        }
    }
}
