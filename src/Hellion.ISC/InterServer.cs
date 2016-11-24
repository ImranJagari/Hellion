using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core.Configuration;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using Hellion.Core.ISC.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hellion.ISC
{
    public sealed class InterServer : NetServer<InterClient>
    {
        /// <summary>
        /// ISC Configuration file path.
        /// </summary>
        private const string IscConfigurationFile = "config/isc.json";

        /// <summary>
        /// Gets the ISC configuration.
        /// </summary>
        public ISCConfiguration IscConfiguration { get; private set; }

        /// <summary>
        /// Creates a new InterServer instance.
        /// </summary>
        public InterServer()
            : base()
        {
            Console.Title = "Hellion ISC";
        }

        /// <summary>
        /// Idle the inter-server.
        /// </summary>
        protected override void Idle()
        {
            Log.Info("ISC Server listening on port {0}", this.Configuration.Port);

            while (this.IsRunning)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Initialize the inter-server.
        /// </summary>
        protected override void Initialize()
        {
            this.LoadConfiguration();
        }

        /// <summary>
        /// On client connected.
        /// </summary>
        /// <param name="client">Client</param>
        protected override void OnClientConnected(NetConnection client)
        {
            Log.Info("New inter client connected from {0}.", client.Socket.RemoteEndPoint.ToString());

            if (client is InterClient)
                (client as InterClient).Server = this;
        }

        /// <summary>
        /// On client disconnected.
        /// </summary>
        /// <param name="client">Client</param>
        protected override void OnClientDisconnected(NetConnection client)
        {
            if (client is InterClient)
                (client as InterClient).Disconnected();

            InterClient loginServer = this.GetLoginServer();

            loginServer?.SendServersList();
        }

        /// <summary>
        /// Dispose the server's resources.
        /// </summary>
        public override void DisposeServer()
        {
        }

        /// <summary>
        /// Load the server configuration.
        /// </summary>
        private void LoadConfiguration()
        {
            Log.Info("Loading configuration...");

            if (File.Exists(IscConfigurationFile) == false)
                ConfigurationManager.Save(new ISCConfiguration(), IscConfigurationFile);

            this.IscConfiguration = ConfigurationManager.Load<ISCConfiguration>(IscConfigurationFile);

            this.Configuration.Ip = this.IscConfiguration.Ip;
            this.Configuration.Port = this.IscConfiguration.Port;

            Log.Done("Configuration loaded!");
        }

        /// <summary>
        /// Send a packet to the login server.
        /// </summary>
        /// <param name="packet"></param>
        internal void SendPacketToLoginServer(NetPacketBase packet)
        {
            var loginServer = this.GetLoginServer();

            loginServer?.Send(packet);
        }
        
        /// <summary>
        /// Get all clusters connected.
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<ClusterServerInfo> GetClusters()
        {
            return from x in this.Clients.Cast<InterClient>()
                   where x.ServerType == InterServerType.Cluster
                   where x.Socket.Connected
                   select x.ServerInfo as ClusterServerInfo;
        }

        /// <summary>
        /// Verify if there is already a login server connected to the ISC.
        /// </summary>
        /// <returns></returns>
        internal bool HasLoginServerConnected()
        {
            return this.GetLoginServer() != null;
        }

        /// <summary>
        /// Gets the login server.
        /// </summary>
        /// <returns></returns>
        internal InterClient GetLoginServer()
        {
            return (from x in this.Clients.Cast<InterClient>()
                    where x.ServerType == InterServerType.Login
                    select x).FirstOrDefault();
        }

        /// <summary>
        /// Get all worlds connected by cluster Id.
        /// </summary>
        /// <param name="clusterId">Parent cluster Id</param>
        /// <returns></returns>
        internal IEnumerable<WorldServerInfo> GetWorldsByClusterId(int clusterId)
        {
            return from x in this.Clients.Cast<InterClient>()
                   where x.ServerType == InterServerType.World
                   where (x.ServerInfo as WorldServerInfo).ClusterId == clusterId
                   where x.Socket.Connected
                   select x.ServerInfo as WorldServerInfo;
        }

        /// <summary>
        /// Get cluster server by Id.
        /// </summary>
        /// <param name="clusterId">Cluster Server Id</param>
        /// <returns></returns>
        internal InterClient GetClusterById(int clusterId)
        {
            return (from x in this.Clients.Cast<InterClient>()
                    where x.ServerType == InterServerType.Cluster
                    where (x.ServerInfo as ClusterServerInfo).Id == clusterId
                    select x).FirstOrDefault();
        }

        /// <summary>
        /// Get world server by Id.
        /// </summary>
        /// <param name="worldId">World server Id</param>
        /// <returns></returns>
        internal InterClient GetWorldById(int worldId)
        {
            return (from x in this.Clients.Cast<InterClient>()
                    where x.ServerType == InterServerType.World
                    where (x.ServerInfo as WorldServerInfo).Id == worldId
                    select x).FirstOrDefault();
        }

        /// <summary>
        /// Check if there is a cluster server with the same Id.
        /// </summary>
        /// <param name="clusterId">Cluster Server Id</param>
        /// <returns></returns>
        internal bool HasClusterWithId(int clusterId)
        {
            return this.GetClusterById(clusterId) != null;
        }

        /// <summary>
        /// Check if the cluster id has worlds.
        /// </summary>
        /// <param name="clusterId"></param>
        /// <returns></returns>
        internal bool HasWorldWithClusterId(int clusterId)
        {
            return this.GetWorldsByClusterId(clusterId).Any();
        }

        internal bool HasWorldInCluster(int clusterId, int worldId)
        {
            var worlds = this.GetWorldsByClusterId(clusterId);

            return (from x in worlds
                    where x.Id == worldId
                    select x).Any();
        }
    }
}
