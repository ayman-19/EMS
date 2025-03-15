using EMS.Domain.Entities;

namespace EMS.Application.Features.Departments.Commands.Create
{
    public sealed record CreateDepertmentResult(Guid Id, string Name, string Description)
    {
        public static implicit operator CreateDepertmentResult(Department department) =>
            new(department.Id, department.Name, department.Description);
    }
}
