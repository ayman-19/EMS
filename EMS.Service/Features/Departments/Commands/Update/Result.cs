using EMS.Domain.Entities;

namespace EMS.Application.Features.Departments.Commands.Update
{
    public sealed record UpdateDepartmentResult(Guid Id, string Name, string Description)
    {
        public static implicit operator UpdateDepartmentResult(Department department) =>
            new(department.Id, department.Name, department.Description);
    }
}
