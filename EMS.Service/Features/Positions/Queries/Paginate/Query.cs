using EMS.Application.Responses;
using MediatR;

namespace EMS.Application.Features.Positions.Queries.Paginate
{
    public sealed record GetPositionsQuery(Guid? Id, int page, int pagesize)
        : IRequest<ResponseOf<GetPositionsResult>>;
}
