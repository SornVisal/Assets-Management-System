using System;
using System.Security.Cryptography;
using System.Text;

namespace Assets_Management_System.Services
{
    public static class PasswordHelper
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;
        private const int Iterations = 10000;

        /// <summary>
        /// Hash a password using PBKDF2
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty");

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);

                    // Combine salt and hash
                    byte[] hashWithSalt = new byte[SaltSize + HashSize];
                    Array.Copy(salt, 0, hashWithSalt, 0, SaltSize);
                    Array.Copy(hash, 0, hashWithSalt, SaltSize, HashSize);

                    // Return as Base64 string
                    return Convert.ToBase64String(hashWithSalt);
                }
            }
        }

        /// <summary>
        /// Verify a password against its hash
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            try
            {
                byte[] hashBytes = Convert.FromBase64String(hash);

                // Extract salt
                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                // Extract hash
                byte[] storedHash = new byte[HashSize];
                Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize);

                // Compute hash of input password
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] computedHash = pbkdf2.GetBytes(HashSize);

                    // Compare hashes
                    return AreHashesEqual(storedHash, computedHash);
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Constant-time comparison to prevent timing attacks
        /// </summary>
        private static bool AreHashesEqual(byte[] hash1, byte[] hash2)
        {
            int minLength = Math.Min(hash1.Length, hash2.Length);
            int result = hash1.Length ^ hash2.Length;

            for (int i = 0; i < minLength; i++)
                result |= hash1[i] ^ hash2[i];

            return result == 0;
        }
    }
}
