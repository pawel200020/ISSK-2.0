using System.Security.Cryptography;
using System.Text;

namespace Configuration.Encryption;

internal class EncryptionManager : IEncryptionManager
{
    /// <summary>
    /// Encrypts plaintext using AES-256 with CBC mode and random IV.
    /// IV is prepended to the ciphertext for storage.
    /// </summary>
    public string Encrypt(string plaintext, string key)
    {
        if (string.IsNullOrEmpty(plaintext))
            throw new ArgumentNullException(nameof(plaintext));
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        // Derive a proper 256-bit key using SHA256
        byte[] keyArray = DeriveKey(key);
        byte[] toEncryptArray = Encoding.UTF8.GetBytes(plaintext);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyArray;
            aes.Mode = CipherMode.CBC; // Secure mode instead of CFB
            aes.Padding = PaddingMode.PKCS7;
            aes.GenerateIV(); // Generate random IV (CRITICAL FIX!)

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (MemoryStream ms = new MemoryStream())
            {
                // Write IV to the beginning (it's not secret, just needs to be stored)
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(toEncryptArray, 0, toEncryptArray.Length);
                    cs.FlushFinalBlock();
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    /// <summary>
    /// Decrypts ciphertext that was encrypted with Encrypt method.
    /// Expects IV to be prepended to the ciphertext.
    /// </summary>
    public string Decrypt(string ciphertext, string key)
    {
        if (string.IsNullOrEmpty(ciphertext))
            throw new ArgumentNullException(nameof(ciphertext));
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key));

        byte[] keyArray = DeriveKey(key);
        byte[] cipherBytes = Convert.FromBase64String(ciphertext);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyArray;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Extract IV from the beginning of ciphertext (first 16 bytes)
            byte[] iv = new byte[16];
            Array.Copy(cipherBytes, 0, iv, 0, iv.Length);
            aes.IV = iv;

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (MemoryStream ms = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }

    /// <summary>
    /// Derives a secure 256-bit key from a passphrase using SHA256.
    /// This is better than direct UTF8 encoding which can produce variable-length keys.
    /// </summary>
    private byte[] DeriveKey(string key)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
    }
}