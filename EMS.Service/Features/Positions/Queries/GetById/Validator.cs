using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Positions.Queries.GetById
{
    public sealed class GetPositionValidator : AbstractValidator<GetPositionQuery>
    {
        private readonly IServiceProvider _serviceProvider;

        public GetPositionValidator(IServiceProvider serviceProvider)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IPositionRepository>());
        }

        private void ValidateRequest(IPositionRepository positionRepository)
        {
            RuleFor(service => service.Id)
                .NotEmpty()
                .WithMessage("Id Is Required.")
                .NotNull()
                .WithMessage("Id Is Required.")
                .MustAsync(
                    async (id, CancellationToken) =>
                        await positionRepository.IsAnyExistAsync(dept => dept.Id == id)
                )
                .WithMessage("Position Not Exist.");
        }
    }
}
