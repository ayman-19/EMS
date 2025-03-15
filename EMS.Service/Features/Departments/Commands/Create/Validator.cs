using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Departments.Commands.Create
{
    public sealed class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateDepartmentValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;

            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IDepartmentRepository>());
        }

        private void ValidateRequest(IDepartmentRepository departmentRepository)
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
                        !await departmentRepository.IsAnyExistAsync(n => n.Name == name)
                )
                .WithMessage("Name Is Exist.");
        }
    }
}
