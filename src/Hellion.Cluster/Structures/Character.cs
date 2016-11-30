using Hellion.Core.Database;
using Hellion.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Cluster.Structures
{
    public class Character
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Name { get; set; }

        public byte Gender { get; set; }

        public int Level { get; set; }

        public int ClassId { get; set; }

        public int Gold { get; set; }

        public int Slot { get; set; }

        public int Strength { get; set; }

        public int Stamina { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int SkinSetId { get; set; }

        public int HairId { get; set; }

        public uint HairColor { get; set; }

        public int FaceId { get; set; }

        public int MapId { get; set; }

        public float PosX { get; set; }

        public float PosY { get; set; }

        public float PosZ { get; set; }

        public List<int> ItemsId { get; set; }

        public object Items { get; set; }

        public Character()
        {
            this.ItemsId = new List<int>();
        }

        public Character(DbCharacter dbCharacter)
        {
            this.FromDbCharacter(dbCharacter);

            this.ItemsId = new List<int>();

            if (dbCharacter.Items.Any())
                foreach (var item in dbCharacter.Items)
                    this.ItemsId.Add(item.Id);
        }

        public void FromDbCharacter(DbCharacter dbCharacter)
        {
            Mapper.Map(dbCharacter, this);
        }
    }
}
