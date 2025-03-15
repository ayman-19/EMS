using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using MediatR;

namespace EMS.Application.Features.Positions.Commands.Create
{
    public sealed record CreatePositionHandler
        : IRequestHandler<CreatePositionCommand, ResponseOf<CreatePositionResult>>
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePositionHandler(IPositionRepository positionRepository, IUnitOfWork unitOfWork)
        {
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseOf<CreatePositionResult>> Handle(
            CreatePositionCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    Position position = request;
                    await _positionRepository.CreateAsync(position, cancellationToken);
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
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
