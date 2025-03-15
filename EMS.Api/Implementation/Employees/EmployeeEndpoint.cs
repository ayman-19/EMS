using EMS.Api.Abstraction;
using EMS.Application.Features.Employees.Commands.Delete;
using EMS.Application.Features.Employees.Commands.Update;
using EMS.Application.Features.Employees.Queries.GetById;
using EMS.Application.Features.Employees.Queries.Paginate;
using MediatR;

namespace EMS.Api.Implementation.Employees
{
    public sealed class EmployeeEndpoint : IEndpoint
    {
        public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            RouteGroupBuilder group = endpoints.MapGroup("/Employees").WithTags("Employees");
            group
                .MapPut(
                    "UpdateAsync/",
                    async (
                        UpdateEmployeeCommand Command,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(Command, cancellationToken))
                )
                .RequireAuthorization();

            group
                .MapDelete(
                    "DeleteAsync/{id}",
                    async (Guid id, ISender sender, CancellationToken cancellationToken) =>
                        Results.Ok(
                            await sender.Send(new DeleteEmployeeCommand(id), cancellationToken)
                        )
                )
                .RequireAuthorization();

            group
                .MapPost(
                    "GetByIdAsync",
                    async (
                        GetEmployeeQuery query,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(query, cancellationToken))
                )
                .RequireAuthorization();

            group
                .MapPost(
                    "PaginateAsync",
                    async (
                        GetEmployeesQuery query,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(query, cancellationToken))
                )
                .RequireAuthorization();
        }
    }
}
