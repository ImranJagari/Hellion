namespace Hellion.ISC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var server = new InterServer())
                server.Start();
        }
    }
}
