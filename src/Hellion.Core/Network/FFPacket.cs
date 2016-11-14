using Ether.Network.Packets;
using System.Collections.Generic;
using System.IO;

namespace Hellion.Core.Network
{
    public class FFPacket : NetPacketBase
    {
        /// <summary>
        /// Gets the FFPacket buffer.
        /// </summary>
        public override byte[] Buffer
        {
            get
            {
                long oldPointer = this.Position;
                this.Position = 1;
                this.Write(this.Size - 5);
                this.Position = oldPointer;

                return this.GetBuffer();
            }
        }

        /// <summary>
        /// Creates a new FFPacket in write-only mode.
        /// </summary>
        public FFPacket()
            : base()
        {
            this.Write<byte>(0x5E);
            this.Write(0);
        }

        /// <summary>
        /// Creates a new FFPacket in read-only mode.
        /// </summary>
        /// <param name="buffer"></param>
        public FFPacket(byte[] buffer)
            : base(buffer)
        {
        }

        /// <summary>
        /// Split the buffer into multiple packets.
        /// </summary>
        /// <param name="buffer">Incoming buffer</param>
        /// <returns>Packets</returns>
        public static IReadOnlyCollection<NetPacketBase> SplitPackets(byte[] buffer)
        {
            int packetSize = 0;
            var packets = new List<NetPacketBase>();

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            using (BinaryReader reader = new BinaryReader(memoryStream))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    reader.BaseStream.Position += 5;
                    packetSize = reader.ReadInt32();

                    if (packetSize <= 0)
                        break;

                    reader.BaseStream.Position += 4;
                    packetSize += 13;

                    if (packetSize > (buffer.Length - reader.BaseStream.Position + 13))
                        packetSize = buffer.Length;
                    
                    reader.BaseStream.Position -= 13;
                    packets.Add(new FFPacket(reader.ReadBytes(packetSize)));
                }
            }

            return packets;
        }
    }
}
