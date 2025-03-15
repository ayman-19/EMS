using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Departments.Commands.Update
{
    public sealed record UpdateDepartmentCommand(Guid Id, string Name, string Description)
        : IRequest<ResponseOf<UpdateDepartmentResult>>;
}
