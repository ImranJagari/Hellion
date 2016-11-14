namespace Hellion.Login
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var server = new LoginServer())
                server.Start();
        }
    }
}
