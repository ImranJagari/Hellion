using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hellion.Core.Data;

namespace Hellion.World.Structures
{
    public abstract class Mover : WorldObject
    {
        public bool IsDead { get; set; }

        public bool IsFlying { get; set; }

        public bool IsSpawned { get; set; }

        public bool IsFighting { get; set; }

        public bool IsFollowing { get; set; }

        public bool IsReseting { get; set; }

        public float Speed { get; set; }
        
        public int Level { get; }

        public override WorldObjectType Type
        {
            get { return WorldObjectType.Mover; }
        }

        public Mover(int modelId)
            : base(modelId)
        {
            this.Speed = 0.1f;
            this.Level = 1;
        }
    }
}
