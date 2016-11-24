using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using Hellion.Core.ISC.Structures;
using System.Net.Sockets;

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
        /// Print disconnected log.
        /// </summary>
        internal void Disconnected()
        {
            if (this.ServerInfo is LoginServerInfo)
                Log.Info("LoginServer disconnected.");
            else if (this.ServerInfo is ClusterServerInfo)
            {
                var clusterInfo = this.ServerInfo as ClusterServerInfo;

                Log.Info("ClusterServer '{0}' disconnected.", clusterInfo.Name);
            }
            else if (this.ServerInfo is WorldServerInfo)
            {
                var worldInfo = this.ServerInfo as WorldServerInfo;

                Log.Info("WorldServer '{0}' disconnected.", worldInfo.Name);
            }
            else
                Log.Info("Unknow client disconnected.");
        }
    }
}
