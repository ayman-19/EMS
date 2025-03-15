using EMS.Api.Abstraction;
using EMS.Application.Features.Positions.Commands.Create;
using EMS.Application.Features.Positions.Commands.Delete;
using EMS.Application.Features.Positions.Commands.Update;
using EMS.Application.Features.Positions.Queries.GetById;
using EMS.Application.Features.Positions.Queries.Paginate;
using MediatR;

namespace EMS.Api.Implementation.Positions
{
    public sealed class PositionEndpoint : IEndpoint
    {
        public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            RouteGroupBuilder group = endpoints.MapGroup("/Positions").WithTags("Positions");

            group
                .MapPost(
                    "CreateAsync/",
                    async (
                        CreatePositionCommand Command,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(Command, cancellationToken))
                )
                .RequireAuthorization();

            group
                .MapPut(
                    "UpdateAsync/",
                    async (
                        UpdatePositionCommand Command,
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
                            await sender.Send(new DeletePositionCommand(id), cancellationToken)
                        )
                )
                .RequireAuthorization();

            group
                .MapPost(
                    "GetByIdAsync",
                    async (
                        GetPositionQuery query,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(query, cancellationToken))
                )
                .RequireAuthorization();

            group
                .MapPost(
                    "PaginateAsync",
                    async (
                        GetPositionsQuery query,
                        ISender sender,
                        CancellationToken cancellationToken
                    ) => Results.Ok(await sender.Send(query, cancellationToken))
                )
                .RequireAuthorization();
        }
    }
}
