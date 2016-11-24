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
    }
}
