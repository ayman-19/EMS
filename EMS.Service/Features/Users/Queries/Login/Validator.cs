using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Users.Queries.Login
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUserQuery>
    {
        private readonly IServiceProvider _serviceProvider;

        public LoginUserValidator(IServiceProvider serviceProvider)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;

            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IUserRepository>());
        }

        private void ValidateRequest(IUserRepository userRepository)
        {
            RuleFor(x => x.email)
                .NotEmpty()
                .WithMessage("Email Is Required.")
                .NotNull()
                .WithMessage("Email Is Required.");

            RuleFor(x => x.password)
                .NotEmpty()
                .WithMessage("Password Is Required.")
                .NotNull()
                .WithMessage("Password Is Required.");

            RuleFor(x => x.email)
                .MustAsync(
                    async (email, CancellationToken) =>
                        await userRepository.IsAnyExistAsync(u => u.Email == email)
                )
                .WithMessage("Email Not Exist.");
        }
    }
}
