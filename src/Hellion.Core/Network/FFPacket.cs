﻿using Ether.Network.Packets;
using Hellion.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hellion.Core.Network
{
    public class FFPacket : NetPacketBase
    {
        private uint header;
        private short mergedPacketCount;

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
        /// Creates a new FFPacket with a header.
        /// </summary>
        /// <param name="packetHeader"></param>
        public FFPacket(object packetHeader)
            : this()
        {
            this.WriteHeader(packetHeader);
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
        /// Write packet header.
        /// </summary>
        /// <param name="packetHeader">FFPacket header</param>
        public void WriteHeader(object packetHeader)
        {
            this.header = (uint)packetHeader;
            this.Write(this.header);
        }

        /// <summary>
        /// Write data into a packet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public override void Write<T>(T value)
        {
            if (typeof(T) == typeof(string))
            {
                this.WriteString(value as string);
                return;
            }

            base.Write<T>(value);
        }

        /// <summary>
        /// Read data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public override T Read<T>()
        {
            if (typeof(T) == typeof(string))
                return (T)(object)this.ReadString();

            return base.Read<T>();
        }

        /// <summary>
        /// Read FF String.
        /// </summary>
        /// <returns></returns>
        private string ReadString()
        {
            int size = this.Read<int>();

            if (size == 0)
                return string.Empty;

            return Encoding.GetEncoding(1252).GetString(this.memoryReader.ReadBytes(size));
        }

        /// <summary>
        /// Read a specified number of bytes.
        /// </summary>
        /// <param name="count">Number of bytes to read</param>
        /// <returns></returns>
        public byte[] ReadBytes(int count)
        {
            return this.memoryReader.ReadBytes(count);
        }

        /// <summary>
        /// Write FF string.
        /// </summary>
        /// <param name="value"></param>
        private void WriteString(string value)
        {
            this.Write(value.Length);
            if (value.Length > 0)
                this.Write(Encoding.GetEncoding(0).GetBytes(value));
        }

        /// <summary>
        /// Start a new merged packet.
        /// </summary>
        /// <param name="moverId"></param>
        /// <param name="command"></param>
        /// <param name="mainCommand"></param>
        public void StartNewMergedPacket(int moverID, object command, uint mainCommand)
        {
            uint packet = (uint)command;

            if (this.mergedPacketCount == 0)
            {
                this.Write((int)mainCommand);
                this.Write(0);
                this.Write(++this.mergedPacketCount);
            }
            else
            {
                this.memoryStream.Seek(13, SeekOrigin.Begin);
                this.Write(++this.mergedPacketCount);
                this.memoryStream.Seek(0, SeekOrigin.End);
            }

            this.Write(moverID);
            this.Write((short)packet);
        }

        /// <summary>
        /// Start a new merged packet.
        /// </summary>
        /// <param name="moverId"></param>
        /// <param name="command"></param>
        public void StartNewMergedPacket(int moverId, object command)
        {
            this.StartNewMergedPacket(moverId, command, 0xFFFFFF00);
        }

        /// <summary>
        /// Indicates if the packet of type T is unknown or just not implemented.
        /// </summary>
        /// <typeparam name="T">enum: Headers type</typeparam>
        /// <param name="header">Header number</param>
        /// <param name="digits">Digit numbers to display</param>
        public static void UnknowPacket<T>(uint header, int digits)
        {
            if (Enum.IsDefined(typeof(T), header))
                Log.Warning("Unimplemented packet {0}", Enum.GetName(typeof(T), header));
            else
                Log.Warning("Unknow packet 0x{0}", header.ToString("X" + digits));
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
