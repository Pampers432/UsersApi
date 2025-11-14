namespace UsersApi.Contracts
{
    public interface IPasswordHasherService
    {
        string GenerateRandomPassword(int length = 12);
        (string Hash, string Salt) HashPassword(string password);
        bool ValidatePasswordStrength(string password);
        bool VerifyPassword(string password, string storedHash, string storedSalt);
    }
}