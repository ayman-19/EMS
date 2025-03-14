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

namespace EMS.Application.Features.Users.Commands.Confirm
{
    public sealed class ConfirmUserHandler : IRequestHandler<ConfirmUserCommand, Response>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;

        public ConfirmUserHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordHasher<User> passwordHasher
        )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<Response> Handle(
            ConfirmUserCommand request,
            CancellationToken cancellationToken
        )
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    User user = await _userRepository.GetAsync(u => u.Email == request.email);
                    if (
                        _passwordHasher.VerifyHashedPassword(user, user.Code, request.code)
                        != PasswordVerificationResult.Success
                    )
                        throw new Exception("Verify Code.");

                    user.ConfirmAccount = true;
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return new Response
                    {
                        Success = true,
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Success.",
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
