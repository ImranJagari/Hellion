using System.Runtime.Serialization;

namespace Hellion.Core.Configuration
{
    /// <summary>
    /// Represents the World server configration file.
    /// </summary>
    [DataContract]
    public class WorldConfiguration
    {
        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        [DataMember(Name = "ip")]
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the listening port.
        /// </summary>
        [DataMember(Name = "port")]
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the ISC configuration.
        /// </summary>
        [DataMember(Name = "isc")]
        public ISCConfiguration ISC { get; set; }

        /// <summary>
        /// Creates a new World configuration instance.
        /// </summary>
        public WorldConfiguration()
        {
            this.Ip = Global.LocalAddress;
            this.Port = Global.WorldDefaultPort;
            this.ISC = new ISCConfiguration();
        }
    }
}
