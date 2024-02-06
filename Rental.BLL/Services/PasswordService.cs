using System.Security.Cryptography;

namespace Rental.BLL.Services
{
    public class PasswordService
    {
        private const int salt_size = 16;
        private const int hash_size = 32;
        private const int iterations = 10000;
        private static HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(salt_size);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithmName, hash_size);

            return string.Join(";", Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool AuthPassword(string storedPassword, string inputPassword)
        {
            var elements = storedPassword.Split(";");
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var inputHash = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, iterations, hashAlgorithmName, hash_size);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
