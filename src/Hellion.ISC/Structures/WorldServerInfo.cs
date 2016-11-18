namespace Hellion.ISC.Structures
{
    /// <summary>
    /// Represents the WorldServer structure.
    /// </summary>
    public sealed class WorldServerInfo : BaseServer
    {
        /// <summary>
        /// Gets the world server Id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the world server's parent cluster Id.
        /// </summary>
        public int ClusterId { get; private set; }

        /// <summary>
        /// Gets the world server capacity.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Creates a new WorldServerInfo instance.
        /// </summary>
        /// <param name="id">World Server Id</param>
        /// <param name="clusterId">Parent cluster Id</param>
        /// <param name="capacity">World server capacity</param>
        /// <param name="ip">World server ip address</param>
        /// <param name="name">World server name</param>
        public WorldServerInfo(int id, int clusterId, int capacity, string ip, string name)
            : base(ip, name)
        {
            this.Id = id;
            this.ClusterId = clusterId;
            this.Capacity = capacity;
        }
    }
}
