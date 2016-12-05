using Hellion.Core.Data.Headers;
using Hellion.Core.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.World
{
    public partial class WorldClient
    {
        public void SendPlayerSpawn()
        {
            var packet = new FFPacket();

            packet.StartNewMergedPacket(this.Player.Id, WorldHeaders.Outgoing.WeatherAll, 0x0000FF00);
            packet.Write(0); // Get weather by season

            packet.StartNewMergedPacket(this.Player.Id, WorldHeaders.Outgoing.ObjectSpawn);

            // Object properties
            packet.Write((byte)this.Player.Type);
            packet.Write(this.Player.ModelId);
            packet.Write((byte)this.Player.Type);
            packet.Write(this.Player.ModelId);
            packet.Write(this.Player.Size);
            packet.Write(this.Player.Position.X);
            packet.Write(this.Player.Position.Y);
            packet.Write(this.Player.Position.Z);
            packet.Write<short>(0); // angle
            packet.Write(this.Player.Id);



            this.Send(packet);
            packet.Dispose();
        }
    }
}
