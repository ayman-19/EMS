using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EMS.Persistence.Repositories
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {
        private readonly EMSDbContext _context;

        public UserRepository(EMSDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task DeleteUserNotConfirmByEmailAsync(
            string Email,
            CancellationToken cancellationToken = default
        ) =>
            await _context
                .Set<User>()
                .Where(u => u.Email.StartsWith(Email) && !u.ConfirmAccount)
                .ExecuteDeleteAsync(cancellationToken);
    }
}
