using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;

namespace EMS.Persistence.Repositories
{
    public sealed class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(EMSDbContext context)
            : base(context) { }
    }
}
