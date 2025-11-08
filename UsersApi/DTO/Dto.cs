namespace UsersApi.DTO
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
    }

    public class UserUpdateDto
    {
        public string Name { get; set; }
        public int RoleId { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RefreshTokenDto
    {
        public string RefreshToken { get; set; }
    }

    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

}
