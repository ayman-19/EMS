using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Positions.Queries.GetById
{
    public sealed record GetPositionQuery(Guid Id) : IRequest<ResponseOf<GetPositionResult>>;
}
