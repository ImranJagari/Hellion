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

        public sbyte Gender { get; set; }

        public int Level { get; set; }

        public int ClassId { get; set; }

        public int Gold { get; set; }

        public int Slot { get; set; }

        public int Strength { get; set; }

        public int Stamina { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int HairId { get; set; }

        public int HairColor { get; set; }

        public int FaceId { get; set; }

        public int MapId { get; set; }

        public float PosX { get; set; }

        public float PosY { get; set; }

        public float PosZ { get; set; }

        public void FromDbCharacter(DbCharacter dbCharacter)
        {
            Mapper.Map(dbCharacter, this);
        }
    }
}
