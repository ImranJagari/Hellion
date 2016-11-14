using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

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
