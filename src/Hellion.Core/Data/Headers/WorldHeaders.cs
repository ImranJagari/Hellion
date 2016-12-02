using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Core.Data.Headers
{
    public class WorldHeaders
    {
        public enum Incoming : uint
        {
            JoinWorldRequest = 0x0000FF00,
        }

        public enum Outgoing : uint
        {
        }
    }
}
