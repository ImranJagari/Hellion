using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hellion.Core.Database.Repository
{
    public class ItemRepository : RepositoryBase<DbItem>
    {
        public ItemRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
