using System.Security.Cryptography;

namespace BFPR4B.EHiring.ApiService.Helpers.Utility
{
    public static class PasswordHelper
    {
        // Recommended defaults based on current best practices (as of 2025)
        // Adjust iteration count upwards as computing power increases.
        private const int SaltSize = 32; // 256 bits
        private const int HashSize = 32; // 256 bits
        private const int Iterations = 310000; // Increased from 100,000 for more modern security.
                                               // Consider higher values like 600,000 or more if performance allows.

        /// <summary>
        /// Generates a cryptographically strong random salt.
        /// </summary>
        /// <returns>A byte array representing the salt.</returns>
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create()) // More modern and recommended than RNGCryptoServiceProvider
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Hashes a password using PBKDF2 with a generated salt.
        /// The salt and hash are combined into a single Base64 encoded string for storage.
        /// </summary>
        /// <param name="password">The plain-text password to hash.</param>
        /// <returns>A Base64 encoded string containing the salt and hash, or null if the password is null or empty.</returns>
        public static string GenerateHashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
            }

            byte[] salt = GenerateSalt();
            byte[] hash;

            // Using Rfc2898DeriveBytes for PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                hash = pbkdf2.GetBytes(HashSize);
            }

            // Combine salt and hash for storage
            byte[] saltAndHash = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, saltAndHash, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, saltAndHash, SaltSize, HashSize);

            return Convert.ToBase64String(saltAndHash);
        }

        /// <summary>
        /// Verifies a password against a stored salt and hash.
        /// </summary>
        /// <param name="password">The plain-text password to verify.</param>
        /// <param name="storedSaltAndHash">The Base64 encoded string containing the stored salt and hash.</param>
        /// <returns>True if the password matches, false otherwise.</returns>
        public static bool VerifyPassword(string password, string storedSaltAndHash)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(storedSaltAndHash))
            {
                throw new ArgumentNullException(nameof(storedSaltAndHash), "Stored salt and hash cannot be null or empty.");
            }

            byte[] saltAndHashBytes = Convert.FromBase64String(storedSaltAndHash);

            if (saltAndHashBytes.Length != SaltSize + HashSize)
            {
                // This indicates a corrupted or invalid stored hash format
                return false;
            }

            byte[] salt = new byte[SaltSize];
            byte[] storedHash = new byte[HashSize];

            Buffer.BlockCopy(saltAndHashBytes, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(saltAndHashBytes, SaltSize, storedHash, 0, HashSize);

            byte[] calculatedHash;

            // Recalculate hash with the extracted salt and the provided password
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                calculatedHash = pbkdf2.GetBytes(HashSize);
            }

            // Compare the calculated hash with the stored hash using a constant-time comparison
            return CryptographicOperations.FixedTimeEquals(calculatedHash, storedHash);
        }
    }
}
