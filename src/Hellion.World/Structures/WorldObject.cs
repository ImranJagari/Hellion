using Hellion.Core.Data;
using Hellion.Core.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHelper = Ether.Network.Helpers;

namespace Hellion.World.Structures
{
    public abstract class WorldObject
    {
        public int ObjectId { get; set; }

        public int ModelId { get; set; }

        public short Size { get; set; }

        public int MapId { get; set; }

        public Vector3 Position { get; set; }

        public ICollection<WorldObject> SpawnedObjects { get; set; }

        public virtual WorldObjectType Type
        {
            get { return WorldObjectType.Object; }
        }

        public WorldObject(int modelId)
        {
            this.ObjectId = EHelper.Helper.GenerateUniqueId();
            this.ModelId = modelId;
            this.Size = 100;
            this.MapId = -1;
            this.Position = new Vector3();
            this.SpawnedObjects = new List<WorldObject>();
        }
    }
}
