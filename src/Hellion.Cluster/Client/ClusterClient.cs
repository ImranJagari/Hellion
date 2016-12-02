using Ether.Network;
using Hellion.Core;
using Hellion.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ether.Network.Packets;
using Hellion.Core.Data.Headers;
using Hellion.Core.Network;
using Hellion.Core.Database;

namespace Hellion.Cluster.Client
{
    public partial class ClusterClient : NetConnection
    {
        private uint sessionId;
        private int selectedServerId;
        private int loginProtectValue;

        /// <summary>
        /// Gets or sets the server reference.
        /// </summary>
        public ClusterServer Server { get; set; }

        /// <summary>
        /// Creates a new ClusterClient instance.
        /// </summary>
        public ClusterClient()
            : base()
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
            this.loginProtectValue = new Random().Next(0, 1000);
        }

        /// <summary>
        /// Creates a new ClusterClient instance.
        /// </summary>
        /// <param name="socket">Cluster client socket</param>
        public ClusterClient(Socket socket)
            : base(socket)
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
            this.loginProtectValue = new Random().Next(0, 1000);
        }

        /// <summary>
        /// Send welcome message.
        /// </summary>
        public override void Greetings()
        {
            this.SendSessionId();
        }

        /// <summary>
        /// Handle incoming packet.
        /// </summary>
        /// <param name="packet">Incoming packet</param>
        public override void HandleMessage(NetPacketBase packet)
        {
            packet.Position += 17;

            var packetHeaderNumber = packet.Read<int>();
            var packetHeader = (ClusterHeaders.Incoming)packetHeaderNumber;

            switch (packetHeader)
            {
                case ClusterHeaders.Incoming.Ping: this.OnPing(packet); break;
                case ClusterHeaders.Incoming.CreateCharacter: this.OnCreateCharacter(packet); break;
                case ClusterHeaders.Incoming.DeleteCharacter: this.OnDeleteCharacter(packet); break;
                case ClusterHeaders.Incoming.CharacterListRequest: this.OnCharacterListRequest(packet); break;

                default: FFPacket.UnknowPacket<ClusterHeaders.Incoming>(packetHeaderNumber, 2); break;
            }

            base.HandleMessage(packet);
        }

        private DbUser GetUserAccount(string username, string password)
        {
            var accounts = from x in DatabaseService.Users.GetAll()
                           where x.Username.ToLower() == username.ToLower()
                           where x.Password.ToLower() == password.ToLower()
                           where x.Authority != 0
                           select x;

            return accounts.FirstOrDefault();
        }

        public string GetWorldIpBySelectedServerId()
        {
            return (from x in this.Server.ConnectedWorldServers
                    where x.Id == this.selectedServerId
                    select x.Ip).FirstOrDefault();
        }
    }
}
