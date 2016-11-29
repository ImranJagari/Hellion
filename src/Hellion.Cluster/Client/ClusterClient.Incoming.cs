using Ether.Network.Packets;
using Hellion.Cluster.Structures;
using Hellion.Core.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Cluster.Client
{
    public partial class ClusterClient
    {
        private void OnPing(NetPacketBase packet)
        {
            var time = packet.Read<int>();

            this.SendPong(time);
        }

        private void OnCharacterListRequest(NetPacketBase packet)
        {
            var buildDate = packet.Read<string>();
            var authKey = packet.Read<int>();
            var username = packet.Read<string>();
            var password = packet.Read<string>();
            var serverId = packet.Read<int>();

            var account = (from x in ClusterServer.DbContext.Users
                           where x.Username.ToLower() == username.ToLower()
                           where x.Password.ToLower() == password.ToLower()
                           where x.Authority != 0
                           select x).FirstOrDefault();

            if (account == null)
            {
                this.Server.RemoveClient(this);
                return;
            }

            var characters = from x in ClusterServer.DbContext.Characters.Include(c => c.Items)
                             where x.AccountId == account.Id
                             select x;

            var test = new List<Character>();

            foreach (var c in characters)
                test.Add(new Character(c));
            
            this.SendCharacterList(authKey, test);
        }

        private void OnCreateCharacter(NetPacketBase packet)
        {
            var username = packet.Read<string>();
            var password = packet.Read<string>();
            int slot = packet.Read<byte>();
            var name = packet.Read<string>();
            int faceId = packet.Read<byte>();
            int costumeId = packet.Read<byte>();
            int skinSet = packet.Read<byte>();
            int hairMeshId = packet.Read<byte>();
            uint hairColor = packet.Read<uint>();
            var gender = Math.Min((byte)1, Math.Max((byte)0, packet.Read<byte>()));
            int job = packet.Read<byte>();
            int headMesh = packet.Read<byte>();
            int bankPassword = packet.Read<int>();
            int authKey = packet.Read<int>();

            var account = (from x in ClusterServer.DbContext.Users
                           where x.Username.ToLower() == username.ToLower()
                           where x.Password.ToLower() == password.ToLower()
                           where x.Authority != 0
                           select x).FirstOrDefault();

            if (account == null)
            {
                this.Server.RemoveClient(this);
                return;
            }

            var character = new DbCharacter()
            {
                AccountId = account.Id,
                Slot = slot,
                Name = name,
                HairId = hairMeshId,
                HairColor = hairColor,
                Gender = gender,
                ClassId = 0,
                FaceId = headMesh,
                Level = this.Server.ClusterConfiguration.DefaultCharacter.Level,
                Strength = this.Server.ClusterConfiguration.DefaultCharacter.Strength,
                Stamina = this.Server.ClusterConfiguration.DefaultCharacter.Stamina,
                Dexterity = this.Server.ClusterConfiguration.DefaultCharacter.Dexterity,
                Intelligence = this.Server.ClusterConfiguration.DefaultCharacter.Intelligence,
                MapId = this.Server.ClusterConfiguration.DefaultCharacter.MapId,
                PosX = this.Server.ClusterConfiguration.DefaultCharacter.PosX,
                PosY = this.Server.ClusterConfiguration.DefaultCharacter.PosY,
                PosZ = this.Server.ClusterConfiguration.DefaultCharacter.PosZ,
                Gold = this.Server.ClusterConfiguration.DefaultCharacter.Gold,
            };

            ClusterServer.DbContext.Characters.Add(character);
            ClusterServer.DbContext.SaveChanges();


            var characters = from x in ClusterServer.DbContext.Characters.Include(c => c.Items)
                             where x.AccountId == account.Id
                             select x;

            var test = new List<Character>();

            foreach (var c in characters)
                test.Add(new Character(c));

            this.SendCharacterList(authKey, test);
        }

        private void OnDeleteCharacter(NetPacketBase packet)
        {
            var username = packet.Read<string>();
            var password = packet.Read<string>();
            var passwordVerification = packet.Read<string>();
            var characterId = packet.Read<int>();
            var authKey = packet.Read<int>();
        }
    }
}
