using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Features.Employees.Queries.Paginate
{
    public sealed record GetEmployeesHandler
        : IRequestHandler<GetEmployeesQuery, ResponseOf<GetEmployeesResult>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesHandler(IEmployeeRepository employeeRepository) =>
            _employeeRepository = employeeRepository;

        public async Task<ResponseOf<GetEmployeesResult>> Handle(
            GetEmployeesQuery request,
            CancellationToken cancellationToken
        )
        {
            int page = request.page == 0 ? 1 : request.page;
            int pagesize = request.pagesize == 0 ? 10 : request.pagesize;

            IReadOnlyCollection<EmployeesResult>? result = await _employeeRepository.PaginateAsync(
                page,
                pagesize,
                s => new EmployeesResult(
                    s.Id,
                    s.User.Name,
                    s.PositionId,
                    s.Position.Name,
                    s.DepartmentId,
                    s.Department.Name,
                    s.Salary
                ),
                d =>
                    (request.Id == null || d.Id == request.Id)
                    && (d.DepartmentId == request.DepartmendId || request.DepartmendId == null),
                q => q.Include(e => e.User).Include(d => d.Department).Include(p => p.Position),
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
