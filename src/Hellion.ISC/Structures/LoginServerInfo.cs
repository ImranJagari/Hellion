namespace Hellion.ISC.Structures
{
    /// <summary>
    /// Represents the structure of a LoginServer.
    /// </summary>
    public sealed class LoginServerInfo : BaseServer
    {
        /// <summary>
        /// Creates a new LoginServerInfo instance.
        /// </summary>
        public LoginServerInfo()
            : base("0.0.0.0", "LoginServer")
        {
        }
    }
}
