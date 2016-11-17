using System.ComponentModel.DataAnnotations.Schema;

namespace Hellion.Core.Database
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("authority")]
        public int Authority { get; set; }
    }
}
