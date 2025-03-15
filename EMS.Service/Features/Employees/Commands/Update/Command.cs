using EMS.Application.Features.Positions.Commands.Update;
using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Employees.Commands.Update
{
    public sealed record UpdateEmployeeCommand(
        Guid Id,
        string Name,
        Guid PositionId,
        Guid DepartmentId,
        double Salary
    ) : IRequest<ResponseOf<UpdateEmployeeResult>>;
}
