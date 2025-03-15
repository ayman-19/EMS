namespace EMS.Application.Features.Employees.Queries.GetById
{
    public sealed record GetEmployeeResult(
        Guid Id,
        string Name,
        Guid? PositionId,
        string PositionName,
        Guid? DepartmentId,
        string DepartmentName,
        double Salary
    );
}
