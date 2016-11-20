namespace Hellion.Cluster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var server = new ClusterServer())
                server.Start();
        }
    }
}
