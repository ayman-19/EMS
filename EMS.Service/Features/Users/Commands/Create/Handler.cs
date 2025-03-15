using System.Net;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EMS.Application.Features.Users.Commands.Create
{
    public sealed class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, ResponseOf<CreateUserResult>>
    {
        private readonly IJWTManager _jwtManager;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJobs _jobs;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CreateUserCommandHandler(
            IJWTManager jwtManager,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher<User> passwordHasher,
            IJobs jobs
        )
        {
            _jwtManager = jwtManager;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jobs = jobs;
        }

        public async Task<ResponseOf<CreateUserResult>> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    User user = request;
                    user.HashPassword(_passwordHasher, request.password);
                    user.Employee = new() { JoiningDate = DateTime.UtcNow };
                    EntityEntry<User> result = await _userRepository.CreateAsync(
                        user,
                        cancellationToken
                    );

                    Token token = await _jwtManager.GenerateTokenAsync(user);
                    user.Token = token;

                    string code = await _jwtManager.GenerateCodeAsync();
                    user.HashedCode(_passwordHasher, code);

                    int success = await _unitOfWork.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    await _jobs.SendEmailByJobAsync(
                        user.Email,
                        $"To Confirm Email Code: <h3>{code}</h3>"
                    );
                    return new ResponseOf<CreateUserResult>
                    {
                        Message = "Success.",
                        Success = true,
                        StatusCode = (int)HttpStatusCode.OK,
                        Result = user,
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception(ex.Message, ex);
                }
            }
        }
    }
}
