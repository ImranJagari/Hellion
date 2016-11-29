using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core;
using Hellion.Core.Cryptography;
using Hellion.Core.Data.Headers;
using Hellion.Core.Network;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Hellion.Login.Client
{
    /// <summary>
    /// Represents a Login client.
    /// </summary>
    public partial class LoginClient : NetConnection
    {
        private uint sessionId;

        /// <summary>
        /// Gets or sets the LoginServer reference.
        /// </summary>
        public LoginServer Server { get; set; }

        /// <summary>
        /// Creates a new Login Client instance.
        /// </summary>
        public LoginClient()
            : base()
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
        }

        /// <summary>
        /// Creates a new Login Client instance.
        /// </summary>
        /// <param name="socket">Socket</param>
        public LoginClient(Socket socket)
            : base(socket)
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
        }

        /// <summary>
        /// Send greetings to the client.
        /// </summary>
        public override void Greetings()
        {
            var packet = new FFPacket();

            packet.WriteHeader(LoginHeaders.Outgoing.Welcome);
            packet.Write(this.sessionId);

            this.Send(packet);
        }

        /// <summary>
        /// Handle incoming packets.
        /// </summary>
        /// <param name="packet">Incoming packet</param>
        public override void HandleMessage(NetPacketBase packet)
        {
            packet.Position += 13;
            var packetHeaderNumber = packet.Read<int>();
            var packetHeader = (LoginHeaders.Incoming)packetHeaderNumber;
            var pak = packet as FFPacket;

            switch (packetHeader)
            {
                case LoginHeaders.Incoming.LoginRequest: this.OnLoginRequest(pak); break;

                default: FFPacket.UnknowPacket<LoginHeaders.Incoming>(packetHeaderNumber, 2); break;
            }
        }

        /// <summary>
        /// Check if the user is already connected.
        /// </summary>
        /// <param name="connectedClient"></param>
        /// <returns></returns>
        private bool IsAlreadyConnected(out LoginClient connectedClient)
        {
            var connectedClients = from x in this.Server.Clients.Cast<LoginClient>()
                                   where x.Socket.Connected
                                   where x.GetHashCode() != this.GetHashCode()
                                   select x;

            connectedClient = connectedClients.FirstOrDefault();

            return connectedClients.Any();
        }

        /// <summary>
        /// Get the server count.
        /// </summary>
        /// <returns></returns>
        private int GetServerCount()
        {
            int count = 0;

            foreach (var cluster in LoginServer.Clusters)
                count += cluster.Worlds.Count + 1; // plus one is for the current cluster

            return count;
        }

        /// <summary>
        /// Decrypt the incoming password using the Rijndael algorithm.
        /// </summary>
        /// <param name="passwordData">Password as byte array</param>
        /// <returns></returns>
        private string DecryptPassword(byte[] passwordData)
        {
            var encryptionKey = this.Server.LoginConfiguration.EncryptionKey;
            var key = Encoding.ASCII.GetBytes(encryptionKey).Concat(Enumerable.Repeat((byte)0, 5).ToArray()).ToArray();

            return Rijndael.Decrypt(passwordData, key).Trim('\0');
        }
    }
}
