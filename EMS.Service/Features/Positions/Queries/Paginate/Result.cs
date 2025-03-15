namespace EMS.Application.Features.Positions.Queries.Paginate
{
    public sealed record PositionsResult(
        Guid Id,
        string Name,
        string Description,
        int NumberOfEmployee
    );

    public sealed record GetPositionsResult(
        int Page,
        int PageSize,
        int TotalPage,
        IReadOnlyCollection<PositionsResult> Departments
    );
}
