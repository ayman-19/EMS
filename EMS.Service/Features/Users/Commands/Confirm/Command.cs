using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Users.Commands.Confirm
{
    public sealed record ConfirmUserCommand(string email, string code) : IRequest<Response>;
}
