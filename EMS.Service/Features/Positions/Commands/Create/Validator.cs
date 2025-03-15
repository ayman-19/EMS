using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Positions.Commands.Create
{
    public sealed class CreatePositionValidator : AbstractValidator<CreatePositionCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public CreatePositionValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;

            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IPositionRepository>());
        }

        private void ValidateRequest(IPositionRepository positionRepository)
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .WithMessage("Name Is Required.")
                .NotNull()
                .WithMessage("Name Is Required.");
            RuleFor(s => s.Description)
                .NotEmpty()
                .WithMessage("Description Is Required.")
                .NotNull()
                .WithMessage("Description Is Required.");

            RuleFor(s => s.Name)
                .MustAsync(
                    async (name, CancellationToken) =>
                        !await positionRepository.IsAnyExistAsync(n => n.Name == name)
                )
                .WithMessage("Name Is Exist.");
        }
    }
}
