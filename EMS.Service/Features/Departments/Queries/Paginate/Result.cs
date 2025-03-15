namespace EMS.Application.Features.Departments.Queries.Paginate
{
    public sealed record DepartmentResult(
        Guid Id,
        string Name,
        string Description,
        int NumberOfEmployee
    );

    public sealed record GetDepartmenrsResult(
        int Page,
        int PageSize,
        int TotalPage,
        IReadOnlyCollection<DepartmentResult> Departments
    );
}
