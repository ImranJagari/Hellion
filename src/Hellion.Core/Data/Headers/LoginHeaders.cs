namespace Hellion.Core.Data.Headers
{
    public class LoginHeaders
    {
        public enum Incoming : uint
        {
            LoginRequest = 0xFC,
            CutConnection = 0x16,
            Ping = 0x14,
            CloseConnection = 0xFF,
        }

        public enum Outgoing : uint
        {
            Welcome = 0x00,
            ServerList = 0xFD,
            LoginMessage = 0xFE,
            Pong = 0x0B,
        }

        public enum LoginErrors : int
        {
            AccountAlreadyOn = 0x67,
            ServiceDown = 0x6D,
            AccountSuspended = 0x77,
            WrongPassword = 0x78,
            WrongID = 0x79,
            AwaitingVerification = 0x7A,
            AccountUnderMaintenance = 0x85,
            ServerError = 0x88,
            ResourceWasFalsified = 0x8A,

        }
    }
}
