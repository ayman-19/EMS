using EMS.Domain.Entities;

namespace EMS.Domain.Abstraction
{
    public interface IUserRepository : IRepository<User>
    {
        Task DeleteUserNotConfirmByEmailAsync(
            string Email,
            CancellationToken cancellationToken = default
        );
    }
}
