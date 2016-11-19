namespace Hellion.Core.ISC.Structures
{
    /// <summary>
    /// Represents a base server informations.
    /// </summary>
    public class BaseServer
    {
        /// <summary>
        /// Gets the server Ip host.
        /// </summary>
        public string Ip { get; private set; }
        
        /// <summary>
        /// Gets the server name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Creates a new Base server instance.
        /// </summary>
        /// <param name="ip">Server Ip host</param>
        /// <param name="name">Server name</param>
        public BaseServer(string ip, string name)
        {
            this.Ip = ip;
            this.Name = name;
        }
    }
}
