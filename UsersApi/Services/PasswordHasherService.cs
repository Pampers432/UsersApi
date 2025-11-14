using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using UsersApi.Contracts;

namespace UsersApi.Services
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private const int SaltSize = 16; // 128 bits
        private const int HashSize = 32; // 256 bits
        private const int Iterations = 10000; // Количество итераций PBKDF2

        /// <summary>
        /// Хэширует пароль и возвращает хэш и соль
        /// </summary>
        /// <param name="password">Пароль в чистом виде</param>
        /// <returns>Кортеж (хэш, соль)</returns>
        public (string Hash, string Salt) HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty", nameof(password));

            // Генерация случайной соли
            byte[] saltBytes = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            // Хэширование пароля с помощью PBKDF2
            byte[] hashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: HashSize);

            // Конвертация в base64 для хранения
            string hash = Convert.ToBase64String(hashBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return (hash, salt);
        }

        /// <summary>
        /// Проверяет пароль против сохраненного хэша и соли
        /// </summary>
        /// <param name="password">Пароль для проверки</param>
        /// <param name="storedHash">Сохраненный хэш</param>
        /// <param name="storedSalt">Сохраненная соль</param>
        /// <returns>True если пароль верный</returns>
        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(storedHash) ||
                string.IsNullOrWhiteSpace(storedSalt))
            {
                return false;
            }

            try
            {
                // Конвертация соли из base64
                byte[] saltBytes = Convert.FromBase64String(storedSalt);
                byte[] storedHashBytes = Convert.FromBase64String(storedHash);

                // Хэширование введенного пароля с той же солью
                byte[] hashBytes = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: Iterations,
                    numBytesRequested: HashSize);

                // Сравнение хэшей (защищено от timing attacks)
                return CryptographicOperations.FixedTimeEquals(hashBytes, storedHashBytes);
            }
            catch (FormatException)
            {
                // Некорректный формат base64
                return false;
            }
        }

        /// <summary>
        /// Быстрая проверка сложности пароля (базовая валидация)
        /// </summary>
        /// <param name="password">Пароль для проверки</param>
        /// <returns>True если пароль соответствует минимальным требованиям</returns>
        public bool ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Минимальные требования к паролю
            return password.Length >= 8 &&
                   password.Length <= 32 &&
                   ContainsDigit(password) &&
                   ContainsLetter(password);
        }

        /// <summary>
        /// Генерация случайного пароля
        /// </summary>
        /// <param name="length">Длина пароля (по умолчанию 12)</param>
        /// <returns>Случайный пароль</returns>
        public string GenerateRandomPassword(int length = 12)
        {
            if (length < 8)
                throw new ArgumentException("Password length must be at least 8 characters", nameof(length));

            const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()-_=+[]{}|;:,.<>?";

            var allChars = uppercase + lowercase + digits + special;
            var passwordChars = new char[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                // Гарантируем наличие хотя бы одного символа каждого типа
                passwordChars[0] = uppercase[GetRandomIndex(rng, uppercase.Length)];
                passwordChars[1] = lowercase[GetRandomIndex(rng, lowercase.Length)];
                passwordChars[2] = digits[GetRandomIndex(rng, digits.Length)];
                passwordChars[3] = special[GetRandomIndex(rng, special.Length)];

                // Заполняем остальные позиции случайными символами
                for (int i = 4; i < length; i++)
                {
                    passwordChars[i] = allChars[GetRandomIndex(rng, allChars.Length)];
                }

                // Перемешиваем символы
                Shuffle(passwordChars, rng);
            }

            return new string(passwordChars);
        }

        private static int GetRandomIndex(RandomNumberGenerator rng, int maxValue)
        {
            byte[] randomNumber = new byte[4];
            rng.GetBytes(randomNumber);
            return Math.Abs(BitConverter.ToInt32(randomNumber, 0)) % maxValue;
        }

        private static void Shuffle(char[] array, RandomNumberGenerator rng)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = GetRandomIndex(rng, i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        private static bool ContainsDigit(string password)
        {
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                    return true;
            }
            return false;
        }

        private static bool ContainsLetter(string password)
        {
            foreach (char c in password)
            {
                if (char.IsLetter(c))
                    return true;
            }
            return false;
        }
    }
}