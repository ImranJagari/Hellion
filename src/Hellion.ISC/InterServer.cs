using Ether.Network;
using Hellion.Core.Configuration;
using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using System;
using System.IO;
using System.Linq;

namespace Hellion.ISC
{
    public sealed class InterServer : NetServer<InterClient>
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
            Log.Info("New inter client connected from {0}.", client.Socket.RemoteEndPoint.ToString());

            if (client is InterClient)
                (client as InterClient).Server = this;
        }

        /// <summary>
        /// On client disconnected.
        /// </summary>
        /// <param name="client">Client</param>
        protected override void OnClientDisconnected(NetConnection client)
        {
            Log.Info("Inter client with id : {0} disconnected.", client.Id);
        }

        /// <summary>
        /// Dispose the server's resources.
        /// </summary>
        public override void DisposeServer()
        {
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

        /// <summary>
        /// Verify if there is already a login server connected to the ISC.
        /// </summary>
        /// <returns></returns>
        internal bool HasLoginServerConnected()
        {
            var connectedLoginServer = from x in this.Clients
                                       where (x as InterClient).ServerType == InterServerType.Login
                                       select x;

            return connectedLoginServer.Any();
        }
    }
}
