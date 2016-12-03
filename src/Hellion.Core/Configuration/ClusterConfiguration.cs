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
        /// Gets or sets the value that indicates if the login protect system is actived or not.
        /// </summary>
        [DataMember(Name = "enableLoginProtect")]
        public bool EnableLoginProtect { get; set; }

        /// <summary>
        /// Gets or sets the ISC configuration.
        /// </summary>
        [DataMember(Name = "isc")]
        public ISCConfiguration ISC { get; set; }

        /// <summary>
        /// Gets or sets the default character configuration.
        /// </summary>
        [DataMember(Name = "defaultCharacter")]
        public DefaultCharacter DefaultCharacter { get; set; }

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
            this.DefaultCharacter = new DefaultCharacter();
        }
    }

    public class DefaultCharacter
    {
        [DataMember(Name = "level")]
        public int Level { get; set; }

        [DataMember(Name = "gold")]
        public int Gold { get; set; }

        [DataMember(Name = "strength")]
        public int Strength { get; set; }

        [DataMember(Name = "stamina")]
        public int Stamina { get; set; }

        [DataMember(Name = "dexterity")]
        public int Dexterity { get; set; }

        [DataMember(Name = "intelligence")]
        public int Intelligence { get; set; }

        [DataMember(Name = "mapId")]
        public int MapId { get; set; }

        [DataMember(Name = "posX")]
        public float PosX { get; set; }

        [DataMember(Name = "posY")]
        public float PosY { get; set; }

        [DataMember(Name = "posZ")]
        public float PosZ { get; set; }

        [DataMember(Name = "man")]
        public DefaultStartItem Man { get; set; }

        [DataMember(Name = "woman")]
        public DefaultStartItem Woman { get; set; }

        public DefaultCharacter()
        {
            this.Level = 1;
            this.Gold = 0;
            this.Strength = 15;
            this.Stamina = 15;
            this.Dexterity = 15;
            this.Intelligence = 15;
            this.MapId = 1;
            this.PosX = 6968;
            this.PosY = 100;
            this.PosZ = 3328;
            this.Man = new DefaultStartItem();
            this.Woman = new DefaultStartItem();
        }
    }

    public class DefaultStartItem
    {
        [DataMember(Name = "startWeapon")]
        public int StartWeapon { get; set; }

        [DataMember(Name = "startSuit")]
        public int StartSuit { get; set; }

        [DataMember(Name = "startHand")]
        public int StartHand { get; set; }

        [DataMember(Name = "startShoes")]
        public int StartShoes { get; set; }

        [DataMember(Name = "startHat")]
        public int StartHat { get; set; }

        public DefaultStartItem()
        {
            this.StartWeapon = -1;
            this.StartSuit = -1;
            this.StartHand = -1;
            this.StartShoes = -1;
            this.StartHat = -1;
        }
    }
}
