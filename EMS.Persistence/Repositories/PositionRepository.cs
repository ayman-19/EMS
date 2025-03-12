using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;

namespace EMS.Persistence.Repositories
{
    public sealed class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(EMSDbContext context)
            : base(context) { }
    }
}
