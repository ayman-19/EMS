using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Departments.Commands.Delete
{
    public sealed record DeleteDepartmentCommand(Guid Id) : IRequest<Response>;
}
