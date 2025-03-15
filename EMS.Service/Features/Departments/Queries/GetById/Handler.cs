using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using MediatR;

namespace EMS.Application.Features.Departments.Queries.GetById
{
    public sealed class GetDepartmentHandler
        : IRequestHandler<GetDepartmentQuery, ResponseOf<GetDepartmentResult>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentHandler(IDepartmentRepository departmentRepository) =>
            _departmentRepository = departmentRepository;

        public async Task<ResponseOf<GetDepartmentResult>> Handle(
            GetDepartmentQuery request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var result = await _departmentRepository.GetAsync(
                    s => s.Id == request.Id,
                    s => new GetDepartmentResult(s.Id, s.Name, s.Description),
                    null!,
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
