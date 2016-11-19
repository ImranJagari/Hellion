using Ether.Network;
using Ether.Network.Packets;
using Hellion.Core;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using Hellion.Core.Network;
using System.Net.Sockets;
using System.Linq;
using Hellion.Core.ISC.Structures;

namespace Hellion.Login
{
    /// <summary>
    /// Represents a Login client.
    /// </summary>
    public class Client : NetConnection
    {
        private uint sessionId;

        /// <summary>
        /// Gets or sets the LoginServer reference.
        /// </summary>
        public LoginServer Server { get; set; }

        /// <summary>
        /// Creates a new Login Client instance.
        /// </summary>
        public Client()
            : base()
        {
            this.sessionId = (uint)Global.GenerateRandomNumber();
        }

        /// <summary>
        /// Creates a new Login Client instance.
        /// </summary>
        /// <param name="socket">Socket</param>
        public Client(Socket socket)
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
                case LoginHeaders.Incoming.LoginRequest:
                    this.OnLoginRequest(pak);
                    break;
                default: FFPacket.UnknowPacket<LoginHeaders.Incoming>(packetHeaderNumber, 2); break;
            }
        }

        private void OnLoginRequest(FFPacket packet)
        {
            var buildVersion = packet.Read<string>();
            var username = packet.Read<string>();
            var password = packet.Read<string>();

            Log.Debug("Recieved from client: buildVersion: {0}, username: {1}, password: {2}", buildVersion, username, password);

            // Database request
            var user = (from x in LoginServer.DbContext.Users
                        where x.Username == username
                        select x).FirstOrDefault();

            if (user == null)
            {
                Log.Info($"User '{username}' logged in with bad credentials. (Bad username)");
                this.SendLoginError(LoginHeaders.LoginErrors.WrongID);
                this.Server.RemoveClient(this);
            }
            else
            {
                if (password.ToLower() != user.Password.ToLower())
                {
                    Log.Info($"User '{username}' logged in with bad credentials. (Bad password)");
                    this.SendLoginError(LoginHeaders.LoginErrors.WrongPassword);
                    this.Server.RemoveClient(this);
                    return;
                }

                if (user.Authority <= 0)
                {
                    Log.Info($"User '{username}' account is suspended.");
                    this.SendLoginError(LoginHeaders.LoginErrors.AccountSuspended);
                    this.Server.RemoveClient(this);
                    return;
                }

                Client connectedClient = null;
                if (this.IsAlreadyConnected(out connectedClient))
                {
                    this.SendLoginError(LoginHeaders.LoginErrors.AccountAlreadyOn);
                    this.Server.RemoveClient(this);
                    this.Server.RemoveClient(connectedClient);
                    return;
                }

                // Send server list
                this.SendServerList();
            }
        }

        private bool IsAlreadyConnected(out Client connectedClient)
        {
            var connectedClients = from x in this.Server.Clients.Cast<Client>()
                                  where x.Socket.Connected
                                  where x.GetHashCode() != this.GetHashCode()
                                  select x;

            connectedClient = connectedClients.FirstOrDefault();

            return connectedClients.Any();
        }

        private int GetServerCount()
        {
            int count = 0;

            foreach (var cluster in LoginServer.Clusters)
                count += cluster.Worlds.Count + 1; // plus one is for the current cluster

            return count;
        }

        private void SendLoginError(LoginHeaders.LoginErrors code)
        {
            this.SendLoginMessage((int)code);
        }

        private void SendLoginMessage(int code)
        {
            var packet = new FFPacket();

            packet.WriteHeader(LoginHeaders.Outgoing.LoginMessage);
            packet.Write(code);

            this.Send(packet);
        }

        private void SendServerList()
        {
            var packet = new FFPacket();

            packet.WriteHeader(LoginHeaders.Outgoing.ServerList);
            packet.Write(0);
            packet.Write<byte>(1);
            packet.Write("admin");
            packet.Write(this.GetServerCount());

            foreach (ClusterServerInfo cluster in LoginServer.Clusters)
            {
                packet.Write(-1);
                packet.Write(cluster.Id);
                packet.Write(cluster.Name);
                packet.Write(cluster.Ip);
                packet.Write(0);
                packet.Write(0);
                packet.Write(1);
                packet.Write(0);

                foreach (WorldServerInfo world in cluster.Worlds)
                {
                    packet.Write(cluster.Id);
                    packet.Write(world.Id); 
                    packet.Write(world.Name); // Channel name
                    packet.Write(world.Ip);
                    packet.Write(0);
                    packet.Write(0);
                    packet.Write(1);
                    packet.Write(world.Capacity);
                }
            }

            this.Send(packet);
        }
    }
}
