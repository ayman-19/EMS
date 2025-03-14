using EMS.Domain.Entities;

namespace EMS.Application.Features.Users.Commands.Create
{
    public sealed record CreateUserResult(Guid Id, string Name, string Email)
    {
        public static implicit operator CreateUserResult(User user) =>
            new(user.Id, user.Name, user.Email);
    }
}
