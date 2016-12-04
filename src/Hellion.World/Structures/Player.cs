using Hellion.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.World.Structures
{
    /// <summary>
    /// Represents a real player in the world.
    /// </summary>
    public class Player : Mover
    {
        public int PlayerId { get; private set; }
        
        public int AccountId { get; set; }
        
        public string Name { get; set; }
        
        public byte Gender { get; set; }

        public long Experience { get; set; }
        
        public int ClassId { get; set; }
        
        public int Gold { get; set; }
        
        public int Slot { get; set; }
        
        public int Strength { get; set; }
        
        public int Stamina { get; set; }
        
        public int Dexterity { get; set; }
        
        public int Intelligence { get; set; }

        public int Hp { get; set; }

        public int Mp { get; set; }

        public int Fp { get; set; }
        
        public int SkinSetId { get; set; }
        
        public int HairId { get; set; }
        
        public uint HairColor { get; set; }
        
        public int FaceId { get; set; }
        
        public int BankCode { get; set; }

        // Add:
        // Inventory
        // Quests
        // Guild
        // Friends
        // Skills
        // Buffs
        // etc...

        /// <summary>
        /// Creates a new Player based on a <see cref="DbCharacter"/> stored in database.
        /// </summary>
        /// <param name="dbCharacter">Character stored in database</param>
        public Player(DbCharacter dbCharacter)
            : base(dbCharacter.Gender == 0 ? 11 : 12)
        {
            this.PlayerId = dbCharacter.Id;
            this.AccountId = dbCharacter.AccountId;
            this.Name = dbCharacter.Name;
            this.Gender = dbCharacter.Gender;
            this.ClassId = dbCharacter.ClassId;
            this.Gold = dbCharacter.Gold;
            this.Slot = dbCharacter.Slot;
            this.Strength = dbCharacter.Strength;
            this.Stamina = dbCharacter.Stamina;
            this.Dexterity = dbCharacter.Dexterity;
            this.Intelligence = dbCharacter.Intelligence;
            this.Hp = dbCharacter.Hp;
            this.Mp = dbCharacter.Mp;
            this.Fp = dbCharacter.Fp;
            this.Experience = dbCharacter.Experience;
            this.SkinSetId = dbCharacter.SkinSetId;
            this.HairId = dbCharacter.HairId;
            this.HairColor = dbCharacter.HairColor;
            this.FaceId = dbCharacter.FaceId;
            this.BankCode = dbCharacter.BankCode;
            this.MapId = dbCharacter.MapId;
            this.Position = new Core.Structures.Vector3(dbCharacter.PosX, dbCharacter.PosY, dbCharacter.PosZ);

            // Initialize inventory, quests, guild, friends, skills etc...
        }
    }
}
