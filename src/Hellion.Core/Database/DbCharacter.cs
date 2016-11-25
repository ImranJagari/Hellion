using System.ComponentModel.DataAnnotations.Schema;

namespace Hellion.Core.Database
{
    [Table("characters")]
    public class DbCharacter
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("accountId")]
        public int AccountId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("gender")]
        public sbyte Gender { get; set; }

        [Column("level")]
        public int Level { get; set; }

        [Column("classId")]
        public int ClassId { get; set; }

        [Column("gold")]
        public int Gold { get; set; }

        [Column("slot")]
        public int Slot { get; set; }

        [Column("strength")]
        public int Strength { get; set; }

        [Column("stamina")]
        public int Stamina { get; set; }

        [Column("dexterity")]
        public int Dexterity { get; set; }

        [Column("intelligence")]
        public int Intelligence { get; set; }

        [Column("hairId")]
        public int HairId { get; set; }

        [Column("hairColor")]
        public int HairColor { get; set; }

        [Column("faceId")]
        public int FaceId { get; set; }

        [Column("mapId")]
        public int MapId { get; set; }

        [Column("posX")]
        public float PosX { get; set; }

        [Column("posY")]
        public float PosY { get; set; }

        [Column("posZ")]
        public float PosZ { get; set; }
    }
}
