using System.Security.Cryptography;
using System.Text;

namespace BFPR4B.EHiring.ApiService.Helpers.Utility
{
    public static class EncryptionHelper
    {
        private const int KeySize = 32; // 256 bits
        private const int IvSize = 16;  // 128 bits
        private const int SaltSize = 16;
        private const int HmacSize = 32; // 256-bit HMAC
        private const int Iterations = 100_000;

        private static readonly string _baseKey;

        static EncryptionHelper()
        {
            _baseKey = Environment.GetEnvironmentVariable("ENCRYPTION_KEY")
                ?? throw new InvalidOperationException("Missing required environment variable: ENCRYPTION_KEY");
        }

        public static string Encrypt(string plainText, string additionalKey) => EncryptInternal(plainText, additionalKey);

        public static string Decrypt(string encryptedBase64, string additionalKey) => DecryptInternal(encryptedBase64, additionalKey);

        private static string EncryptInternal(string plainText, string? additionalKey)
        {
            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            var iv = new byte[IvSize];
            rng.GetBytes(salt);
            rng.GetBytes(iv);

            var fullPassphrase = CombineKeys(_baseKey, additionalKey);
            var aesKey = DeriveKey(fullPassphrase, salt);
            var hmacKey = DeriveKey(fullPassphrase + "|HMAC", salt); // separate key for HMAC

            byte[] cipherBytes;
            using (var aes = Aes.Create())
            {
                aes.Key = aesKey;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using var ms = new MemoryStream();
                using (var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cryptoStream, Encoding.UTF8))
                {
                    writer.Write(plainText);
                }

                cipherBytes = ms.ToArray();
            }

            // Combine salt + iv + ciphertext
            var payload = new byte[SaltSize + IvSize + cipherBytes.Length];
            Buffer.BlockCopy(salt, 0, payload, 0, SaltSize);
            Buffer.BlockCopy(iv, 0, payload, SaltSize, IvSize);
            Buffer.BlockCopy(cipherBytes, 0, payload, SaltSize + IvSize, cipherBytes.Length);

            // Compute HMAC
            byte[] hmac;
            using (var hmacsha256 = new HMACSHA256(hmacKey))
            {
                hmac = hmacsha256.ComputeHash(payload);
            }

            // Final payload = salt + iv + ciphertext + hmac
            var finalPayload = new byte[payload.Length + HmacSize];
            Buffer.BlockCopy(payload, 0, finalPayload, 0, payload.Length);
            Buffer.BlockCopy(hmac, 0, finalPayload, payload.Length, HmacSize);

            return Convert.ToBase64String(finalPayload);
        }

        private static string DecryptInternal(string encryptedBase64, string? additionalKey)
        {
            var fullData = Convert.FromBase64String(encryptedBase64);

            if (fullData.Length < SaltSize + IvSize + HmacSize)
                throw new CryptographicException("Invalid encrypted data format.");

            var payloadLength = fullData.Length - HmacSize;

            var salt = new byte[SaltSize];
            var iv = new byte[IvSize];
            var cipherText = new byte[payloadLength - SaltSize - IvSize];
            var hmac = new byte[HmacSize];

            Buffer.BlockCopy(fullData, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(fullData, SaltSize, iv, 0, IvSize);
            Buffer.BlockCopy(fullData, SaltSize + IvSize, cipherText, 0, cipherText.Length);
            Buffer.BlockCopy(fullData, payloadLength, hmac, 0, HmacSize);

            var fullPassphrase = CombineKeys(_baseKey, additionalKey);
            var aesKey = DeriveKey(fullPassphrase, salt);
            var hmacKey = DeriveKey(fullPassphrase + "|HMAC", salt); // use same derivation logic

            // Recompute and verify HMAC
            var payload = new byte[payloadLength];
            Buffer.BlockCopy(fullData, 0, payload, 0, payloadLength);

            using var hmacsha256 = new HMACSHA256(hmacKey);
            var computedHmac = hmacsha256.ComputeHash(payload);

            if (!CryptographicOperations.FixedTimeEquals(computedHmac, hmac))
                throw new CryptographicException("HMAC validation failed. Data may be tampered.");

            // Proceed with decryption
            using var aes = Aes.Create();
            aes.Key = aesKey;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var ms = new MemoryStream(cipherText);
            using var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream, Encoding.UTF8);

            return reader.ReadToEnd();
        }

        private static byte[] DeriveKey(string fullPassphrase, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(fullPassphrase, salt, Iterations, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(KeySize);
        }

        private static string CombineKeys(string envKey, string? additionalKey)
        {
            return additionalKey == null ? envKey : $"{envKey}:{additionalKey}";
        }
    }
}
