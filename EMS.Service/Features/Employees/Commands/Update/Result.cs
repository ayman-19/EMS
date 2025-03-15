using EMS.Domain.Entities;

namespace EMS.Application.Features.Employees.Commands.Update
{
    public sealed record UpdateEmployeeResult(
        Guid Id,
        string Name,
        Guid? PositionId,
        Guid? DepartmentId,
        double Salary
    )
    {
        public static implicit operator UpdateEmployeeResult(Employee employee) =>
            new(
                employee.Id,
                employee.User.Name,
                employee.PositionId,
                employee.DepartmentId,
                employee.Salary
            );
    }
}
