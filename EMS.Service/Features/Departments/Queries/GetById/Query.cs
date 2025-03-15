using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Departments.Queries.GetById
{
    public sealed record GetDepartmentQuery(Guid Id) : IRequest<ResponseOf<GetDepartmentResult>>;
}
