using EMS.Domain.Base;

namespace EMS.Domain.Entities
{
    public sealed record Token : Entity<Guid>
    {
        private Token(string content, DateTime expireOn, Guid userId)
        {
            Content = content;
            ExpireOn = expireOn;
            UserId = userId;
        }

        public string Content { get; set; }
        public DateTime CreateOn => DateTime.Now;
        public DateTime ExpireOn { get; set; }
        public bool IsExpire => ExpireOn <= DateTime.Now;
        public bool IsValid => !IsExpire;
        public Guid UserId { get; set; }
        public User User { get; set; }

        public static Token Create(string content, DateTime expireOn, Guid userId) =>
            new(content, expireOn, userId);
    }
}
