using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Departments.Commands.Update
{
    public sealed class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateDepartmentValidator(IServiceProvider serviceProvider)
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
            RuleFor(s => s.Id)
                .NotEmpty()
                .WithMessage("Id Is Required.")
                .NotNull()
                .WithMessage("Id Is Required.");

            RuleFor(s => s.Description)
                .NotEmpty()
                .WithMessage("Description Is Required.")
                .NotNull()
                .WithMessage("Description Is Required.");

            RuleFor(s => s.Id)
                .MustAsync(
                    async (id, CancellationToken) =>
                        await departmentRepository.IsAnyExistAsync(n => n.Id == id)
                )
                .WithMessage("Department Not Exist");

            RuleFor(s => s)
                .MustAsync(
                    async (request, CancellationToken) =>
                        !await departmentRepository.IsAnyExistAsync(n =>
                            n.Name == request.Name && n.Id != request.Id
                        )
                )
                .WithMessage("Name Is Exist.");
        }
    }
}
