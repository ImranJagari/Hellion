using Ether.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using Hellion.Core.ISC.Structures;

namespace Hellion.ISC
{
    public partial class InterClient : NetConnection
    {
        /// <summary>
        /// Gets the server informations
        /// </summary>
        public BaseServer ServerInfo { get; private set; }

        /// <summary>
        /// Gets the server type.
        /// </summary>
        public InterServerType ServerType { get; private set; }

        /// <summary>
        /// Gets or sets the InterServer reference.
        /// </summary>
        public InterServer Server { get; internal set; }

        /// <summary>
        /// Creates a new InterClient instance.
        /// </summary>
        public InterClient()
            : base()
        {
            this.ServerType = InterServerType.None;
        }

        /// <summary>
        /// Creates a new InterClient instance.
        /// </summary>
        /// <param name="socket">Socket</param>
        public InterClient(Socket socket)
            : base(socket)
        {
            this.ServerType = InterServerType.None;
        }
        
        /// <summary>
        /// Sends a welcome message to the inter client.
        /// </summary>
        public override void Greetings()
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.CanAuthtificate);

            this.Send(packet);
        }

        /// <summary>
        /// Handles the incoming message.
        /// </summary>
        /// <param name="packet">Packet</param>
        public override void HandleMessage(NetPacketBase packet)
        {
            var packetHeaderNumber = packet.Read<int>();
            var packetHeader = (InterHeaders)packetHeaderNumber;

            switch (packetHeader)
            {
                case InterHeaders.Authentification: this.OnAuthentification(packet); break;

                default:
                    Log.Warning("Unknow packet: 0x{0}", packetHeaderNumber.ToString("X2"));
                    break;
            }
        }

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
                Log.Warning("A client tryied to authentificate with an incorect password.");
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

            if (serverType == InterServerType.Cluster)
            {
            }

            if (serverType == InterServerType.World)
            {
            }

            this.ServerType = serverType;
            this.SendAuthentificationResult(true);
            this.SendServersList();
        }

        /// <summary>
        /// Send the authentification result.
        /// </summary>
        /// <param name="result"></param>
        private void SendAuthentificationResult(bool result)
        {
            var packet = new NetPacket();

            packet.Write((int)InterHeaders.AuthentificationResult);
            packet.Write(result);

            this.Send(packet);
        }

        private IEnumerable<ClusterServerInfo> GetClusters()
        {
            return from x in this.Server.Clients.Cast<InterClient>()
                   where x.ServerType == InterServerType.Cluster
                   select x.ServerInfo as ClusterServerInfo;
        }

        private IEnumerable<WorldServerInfo> GetWorlds(int clusterId)
        {
            return from x in this.Server.Clients.Cast<InterClient>()
                   where x.ServerType == InterServerType.World
                   where (x.ServerInfo as WorldServerInfo).ClusterId == clusterId
                   select x.ServerInfo as WorldServerInfo;
        }

        public void SendServersList()
        {
            var packet = new NetPacket();
            IEnumerable<ClusterServerInfo> clusters = this.GetClusters();

            packet.Write((int)InterHeaders.UpdateServerList);
            packet.Write(clusters.Count());

            foreach (var cluster in clusters)
            {
                packet.Write(cluster.Id);
                packet.Write(cluster.Ip);
                packet.Write(cluster.Name);

                IEnumerable<WorldServerInfo> worlds = this.GetWorlds(cluster.Id);

                packet.Write(worlds.Count());
                foreach (var world in worlds)
                {
                    packet.Write(world.Id);
                    packet.Write(world.Ip);
                    packet.Write(world.Name);
                    packet.Write(world.Capacity);
                }
            }

            this.Server.SendPacketToLoginServer(packet);
        }
    }
}
