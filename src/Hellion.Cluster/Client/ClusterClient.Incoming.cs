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
                Log.Warning("Unknow account for username '{0}'.", username);
                this.Server.RemoveClient(this);
                return;
            }

            this.Id = account.Id;
            this.selectedServerId = serverId;
            var characters = from x in DatabaseService.Characters.GetAll(c => c.Items)
                             where x.AccountId == account.Id
                             select x;

            this.SendWorldIp(this.GetWorldIpBySelectedServerId());
            this.SendCharacterList(authKey, characters.ToList());
            this.SendLoginNumPad();
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
                Log.Warning("Unknow account for username '{0}'.", username);
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
                BankCode = bankPassword,
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
                    new DbItem() { ItemId = defaultEquipment.StartSuit, ItemSlot = 44, ItemCount = 1 },
                    new DbItem() { ItemId = defaultEquipment.StartHand, ItemSlot = 46, ItemCount = 1 },
                    new DbItem() { ItemId = defaultEquipment.StartShoes, ItemSlot = 47, ItemCount = 1 },
                    new DbItem() { ItemId = defaultEquipment.StartWeapon, ItemSlot = 52, ItemCount = 1 }
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
                Log.Warning("Unknow account for username '{0}'.", username);
                this.Server.RemoveClient(this);
                return;
            }

            if (password.ToLower() != passwordVerification.ToLower())
            {
                Log.Error("Password doesn't match for client '{0}' with id {1}", account.Username, account.Id);
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

        /// <summary>
        /// On character pre join the world.
        /// </summary>
        /// <param name="packet"></param>
        private void OnPreJoin(NetPacketBase packet)
        {
            var username = packet.Read<string>();
            var characterId = packet.Read<int>();
            var characterName = packet.Read<string>();
            var bankCode = packet.Read<int>();
            var account = DatabaseService.Users.Get(x => x.Id == this.Id);

            if (account == null || account.Username.ToLower() != username.ToLower())
            {
                Log.Warning("Hack attempt. Wrong username for account with id {1}.", this.Id);
                this.Server.RemoveClient(this);
                return;
            }

            var selectedCharacter = DatabaseService.Characters.Get(x => x.AccountId == account.Id && x.Id == characterId);

            if (selectedCharacter == null)
            {
                Log.Error("Cannot find character '{0}' with id {1} in database.", characterName, characterId);
                this.Server.RemoveClient(this);
                return;
            }

            if (this.Server.ClusterConfiguration.EnableLoginProtect)
            {
                bankCode = LoginProtect.GetNumPadToPassword(this.loginProtectValue, bankCode);
                if (bankCode != selectedCharacter.BankCode)
                {
                    Log.Error("Character '{0}' tried to connect with incorrect bank password.", selectedCharacter.Name);
                    this.loginProtectValue = new Random().Next(0, 1000);
                    this.SendLoginProtect();
                    return;
                }
            }

            this.SendJoinWorld();
        }
    }
}
