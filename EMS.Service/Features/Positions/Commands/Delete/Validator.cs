using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Positions.Commands.Delete
{
    public sealed class DeletePositionValidator : AbstractValidator<DeletePositionCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public DeletePositionValidator(IServiceProvider serviceProvider)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IPositionRepository>());
        }

        private void ValidateRequest(IPositionRepository positionRepository)
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("Id Is Required.")
                .NotNull()
                .WithMessage("Id Is Required.")
                .MustAsync(
                    async (id, CancellationToken) =>
                        await positionRepository.IsAnyExistAsync(service => service.Id == id)
                )
                .WithMessage("Position Not Exist.");
        }
    }
}
