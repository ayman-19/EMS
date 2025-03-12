using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;

namespace EMS.Persistence.Repositories
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EMSDbContext context)
            : base(context) { }
    }
}
