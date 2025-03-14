using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EMS.Application.Responses;
using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EMS.Application.Features.Users.Queries.Login
{
    public sealed class LoginUserHandler
        : IRequestHandler<LoginUserQuery, ResponseOf<LoginUserResult>>
    {
        private readonly IJWTManager _jwtManager;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public LoginUserHandler(
            IJWTManager jwtManager,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher<User> passwordHasher
        )
        {
            _jwtManager = jwtManager;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseOf<LoginUserResult>> Handle(
            LoginUserQuery request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    User user = await _userRepository.GetAsync(
                        u => u.Email == request.email,
                        astracking: false
                    );
                    if (!user.ConfirmAccount)
                        throw new Exception("Email Not Confirmed.");

                    if (!VerifyPassword(user, user.HashedPassword, request.password))
                        throw new Exception("Incorrect Password.");

                    return new ResponseOf<LoginUserResult>
                    {
                        Message = "Success.",
                        Success = true,
                        StatusCode = (int)HttpStatusCode.OK,
                        Result = await _jwtManager.LoginAsync(user),
                    };
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private bool VerifyPassword(User user, string hashedPassword, string password) =>
            _passwordHasher.VerifyHashedPassword(user, hashedPassword, password)
            == PasswordVerificationResult.Success;
    }
}
