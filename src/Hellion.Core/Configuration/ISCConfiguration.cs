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
        /// Creates a new ISCConfiguration instance.
        /// </summary>
        public ISCConfiguration()
        {
            this.Ip = Global.LocalAddress;
            this.Port = Global.IscDefaultPort;
        }
    }
}
