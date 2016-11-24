using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using Hellion.Core.ISC.Structures;

namespace Hellion.ISC
{
    public partial class InterClient
    {
        /// <summary>
        /// Process the authentification of a server.
        /// </summary>
        /// <param name="packet">Incoming packet</param>
        private void OnAuthentification(NetPacketBase packet)
        {
            var serverTypeNumber = packet.Read<int>();
            var interPassword = packet.Read<string>();
            var serverType = (InterServerType)serverTypeNumber;

            if (interPassword.ToLower() != this.Server.IscConfiguration.Password.ToLower())
            {
                Log.Warning("A client tried to authentificate with an incorect password.");
                this.Server.RemoveClient(this);
                return;
            }

            if (serverType == InterServerType.Login)
            {
                if (this.Server.HasLoginServerConnected())
                {
                    Log.Warning("A login server is already connected to the ISC.");
                    this.SendAuthentificationResult(false);
                    this.Server.RemoveClient(this);
                    return;
                }

                Log.Info("New LoginServer authentificated from {0}.", this.Socket.RemoteEndPoint.ToString());
            }

            if (serverType == InterServerType.Cluster && this.Server.HasLoginServerConnected())
            {
                int clusterId = packet.Read<int>();
                string clusterName = packet.Read<string>();
                string clusterIp = packet.Read<string>();

                if (this.Server.HasClusterWithId(clusterId))
                {
                    Log.Warning("A cluster server with same id is already connected to the ISC.");
                    this.SendAuthentificationResult(false);
                    this.Server.RemoveClient(this);
                    return;
                }
                
                this.ServerInfo = new ClusterServerInfo(clusterId, clusterIp, clusterName);

                Log.Info("New ClusterServer '{0}' authentificated from {1}", clusterName, this.Socket.RemoteEndPoint.ToString());
            }

            if (serverType == InterServerType.World)
            {
                int clusterId = packet.Read<int>();
                int worldId = packet.Read<int>();
                string worldName = packet.Read<string>();
                string worldIp = packet.Read<string>();
                int capacity = packet.Read<int>();
                int connectedPlayerCount = packet.Read<int>();

                if (this.Server.HasClusterWithId(clusterId) == false)
                {
                    Log.Warning("WorldServer '{0}' tried to connect to an unknown cluster.", worldName);
                    this.SendAuthentificationResult(false);
                    this.Server.RemoveClient(this);
                    return;
                }

                if (this.Server.HasWorldInCluster(clusterId, worldId))
                {
                    Log.Warning("A WorldServer with id {0} already exists in Cluster {1}", worldId, clusterId);
                    this.SendAuthentificationResult(false);
                    this.Server.RemoveClient(this);
                    return;
                }

                this.ServerInfo = new WorldServerInfo(worldId, clusterId, capacity, worldIp, worldName, connectedPlayerCount);
                Log.Info("New WorldServer '{0}' authentificated from {1}", worldName, this.Socket.RemoteEndPoint.ToString());
            }

            this.ServerType = serverType;
            this.SendAuthentificationResult(true);
            this.SendServersList();
        }
    }
}
