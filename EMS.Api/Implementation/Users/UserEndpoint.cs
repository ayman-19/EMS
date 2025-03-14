using EMS.Api.Abstraction;
using EMS.Application.Features.Users.Commands.Confirm;
using EMS.Application.Features.Users.Commands.Create;
using EMS.Application.Features.Users.Queries.Login;
using MediatR;

namespace EMS.Api.Implementation.Users
{
    public sealed class UserEndpoint : IEndpoint
    {
        public void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            RouteGroupBuilder group = endpoints.MapGroup("/Users").WithTags("Users");

            group.MapPost(
                "/RegisterAsync",
                async (
                    CreateUserCommand command,
                    ISender _sender,
                    CancellationToken cancellationToken
                ) => Results.Ok(await _sender.Send(command, cancellationToken))
            );

            group.MapPost(
                "/LoginAsync",
                async (
                    LoginUserQuery query,
                    ISender _sender,
                    CancellationToken cancellationToken
                ) => Results.Ok(await _sender.Send(query, cancellationToken))
            );

            group.MapPut(
                "/ConfirmAsync",
                async (
                    ConfirmUserCommand command,
                    ISender _sender,
                    CancellationToken cancellationToken
                ) => Results.Ok(await _sender.Send(command, cancellationToken))
            );
        }
    }
}
