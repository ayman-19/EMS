using EMS.Domain.Entities;

namespace EMS.Domain.Abstraction
{
    public interface IPositionRepository : IRepository<Position>
    {
        ValueTask DeleteByIdAsync(Guid Id, CancellationToken cancellationToken);
    }
}
