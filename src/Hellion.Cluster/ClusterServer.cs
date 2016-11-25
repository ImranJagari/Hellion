using Ether.Network;
using Ether.Network.Packets;
using Hellion.Cluster.Client;
using Hellion.Cluster.ISC;
using Hellion.Core.Configuration;
using Hellion.Core.Database;
using Hellion.Core.IO;
using Hellion.Core.Network;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hellion.Cluster
{
    public class ClusterServer : NetServer<ClusterClient>
    {
        private const string ClusterConfigurationFile = "config/cluster.json";
        private const string DatabaseConfigurationFile = "config/database.json";

        /// <summary>
        /// Gets the Database context.
        /// </summary>
        public static DatabaseContext DbContext
        {
            get
            {
                lock (syncDatabase)
                {
                    return dbContext;
                }
            }
        }
        private static object syncDatabase = new object();
        private static DatabaseContext dbContext = null;

        private InterConnector connector;
        private Thread iscThread;

        /// <summary>
        /// Gets the cluster server configuration.
        /// </summary>
        public ClusterConfiguration ClusterConfiguration { get; private set; }

        /// <summary>
        /// Gets the database configuration.
        /// </summary>
        public DatabaseConfiguration DatabaseConfiguration { get; private set; }

        /// <summary>
        /// Creates a new ClusterServer instance.
        /// </summary>
        public ClusterServer()
            : base()
        {
            Console.Title = "Hellion ClusterServer";
            Log.Info("Starting ClusterServer...");
        }

        /// <summary>
        /// Dispose the server's resources.
        /// </summary>
        public override void DisposeServer()
        {
        }

        /// <summary>
        /// ClusterServer idle.
        /// </summary>
        protected override void Idle()
        {
            Log.Info("Server listening on port {0}", this.ClusterConfiguration.Port);

            while (this.IsRunning)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Initialize the ClusterServer.
        /// </summary>
        protected override void Initialize()
        {
            this.LoadConfiguration();
            this.ConnectToDatabase();
            this.ConnectToISC();

            Console.WriteLine();
        }

        /// <summary>
        /// On client connected.
        /// </summary>
        /// <param name="client">Client</param>
        protected override void OnClientConnected(NetConnection client)
        {
            Log.Info("New client connected from {0}", client.Socket.RemoteEndPoint.ToString());

            if (client is ClusterClient)
                (client as ClusterClient).Server = this;
        }

        /// <summary>
        /// On client disconnected.
        /// </summary>
        /// <param name="client">Client</param>
        protected override void OnClientDisconnected(NetConnection client)
        {
            Log.Info("Client with id {0} disconnected.", client.Id);
        }

        /// <summary>
        /// Split incoming buffer into several FFPacket.
        /// </summary>
        /// <param name="buffer">Incoming buffer</param>
        /// <returns></returns>
        protected override IReadOnlyCollection<NetPacketBase> SplitPackets(byte[] buffer)
        {
            return FFPacket.SplitPackets(buffer);
        }

        /// <summary>
        /// Load the ClusterServer configuration.
        /// </summary>
        private void LoadConfiguration()
        {
            Log.Info("Loading configuration...");

            if (File.Exists(ClusterConfigurationFile) == false)
                ConfigurationManager.Save(new LoginConfiguration(), ClusterConfigurationFile);

            this.ClusterConfiguration = ConfigurationManager.Load<ClusterConfiguration>(ClusterConfigurationFile);

            this.Configuration.Ip = this.ClusterConfiguration.Ip;
            this.Configuration.Port = this.ClusterConfiguration.Port;

            if (File.Exists(DatabaseConfigurationFile) == false)
                ConfigurationManager.Save(new DatabaseConfiguration(), DatabaseConfigurationFile);

            this.DatabaseConfiguration = ConfigurationManager.Load<DatabaseConfiguration>(DatabaseConfigurationFile);

            Log.Done("Configuration loaded!");
        }


        /// <summary>
        /// Connect to the database.
        /// </summary>
        private void ConnectToDatabase()
        {
            try
            {
                Log.Info("Connecting to database...");
                dbContext = new DatabaseContext(this.DatabaseConfiguration);
                dbContext.Database.EnsureCreated();

                Log.Done("Connected to database!");
            }
            catch (Exception e)
            {
                Log.Error($"Cannot connect to database. {e.Message}");
            }
        }

        /// <summary>
        /// Connect to the Inter-Server.
        /// </summary>
        private void ConnectToISC()
        {
            Log.Info("Connecting to Inter-Server...");

            this.connector = new InterConnector(this);

            try
            {
                this.connector.Connect(this.ClusterConfiguration.ISC.Ip, this.ClusterConfiguration.ISC.Port);
                this.iscThread = new Thread(this.connector.Run);
                this.iscThread.Start();
            }
            catch (Exception e)
            {
                Log.Error("Cannot connect to ISC. {0}", e.Message);
                Environment.Exit(0);
            }

            Log.Done("Connected to Inter-Server!");
        }
    }
}
