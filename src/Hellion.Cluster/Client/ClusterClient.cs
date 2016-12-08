using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core;
using Hellion.Core.Data.Headers;
using Hellion.Core.Database;
using Hellion.Core.Network;
using System;
using System.Linq;
using System.Net.Sockets;

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

            var packetHeaderNumber = packet.Read<uint>();
            var packetHeader = (ClusterHeaders.Incoming)packetHeaderNumber;

            switch (packetHeader)
            {
                case ClusterHeaders.Incoming.Ping: this.OnPing(packet); break;
                case ClusterHeaders.Incoming.CreateCharacter: this.OnCreateCharacter(packet); break;
                case ClusterHeaders.Incoming.DeleteCharacter: this.OnDeleteCharacter(packet); break;
                case ClusterHeaders.Incoming.CharacterListRequest: this.OnCharacterListRequest(packet); break;
                case ClusterHeaders.Incoming.PreJoin: this.OnPreJoin(packet); break;

                default: FFPacket.UnknowPacket<ClusterHeaders.Incoming>((uint)packetHeaderNumber, 2); break;
            }

            base.HandleMessage(packet);
        }

        /// <summary>
        /// Gets the user account with the username and password.
        /// </summary>
        /// <param name="username">Account username</param>
        /// <param name="password">Account password</param>
        /// <returns></returns>
        private DbUser GetUserAccount(string username, string password)
        {
            var accounts = from x in DatabaseService.Users.GetAll()
                           where x.Username.ToLower() == username.ToLower()
                           where x.Password.ToLower() == password.ToLower()
                           where x.Authority != 0
                           select x;

            return accounts.FirstOrDefault();
        }

        /// <summary>
        /// Gets the world ip address by the selected server id.
        /// </summary>
        /// <returns></returns>
        public string GetWorldIpBySelectedServerId()
        {
            return (from x in this.Server.ConnectedWorldServers
                    where x.Id == this.selectedServerId
                    select x.Ip).FirstOrDefault();
        }
    }
}
