using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Departments.Queries.Paginate
{
    public sealed record GetDepartmentsQuery(Guid? Id, int page, int pagesize)
        : IRequest<ResponseOf<GetDepartmenrsResult>>;
}
