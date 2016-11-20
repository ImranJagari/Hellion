using System.Runtime.Serialization;

namespace Hellion.Core.Configuration
{
    [DataContract]
    public class LoginConfiguration
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
        /// Gets or sets the client build version.
        /// </summary>
        /// <remarks>
        /// During the authentification process, if this build version doesn't match the client build version
        /// you will not be allowed to connect to the Login Server.
        /// </remarks>
        [DataMember(Name = "buildVersion")]
        public string BuildVersion { get; set; }

        /// <summary>
        /// Gets or sets the value if we check the account verification.
        /// </summary>
        [DataMember(Name = "accountVerification")]
        public bool AccountVerification { get; set; }

        /// <summary>
        /// Gets or sets the ISC configuration.
        /// </summary>
        [DataMember(Name = "isc")]
        public ISCConfiguration ISC { get; set; }

        /// <summary>
        /// Creates a new LoginConfiguration instance.
        /// </summary>
        public LoginConfiguration()
        {
            this.Ip = Global.LocalAddress;
            this.Port = Global.LoginDefaultPort;

            this.ISC = new ISCConfiguration();
        }
    }
}
