using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace EnigmatShopAPI.Helpers
{
    public class Utility
    {
        public static string Encrypt(string password)
        {
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] encryptedPassword = ProtectedData.Protect(passwordBytes, salt, DataProtectionScope.CurrentUser);
            string encryptedPasswordBase64 = Convert.ToBase64String(encryptedPassword);

            return encryptedPasswordBase64;
        }

        // decrypt (!IMPORTANT DONT SHARE IT TO ANYONE WHO NEED IT)
        public static string Decrypt(string encryptedPasswordBase64)
        {
            byte[] encryptedPassword = Convert.FromBase64String(encryptedPasswordBase64);
            byte[] passwordBytes = ProtectedData.Unprotect(encryptedPassword, null, DataProtectionScope.CurrentUser);
            string password = Encoding.UTF8.GetString(passwordBytes);

            return password;
        }

        public static string EncryptPassword(string password)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new Rfc2898DeriveBytes("key", Encoding.UTF8.GetBytes("salt")).GetBytes(16);
            using var aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV();
            using var memoryStream = new MemoryStream();
            memoryStream.Write(aes.IV, 0, 16);
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string DecryptPassword(string encrypted)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encrypted);
            byte[] keyBytes = new Rfc2898DeriveBytes("key", Encoding.UTF8.GetBytes("salt")).GetBytes(16);
            using var aes = Aes.Create();
            aes.Key = keyBytes;
            byte[] iv = new byte[16];
            Array.Copy(cipherTextBytes, 0, iv, 0, 16);
            aes.IV = iv;
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherTextBytes, 16, cipherTextBytes.Length - 16);
            cryptoStream.FlushFinalBlock();
            byte[] plainTextBytes = memoryStream.ToArray();
            return Encoding.UTF8.GetString(plainTextBytes);
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
