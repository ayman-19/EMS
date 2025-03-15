using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Features.Departments.Queries.Paginate
{
    public sealed record GetDepartmenrsHandler
        : IRequestHandler<GetDepartmentsQuery, ResponseOf<GetDepartmenrsResult>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmenrsHandler(IDepartmentRepository departmentRepository) =>
            _departmentRepository = departmentRepository;

        public async Task<ResponseOf<GetDepartmenrsResult>> Handle(
            GetDepartmentsQuery request,
            CancellationToken cancellationToken
        )
        {
            int page = request.page == 0 ? 1 : request.page;
            int pagesize = request.pagesize == 0 ? 10 : request.pagesize;

            IReadOnlyCollection<DepartmentResult>? result =
                await _departmentRepository.PaginateAsync(
                    page,
                    pagesize,
                    ws => new DepartmentResult(
                        ws.Id,
                        ws.Name,
                        ws.Description,
                        ws.Employees.Count()
                    ),
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
