using System.Runtime.Serialization;

namespace Hellion.Core.Configuration
{
    [DataContract]
    public class ISCConfiguration
    {
        /// <summary>
        /// Gets or sets the IP adress.
        /// </summary>
        [DataMember(Name = "ip")]
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the listening port.
        /// </summary>
        [DataMember(Name = "port")]
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the inter password.
        /// </summary>
        /// <remarks>
        /// This password will be used during the authentification process.
        /// It will allow only the servers that have the this password to authentificate them selfs.
        /// </remarks>
        [DataMember(Name = "interPassword")]
        public string Password { get; set; }

        /// <summary>
        /// Creates a new ISCConfiguration instance.
        /// </summary>
        public ISCConfiguration()
        {
            this.Ip = Global.LocalAddress;
            this.Port = Global.IscDefaultPort;
            this.Password = Global.IscDefaultPassword;
        }
    }
}
