namespace Hellion.Core.Data.Headers
{
    public enum InterServerType
    {
        None = 0,
        Login = 1,
        Cluster = 2,
        World = 3,
    }

    public enum InterHeaders
    {
        CanAuthtificate = 0x00,
        Authentification = 0x01,
        AuthentificationResult = 0x02,
    }
}
