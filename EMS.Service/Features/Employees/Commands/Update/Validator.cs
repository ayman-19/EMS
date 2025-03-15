using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Employees.Commands.Update
{
    public sealed class UpdateServiceValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateServiceValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;

            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(
                scope.ServiceProvider.GetRequiredService<IEmployeeRepository>(),
                scope.ServiceProvider.GetRequiredService<IDepartmentRepository>(),
                scope.ServiceProvider.GetRequiredService<IPositionRepository>()
            );
        }

        private void ValidateRequest(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IPositionRepository positionRepository
        )
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

            RuleFor(s => s.DepartmentId)
                .NotEmpty()
                .WithMessage("Department Is Required.")
                .NotNull()
                .WithMessage("NamDepartmente Is Required.");
            RuleFor(s => s.PositionId)
                .NotEmpty()
                .WithMessage("Position Is Required.")
                .NotNull()
                .WithMessage("Position Is Required.");
            RuleFor(s => s.Salary)
                .NotEmpty()
                .WithMessage("Salary Is Required.")
                .NotNull()
                .WithMessage("Salary Is Required.")
                .GreaterThan(0)
                .WithMessage("Salary must be Greater Than 0.");

            RuleFor(s => s.Id)
                .MustAsync(
                    async (id, CancellationToken) =>
                        await employeeRepository.IsAnyExistAsync(n => n.Id == id)
                )
                .WithMessage("Employee Not Exist.");

            RuleFor(s => s.DepartmentId)
                .MustAsync(
                    async (id, CancellationToken) =>
                        await departmentRepository.IsAnyExistAsync(n => n.Id == id)
                )
                .WithMessage("Department Not Exist.");

            RuleFor(s => s.PositionId)
                .MustAsync(
                    async (id, CancellationToken) =>
                        await positionRepository.IsAnyExistAsync(n => n.Id == id)
                )
                .WithMessage("Position Not Exist.");
        }
    }
}
