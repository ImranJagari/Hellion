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
        /// Gets or sets the world's parent cluster Id.
        /// </summary>
        [DataMember(Name = "clusterId")]
        public int ClusterId { get; set; }

        /// <summary>
        /// Gets or sets the world Id.
        /// </summary>
        [DataMember(Name = "worldId")]
        public int WorldId { get; set; }

        /// <summary>
        /// Gets or sest the world name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the world server's amount of players.
        /// </summary>
        [DataMember(Name = "capacity")]
        public int Capacity { get; set; }

        /// <summary>
        /// Gets or sets the ISC configuration.
        /// </summary>
        [DataMember(Name = "isc")]
        public ISCConfiguration ISC { get; set; }

        /// <summary>
        /// Gets or sets the rates configuration.
        /// </summary>
        [DataMember(Name = "rates")]
        public RatesConfiguration Rates { get; set; }

        /// <summary>
        /// Creates a new World configuration instance.
        /// </summary>
        public WorldConfiguration()
        {
            this.Ip = Global.LocalAddress;
            this.Port = Global.WorldDefaultPort;
            this.ISC = new ISCConfiguration();
            this.Rates = new RatesConfiguration();
        }
    }

    public class RatesConfiguration
    {
        /// <summary>
        /// Gets or sets the experience rate.
        /// </summary>
        public float Exp { get; set; }

        /// <summary>
        /// Gets or sets the drop rate.
        /// </summary>
        public float Drop { get; set; }

        /// <summary>
        /// Gets or sets the gold drop rate.
        /// </summary>
        public float Gold { get; set; }

        /// <summary>
        /// Creates a new RatesConfiguration instance.
        /// </summary>
        public RatesConfiguration()
        {
            this.Exp = 1;
            this.Drop = 1;
            this.Gold = 1;
        }
    }
}
