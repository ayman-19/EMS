using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Employees.Queries.GetById
{
    public sealed class GetEmployeeValidator : AbstractValidator<GetEmployeeQuery>
    {
        private readonly IServiceProvider _serviceProvider;

        public GetEmployeeValidator(IServiceProvider serviceProvider)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IEmployeeRepository>());
        }

        private void ValidateRequest(IEmployeeRepository employeeRepository)
        {
            RuleFor(service => service.Id)
                .NotEmpty()
                .WithMessage("Id Is Required.")
                .NotNull()
                .WithMessage("Id Is Required.")
                .MustAsync(
                    async (id, CancellationToken) =>
                        await employeeRepository.IsAnyExistAsync(emp => emp.Id == id)
                )
                .WithMessage("Employee Not Exist.");
        }
    }
}
