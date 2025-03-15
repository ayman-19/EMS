using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence.Repositories
{
    public sealed class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly EMSDbContext _context;

        public EmployeeRepository(EMSDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async ValueTask DeleteByIdAsync(Guid Id, CancellationToken cancellationToken) =>
            await _context.Set<User>().Where(s => s.Id == Id).ExecuteDeleteAsync(cancellationToken);
    }
}
