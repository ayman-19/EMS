using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Features.Positions.Queries.Paginate
{
    public sealed record GetPositionsHandler
        : IRequestHandler<GetPositionsQuery, ResponseOf<GetPositionsResult>>
    {
        private readonly IPositionRepository _positionRepository;

        public GetPositionsHandler(IPositionRepository positionRepository) =>
            _positionRepository = positionRepository;

        public async Task<ResponseOf<GetPositionsResult>> Handle(
            GetPositionsQuery request,
            CancellationToken cancellationToken
        )
        {
            int page = request.page == 0 ? 1 : request.page;
            int pagesize = request.pagesize == 0 ? 10 : request.pagesize;

            IReadOnlyCollection<PositionsResult>? result = await _positionRepository.PaginateAsync(
                page,
                pagesize,
                ws => new PositionsResult(ws.Id, ws.Name, ws.Description, ws.Employees.Count()),
                d => request.Id == null || d.Id == request.Id,
                q => q.Include(d => d.Employees),
                cancellationToken
            );

            return new()
            {
                Message = "Success.",
                Success = true,
                StatusCode = (int)HttpStatusCode.OK,
                Result = new(
                    page,
                    pagesize,
                    (int)Math.Ceiling(result.Count / (double)pagesize),
                    result
                ),
            };
        }
    }
}
