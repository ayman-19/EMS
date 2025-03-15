using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using MediatR;

namespace EMS.Application.Features.Departments.Commands.Delete
{
    public sealed class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, Response>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDepartmentHandler(
            IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(
            DeleteDepartmentCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _departmentRepository.DeleteByIdAsync(request.Id, cancellationToken);
                    await transaction.CommitAsync();
                    return new()
                    {
                        Message = "Success.",
                        Success = true,
                        StatusCode = (int)HttpStatusCode.OK,
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
