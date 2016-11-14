using Ether.Network;
using Hellion.Core.Configuration;
using Hellion.Core.IO;
using System;
using System.IO;

namespace Hellion.ISC
{
    public sealed class InterServer : NetServer<Client>
    {
        /// <summary>
        /// ISC Configuration file path.
        /// </summary>
        private const string IscConfigurationFile = "config/isc.json";

        /// <summary>
        /// Gets the ISC configuration.
        /// </summary>
        public ISCConfiguration IscConfiguration { get; private set; }

        /// <summary>
        /// Creates a new InterServer instance.
        /// </summary>
        public InterServer()
            : base()
        {
            Console.Title = "Hellion ISC";
        }

        /// <summary>
        /// Idle the inter-server.
        /// </summary>
        protected override void Idle()
        {
            Log.Info("ISC Server listening on port {0}", this.Configuration.Port);

            while (true)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Initialize the inter-server.
        /// </summary>
        protected override void Initialize()
        {
            this.LoadConfiguration();
        }

        /// <summary>
        /// On client connected.
        /// </summary>
        /// <param name="client">Client</param>
        protected override void OnClientConnected(NetConnection client)
        {
            Log.Info("New client connected");
        }

        /// <summary>
        /// On client disconnected.
        /// </summary>
        /// <param name="client">Client</param>
        protected override void OnClientDisconnected(NetConnection client)
        {
            Log.Info("Client disconnected");
        }

        /// <summary>
        /// Load the server configuration.
        /// </summary>
        private void LoadConfiguration()
        {
            Log.Info("Loading configuration...");

            if (File.Exists(IscConfigurationFile) == false)
                ConfigurationManager.Save(new ISCConfiguration(), IscConfigurationFile);

            this.IscConfiguration = ConfigurationManager.Load<ISCConfiguration>(IscConfigurationFile);

            this.Configuration.Ip = this.IscConfiguration.Ip;
            this.Configuration.Port = this.IscConfiguration.Port;

            Log.Done("Configuration loaded!");
        }
    }
}
