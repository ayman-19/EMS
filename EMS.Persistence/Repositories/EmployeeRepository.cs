using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;

namespace EMS.Persistence.Repositories
{
    public sealed class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EMSDbContext context)
            : base(context) { }
    }
}
