namespace UsersApi.DTO
{
    public record UserDto(
        Guid Id,
        string Name,
        string Email,
        string Role,
        bool IsActive,
        bool IsEmailConfirmed
    );

    public record RegisterDto(
        string Name,
        string Email,
        string Password
    );

    public record UserUpdateDto(
        string Name,
        int RoleId
    );

    public record LoginDto(
        string Email,
        string Password
    );

    public record RefreshTokenDto(
        string RefreshToken
    );

    public record ForgotPasswordDto(
        string Email
    );

    public record ResetPasswordDto(
        string Token,
        string NewPassword
    );
}
