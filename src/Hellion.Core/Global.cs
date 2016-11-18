using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Core
{
    public class Global
    {
        public const string LocalAddress = "127.0.0.1";
        public const string IscDefaultPassword = "4fded1464736e77865df232cbcb4cd19";
        public const int IscDefaultPort = 15000;
        public const int LoginDefaultPort = 23000;
        public const int ClusterDefaultPort = 28000;
        public const int WorldDefaultPort = 14500;

        public static int GenerateRandomNumber()
        {
            return new Random().Next(0, int.MaxValue);
        }
    }
}
