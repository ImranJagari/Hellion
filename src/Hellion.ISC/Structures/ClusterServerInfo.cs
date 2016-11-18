namespace Hellion.ISC.Structures
{
    /// <summary>
    /// Reprensents the structure of a Cluster Server.
    /// </summary>
    public sealed class ClusterServerInfo : BaseServer
    {
        /// <summary>
        /// Gets the cluster server ID.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Creates a new ClusterServerInfo instance.
        /// </summary>
        /// <param name="id">Cluster Id</param>
        /// <param name="ip">Cluster Ip address</param>
        /// <param name="name">Cluster name</param>
        public ClusterServerInfo(int id, string ip, string name)
            : base(ip, name)
        {
            this.Id = id;
        }
    }
}
