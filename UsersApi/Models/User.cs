using System;
using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Хэш пароля обязателен")]
        [MinLength(32, ErrorMessage = "Хэш пароля должен быть не менее 32 символов")]
        public string Password_Hash { get; set; }

        [Required(ErrorMessage = "Соль обязательна")]
        [MinLength(16, ErrorMessage = "Соль должна быть не менее 16 символов")]
        public string Salt { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsEmailConfirmed { get; set; } = false;

        public int UserRole_Id { get; set; }

        public UserRole Role { get; set; }

        public User() { }

        private User(string name, string email, string password_Hash, string salt, int userRole_Id)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password_Hash = password_Hash;
            Salt = salt;
            UserRole_Id = userRole_Id;
        }

        public static User CreateUser(string name, string email, string password_Hash, string salt, int userRole_Id)
        {
            return new User(name, email, password_Hash, salt, userRole_Id);
        }
    }
}
