using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EMS.Domain.Abstraction;
using EMS.Domain.Entities;
using EMS.Persistence.Context;
using EMS.Persistence.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EMS.Persistence.Repositories
{
    public sealed class JWTManager : IJWTManager
    {
        private readonly EMSDbContext _context;

        public JWTManager(EMSDbContext context)
        {
            _context = context;
        }

        public Task<string> GenerateCodeAsync() =>
            Task.FromResult(new Random().Next(1, 100000).ToString("D6"));

        public Task<Token> GenerateTokenAsync(
            User user,
            CancellationToken cancellationToken = default
        )
        {
            var Claims = GetClaims(user);
            var symetreckey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));
            var signCreadential = new SigningCredentials(
                symetreckey,
                SecurityAlgorithms.HmacSha256
            );
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: JwtSettings.Issuer,
                audience: JwtSettings.Audience,
                claims: Claims,
                signingCredentials: signCreadential,
                expires: DateTime.Now.AddMonths((int)JwtSettings.AccessTokenExpireDate)
            );
            return Task.FromResult(
                Token.Create(
                    new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    jwtSecurityToken.ValidTo,
                    user.Id
                )
            );
        }

        public async Task<User> LoginAsync(User user, CancellationToken cancellationToken = default)
        {
            if (user.Token != null)
            {
                if (user.Token.IsValid)
                    return user;
                else
                {
                    await _context
                        .Set<Token>()
                        .Where(t => t.UserId == user.Id)
                        .ExecuteDeleteAsync(cancellationToken);
                    user.Token = await GenerateTokenAsync(user, cancellationToken);
                    await _context.SaveChangesAsync();
                    return user;
                }
            }
            else
            {
                user.Token = await GenerateTokenAsync(user, cancellationToken);
                await _context.SaveChangesAsync();
                return user;
            }
        }

        private IReadOnlyCollection<Claim> GetClaims(User user)
        {
            List<Claim> claims = new();

            claims.Add(new Claim("UserId", user.Id.ToString()));

            return claims;
        }
    }
}
