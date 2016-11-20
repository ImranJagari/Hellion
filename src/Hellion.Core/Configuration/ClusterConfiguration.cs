using System.Runtime.Serialization;

namespace Hellion.Core.Configuration
{
    /// <summary>
    /// Reprensents the cluster configuration file.
    /// </summary>
    [DataContract]
    public class ClusterConfiguration
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
        /// Gets or sets the cluster Id.
        /// </summary>
        [DataMember(Name = "clusterId")]
        public int ClusterId { get; set; }

        /// <summary>
        /// Gets or sets the cluster name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ISC configuration.
        /// </summary>
        [DataMember(Name = "isc")]
        public ISCConfiguration ISC { get; set; }

        /// <summary>
        /// Creates a new ClusterConfiguration instance.
        /// </summary>
        public ClusterConfiguration()
        {
            this.Ip = Global.LocalAddress;
            this.Port = Global.ClusterDefaultPort;
            this.ClusterId = 1;
            this.Name = "Server 1";
            this.ISC = new ISCConfiguration();
        }
    }
}
