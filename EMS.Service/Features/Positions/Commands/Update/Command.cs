using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Positions.Commands.Update
{
    public sealed record UpdatePositionCommand(Guid Id, string Name, string Description)
        : IRequest<ResponseOf<UpdatePositionResult>>;
}
