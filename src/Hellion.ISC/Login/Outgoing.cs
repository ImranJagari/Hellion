﻿using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.ISC.Structures;
using System.Collections.Generic;
using System.Linq;

namespace Hellion.ISC
{
    public partial class InterClient
    {
        /// <summary>
        /// Send the authentification result.
        /// </summary>
        /// <param name="result"></param>
        private void SendAuthentificationResult(bool result)
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.AuthenticationResult);
            packet.Write(result);

            this.Send(packet);
        }

        /// <summary>
        /// Send the server list to the LoginServer.
        /// </summary>
        public void SendServersList()
        {
            var packet = new NetPacket();
            IEnumerable<ClusterServerInfo> clusters = this.Server.GetClusters();

            packet.Write((int)InterHeaders.UpdateServerList);
            packet.Write(clusters.Count());

            foreach (var cluster in clusters)
            {
                packet.Write(cluster.Id);
                packet.Write(cluster.Ip);
                packet.Write(cluster.Name);

                IEnumerable<WorldServerInfo> worlds = this.Server.GetWorldsByClusterId(cluster.Id);

                packet.Write(worlds.Count());
                foreach (var world in worlds)
                {
                    packet.Write(world.Id);
                    packet.Write(world.Ip);
                    packet.Write(world.Name);
                    packet.Write(world.Capacity);
                    packet.Write(world.ConnectedPlayerCount);
                }
            }

            this.Server.SendPacketToLoginServer(packet);
        }

        public void SendWorldServerListToCluster(int clusterId)
        {
            InterClient clusterClient = this.Server.GetClusterById(clusterId);
            IEnumerable<WorldServerInfo> worlds = this.Server.GetWorldsByClusterId(clusterId);

            if (clusterClient != null && worlds.Any())
            {
                var packet = new NetPacket();

                packet.Write((int)InterHeaders.UpdateWorldServerList);
                packet.Write(worlds.Count());

                foreach (var worldServer in worlds)
                {
                    packet.Write(worldServer.Id);
                    packet.Write(worldServer.Ip);
                    packet.Write(worldServer.Name);
                    packet.Write(worldServer.Capacity);
                    packet.Write(worldServer.ConnectedPlayerCount);
                }

                clusterClient.Send(packet);
            }
        }
    }
}
