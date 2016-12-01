using Ether.Network.Packets;
using Hellion.Core.Configuration;
using Hellion.Core.Data.Headers;
using Hellion.Core.Database;
using Hellion.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hellion.Cluster.Client
{
    public partial class ClusterClient
    {
        /// <summary>
        /// Recieves the ping request and send the pong.
        /// </summary>
        /// <param name="packet"></param>
        private void OnPing(NetPacketBase packet)
        {
            var time = packet.Read<int>();

            this.SendPong(time);
        }

        /// <summary>
        /// Retrieves all character of the current account and send them to the client.
        /// </summary>
        /// <param name="packet"></param>
        private void OnCharacterListRequest(NetPacketBase packet)
        {
            var buildDate = packet.Read<string>();
            var authKey = packet.Read<int>();
            var username = packet.Read<string>();
            var password = packet.Read<string>();
            var serverId = packet.Read<int>();
            var account = this.GetUserAccount(username, password);

            if (account == null)
            {
                this.Server.RemoveClient(this);
                return;
            }

            var characters = from x in DatabaseService.Characters.GetAll(c => c.Items)
                             where x.AccountId == account.Id
                             select x;

            this.SendCharacterList(authKey, characters.ToList());
        }

        /// <summary>
        /// Creates a new character.
        /// </summary>
        /// <param name="packet"></param>
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
            var account = this.GetUserAccount(username, password);

            if (account == null)
            {
                this.Server.RemoveClient(this);
                return;
            }

            var characterWithSameName = DatabaseService.Characters.Get(x => x.Name.ToLower() == name.ToLower());
            if (characterWithSameName != null)
            {
                this.SendClusterError(ClusterHeaders.Errors.NameAlreadyInUse);
                return;
            }

            DefaultStartItem defaultEquipment = gender == 0 ? 
                this.Server.ClusterConfiguration.DefaultCharacter.Man :
                this.Server.ClusterConfiguration.DefaultCharacter.Woman;

            var character = new DbCharacter()
            {
                AccountId = account.Id,
                Slot = slot,
                Name = name,
                SkinSetId = skinSet,
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
                Items = new HashSet<DbItem>()
                {
                    new DbItem() { Id = defaultEquipment.StartSuit, ItemSlot = 44, ItemCount = 1 },
                    new DbItem() { Id = defaultEquipment.StartHand, ItemSlot = 46, ItemCount = 1 },
                    new DbItem() { Id = defaultEquipment.StartShoes, ItemSlot = 47, ItemCount = 1 },
                    new DbItem() { Id = defaultEquipment.StartWeapon, ItemSlot = 52, ItemCount = 1 }
                }
            };

            DatabaseService.Characters.Add(character);
            Log.Info("Character '{0}' has been created!", character.Name);
            
            var characters = from x in DatabaseService.Characters.GetAll(c => c.Items)
                             where x.AccountId == account.Id
                             select x;
            
            this.SendCharacterList(authKey, characters.ToList());
        }

        /// <summary>
        /// Delete a character.
        /// </summary>
        /// <param name="packet"></param>
        private void OnDeleteCharacter(NetPacketBase packet)
        {
            var username = packet.Read<string>();
            var password = packet.Read<string>();
            var passwordVerification = packet.Read<string>();
            var characterId = packet.Read<int>();
            var authKey = packet.Read<int>();
            var account = this.GetUserAccount(username, password);

            if (account == null)
            {
                this.Server.RemoveClient(this);
                return;
            }

            if (password.ToLower() != passwordVerification.ToLower())
            {
                this.SendClusterError(ClusterHeaders.Errors.PasswordDontMatch);
                return;
            }

            var character = DatabaseService.Characters.Get(x => x.Id == characterId);

            if (character == null)
            {
                Log.Warning("Unknow character with Id: {0}", characterId);
                return;
            }

            DatabaseService.Characters.Delete(character);
            Log.Info("Character '{0}' has been deleted.", character.Name);

            var characters = from x in DatabaseService.Characters.GetAll()
                             where x.AccountId == account.Id
                             select x;
            
            this.SendCharacterList(authKey, characters.ToList());
        }
    }
}
