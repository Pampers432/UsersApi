using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using RegistrationApi.Contracts;
using System.Security.Claims;
using UsersApi.Models;
using UsersApi.Abstraction;

namespace RegistrationApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("email", user.Email),
                new Claim("userId", user.Id.ToString()),
                new Claim("role", user.UserRole_Id.ToString()),
                new Claim("email_confirmed", user.IsEmailConfirmed.ToString().ToLower()),
                new Claim("is_active", user.IsActive.ToString().ToLower()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpireMinutes)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
