using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Features.Employees.Queries.GetById
{
    public sealed class GetEmployeeHandler
        : IRequestHandler<GetEmployeeQuery, ResponseOf<GetEmployeeResult>>
    {
        private readonly IEmployeeRepository _positionRepository;

        public GetEmployeeHandler(IEmployeeRepository positionRepository) =>
            _positionRepository = positionRepository;

        public async Task<ResponseOf<GetEmployeeResult>> Handle(
            GetEmployeeQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var result = await _positionRepository.GetAsync(
                    s => s.Id == request.Id,
                    s => new GetEmployeeResult(
                        s.Id,
                        s.User.Name,
                        s.PositionId,
                        s.Position.Name,
                        s.DepartmentId,
                        s.Department.Name,
                        s.Salary
                    ),
                    q => q.Include(e => e.User).Include(d => d.Department).Include(p => p.Position),
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
