using System.Runtime.Serialization;

namespace Hellion.Core.Configuration
{
    [DataContract]
    public class DatabaseConfiguration
    {
        /// <summary>
        /// Gets or sets the IP address of the database host.
        /// </summary>
        [DataMember(Name = "ip")]
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the username of the database host.
        /// </summary>
        [DataMember(Name = "user")]
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the password of the database host.
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        [DataMember(Name = "databaseName")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Creates a new DatabaseConfiguration instance.
        /// </summary>
        public DatabaseConfiguration()
        {
            this.Ip = Global.LocalAddress;
            this.User = "root";
            this.Password = "";
            this.DatabaseName = "hellion";
        }
    }
}
