using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using MediatR;

namespace EMS.Application.Features.Departments.Commands.Create
{
    public sealed record CreateDepartmentHandler
        : IRequestHandler<CreateDepartmentCommand, ResponseOf<CreateDepertmentResult>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDepartmentHandler(
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOf<CreateDepertmentResult>> Handle(
            CreateDepartmentCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Department department = request;
                    await _departmentRepository.CreateAsync(department, cancellationToken);
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
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
