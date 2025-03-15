namespace EMS.Application.Features.Employees.Queries.Paginate
{
    public sealed record EmployeesResult(
        Guid Id,
        string Name,
        Guid? PositionId,
        string PositionName,
        Guid? DepartmentId,
        string DepartmentName,
        double Salary
    );

    public sealed record GetEmployeesResult(
        int Page,
        int PageSize,
        int TotalPage,
        IReadOnlyCollection<EmployeesResult> Departments
    );
}
