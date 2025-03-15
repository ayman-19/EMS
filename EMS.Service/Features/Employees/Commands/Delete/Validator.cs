using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Employees.Commands.Delete
{
    public sealed class DeleteEmployeeValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public DeleteEmployeeValidator(IServiceProvider serviceProvider)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IEmployeeRepository>());
        }

        private void ValidateRequest(IEmployeeRepository employeeRepository)
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("Id Is Required.")
                .NotNull()
                .WithMessage("Id Is Required.")
                .MustAsync(
                    async (id, CancellationToken) =>
                        await employeeRepository.IsAnyExistAsync(service => service.Id == id)
                )
                .WithMessage("Employee Not Exist.");
        }
    }
}
