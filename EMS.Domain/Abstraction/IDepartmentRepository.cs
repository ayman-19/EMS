using EMS.Domain.Entities;

namespace EMS.Domain.Abstraction
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        ValueTask DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);
    }
}
