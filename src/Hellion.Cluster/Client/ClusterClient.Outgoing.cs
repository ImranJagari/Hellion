using Hellion.Core.Data.Headers;
using Hellion.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Cluster.Client
{
    public partial class ClusterClient
    {
        /// <summary>
        /// Sends the cluster client session Id.
        /// </summary>
        private void SendSessionId()
        {
            var packet = new FFPacket();

            packet.Write(0);
            packet.Write((int)this.sessionId);

            this.Send(packet);
        }

        /// <summary>
        /// Send the pong request to the client.
        /// </summary>
        /// <param name="time"></param>
        private void SendPong(int time)
        {
            var packet = new FFPacket();

            packet.WriteHeader(ClusterHeaders.Outgoing.Pong);
            packet.Write(time);

            this.Send(packet);
        }
    }
}
