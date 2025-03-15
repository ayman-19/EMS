using EMS.Domain.Entities;

namespace EMS.Application.Features.Positions.Commands.Create
{
    public sealed record CreatePositionResult(Guid Id, string Name)
    {
        public static implicit operator CreatePositionResult(Position position) =>
            new(position.Id, position.Name);
    }
}
