using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Employees.Commands.Delete
{
    public sealed record DeleteEmployeeCommand(Guid Id) : IRequest<Response>;
}
