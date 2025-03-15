using EMS.Domain.Entities;

namespace EMS.Application.Features.Positions.Commands.Update
{
    public sealed record UpdatePositionResult(Guid Id, string Name, string Description)
    {
        public static implicit operator UpdatePositionResult(Position position) =>
            new(position.Id, position.Name, position.Description);
    }
}
