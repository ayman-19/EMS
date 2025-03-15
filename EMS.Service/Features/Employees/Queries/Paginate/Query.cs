using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Employees.Queries.Paginate
{
    public sealed record GetEmployeesQuery(Guid? Id, Guid? DepartmendId, int page, int pagesize)
        : IRequest<ResponseOf<GetEmployeesResult>>;
}
