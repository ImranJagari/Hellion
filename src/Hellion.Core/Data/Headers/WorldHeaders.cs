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
            Join = 0x0000FF00,
        }

        public enum Outgoing : uint
        {
            WeatherClear = 0x00000060,
            WeatherSnow = 0x00000061,
            WeatherRain = 0x00000062,
            WeatherAll = 0x00000063,

            ObjectSpawn = 0x000000F0,
            ObjectDespawn = 0x000000F1,
            MapTransfer = 0x000000F2,
        }
    }
}
