using EMS.Api.Abstraction;
using EMS.Application.Features.Departments.Commands.Create;
using EMS.Application.Features.Departments.Commands.Delete;
using EMS.Application.Features.Departments.Commands.Update;
using EMS.Application.Features.Departments.Queries.GetById;
using EMS.Application.Features.Departments.Queries.Paginate;
using MediatR;

namespace EMS.Api.Implementation.Departments
{
    public sealed class DepartmentEndpoint : IEndpoint
    {
        public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            RouteGroupBuilder group = endpoints.MapGroup("/Departments").WithTags("Departments");

            group
                .MapPost(
                    "CreateAsync/",
                    async (
                        CreateDepartmentCommand Command,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(Command, cancellationToken))
                )
                .RequireAuthorization();

            group
                .MapPut(
                    "UpdateAsync/",
                    async (
                        UpdateDepartmentCommand Command,
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
                            await sender.Send(new DeleteDepartmentCommand(id), cancellationToken)
                        )
                )
                .RequireAuthorization();

            group
                .MapPost(
                    "GetByIdAsync",
                    async (
                        GetDepartmentQuery query,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(query, cancellationToken))
                )
                .RequireAuthorization();

            group
                .MapPost(
                    "PaginateAsync",
                    async (
                        GetDepartmentsQuery query,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(query, cancellationToken))
                )
                .RequireAuthorization();
        }
    }
}
