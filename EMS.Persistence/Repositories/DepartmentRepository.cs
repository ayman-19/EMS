using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;

namespace EMS.Persistence.Repositories
{
    public sealed class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EMSDbContext context)
            : base(context) { }
    }
}
