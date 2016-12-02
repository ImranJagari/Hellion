using Hellion.Cluster.Structures;
using Hellion.Core.Data.Headers;
using Hellion.Core.Database;
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

        /// <summary>
        /// Sends a cluster error to the client.
        /// </summary>
        /// <param name="code"></param>
        private void SendClusterError(ClusterHeaders.Errors code)
        {
            this.SendClusterMessage((int)code);
        }

        /// <summary>
        /// Sends a cluster message to the client with the specific code.
        /// </summary>
        /// <param name="code"></param>
        private void SendClusterMessage(int code)
        {
            var packet = new FFPacket();

            packet.WriteHeader(ClusterHeaders.Outgoing.LoginMessage);
            packet.Write(code);

            this.Send(packet);
        }

        private void SendWorldIp(string ip)
        {
            var packet = new FFPacket();

            packet.WriteHeader(ClusterHeaders.Outgoing.GameServerIP);
            packet.Write(ip);

            this.Send(packet);
        }

        /// <summary>
        /// Send the login protect to the client
        /// </summary>
        public void SendLoginNumPad()
        {
            var packet = new FFPacket();
            
            packet.Write(0x88100200);
            packet.Write(this.loginProtectValue);

            this.Send(packet);
        }

        /// <summary>
        /// Send the character list to the client.
        /// </summary>
        /// <param name="authKey">authentication key</param>
        /// <param name="characters"></param>
        private void SendCharacterList(int authKey, IEnumerable<DbCharacter> characters)
        {
            var packet = new FFPacket();

            packet.WriteHeader(ClusterHeaders.Outgoing.CharacterList);
            packet.Write(authKey);

            if (characters.Any())
            {
                packet.Write(characters.Count());
                foreach (var character in characters)
                {
                    packet.Write(character.Slot);
                    packet.Write(1); // this number represents the selected character in the window
                    packet.Write(character.MapId);
                    packet.Write(0x0B + character.Gender); // Model id
                    packet.Write(character.Name);
                    packet.Write(character.PosX);
                    packet.Write(character.PosY);
                    packet.Write(character.PosZ);
                    packet.Write(character.Id);
                    packet.Write(0); // Party id
                    packet.Write(0); // Guild id
                    packet.Write(0); // War Id
                    packet.Write(character.SkinSetId); // SkinSet Id
                    packet.Write(character.HairId);
                    packet.Write(character.HairColor);
                    packet.Write(character.FaceId);
                    packet.Write(character.Gender);
                    packet.Write(character.ClassId);
                    packet.Write(character.Level);
                    packet.Write(0); // Job Level (Maybe master of hero ?)
                    packet.Write(character.Strength);
                    packet.Write(character.Stamina);
                    packet.Write(character.Dexterity);
                    packet.Write(character.Intelligence);
                    packet.Write(0); // Mode ??
                    packet.Write(character.Items.Count);

                    foreach (var item in character.Items)
                        packet.Write(item.ItemId);
                }

                // Messenger?
                packet.Write(0);
            }
            else
                packet.Write<long>(0);

            this.Send(packet);
        }
    }
}
