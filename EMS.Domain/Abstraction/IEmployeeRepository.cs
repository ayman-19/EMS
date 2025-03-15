using EMS.Domain.Entities;

namespace EMS.Domain.Abstraction
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        ValueTask DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);
    }
}
