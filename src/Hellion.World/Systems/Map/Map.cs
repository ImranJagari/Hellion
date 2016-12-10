using Hellion.World.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hellion.World.Systems.Map
{
    public class Map
    {
        private Thread thread;

        public string Name { get; private set; }

        public ICollection<WorldObject> Objects { get; private set; }

        public Map()
        {
            this.Objects = new HashSet<WorldObject>();

            this.thread = new Thread(this.Update);
            //this.thread.Start();
        }

        public void Load(string mapPath)
        {
            // Load .wld
            // Load .dyo
            // Load .rgn
            // Load .lnd
        }

        public void Update()
        {
            this.UpdatePlayerVisibility();
        }

        private void UpdatePlayerVisibility()
        {
            foreach (Player player in this.Objects)
            {
                if (!player.IsSpawned)
                    continue;

                // Update current player timers

                foreach (WorldObject obj in this.Objects)
                {
                    if (!obj.IsSpawned || player.GetHashCode() == obj.GetHashCode())
                        continue;

                    if (player.CanSee(obj))
                    {
                        if (!player.SpawnedObjects.Contains(obj))
                        {
                            // spawn the object because it's not spawn in the client list.
                        }
                    }
                    else
                    {
                        // despawn object because it's not in range of the player
                    }
                }
            }
        }
    }
}
