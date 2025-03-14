using EMS.Domain.Abstraction;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Application.Features.Users.Commands.Create
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateUserValidator(IServiceProvider serviceProvider)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            ClassLevelCascadeMode = CascadeMode.Stop;
            _serviceProvider = serviceProvider;
            var scope = _serviceProvider.CreateScope();
            ValidateRequest(scope.ServiceProvider.GetRequiredService<IUserRepository>());
        }

        private void ValidateRequest(IUserRepository userRepository)
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .WithMessage("Name Is Required.")
                .NotNull()
                .WithMessage("Name Is Required.");

            RuleFor(x => x.email)
                .EmailAddress()
                .WithMessage("Email Not Valid.")
                .NotEmpty()
                .WithMessage("Email Is Required.")
                .NotNull()
                .WithMessage("Email Is Required.");

            RuleFor(x => x.password)
                .NotEmpty()
                .WithMessage("Password Is Required")
                .NotNull()
                .WithMessage("Password Is Required")
                .MinimumLength(8)
                .WithMessage("Min Length 8 char.")
                .MaximumLength(20)
                .WithMessage("Max Length 20 char.")
                .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d")
                .WithMessage("Password must contain at least one digit.")
                .Matches(@"[\W_]")
                .WithMessage(
                    "Password must contain at least one special character (!@#$%^&* etc.)."
                );

            RuleFor(x => x.confirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password Is Required.")
                .NotNull()
                .WithMessage("Confirm Password Is Required.");

            RuleFor(x => x.confirmPassword)
                .Equal(x => x.password)
                .WithMessage("Confirm Password Not Equal Password.");

            RuleFor(x => x)
                .MustAsync(
                    async (e, cancellationToken) =>
                        !await userRepository.IsAnyExistAsync(
                            user => user.Email.Trim().Equals(e.email),
                            cancellationToken
                        )
                )
                .WithMessage("Email Is Exist.");
        }
    }
}
