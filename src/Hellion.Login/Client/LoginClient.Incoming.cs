using Hellion.Core.Data.Headers;
using Hellion.Core.IO;
using Hellion.Core.Network;
using System.Linq;

namespace Hellion.Login.Client
{
    public partial class LoginClient
    {
        /// <summary>
        /// Login request.
        /// </summary>
        /// <param name="packet"></param>
        private void OnLoginRequest(FFPacket packet)
        {
            var buildVersion = packet.Read<string>();
            var username = packet.Read<string>();
            string password = string.Empty;

            if (this.Server.LoginConfiguration.PasswordEncryption)
                password = this.DecryptPassword(packet.ReadBytes(16 * 42));
            else
                password = packet.Read<string>();

            Log.Debug("Recieved from client: buildVersion: {0}, username: {1}, password: {2}", buildVersion, username, password);
            
            var user = LoginServer.UserRepository.Get(x => x.Username.ToLower() == username.ToLower());

            if (user == null)
            {
                Log.Info($"User '{username}' logged in with bad credentials. (Bad username)");
                this.SendLoginError(LoginHeaders.LoginErrors.WrongID);
                this.Server.RemoveClient(this);
            }
            else
            {
                if (buildVersion.ToLower() != this.Server.LoginConfiguration.BuildVersion?.ToLower())
                {
                    Log.Info($"User '{username}' logged in with bad build version.");
                    this.SendLoginError(LoginHeaders.LoginErrors.ServerError);
                    this.Server.RemoveClient(this);
                    return;
                }

                if (password.ToLower() != user.Password.ToLower())
                {
                    Log.Info($"User '{username}' logged in with bad credentials. (Bad password)");
                    this.SendLoginError(LoginHeaders.LoginErrors.WrongPassword);
                    this.Server.RemoveClient(this);
                    return;
                }

                if (user.Authority <= 0)
                {
                    Log.Info($"User '{username}' account is suspended.");
                    this.SendLoginError(LoginHeaders.LoginErrors.AccountSuspended);
                    this.Server.RemoveClient(this);
                    return;
                }

                if (user.Verification == false && this.Server.LoginConfiguration.AccountVerification == true)
                {
                    Log.Info($"User '{username}' account's has not been verified yet.");
                    this.SendLoginError(LoginHeaders.LoginErrors.AccountSuspended);
                    this.Server.RemoveClient(this);
                    return;
                }

                LoginClient connectedClient = null;
                if (this.IsAlreadyConnected(out connectedClient))
                {
                    this.SendLoginError(LoginHeaders.LoginErrors.AccountAlreadyOn);
                    this.Server.RemoveClient(this);
                    this.Server.RemoveClient(connectedClient);
                    return;
                }

                this.SendServerList();
            }
        }
    }
}
