using Hellion.Core.Database;
using Hellion.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Cluster.Structures
{
    public class Character
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public string Name { get; set; }

        public sbyte Gender { get; set; }

        public int Level { get; set; }

        public int ClassId { get; set; }

        public int Gold { get; set; }

        public void FromDbCharacter(DbCharacter dbCharacter)
        {
            Mapper.Map(dbCharacter, this);
        }
    }
}
