using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Features.Positions.Queries.GetById
{
    public sealed class GetPositionHandler
        : IRequestHandler<GetPositionQuery, ResponseOf<GetPositionResult>>
    {
        private readonly IPositionRepository _positionRepository;

        public GetPositionHandler(IPositionRepository positionRepository) =>
            _positionRepository = positionRepository;

        public async Task<ResponseOf<GetPositionResult>> Handle(
            GetPositionQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var result = await _positionRepository.GetAsync(
                    s => s.Id == request.Id,
                    s => new GetPositionResult(s.Id, s.Name, s.Description, s.Employees.Count),
                    p => p.Include(e => e.Employees),
                    false,
                    cancellationToken
                );
                return new()
                {
                    Message = "Success.",
                    Success = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
