using UsersApi.Models;

namespace UsersApi.Abstraction
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}