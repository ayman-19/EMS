using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Users.Queries.Login
{
    public sealed record LoginUserQuery(string email, string password)
        : IRequest<ResponseOf<LoginUserResult>>;
}
