using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Hellion.Core.Database.Repository
{
    public class CharacterRepository : RepositoryBase<DbCharacter>
    {
        public CharacterRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        
    }
}
