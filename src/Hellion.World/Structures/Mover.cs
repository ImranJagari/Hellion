using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.World.Structures
{
    public abstract class Mover : WorldObject
    {
        public Mover(int modelId)
            : base(modelId)
        {
        }
    }
}
