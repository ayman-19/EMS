using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using MediatR;

namespace EMS.Application.Features.Positions.Commands.Delete
{
    public sealed class DeletePositionHandler : IRequestHandler<DeletePositionCommand, Response>
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePositionHandler(IPositionRepository positionRepository, IUnitOfWork unitOfWork)
        {
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(
            DeletePositionCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _positionRepository.DeleteByIdAsync(request.Id, cancellationToken);
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
