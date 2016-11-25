using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Core.Database
{
    [Table("items")]
    public class DbItem
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("characterId")]
        public int CharacterId { get; set; }

        [Column("itemCount")]
        public int ItemCount { get; set; }

        [Column("itemSlot")]
        public int ItemSlot { get; set; }

        public DbCharacter Character { get; set; }
    }
}
