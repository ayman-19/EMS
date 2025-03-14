using EMS.Application.Responses;
using EMS.Domain.Entities;
using MediatR;

namespace EMS.Application.Features.Users.Commands.Create
{
    public sealed record CreateUserCommand(
        string name,
        string email,
        string password,
        string confirmPassword
    ) : IRequest<ResponseOf<CreateUserResult>>
    {
        public static implicit operator User(CreateUserCommand command) =>
            User.Create(command.name, command.email);
    }
}
