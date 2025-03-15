using EMS.Application.Responses;
using EMS.Domain.Entities;
using MediatR;

namespace EMS.Application.Features.Positions.Commands.Create
{
    public sealed record CreatePositionCommand(string Name, string Description)
        : IRequest<ResponseOf<CreatePositionResult>>
    {
        public static implicit operator Position(CreatePositionCommand command) =>
            Position.Create(command.Name, command.Description);
    }
}
