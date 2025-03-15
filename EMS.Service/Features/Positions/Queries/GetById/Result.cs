namespace EMS.Application.Features.Positions.Queries.GetById
{
    public sealed record GetPositionResult(
        Guid Id,
        string Name,
        string Description,
        int NumberOfEmployee
    );
}
