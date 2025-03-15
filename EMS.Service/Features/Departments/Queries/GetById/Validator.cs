using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Departments.Queries.GetById
{
    public sealed class GetDepartmentValidator : AbstractValidator<GetDepartmentQuery>
    {
        private readonly IServiceProvider _serviceProvider;

        public GetDepartmentValidator(IServiceProvider serviceProvider)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IDepartmentRepository>());
        }

        private void ValidateRequest(IDepartmentRepository departmentRepository)
        {
            RuleFor(service => service.Id)
                .NotEmpty()
                .WithMessage("Id Is Required.")
                .NotNull()
                .WithMessage("Id Is Required.")
                .MustAsync(
                    async (id, CancellationToken) =>
                        await departmentRepository.IsAnyExistAsync(dept => dept.Id == id)
                )
                .WithMessage("Department Not Exist.");
        }
    }
}
