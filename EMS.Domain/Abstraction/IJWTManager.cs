using EMS.Domain.Entities;

namespace EMS.Domain.Abstraction
{
    public interface IJWTManager
    {
        Task<Token> GenerateTokenAsync(User user, CancellationToken cancellationToken = default);
        Task<User> LoginAsync(User user, CancellationToken cancellationToken = default);
    }
}
