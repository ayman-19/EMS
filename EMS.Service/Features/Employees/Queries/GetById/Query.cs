using EMS.Application.Features.Positions.Queries.GetById;
using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Employees.Queries.GetById
{
    public sealed record GetEmployeeQuery(Guid Id) : IRequest<ResponseOf<GetEmployeeResult>>;
}
