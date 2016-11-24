namespace Hellion.Core.Data.Headers
{
    public class ClusterHeaders
    {
        public enum Incoming
        {
            CharacterListRequest = 0xF6,
            DeleteCharacter = 0xF5,
            Ping = 0x14,
            AuthQuery = 0x0B,
            CreateCharacter = 0xF4,
            WorldTransfer = 0xFF05,
        }

        public enum Outgoing
        {
            GameServerIP = 0xF2,
            CharacterList = 0xF3,
            Pong = 0x14,
            LoginMessage = 0xFE,
            WorldTransfer = 0xFF05,
        }
    }
}
