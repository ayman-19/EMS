using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using MediatR;

namespace EMS.Application.Features.Departments.Commands.Update
{
    public sealed class UpdateDepartmentHandler
        : IRequestHandler<UpdateDepartmentCommand, ResponseOf<UpdateDepartmentResult>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDepartmentHandler(
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOf<UpdateDepartmentResult>> Handle(
            UpdateDepartmentCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Department department = await _departmentRepository.GetAsync(d =>
                        d.Id == request.Id
                    );
                    department.UpdateDepartment(request.Name, request.Description);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();
                    return new()
                    {
                        Message = "Success.",
                        Success = true,
                        StatusCode = (int)HttpStatusCode.OK,
                        Result = department,
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
