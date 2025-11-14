using RegistrationApi.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersApi.Abstraction;
using UsersApi.Contracts;
using UsersApi.DTO;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository repository, IPasswordHasherService passwordHasher, IJwtService jwtService)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _repository.GetByEmailAsync(dto.Email);
            if (existingUser != null) return string.Empty;

            if (!_passwordHasher.ValidatePasswordStrength(dto.Password)) return string.Empty;

            var (hash, salt) = _passwordHasher.HashPassword(dto.Password);

            var user = User.CreateUser(
                name: dto.Name,
                email: dto.Email,
                password_Hash: hash,
                salt: salt,
                userRole_Id: 1
            );

            await _repository.AddAsync(user);

            return _jwtService.GenerateToken(user);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);

            if (user == null || !user.IsActive) return string.Empty;

            if (!_passwordHasher.VerifyPassword(dto.Password, user.Password_Hash, user.Salt)) return string.Empty;

            //if (!user.IsEmailConfirmed) return string.Empty;

            return _jwtService.GenerateToken(user);
        }

        public async Task<string> RefreshTokenAsync(RefreshTokenDto dto)
        {
            // Базовая реализация refresh токена
            // В реальном приложении нужно:
            // 1. Проверить refresh token в БД
            // 2. Найти пользователя по refresh token
            // 3. Сгенерировать новый access token
            // 4. Обновить refresh token в БД
            return string.Empty;
        }



        public async Task<bool> ConfirmEmailAsync(string token)
        {
            // TODO: найти пользователя по токену и подтвердить email

            return false;
        }

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
            await _repository.DeleteAsync(id);

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
