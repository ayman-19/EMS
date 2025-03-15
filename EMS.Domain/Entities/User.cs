using EMS.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace EMS.Domain.Entities
{
    public sealed record User : Entity<Guid>
    {
        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool ConfirmAccount { get; set; }
        public string HashedPassword { get; set; }
        public string? Code { get; set; }
        public Employee Employee { get; set; }
        public Token Token { get; set; }

        public static User Create(string name, string email) => new(name, email);

        public void HashPassword(IPasswordHasher<User> passwordHasher, string password) =>
            HashedPassword = passwordHasher.HashPassword(this, password);

        public void HashedCode(IPasswordHasher<User> passwordHasher, string code) =>
            Code = passwordHasher.HashPassword(this, code);
    }
}
