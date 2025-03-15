using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence.Repositories
{
    public sealed class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly EMSDbContext _context;

        public DepartmentRepository(EMSDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async ValueTask DeleteByIdAsync(Guid Id, CancellationToken cancellationToken) =>
            await _context
                .Set<Department>()
                .Where(s => s.Id == Id)
                .ExecuteDeleteAsync(cancellationToken);
    }
}
