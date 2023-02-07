using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace EnigmatShopAPI.Helpers
{
    public class Utility
    {
        public static string EncryptPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] encryptedPassword = ProtectedData.Protect(passwordBytes, salt, DataProtectionScope.CurrentUser);
            string encryptedPasswordBase64 = Convert.ToBase64String(encryptedPassword);

            return encryptedPasswordBase64;
        }

        // decrypt
        public static string DecryptPassword(string encryptedPasswordBase64)
        {
            byte[] encryptedPassword = Convert.FromBase64String(encryptedPasswordBase64);
            byte[] passwordBytes = ProtectedData.Unprotect(encryptedPassword, null, DataProtectionScope.CurrentUser);
            string password = Encoding.UTF8.GetString(passwordBytes);

            return password;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        // contoh refresh token save di cache
        private void SaveRefreshToken(int userId, string refreshToken, IMemoryCache _cache)
        {
            // Simpan refresh token pada database atau cache
            _cache.Set(refreshToken, userId, new TimeSpan(30, 0, 0, 0));
        }

        private int? ValidateRefreshToken(string refreshToken, IMemoryCache _cache)
        {
            // Validasi refresh token dan hapus refresh token jika valid
            if (_cache.TryGetValue(refreshToken, out int userId))
            {
                _cache.Remove(refreshToken);
                return userId;
            }

            return null;
        }
    }
}
