using Microsoft.EntityFrameworkCore;

namespace Hellion.Core.Database.Repository
{
    public class UserRepository : RepositoryBase<DbUser>
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
