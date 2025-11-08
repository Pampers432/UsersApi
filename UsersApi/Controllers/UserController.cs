using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UsersApi.DTO;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // --- Аутентификация / Авторизация ---

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // TODO: логика выдачи токена
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // TODO: логика выдачи токена
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            // TODO: логика обновления токена
            return Ok();
        }

        // --- Подтверждение аккаунта ---

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
        {
            // TODO: логика подтверждения
            return Ok();
        }

        // --- Восстановление пароля ---

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            // TODO: логика генерации токена
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            // TODO: логика сброса пароля
            return Ok();
        }

        // --- Управление пользователями (Админ) ---

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // TODO: логика получения списка
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            // TODO: логика удаления
            return Ok();
        }

        [HttpPost("{id:guid}/deactivate")]
        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            // TODO: логика деактивации + SoftDelete продуктов
            return Ok();
        }

        [HttpPost("{id:guid}/activate")]
        public async Task<IActionResult> ActivateUser(Guid id)
        {
            // TODO: логика активации + восстановление продуктов
            return Ok();
        }
    }
}
