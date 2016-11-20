using Ether.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Cluster
{
    public class ClusterServer : NetServer<ClusterClient>
    {
        /// <summary>
        /// Creates a new ClusterServer instance.
        /// </summary>
        public ClusterServer()
            : base()
        {
        }

        public override void DisposeServer()
        {
        }

        protected override void Idle()
        {
        }

        protected override void Initialize()
        {
        }

        protected override void OnClientConnected(NetConnection client)
        {
        }

        protected override void OnClientDisconnected(NetConnection client)
        {
        }
    }
}
