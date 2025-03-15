using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using MediatR;

namespace EMS.Application.Features.Positions.Commands.Update
{
    public sealed class UpdatePositionHandler
        : IRequestHandler<UpdatePositionCommand, ResponseOf<UpdatePositionResult>>
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePositionHandler(IPositionRepository positionRepository, IUnitOfWork unitOfWork)
        {
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOf<UpdatePositionResult>> Handle(
            UpdatePositionCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Position position = await _positionRepository.GetAsync(d => d.Id == request.Id);
                    position.UpdatePosition(request.Name, request.Description);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();
                    return new()
                    {
                        Message = "Success.",
                        Success = true,
                        StatusCode = (int)HttpStatusCode.OK,
                        Result = position,
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
