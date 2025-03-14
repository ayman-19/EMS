using EMS.Domain.Entities;

namespace EMS.Application.Features.Users.Queries.Login
{
    public sealed record LoginUserResult(Guid userId, string content)
    {
        public static implicit operator LoginUserResult(User user) =>
            new(user.Id, user.Token.Content);
    }
}
