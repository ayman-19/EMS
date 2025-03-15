using EMS.Domain.Entities;

namespace EMS.Application.Features.Departments.Queries.GetById
{
    public sealed record GetDepartmentResult(Guid Id, string Name, string Description);
}
