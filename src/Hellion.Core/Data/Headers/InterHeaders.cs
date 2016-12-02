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
        CanAuthticate = 0x00,
        Authentication = 0x01,
        AuthenticationResult = 0x02,
        UpdateServerList = 0x03,
        UpdateWorldServerList = 0x04,
    }
}
