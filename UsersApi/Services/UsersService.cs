using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersApi.Contracts;
using UsersApi.DTO;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var existingUser = _repository.GetByEmailAsync(dto.Email);
            if (existingUser != null) return string.Empty;

            // TODO: хэширование пароля + генерация соли
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Password_Hash = dto.Password, // TODO: заменить на хэш
                Salt = "TODO",                // TODO: заменить на соль
                UserRole_Id = 1,
                IsActive = true,
                IsEmailConfirmed = false
            };

            await _repository.AddAsync(user);

            // TODO: генерация JWT токена
            return "TODO: token"; ;
        }

        // --- Логин ---
        public async Task<string> LoginAsync(LoginDto dto)
        {
            // TODO: проверить email + пароль через репозиторий
            return "TODO: token";
        }

        // --- Refresh токена ---
        public async Task<string> RefreshTokenAsync(RefreshTokenDto dto)
        {
            // TODO: обновление токена
            return "TODO: new_token";
        }

        // --- Подтверждение email ---
        public async Task<bool> ConfirmEmailAsync(string token)
        {
            // TODO: найти пользователя по токену и подтвердить email
            return false;
        }

        // --- Восстановление пароля ---
        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            // TODO: создать токен восстановления и отправить письмо
            return false;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            // TODO: найти пользователя по токену и обновить пароль
            return false;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => new UserDto(
                u.Id,
                u.Name,
                u.Email,
                u.Role.Role,
                u.IsActive,
                u.IsEmailConfirmed))
                .ToList();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            // TODO: логика удаления пользователя
            return await Task.FromResult(true);
        }

        public async Task<bool> DeactivateUserAsync(Guid id)
        {
            // TODO: логика деактивации + SoftDelete продуктов
            return await Task.FromResult(true);
        }

        public async Task<bool> ActivateUserAsync(Guid id)
        {
            // TODO: логика активации + восстановление продуктов
            return await Task.FromResult(true);
        }
    }
}
