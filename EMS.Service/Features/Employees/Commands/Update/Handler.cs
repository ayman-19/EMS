using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EMS.Application.Features.Employees.Commands.Update
{
    public sealed class UpdateEmployeeHandler
        : IRequestHandler<UpdateEmployeeCommand, ResponseOf<UpdateEmployeeResult>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOf<UpdateEmployeeResult>> Handle(
            UpdateEmployeeCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Employee employee = await _employeeRepository.GetAsync(
                        d => d.Id == request.Id,
                        includes: u => u.Include(u => u.User)
                    );
                    employee.UpdateEmployee(
                        request.Name,
                        request.Salary,
                        request.DepartmentId,
                        request.PositionId
                    );
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();
                    return new()
                    {
                        Message = "Success.",
                        Success = true,
                        StatusCode = (int)HttpStatusCode.OK,
                        Result = employee,
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}
