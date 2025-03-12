using Microsoft.EntityFrameworkCore.Storage;

namespace EMS.Domain.Abstraction
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default
        );
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
