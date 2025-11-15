using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersApi.Models;

namespace UsersApi.Contracts
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<string> RefreshTokenAsync(RefreshTokenDto dto);
        Task<bool> ConfirmEmailAsync(string token);
        Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<bool> DeleteUserAsync(Guid id);
        Task<bool> DeactivateUserAsync(Guid id);
        Task<bool> ActivateUserAsync(Guid id);
    }
}
