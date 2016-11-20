using Ether.Network;
using Hellion.Core;
using Hellion.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ether.Network.Packets;

namespace Hellion.Cluster
{
    public class ClusterClient : NetConnection
    {
        private uint sessionId;

        /// <summary>
        /// Creates a new ClusterClient instance.
        /// </summary>
        public ClusterClient()
            : base()
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
        }

        /// <summary>
        /// Creates a new ClusterClient instance.
        /// </summary>
        /// <param name="socket">Cluster client socket</param>
        public ClusterClient(Socket socket)
            : base(socket)
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
        }

        /// <summary>
        /// Send welcome message.
        /// </summary>
        public override void Greetings()
        {
        }

        /// <summary>
        /// Handle incoming packet.
        /// </summary>
        /// <param name="packet">Incoming packet</param>
        public override void HandleMessage(NetPacketBase packet)
        {
            base.HandleMessage(packet);
        }
    }
}
