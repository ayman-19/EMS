using EMS.Application.Responses;
using EMS.Domain.Entities;
using MediatR;

namespace EMS.Application.Features.Departments.Commands.Create
{
    public sealed record CreateDepartmentCommand(string Name, string Description)
        : IRequest<ResponseOf<CreateDepertmentResult>>
    {
        public static implicit operator Department(CreateDepartmentCommand command) =>
            Department.Create(command.Name, command.Description);
    }
}
