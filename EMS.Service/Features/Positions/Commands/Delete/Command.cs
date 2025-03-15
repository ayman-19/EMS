using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Positions.Commands.Delete
{
    public sealed record DeletePositionCommand(Guid Id) : IRequest<Response>;
}
