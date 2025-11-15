using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models
{
    public enum TokenType
    {
        EmailConfirmation = 0,
        PasswordReset = 1
    }

    public class UserToken
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; } 

        [Required]
        public TokenType Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public UserToken() { }

        private UserToken(Guid userId, string token, TokenType type)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Token = token;
            Type = type;
            CreatedAt = DateTime.UtcNow;
        }

        public static UserToken Create(Guid userId, string token, TokenType type)
        {
            return new UserToken(userId, token, type);
        }
    }
}
