using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UsersApi.Contracts;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        // --- Аутентификация / Авторизация ---

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var token = await _userService.RegisterAsync(dto);

                if (string.IsNullOrEmpty(token))
                    return BadRequest(new { error = "Ошибка регистрации" });

                // Опционально: установка токена в куки
                Response.Cookies.Append("TastyCoks", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });

                return Ok(new { token = token });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Внутренняя ошибка сервера" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _userService.LoginAsync(dto);

                if (string.IsNullOrEmpty(token))
                    return Unauthorized(new { error = "Неверный email или пароль" });

                // Установка токена в куки
                Response.Cookies.Append("TastyCoks", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });

                return Ok(new { token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Внутренняя ошибка сервера" });
            }
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
        // Email: admin123admin@gmail.com password:Admin123Admin
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Логирование ошибки (реализуйте при необходимости)
                // _logger.LogError(ex, "Ошибка при получении списка пользователей");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        //[HttpGet("debug-my-token")]
        //[Authorize] // Любой аутентифицированный пользователь
        //public IActionResult DebugMyToken()
        //{
        //    var allClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

        //    return Ok(new
        //    {
        //        AllClaims = allClaims,
        //        HasRoleClaim = User.HasClaim(c => c.Type == "role"),
        //        RoleValue = User.FindFirst("role")?.Value,
        //        IsAdmin = User.FindFirst("role")?.Value == "2"
        //    });
        //}

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
