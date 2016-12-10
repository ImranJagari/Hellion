using Hellion.Core.Configuration;
using Hellion.Core.Data.Resources;
using Hellion.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.World
{
    public partial class WorldServer
    {
        private Dictionary<string, int> defines = new Dictionary<string, int>();
        private string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "data");

        /// <summary>
        /// Loads the world server data like resources, maps, quests, dialogs, etc...
        /// </summary>
        private void LoadData()
        {
            var startTime = DateTime.Now;
            Log.Info("Loading world data...");
            
            this.LoadDefines();
            this.Clear();

            Log.Done("World data loaded in {0}s", (DateTime.Now - startTime).TotalSeconds);
        }

        /// <summary>
        /// Clear all unused resources.
        /// </summary>
        private void Clear()
        {
            this.defines.Clear();
        }

        /// <summary>
        /// Load the flyff defines files.
        /// </summary>
        private void LoadDefines()
        {
            string[] defines = {
                                    "define.h",
                                    "defineAttribute.h",
                                    "defineEvent.h",
                                    "defineHonor.h",
                                    "defineItem.h",
                                    "defineItemkind.h",
                                    "defineJob.h",
                                    "defineNeuz.h",
                                    "defineObj.h",
                                    "defineSkill.h",
                                    "defineSound.h",
                                    "defineText.h",
                                    "defineWorld.h"
                                };

            foreach (var defineFile in defines)
            {
                var defineFileContent = new DefineFile(Path.Combine(this.dataPath, "res", "data", defineFile));
                defineFileContent.Parse();

                foreach (var define in defineFileContent.Defines)
                {
                    if (!this.defines.ContainsKey(define.Key))
                        this.defines.Add(define.Key, define.Value);
                }
            }
        }
    }
}
