using Hellion.Core.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hellion.Core.Data
{
    public sealed class RgnFile
    {
        private byte[] fileData;


        public RgnFile(byte[] rgnData)
        {
            this.fileData = rgnData;
        }


        public void Read()
        {
            using (var memoryStream = new MemoryStream(this.fileData))
            using (var memoryReader = new BinaryReader(memoryStream))
            {
            }
        }
    }
}
